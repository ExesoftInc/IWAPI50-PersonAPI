using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class Person {
        
        [Key()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Display(Name = "Person type")]
        public string PersonType { get; set; }
        
        [Display(Name = "Name style")]
        public bool NameStyle { get; set; }
        
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Display(Name = "Suffix")]
        public string Suffix { get; set; }
        
        [Display(Name = "Email promotion")]
        public int EmailPromotion { get; set; }
        
        [Display(Name = "Additional contact info")]
        public string AdditionalContactInfo { get; set; }
        
        [Display(Name = "Demographics")]
        public string Demographics { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[PersonID] point to this entity (FK_BusinessEntityContact_Person_PersonID)
        public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new HashSet<BusinessEntityContact>();
        
        /// Child EmailAddresses where [EmailAddress].[BusinessEntityID] point to this entity (FK_EmailAddress_Person_BusinessEntityID)
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; } = new HashSet<EmailAddress>();
        
        /// Parent (One-to-One) Person pointed by [Password].[BusinessEntityID] (FK_Password_Person_BusinessEntityID)
        public virtual Password Password { get; set; }
        
        /// Child PersonPhones where [PersonPhone].[BusinessEntityID] point to this entity (FK_PersonPhone_Person_BusinessEntityID)
        public virtual ICollection<PersonPhone> PersonPhones { get; set; } = new HashSet<PersonPhone>();
        
        // Parent BusinessEntity pointed by [Person].([BusinessEntityID]) (FK_Person_BusinessEntity_BusinessEntityID)
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}

