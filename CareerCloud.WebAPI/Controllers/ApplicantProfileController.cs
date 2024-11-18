using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic applicantProfileLogic;
        public ApplicantProfileController(CareerCloudContext context)
        {
            applicantProfileLogic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(context));
        }
        [HttpPost]
        [Route("profile")]
        public ActionResult PostApplicantProfile(ApplicantProfilePoco[] entities)
        {
            applicantProfileLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("profile/{Id}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantProfile(Guid Id)
        {
           
            var entity = applicantProfileLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(List<ApplicantProfilePoco>), 200)]
        public ActionResult<List<ApplicantProfilePoco>> GetAllApplicantProfile()
        {
            var entities = applicantProfileLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("profile")]
        public ActionResult PutApplicantProfile(ApplicantProfilePoco[] entities)
        {
            try
            {
                applicantProfileLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteApplicantProfile(ApplicantProfilePoco[] entities)

        {
            try
            {
                applicantProfileLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
