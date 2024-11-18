using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository:IDataRepository<CompanyProfilePoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyProfileRepository()
        {
            connectionString = ConnectionHelper.connStr;
           
        }
        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            List<CompanyProfilePoco> entities = new List<CompanyProfilePoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Profiles";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyProfilePoco entity = new CompanyProfilePoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    RegistrationDate = (DateTime)rdr["Registration_Date"],
                                    CompanyWebsite = rdr["Company_Website"] != DBNull.Value ? "" + rdr["Company_Website"] : null,
                                    ContactPhone = "" + rdr["Contact_Phone"],
                                    ContactName = rdr["Contact_Name"] != DBNull.Value ? "" + rdr["Contact_Name"] : null,
                                    CompanyLogo = rdr["Company_Logo"] != DBNull.Value ? (byte[])rdr["Company_Logo"] : null,
                                };

                                entities.Add(entity);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in Select Application: " + ex.Message);
                    }
                    
                }
            }
            return entities;
        }
        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyProfilePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Profiles ([Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo])" +
                        " values (@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);
                        cmd.Parameters.AddWithValue("@CompanyWebsite", (object?)entity.CompanyWebsite ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ContactPhone", entity.ContactPhone);
                        cmd.Parameters.AddWithValue("@ContactName", (object?)entity.ContactName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompanyLogo", (object?)entity.CompanyLogo ?? DBNull.Value);

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in Insert Application: " + ex.Message);
                        }
                    }
                }
            }
            
        }

        public void Update(CompanyProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyProfilePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Profiles set " +
                        "Registration_Date = @RegistrationDate, Company_Website = @CompanyWebsite, Contact_Phone = @ContactPhone, Contact_Name = @ContactName, " +
                        "Company_Logo = @CompanyLogo " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);
                        cmd.Parameters.AddWithValue("@CompanyWebsite", (object?)entity.CompanyWebsite ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ContactPhone", entity.ContactPhone);
                        cmd.Parameters.AddWithValue("@ContactName", (object?)entity.ContactName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompanyLogo", (object?)entity.CompanyLogo ?? DBNull.Value);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in Update Application: " + ex.Message);
                        }
                    }
                }
            }

                
        }

        public void Remove(CompanyProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyProfilePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Profiles where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in Delete Application: " + ex.Message);
                        }
                    }
                }
            }

        }
    }
}
