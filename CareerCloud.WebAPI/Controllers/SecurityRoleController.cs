using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic securityRoleLogic;
        public SecurityRoleController(CareerCloudContext context)
        {
            securityRoleLogic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(context));
        }
        [HttpPost]
        [Route("role")]
        public ActionResult PostSecurityRole(SecurityRolePoco[] entities)
        {
            securityRoleLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("role/{Id}")]
        [ProducesResponseType(typeof(SecurityRolePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid Id)
        {
            var entity = securityRoleLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("role")]
        [ProducesResponseType(typeof(List<SecurityRolePoco>), 200)]
        public ActionResult<List<SecurityRolePoco>> GetAllSecurityRole()
        {
            var entities = securityRoleLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("role")]
        public ActionResult PutSecurityRole(SecurityRolePoco[] entities)
        {
            try
            {
                securityRoleLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("role")]
        public ActionResult DeleteSecurityRole(SecurityRolePoco[] entities)

        {
            try
            {
                securityRoleLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
