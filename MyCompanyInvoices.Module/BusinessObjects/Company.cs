using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace MyCompanyInvoices.Module.BusinessObjects
{
    [NavigationItem("Company Managment")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Company : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string companyName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                SetPropertyValue(nameof(CompanyName), ref companyName, value);
            }
        }
        [Association("Company-CompanySeller")]
        public XPCollection<CompanySeller> CompanySeller
        {
            get
            {
                return GetCollection<CompanySeller>(nameof(CompanySeller));
            }
        }
        [Association("Company-Subsidiaries")]
        public XPCollection<Subsidiary> Subsidiaries
        {
            get
            {
                return GetCollection<Subsidiary>(nameof(Subsidiaries));
            }
        }
    }
}