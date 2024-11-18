using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic companyJobEducationLogic;
        public CompanyJobEducationController(CareerCloudContext context)
        {
            companyJobEducationLogic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>(context));
        }
        [HttpPost]
        [Route("jobEducation")]
        public ActionResult PostCompanyJobEducation(CompanyJobEducationPoco[] entities)
        {
            companyJobEducationLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("jobEducation/{Id}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid Id)
        {
            var entity = companyJobEducationLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("jobEducation")]
        [ProducesResponseType(typeof(List<CompanyJobEducationPoco>), 200)]
        public ActionResult<List<CompanyJobEducationPoco>> GetAllCompanyJobEducation()
        {
            var entities = companyJobEducationLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("jobEducation")]
        public ActionResult PutCompanyJobEducation(CompanyJobEducationPoco[] entities)
        {
            try
            {
                companyJobEducationLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("jobEducation")]
        public ActionResult DeleteCompanyJobEducation(CompanyJobEducationPoco[] entities)

        {
            try
            {
                companyJobEducationLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
