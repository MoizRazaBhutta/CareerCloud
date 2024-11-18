using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic systemCountryCodeLogic;
        public SystemCountryCodeController(CareerCloudContext context)
        {
            systemCountryCodeLogic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>(context));
        }
        [HttpPost]
        [Route("countryCode")]
        public ActionResult PostSystemCountryCode(SystemCountryCodePoco[] entities)
        {
            systemCountryCodeLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("countryCode/{Id}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string Id)
        {
            var entity = systemCountryCodeLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("countryCode")]
        [ProducesResponseType(typeof(List<SystemCountryCodePoco>), 200)]
        public ActionResult<List<SystemCountryCodePoco>> GetAllSystemCountryCode()
        {
            var entities = systemCountryCodeLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("countryCode")]
        public ActionResult PutSystemCountryCode(SystemCountryCodePoco[] entities)
        {
            try
            {
                systemCountryCodeLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("countryCode")]
        public ActionResult DeleteSystemCountryCode(SystemCountryCodePoco[] entities)

        {
            try
            {
                systemCountryCodeLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
