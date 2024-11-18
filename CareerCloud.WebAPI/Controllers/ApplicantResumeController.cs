using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic applicantResumeLogic;
        public ApplicantResumeController(CareerCloudContext context)
        {
            applicantResumeLogic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(context));
        }
        [HttpPost]
        [Route("resume")]
        public ActionResult PostApplicantResume(ApplicantResumePoco[] entities)
        {
            applicantResumeLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("resume/{Id}")]
        [ProducesResponseType(typeof(ApplicantResumePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid Id)
        {
            
            var entity = applicantResumeLogic.Get(Id);
            if(entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
            
        }
        [HttpGet]
        [Route("resume")]
        [ProducesResponseType(typeof(List<ApplicantResumePoco>), 200)]
        public ActionResult<List<ApplicantResumePoco>> GetAllApplicantResume()
        {
            var entities = applicantResumeLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("resume")]
        public ActionResult PutApplicantResume(ApplicantResumePoco[] entities)
        {
            try
            {
                applicantResumeLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("resume")]
        public ActionResult DeleteApplicantResume(ApplicantResumePoco[] entities)

        {
            try
            {
                applicantResumeLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
