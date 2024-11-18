using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService:CompanyDescription.CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic companyDescriptionlogic;
        public CompanyDescriptionService(CareerCloudContext context)
        {
            companyDescriptionlogic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>(context));
        }
        public override Task<Empty> PostCompanyDescription(MultipleCompanyDescriptions request, ServerCallContext context)
        {
            var companyDescriptionPocos = new List<CompanyDescriptionPoco>();
            foreach(var req in request.Companydescriptions)
            {
                CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
                {
                    Id = Guid.Parse(req.Id),                          
                    Company = Guid.Parse(req.Company),                            
                    LanguageId = req.LanguageId,                           
                    CompanyName = req.CompanyName,                             
                    CompanyDescription = req.CompanyDescription,                           
                };

                companyDescriptionPocos.Add(poco);
            }
            CompanyDescriptionPoco[] companyDescriptionPocosArr = companyDescriptionPocos.ToArray();
            companyDescriptionlogic.Add(companyDescriptionPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutCompanyDescription(MultipleCompanyDescriptions request, ServerCallContext context)
        {
            var companyDescriptionPocos = new List<CompanyDescriptionPoco>();
            foreach (var req in request.Companydescriptions)
            {
                CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Company = Guid.Parse(req.Company),
                    LanguageId = req.LanguageId,
                    CompanyName = req.CompanyName,
                    CompanyDescription = req.CompanyDescription,
                };

                companyDescriptionPocos.Add(poco);
            }
            CompanyDescriptionPoco[] companyDescriptionPocosArr = companyDescriptionPocos.ToArray();
            companyDescriptionlogic.Update(companyDescriptionPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteCompanyDescription(MultipleCompanyDescriptions request, ServerCallContext context)
        {
            var companyDescriptionPocos = new List<CompanyDescriptionPoco>();
            foreach (var req in request.Companydescriptions)
            {
                CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Company = Guid.Parse(req.Company),
                    LanguageId = req.LanguageId,
                    CompanyName = req.CompanyName,
                    CompanyDescription = req.CompanyDescription,
                };

                companyDescriptionPocos.Add(poco);
            }
            CompanyDescriptionPoco[] companyDescriptionPocosArr = companyDescriptionPocos.ToArray();
            companyDescriptionlogic.Delete(companyDescriptionPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<CompanyDescriptionType> GetCompanyDescription(CompanyDescriptionId request, ServerCallContext context)
        {
            CompanyDescriptionPoco companyDescriptionPoco = companyDescriptionlogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new CompanyDescriptionType
            {
                Id = request.Id,
                Company = companyDescriptionPoco.Company.ToString(),
                LanguageId = companyDescriptionPoco.LanguageId,
                CompanyName = companyDescriptionPoco.CompanyName,
                CompanyDescription = companyDescriptionPoco.CompanyDescription,
              
            });
        }

        public override Task<MultipleCompanyDescriptions> GetAllCompanyDescription(Empty request, ServerCallContext context)
        {
            List<CompanyDescriptionPoco> companyDescriptionPocos = companyDescriptionlogic.GetAll();
            List<CompanyDescriptionType> companyDescriptionTypes = new List<CompanyDescriptionType>();
            foreach (CompanyDescriptionPoco poco in companyDescriptionPocos)
            {
                CompanyDescriptionType companyDescriptionType = new CompanyDescriptionType
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    LanguageId = poco.LanguageId,
                    CompanyName = poco.CompanyName,
                    CompanyDescription = poco.CompanyDescription,

                };
                companyDescriptionTypes.Add(companyDescriptionType);
            }

            return Task.FromResult(new MultipleCompanyDescriptions
            {
                Companydescriptions = { companyDescriptionTypes }
            });
        }
    }
}
