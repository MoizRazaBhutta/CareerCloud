using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        IDataRepository<SystemCountryCodePoco> _repo;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) 
        {
            _repo = repository;
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repo.Add(pocos);

        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repo.Update(pocos);

        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repo.Remove(pocos);

        }
        public SystemCountryCodePoco Get(string Id)
        {
            return _repo.GetSingle(poco => poco.Code == Id);
        }
        public IList<SystemCountryCodePoco> GetAll()
        {
            return _repo.GetAll();
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, "Code cannot be empty"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, "Name cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }

}
