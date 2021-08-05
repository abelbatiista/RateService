
namespace RateService
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
            this.rateServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.rateServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // rateServiceProcessInstaller
            // 
            this.rateServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.rateServiceProcessInstaller.Password = null;
            this.rateServiceProcessInstaller.Username = null;
            // 
            // rateServiceInstaller
            // 
            this.rateServiceInstaller.ServiceName = "RateService";
            this.rateServiceInstaller.DisplayName = "Rate Service";
            this.rateServiceInstaller.Description = "The service provides the dollar and japanesse yen worth with a comparison among them.";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.rateServiceProcessInstaller,
            this.rateServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller rateServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller rateServiceInstaller;
    }
}