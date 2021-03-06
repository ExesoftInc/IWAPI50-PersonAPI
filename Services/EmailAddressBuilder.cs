// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;
using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace PersonAPI.Services {
    
    
    public class EmailAddressBuilder : IEmailAddressBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public EmailAddressBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<EmailAddress, EmailAddressModel>>  ProjectToModel {
            get {
                return entity => new EmailAddressModel(entity);
            }
        }
        
        public async Task<IQueryable<EmailAddressModel>> GetEmailAddresses() {
            return await Task.FromResult(_entities.EmailAddresses.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.EmailAddresses.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetEmailAddress_ByBusinessEntityIDEmailAddressID(int businessEntityID, int emailAddressID) {
            var query = Search(_entities.EmailAddresses, x => x.BusinessEntityID == businessEntityID&& x.EmailAddressID == emailAddressID).Select(ProjectToModel);
            if (query.Any()) {
                return await Task.FromResult(new BuilderResponse{ Model = query.Single() }); 
            }
            else {
                return await Task.FromResult(new BuilderResponse { ValidationMessage = $"Record Not Found; EmailAddress with businessEntityID, emailAddressID = '{businessEntityID}', '{emailAddressID}' doesn't exist." }); 
            }
        }
        
        public async Task<IQueryable<EmailAddressModel>> GetEmailAddress_ByBusinessEntityID(int businessEntityID) {

            var query = await Task.FromResult(Search(_entities.EmailAddresses, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel));

            return query;
        }
        
        public async Task<BuilderResponse> AddEmailAddress(EmailAddressModel model) {

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }


            var entity = ModelExtender.ToEntity(model);
            _entities.EmailAddresses.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmailAddress added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new EmailAddressModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdateEmailAddress(EmailAddressModel model) {

            var query = Search(_entities.EmailAddresses, x =>  x.BusinessEntityID == model.BusinessEntityID && x.EmailAddressID == model.EmailAddressID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmailAddress with _businessEntityID, _emailAddressID = '{0}', '{1}' doesn't exist.",model.BusinessEntityID, model.EmailAddressID)}; 
            }

            var matchBusinessEntityID = _entities.People.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse{ ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("EmailAddress with BusinessEntityID = '{0}' doesn't exist.", model.BusinessEntityID)}; 
            }

            EmailAddress entity = query.SingleOrDefault();
            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmailAddress update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeleteEmailAddress(int businessEntityID, int emailAddressID) {

            var query = Search(_entities.EmailAddresses, x => x.BusinessEntityID == businessEntityID&& x.EmailAddressID == emailAddressID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("EmailAddress with _businessEntityID, _emailAddressID = '{0}', '{1}' doesn't exist.",businessEntityID, emailAddressID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.EmailAddresses.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("EmailAddress deleted with values: '{0}'", JsonConvert.SerializeObject(new EmailAddressModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<EmailAddress> Search(IQueryable<EmailAddress> query, Expression<Func<EmailAddress, bool>> filter) {
            return query.Where(filter);
        }
    }
}

