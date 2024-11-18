using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository:IDataRepository<ApplicantEducationPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantEducationRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            List<ApplicantEducationPoco> entities = new List<ApplicantEducationPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Educations";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantEducationPoco entity = new ApplicantEducationPoco();
                                
                                entity.Id = (Guid)rdr["Id"];
                                entity.Applicant = (Guid)rdr["Applicant"];
                                entity.Major = "" + rdr["Major"];
                                entity.CertificateDiploma = rdr["Certificate_Diploma"] != DBNull.Value ? "" + rdr["Certificate_Diploma"] : null;
                                entity.StartDate = rdr["Start_Date"] != DBNull.Value ? (DateTime)rdr["Start_Date"] : null;
                                entity.CompletionDate = rdr["Completion_Date"] != DBNull.Value ? (DateTime)rdr["Completion_Date"] : null;
                                entity.CompletionPercent = rdr["Completion_Percent"] != DBNull.Value ? (byte)rdr["Completion_Percent"] : null;
                                
        
                                entities.Add(entity);

                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error in Select Statement" + ex.Message);
                    }
                    
                }
            }
            
            return entities;
        }
        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantEducationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantEducationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Educations([Id], [Applicant], [Major], [Certificate_Diploma]," +
                            " [Start_Date], [Completion_Date], [Completion_Percent])" +
                            " values (@Id, @Applicant, @Major, @CertificateDiploma, @StartDate," +
                            " @CompletionDate, @CompletionPercent)";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Major", entity.Major);
                        cmd.Parameters.AddWithValue("@CertificateDiploma", (object?)entity.CertificateDiploma ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@StartDate", (object?)entity.StartDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompletionDate", (object?)entity.CompletionDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompletionPercent", (object?)entity.CompletionPercent ?? DBNull.Value);
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

        public void Update(ApplicantEducationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantEducationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Educations set " +
                            " Applicant = @Applicant, Major = @Major, Certificate_Diploma = @CertificateDiploma, Start_Date=@StartDate," +
                            " Completion_Date= @CompletionDate, Completion_Percent= @CompletionPercent" +
                            " where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Major", entity.Major);
                        cmd.Parameters.AddWithValue("@CertificateDiploma", (object?)entity.CertificateDiploma ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@StartDate", (object?)entity.StartDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompletionDate", (object?)entity.CompletionDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompletionPercent", (object?)entity.CompletionPercent ?? DBNull.Value);
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

        public void Remove(ApplicantEducationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantEducationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Educations where Id = @Id";

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
