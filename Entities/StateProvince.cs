using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class StateProvince {
        
        [Key()]
        [Display(Name = "State province ID")]
        public int StateProvinceID { get; set; }
        
        [Display(Name = "State province code")]
        public string StateProvinceCode { get; set; }
        
        [Display(Name = "Country region code")]
        public string CountryRegionCode { get; set; }
        
        [Display(Name = "Is only state province flag")]
        public bool IsOnlyStateProvinceFlag { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Territory ID")]
        public int TerritoryID { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child Addresses where [Address].[StateProvinceID] point to this entity (FK_Address_StateProvince_StateProvinceID)
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        
        // Parent CountryRegion pointed by [StateProvince].([CountryRegionCode]) (FK_StateProvince_CountryRegion_CountryRegionCode)
        public virtual CountryRegion CountryRegion { get; set; }
    }
}

