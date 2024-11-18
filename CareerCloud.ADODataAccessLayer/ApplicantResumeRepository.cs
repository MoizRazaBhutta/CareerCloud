using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository:IDataRepository<ApplicantResumePoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantResumeRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            List<ApplicantResumePoco> entities = new List<ApplicantResumePoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Resumes";
                    
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantResumePoco entity = new ApplicantResumePoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Applicant = (Guid)rdr["Applicant"],
                                    Resume = "" + rdr["Resume"],
                                    LastUpdated = rdr["Last_Updated"] != DBNull.Value ? (DateTime)rdr["Last_Updated"] : null,
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
        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantResumePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantResumePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Resumes([Id],[Applicant], [Resume], [Last_Updated])" +
                            " values (@Id, @Applicant, @Resume, @LastUpdated)";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Resume", entity.Resume);
                        cmd.Parameters.AddWithValue("@LastUpdated", (object?)entity.LastUpdated ?? DBNull.Value);
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

        public void Update(ApplicantResumePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantResumePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Resumes set " +
                            " Applicant = @Applicant, Resume = @Resume, Last_Updated = @LastUpdated" +
                            " where Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Resume", entity.Resume);
                        cmd.Parameters.AddWithValue("@LastUpdated", (object?)entity.LastUpdated ?? DBNull.Value);
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

        public void Remove(ApplicantResumePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantResumePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Resumes where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
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
    }
}
