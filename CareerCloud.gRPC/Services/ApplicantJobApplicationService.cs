using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService:ApplicantJobApplication.ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic applicantJobApplicationlogic;
        public ApplicantJobApplicationService(CareerCloudContext context)
        {
            applicantJobApplicationlogic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(context));
        }
        public override Task<Empty> PostApplicantJobApplication(MultipleApplicantJobApplications request, ServerCallContext context)
        {
            var applicantJobApplicationPocos = new List<ApplicantJobApplicationPoco>();
            foreach(var req in request.Applicantjobapplications)
            {
                ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Job = Guid.Parse(req.Job),
                    ApplicationDate = req.ApplicationDate.ToDateTime(),                  
                };

                applicantJobApplicationPocos.Add(poco);
            }
            ApplicantJobApplicationPoco[] applicantJobApplicationPocosArr = applicantJobApplicationPocos.ToArray();
            applicantJobApplicationlogic.Add(applicantJobApplicationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutApplicantJobApplication(MultipleApplicantJobApplications request, ServerCallContext context)
        {
            var applicantJobApplicationPocos = new List<ApplicantJobApplicationPoco>();
            foreach (var req in request.Applicantjobapplications)
            {
                ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Job = Guid.Parse(req.Job),
                    ApplicationDate = req.ApplicationDate.ToDateTime(),
                };

                applicantJobApplicationPocos.Add(poco);
            }
            ApplicantJobApplicationPoco[] applicantJobApplicationPocosArr = applicantJobApplicationPocos.ToArray();
            applicantJobApplicationlogic.Update(applicantJobApplicationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantJobApplication(MultipleApplicantJobApplications request, ServerCallContext context)
        {
            var applicantJobApplicationPocos = new List<ApplicantJobApplicationPoco>();
            foreach (var req in request.Applicantjobapplications)
            {
                ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Job = Guid.Parse(req.Job),
                    ApplicationDate = req.ApplicationDate.ToDateTime(),
                };

                applicantJobApplicationPocos.Add(poco);
            }
            ApplicantJobApplicationPoco[] applicantJobApplicationPocosArr = applicantJobApplicationPocos.ToArray();
            applicantJobApplicationlogic.Delete(applicantJobApplicationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<ApplicantJobApplicationType> GetApplicantJobApplication(ApplicantJobEducationId request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco applicantJobApplicationPoco = applicantJobApplicationlogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new ApplicantJobApplicationType
            {
                Id = request.Id,
                Applicant = applicantJobApplicationPoco.Applicant.ToString(),
                Job = applicantJobApplicationPoco.Job.ToString(),
                ApplicationDate = Timestamp.FromDateTime(DateTime.SpecifyKind(applicantJobApplicationPoco.ApplicationDate, DateTimeKind.Utc)),
            });
        }

        public override Task<MultipleApplicantJobApplications> GetAllApplicantJobApplication(Empty request, ServerCallContext context)
        {
            List<ApplicantJobApplicationPoco> applicantJobApplicationPocos = applicantJobApplicationlogic.GetAll();
            List<ApplicantJobApplicationType> applicantJobApplicationTypes = new List<ApplicantJobApplicationType>();
            foreach (ApplicantJobApplicationPoco poco in applicantJobApplicationPocos)
            {
                ApplicantJobApplicationType applicantJobApplicationType = new ApplicantJobApplicationType
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Job = poco.Job.ToString(),
                    ApplicationDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.ApplicationDate, DateTimeKind.Utc)),
                };
                applicantJobApplicationTypes.Add(applicantJobApplicationType);
            }

            return Task.FromResult(new MultipleApplicantJobApplications
            {
                Applicantjobapplications = { applicantJobApplicationTypes }
            });
        }
    }
}
