using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic systemLanguageCodeLogic;
        public SystemLanguageCodeController(CareerCloudContext context)
        {
            systemLanguageCodeLogic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(context));
        }
        [HttpPost]
        [Route("languageCode")]
        public ActionResult PostSystemLanguageCode(SystemLanguageCodePoco[] entities)
        {
            systemLanguageCodeLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("languageCode/{Id}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemLanguageCode(string Id)
        {
            var entity = systemLanguageCodeLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("languageCode")]
        [ProducesResponseType(typeof(List<SystemLanguageCodePoco>), 200)]
        public ActionResult<List<SystemLanguageCodePoco>> GetAllSystemLanguageCode()
        {
            var entities = systemLanguageCodeLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("languageCode")]
        public ActionResult PutSystemLanguageCode(SystemLanguageCodePoco[] entities)
        {
            try
            {
                systemLanguageCodeLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("languageCode")]
        public ActionResult DeleteSystemLanguageCode(SystemLanguageCodePoco[] entities)

        {
            try
            {
                systemLanguageCodeLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
