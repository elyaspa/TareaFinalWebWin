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
   
   [DefaultProperty("City")]  
    public class Address : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Address(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string zipPostal;
        string stateProvince;
        string city;
        string street;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Street
        {
            get { return street; }
            set { SetPropertyValue(nameof(Street), ref street, value); }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string City
        {
            get {return city; }
            set { SetPropertyValue(nameof(City), ref city, value); }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StateProvince
        {
            get { return stateProvince; }
            set { SetPropertyValue(nameof(StateProvince), ref stateProvince, value); }
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ZipPostal
        {
            get { return zipPostal; }
            set { SetPropertyValue(nameof(ZipPostal), ref zipPostal, value); }
        }
    }
}