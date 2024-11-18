using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository:IDataRepository<SecurityRolePoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public SecurityRoleRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            List<SecurityRolePoco> entities = new List<SecurityRolePoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Security_Roles";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                SecurityRolePoco entity = new SecurityRolePoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Role = "" + rdr["Role"],
                                    IsInactive = (bool)rdr["Is_Inactive"]
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
        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)

        {

            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(SecurityRolePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityRolePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Security_Roles ([Id], [Role], [Is_Inactive])" +
                        " values (@Id, @Role, @IsInactive)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Role", entity.Role);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
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

        public void Update(SecurityRolePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityRolePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Security_Roles set " +
                        "Role = @Role, Is_Inactive = @IsInactive " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Role", entity.Role);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
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

        public void Remove(SecurityRolePoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (SecurityRolePoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Security_Roles where Id = @Id";

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
