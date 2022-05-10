// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public interface IBusinessEntityBuilder {
        
        IQueryable<BusinessEntityModel> GetBusinessEntities();
        
        IList<ExpandoObject> GetDisplayModels(List<string> propNames);
        
        Task<BuilderResponse> GetBusinessEntity_ByBusinessEntityID(int businessEntityID);
        
        Task<BuilderResponse> AddBusinessEntity(BusinessEntityModel model);
        
        Task<BuilderResponse> UpdateBusinessEntity(BusinessEntityModel model);
        
        Task<BuilderResponse> DeleteBusinessEntity(int businessEntityID);
    }
}

