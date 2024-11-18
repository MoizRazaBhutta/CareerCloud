using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository:IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public SystemLanguageCodeRepository()
        {
            connectionString = ConnectionHelper.connStr;
            /*

                                     using (sqlConnection = new SqlConnection(connectionString))
                                     {
                                         sqlConnection.Open();
                                         using (SqlCommand cmd = sqlConnection.CreateCommand())
                                         { 
                                             foreach (SystemLanguageCodePoco entity in entities)
                                             {
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
                                      */
        }
        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            List<SystemLanguageCodePoco> entities = new List<SystemLanguageCodePoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.System_Language_Codes";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                SystemLanguageCodePoco entity = new SystemLanguageCodePoco()
                                {
                                    LanguageID = "" + rdr["LanguageID"],
                                    Name = "" + rdr["Name"],
                                    NativeName = "" + rdr["Native_Name"]
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
        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)

        {

            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(SystemLanguageCodePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SystemLanguageCodePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.System_Language_Codes ([LanguageID], [Name], [Native_Name])" +
                        " values (@LanguageID, @Name, @NativeName)";

                        cmd.Parameters.AddWithValue("@LanguageID", entity.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", entity.Name);
                        cmd.Parameters.AddWithValue("@NativeName", entity.NativeName);
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

        public void Update(SystemLanguageCodePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SystemLanguageCodePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.System_Language_Codes set " +
                        "Name = @Name, Native_Name = @NativeName " +
                        "where LanguageID = @LanguageID";

                        cmd.Parameters.AddWithValue("@LanguageID", entity.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", entity.Name);
                        cmd.Parameters.AddWithValue("@NativeName", entity.NativeName);

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

        public void Remove(SystemLanguageCodePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SystemLanguageCodePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.System_Language_Codes where LanguageID = @LanguageID";

                        cmd.Parameters.AddWithValue("@LanguageID", entity.LanguageID);
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
