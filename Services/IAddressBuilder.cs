using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IAddressBuilder {
        
        Task<IQueryable<AddressModel>> GetAddresses();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetAddress_ByAddressID(int addressID);
        
        Task<IQueryable<AddressModel>> GetAddress_ByStateProvinceID(int stateProvinceID);
        
        Task<BuilderResponse> AddAddress(AddressModel model);
        
        Task<BuilderResponse> UpdateAddress(AddressModel model);
        
        Task<BuilderResponse> DeleteAddress(int addressID);
    }
}

