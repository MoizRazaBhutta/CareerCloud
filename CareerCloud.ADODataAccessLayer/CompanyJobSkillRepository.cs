using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobSkillRepository:IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyJobSkillRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            List<CompanyJobSkillPoco> entities = new List<CompanyJobSkillPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Job_Skills";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyJobSkillPoco entity = new CompanyJobSkillPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Job = (Guid)rdr["Job"],
                                    Skill = "" + rdr["Skill"],
                                    SkillLevel = "" + rdr["Skill_Level"],
                                    Importance = (int)rdr["Importance"],
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
        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyJobSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Job_Skills ([Id], [Job], [Skill], [Skill_Level], [Importance])" +
                        " values (@Id, @Job, @Skill, @SkillLevel, @Importance)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@Skill", entity.Skill);
                        cmd.Parameters.AddWithValue("@SkillLevel", entity.SkillLevel);
                        cmd.Parameters.AddWithValue("@Importance", entity.Importance);
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

        public void Update(CompanyJobSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Job_Skills set " +
                        "Job = @Job, Skill = @Skill, Skill_Level = @SkillLevel, Importance = @Importance " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Job", entity.Job);
                        cmd.Parameters.AddWithValue("@Skill", entity.Skill);
                        cmd.Parameters.AddWithValue("@SkillLevel", entity.SkillLevel);
                        cmd.Parameters.AddWithValue("@Importance", entity.Importance);
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

        public void Remove(CompanyJobSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Job_Skills where Id = @Id";

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
