using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic companyJobDescriptionLogic;
        public CompanyJobsDescriptionController(CareerCloudContext context)
        {
            companyJobDescriptionLogic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>(context));
        }
        [HttpPost]
        [Route("jobDescription")]
        public ActionResult PostCompanyJobsDescription(CompanyJobDescriptionPoco[] entities)
        {
            companyJobDescriptionLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("jobDescription/{Id}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobsDescription(Guid Id)
        {
            var entity = companyJobDescriptionLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("jobDescription")]
        [ProducesResponseType(typeof(List<CompanyJobDescriptionPoco>), 200)]
        public ActionResult<List<CompanyJobDescriptionPoco>> GetAllCompanyJobsDescription()
        {
            var entities = companyJobDescriptionLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("jobDescription")]
        public ActionResult PutCompanyJobsDescription(CompanyJobDescriptionPoco[] entities)
        {
            try
            {
                companyJobDescriptionLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("jobDescription")]
        public ActionResult DeleteCompanyJobsDescription(CompanyJobDescriptionPoco[] entities)

        {
            try
            {
                companyJobDescriptionLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();

            }

        }
    }
}
