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
using DevExpress.ExpressApp.Xpo;

namespace MyCompanyInvoices.Module.BusinessObjects
{
    
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SingletonSettings : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        private SingletonSettings(Session session)
            : base(session)
        {
        }

        public static SingletonSettings GetInstance(IObjectSpace objectSpace)
        {
            XPObjectSpace OsX = (XPObjectSpace)objectSpace;
            var instance = OsX.Session.FindObject<SingletonSettings>(null);
            if (instance == null)
            {
                instance = new SingletonSettings(OsX.Session);
                instance.lastUpdate = DateTime.Now;
                instance.user = "pepe";
                instance.comments = "esto es una prueba";
            }
            return instance;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnDeleting()
        {
            throw new UserFriendlyException("Singleton item cant be deleted");
        }

        string comments;
        string user;
        DateTime lastUpdate;

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { SetPropertyValue(nameof(LastUpdate), ref lastUpdate, value); }
            }

            [Size(SizeAttribute.DefaultStringMappingFieldSize)]
            public string User
        {
            get { return user; }
            set { SetPropertyValue(nameof(User), ref user, value); }
            }

            [Size(SizeAttribute.DefaultStringMappingFieldSize)]
            public  string Comments
        {
            get {return comments; }
            set { SetPropertyValue(nameof(Comments), ref comments, value); }
            }

        }
    }