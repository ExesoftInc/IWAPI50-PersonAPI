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
    
    
    public class BusinessEntityContactBuilder : IBusinessEntityContactBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public BusinessEntityContactBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<BusinessEntityContact, BusinessEntityContactModel>>  ProjectToModel {
            get {
                return entity => new BusinessEntityContactModel(entity);
            }
        }
        
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContacts() {
            return await Task.FromResult(_entities.BusinessEntityContacts.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.BusinessEntityContacts.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID(int businessEntityID, int personID, int contactTypeID) {
            var query = await Search(_entities.BusinessEntityContacts, x => x.BusinessEntityID == businessEntityID&& x.PersonID == personID&& x.ContactTypeID == contactTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; BusinessEntityContact with businessEntityID, personID, contactTypeID = '{businessEntityID}', '{personID}', '{contactTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityContacts, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByPersonID(int personID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityContacts, x => x.PersonID == personID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByContactTypeID(int contactTypeID) {

            var query = await Task.FromResult(Search(_entities.BusinessEntityContacts, x => x.ContactTypeID == contactTypeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddBusinessEntityContact(BusinessEntityContactModel model) {

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            var matchPersonID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.PersonID));
            if (!matchPersonID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.PersonID) + " '{model.PersonID}' doesn't exist in the system."}; 
            }

            var matchContactTypeID = _entities.ContactTypes.Where(x => x.ContactTypeID.Equals(model.ContactTypeID));
            if (!matchContactTypeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.ContactTypeID) + " '{model.ContactTypeID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.BusinessEntityContacts.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityContact added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new BusinessEntityContactModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateBusinessEntityContact(BusinessEntityContactModel model) {

            var query = Search(_entities.BusinessEntityContacts, x =>  x.BusinessEntityID == model.BusinessEntityID && x.PersonID == model.PersonID && x.ContactTypeID == model.ContactTypeID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntityContact with _businessEntityID, _personID, _contactTypeID = '{0}', '{1}', '{2}' doesn't exist.",model.BusinessEntityID, model.PersonID, model.ContactTypeID)}; 
            }

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            var matchPersonID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.PersonID));
            if (!matchPersonID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.PersonID) + string.Format("PersonID = '{0}' doesn't exist in the system.", model.PersonID)}; 
            }

            var matchContactTypeID = _entities.ContactTypes.Where(x => x.ContactTypeID.Equals(model.ContactTypeID));
            if (!matchContactTypeID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.ContactTypeID) + string.Format("ContactTypeID = '{0}' doesn't exist in the system.", model.ContactTypeID)}; 
            }

            BusinessEntityContact entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityContact update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteBusinessEntityContact(int businessEntityID, int personID, int contactTypeID) {

            var query = Search(_entities.BusinessEntityContacts, x => x.BusinessEntityID == businessEntityID&& x.PersonID == personID&& x.ContactTypeID == contactTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("BusinessEntityContact with _businessEntityID, _personID, _contactTypeID = '{0}', '{1}', '{2}' doesn't exist.",businessEntityID, personID, contactTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.BusinessEntityContacts.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("BusinessEntityContact deleted with values: '{0}'", JsonConvert.SerializeObject(new BusinessEntityContactModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<BusinessEntityContact> Search(IQueryable<BusinessEntityContact> query, Expression<Func<BusinessEntityContact, bool>> filter) {
            return query.Where(filter);
        }
    }
}

