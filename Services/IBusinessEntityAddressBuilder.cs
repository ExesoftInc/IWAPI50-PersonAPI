using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IBusinessEntityAddressBuilder {
        
        Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddresses();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(int businessEntityID, int addressID, int addressTypeID);
        
        Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByBusinessEntityID(int businessEntityID);
        
        Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressID(int addressID);
        
        Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressTypeID(int addressTypeID);
        
        Task<BuilderResponse> AddBusinessEntityAddress(BusinessEntityAddressModel model);
        
        Task<BuilderResponse> UpdateBusinessEntityAddress(BusinessEntityAddressModel model);
        
        Task<BuilderResponse> DeleteBusinessEntityAddress(int businessEntityID, int addressID, int addressTypeID);
    }
}

