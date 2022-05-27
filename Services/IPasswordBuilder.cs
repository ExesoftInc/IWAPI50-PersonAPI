using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IPasswordBuilder {
        
        Task<IQueryable<PasswordModel>> GetPasswords();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetPassword_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddPassword(PasswordModel model);
        
        Task<BuilderResponse> UpdatePassword(PasswordModel model);
        
        Task<BuilderResponse> DeletePassword(int businessEntityID);
    }
}

