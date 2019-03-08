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
    [NavigationItem("Clients Managment")]
    public class Resume : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Resume(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ResumeDescription
        {
            get
            {
                return resumeDescription;
            }
            set
            {
                SetPropertyValue(nameof(ResumeDescription), ref resumeDescription, value);
            }
        }
        string resumeDescription;
        Client client;

        [Association("Client-Resume")]
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
        [DevExpress.Xpo.Aggregated, Association("Resume-Portfolio")]
        public XPCollection<Portfolio> Portfolio
        {
            get { return GetCollection<Portfolio>("Portfolio"); }
        }
        
       
    }
}