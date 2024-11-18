using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public static class ConnectionHelper
    {
        public static string connStr = string.Empty;
        static ConnectionHelper()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

        }


    }
    
}
