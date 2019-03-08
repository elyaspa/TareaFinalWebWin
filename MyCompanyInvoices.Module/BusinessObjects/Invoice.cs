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
    [NavigationItem("Business Area")]
    [DefaultProperty("Name")]
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Invoice : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Invoice(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CreatedDate=DateTime.Now;
            CompanySeller user = Session.FindObject<CompanySeller>(new BinaryOperator("Oid", SecuritySystem.CurrentUserId));
            if (user != null)
            {
                Subsidiary = user.Subsidiary;
                seller = user;
            }
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        CompanySeller seller;
        double? invoiceTotal;

        DateTime createdDate;
        Client client;
        Subsidiary subsidiary;
        string code;
        string name;
        
        [Association("CompanySeller-Invoices")]

        
        [DataSourceProperty("Subsidiary.Sellers")]
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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                SetPropertyValue(nameof(Code), ref code, value);
            }
        }

       // [DataSourceProperty("Seller.Subsidiary")]
       [DataSourceCriteria("Oid='@This.Oid'")]
        [Association("Subsidiary-InvoicesList")]
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
        public XPCollection<Product> Products
        {
            get
            {
                var collection = GetCollection<Product>(nameof(Products));
                collection.CollectionChanged += UpdateCurrentInvoiceTotal;
                return collection;

            }
        }
        private Status status;
        public Status Status
        {
            get { return status; }
            set
            {
                SetPropertyValue("Status", ref status, value);
            }
        }
        [Association("Client-Invoices")]
        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                SetPropertyValue(nameof(Client), ref client, value);
            }
        }
        
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                SetPropertyValue(nameof(CreatedDate), ref createdDate, value);
            }
        }
        // calculating taxes starts here
        void UpdateCurrentInvoiceTotal(object sender, XPCollectionChangedEventArgs args)
        {
            OnChanged("CurrentInvoiceTotal");
        }


        [Association("Invoice-Taxes")]
        public XPCollection<Tax> Taxes
        {
            get
            {
                var collection = GetCollection<Tax>(nameof(Taxes));
                collection.CollectionChanged += UpdateCurrentInvoiceTotal;
                return collection;
            }
        }

        [Persistent]
        [Browsable(false)]
        public double? InvoiceTotal
        {
            get
            {
                return invoiceTotal;
            }
            set { SetPropertyValue(nameof(InvoiceTotal), ref invoiceTotal, value); }
        }
        [NonPersistent]
        public double CurrentInvoiceTotal
        {
            get
            {
                if (Products.Any())
                {
                    var total = GetTotal();
                    InvoiceTotal = total;
                    return total;
                }
                return 0;
            }
        }

        public double GetTotal()
        {
            double total = 0;
            double taxesTotal = 0;

            foreach (var product in Products)
            {
                total += product.Price;
            }

            foreach (var tax in Taxes)
            {
                switch (tax.Type)
                {
                    case TaxType.percentual:
                        {
                            taxesTotal += total * ( tax.NumericValue/ 100);
                            break;
                        }
                    case TaxType.global:
                        {
                            taxesTotal += tax.NumericValue;
                            break;
                        }
                    case TaxType.percentualByItem:
                        {
                            foreach (var product in Products)
                            {
                                taxesTotal += (product.Price * tax.NumericValue) / 100;
                            }
                            break;
                        }
                }
            }

            total += taxesTotal;

            return total;
        }




        //ends here

    }
    public enum Status { 
    [ImageName("State_Task_NotStarted")]
        Started,        
    [ImageName("State_Task_InProgress")]
        InProgress,        
    [ImageName("State_Task_Completed")]
        Completed
    }
}