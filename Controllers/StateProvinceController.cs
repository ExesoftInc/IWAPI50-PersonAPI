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
    [Route("StateProvince")]
    public class StateProvinceController : ControllerBase {
        
        private IStateProvinceBuilder _builder;
        
        public StateProvinceController(IStateProvinceBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetStateProvinces() {

            return Ok(await _builder.GetStateProvinces());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(StateProvinceModel.StateProvinceID));
            propNames.Add(nameof(StateProvinceModel.StateProvinceCode));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetStateProvinces();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{stateProvinceID}")]
        public async Task<ActionResult> GetStateProvince_ByStateProvinceID(int stateProvinceID) {

             var response = await _builder.GetStateProvince_ByStateProvinceID(stateProvinceID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetStateProvince_ByCountryRegionCode/{countryRegionCode}")]
        public async Task<IQueryable<StateProvinceModel>> GetStateProvince_ByCountryRegionCode(string countryRegionCode) {

            return await _builder.GetStateProvince_ByCountryRegionCode(countryRegionCode);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddStateProvince([FromBody]StateProvinceModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddStateProvince(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetStateProvince_ByStateProvinceID", new {stateProvinceID = ((StateProvinceModel)response.Model).StateProvinceID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateStateProvince([FromBody]StateProvinceModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateStateProvince(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetStateProvince_ByStateProvinceID", new {stateProvinceID = model.StateProvinceID}, model);
        }
        
        [HttpDelete("{stateProvinceID}")]
        public async Task<ActionResult> DeleteStateProvince(int stateProvinceID) {

            var response = await _builder.DeleteStateProvince(stateProvinceID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

