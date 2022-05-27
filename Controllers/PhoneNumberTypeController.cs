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
    [Route("PhoneNumberType")]
    public class PhoneNumberTypeController : ControllerBase {
        
        private IPhoneNumberTypeBuilder _builder;
        
        public PhoneNumberTypeController(IPhoneNumberTypeBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetPhoneNumberTypes() {

            return Ok(await _builder.GetPhoneNumberTypes());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(PhoneNumberTypeModel.PhoneNumberTypeID));
            propNames.Add(nameof(PhoneNumberTypeModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetPhoneNumberTypes();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{phoneNumberTypeID}")]
        public async Task<ActionResult> GetPhoneNumberType_ByPhoneNumberTypeID(int phoneNumberTypeID) {

             var response = await _builder.GetPhoneNumberType_ByPhoneNumberTypeID(phoneNumberTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddPhoneNumberType([FromBody]PhoneNumberTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddPhoneNumberType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetPhoneNumberType_ByPhoneNumberTypeID", new {phoneNumberTypeID = ((PhoneNumberTypeModel)response.Model).PhoneNumberTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdatePhoneNumberType([FromBody]PhoneNumberTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdatePhoneNumberType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetPhoneNumberType_ByPhoneNumberTypeID", new {phoneNumberTypeID = model.PhoneNumberTypeID}, model);
        }
        
        [HttpDelete("{phoneNumberTypeID}")]
        public async Task<ActionResult> DeletePhoneNumberType(int phoneNumberTypeID) {

            var response = await _builder.DeletePhoneNumberType(phoneNumberTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

