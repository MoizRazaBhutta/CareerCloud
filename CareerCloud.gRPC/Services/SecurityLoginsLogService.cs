using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService:SecurityLoginLogs.SecurityLoginLogsBase
    {
        private readonly SecurityLoginsLogLogic securityLoginsLoglogic;
        public SecurityLoginsLogService(CareerCloudContext context)
        {
            securityLoginsLoglogic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(context));
        }
        public override Task<Empty> PostSecurityLoginLogs(MultipleSecurityLoginLogs request, ServerCallContext context)
        {
            var securityLoginsLogPocos = new List<SecurityLoginsLogPoco>();
            foreach(var req in request.Securityloginslogs)
            {
                SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = Guid.Parse(req.Login),
                    SourceIP = req.SourceIP,
                    LogonDate = req.LogonDate.ToDateTime(),
                    IsSuccesful = req.IsSuccessful


                };

                securityLoginsLogPocos.Add(poco);
            }
            SecurityLoginsLogPoco[] securityLoginsLogPocosArr = securityLoginsLogPocos.ToArray();
            securityLoginsLoglogic.Add(securityLoginsLogPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutSecurityLoginLogs(MultipleSecurityLoginLogs request, ServerCallContext context)
        {
            var securityLoginsLogPocos = new List<SecurityLoginsLogPoco>();
            foreach (var req in request.Securityloginslogs)
            {
                SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = Guid.Parse(req.Login),
                    SourceIP = req.SourceIP,
                    LogonDate = req.LogonDate.ToDateTime(),
                    IsSuccesful = req.IsSuccessful
                    

                };

                securityLoginsLogPocos.Add(poco);
            }
            SecurityLoginsLogPoco[] securityLoginsLogPocosArr = securityLoginsLogPocos.ToArray();
            securityLoginsLoglogic.Update(securityLoginsLogPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSecurityLoginLogs(MultipleSecurityLoginLogs request, ServerCallContext context)
        {
            var securityLoginsLogPocos = new List<SecurityLoginsLogPoco>();
            foreach (var req in request.Securityloginslogs)
            {
                SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = Guid.Parse(req.Login),
                    SourceIP = req.SourceIP,
                    LogonDate = req.LogonDate.ToDateTime(),
                    IsSuccesful = req.IsSuccessful


                };

                securityLoginsLogPocos.Add(poco);
            }
            SecurityLoginsLogPoco[] securityLoginsLogPocosArr = securityLoginsLogPocos.ToArray();
            securityLoginsLoglogic.Delete(securityLoginsLogPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<SecurityLoginLogsType> GetSecurityLoginLogs(SecurityLoginsLogId request, ServerCallContext context)
        {
            SecurityLoginsLogPoco securityLoginsLogPoco = securityLoginsLoglogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new SecurityLoginLogsType
            {
                Id = request.Id,
                Login = securityLoginsLogPoco.Login.ToString(),
                SourceIP = securityLoginsLogPoco.SourceIP,
                LogonDate = Timestamp.FromDateTime(DateTime.SpecifyKind(securityLoginsLogPoco.LogonDate, DateTimeKind.Utc)),
                IsSuccessful = securityLoginsLogPoco.IsSuccesful
                
                
              
            });
        }

        public override Task<MultipleSecurityLoginLogs> GetAllSecurityLoginLogs(Empty request, ServerCallContext context)
        {
            List<SecurityLoginsLogPoco> securityLoginsLogPocos = securityLoginsLoglogic.GetAll();
            List<SecurityLoginLogsType> securityLoginsLogTypes = new List<SecurityLoginLogsType>();
            foreach (SecurityLoginsLogPoco poco in securityLoginsLogPocos)
            {
                SecurityLoginLogsType securityLoginsLogType = new SecurityLoginLogsType
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    SourceIP = poco.SourceIP,
                    LogonDate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.LogonDate, DateTimeKind.Utc)),
                    IsSuccessful = poco.IsSuccesful



                };
                securityLoginsLogTypes.Add(securityLoginsLogType);
            }

            return Task.FromResult(new MultipleSecurityLoginLogs
            {
                Securityloginslogs = { securityLoginsLogTypes }
            });
        }
    }
}
