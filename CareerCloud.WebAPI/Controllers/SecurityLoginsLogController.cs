using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic securityLoginsLogLogic;
        public SecurityLoginsLogController(CareerCloudContext context)
        {
            securityLoginsLogLogic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(context));
        }
        [HttpPost]
        [Route("loginsLog")]
        public ActionResult PostSecurityLoginLog(SecurityLoginsLogPoco[] entities)
        {
            securityLoginsLogLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("loginsLog/{Id}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginLog(Guid Id)
        {
            var entity = securityLoginsLogLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("loginsLog")]
        [ProducesResponseType(typeof(List<SecurityLoginsLogPoco>), 200)]
        public ActionResult<List<SecurityLoginsLogPoco>> GetAllSecurityLoginsLog()
        {
            var entities = securityLoginsLogLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("loginsLog")]
        public ActionResult PutSecurityLoginLog(SecurityLoginsLogPoco[] entities)
        {
            try
            {
                securityLoginsLogLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("loginsLog")]
        public ActionResult DeleteSecurityLoginLog(SecurityLoginsLogPoco[] entities)

        {
            try
            {
                securityLoginsLogLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
