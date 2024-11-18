using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository:IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantJobApplicationRepository()
        {
            connectionString = ConnectionHelper.connStr;
        }
        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            List<ApplicantJobApplicationPoco> entities = new List<ApplicantJobApplicationPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Job_Applications";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantJobApplicationPoco entity = new ApplicantJobApplicationPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Applicant = (Guid)rdr["Applicant"],
                                    Job = (Guid)rdr["Job"],
                                    ApplicationDate = (DateTime)rdr["Application_Date"],
                                };

                                entities.Add(entity);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in Get All Application: " + ex.Message);
                    }
                }

            }
           
            return entities;
        }
        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantJobApplicationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantJobApplicationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Job_Applications([Id], [Applicant], [Job], [Application_Date])" +
                            " values (@Id, @Applicant, @Job, @ApplicationDate)";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@ApplicationDate", entity.ApplicationDate);

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

        public void Update(ApplicantJobApplicationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantJobApplicationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Job_Applications set " +
                            " Applicant = @Applicant, Job = @Job, Application_Date = @ApplicationDate" +
                            " where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@ApplicationDate", entity.ApplicationDate);
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

        public void Remove(ApplicantJobApplicationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantJobApplicationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Job_Applications where Id = @Id";

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
