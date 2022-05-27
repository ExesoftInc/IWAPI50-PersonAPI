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
    
    
    public class PhoneNumberTypeBuilder : IPhoneNumberTypeBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public PhoneNumberTypeBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<PhoneNumberType, PhoneNumberTypeModel>>  ProjectToModel {
            get {
                return entity => new PhoneNumberTypeModel(entity);
            }
        }
        
        public async Task<IQueryable<PhoneNumberTypeModel>> GetPhoneNumberTypes() {
            return await Task.FromResult(_entities.PhoneNumberTypes.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.PhoneNumberTypes.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetPhoneNumberType_ByPhoneNumberTypeID(int phoneNumberTypeID) {
            var query = await Search(_entities.PhoneNumberTypes, x => x.PhoneNumberTypeID == phoneNumberTypeID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; PhoneNumberType with phoneNumberTypeID = '{phoneNumberTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddPhoneNumberType(PhoneNumberTypeModel model) {


            var entity = ModelExtender.ToEntity(model);
            _entities.PhoneNumberTypes.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PhoneNumberType added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new PhoneNumberTypeModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdatePhoneNumberType(PhoneNumberTypeModel model) {

            var query = Search(_entities.PhoneNumberTypes, x =>  x.PhoneNumberTypeID == model.PhoneNumberTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("PhoneNumberType with _phoneNumberTypeID = '{0}' doesn't exist.",model.PhoneNumberTypeID)}; 
            }

            PhoneNumberType entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PhoneNumberType update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeletePhoneNumberType(int phoneNumberTypeID) {

            var query = Search(_entities.PhoneNumberTypes, x => x.PhoneNumberTypeID == phoneNumberTypeID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("PhoneNumberType with _phoneNumberTypeID = '{0}' doesn't exist.",phoneNumberTypeID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.PhoneNumberTypes.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PhoneNumberType deleted with values: '{0}'", JsonConvert.SerializeObject(new PhoneNumberTypeModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<PhoneNumberType> Search(IQueryable<PhoneNumberType> query, Expression<Func<PhoneNumberType, bool>> filter) {
            return query.Where(filter);
        }
    }
}

