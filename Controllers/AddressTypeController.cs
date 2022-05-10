// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using InstantHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonAPI.Models;
using PersonAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace PersonAPI.Controllers {
    
    
    // Uncomment the following line to use an API Key; change the value of the key in appSetting (X-API-Key)
    // [ApiKey()]
    [Route("AddressType")]
    public class AddressTypeController : ControllerBase {
        
        private IAddressTypeBuilder _builder;
        
        public AddressTypeController(IAddressTypeBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<IList<AddressTypeModel>> GetAddressTypes() {

            return await _builder.GetAddressTypes()?.ToListAsync();
        }
        
        [HttpGet("Display")]
        public IList<ExpandoObject> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(AddressTypeModel.AddressTypeID));
            propNames.Add(nameof(AddressTypeModel.Name));

            return _builder.GetDisplayModels(propNames);
        }
        
        [HttpGet("Paged")]
        public async Task<IPagedList<AddressTypeModel>> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetAddressTypes()?.ToListAsync();

            return models.ToPagedList(pageIndex, pageSize, 0, models.Count);
        }
        
        [HttpGet("{addressTypeID}")]
        public async Task<ActionResult> GetAddressType_ByAddressTypeID(int addressTypeID) {

             var response = await _builder.GetAddressType_ByAddressTypeID(addressTypeID);
            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddAddressType([FromBody]AddressTypeModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }

            var response = await _builder.AddAddressType(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(AddAddressType)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return CreatedAtAction("GetAddressType_ByAddressTypeID", new {addressTypeID = ((AddressTypeModel)response.Model).AddressTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateAddressType([FromBody]AddressTypeModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }
            var response = await _builder.UpdateAddressType(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(UpdateAddressType)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete("{addressTypeID}")]
        public async Task<ActionResult> DeleteAddressType(int addressTypeID) {

            var response = await _builder.DeleteAddressType(addressTypeID);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(DeleteAddressType)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}
