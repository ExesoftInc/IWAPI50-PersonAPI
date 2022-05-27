using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IStateProvinceBuilder {
        
        Task<IQueryable<StateProvinceModel>> GetStateProvinces();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetStateProvince_ByStateProvinceID(int stateProvinceID);
        
        Task<IQueryable<StateProvinceModel>> GetStateProvince_ByCountryRegionCode(string countryRegionCode);
        
        Task<BuilderResponse> AddStateProvince(StateProvinceModel model);
        
        Task<BuilderResponse> UpdateStateProvince(StateProvinceModel model);
        
        Task<BuilderResponse> DeleteStateProvince(int stateProvinceID);
    }
}

