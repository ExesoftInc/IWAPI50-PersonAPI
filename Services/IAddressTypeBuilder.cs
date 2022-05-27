using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IAddressTypeBuilder {
        
        Task<IQueryable<AddressTypeModel>> GetAddressTypes();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetAddressType_ByAddressTypeID(int addressTypeID);
        
        Task<BuilderResponse> AddAddressType(AddressTypeModel model);
        
        Task<BuilderResponse> UpdateAddressType(AddressTypeModel model);
        
        Task<BuilderResponse> DeleteAddressType(int addressTypeID);
    }
}

