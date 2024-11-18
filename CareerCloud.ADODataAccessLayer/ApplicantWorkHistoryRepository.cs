using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository:IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantWorkHistoryRepository()
        {
            connectionString = ConnectionHelper.connStr;
        }
        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            List<ApplicantWorkHistoryPoco> entities = new List<ApplicantWorkHistoryPoco>();

            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Work_History";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantWorkHistoryPoco entity = new ApplicantWorkHistoryPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Applicant = (Guid)rdr["Applicant"],
                                    CompanyName = "" + rdr["Company_Name"],
                                    CountryCode = "" + rdr["Country_Code"],
                                    Location = "" + rdr["Location"],
                                    JobTitle = "" + rdr["Job_Title"],
                                    JobDescription = "" + rdr["Job_Description"],
                                    StartMonth = (short)rdr["Start_Month"],
                                    StartYear = (int)rdr["Start_Year"],
                                    EndMonth = (short)rdr["End_Month"],
                                    EndYear = (int)rdr["End_Year"],
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
        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantWorkHistoryPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantWorkHistoryPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Work_History ([Id],[Applicant], [Company_Name], [Country_Code], [Location], [Job_Title], [Job_Description], [Start_Month]," +
                        "[Start_Year],[End_Month],[End_Year])"
                           + " values (@Id, @Applicant, @CompanyName, @CountryCode," +
                           "@Location, @JobTitle, @JobDescription, @StartMonth, @StartYear, " +
                           "@EndMonth, @EndYear) ";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                        cmd.Parameters.AddWithValue("@CountryCode", entity.CountryCode);
                        cmd.Parameters.AddWithValue("@Location", entity.Location);
                        cmd.Parameters.AddWithValue("@JobTitle", entity.JobTitle);
                        cmd.Parameters.AddWithValue("@JobDescription", entity.JobDescription);
                        cmd.Parameters.AddWithValue("@StartMonth", entity.StartMonth);
                        cmd.Parameters.AddWithValue("@StartYear", entity.StartYear);
                        cmd.Parameters.AddWithValue("@EndMonth", entity.EndMonth);
                        cmd.Parameters.AddWithValue("@EndYear", entity.EndYear);
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

        public void Update(ApplicantWorkHistoryPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantWorkHistoryPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Work_History set " +
                            " Applicant = @Applicant, Company_Name = @CompanyName, Country_Code = @CountryCode, Location = @Location," +
                            " Job_Title=@JobTitle, Job_Description=@JobDescription, Start_Month=@StartMonth, Start_Year = @StartYear," +
                            " End_Month=@EndMonth, End_Year=@EndYear" +
                            " where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                        cmd.Parameters.AddWithValue("@CountryCode", entity.CountryCode);
                        cmd.Parameters.AddWithValue("@Location", entity.Location);
                        cmd.Parameters.AddWithValue("@JobTitle", entity.JobTitle);
                        cmd.Parameters.AddWithValue("@JobDescription", entity.JobDescription);
                        cmd.Parameters.AddWithValue("@StartMonth", entity.StartMonth);
                        cmd.Parameters.AddWithValue("@StartYear", entity.StartYear);
                        cmd.Parameters.AddWithValue("@EndMonth", entity.EndMonth);
                        cmd.Parameters.AddWithValue("@EndYear", entity.EndYear);
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

        public void Remove(ApplicantWorkHistoryPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantWorkHistoryPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Work_History where Id = @Id";

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
