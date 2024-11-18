using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic applicantSkillLogic;
        public ApplicantSkillController(CareerCloudContext context)
        {
            applicantSkillLogic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(context));
        }
        [HttpPost]
        [Route("skill")]
        public ActionResult PostApplicantSkill(ApplicantSkillPoco[] entities)
        {
            applicantSkillLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("skill/{Id}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid Id)
        {
            
            var entity = applicantSkillLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
            
        }
        [HttpGet]
        [Route("skill")]
        [ProducesResponseType(typeof(List<ApplicantSkillPoco>), 200)]
        public ActionResult<List<ApplicantSkillPoco>> GetAllApplicantSkill()
        {
            var entities = applicantSkillLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("skill")]
        public ActionResult PutApplicantSkill(ApplicantSkillPoco[] entities)
        {
            try
            {
                applicantSkillLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("skill")]
        public ActionResult DeleteApplicantSkill(ApplicantSkillPoco[] entities)

        {
            try
            {
                applicantSkillLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
