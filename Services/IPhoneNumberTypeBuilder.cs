using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IPhoneNumberTypeBuilder {
        
        Task<IQueryable<PhoneNumberTypeModel>> GetPhoneNumberTypes();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetPhoneNumberType_ByPhoneNumberTypeID(int phoneNumberTypeID);
        
        Task<BuilderResponse> AddPhoneNumberType(PhoneNumberTypeModel model);
        
        Task<BuilderResponse> UpdatePhoneNumberType(PhoneNumberTypeModel model);
        
        Task<BuilderResponse> DeletePhoneNumberType(int phoneNumberTypeID);
    }
}

