namespace DashboardReminderService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dashboardserviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.dashboardServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // dashboardserviceProcessInstaller
            // 
            this.dashboardserviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.dashboardserviceProcessInstaller.Password = null;
            this.dashboardserviceProcessInstaller.Username = null;
            // 
            // dashboardServiceInstaller
            // 
            this.dashboardServiceInstaller.Description = "This service sends reminder mails for weekly status of the project";
            this.dashboardServiceInstaller.DisplayName = "DashboardReminderService";
            this.dashboardServiceInstaller.ServiceName = "DashboardReminderService";
            this.dashboardServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.dashboardServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.dashboardServiceInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.dashboardserviceProcessInstaller,
            this.dashboardServiceInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller dashboardserviceProcessInstaller;
        public System.ServiceProcess.ServiceInstaller dashboardServiceInstaller;
    }
}