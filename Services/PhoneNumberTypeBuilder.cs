// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using AutoMapper;
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
        
        private IMapper _mapper;
        
        private ILoggerManager _logger;
        
        public PhoneNumberTypeBuilder(EntitiesContext context, IMapper mapper, ILoggerManager logger) {
            _entities = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        private Expression<Func<PhoneNumberType, PhoneNumberTypeModel>>  ProjectToModel {
            get {
                return entity => _mapper.Map<PhoneNumberTypeModel>(entity);
            }
        }
        
        public IQueryable<PhoneNumberTypeModel> GetPhoneNumberTypes() {
            return _entities.PhoneNumberTypes.Select(ProjectToModel);
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
           return new BuilderResponse { RequestMessage = $"Record Not Found; PhoneNumberType with phoneNumberTypeID = '{phoneNumberTypeID}' doesn't exist." }; 
            }
        }
        
        public async Task<BuilderResponse> AddPhoneNumberType(PhoneNumberTypeModel model) {
           try
           {
                 var entity = _mapper.Map<PhoneNumberType>(model);
                _entities.PhoneNumberTypes.Add(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("PhoneNumberType added with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ Model = new PhoneNumberTypeModel(entity) }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert explicit value for identity column"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "IDENTITY_INSERT is set to OFF; Cannot insert explicit value for identity column when IDENTITY_INSERT is set to OFF."};
                }
                else if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> UpdatePhoneNumberType(PhoneNumberTypeModel model) {

          var query = Search(_entities.PhoneNumberTypes, x =>  x.PhoneNumberTypeID == model.PhoneNumberTypeID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("PhoneNumberType with _phoneNumberTypeID = '{0}' doesn't exist.",model.PhoneNumberTypeID)}; 
            }
           try
           {
            PhoneNumberType entity = query.SingleOrDefault();
             entity = model.ToEntity(entity);
               await _entities.SaveChangesAsync();
                _logger.LogInfo(string.Format("PhoneNumberType update with values: '{0}'", JsonConvert.SerializeObject(model)));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.Created }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("Cannot insert duplicate key row"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + Environment.NewLine + JsonConvert.SerializeObject(model) + Environment.NewLine + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Duplicate exception; Please verify that an item with these values doesn't already exists."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        public async Task<BuilderResponse> DeletePhoneNumberType(int phoneNumberTypeID) {
          var query = Search(_entities.PhoneNumberTypes, x => x.PhoneNumberTypeID == phoneNumberTypeID);
            if (!query.Any()) {
              return new BuilderResponse { RequestMessage = "Record Not Found; " + string.Format("PhoneNumberType with _phoneNumberTypeID = '{0}' doesn't exist.",phoneNumberTypeID)}; 
            }
            var entity = query.SingleOrDefault();

           try
           {
                _entities.PhoneNumberTypes.Remove(entity);
               await _entities.SaveChangesAsync();
            _logger.LogInfo(string.Format("PhoneNumberType deleted with values: '{0}'", JsonConvert.SerializeObject(new PhoneNumberTypeModel(entity))));
               return new BuilderResponse{ StatusCode = (int)HttpStatusCode.NoContent }; 
            }
            catch (DbUpdateException ue)
            {
                if(ue.InnerException != null && ue.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    var inner = ue.InnerException;
                    _logger.LogError(inner.Message + inner.StackTrace);
                    return new BuilderResponse { ErrorMessage = "Please delete related items first."};
                }
                _logger.LogError(ue.Message + ue.StackTrace);
                return new BuilderResponse { ErrorMessage = ue.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
                return new BuilderResponse { ErrorMessage = e.Message};
            }
        }
        
        private IQueryable<PhoneNumberType> Search(IQueryable<PhoneNumberType> query, Expression<Func<PhoneNumberType, bool>> filter) {
            return query.Where(filter);
        }
    }
}

