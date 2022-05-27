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
    
    
    public class AddressBuilder : IAddressBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public AddressBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Address, AddressModel>>  ProjectToModel {
            get {
                return entity => new AddressModel(entity);
            }
        }
        
        public async Task<IQueryable<AddressModel>> GetAddresses() {
            return await Task.FromResult(_entities.Addresses.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Addresses.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetAddress_ByAddressID(int addressID) {
            var query = await Search(_entities.Addresses, x => x.AddressID == addressID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; Address with addressID = '{addressID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<AddressModel>> GetAddress_ByStateProvinceID(int stateProvinceID) {

            var query = await Task.FromResult(Search(_entities.Addresses, x => x.StateProvinceID == stateProvinceID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddAddress(AddressModel model) {

            var matchStateProvinceID = _entities.StateProvinces.Where(x => x.StateProvinceID.Equals(model.StateProvinceID));
            if (!matchStateProvinceID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.StateProvinceID) + " '{model.StateProvinceID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.Addresses.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Address added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new AddressModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateAddress(AddressModel model) {

            var query = Search(_entities.Addresses, x =>  x.AddressID == model.AddressID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Address with _addressID = '{0}' doesn't exist.",model.AddressID)}; 
            }

            var matchStateProvinceID = _entities.StateProvinces.Where(x => x.StateProvinceID.Equals(model.StateProvinceID));
            if (!matchStateProvinceID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.StateProvinceID) + string.Format("Address with StateProvinceID = '{0}' doesn't exist.", model.StateProvinceID)}; 
            }

            Address entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Address update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteAddress(int addressID) {

            var query = Search(_entities.Addresses, x => x.AddressID == addressID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Address with _addressID = '{0}' doesn't exist.",addressID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Addresses.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Address deleted with values: '{0}'", JsonConvert.SerializeObject(new AddressModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Address> Search(IQueryable<Address> query, Expression<Func<Address, bool>> filter) {
            return query.Where(filter);
        }
    }
}

