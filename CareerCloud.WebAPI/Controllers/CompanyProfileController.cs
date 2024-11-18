using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic companyProfileLogic;
        public CompanyProfileController(CareerCloudContext context)
        {
            companyProfileLogic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>(context));
        }
        [HttpPost]
        [Route("profile")]
        public ActionResult PostCompanyProfile(CompanyProfilePoco[] entities)
        {
            companyProfileLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("profile/{Id}")]
        [ProducesResponseType(typeof(CompanyProfilePoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid Id)
        {
            var entity = companyProfileLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();

            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(List<CompanyProfilePoco>), 200)]
        public ActionResult<List<CompanyProfilePoco>> GetAllCompanyProfile()
        {
            var entities = companyProfileLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("profile")]
        public ActionResult PutCompanyProfile(CompanyProfilePoco[] entities)
        {
            try
            {
                companyProfileLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteCompanyProfile(CompanyProfilePoco[] entities)

        {
            try
            {
                companyProfileLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
