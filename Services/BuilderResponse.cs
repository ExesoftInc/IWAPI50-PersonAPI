using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Models {
    
    
    [Table("BuilderResponse")]
    public partial class BuilderResponse {
        
        // validation messages
        public string ValidationMessage;
        
        // HttpStatusCode
        public int StatusCode;
        
        // model crated or updated
        public object Model;
    }
}

