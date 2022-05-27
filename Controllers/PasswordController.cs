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
    [Route("Password")]
    public class PasswordController : ControllerBase {
        
        private IPasswordBuilder _builder;
        
        public PasswordController(IPasswordBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetPasswords() {

            return Ok(await _builder.GetPasswords());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(PasswordModel.BusinessEntityID));
            propNames.Add(nameof(PasswordModel.PasswordHash));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetPasswords();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}")]
        public async Task<ActionResult> GetPassword_ByBusinessEntityID(int businessEntityID) {

             var response = await _builder.GetPassword_ByBusinessEntityID(businessEntityID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpPost("")]
        public async Task<ActionResult> AddPassword([FromBody]PasswordModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var response = await _builder.AddPassword(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetPassword_ByBusinessEntityID", new {businessEntityID = ((PasswordModel)response.Model).BusinessEntityID}, response.Model);
        }
        
        [HttpPut("")]
        public async Task<ActionResult> UpdatePassword([FromBody]PasswordModel model) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var response = await _builder.UpdatePassword(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetPassword_ByBusinessEntityID", new {businessEntityID = model.BusinessEntityID}, model);
        }
        
        [HttpDelete("{businessEntityID}")]
        public async Task<ActionResult> DeletePassword(int businessEntityID) {

            var response = await _builder.DeletePassword(businessEntityID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

