using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IEmailAddressBuilder {
        
        Task<IQueryable<EmailAddressModel>> GetEmailAddresses();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetEmailAddress_ByBusinessEntityIDEmailAddressID(int businessEntityID, int emailAddressID);
        
        Task<IQueryable<EmailAddressModel>> GetEmailAddress_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddEmailAddress(EmailAddressModel model);
        
        Task<BuilderResponse> UpdateEmailAddress(EmailAddressModel model);
        
        Task<BuilderResponse> DeleteEmailAddress(int businessEntityID, int emailAddressID);
    }
}

