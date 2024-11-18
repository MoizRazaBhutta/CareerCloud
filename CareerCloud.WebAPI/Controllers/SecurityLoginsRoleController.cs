using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic securityLoginsRoleLogic;
        public SecurityLoginsRoleController(CareerCloudContext context)
        {
            securityLoginsRoleLogic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(context));
        }
        [HttpPost]
        [Route("loginsRole")]
        public ActionResult PostSecurityLoginRole(SecurityLoginsRolePoco[] entities)
        {
            securityLoginsRoleLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("loginsRole/{Id}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid Id)
        {
            var entity = securityLoginsRoleLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("loginsRole")]
        [ProducesResponseType(typeof(List<SecurityLoginsRolePoco>), 200)]
        public ActionResult<List<SecurityLoginsRolePoco>> GetAllSecurityLoginsRole()
        {
            var entities = securityLoginsRoleLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("loginsRole")]
        public ActionResult PutSecurityLoginRole(SecurityLoginsRolePoco[] entities)
        {
            try
            {
                securityLoginsRoleLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("loginsRole")]
        public ActionResult DeleteSecurityLoginRole(SecurityLoginsRolePoco[] entities)

        {
            try
            {
                securityLoginsRoleLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
