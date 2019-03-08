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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace MyCompanyInvoices.Module.BusinessObjects
{
    [NavigationItem("Company Managment")]
    [DefaultClassOptions,DefaultProperty("UserName")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class CompanySeller : PermissionPolicyUser
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public CompanySeller(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string lastName;
        string name;
        Subsidiary subsidiary;
        Company company;

        [Association("CompanySeller-Invoices")]
        public XPCollection<Invoice> Invoice
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoice));
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetPropertyValue(nameof(LastName), ref lastName, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetPropertyValue(nameof(Name), ref name, value);
            }
        }
        [Association("Company-CompanySeller")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[RuleUniqueValue]
        public Company Company
        {
            get
            {
                return company;
            }
            set
            {
                SetPropertyValue(nameof(Company), ref company, value);
            }
        }
        
        //[DataSourceProperty("Company.Subsidiaries")]
        [Association("Subsidiary-CompanySellers")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Subsidiary Subsidiary
        {
            get
            {
                return subsidiary;
            }
            set { SetPropertyValue(nameof(Subsidiary), ref subsidiary, value); }
        }
        [Association("CompanySeller-Products")]      
        public XPCollection<Product> Product
        {
            get
            {
                return GetCollection<Product>(nameof(Product));
            }
        }
    }
}