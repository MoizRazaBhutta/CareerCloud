using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository:IDataRepository<SecurityLoginPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public SecurityLoginRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            List<SecurityLoginPoco> entities = new List<SecurityLoginPoco>();
               using (sqlConnection = new SqlConnection(connectionString))
               {
                   sqlConnection.Open();
                   using (SqlCommand cmd = sqlConnection.CreateCommand())
                   { 
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "Select * from dbo.Security_Logins";
                        try
                        {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                            {

                                while (rdr.Read())
                                {
                                    SecurityLoginPoco entity = new SecurityLoginPoco()
                                    {
                                        Id = (Guid)rdr["Id"],
                                        Login = "" + rdr["Login"],
                                        Password = "" + rdr["Password"],
                                        Created = (DateTime)rdr["Created_Date"],
                                        PasswordUpdate = rdr["Password_Update_Date"] != DBNull.Value ? (DateTime?)rdr["Password_Update_Date"] : null,
                                        AgreementAccepted = rdr["Agreement_Accepted_Date"] != DBNull.Value ? (DateTime?)rdr["Agreement_Accepted_Date"] : null,
                                        IsLocked = (bool)rdr["Is_Locked"],
                                        IsInactive = (bool)rdr["Is_Inactive"],
                                        EmailAddress = "" + rdr["Email_Address"],
                                        PhoneNumber = rdr["Phone_Number"] != DBNull.Value ? "" + rdr["Phone_Number"] : null,
                                        FullName = rdr["Full_Name"] != DBNull.Value ? "" + rdr["Full_Name"] : null,
                                        ForceChangePassword = (bool)rdr["Force_Change_Password"],
                                        PrefferredLanguage = rdr["Prefferred_Language"] != DBNull.Value ? "" + rdr["Prefferred_Language"] : null,
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
        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)

        {

            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(SecurityLoginPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Security_Logins ([Id], [Login], [Password], [Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], [Is_Inactive], [Email_Address], [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language])" +
                        " values (@Id, @Login, @Password, @Created, @PasswordUpdate, @AgreementAccepted, @IsLocked, @IsInactive, @EmailAddress, @PhoneNumber, @FullName, @ForceChangePassword, @PrefferredLanguage)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@Password", entity.Password);
                        cmd.Parameters.AddWithValue("@Created", entity.Created);
                        cmd.Parameters.AddWithValue("@PasswordUpdate", (object?)entity.PasswordUpdate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AgreementAccepted", (object?)entity.AgreementAccepted ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsLocked", entity.IsLocked);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
                        cmd.Parameters.AddWithValue("@EmailAddress", entity.EmailAddress);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object?)entity.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FullName", (object?)entity.FullName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ForceChangePassword", entity.ForceChangePassword);
                        cmd.Parameters.AddWithValue("@PrefferredLanguage", (object?)entity.PrefferredLanguage ?? DBNull.Value);
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

        public void Update(SecurityLoginPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Security_Logins set " +
                        "Login = @Login, Password = @Password, Created_Date = @Created, Password_Update_Date = @PasswordUpdate, Agreement_Accepted_Date = @AgreementAccepted, " +
                        "Is_Locked = @IsLocked, Is_Inactive = @IsInactive, Email_Address = @EmailAddress, Phone_Number = @PhoneNumber, Full_Name = @FullName, Force_Change_Password = @ForceChangePassword, Prefferred_Language = @PrefferredLanguage " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Login", entity.Login);
                        cmd.Parameters.AddWithValue("@Password", entity.Password);
                        cmd.Parameters.AddWithValue("@Created", entity.Created);
                        cmd.Parameters.AddWithValue("@PasswordUpdate", (object?)entity.PasswordUpdate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AgreementAccepted", (object?)entity.AgreementAccepted ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsLocked", entity.IsLocked);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
                        cmd.Parameters.AddWithValue("@EmailAddress", entity.EmailAddress);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object?)entity.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FullName", (object?)entity.FullName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ForceChangePassword", entity.ForceChangePassword);
                        cmd.Parameters.AddWithValue("@PrefferredLanguage", (object?)entity.PrefferredLanguage ?? DBNull.Value);
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

        public void Remove(SecurityLoginPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityLoginPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Security_Logins where Id = @Id";

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
