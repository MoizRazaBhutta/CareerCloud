using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService:ApplicantEducation.ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic applicantEducationLogic;
        public ApplicantEducationService(CareerCloudContext context)
        {
            applicantEducationLogic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(context));
        }
        public override Task<Empty> PostApplicantEducation(MultipleApplicantEducations request, ServerCallContext context)
        {
            var applicantEducationPocos = new List<ApplicantEducationPoco>();
            foreach(var req in request.Applicanteducations)
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Major = req.Major,
                    CertificateDiploma = req.CertificateDiploma,
                    StartDate = req.StartDate?.ToDateTime(),
                    CompletionDate = req.CompletionDate?.ToDateTime(),
                    CompletionPercent = (byte)req.CompletionPercent,                    
                };

                applicantEducationPocos.Add(poco);
            }
            ApplicantEducationPoco[] applicantEducationPocosArr = applicantEducationPocos.ToArray();
            applicantEducationLogic.Add(applicantEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutApplicantEducation(MultipleApplicantEducations request, ServerCallContext context)
        {
            var applicantEducationPocos = new List<ApplicantEducationPoco>();
            foreach (var req in request.Applicanteducations)
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Major = req.Major,
                    CertificateDiploma = req.CertificateDiploma,
                    StartDate = req.StartDate?.ToDateTime(),
                    CompletionDate = req.CompletionDate?.ToDateTime(),
                    CompletionPercent = (byte)req.CompletionPercent,
                };

                applicantEducationPocos.Add(poco);
            }
            ApplicantEducationPoco[] applicantEducationPocosArr = applicantEducationPocos.ToArray();
            applicantEducationLogic.Update(applicantEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteApplicantEducation(MultipleApplicantEducations request, ServerCallContext context)
        {
            var applicantEducationPocos = new List<ApplicantEducationPoco>();
            foreach (var req in request.Applicanteducations)
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Applicant = Guid.Parse(req.Applicant),
                    Major = req.Major,
                    CertificateDiploma = req.CertificateDiploma,
                    StartDate = req.StartDate?.ToDateTime(),
                    CompletionDate = req.CompletionDate?.ToDateTime(),
                    CompletionPercent = (byte)req.CompletionPercent,
                };

                applicantEducationPocos.Add(poco);
            }
            ApplicantEducationPoco[] applicantEducationPocosArr = applicantEducationPocos.ToArray();
            applicantEducationLogic.Delete(applicantEducationPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<ApplicantEducationType> GetApplicantEducation(GetById request, ServerCallContext context)
        {
            ApplicantEducationPoco applicantEducationPoco = applicantEducationLogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new ApplicantEducationType
            {
                Id = request.Id,
                Applicant = applicantEducationPoco.Applicant.ToString(),
                Major = applicantEducationPoco.Major,
                CertificateDiploma = applicantEducationPoco?.CertificateDiploma,
                StartDate = Timestamp.FromDateTime(DateTime.SpecifyKind(applicantEducationPoco.StartDate.Value, DateTimeKind.Utc)),
                CompletionDate = Timestamp.FromDateTime(DateTime.SpecifyKind(applicantEducationPoco.CompletionDate.Value, DateTimeKind.Utc)),
                CompletionPercent = applicantEducationPoco.CompletionPercent.HasValue ?
                (uint)applicantEducationPoco.CompletionPercent.Value : 0
            });
        }

        public override Task<MultipleApplicantEducations> GetAllApplicantEducation(Empty request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> applicantEducationPocos = applicantEducationLogic.GetAll();
            List<ApplicantEducationType> applicantEducationTypes = new List<ApplicantEducationType>();
            foreach (ApplicantEducationPoco poco in applicantEducationPocos)
            {
                ApplicantEducationType applicantEducationType = new ApplicantEducationType
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Major = poco.Major,
                    CertificateDiploma = poco?.CertificateDiploma,
                    StartDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.StartDate.Value, DateTimeKind.Utc)),
                    CompletionDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.CompletionDate.Value, DateTimeKind.Utc)),
                    CompletionPercent = poco.CompletionPercent.HasValue ?
                    (uint)poco.CompletionPercent.Value : 0
                };
                applicantEducationTypes.Add(applicantEducationType);
            }

            return Task.FromResult(new MultipleApplicantEducations
            {
                Applicanteducations = { applicantEducationTypes }
            });
        }
    }
}
