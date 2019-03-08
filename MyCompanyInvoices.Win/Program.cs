using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using MyCompanyInvoices.Module.BusinessObjects;

namespace MyCompanyInvoices.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            if(Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder) {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            MyCompanyInvoicesWindowsFormsApplication winApplication = new MyCompanyInvoicesWindowsFormsApplication();
            // Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
            //winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
            SecurityAdapterHelper.Enable();
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            // added by me
           winApplication.CreateCustomLogonWindowObjectSpace +=
           application_CreateCustomLogonWindowObjectSpace;
           // winApplication.Setup();
            //end 
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            try {
                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }       

          }
        // added by me
        private static void application_CreateCustomLogonWindowObjectSpace(object sender,
        CreateCustomLogonWindowObjectSpaceEventArgs e)
        {
            e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace(typeof(CustomLogon));
            if (e.ObjectSpace is NonPersistentObjectSpace)
            {
                IObjectSpace objectSpaceEmployee = ((XafApplication)sender).CreateObjectSpace(typeof(Company));
                ((NonPersistentObjectSpace)e.ObjectSpace).AdditionalObjectSpaces.Add(objectSpaceEmployee);
            }
        }


    }

}
