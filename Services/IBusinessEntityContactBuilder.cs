using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IBusinessEntityContactBuilder {
        
        Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContacts();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID(int businessEntityID, int personID, int contactTypeID);
        
        Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByBusinessEntityID(int businessEntityID);
        
        Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByPersonID(int personID);
        
        Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByContactTypeID(int contactTypeID);
        
        Task<BuilderResponse> AddBusinessEntityContact(BusinessEntityContactModel model);
        
        Task<BuilderResponse> UpdateBusinessEntityContact(BusinessEntityContactModel model);
        
        Task<BuilderResponse> DeleteBusinessEntityContact(int businessEntityID, int personID, int contactTypeID);
    }
}

