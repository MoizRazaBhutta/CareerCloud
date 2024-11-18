using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository:IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public SecurityLoginsLogRepository()
        {
            connectionString = ConnectionHelper.connStr;
           
        }
        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            List<SecurityLoginsLogPoco> entities = new List<SecurityLoginsLogPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Security_Logins_Log";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                SecurityLoginsLogPoco entity = new SecurityLoginsLogPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Login = (Guid)rdr["Login"],
                                    SourceIP = "" + rdr["Source_IP"],
                                    LogonDate = (DateTime)rdr["Logon_Date"],
                                    IsSuccesful = (bool)rdr["Is_Succesful"]
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
        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)

        {

            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(SecurityLoginsLogPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginsLogPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Security_Logins_Log ([Id], [Login], [Source_IP], [Logon_Date], [Is_Succesful])" +
                        " values (@Id, @Login, @SourceIP, @LogonDate, @IsSuccesful)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@SourceIP", entity.SourceIP);
                        cmd.Parameters.AddWithValue("@LogonDate", entity.LogonDate);
                        cmd.Parameters.AddWithValue("@IsSuccesful", entity.IsSuccesful);
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

        public void Update(SecurityLoginsLogPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginsLogPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Security_Logins_Log set " +
                        "Login = @Login, Source_IP = @SourceIP, Logon_Date = @LogonDate, Is_Succesful = @IsSuccesful " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@SourceIP", entity.SourceIP);
                        cmd.Parameters.AddWithValue("@LogonDate", entity.LogonDate);
                        cmd.Parameters.AddWithValue("@IsSuccesful", entity.IsSuccesful);
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

        public void Remove(SecurityLoginsLogPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginsLogPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Security_Logins_Log where Id = @Id";

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
