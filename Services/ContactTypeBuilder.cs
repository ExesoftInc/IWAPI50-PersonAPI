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
    
    
    public class ContactTypeBuilder : IContactTypeBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public ContactTypeBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<ContactType, ContactTypeModel>>  ProjectToModel {
            get {
                return entity => new ContactTypeModel(entity);
            }
        }
        
        public async Task<IQueryable<ContactTypeModel>> GetContactTypes() {
            return await Task.FromResult(_entities.ContactTypes.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.ContactTypes.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetContactType_ByContactTypeID(int contactTypeID) {
            var query = await Search(_entities.ContactTypes, x => x.ContactTypeID == contactTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; ContactType with contactTypeID = '{contactTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddContactType(ContactTypeModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.ContactTypes.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ContactType added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new ContactTypeModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateContactType(ContactTypeModel model) {

            var query = Search(_entities.ContactTypes, x =>  x.ContactTypeID == model.ContactTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ContactType with _contactTypeID = '{0}' doesn't exist.",model.ContactTypeID)}; 
            }

            ContactType entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ContactType update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteContactType(int contactTypeID) {

            var query = Search(_entities.ContactTypes, x => x.ContactTypeID == contactTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("ContactType with _contactTypeID = '{0}' doesn't exist.",contactTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.ContactTypes.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("ContactType deleted with values: '{0}'", JsonConvert.SerializeObject(new ContactTypeModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<ContactType> Search(IQueryable<ContactType> query, Expression<Func<ContactType, bool>> filter) {
            return query.Where(filter);
        }
    }
}

