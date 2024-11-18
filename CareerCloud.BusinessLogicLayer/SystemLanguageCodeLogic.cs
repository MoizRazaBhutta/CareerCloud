using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic 
    {
        IDataRepository<SystemLanguageCodePoco> _repo;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repo = repository;
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            
            Verify(pocos);
            _repo.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repo.Update(pocos);

        }
        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repo.Remove(pocos);
        }
        public SystemLanguageCodePoco Get(string Id)
        {
           return _repo.GetSingle(poco => poco.LanguageID == Id);
        }
        public IList<SystemLanguageCodePoco> GetAll()
        {
            return _repo.GetAll();
        }
        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, "LanguageID cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, "Name cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, "NativeName cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
