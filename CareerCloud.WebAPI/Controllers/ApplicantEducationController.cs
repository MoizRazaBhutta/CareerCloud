using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic applicantEducationLogic;
        public ApplicantEducationController(CareerCloudContext context)
        {
            applicantEducationLogic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(context));
        }
        [HttpPost]
        [Route("education")]
        public ActionResult PostApplicantEducation(ApplicantEducationPoco[] entities)
        {
            applicantEducationLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("education/{Id}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid Id)
        {
           
            var entity = applicantEducationLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("education")]
        [ProducesResponseType(typeof(List<ApplicantEducationPoco>), 200)]
        public ActionResult<List<ApplicantEducationPoco>> GetAllApplicantEducation()
        {
            var entities = applicantEducationLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("education")]
        public ActionResult PutApplicantEducation(ApplicantEducationPoco[] entities)
        {
            try
            {
                applicantEducationLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("education")]
        public ActionResult DeleteApplicantEducation(ApplicantEducationPoco[] entities)

        {
            try
            {
                applicantEducationLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
