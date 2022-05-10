// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonAPI.Entities;
using PersonAPI.Models;
using PersonAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public class CountryRegionBuilder : ICountryRegionBuilder {
        
        private IDbEntities _entities;
        
        private IMapper _mapper;
        
        private ILoggerManager _logger;
        
        public CountryRegionBuilder(EntitiesContext context, IMapper mapper, ILoggerManager logger) {
            _entities = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        private Expression<Func<CountryRegion, CountryRegionModel>>  ProjectToModel {
            get {
                return entity => _mapper.Map<CountryRegionModel>(entity);
            }
        }
        
        public IQueryable<CountryRegionModel> GetCountryRegions() {
            return _entities.CountryRegions.Select(ProjectToModel);
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.CountryRegions.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetCountryRegion_ByCountryRegionCode(string countryRegionCode) {
          var query = await Search(_entities.CountryRegions, x => x.CountryRegionCode == countryRegionCode).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
              return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
           return new BuilderResponse { RequestMessage = $"Record Not Found; CountryRegion with countryRegionCode = '{countryRegionCode}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddCountryRegion(CountryRegionModel model) {
           try
           {
                 var entity = _mapper.Map<CountryRegion>(model);
                _entities.CountryRegions.Add(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("CountryRegion added with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ Model = new CountryRegionModel(entity) }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert explicit value for identity column"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "IDENTITY_INSERT is set to OFF; Cannot insert explicit value for identity column when IDENTITY_INSERT is set to OFF."};
                }
                else if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> UpdateCountryRegion(CountryRegionModel model) {

          var query = Search(_entities.CountryRegions, x =>  x.CountryRegionCode == model.CountryRegionCode);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("CountryRegion with _countryRegionCode = '{0}' doesn't exist.",model.CountryRegionCode)}; 
            }
           try
           {
            CountryRegion entity = query.SingleOrDefault();
             entity = model.ToEntity(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("CountryRegion update with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Created }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> DeleteCountryRegion(string countryRegionCode) {
          var query = Search(_entities.CountryRegions, x => x.CountryRegionCode == countryRegionCode);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("CountryRegion with _countryRegionCode = '{0}' doesn't exist.",countryRegionCode)}; 
            }
            var entity = query.SingleOrDefault();

           try
           {
                _entities.CountryRegions.Remove(entity);
               await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CountryRegion deleted with values: '{0}'", JsonConvert.SerializeObject(new CountryRegionModel(entity))));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Please delete related items first."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        private IQueryable<CountryRegion> Search(IQueryable<CountryRegion> query, Expression<Func<CountryRegion, bool>> filter) {
            return query.Where(filter);
        }
    }
}

