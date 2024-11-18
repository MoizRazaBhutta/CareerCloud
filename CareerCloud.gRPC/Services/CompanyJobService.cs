using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService:CompanyJob.CompanyJobBase
    {
        private readonly CompanyJobLogic companyJoblogic;
        public CompanyJobService(CareerCloudContext context)
        {
            companyJoblogic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>(context));
        }
        public override Task<Empty> PostCompanyJob(MultipleCompanyJobs request, ServerCallContext context)
        {
            var companyJobPocos = new List<CompanyJobPoco>();
            foreach(var req in request.Companyjobs)
            {
                CompanyJobPoco poco = new CompanyJobPoco()
                {
                    Id = Guid.Parse(req.Id),                          
                    Company = Guid.Parse(req.Company),                            
                    ProfileCreated = req.ProfileCreated.ToDateTime(),                           
                    IsInactive = req.IsInactive,                             
                    IsCompanyHidden = req.IsCompanyHidden,                           
                };

                companyJobPocos.Add(poco);
            }
            CompanyJobPoco[] companyJobPocosArr = companyJobPocos.ToArray();
            companyJoblogic.Add(companyJobPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutCompanyJob(MultipleCompanyJobs request, ServerCallContext context)
        {
            var companyJobPocos = new List<CompanyJobPoco>();
            foreach (var req in request.Companyjobs)
            {
                CompanyJobPoco poco = new CompanyJobPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Company = Guid.Parse(req.Company),
                    ProfileCreated = req.ProfileCreated.ToDateTime(),
                    IsInactive = req.IsInactive,
                    IsCompanyHidden = req.IsCompanyHidden,
                };

                companyJobPocos.Add(poco);
            }
            CompanyJobPoco[] companyJobPocosArr = companyJobPocos.ToArray();
            companyJoblogic.Update(companyJobPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteCompanyJob(MultipleCompanyJobs request, ServerCallContext context)
        {
            var companyJobPocos = new List<CompanyJobPoco>();
            foreach (var req in request.Companyjobs)
            {
                CompanyJobPoco poco = new CompanyJobPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Company = Guid.Parse(req.Company),
                    ProfileCreated = req.ProfileCreated.ToDateTime(),
                    IsInactive = req.IsInactive,
                    IsCompanyHidden = req.IsCompanyHidden,
                };

                companyJobPocos.Add(poco);
            }
            CompanyJobPoco[] companyJobPocosArr = companyJobPocos.ToArray();
            companyJoblogic.Delete(companyJobPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<CompanyJobType> GetCompanyJob(CompanyJobId request, ServerCallContext context)
        {
            CompanyJobPoco companyJobPoco = companyJoblogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new CompanyJobType
            {
                Id = request.Id,
                Company = companyJobPoco.Company.ToString(),
                ProfileCreated = Timestamp.FromDateTime(DateTime.SpecifyKind(companyJobPoco.ProfileCreated, DateTimeKind.Utc)),
                IsInactive = companyJobPoco.IsInactive,
                IsCompanyHidden = companyJobPoco.IsCompanyHidden,
              
            });
        }

        public override Task<MultipleCompanyJobs> GetAllCompanyJob(Empty request, ServerCallContext context)
        {
            List<CompanyJobPoco> companyJobPocos = companyJoblogic.GetAll();
            List<CompanyJobType> companyJobTypes = new List<CompanyJobType>();
            foreach (CompanyJobPoco poco in companyJobPocos)
            {
                CompanyJobType companyJobType = new CompanyJobType
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    ProfileCreated = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.ProfileCreated, DateTimeKind.Utc)),
                    IsInactive = poco.IsInactive,
                    IsCompanyHidden = poco.IsCompanyHidden
                };
                companyJobTypes.Add(companyJobType);
            }

            return Task.FromResult(new MultipleCompanyJobs
            {
                Companyjobs = { companyJobTypes }
            });
        }
    }
}
