using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IContactTypeBuilder {
        
        Task<IQueryable<ContactTypeModel>> GetContactTypes();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetContactType_ByContactTypeID(int contactTypeID);
        
        Task<BuilderResponse> AddContactType(ContactTypeModel model);
        
        Task<BuilderResponse> UpdateContactType(ContactTypeModel model);
        
        Task<BuilderResponse> DeleteContactType(int contactTypeID);
    }
}

