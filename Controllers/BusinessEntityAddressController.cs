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
    [Route("BusinessEntityAddress")]
    public class BusinessEntityAddressController : ControllerBase {
        
        private IBusinessEntityAddressBuilder _builder;
        
        public BusinessEntityAddressController(IBusinessEntityAddressBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetBusinessEntityAddresses() {

            return Ok(await _builder.GetBusinessEntityAddresses());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(BusinessEntityAddressModel.BusinessEntityID));
            propNames.Add(nameof(BusinessEntityAddressModel.AddressID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetBusinessEntityAddresses();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{addressID}/{addressTypeID}")]
        public async Task<ActionResult> GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(int businessEntityID, int addressID, int addressTypeID) {

             var response = await _builder.GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID(businessEntityID, addressID, addressTypeID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetBusinessEntityAddress_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetBusinessEntityAddress_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpGet("GetBusinessEntityAddress_ByAddressID/{addressID}")]
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressID(int addressID) {

            return await _builder.GetBusinessEntityAddress_ByAddressID(addressID);
        }
        
        [HttpGet("GetBusinessEntityAddress_ByAddressTypeID/{addressTypeID}")]
        public async Task<IQueryable<BusinessEntityAddressModel>> GetBusinessEntityAddress_ByAddressTypeID(int addressTypeID) {

            return await _builder.GetBusinessEntityAddress_ByAddressTypeID(addressTypeID);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddBusinessEntityAddress([FromBody]BusinessEntityAddressModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddBusinessEntityAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID", new {businessEntityID = ((BusinessEntityAddressModel)response.Model).BusinessEntityID, addressID = ((BusinessEntityAddressModel)response.Model).AddressID, addressTypeID = ((BusinessEntityAddressModel)response.Model).AddressTypeID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateBusinessEntityAddress([FromBody]BusinessEntityAddressModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateBusinessEntityAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetBusinessEntityAddress_ByBusinessEntityIDAddressIDAddressTypeID", new {businessEntityID = model.BusinessEntityID, addressID = model.AddressID, addressTypeID = model.AddressTypeID}, model);
        }
        
        [HttpDelete("{businessEntityID}/{addressID}/{addressTypeID}")]
        public async Task<ActionResult> DeleteBusinessEntityAddress(int businessEntityID, int addressID, int addressTypeID) {

            var response = await _builder.DeleteBusinessEntityAddress(businessEntityID, addressID, addressTypeID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

