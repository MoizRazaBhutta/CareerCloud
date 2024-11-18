using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic companyJobLogic;
        public CompanyJobController(CareerCloudContext context)
        {
            companyJobLogic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>(context));
        }
        [HttpPost]
        [Route("job")]
        public ActionResult PostCompanyJob(CompanyJobPoco[] entities)
        {
            companyJobLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("job/{Id}")]
        [ProducesResponseType(typeof(CompanyJobPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJob(Guid Id)
        {
            var entity = companyJobLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("job")]
        [ProducesResponseType(typeof(List<CompanyJobPoco>), 200)]
        public ActionResult<List<CompanyJobPoco>> GetAllCompanyJob()
        {
            var entities = companyJobLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("job")]
        public ActionResult PutCompanyJob(CompanyJobPoco[] entities)
        {
            try
            {
                companyJobLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteCompanyJob(CompanyJobPoco[] entities)

        {
            try
            {
                companyJobLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
