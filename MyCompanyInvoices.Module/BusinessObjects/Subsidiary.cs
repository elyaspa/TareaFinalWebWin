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
    [DefaultProperty("SubsidiaryName")]
    [NavigationItem("Company Managment")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Subsidiary : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Subsidiary(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        Company company;
        string subsidiaryName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SubsidiaryName
        {
            get
            {
                return subsidiaryName;
            }
            set
            {
                SetPropertyValue(nameof(SubsidiaryName), ref subsidiaryName, value);
            }
        }
        [Association("Subsidiary-CompanySellers")]
        public XPCollection<CompanySeller> Sellers
        {
            get
            {
                return GetCollection<CompanySeller>(nameof(Sellers));
            }
        }
        [Association("Subsidiary-Clients")]
        public XPCollection<Client> Clients
        {
            get
            {
                return GetCollection<Client>(nameof(Clients));
            }
        }
        [Association("Company-Subsidiaries")]
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

       // [DataSourceCriteria("Products.isGlobal AND @this.Oid = 'Products.SubsidiaryId'")]
       //[LinkNewObjectToParentImmediately(true) ]
        [Association("Subsidiary-Products")]
        public XPCollection<Product> Product
        {
            get
            {                
               return GetCollection<Product>(nameof(Product));
            }
        }
        [Association("Subsidiary-InvoicesList")]
        public XPCollection<Invoice> InvoicesList
        {
            get
            {
                return GetCollection<Invoice>(nameof(InvoicesList));
            }
        }
    }
}