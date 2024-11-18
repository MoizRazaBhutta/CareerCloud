using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository:IDataRepository<ApplicantSkillPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public ApplicantSkillRepository()
        {
            connectionString = ConnectionHelper.connStr;
        }
        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            List<ApplicantSkillPoco> entities = new List<ApplicantSkillPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Applicant_Skills";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                ApplicantSkillPoco entity = new ApplicantSkillPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Applicant = (Guid)rdr["Applicant"],
                                    Skill = "" + rdr["Skill"],
                                    SkillLevel = "" + rdr["Skill_Level"],
                                    StartMonth = (byte)rdr["Start_Month"],
                                    StartYear = (int)rdr["Start_Year"],
                                    EndMonth = (byte)rdr["End_Month"],
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
        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)

        {

            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(ApplicantSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Applicant_Skills([Id], [Applicant], [Skill], [Skill_Level], [Start_Month]," +
                        "[Start_Year],[End_Month],[End_Year])"
                           + " values (@Id, @Applicant, @Skill, @SkillLevel, @StartMonth, @StartYear, " +
                           "@EndMonth, @EndYear)";
                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Skill", entity.Skill);
                        cmd.Parameters.AddWithValue("@SkillLevel", entity.SkillLevel);
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

        public void Update(ApplicantSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Applicant_Skills set " +
                            " Applicant = @Applicant, Skill = @Skill, Skill_Level = @SkillLevel, Start_Month=@StartMonth, Start_Year = @StartYear," +
                            " End_Month=@EndMonth, End_Year=@EndYear" +
                            " where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Applicant", entity.Applicant);
                        cmd.Parameters.AddWithValue("@Skill", entity.Skill);
                        cmd.Parameters.AddWithValue("@SkillLevel", entity.SkillLevel);
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

        public void Remove(ApplicantSkillPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (ApplicantSkillPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Applicant_Skills where Id = @Id";

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
