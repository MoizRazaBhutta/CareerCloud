using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService:CompanyJobEducation.CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic companyJobEducationlogic;
        public CompanyJobEducationService(CareerCloudContext context)
        {
            companyJobEducationlogic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>(context));
        }
        public override Task<Empty> PostCompanyJobEducation(MultipleCompanyJobEducations request, ServerCallContext context)
        {
            var companyJobEducationPocos = new List<CompanyJobEducationPoco>();
            foreach(var req in request.Companyjobeducations)
            {
                CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
                {
                    Id = Guid.Parse(req.Id),                          
                    Job = Guid.Parse(req.Job),                            
                    Major = req.Major,                             
                    Importance = (short) req.Importance,                           
                };

                companyJobEducationPocos.Add(poco);
            }
            CompanyJobEducationPoco[] companyJobEducationPocosArr = companyJobEducationPocos.ToArray();
            companyJobEducationlogic.Add(companyJobEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutCompanyJobEducation(MultipleCompanyJobEducations request, ServerCallContext context)
        {
            var companyJobEducationPocos = new List<CompanyJobEducationPoco>();
            foreach (var req in request.Companyjobeducations)
            {
                CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Job = Guid.Parse(req.Job),
                    Major = req.Major,
                    Importance = (short)req.Importance,
                };

                companyJobEducationPocos.Add(poco);
            }
            CompanyJobEducationPoco[] companyJobEducationPocosArr = companyJobEducationPocos.ToArray();
            companyJobEducationlogic.Update(companyJobEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteCompanyJobEducation(MultipleCompanyJobEducations request, ServerCallContext context)
        {
            var companyJobEducationPocos = new List<CompanyJobEducationPoco>();
            foreach (var req in request.Companyjobeducations)
            {
                CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Job = Guid.Parse(req.Job),
                    Major = req.Major,
                    Importance = (short)req.Importance,
                };

                companyJobEducationPocos.Add(poco);
            }
            CompanyJobEducationPoco[] companyJobEducationPocosArr = companyJobEducationPocos.ToArray();
            companyJobEducationlogic.Delete(companyJobEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<CompanyJobEducationType> GetCompanyJobEducation(CompanyJobEducationId request, ServerCallContext context)
        {
            CompanyJobEducationPoco companyJobEducationPoco = companyJobEducationlogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new CompanyJobEducationType
            {
                Id = request.Id,
                Job = companyJobEducationPoco.Job.ToString(),
                Major = companyJobEducationPoco.Major.ToString(),
                Importance = companyJobEducationPoco.Importance
              
            });
        }

        public override Task<MultipleCompanyJobEducations> GetAllCompanyJobEducation(Empty request, ServerCallContext context)
        {
            List<CompanyJobEducationPoco> companyJobEducationPocos = companyJobEducationlogic.GetAll();
            List<CompanyJobEducationType> companyJobEducationTypes = new List<CompanyJobEducationType>();
            foreach (CompanyJobEducationPoco poco in companyJobEducationPocos)
            {
                CompanyJobEducationType companyJobEducationType = new CompanyJobEducationType
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Major = poco.Major.ToString(),
                    Importance = poco.Importance

                };
                companyJobEducationTypes.Add(companyJobEducationType);
            }

            return Task.FromResult(new MultipleCompanyJobEducations
            {
                Companyjobeducations = { companyJobEducationTypes }
            });
        }
    }
}
