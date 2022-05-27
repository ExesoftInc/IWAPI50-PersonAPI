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
    
    
    public class StateProvinceBuilder : IStateProvinceBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public StateProvinceBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<StateProvince, StateProvinceModel>>  ProjectToModel {
            get {
                return entity => new StateProvinceModel(entity);
            }
        }
        
        public async Task<IQueryable<StateProvinceModel>> GetStateProvinces() {
            return await Task.FromResult(_entities.StateProvinces.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.StateProvinces.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetStateProvince_ByStateProvinceID(int stateProvinceID) {
            var query = await Search(_entities.StateProvinces, x => x.StateProvinceID == stateProvinceID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; StateProvince with stateProvinceID = '{stateProvinceID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<StateProvinceModel>> GetStateProvince_ByCountryRegionCode(string countryRegionCode) {

            var query = await Task.FromResult(Search(_entities.StateProvinces, x => x.CountryRegionCode == countryRegionCode).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddStateProvince(StateProvinceModel model) {

            var matchCountryRegionCode = _entities.CountryRegions.Where(x => x.CountryRegionCode.Equals(model.CountryRegionCode));
            if (!matchCountryRegionCode.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.CountryRegionCode) + " '{model.CountryRegionCode}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.StateProvinces.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("StateProvince added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new StateProvinceModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateStateProvince(StateProvinceModel model) {

            var query = Search(_entities.StateProvinces, x =>  x.StateProvinceID == model.StateProvinceID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("StateProvince with _stateProvinceID = '{0}' doesn't exist.",model.StateProvinceID)}; 
            }

            var matchCountryRegionCode = _entities.CountryRegions.Where(x => x.CountryRegionCode.Equals(model.CountryRegionCode));
            if (!matchCountryRegionCode.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.CountryRegionCode) + string.Format("StateProvince with CountryRegionCode = '{0}' doesn't exist.", model.CountryRegionCode)}; 
            }

            StateProvince entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("StateProvince update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteStateProvince(int stateProvinceID) {

            var query = Search(_entities.StateProvinces, x => x.StateProvinceID == stateProvinceID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("StateProvince with _stateProvinceID = '{0}' doesn't exist.",stateProvinceID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.StateProvinces.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("StateProvince deleted with values: '{0}'", JsonConvert.SerializeObject(new StateProvinceModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<StateProvince> Search(IQueryable<StateProvince> query, Expression<Func<StateProvince, bool>> filter) {
            return query.Where(filter);
        }
    }
}

