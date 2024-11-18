using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository:IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyJobDescriptionRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            List<CompanyJobDescriptionPoco> entities = new List<CompanyJobDescriptionPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Jobs_Descriptions";

                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyJobDescriptionPoco entity = new CompanyJobDescriptionPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Job = (Guid)rdr["Job"],
                                    JobName = "" + rdr["Job_Name"],
                                    JobDescriptions = "" + rdr["Job_Descriptions"],
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
        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyJobDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobDescriptionPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Jobs_Descriptions ([Id], [Job], [Job_Name], [Job_Descriptions])" +
                        " values (@Id, @Job, @JobName, @JobDescriptions)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@JobName", entity.JobName);
                        cmd.Parameters.AddWithValue("@JobDescriptions", entity.JobDescriptions);
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

        public void Update(CompanyJobDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobDescriptionPoco entity in entities)
                    {

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Jobs_Descriptions set " +
                        "Job = @Job, Job_Name = @JobName, Job_Descriptions = @JobDescriptions " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@JobName", entity.JobName);
                        cmd.Parameters.AddWithValue("@JobDescriptions", entity.JobDescriptions);
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

        public void Remove(CompanyJobDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobDescriptionPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Jobs_Descriptions where Id = @Id";

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
