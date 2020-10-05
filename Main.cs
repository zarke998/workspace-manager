using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WorkspaceManager.Core;
using WorkspaceManager.Core.Domain;
using ProgramInfo = WorkspaceManager.Core.Domain.Program;

namespace WorkspaceManager
{
    public partial class Main : Form
    {
        private readonly string _appFolder;
        private readonly string _profilesFolder;

        private ICollection<Profile> _profiles;

        private static readonly string tbProfileNamePlaceholder = "Profile name...";

        public Main()
        {
            _appFolder = AppDomain.CurrentDomain.BaseDirectory;
            _profilesFolder = Path.Combine(_appFolder, "profiles");

            _profiles = new List<Profile>(GetAllProfiles());

            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            InitializeTbProfileName();
            PopulateProfilesCombo(_profiles);
            PopulateProgramsCombo();
        }

        #region EventHandlers
        private void btnNewProfile_Click(object sender, EventArgs e)
        {
            var profileName = tbProfileName.Text;
            if (!ValidateProfileName(profileName))
            {
                return;
            }

            var profile = new Profile();
            var programs = new List<ProgramInfo>();

            var visibleWindowsProcesses = WindowManager.GetVisibleWindowsProcessses();
            foreach (var wp in visibleWindowsProcesses)
            {
                var program = new ProgramInfo();
                program.Name = wp.Item1.ProcessName;
                program.Path = wp.Item1.MainModule.FileName;

                if(wp.Item3 == null)
                {
                    program.Position = WindowManager.GetWindowPosition(wp.Item2);
                }
                else
                {
                    program.Position = WindowManager.GetWindowPosition(wp.Item3.MainWindowHandle);
                    program.HasWindowProcess = true;
                    program.WindowProcessName = wp.Item3.ProcessName;
                }

                programs.Add(program);
            }
            profile.Programs = programs.ToArray();
            profile.Name = profileName;


            Directory.CreateDirectory(Path.Combine(_profilesFolder));

            var serializer = new XmlSerializer(typeof(Profile));

            using (var writer = File.Open(Path.Combine(_profilesFolder, $"{profileName}.xml"), FileMode.Create))
            {
                serializer.Serialize(writer, profile);
            }
            _profiles.Add(profile);
            PopulateProfilesCombo(_profiles);
            PopulateProgramsCombo();
        }
        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            if (cbxProfiles.Items.Count == 0)
                return;

            var profile = cbxProfiles.SelectedItem as Profile;

            foreach(var p in profile.Programs)
            {
                var process = Process.Start(p.Path, p.Arguments);
                try
                {
                    if (p.HasWindowProcess)
                    {
                        var windowProcess = process.GetChildProcesses().First();

                        process.WaitForMainWindowHandle(10);
                        WindowManager.SetWindowPosition(windowProcess.MainWindowHandle, p.Position, windowProcess.ProcessName);
                    }
                    else
                    {
                        process.WaitForMainWindowHandle(10);
                        WindowManager.SetWindowPosition(process.MainWindowHandle, p.Position, process.ProcessName);
                    }
                    
                }
                catch(InvalidOperationException ex) // Process exits immediately after start (eg. Chrome)
                {
                    var processName = Path.GetFileNameWithoutExtension(p.Path);

                    //if (p.HasWindowProcess)
                    //{
                    //    var windowProcess = process.GetChildProcesses().First();
                    //    WindowManager.SetWindowPosition(windowProcess.MainWindowHandle, p.Position);
                    //}
                    WindowManager.SetWindowPosition(processName, p.Position);
                }
                
            }
        }

        private void cbxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProgramsCombo();
        }
        private void cbxPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            var program = cbxPrograms.SelectedItem as ProgramInfo;

            if(program == null)
            {
                tbArguments.Text = String.Empty;
                return;
            }
            tbArguments.Text = program.Arguments;
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            var arguments = tbArguments.Text;
            var program = cbxPrograms.SelectedItem as ProgramInfo;

            if (program == null)
                return;

            program.Arguments = arguments;

            var profile = cbxProfiles.SelectedItem as Profile;

            if (UpdateProfile(profile))
            {
                MessageBox.Show("Profile saved.");
            }
            else
            {
                MessageBox.Show("Error saving profile.");
            }
        }
        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            var profile = cbxProfiles.SelectedItem as Profile;

            if (profile == null)
                return;

            var xmlPath = Path.Combine(_profilesFolder, $"{profile.Name}.xml");

            File.Delete(xmlPath);
            _profiles.Remove(profile);

            PopulateProfilesCombo(_profiles);
            PopulateProgramsCombo();
        }
        #endregion

        private IEnumerable<Profile> GetAllProfiles()
        {
            ICollection<Profile> profiles = new List<Profile>();

            if (!Directory.Exists(_profilesFolder))
            {
                return profiles;
            }

            var profileXmls = Directory.GetFiles(_profilesFolder);

            var serializer = new XmlSerializer(typeof(Profile));
            foreach(var xml in profileXmls)
            {
                using(var stream = File.OpenRead(xml))
                {
                    var profile = serializer.Deserialize(stream) as Profile;
                    profiles.Add(profile);
                }
            }

            return profiles;
        }

        private bool ProfileNameExists(string name)
        {
            return _profiles.Select(p => p.Name).Contains(name);
        }
        private bool ValidateProfileName(string profileName)
        {
            if (profileName == tbProfileNamePlaceholder || profileName == String.Empty)
            {
                MessageBox.Show("Please input profile name.");
                return false;
            }
            else if (ProfileNameExists(profileName))
            {
                MessageBox.Show("Profile name already exists.");
                return false;
            }

            return true;
        }

        private void InitializeTbProfileName()
        {
            tbProfileName.Text = tbProfileNamePlaceholder;
            tbProfileName.GotFocus += (s, ev) =>
            {
                if (tbProfileName.Text == tbProfileNamePlaceholder)
                    tbProfileName.Text = String.Empty;
            };
            tbProfileName.LostFocus += (s, ev) =>
            {
                if (tbProfileName.Text == String.Empty)
                    tbProfileName.Text = tbProfileNamePlaceholder;
            };
        }

        private void PopulateProfilesCombo(IEnumerable<Profile> profiles)
        {
            cbxProfiles.DataSource = null;

            cbxProfiles.DataSource = profiles.ToList();
            cbxProfiles.DisplayMember = "Name";
        }
        private void PopulateProgramsCombo()
        {
            var profile = cbxProfiles.SelectedItem as Profile;

            if (profile == null)
            {
                cbxPrograms.DataSource = null;
                return;
            }

            cbxPrograms.DataSource = profile.Programs;
            cbxPrograms.DisplayMember = "Name";
        }

        private bool UpdateProfile(Profile profile)
        {
            var xmlPath = Path.Combine(_profilesFolder, $"{profile.Name}.xml");

            var serializer = new XmlSerializer(typeof(Profile));
            using(var stream = File.Open(xmlPath, FileMode.Create))
            {
                serializer.Serialize(stream, profile);
            }

            return true;
        }

    }
}