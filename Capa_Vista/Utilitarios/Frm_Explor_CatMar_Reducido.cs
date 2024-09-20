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

namespace Onion_Commerce.Utilitarios
{
    public partial class Frm_Explor_CatMar_Reducido : BaseForm
    {
        public Frm_Explor_CatMar_Reducido()
        {
            InitializeComponent();
        }

        public string modo;
        private async void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            Configurar_ListView();

            if (modo == "Categoria")
            {
                lbl_title.Text = "Seleccione una categoria";
                await Cargar_Todos_categoria();
            }
            else if (modo == "Marca")
            {
                lbl_title.Text = "Seleccione una marca";
                await Cargar_Todos_marca();
            }
        }


        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void Configurar_ListView()
        {
            var lis = lsv;

            lsv.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //configurar columnas
            lis.Columns.Add("Id", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre", 250, HorizontalAlignment.Left);
        }

        private void Llenar_ListViewMarca(DataTable data)
        {
            lsv.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Marca"].ToString());
                list.SubItems.Add(dr["Marca"].ToString());
                lsv.Items.Add(list);
            }
        }

        private void Llenar_ListViewCategoria(DataTable data)
        {
            lsv.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Cat"].ToString());
                list.SubItems.Add(dr["Categoria"].ToString());
                lsv.Items.Add(list);
            }
        }

        private async Task Cargar_Todos_categoria()
        {
            RN_Categoria obj = new RN_Categoria();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy buscando...";

            dato = await Task.Run(()=>obj.RN_Mostrar_Todas_Categoria());
            if (dato.Rows.Count > 0)
            {
                Llenar_ListViewCategoria(dato);
            }
            else
            {
                lsv.Items.Clear();
            }
            pnl_load.Visible = false;
        }

        private async Task Cargar_Todos_marca()
        {
            RN_Marcas obj = new RN_Marcas();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy buscando...";

            dato = await Task.Run(()=>obj.RN_Cargar_Todas_Marca());
            if (dato.Rows.Count > 0)
            {
                Llenar_ListViewMarca(dato);
            }
            else
            {
                lsv.Items.Clear();
            }
            pnl_load.Visible = false;

        }

        public string id;
        public string nombre;
        private void lsv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsv.SelectedIndices.Count == 0)
            {
                return;
            }
            else
            {
                id = lsv.SelectedItems[0].SubItems[0].Text;
                nombre = lsv.SelectedItems[0].SubItems[1].Text;

                this.Tag = "A";
                this.Close();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (lsv.SelectedIndices.Count == 0)
            {
                return;
            }
            else
            {
                id = lsv.SelectedItems[0].SubItems[0].Text;
                nombre = lsv.SelectedItems[0].SubItems[1].Text;

                this.Tag = "A";
                this.Close();
            }
        }

        private void lsv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lsv.SelectedIndices.Count == 0)
                {
                    return;
                }
                else
                {
                    id = lsv.SelectedItems[0].SubItems[0].Text;
                    nombre = lsv.SelectedItems[0].SubItems[1].Text;

                    this.Tag = "A";
                    this.Close();
                }
            }
        }
    }
}
