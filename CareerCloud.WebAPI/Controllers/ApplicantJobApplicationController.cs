using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic applicantJobApplicationLogic;
        public ApplicantJobApplicationController(CareerCloudContext context)
        {
            applicantJobApplicationLogic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(context));
        }
        [HttpPost]
        [Route("jobApplication")]
        public ActionResult PostApplicantJobApplication(ApplicantJobApplicationPoco[] entities)
        {
            applicantJobApplicationLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("jobApplication/{Id}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantJobApplication(Guid Id)
        {
         
            var entity = applicantJobApplicationLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("jobApplication")]
        [ProducesResponseType(typeof(List<ApplicantJobApplicationPoco>), 200)]
        public ActionResult<List<ApplicantJobApplicationPoco>> GetAllApplicantJobApplication()
        {
            var entities = applicantJobApplicationLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("jobApplication")]
        public ActionResult PutApplicantJobApplication(ApplicantJobApplicationPoco[] entities)
        {
            try
            {
                applicantJobApplicationLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("jobApplication")]
        public ActionResult DeleteApplicantJobApplication(ApplicantJobApplicationPoco[] entities)

        {
            try
            {
                applicantJobApplicationLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
