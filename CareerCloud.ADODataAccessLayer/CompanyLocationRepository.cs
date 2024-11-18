using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository:IDataRepository<CompanyLocationPoco>
    {
        private readonly string? connectionString;
        private SqlConnection sqlConnection;

        public CompanyLocationRepository()
        {
            connectionString = ConnectionHelper.connStr;
            
        }
        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            List<CompanyLocationPoco> entities = new List<CompanyLocationPoco>();
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Select * from dbo.Company_Locations";
                    try
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {

                            while (rdr.Read())
                            {
                                CompanyLocationPoco entity = new CompanyLocationPoco()
                                {
                                    Id = (Guid)rdr["Id"],
                                    Company = (Guid)rdr["Company"],
                                    CountryCode = "" + rdr["Country_Code"],
                                    Province = rdr["State_Province_Code"] != DBNull.Value ? "" + rdr["State_Province_Code"] : null,
                                    Street = rdr["Street_Address"] != DBNull.Value ? "" + rdr["Street_Address"] : null,
                                    City = rdr["City_Town"] != DBNull.Value ? "" + rdr["City_Town"] : null,
                                    PostalCode = rdr["Zip_Postal_Code"] != DBNull.Value ? "" + rdr["Zip_Postal_Code"] : null,
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
        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)

        {

            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)

        {

            throw new NotImplementedException();

        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)

        {

            throw new NotImplementedException();

        }


        public void Add(CompanyLocationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyLocationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into dbo.Company_Locations ([Id], [Company], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code])" +
                        " values (@Id, @Company, @CountryCode, @Province, @Street, @City, @PostalCode)";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@CountryCode", entity.CountryCode);
                        cmd.Parameters.AddWithValue("@Province", (object?)entity.Province ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Street", (object?)entity.Street ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", (object?)entity.City ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PostalCode", (object?)entity.PostalCode ?? DBNull.Value);
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

        public void Update(CompanyLocationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyLocationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update dbo.Company_Locations set " +
                        "Company = @Company, Country_Code = @CountryCode, State_Province_Code = @Province, Street_Address = @Street, City_Town = @City, Zip_Postal_Code = @PostalCode " +
                        "where Id = @Id";

                        cmd.Parameters.AddWithValue("@Id", entity.Id);
                        cmd.Parameters.AddWithValue("@Company", entity.Company);
                        cmd.Parameters.AddWithValue("@CountryCode", entity.CountryCode);
                        cmd.Parameters.AddWithValue("@Province", (object?)entity.Province ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Street", (object?)entity.Street ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", (object?)entity.City ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PostalCode", (object?)entity.PostalCode ?? DBNull.Value);
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

        public void Remove(CompanyLocationPoco[] entities)
        {
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    foreach (CompanyLocationPoco entity in entities)
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "delete from dbo.Company_Locations where Id = @Id";

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
