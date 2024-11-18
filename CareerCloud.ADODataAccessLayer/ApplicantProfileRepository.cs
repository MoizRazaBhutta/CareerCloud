using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository:IDataRepository<ApplicantProfilePoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantProfileRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            List<ApplicantProfilePoco> entities = new List<ApplicantProfilePoco>();

            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Profiles";

                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantProfilePoco entity = new ApplicantProfilePoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Login = (Guid)rdr["Login"],
                                    CurrentSalary = rdr["Current_Salary"] != DBNull.Value ? (decimal)rdr["Current_Salary"] : null,
                                    CurrentRate = rdr["Current_Rate"] != DBNull.Value ? (decimal)rdr["Current_Rate"] : null,
                                    Currency = rdr["Currency"] != DBNull.Value ? "" + rdr["Currency"] : null,
                                    Country = "" + rdr["Country_Code"],
                                    Province = rdr["State_Province_Code"] != DBNull.Value ? "" + rdr["State_Province_Code"] : null,
                                    Street = rdr["Street_Address"] != DBNull.Value ? "" + rdr["Street_Address"] : null,
                                    City = rdr["City_Town"] != DBNull.Value ? "" + rdr["City_Town"] : null,
                                    PostalCode = rdr["Zip_Postal_Code"] != DBNull.Value ? "" + rdr["Zip_Postal_Code"] : null,
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
        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantProfilePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Profiles([Id], [Login], [Current_Salary], [Current_Rate], [Currency]," +
                        "[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])"
                           + " values (@Id, @Login, @CurrentSalary, @CurrentRate, @Currency, @Country, @Province, " +
                           "@Street, @City, @PostalCode)";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@CurrentSalary", (object?)entity.CurrentSalary ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CurrentRate", (object?)entity.CurrentRate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Currency", (object?)entity.Currency ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", entity.Country);
                        cmd.Parameters.AddWithValue("@Province", (object?)entity.Province ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Street", (object?)entity.Street ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", (object?)entity.City ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PostalCode", (object?)entity.PostalCode ?? DBNull.Value);
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

        public void Update(ApplicantProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantProfilePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Profiles set " +
                            " Login = @Login, Current_Salary = @CurrentSalary, Current_Rate = @CurrentRate, Currency = @Currency, Country_Code=@Country, State_Province_Code = @Province," +
                            " Street_Address=@Street, City_Town=@City,Zip_Postal_Code=@PostalCode" +
                            " where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@CurrentSalary", (object?)entity.CurrentSalary ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CurrentRate", (object?)entity.CurrentRate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Currency", (object?)entity.Currency ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Country", entity.Country);
                        cmd.Parameters.AddWithValue("@Province", (object?)entity.Province ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Street", (object?)entity.Street ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", (object?)entity.City ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PostalCode", (object?)entity.PostalCode ?? DBNull.Value);
                       
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

        public void Remove(ApplicantProfilePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantProfilePoco entity in entities)
                    {

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Profiles where Id = @Id";

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
