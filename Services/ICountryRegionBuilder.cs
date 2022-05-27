using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface ICountryRegionBuilder {
        
        Task<IQueryable<CountryRegionModel>> GetCountryRegions();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetCountryRegion_ByCountryRegionCode(string countryRegionCode);
        
        Task<BuilderResponse> AddCountryRegion(CountryRegionModel model);
        
        Task<BuilderResponse> UpdateCountryRegion(CountryRegionModel model);
        
        Task<BuilderResponse> DeleteCountryRegion(string countryRegionCode);
    }
}

