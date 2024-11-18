using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic companyJobSkillLogic;
        public CompanyJobSkillController(CareerCloudContext context)
        {
            companyJobSkillLogic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>(context));
        }
        [HttpPost]
        [Route("jobSkill")]
        public ActionResult PostCompanyJobSkill(CompanyJobSkillPoco[] entities)
        {
            companyJobSkillLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("jobSkill/{Id}")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobSkill(Guid Id)
        {
            var entity = companyJobSkillLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("jobSkill")]
        [ProducesResponseType(typeof(List<CompanyJobSkillPoco>), 200)]
        public ActionResult<List<CompanyJobSkillPoco>> GetAllCompanyJobSkill()
        {
            var entities = companyJobSkillLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("jobSkill")]
        public ActionResult PutCompanyJobSkill(CompanyJobSkillPoco[] entities)
        {
            try
            {
                companyJobSkillLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("jobSkill")]
        public ActionResult DeleteCompanyJobSkill(CompanyJobSkillPoco[] entities)

        {
            try
            {
                companyJobSkillLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
