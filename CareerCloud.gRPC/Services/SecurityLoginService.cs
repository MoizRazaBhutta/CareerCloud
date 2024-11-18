using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService:SecurityLogin.SecurityLoginBase
    {
        private readonly SecurityLoginLogic securityLoginlogic;
        public SecurityLoginService(CareerCloudContext context)
        {
            securityLoginlogic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(context));
        }
        public override Task<Empty> PostSecurityLogin(MultipleSecurityLogins request, ServerCallContext context)
        {
            var securityLoginPocos = new List<SecurityLoginPoco>();
            foreach(var req in request.Securitylogins)
            {
                SecurityLoginPoco poco = new SecurityLoginPoco()
                {
                    Id = Guid.Parse(req.Id),                          
                    Login = req.Login,
                    Password = req.Password,
                    Created = req.Created.ToDateTime(),   
                    PasswordUpdate = req.PasswordUpdate.ToDateTime(),
                    AgreementAccepted = req.AgreementAccepted.ToDateTime(),
                    IsLocked = req.IsLocked,
                    IsInactive = req.IsInactive,                             
                    EmailAddress = req.EmailAddress, 
                    PhoneNumber = req.PhoneNumber,
                    FullName = req.FullName,
                    ForceChangePassword = req.ForceChangePassword,
                    PrefferredLanguage = req.PrefferredLanguage,

                };

                securityLoginPocos.Add(poco);
            }
            SecurityLoginPoco[] securityLoginPocosArr = securityLoginPocos.ToArray();
            securityLoginlogic.Add(securityLoginPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutSecurityLogin(MultipleSecurityLogins request, ServerCallContext context)
        {
            var securityLoginPocos = new List<SecurityLoginPoco>();
            foreach (var req in request.Securitylogins)
            {
                SecurityLoginPoco poco = new SecurityLoginPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = req.Login,
                    Password = req.Password,
                    Created = req.Created.ToDateTime(),
                    PasswordUpdate = req.PasswordUpdate.ToDateTime(),
                    AgreementAccepted = req.AgreementAccepted.ToDateTime(),
                    IsLocked = req.IsLocked,
                    IsInactive = req.IsInactive,
                    EmailAddress = req.EmailAddress,
                    PhoneNumber = req.PhoneNumber,
                    FullName = req.FullName,
                    ForceChangePassword = req.ForceChangePassword,
                    PrefferredLanguage = req.PrefferredLanguage,

                };

                securityLoginPocos.Add(poco);
            }
            SecurityLoginPoco[] securityLoginPocosArr = securityLoginPocos.ToArray();
            securityLoginlogic.Update(securityLoginPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSecurityLogin(MultipleSecurityLogins request, ServerCallContext context)
        {
            var securityLoginPocos = new List<SecurityLoginPoco>();
            foreach (var req in request.Securitylogins)
            {
                SecurityLoginPoco poco = new SecurityLoginPoco()
                {
                    Id = Guid.Parse(req.Id),
                    Login = req.Login,
                    Password = req.Password,
                    Created = req.Created.ToDateTime(),
                    PasswordUpdate = req.PasswordUpdate.ToDateTime(),
                    AgreementAccepted = req.AgreementAccepted.ToDateTime(),
                    IsLocked = req.IsLocked,
                    IsInactive = req.IsInactive,
                    EmailAddress = req.EmailAddress,
                    PhoneNumber = req.PhoneNumber,
                    FullName = req.FullName,
                    ForceChangePassword = req.ForceChangePassword,
                    PrefferredLanguage = req.PrefferredLanguage,

                };

                securityLoginPocos.Add(poco);
            }
            SecurityLoginPoco[] securityLoginPocosArr = securityLoginPocos.ToArray();
            securityLoginlogic.Delete(securityLoginPocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<SecurityLoginType> GetSecurityLogin(SecurityLoginId request, ServerCallContext context)
        {
            SecurityLoginPoco securityLoginPoco = securityLoginlogic.Get(Guid.Parse(request.Id));

            return Task.FromResult(new SecurityLoginType
            {
                Id = request.Id,
                Login = securityLoginPoco.Login,
                Password = securityLoginPoco.Password,
                Created = Timestamp.FromDateTime(DateTime.SpecifyKind(securityLoginPoco.Created, DateTimeKind.Utc)),
                PasswordUpdate = Timestamp.FromDateTime(DateTime.SpecifyKind(securityLoginPoco.PasswordUpdate.Value, DateTimeKind.Utc)),
                AgreementAccepted = Timestamp.FromDateTime(DateTime.SpecifyKind(securityLoginPoco.AgreementAccepted.Value, DateTimeKind.Utc)),
                IsLocked = securityLoginPoco.IsLocked,
                IsInactive = securityLoginPoco.IsInactive,
                EmailAddress = securityLoginPoco.EmailAddress,
                PhoneNumber = securityLoginPoco.PhoneNumber,
                ForceChangePassword = securityLoginPoco.ForceChangePassword,
                PrefferredLanguage = securityLoginPoco.PrefferredLanguage,
                
              
            });
        }

        public override Task<MultipleSecurityLogins> GetAllSecurityLogin(Empty request, ServerCallContext context)
        {
            List<SecurityLoginPoco> securityLoginPocos = securityLoginlogic.GetAll();
            List<SecurityLoginType> securityLoginTypes = new List<SecurityLoginType>();
            foreach (SecurityLoginPoco poco in securityLoginPocos)
            {
                SecurityLoginType securityLoginType = new SecurityLoginType
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login,
                    Password = poco.Password,
                    Created = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.Created, DateTimeKind.Utc)),
                    PasswordUpdate = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.PasswordUpdate.Value, DateTimeKind.Utc)),
                    AgreementAccepted = Timestamp.FromDateTime(DateTime.SpecifyKind(poco.AgreementAccepted.Value, DateTimeKind.Utc)),
                    IsLocked = poco.IsLocked,
                    IsInactive = poco.IsInactive,
                    EmailAddress = poco.EmailAddress,
                    PhoneNumber = poco.PhoneNumber,
                    ForceChangePassword = poco.ForceChangePassword,
                    PrefferredLanguage = poco.PrefferredLanguage,


                };
                securityLoginTypes.Add(securityLoginType);
            }

            return Task.FromResult(new MultipleSecurityLogins
            {
                Securitylogins = { securityLoginTypes }
            });
        }
    }
}
