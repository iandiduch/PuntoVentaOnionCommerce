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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Proveedores
{
    public partial class Frm_Explor_Prov_Reducido : BaseForm
    {
        public Frm_Explor_Prov_Reducido()
        {
            InitializeComponent();
        }

        private void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
        }


        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void Configurar_ListView()
        {
            var lis = lsv_proveedores;

            lsv_proveedores.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //configurar columnas
            lis.Columns.Add("Cuit", 81, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre", 170, HorizontalAlignment.Left);
            lis.Columns.Add("ID", 0, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_proveedores.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["RUC"].ToString());
                list.SubItems.Add(dr["NOMBRE"].ToString());
                list.SubItems.Add(dr["IDPROVEE"].ToString());
                lsv_proveedores.Items.Add(list);
            }
        }

        public async Task Buscar(string val)
        {
            RN_Proveedor obj = new RN_Proveedor();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy buscando...";

            dato = await Task.Run(() => obj.RN_Listar_PorValor(val));
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_proveedores.Items.Clear();
            }

            pnl_load.Visible = false;
        }

        public string cuit;
        public string nombre;
        public string id;
        private void lsv_proveedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                return;
            } else
            {
                cuit = lsv_proveedores.SelectedItems[0].SubItems[0].Text;
                nombre = lsv_proveedores.SelectedItems[0].SubItems[1].Text;
                id = lsv_proveedores.SelectedItems[0].SubItems[2].Text;

                this.Tag = "A";
                this.Close();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                return;
            }
            else
            {
                cuit = lsv_proveedores.SelectedItems[0].SubItems[0].Text;
                nombre = lsv_proveedores.SelectedItems[0].SubItems[1].Text;
                id = lsv_proveedores.SelectedItems[0].SubItems[2].Text;

                this.Tag = "A";
                this.Close();
            }
        }

        private void lsv_proveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (lsv_proveedores.SelectedIndices.Count == 0)
                {
                    return;
                }
                else
                {
                    cuit = lsv_proveedores.SelectedItems[0].SubItems[0].Text;
                    nombre = lsv_proveedores.SelectedItems[0].SubItems[1].Text;
                    id = lsv_proveedores.SelectedItems[0].SubItems[2].Text;

                    this.Tag = "A";
                    this.Close();
                }
            }
        }

        private void Frm_Explor_Prov_Reducido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Tag = "";
                this.Close();
            }
        }
    }
}
