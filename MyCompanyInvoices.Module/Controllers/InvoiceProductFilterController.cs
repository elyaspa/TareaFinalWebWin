using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MyCompanyInvoices.Module.BusinessObjects;

namespace MyCompanyInvoices.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class InvoiceProductFilterController : ViewController
    {
        public InvoiceProductFilterController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var instance = ObjectSpace.FindObject<CompanySeller>(new BinaryOperator("Oid", SecuritySystem.CurrentUserId));

            if ((View is ListView) & (View.ObjectTypeInfo.Type == typeof(Invoice)))
            {
                ((ListView)View).CollectionSource.Criteria["Filter1"]= new BinaryOperator("Subsidiary", instance.Subsidiary); ;
            }else
                if ((View is ListView) & (View.ObjectTypeInfo.Type == typeof(Product)))
            {
                CriteriaOperator op = GroupOperator.Or(new BinaryOperator("Subsidiary", instance.Subsidiary), new BinaryOperator("IsGlobal", true));
                ((ListView)View).CollectionSource.Criteria["InvoiceFilter"] = op;

            }
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
