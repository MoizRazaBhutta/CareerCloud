using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic companyDescriptionLogic;
        public CompanyDescriptionController(CareerCloudContext context)
        {
            companyDescriptionLogic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>(context));
        }
        [HttpPost]
        [Route("description")]
        public ActionResult PostCompanyDescription(CompanyDescriptionPoco[] entities)
        {
            companyDescriptionLogic.Add(entities);
            return Ok();
        }
        [HttpGet]
        [Route("description/{Id}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyDescription(Guid Id)
        {
            var entity = companyDescriptionLogic.Get(Id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpGet]
        [Route("description")]
        [ProducesResponseType(typeof(List<CompanyDescriptionPoco>), 200)]
        public ActionResult<List<CompanyDescriptionPoco>> GetAllCompanyDescription()
        {
            var entities = companyDescriptionLogic.GetAll();
            return Ok(entities);
        }
        [HttpPut]
        [Route("description")]
        public ActionResult PutCompanyDescription(CompanyDescriptionPoco[] entities)
        {
            try
            {
                companyDescriptionLogic.Update(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
        }
        [HttpDelete]
        [Route("description")]
        public ActionResult DeleteCompanyDescription(CompanyDescriptionPoco[] entities)

        {
            try
            {
                companyDescriptionLogic.Delete(entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Could not find the corresponding record");
            }
            
        }
    }
}
