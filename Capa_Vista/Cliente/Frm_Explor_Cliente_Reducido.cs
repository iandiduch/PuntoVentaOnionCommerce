using Onion_Commerce.Utilitarios;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Cliente
{
    public partial class Frm_Explor_Cliente_Reducido : BaseForm
    {
        public Frm_Explor_Cliente_Reducido()
        {
            InitializeComponent();
        }

        public string valor;

        private async void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
            await Cargar_Clientes_PorValorAsync(valor);
            txt_buscar.Focus();
            txt_buscar.Text = valor;
        }


        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void Configurar_ListView()
        {
            var lis = lsv_clientes;

            lsv_clientes.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //configurar columnas
            lis.Columns.Add("Cuit", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre", 348, HorizontalAlignment.Left);
            lis.Columns.Add("Saldo", 165, HorizontalAlignment.Left);
            lis.Columns.Add("ID", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Direccion", 0, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_clientes.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["DNI"].ToString());
                list.SubItems.Add(dr["Razon_Social_Nombres"].ToString() + " " + dr["Apellido"].ToString());
                list.SubItems.Add(dr["Limit_Credit"].ToString()); //cambiar por saldo...
                list.SubItems.Add(dr["Id_Cliente"].ToString());
                list.SubItems.Add(dr["Direccion"].ToString() + " - " + dr["Localidad"].ToString() + " - " + dr["Codigo_Postal"].ToString());
                lsv_clientes.Items.Add(list);
            }
        }

        private async Task Cargar_Clientes_PorValorAsync(string val)
        {
            RN_Cliente obj = new RN_Cliente();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy buscando...";

            dato = await Task.Run(() => obj.RN_ListarPorValor(val, "Activo"));
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_clientes.Items.Clear();
            }
            pnl_load.Visible = false;
        }

        private async Task Cargar_Clientes()
        {
            RN_Cliente obj = new RN_Cliente();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy cargando...";

            dato = await Task.Run(() => obj.RN_Listar("Activo"));
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_clientes.Items.Clear();
            }
            pnl_load.Visible = false;
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void txt_buscar_TextChange(object sender, EventArgs e)
        {
            if (txt_buscar.Text.Length > 2)
            {
                cts.Cancel(); // Cancel previous search
                cts = new CancellationTokenSource();

                try
                {
                    await Task.Delay(1000, cts.Token); // Wait for 1 second, but cancel if new input comes

                    if (!cts.Token.IsCancellationRequested)
                    {
                        string busqueda = txt_buscar.Text;
                        await Cargar_Clientes_PorValorAsync(busqueda);
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }

        }

        public string cuit;
        public string nombre;
        public string direccion;
        public string id;
        private void lsv_proveedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Seleccionar();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void lsv_proveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Seleccionar();
            }
        }

        private void Seleccionar()
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                return;
            }
            else
            {
                cuit = lsv_clientes.SelectedItems[0].SubItems[0].Text;
                nombre = lsv_clientes.SelectedItems[0].SubItems[1].Text;
                id = lsv_clientes.SelectedItems[0].SubItems[3].Text;
                direccion = lsv_clientes.SelectedItems[0].SubItems[4].Text;

                this.Tag = "A";
                this.Close();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Add_Cliente add = new Frm_Add_Cliente();

            add.modo = "Nuevo";

            fil.Show();
            add.ShowDialog();
            fil.Hide();


            if (add.Tag.ToString() == "A")
            {
                txt_buscar.Text = add.cuitCliente;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void Frm_Explor_Cliente_Reducido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btn_add_Click(sender, e);
            }
        }

        private async void txt_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                
                if(lsv_clientes.Items.Count > 0)
                {
                    lsv_clientes.Items[0].Selected = true;
                    lsv_clientes.Select();
                }
                else
                {
                    await Cargar_Clientes();
                }
            }
        }
    }
}
