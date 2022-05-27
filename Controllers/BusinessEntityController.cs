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
    [Route("BusinessEntity")]
    public class BusinessEntityController : ControllerBase {
        
        private IBusinessEntityBuilder _builder;
        
        public BusinessEntityController(IBusinessEntityBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetBusinessEntities() {

            return Ok(await _builder.GetBusinessEntities());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(BusinessEntityModel.BusinessEntityID));
            propNames.Add(nameof(BusinessEntityModel.Rowguid));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetBusinessEntities();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}")]
        public async Task<ActionResult> GetBusinessEntity_ByBusinessEntityID(int businessEntityID) {

             var response = await _builder.GetBusinessEntity_ByBusinessEntityID(businessEntityID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddBusinessEntity([FromBody]BusinessEntityModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddBusinessEntity(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetBusinessEntity_ByBusinessEntityID", new {businessEntityID = ((BusinessEntityModel)response.Model).BusinessEntityID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateBusinessEntity([FromBody]BusinessEntityModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateBusinessEntity(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetBusinessEntity_ByBusinessEntityID", new {businessEntityID = model.BusinessEntityID}, model);
        }
        
        [HttpDelete("{businessEntityID}")]
        public async Task<ActionResult> DeleteBusinessEntity(int businessEntityID) {

            var response = await _builder.DeleteBusinessEntity(businessEntityID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

