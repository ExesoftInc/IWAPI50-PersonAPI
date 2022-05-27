using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IPersonPhoneBuilder {
        
        Task<IQueryable<PersonPhoneModel>> GetPersonPhones();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID(int businessEntityID, string phoneNumber, int phoneNumberTypeID);
        
        Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByBusinessEntityID(int businessEntityID);
        
        Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByPhoneNumberTypeID(int phoneNumberTypeID);
        
        Task<BuilderResponse> AddPersonPhone(PersonPhoneModel model);
        
        Task<BuilderResponse> UpdatePersonPhone(PersonPhoneModel model);
        
        Task<BuilderResponse> DeletePersonPhone(int businessEntityID, string phoneNumber, int phoneNumberTypeID);
    }
}

