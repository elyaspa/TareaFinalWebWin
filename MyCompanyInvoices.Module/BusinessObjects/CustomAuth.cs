using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInvoices.Module.BusinessObjects
{
   public  class CustomAuth : AuthenticationBase, IAuthenticationStandard
    {

        private CustomLogon customLogon;
        public CustomAuth()
        {
            customLogon = new CustomLogon();
        }
        public override void Logoff()
        {
            base.Logoff();
            customLogon = new CustomLogon();
        }
        public override void ClearSecuredLogonParameters()
        {
            customLogon.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {

           CompanySeller employee = objectSpace.FindObject<CompanySeller>(
                new BinaryOperator("UserName", customLogon.UserName));

            if (employee == null)
                throw new ArgumentNullException("Employee cant be empty");

            if (!employee.ComparePassword(customLogon.Password))
                throw new AuthenticationException(
                    employee.UserName, "Password mismatch.");

            return employee;
        }

        public override void SetLogonParameters(object logonParameters)
        {
            this.customLogon = (CustomLogon)logonParameters;
        }

        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(CustomLogon) };
        }
        public override bool AskLogonParametersViaUI
        {
            get { return true; }
        }
        public override object LogonParameters
        {
            get { return customLogon; }
        }
        public override bool IsLogoffEnabled
        {
            get { return true; }
        }




    }
}
