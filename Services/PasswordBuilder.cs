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
    
    
    public class PasswordBuilder : IPasswordBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public PasswordBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Password, PasswordModel>>  ProjectToModel {
            get {
                return entity => new PasswordModel(entity);
            }
        }
        
        public async Task<IQueryable<PasswordModel>> GetPasswords() {
            return await Task.FromResult(_entities.Passwords.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.Passwords.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetPassword_ByBusinessEntityID(int businessEntityID) {
            var query = await Search(_entities.Passwords, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel)?.ToListAsync();
            if (query.Any()) {
                return new BuilderResponse{ Model = query.Single() }; 
            }
            else {
                return new BuilderResponse { ValidationMessage = $"Record Not Found; Password with businessEntityID = '{businessEntityID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddPassword(PasswordModel model) {

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            System.Int32 maxCount = 0;
            if(_entities.Passwords.Count() > 0)
            maxCount = _entities.Passwords.Max(x => x.BusinessEntityID);
            model.BusinessEntityID= ++maxCount;

            var entity = ModelExtender.ToEntity(model);
            _entities.Passwords.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Password added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new PasswordModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdatePassword(PasswordModel model) {

            var query = Search(_entities.Passwords, x =>  x.BusinessEntityID == model.BusinessEntityID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Password with _businessEntityID = '{0}' doesn't exist.",model.BusinessEntityID)}; 
            }

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            Password entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Password update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeletePassword(int businessEntityID) {

            var query = Search(_entities.Passwords, x => x.BusinessEntityID == businessEntityID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Password with _businessEntityID = '{0}' doesn't exist.",businessEntityID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.Passwords.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Password deleted with values: '{0}'", JsonConvert.SerializeObject(new PasswordModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Password> Search(IQueryable<Password> query, Expression<Func<Password, bool>> filter) {
            return query.Where(filter);
        }
    }
}

