using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic applicantWorkHistoryLogic;
        public ApplicantWorkHistoryController(CareerCloudContext context)
        {
            applicantWorkHistoryLogic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(context));
        }
        [HttpPost]
        [Route("workHistory")]
        public ActionResult PostApplicantWorkHistory(ApplicantWorkHistoryPoco[] entities)
        {
            applicantWorkHistoryLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("workHistory/{Id}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantWorkHistory(Guid Id)
        {

            var entity = applicantWorkHistoryLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("workHistory")]
        [ProducesResponseType(typeof(List<ApplicantWorkHistoryPoco>), 200)]
        public ActionResult<List<ApplicantWorkHistoryPoco>> GetAllApplicantWorkHistory()
        {
            var entities = applicantWorkHistoryLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("workHistory")]
        public ActionResult PutApplicantWorkHistory(ApplicantWorkHistoryPoco[] entities)
        {
            try
            {
                applicantWorkHistoryLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("workHistory")]
        public ActionResult DeleteApplicantWorkHistory(ApplicantWorkHistoryPoco[] entities)

        {
            try
            {
                applicantWorkHistoryLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
