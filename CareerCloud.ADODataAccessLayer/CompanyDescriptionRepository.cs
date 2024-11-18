using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository:IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyDescriptionRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            List<CompanyDescriptionPoco> entities = new List<CompanyDescriptionPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Descriptions";
                    try
                    {

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyDescriptionPoco entity = new CompanyDescriptionPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Company = (Guid)rdr["Company"],
                                    LanguageId = "" + rdr["LanguageID"],
                                    CompanyName = "" + rdr["Company_Name"],
                                    CompanyDescription = "" + rdr["Company_Description"],
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
        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyDescriptionPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Descriptions ([Id], [Company], [LanguageID], [Company_Name], [Company_Description])" +
                        " values (@Id, @Company, @LanguageID, @CompanyName, @CompanyDescription)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@LanguageID", entity.LanguageId);
                        cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                        cmd.Parameters.AddWithValue("@CompanyDescription", entity.CompanyDescription);
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

        public void Update(CompanyDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyDescriptionPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Descriptions set " +
                         "Company = @Company, LanguageID = @LanguageID, Company_Name = @CompanyName, Company_Description = @CompanyDescription " +
                         "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@LanguageID", entity.LanguageId);
                        cmd.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                        cmd.Parameters.AddWithValue("@CompanyDescription", entity.CompanyDescription);
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

        public void Remove(CompanyDescriptionPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyDescriptionPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Descriptions where Id = @Id";

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
