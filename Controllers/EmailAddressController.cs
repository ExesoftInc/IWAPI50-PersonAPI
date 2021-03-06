// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

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
    [Route("EmailAddress")]
    public class EmailAddressController : ControllerBase {
        
        private IEmailAddressBuilder _builder;
        
        public EmailAddressController(IEmailAddressBuilder builder) {
            _builder = builder;
        }
        
        [HttpGet("")]
        public async Task<ActionResult> GetEmailAddresses() {

            return Ok(await _builder.GetEmailAddresses());
        }
        
        [HttpGet("Display")]
        public async Task<ActionResult> GetDisplayModels() {
            //List all model properties that should be displayed
            //Here only a couple have been added as an example
            var propNames = new List<string>();
            propNames.Add(nameof(EmailAddressModel.BusinessEntityID));
            propNames.Add(nameof(EmailAddressModel.EmailAddressID));

            return Ok(await Task.FromResult(_builder.GetDisplayModels(propNames)));
        }
        
        [HttpGet("Paged")]
        public async Task<ActionResult> Paged(int pageIndex, int pageSize) {

            var models = await _builder.GetEmailAddresses();

            return Ok(models.ToPagedList(pageIndex, pageSize, 0, models.Count()));
        }
        
        [HttpGet("{businessEntityID}/{emailAddressID}")]
        public async Task<ActionResult> GetEmailAddress_ByBusinessEntityIDEmailAddressID(int businessEntityID, int emailAddressID) {

             var response = await _builder.GetEmailAddress_ByBusinessEntityIDEmailAddressID(businessEntityID, emailAddressID);
            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return Ok(response.Model);
        }
        
        [HttpGet("GetEmailAddress_ByBusinessEntityID/{businessEntityID}")]
        public async Task<IQueryable<EmailAddressModel>> GetEmailAddress_ByBusinessEntityID(int businessEntityID) {

            return await _builder.GetEmailAddress_ByBusinessEntityID(businessEntityID);
        }
        
        [HttpPost("")]
        [ModelStateValidation()]
        public async Task<ActionResult> AddEmailAddress([FromBody]EmailAddressModel model) {

            var response = await _builder.AddEmailAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return CreatedAtAction("GetEmailAddress_ByBusinessEntityIDEmailAddressID", new {businessEntityID = ((EmailAddressModel)response.Model).BusinessEntityID, emailAddressID = ((EmailAddressModel)response.Model).EmailAddressID}, response.Model);
        }
        
        [HttpPut("")]
        [ModelStateValidation()]
        public async Task<ActionResult> UpdateEmailAddress([FromBody]EmailAddressModel model) {
            var response = await _builder.UpdateEmailAddress(model);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return AcceptedAtAction("GetEmailAddress_ByBusinessEntityIDEmailAddressID", new {businessEntityID = model.BusinessEntityID, emailAddressID = model.EmailAddressID}, model);
        }
        
        [HttpDelete("{businessEntityID}/{emailAddressID}")]
        public async Task<ActionResult> DeleteEmailAddress(int businessEntityID, int emailAddressID) {

            var response = await _builder.DeleteEmailAddress(businessEntityID, emailAddressID);

            if (response.ValidationMessage != null) {
                return BadRequest(response.ValidationMessage);
            }

            return StatusCode(response.StatusCode);
        }
    }
}

