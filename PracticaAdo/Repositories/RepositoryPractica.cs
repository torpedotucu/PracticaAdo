using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using PracticaAdo.Helpers;
using PracticaAdo.Models;
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
        public async Task<Cliente> GetInfoClienteAsync(string nombreEmpresa)
        {
            string sql = "SP_CLIENTE_INFO";
            this.com.Parameters.AddWithValue("@nombre", nombreEmpresa);
            this.com.CommandType=CommandType.StoredProcedure;
            this.com.CommandText=sql;
            await this.cn.OpenAsync();
            this.reader=await this.com.ExecuteReaderAsync();
            Cliente cliente = new Cliente();
            while (await this.reader.ReadAsync())
            {
               
                cliente.CodigoCliente=this.reader["CodigoCliente"].ToString();
                cliente.Empresa=this.reader["Empresa"].ToString();
                cliente.Contacto=this.reader["Contacto"].ToString();
                cliente.Cargo=this.reader["Cargo"].ToString();
                cliente.Ciudad=this.reader["Ciudad"].ToString();
                cliente.Telefono=int.Parse(this.reader["Telefono"].ToString());
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

            return cliente;

        }

        public async Task<List<string>> GetPedidosAsync(string nombre)
        {
            string sql = "SP_PEDIDOS_CLIENTE";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.CommandType=CommandType.StoredProcedure;
            this.com.CommandText=sql;

            await this.cn.OpenAsync();
            this.reader=await this.com.ExecuteReaderAsync();
            List<string> pedidos = new List<string>();
            while(await this.reader.ReadAsync())
            {
                string codigoPedido = this.reader["CodigoPedido"].ToString();
                pedidos.Add(codigoPedido);
            }
            await this.cn.CloseAsync();
            await this.reader.CloseAsync();
            this.com.Parameters.Clear();

            return pedidos;
        }

        public async Task<Pedido> GetInfoPedido(string codigo)
        {
            string sql = "SELECT * FROM pedidos where CodigoPedido=@pedido";
            this.com.Parameters.AddWithValue("@pedido", codigo);
            this.com.CommandType=CommandType.Text;
            this.com.CommandText=sql;

            await this.cn.OpenAsync();
            this.reader=await this.com.ExecuteReaderAsync();
            Pedido pedido = new Pedido();
            while(await this.reader.ReadAsync())
            {
                pedido.CodigoPedido=this.reader["CodigoPedido"].ToString();
                pedido.CodigoCliente=this.reader["CodigoCliente"].ToString();
                pedido.FechaEntrega=this.reader["FechaEntrega"].ToString();
                pedido.FormaEnvio=this.reader["FormaEnvio"].ToString();
                pedido.Importe=int.Parse(this.reader["Importe"].ToString());
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

            return pedido;
        }

        public async Task EliminarPedido(string codigo)
        {
            string sql = "SP_DELETE_PEDIDO";
            this.com.Parameters.AddWithValue("@codigo", codigo);
            this.com.CommandType=CommandType.StoredProcedure;
            this.com.CommandText=sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
