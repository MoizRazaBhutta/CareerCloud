using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic securityLoginLogic;
        public SecurityLoginController(CareerCloudContext context)
        {
            securityLoginLogic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(context));
        }
        [HttpPost]
        [Route("login")]
        public ActionResult PostSecurityLogin(SecurityLoginPoco[] entities)
        {
            securityLoginLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("login/{Id}")]
        [ProducesResponseType(typeof(SecurityLoginPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid Id)
        {
            var entity = securityLoginLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("login")]
        [ProducesResponseType(typeof(List<SecurityLoginPoco>), 200)]
        public ActionResult<List<SecurityLoginPoco>> GetAllSecurityLogin()
        {
            var entities = securityLoginLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin(SecurityLoginPoco[] entities)
        {
            try
            {
                securityLoginLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin(SecurityLoginPoco[] entities)

        {
            try
            {
                securityLoginLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
