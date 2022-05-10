// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class BusinessEntity {
        
        [Key()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[BusinessEntityID] point to this entity (FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID)
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; } = new HashSet<BusinessEntityAddress>();
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[BusinessEntityID] point to this entity (FK_BusinessEntityContact_BusinessEntity_BusinessEntityID)
        public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new HashSet<BusinessEntityContact>();
        
        /// Parent (One-to-One) BusinessEntity pointed by [Person].[BusinessEntityID] (FK_Person_BusinessEntity_BusinessEntityID)
        public virtual Person Person { get; set; }
    }
}
