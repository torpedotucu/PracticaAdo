using PracticaAdo.Models;
using PracticaAdo.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class FormPractica : Form
    {
        RepositoryPractica repo;
        public FormPractica()
        {
            InitializeComponent();
            this.repo=new RepositoryPractica();
            this.LoadClientesAsync();
        }

        private async Task LoadClientesAsync()
        {
            List<string> clientes = await this.repo.GetClientes();
            this.cmbclientes.Items.Clear();
            foreach (string empresa in clientes)
            {
                this.cmbclientes.Items.Add(empresa);
            }
        }
        private async Task LoadInfoClienteAsync(string nombre)
        {
            Cliente cliente = await this.repo.GetInfoClienteAsync(nombre);
            this.txtempresa.Text=cliente.Empresa;
            this.txtcontacto.Text=cliente.Contacto;
            this.txttelefono.Text=cliente.Telefono.ToString();
            this.txtcargo.Text=cliente.Cargo;
            this.txtciudad.Text=cliente.Ciudad;
        }
        private async Task LoadPedidosClientes(string nombreEmpresa)
        {
            List<string> pedidos = await this.repo.GetPedidosAsync(nombreEmpresa);
            this.lstpedidos.Items.Clear();
            foreach (string codigo in pedidos)
            {
                this.lstpedidos.Items.Add(codigo);
            }
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {

        }

        private async void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            await this.LoadInfoClienteAsync(this.cmbclientes.SelectedItem.ToString());
            await this.LoadPedidosClientes(this.cmbclientes.SelectedItem.ToString());
        }

        private async void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtcodigopedido.Clear();
            this.txtfechaentrega.Clear();
            this.txtformaenvio.Clear();
            this.txtimporte.Clear();
            string codigoPedido = this.lstpedidos.SelectedItem.ToString();
            Pedido pedido = new Pedido();
            pedido=await this.repo.GetInfoPedido(codigoPedido);

            this.txtcodigopedido.Text=pedido.CodigoPedido;
            this.txtfechaentrega.Text=pedido.FechaEntrega;
            this.txtformaenvio.Text=pedido.FormaEnvio;
            this.txtimporte.Text=pedido.Importe.ToString();
        }

        private async void btneliminarpedido_Click(object sender, EventArgs e)
        {
            string codigo = this.lstpedidos.SelectedItem.ToString();
            await this.repo.EliminarPedido(codigo);
            this.txtcodigopedido.Clear();
            this.txtfechaentrega.Clear();
            this.txtformaenvio.Clear();
            this.txtimporte.Clear();
            this.LoadPedidosClientes(this.cmbclientes.SelectedItem.ToString());
        }
    }
}
