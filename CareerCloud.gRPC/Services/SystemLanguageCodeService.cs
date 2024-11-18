using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService:SystemLanguageCode.SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic systemLanguageCodelogic;
        public SystemLanguageCodeService(CareerCloudContext context)
        {
            systemLanguageCodelogic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>(context));
        }
        public override Task<Empty> PostSystemLanguageCode(MultipleSystemLanguageCode request, ServerCallContext context)
        {
            var systemLanguageCodePocos = new List<SystemLanguageCodePoco>();
            foreach(var req in request.Systemlanguagecodes)
            {
                SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
                {
                    LanguageID = req.LanguageID,
                    Name = req.Name,
                    NativeName = req.NativeName                   
                };

                systemLanguageCodePocos.Add(poco);
            }
            SystemLanguageCodePoco[] systemLanguageCodePocosArr = systemLanguageCodePocos.ToArray();
            systemLanguageCodelogic.Add(systemLanguageCodePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> PutSystemLanguageCode(MultipleSystemLanguageCode request, ServerCallContext context)
        {
            var systemLanguageCodePocos = new List<SystemLanguageCodePoco>();
            foreach (var req in request.Systemlanguagecodes)
            {
                SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
                {
                    LanguageID = req.LanguageID,
                    Name = req.Name,
                    NativeName = req.NativeName
                };

                systemLanguageCodePocos.Add(poco);
            }
            SystemLanguageCodePoco[] systemLanguageCodePocosArr = systemLanguageCodePocos.ToArray();
            systemLanguageCodelogic.Update(systemLanguageCodePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> DeleteSystemLanguageCode(MultipleSystemLanguageCode request, ServerCallContext context)
        {
            var systemLanguageCodePocos = new List<SystemLanguageCodePoco>();
            foreach (var req in request.Systemlanguagecodes)
            {
                SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
                {
                    LanguageID = req.LanguageID,
                    Name = req.Name,
                    NativeName = req.NativeName
                };

                systemLanguageCodePocos.Add(poco);
            }
            SystemLanguageCodePoco[] systemLanguageCodePocosArr = systemLanguageCodePocos.ToArray();
            systemLanguageCodelogic.Delete(systemLanguageCodePocosArr);
            return Task.FromResult(new Empty());
        }

        public override Task<SystemLanguageCodeType> GetSystemLanguageCode(SystemLanguageCodeId request, ServerCallContext context)
        {
            SystemLanguageCodePoco systemLanguageCodePoco = systemLanguageCodelogic.Get(request.LanguageID);

            return Task.FromResult(new SystemLanguageCodeType
            {
                LanguageID = request.LanguageID,
                Name = systemLanguageCodePoco.Name,
                NativeName = systemLanguageCodePoco.NativeName
               
              
            });
        }

        public override Task<MultipleSystemLanguageCode> GetAllSystemLanguageCode(Empty request, ServerCallContext context)
        {
            IList<SystemLanguageCodePoco> systemLanguageCodePocos = systemLanguageCodelogic.GetAll();
            List<SystemLanguageCodeType> systemLanguageCodeTypes = new List<SystemLanguageCodeType>();
            foreach (SystemLanguageCodePoco poco in systemLanguageCodePocos)
            {
                SystemLanguageCodeType systemLanguageCodeType = new SystemLanguageCodeType
                {
                    LanguageID = poco.LanguageID,
                    Name = poco.Name,
                    NativeName = poco.NativeName
                };
                systemLanguageCodeTypes.Add(systemLanguageCodeType);
            }

            return Task.FromResult(new MultipleSystemLanguageCode
            {
                Systemlanguagecodes = { systemLanguageCodeTypes }
            });
        }
    }
}
