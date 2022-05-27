using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IPersonBuilder {
        
        Task<IQueryable<PersonModel>> GetPeople();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetPerson_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddPerson(PersonModel model);
        
        Task<BuilderResponse> UpdatePerson(PersonModel model);
        
        Task<BuilderResponse> DeletePerson(int businessEntityID);
    }
}

