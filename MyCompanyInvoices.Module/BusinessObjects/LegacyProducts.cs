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
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class LegacyProducts : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public LegacyProducts(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        double price;
        int iD;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Persistent]
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                SetPropertyValue(nameof(Name), ref name, value);
            }
        }

        [Key(false)]
        [Persistent]
        public int ID
        {
            get
            {
                return iD;
            }
            protected set
            {
                SetPropertyValue(nameof(ID), ref iD, value);
            }
        }

        [Persistent]
        public double Price
        {
            get
            {
                return price;
            }
            protected set
            {
                SetPropertyValue(nameof(Price), ref price, value);
            }
        }
       
    }
}