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
    
    
    public class AddressTypeBuilder : IAddressTypeBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public AddressTypeBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<AddressType, AddressTypeModel>>  ProjectToModel {
            get {
                return entity => new AddressTypeModel(entity);
            }
        }
        
        public async Task<IQueryable<AddressTypeModel>> GetAddressTypes() {
            return await Task.FromResult(_entities.AddressTypes.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.AddressTypes.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetAddressType_ByAddressTypeID(int addressTypeID) {
            var query = await Search(_entities.AddressTypes, x => x.AddressTypeID == addressTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; AddressType with addressTypeID = '{addressTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddAddressType(AddressTypeModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.AddressTypes.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("AddressType added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new AddressTypeModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateAddressType(AddressTypeModel model) {

            var query = Search(_entities.AddressTypes, x =>  x.AddressTypeID == model.AddressTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("AddressType with _addressTypeID = '{0}' doesn't exist.",model.AddressTypeID)}; 
            }

            AddressType entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("AddressType update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteAddressType(int addressTypeID) {

            var query = Search(_entities.AddressTypes, x => x.AddressTypeID == addressTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("AddressType with _addressTypeID = '{0}' doesn't exist.",addressTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.AddressTypes.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("AddressType deleted with values: '{0}'", JsonConvert.SerializeObject(new AddressTypeModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<AddressType> Search(IQueryable<AddressType> query, Expression<Func<AddressType, bool>> filter) {
            return query.Where(filter);
        }
    }
}

