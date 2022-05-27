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
    [Route("CountryRegion")]
    public class CountryRegionController : ControllerBase {
        
        private ICountryRegionBuilder _builder;
        
        public CountryRegionController(ICountryRegionBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetCountryRegions() {

            return Ok(await _builder.GetCountryRegions());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(CountryRegionModel.CountryRegionCode));
            propNames.Add(nameof(CountryRegionModel.Name));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetCountryRegions();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{countryRegionCode}")]
        public async Task<ActionResult> GetCountryRegion_ByCountryRegionCode(string countryRegionCode) {

             var response = await _builder.GetCountryRegion_ByCountryRegionCode(countryRegionCode);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddCountryRegion([FromBody]CountryRegionModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddCountryRegion(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetCountryRegion_ByCountryRegionCode", new {countryRegionCode = ((CountryRegionModel)response.Model).CountryRegionCode}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdateCountryRegion([FromBody]CountryRegionModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdateCountryRegion(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetCountryRegion_ByCountryRegionCode", new {countryRegionCode = model.CountryRegionCode}, model);
        }
        
        [HttpDelete("{countryRegionCode}")]
        public async Task<ActionResult> DeleteCountryRegion(string countryRegionCode) {

            var response = await _builder.DeleteCountryRegion(countryRegionCode);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

