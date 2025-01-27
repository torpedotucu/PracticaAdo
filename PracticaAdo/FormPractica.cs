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
            foreach(string empresa in clientes)
            {
                this.cmbclientes.Items.Add(empresa);
            }
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {

        }
    }
}
