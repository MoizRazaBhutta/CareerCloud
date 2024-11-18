using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository:IDataRepository<CompanyJobPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyJobRepository()
        {
            connectionString = ConnectionHelper.connStr;
          
        }
        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            List<CompanyJobPoco> entities = new List<CompanyJobPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Jobs";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyJobPoco entity = new CompanyJobPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Company = (Guid)rdr["Company"],
                                    ProfileCreated = (DateTime)rdr["Profile_Created"],
                                    IsInactive = (bool)rdr["Is_Inactive"],
                                    IsCompanyHidden = (bool)rdr["Is_Company_Hidden"],
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
        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyJobPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Jobs ([Id], [Company], [Profile_Created], [Is_Inactive], [Is_Company_Hidden])" +
                        " values (@Id, @Company, @ProfileCreated, @IsInactive, @IsCompanyHidden)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@ProfileCreated", entity.ProfileCreated);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
                        cmd.Parameters.AddWithValue("@IsCompanyHidden", entity.IsCompanyHidden);
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

        public void Update(CompanyJobPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Jobs set " +
                        "Company = @Company, Profile_Created = @ProfileCreated, Is_Inactive = @IsInactive, Is_Company_Hidden = @IsCompanyHidden " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@ProfileCreated", entity.ProfileCreated);
                        cmd.Parameters.AddWithValue("@IsInactive", entity.IsInactive);
                        cmd.Parameters.AddWithValue("@IsCompanyHidden", entity.IsCompanyHidden);
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

        public void Remove(CompanyJobPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyJobPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Jobs where Id = @Id";

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
