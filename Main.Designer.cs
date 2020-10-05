namespace WorkspaceManager
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabProfiles = new System.Windows.Forms.TabPage();
            this.btnDeleteProfile = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.tbProfileName = new System.Windows.Forms.TextBox();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.btnNewProfile = new System.Windows.Forms.Button();
            this.lblProfiles = new System.Windows.Forms.Label();
            this.cbxProfiles = new System.Windows.Forms.ComboBox();
            this.lblArguments = new System.Windows.Forms.Label();
            this.tbArguments = new System.Windows.Forms.TextBox();
            this.lblPrograms = new System.Windows.Forms.Label();
            this.cbxPrograms = new System.Windows.Forms.ComboBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabProfiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabProfiles);
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(275, 313);
            this.tabControl.TabIndex = 0;
            // 
            // tabProfiles
            // 
            this.tabProfiles.Controls.Add(this.btnDeleteProfile);
            this.tabProfiles.Controls.Add(this.btnSaveProfile);
            this.tabProfiles.Controls.Add(this.tbProfileName);
            this.tabProfiles.Controls.Add(this.btnLoadProfile);
            this.tabProfiles.Controls.Add(this.btnNewProfile);
            this.tabProfiles.Controls.Add(this.lblProfiles);
            this.tabProfiles.Controls.Add(this.cbxProfiles);
            this.tabProfiles.Controls.Add(this.lblArguments);
            this.tabProfiles.Controls.Add(this.tbArguments);
            this.tabProfiles.Controls.Add(this.lblPrograms);
            this.tabProfiles.Controls.Add(this.cbxPrograms);
            this.tabProfiles.Location = new System.Drawing.Point(4, 22);
            this.tabProfiles.Name = "tabProfiles";
            this.tabProfiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabProfiles.Size = new System.Drawing.Size(267, 287);
            this.tabProfiles.TabIndex = 0;
            this.tabProfiles.Text = "Profiles";
            this.tabProfiles.UseVisualStyleBackColor = true;
            // 
            // btnDeleteProfile
            // 
            this.btnDeleteProfile.Location = new System.Drawing.Point(94, 46);
            this.btnDeleteProfile.Name = "btnDeleteProfile";
            this.btnDeleteProfile.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteProfile.TabIndex = 9;
            this.btnDeleteProfile.Text = "Delete";
            this.btnDeleteProfile.UseVisualStyleBackColor = true;
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Location = new System.Drawing.Point(97, 151);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveProfile.TabIndex = 8;
            this.btnSaveProfile.Text = "Save";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // tbProfileName
            // 
            this.tbProfileName.Location = new System.Drawing.Point(11, 247);
            this.tbProfileName.Name = "tbProfileName";
            this.tbProfileName.Size = new System.Drawing.Size(239, 20);
            this.tbProfileName.TabIndex = 7;
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Location = new System.Drawing.Point(175, 46);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadProfile.TabIndex = 6;
            this.btnLoadProfile.Text = "Load";
            this.btnLoadProfile.UseVisualStyleBackColor = true;
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.Location = new System.Drawing.Point(97, 218);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(75, 23);
            this.btnNewProfile.TabIndex = 1;
            this.btnNewProfile.Text = "New Profile";
            this.btnNewProfile.UseVisualStyleBackColor = true;
            this.btnNewProfile.Click += new System.EventHandler(this.btnNewProfile_Click);
            // 
            // lblProfiles
            // 
            this.lblProfiles.AutoSize = true;
            this.lblProfiles.Location = new System.Drawing.Point(8, 20);
            this.lblProfiles.Name = "lblProfiles";
            this.lblProfiles.Size = new System.Drawing.Size(41, 13);
            this.lblProfiles.TabIndex = 5;
            this.lblProfiles.Text = "Profiles";
            // 
            // cbxProfiles
            // 
            this.cbxProfiles.FormattingEnabled = true;
            this.cbxProfiles.Location = new System.Drawing.Point(65, 17);
            this.cbxProfiles.Name = "cbxProfiles";
            this.cbxProfiles.Size = new System.Drawing.Size(185, 21);
            this.cbxProfiles.TabIndex = 4;
            this.cbxProfiles.SelectedIndexChanged += new System.EventHandler(this.cbxProfiles_SelectedIndexChanged);
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(8, 126);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(57, 13);
            this.lblArguments.TabIndex = 3;
            this.lblArguments.Text = "Arguments";
            // 
            // tbArguments
            // 
            this.tbArguments.Location = new System.Drawing.Point(65, 123);
            this.tbArguments.Name = "tbArguments";
            this.tbArguments.Size = new System.Drawing.Size(185, 20);
            this.tbArguments.TabIndex = 2;
            // 
            // lblPrograms
            // 
            this.lblPrograms.AutoSize = true;
            this.lblPrograms.Location = new System.Drawing.Point(8, 87);
            this.lblPrograms.Name = "lblPrograms";
            this.lblPrograms.Size = new System.Drawing.Size(51, 13);
            this.lblPrograms.TabIndex = 1;
            this.lblPrograms.Text = "Programs";
            // 
            // cbxPrograms
            // 
            this.cbxPrograms.FormattingEnabled = true;
            this.cbxPrograms.Location = new System.Drawing.Point(65, 84);
            this.cbxPrograms.Name = "cbxPrograms";
            this.cbxPrograms.Size = new System.Drawing.Size(185, 21);
            this.cbxPrograms.TabIndex = 0;
            this.cbxPrograms.SelectedIndexChanged += new System.EventHandler(this.cbxPrograms_SelectedIndexChanged);
            // 
            // tabSettings
            // 
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(267, 287);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 313);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Workspace Manager";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl.ResumeLayout(false);
            this.tabProfiles.ResumeLayout(false);
            this.tabProfiles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProfiles;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.ComboBox cbxPrograms;
        private System.Windows.Forms.Label lblPrograms;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.TextBox tbArguments;
        private System.Windows.Forms.Label lblProfiles;
        private System.Windows.Forms.ComboBox cbxProfiles;
        private System.Windows.Forms.Button btnNewProfile;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.TextBox tbProfileName;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnDeleteProfile;
    }
}

