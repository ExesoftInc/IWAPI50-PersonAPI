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
    
    
    public class BusinessEntityAddressBuilder : IBusinessEntityAddressBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public BusinessEntityAddressBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<BusinessEntityAddress, BusinessEntityAddressModel>>  ProjectToModel {
            get {
                return entity => new BusinessEntityAddressModel(entity);
            }
        }
        
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddresses() {
            return await Task.FromResult(_entities.BusinessEntityAddresses.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.BusinessEntityAddresses.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(int businessEntityID, int addressID, int addressTypeID) {
            var query = await Search(_entities.BusinessEntityAddresses, x => x.BusinessEntityID == businessEntityID&& x.AddressID == addressID&& x.AddressTypeID == addressTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; BusinessEntityAddress with businessEntityID, addressID, addressTypeID = '{businessEntityID}', '{addressID}', '{addressTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityAddresses, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressID(int addressID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityAddresses, x => x.AddressID == addressID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressTypeID(int addressTypeID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityAddresses, x => x.AddressTypeID == addressTypeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddBusinessEntityAddress(BusinessEntityAddressModel model) {

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            var matchAddressID = _entities.Addresses.Where(x => x.AddressID.Equals(model.AddressID));
            if (!matchAddressID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.AddressID) + " '{model.AddressID}' doesn't exist in the system."}; 
            }

            var matchAddressTypeID = _entities.AddressTypes.Where(x => x.AddressTypeID.Equals(model.AddressTypeID));
            if (!matchAddressTypeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.AddressTypeID) + " '{model.AddressTypeID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.BusinessEntityAddresses.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityAddress added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new BusinessEntityAddressModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateBusinessEntityAddress(BusinessEntityAddressModel model) {

            var query = Search(_entities.BusinessEntityAddresses, x =>  x.BusinessEntityID == model.BusinessEntityID && x.AddressID == model.AddressID && x.AddressTypeID == model.AddressTypeID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntityAddress with _businessEntityID, _addressID, _addressTypeID = '{0}', '{1}', '{2}' doesn't exist.",model.BusinessEntityID, model.AddressID, model.AddressTypeID)}; 
            }

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            var matchAddressID = _entities.Addresses.Where(x => x.AddressID.Equals(model.AddressID));
            if (!matchAddressID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.AddressID) + string.Format("AddressID = '{0}' doesn't exist in the system.", model.AddressID)}; 
            }

            var matchAddressTypeID = _entities.AddressTypes.Where(x => x.AddressTypeID.Equals(model.AddressTypeID));
            if (!matchAddressTypeID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.AddressTypeID) + string.Format("AddressTypeID = '{0}' doesn't exist in the system.", model.AddressTypeID)}; 
            }

            BusinessEntityAddress entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityAddress update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteBusinessEntityAddress(int businessEntityID, int addressID, int addressTypeID) {

            var query = Search(_entities.BusinessEntityAddresses, x => x.BusinessEntityID == businessEntityID&& x.AddressID == addressID&& x.AddressTypeID == addressTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntityAddress with _businessEntityID, _addressID, _addressTypeID = '{0}', '{1}', '{2}' doesn't exist.",businessEntityID, addressID, addressTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.BusinessEntityAddresses.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityAddress deleted with values: '{0}'", JsonConvert.SerializeObject(new BusinessEntityAddressModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<BusinessEntityAddress> Search(IQueryable<BusinessEntityAddress> query, Expression<Func<BusinessEntityAddress, bool>> filter) {
            return query.Where(filter);
        }
    }
}

