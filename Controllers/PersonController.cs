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
    [Route("Person")]
    public class PersonController : ControllerBase {
        
        private IPersonBuilder _builder;
        
        public PersonController(IPersonBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetPeople() {

            return Ok(await _builder.GetPeople());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(PersonModel.BusinessEntityID));
            propNames.Add(nameof(PersonModel.PersonType));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetPeople();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}")]
        public async Task<ActionResult> GetPerson_ByBusinessEntityID(int businessEntityID) {

             var response = await _builder.GetPerson_ByBusinessEntityID(businessEntityID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddPerson([FromBody]PersonModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddPerson(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetPerson_ByBusinessEntityID", new {businessEntityID = ((PersonModel)response.Model).BusinessEntityID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdatePerson([FromBody]PersonModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdatePerson(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetPerson_ByBusinessEntityID", new {businessEntityID = model.BusinessEntityID}, model);
        }
        
        [HttpDelete("{businessEntityID}")]
        public async Task<ActionResult> DeletePerson(int businessEntityID) {

            var response = await _builder.DeletePerson(businessEntityID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

