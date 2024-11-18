using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (!string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    string[] validExtensions = { ".ca", ".com", ".biz" };
                    if (!validExtensions.Any(ext => poco.CompanyWebsite.EndsWith(ext)))
                    {
                        exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                    }
                }

                if (string.IsNullOrEmpty(poco.ContactPhone) || !System.Text.RegularExpressions.Regex.IsMatch(poco.ContactPhone, @"^\d{3}-\d{3}-\d{4}$"))
                {
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phone number (e.g. 416-555-1234)"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }

}
