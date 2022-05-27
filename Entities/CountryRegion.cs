using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class CountryRegion {
        
        [Key()]
        [Display(Name = "Country region code")]
        public string CountryRegionCode { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child StateProvinces where [StateProvince].[CountryRegionCode] point to this entity (FK_StateProvince_CountryRegion_CountryRegionCode)
        public virtual ICollection<StateProvince> StateProvinces { get; set; } = new HashSet<StateProvince>();
    }
}

