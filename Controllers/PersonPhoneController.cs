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
    [Route("PersonPhone")]
    public class PersonPhoneController : ControllerBase {
        
        private IPersonPhoneBuilder _builder;
        
        public PersonPhoneController(IPersonPhoneBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetPersonPhones() {

            return Ok(await _builder.GetPersonPhones());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(PersonPhoneModel.BusinessEntityID));
            propNames.Add(nameof(PersonPhoneModel.PhoneNumber));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetPersonPhones();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{phoneNumber}/{phoneNumberTypeID}")]
        public async Task<ActionResult> GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID(int businessEntityID, string phoneNumber, int phoneNumberTypeID) {

             var response = await _builder.GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID(businessEntityID, phoneNumber, phoneNumberTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetPersonPhone_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetPersonPhone_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpGet("GetPersonPhone_ByPhoneNumberTypeID/{phoneNumberTypeID}")]
        public async Task<IQueryable<PersonPhoneModel>> GetPersonPhone_ByPhoneNumberTypeID(int phoneNumberTypeID) {

            return await _builder.GetPersonPhone_ByPhoneNumberTypeID(phoneNumberTypeID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddPersonPhone([FromBody]PersonPhoneModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddPersonPhone(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID", new {businessEntityID = ((PersonPhoneModel)response.Model).BusinessEntityID, phoneNumber = ((PersonPhoneModel)response.Model).PhoneNumber, phoneNumberTypeID = ((PersonPhoneModel)response.Model).PhoneNumberTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdatePersonPhone([FromBody]PersonPhoneModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdatePersonPhone(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetPersonPhone_ByBusinessEntityIDPhoneNumberPhoneNumberTypeID", new {businessEntityID = model.BusinessEntityID, phoneNumber = model.PhoneNumber, phoneNumberTypeID = model.PhoneNumberTypeID}, model);
        }
        
        [HttpDelete("{businessEntityID}/{phoneNumber}/{phoneNumberTypeID}")]
        public async Task<ActionResult> DeletePersonPhone(int businessEntityID, string phoneNumber, int phoneNumberTypeID) {

            var response = await _builder.DeletePersonPhone(businessEntityID, phoneNumber, phoneNumberTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

