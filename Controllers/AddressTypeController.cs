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
    [Route("AddressType")]
    public class AddressTypeController : ControllerBase {
        
        private IAddressTypeBuilder _builder;
        
        public AddressTypeController(IAddressTypeBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetAddressTypes() {

            return Ok(await _builder.GetAddressTypes());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(AddressTypeModel.AddressTypeID));
            propNames.Add(nameof(AddressTypeModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetAddressTypes();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{addressTypeID}")]
        public async Task<ActionResult> GetAddressType_ByAddressTypeID(int addressTypeID) {

             var response = await _builder.GetAddressType_ByAddressTypeID(addressTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddAddressType([FromBody]AddressTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddAddressType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetAddressType_ByAddressTypeID", new {addressTypeID = ((AddressTypeModel)response.Model).AddressTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateAddressType([FromBody]AddressTypeModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateAddressType(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetAddressType_ByAddressTypeID", new {addressTypeID = model.AddressTypeID}, model);
        }
        
        [HttpDelete("{addressTypeID}")]
        public async Task<ActionResult> DeleteAddressType(int addressTypeID) {

            var response = await _builder.DeleteAddressType(addressTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

