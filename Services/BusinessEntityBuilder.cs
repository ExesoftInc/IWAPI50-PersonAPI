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
    
    
    public class BusinessEntityBuilder : IBusinessEntityBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public BusinessEntityBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<BusinessEntity, BusinessEntityModel>>  ProjectToModel {
            get {
                return entity => new BusinessEntityModel(entity);
            }
        }
        
        public async Task<IQueryable<BusinessEntityModel>> GetBusinessEntities() {
            return await Task.FromResult(_entities.BusinessEntities.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.BusinessEntities.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetBusinessEntity_ByBusinessEntityID(int businessEntityID) {
            var query = await Search(_entities.BusinessEntities, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; BusinessEntity with businessEntityID = '{businessEntityID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddBusinessEntity(BusinessEntityModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.BusinessEntities.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntity added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new BusinessEntityModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateBusinessEntity(BusinessEntityModel model) {

            var query = Search(_entities.BusinessEntities, x =>  x.BusinessEntityID == model.BusinessEntityID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntity with _businessEntityID = '{0}' doesn't exist.",model.BusinessEntityID)}; 
            }

            BusinessEntity entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntity update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteBusinessEntity(int businessEntityID) {

            var query = Search(_entities.BusinessEntities, x => x.BusinessEntityID == businessEntityID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntity with _businessEntityID = '{0}' doesn't exist.",businessEntityID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.BusinessEntities.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntity deleted with values: '{0}'", JsonConvert.SerializeObject(new BusinessEntityModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<BusinessEntity> Search(IQueryable<BusinessEntity> query, Expression<Func<BusinessEntity, bool>> filter) {
            return query.Where(filter);
        }
    }
}

