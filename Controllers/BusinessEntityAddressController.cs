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
    [Route("BusinessEntityAddress")]
    public class BusinessEntityAddressController : ControllerBase {
        
        private IBusinessEntityAddressBuilder _builder;
        
        public BusinessEntityAddressController(IBusinessEntityAddressBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<IList<BusinessEntityAddressModel>> GetBusinessEntityAddresses() {

            return await _builder.GetBusinessEntityAddresses()?.ToListAsync();
        }
        
        [HttpGet("Display")]
        public IList<ExpandoObject> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(BusinessEntityAddressModel.BusinessEntityID));
            propNames.Add(nameof(BusinessEntityAddressModel.AddressID));

            return _builder.GetDisplayModels(propNames);
        }
        
        [HttpGet("Paged")]
        public async Task<IPagedList<BusinessEntityAddressModel>> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetBusinessEntityAddresses()?.ToListAsync();

            return models.ToPagedList(pageIndex, pageSize, 0, models.Count);
        }
        
        [HttpGet("{businessEntityID}/{addressID}/{addressTypeID}")]
        public async Task<ActionResult> GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(int businessEntityID, int addressID, int addressTypeID) {

             var response = await _builder.GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(businessEntityID, addressID, addressTypeID);
            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetBusinessEntityAddress_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IList<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetBusinessEntityAddress_ByBusinessEntityID(businessEntityID)?.ToListAsync();
        }
        
        [HttpGet("GetBusinessEntityAddress_ByAddressID/{addressID}")]
        public async Task<IList<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressID(int addressID) {

            return await _builder.GetBusinessEntityAddress_ByAddressID(addressID)?.ToListAsync();
        }
        
        [HttpGet("GetBusinessEntityAddress_ByAddressTypeID/{addressTypeID}")]
        public async Task<IList<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressTypeID(int addressTypeID) {

            return await _builder.GetBusinessEntityAddress_ByAddressTypeID(addressTypeID)?.ToListAsync();
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddBusinessEntityAddress([FromBody]BusinessEntityAddressModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }

            var response = await _builder.AddBusinessEntityAddress(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(AddBusinessEntityAddress)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return CreatedAtAction("GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID", new {businessEntityID = ((BusinessEntityAddressModel)response.Model).BusinessEntityID, addressID = ((BusinessEntityAddressModel)response.Model).AddressID, addressTypeID = ((BusinessEntityAddressModel)response.Model).AddressTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateBusinessEntityAddress([FromBody]BusinessEntityAddressModel model) {

            if (!ModelState.IsValid) {
              return BadRequest(ModelState);
            }
            var response = await _builder.UpdateBusinessEntityAddress(model);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(UpdateBusinessEntityAddress)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete("{businessEntityID}/{addressID}/{addressTypeID}")]
        public async Task<ActionResult> DeleteBusinessEntityAddress(int businessEntityID, int addressID, int addressTypeID) {

            var response = await _builder.DeleteBusinessEntityAddress(businessEntityID, addressID, addressTypeID);
            if (response.ErrorMessage != null) {
              return BadRequest($"Error for {nameof(DeleteBusinessEntityAddress)}; {response.ErrorMessage}");
            }

            if (response.RequestMessage != null) {
              return BadRequest(response.RequestMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

