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
    
    
    public class PersonPhoneBuilder : IPersonPhoneBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public PersonPhoneBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<PersonPhone, PersonPhoneModel>>  ProjectToModel {
            get {
                return entity => new PersonPhoneModel(entity);
            }
        }
        
        public async Task<IQueryable<PersonPhoneModel>> GetPersonPhones() {
            return await Task.FromResult(_entities.PersonPhones.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.PersonPhones.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID(int businessEntityID, string phoneNumber, int phoneNumberTypeID) {
            var query = await Search(_entities.PersonPhones, x => x.BusinessEntityID == businessEntityID&& x.PhoneNumber == phoneNumber&& x.PhoneNumberTypeID == phoneNumberTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; PersonPhone with businessEntityID, phoneNumber, phoneNumberTypeID = '{businessEntityID}', '{phoneNumber}', '{phoneNumberTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.PersonPhones, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByPhoneNumberTypeID(int phoneNumberTypeID) {

            var query = await Task.FromResult(Search(_entities.PersonPhones, x => x.PhoneNumberTypeID == phoneNumberTypeID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddPersonPhone(PersonPhoneModel model) {

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            var matchPhoneNumberTypeID = _entities.PhoneNumberTypes.Where(x => x.PhoneNumberTypeID.Equals(model.PhoneNumberTypeID));
            if (!matchPhoneNumberTypeID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.PhoneNumberTypeID) + " '{model.PhoneNumberTypeID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.PersonPhones.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PersonPhone added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new PersonPhoneModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdatePersonPhone(PersonPhoneModel model) {

            var query = Search(_entities.PersonPhones, x =>  x.BusinessEntityID == model.BusinessEntityID && x.PhoneNumber == model.PhoneNumber && x.PhoneNumberTypeID == model.PhoneNumberTypeID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("PersonPhone with _businessEntityID, _phoneNumber, _phoneNumberTypeID = '{0}', '{1}', '{2}' doesn't exist.",model.BusinessEntityID, model.PhoneNumber, model.PhoneNumberTypeID)}; 
            }

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            var matchPhoneNumberTypeID = _entities.PhoneNumberTypes.Where(x => x.PhoneNumberTypeID.Equals(model.PhoneNumberTypeID));
            if (!matchPhoneNumberTypeID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.PhoneNumberTypeID) + string.Format("PhoneNumberTypeID = '{0}' doesn't exist in the system.", model.PhoneNumberTypeID)}; 
            }

            PersonPhone entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PersonPhone update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeletePersonPhone(int businessEntityID, string phoneNumber, int phoneNumberTypeID) {

            var query = Search(_entities.PersonPhones, x => x.BusinessEntityID == businessEntityID&& x.PhoneNumber == phoneNumber&& x.PhoneNumberTypeID == phoneNumberTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("PersonPhone with _businessEntityID, _phoneNumber, _phoneNumberTypeID = '{0}', '{1}', '{2}' doesn't exist.",businessEntityID, phoneNumber, phoneNumberTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.PersonPhones.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PersonPhone deleted with values: '{0}'", JsonConvert.SerializeObject(new PersonPhoneModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<PersonPhone> Search(IQueryable<PersonPhone> query, Expression<Func<PersonPhone, bool>> filter) {
            return query.Where(filter);
        }
    }
}

