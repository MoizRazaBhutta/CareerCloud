using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic companyLocationLogic;
        public CompanyLocationController(CareerCloudContext context)
        {
            companyLocationLogic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>(context));
        }
        [HttpPost]
        [Route("location")]
        public ActionResult PostCompanyLocation(CompanyLocationPoco[] entities)
        {
            companyLocationLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("location/{Id}")]
        [ProducesResponseType(typeof(CompanyLocationPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid Id)
        {
            var entity = companyLocationLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("location")]
        [ProducesResponseType(typeof(List<CompanyLocationPoco>), 200)]
        public ActionResult<List<CompanyLocationPoco>> GetAllCompanyLocation()
        {
            var entities = companyLocationLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("location")]
        public ActionResult PutCompanyLocation(CompanyLocationPoco[] entities)
        {
            try
            {
                companyLocationLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("location")]
        public ActionResult DeleteCompanyLocation(CompanyLocationPoco[] entities)

        {
            try
            {
                companyLocationLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
