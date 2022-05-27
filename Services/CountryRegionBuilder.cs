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
        
        private ILoggerManager _logger;
        
        public CountryRegionBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<CountryRegion, CountryRegionModel>>  ProjectToModel {
            get {
                return entity => new CountryRegionModel(entity);
            }
        }
        
        public async Task<IQueryable<CountryRegionModel>> GetCountryRegions() {
            return await Task.FromResult(_entities.CountryRegions.Select(ProjectToModel));
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
                return new BuilderResponse { ValidationMessage = $"Record Not Found; CountryRegion with countryRegionCode = '{countryRegionCode}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddCountryRegion(CountryRegionModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.CountryRegions.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CountryRegion added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new CountryRegionModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateCountryRegion(CountryRegionModel model) {

            var query = Search(_entities.CountryRegions, x =>  x.CountryRegionCode == model.CountryRegionCode);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CountryRegion with _countryRegionCode = '{0}' doesn't exist.",model.CountryRegionCode)}; 
            }

            CountryRegion entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CountryRegion update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteCountryRegion(string countryRegionCode) {

            var query = Search(_entities.CountryRegions, x => x.CountryRegionCode == countryRegionCode);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("CountryRegion with _countryRegionCode = '{0}' doesn't exist.",countryRegionCode)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.CountryRegions.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("CountryRegion deleted with values: '{0}'", JsonConvert.SerializeObject(new CountryRegionModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<CountryRegion> Search(IQueryable<CountryRegion> query, Expression<Func<CountryRegion, bool>> filter) {
            return query.Where(filter);
        }
    }
}

