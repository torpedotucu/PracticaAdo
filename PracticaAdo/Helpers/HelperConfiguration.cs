using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaAdo.Helpers
{
    public class HelperConfiguration
    {

        public static string GetConnectionString()
        {
            
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true);
            //EL OBJ PARA RECUPERAR LAS KEYS
            IConfigurationRoot configuration = builder.Build();
            //EXISTEN CLAVES QUE YA VIENEN CREADAS POR DEFECTO: ConnectionStrings
            string connectionStrings = configuration.GetConnectionString("SqlPractica");
            return connectionStrings;
        }
    }
}
