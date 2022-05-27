using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using PersonAPI.Models;
using PersonAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonAPI.Controllers {
    
    
    // TODO: Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("BusinessEntityContact")]
    public class BusinessEntityContactController : ControllerBase {
        
        private IBusinessEntityContactBuilder _builder;
        
        public BusinessEntityContactController(IBusinessEntityContactBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetBusinessEntityContacts() {

            return Ok(await _builder.GetBusinessEntityContacts());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(BusinessEntityContactModel.BusinessEntityID));
            propNames.Add(nameof(BusinessEntityContactModel.PersonID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetBusinessEntityContacts();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{personID}/{contactTypeID}")]
        public async Task<ActionResult> GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID(int businessEntityID, int personID, int contactTypeID) {

             var response = await _builder.GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID(businessEntityID, personID, contactTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetBusinessEntityContact_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetBusinessEntityContact_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpGet("GetBusinessEntityContact_ByPersonID/{personID}")]
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByPersonID(int personID) {

            return await _builder.GetBusinessEntityContact_ByPersonID(personID);
        }
        
        [HttpGet("GetBusinessEntityContact_ByContactTypeID/{contactTypeID}")]
        public async Task<IQueryable<BusinessEntityContactModel>> GetBusinessEntityContact_ByContactTypeID(int contactTypeID) {

            return await _builder.GetBusinessEntityContact_ByContactTypeID(contactTypeID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddBusinessEntityContact([FromBody]BusinessEntityContactModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddBusinessEntityContact(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID", new {businessEntityID = ((BusinessEntityContactModel)response.Model).BusinessEntityID, personID = ((BusinessEntityContactModel)response.Model).PersonID, contactTypeID = ((BusinessEntityContactModel)response.Model).ContactTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateBusinessEntityContact([FromBody]BusinessEntityContactModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateBusinessEntityContact(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetBusinessEntityContact_ByBusinessEntityIDPersonIDContactTypeID", new {businessEntityID = model.BusinessEntityID, personID = model.PersonID, contactTypeID = model.ContactTypeID}, model);
        }
        
        [HttpDelete("{businessEntityID}/{personID}/{contactTypeID}")]
        public async Task<ActionResult> DeleteBusinessEntityContact(int businessEntityID, int personID, int contactTypeID) {

            var response = await _builder.DeleteBusinessEntityContact(businessEntityID, personID, contactTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

