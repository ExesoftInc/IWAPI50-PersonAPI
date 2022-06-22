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
    
    
    public class PersonBuilder : IPersonBuilder {
        
        private IDbEntities _entities;
        
        private ILoggerManager _logger;
        
        public PersonBuilder(EntitiesContext context, ILoggerManager logger) {
            _entities = context;
            _logger = logger;
        }
        
        private Expression<Func<Person, PersonModel>>  ProjectToModel {
            get {
                return entity => new PersonModel(entity);
            }
        }
        
        public async Task<IQueryable<PersonModel>> GetPeople() {
            return await Task.FromResult(_entities.People.Select(ProjectToModel));
        }
        
        public IList<ExpandoObject> GetDisplayModels(List<string> propNames) {
            var models = _entities.People.Select(ProjectToModel);

            var displayModels = new List<ExpandoObject>();
            foreach (var model in models)
            {
                dynamic displayModel = DynamicHelper.ConvertToExpando(model, propNames);
                displayModels.Add(displayModel);
            }

            return displayModels;
        }
        
        public async Task<BuilderResponse> GetPerson_ByBusinessEntityID(int businessEntityID) {
            var query = Search(_entities.People, x => x.BusinessEntityID == businessEntityID).Select(ProjectToModel);
            if (query.Any()) {
                return await Task.FromResult(new BuilderResponse{ Model = query.Single() }); 
            }
            else {
                return await Task.FromResult(new BuilderResponse { ValidationMessage = $"Record Not Found; Person with businessEntityID = '{businessEntityID}' doesn't exist." }); 
            }
        }
        
        public async Task<BuilderResponse> AddPerson(PersonModel model) {

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
                return new BuilderResponse { ValidationMessage = $"Foreign Key Violation; " + nameof(model.BusinessEntityID) + " '{model.BusinessEntityID}' doesn't exist in the system."}; 
            }

            System.Int32 maxCount = 0;
            if(_entities.People.Count() > 0)
            maxCount = _entities.People.Max(x => x.BusinessEntityID);
            model.BusinessEntityID= ++maxCount;

            var entity = ModelExtender.ToEntity(model);
            _entities.People.Add(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Person added with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ Model = new PersonModel(entity) }; 
        }
        
        public async Task<BuilderResponse> UpdatePerson(PersonModel model) {

            var query = Search(_entities.People, x =>  x.BusinessEntityID == model.BusinessEntityID);
            if (!query.Any()) {
            return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Person with _businessEntityID = '{0}' doesn't exist.",model.BusinessEntityID)}; 
            }

            var matchBusinessEntityID = _entities.BusinessEntities.Where(x => x.BusinessEntityID.Equals(model.BusinessEntityID));
            if (!matchBusinessEntityID.Any()) {
            return new BuilderResponse { ValidationMessage = "Foreign Key Violation; " + nameof(model.BusinessEntityID) + string.Format("BusinessEntityID = '{0}' doesn't exist in the system.", model.BusinessEntityID)}; 
            }

            Person entity = query.SingleOrDefault();

            entity = model.ToEntity(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Person update with values: '{0}'", JsonConvert.SerializeObject(model)));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Accepted }; 
        }
        
        public async Task<BuilderResponse> DeletePerson(int businessEntityID) {

            var query = Search(_entities.People, x => x.BusinessEntityID == businessEntityID);
            if (!query.Any()) {
                return new BuilderResponse { ValidationMessage = "Record Not Found; " + string.Format("Person with _businessEntityID = '{0}' doesn't exist.",businessEntityID)}; 
            }

            var entity = query.SingleOrDefault();

            _entities.People.Remove(entity);
            await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("Person deleted with values: '{0}'", JsonConvert.SerializeObject(new PersonModel(entity))));

            return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
        }
        
        private IQueryable<Person> Search(IQueryable<Person> query, Expression<Func<Person, bool>> filter) {
            return query.Where(filter);
        }
    }
}

