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
{   [NavigationItem("Business Area")]
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Product : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        
        public Product(Session session)
            : base(session)
        {
            
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
            CompanySeller user = Session.FindObject<CompanySeller>(new BinaryOperator("Oid", SecuritySystem.CurrentUserId));
            if (user != null)
            {
                Subsidiary = user.Subsidiary;
            }



            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }



        CompanySeller seller;
        bool isGlobal;

        Category category;
        Subsidiary subsidiary;
        string tradeMark;
        double price;
        string name;


        
        [Association("CompanySeller-Products")]
        public CompanySeller Seller
        {
            get
            {
                return seller;
            }
            set
            {
                SetPropertyValue(nameof(Seller), ref seller, value);
            }
        }

        [ImmediatePostData]
        public bool IsGlobal
        {
            get
            {
                return isGlobal;
            }
            set
            {
                SetPropertyValue(nameof(IsGlobal), ref isGlobal, value);
            }
        }

        //[Browsable(false)]
        

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

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue(nameof(Price), ref price, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TradeMark
        {
            get
            {
                return tradeMark;
            }
            set
            {
                SetPropertyValue(nameof(TradeMark), ref tradeMark, value);
            }
        }
        [Association("Subsidiary-Products")]
        public Subsidiary Subsidiary
        {
            get
            {
                return subsidiary;
            }
            set
            {
               
                SetPropertyValue(nameof(Subsidiary), ref subsidiary, value);
            }
        }
        [Association("Invoice-Products")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }
        [Association("Category-Product")]
        public Category Category
        {
            get { return category; }
            set { SetPropertyValue(nameof(Category), ref category, value); }
        }

    }
}