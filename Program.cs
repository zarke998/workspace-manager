using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkspaceManager.Core;
using WorkspaceManager.Core.Domain;

namespace WorkspaceManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

            //TestApplication();
        }

        private static void TestApplication()
        {
            var visibleProcesses = WindowManager.GetVisibleWindowsProcessses();
            var chromeProcess = visibleProcesses.Single(p => p.Item1.ProcessName == "chrome");

            //var rect = WindowManager.GetWindowPosition(chromeProcess.MainWindowHandle);

            //rect.Left += 500;
            //rect.Right += 500;

            //WindowManager.SetWindowPosition(chromeProcess.MainWindowHandle, rect);
            Console.ReadLine();
        }
    }
}
