using Microsoft.Data.SqlClient;
using PracticaAdo.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticaAdo.Repositories
{
    public class RepositoryPractica
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryPractica()
        {
            string connectionString = HelperConfiguration.GetConnectionString();
            this.cn=new SqlConnection(connectionString);
            this.com=new SqlCommand();
            this.com.Connection=this.cn;
        }

        public async Task<List<string>> GetClientes()
        {
            string sql = "SP_ALL_CLIENTES";
            this.com.CommandType=CommandType.StoredProcedure;
            this.com.CommandText=sql;
            await this.cn.OpenAsync();
            this.reader=await this.com.ExecuteReaderAsync();
            List<string> clientes = new List<string>();
            while(await this.reader.ReadAsync())
            {
                string empresa = this.reader["Empresa"].ToString();
                clientes.Add(empresa);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return clientes;
        }
    }
}
