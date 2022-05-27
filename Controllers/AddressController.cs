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
    [Route("Address")]
    public class AddressController : ControllerBase {
        
        private IAddressBuilder _builder;
        
        public AddressController(IAddressBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetAddresses() {

            return Ok(await _builder.GetAddresses());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(AddressModel.AddressID));
            propNames.Add(nameof(AddressModel.AddressLine1));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetAddresses();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{addressID}")]
        public async Task<ActionResult> GetAddress_ByAddressID(int addressID) {

             var response = await _builder.GetAddress_ByAddressID(addressID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetAddress_ByStateProvinceID/{stateProvinceID}")]
        public async Task<IQueryable<AddressModel>> GetAddress_ByStateProvinceID(int stateProvinceID) {

            return await _builder.GetAddress_ByStateProvinceID(stateProvinceID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddAddress([FromBody]AddressModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetAddress_ByAddressID", new {addressID = ((AddressModel)response.Model).AddressID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateAddress([FromBody]AddressModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetAddress_ByAddressID", new {addressID = model.AddressID}, model);
        }
        
        [HttpDelete("{addressID}")]
        public async Task<ActionResult> DeleteAddress(int addressID) {

            var response = await _builder.DeleteAddress(addressID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

