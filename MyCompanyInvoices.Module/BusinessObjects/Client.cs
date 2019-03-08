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
using MyCompanyInvoices.Module.BusinessObjects;

namespace MyCompanyInvoices.Module.BusinessObjects
{
    [NavigationItem("Clients Managment")]
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Client : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Client(Session session)
            : base(session)
        {
            
        }
        public override void AfterConstruction()
        {             
            base.AfterConstruction();
            UserAddress = new Address(Session);
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        string phoneNumber;
        Address userAddress;
        string id;
        string lastName;
        string name;
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue(nameof(Name), ref name, value); }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue(nameof(LastName), ref lastName, value); }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Id
        {
            get { return id; }
            set { SetPropertyValue(nameof(Id), ref id, value); }
        }
          
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [ImmediatePostData]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetPropertyValue(nameof(PhoneNumber), ref phoneNumber, value); }
        }
        [DevExpress.Xpo.Aggregated]
        public Address UserAddress
        {
            get { return userAddress; }
            set { SetPropertyValue(nameof(UserAddress), ref userAddress, value); }
        }
        
        private UsserType? type;
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public UsserType? Type
        {
            get { return type; }
            set
            {
                SetPropertyValue("Type", ref type, value);
            }
        }
        [Association("Client-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }

        

        [Association("Subsidiary-Clients")]
        public XPCollection<Subsidiary> Subsidiaries
        {
            get
            {
                return GetCollection<Subsidiary>(nameof(Subsidiaries));
            }
        }
        [Association("Client-Resume")]
        public XPCollection<Resume> Resume
        {
            get
            {
                return GetCollection<Resume>(nameof(Resume));
            }
        }

        // public virtual IList<Resume> Resume { get; set; }
    }
    
    public enum UsserType { 
      
        TypeA=1, 
        TypeB=2
        
         }
}