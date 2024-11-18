using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService:ApplicantProfile.ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic applicantProfilelogic;
        public ApplicantProfileService(CareerCloudContext context)
        {
            applicantProfilelogic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(context));
        }
        public override Task<Empty> PostApplicantProfile(MultipleApplicantProfiles request, ServerCallContext context)
        {
            var applicantProfilePocos = new List<ApplicantProfilePoco>();
            foreach(var req in request.Applicantprofiles)
            {
                ApplicantProfilePoco poco = new ApplicantProfilePoco()
                {
                    Id = Guid.Parse(req.Id),                          
                    Login = Guid.Parse(req.Login),                    
                    CurrentSalary = (decimal?)req.CurrentSalary, 
                    CurrentRate =(decimal?)req.CurrentRate,         
                    Currency = req.Currency,                           
                    Country = req.Country,                             
                    Province = req.Province,                           
                    Street = req.Street,                               
                    City = req.City,                                   
                    PostalCode = req.PostalCode
                };

                applicantProfilePocos.Add(poco);
            }
            ApplicantProfilePoco[] applicantProfilePocosArr = applicantProfilePocos.ToArray();
            applicantProfilelogic.Add(applicantProfilePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutApplicantProfile(MultipleApplicantProfiles request, ServerCallContext context)
        {
            var applicantProfilePocos = new List<ApplicantProfilePoco>();
            foreach (var req in request.Applicantprofiles)
            {
                ApplicantProfilePoco poco = new ApplicantProfilePoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = Guid.Parse(req.Login),
                    CurrentSalary = (decimal?)req.CurrentSalary,
                    CurrentRate = (decimal?)req.CurrentRate,
                    Currency = req.Currency,
                    Country = req.Country,
                    Province = req.Province,
                    Street = req.Street,
                    City = req.City,
                    PostalCode = req.PostalCode
                };

                applicantProfilePocos.Add(poco);
            }
            ApplicantProfilePoco[] applicantProfilePocosArr = applicantProfilePocos.ToArray();
            applicantProfilelogic.Update(applicantProfilePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantProfile(MultipleApplicantProfiles request, ServerCallContext context)
        {
            var applicantProfilePocos = new List<ApplicantProfilePoco>();
            foreach (var req in request.Applicantprofiles)
            {
                ApplicantProfilePoco poco = new ApplicantProfilePoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = Guid.Parse(req.Login),
                    CurrentSalary = (decimal?)req.CurrentSalary,
                    CurrentRate = (decimal?)req.CurrentRate,
                    Currency = req.Currency,
                    Country = req.Country,
                    Province = req.Province,
                    Street = req.Street,
                    City = req.City,
                    PostalCode = req.PostalCode
                };

                applicantProfilePocos.Add(poco);
            }
            ApplicantProfilePoco[] applicantProfilePocosArr = applicantProfilePocos.ToArray();
            applicantProfilelogic.Delete(applicantProfilePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<ApplicantProfileType> GetApplicantProfile(ApplicantProfileId request, ServerCallContext context)
        {
            ApplicantProfilePoco applicantProfilePoco = applicantProfilelogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new ApplicantProfileType
            {
                Id = request.Id,
                Login = applicantProfilePoco.Login.ToString(),
                CurrentSalary = (double) applicantProfilePoco.CurrentSalary,
                CurrentRate = (double)applicantProfilePoco.CurrentRate,
                Currency = applicantProfilePoco.Currency,
                Country = applicantProfilePoco.Country,
                Province = applicantProfilePoco.Province,
                Street = applicantProfilePoco.Street,
                City = applicantProfilePoco.City,
                PostalCode = applicantProfilePoco.PostalCode
            });
        }

        public override Task<MultipleApplicantProfiles> GetAllApplicantProfile(Empty request, ServerCallContext context)
        {
            List<ApplicantProfilePoco> applicantProfilePocos = applicantProfilelogic.GetAll();
            List<ApplicantProfileType> applicantProfileTypes = new List<ApplicantProfileType>();
            foreach (ApplicantProfilePoco poco in applicantProfilePocos)
            {
                ApplicantProfileType applicantProfileType = new ApplicantProfileType
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    CurrentSalary = (double)poco.CurrentSalary,
                    CurrentRate = (double)poco.CurrentRate,
                    Currency = poco.Currency,
                    Country = poco.Country,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                };
                applicantProfileTypes.Add(applicantProfileType);
            }

            return Task.FromResult(new MultipleApplicantProfiles
            {
                Applicantprofiles = { applicantProfileTypes }
            });
        }
    }
}
