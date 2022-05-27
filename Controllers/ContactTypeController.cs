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
    [Route("ContactType")]
    public class ContactTypeController : ControllerBase {
        
        private IContactTypeBuilder _builder;
        
        public ContactTypeController(IContactTypeBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetContactTypes() {

            return Ok(await _builder.GetContactTypes());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(ContactTypeModel.ContactTypeID));
            propNames.Add(nameof(ContactTypeModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetContactTypes();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{contactTypeID}")]
        public async Task<ActionResult> GetContactType_ByContactTypeID(int contactTypeID) {

             var response = await _builder.GetContactType_ByContactTypeID(contactTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddContactType([FromBody]ContactTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddContactType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetContactType_ByContactTypeID", new {contactTypeID = ((ContactTypeModel)response.Model).ContactTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateContactType([FromBody]ContactTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateContactType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetContactType_ByContactTypeID", new {contactTypeID = model.ContactTypeID}, model);
        }
        
        [HttpDelete("{contactTypeID}")]
        public async Task<ActionResult> DeleteContactType(int contactTypeID) {

            var response = await _builder.DeleteContactType(contactTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

