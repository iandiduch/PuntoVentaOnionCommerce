using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;

namespace Onion_Commerce.Utilitarios
{
    public partial class Frm_Categoria : BaseForm
    {
        public Frm_Categoria()
        {
            InitializeComponent();
        }

        private void Frm_Reg_Prod_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
            Cargar_Todos_categoria();
        }

        private void pnl_titu_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitario obj = new Utilitario();

            if (e.Button == MouseButtons.Left)
            {
                obj.Mover_formulario(this);

            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Configurar_ListView()
        {
            var lis = lsv_categoria;

            lsv_categoria.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //configurar columnas
            lis.Columns.Add("ID", 40, HorizontalAlignment.Left);
            lis.Columns.Add("Categoria", 200, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_categoria.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Cat"].ToString());
                list.SubItems.Add(dr["Categoria"].ToString());
                lsv_categoria.Items.Add(list);
            }
        }

        private void Cargar_Todos_categoria()
        {
            RN_Categoria obj = new RN_Categoria();
            DataTable dato = new DataTable();

            dato = obj.RN_Mostrar_Todas_Categoria();
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_categoria.Items.Clear();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            pnl_add.Visible = true;
            txt_nom.Focus();
            Editar = false;
            btn_edit.Enabled = false;
            btn_remove.Enabled = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            pnl_add.Visible = false;
            txt_nom.Text = "";
            txt_id.Text = "";
            Editar = false;
            btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_add.Enabled = true;
        }

        private void btn_listo_Click(object sender, EventArgs e)
        {
            RN_Categoria obj = new RN_Categoria();

            if (txt_nom.Text.Trim().Length < 0)
            {
                MessageBox.Show("Ingresa el nombre de la categoria", "Registrar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Editar == false)
            {
                //nuevo
                obj.RN_Registrar_Categoria(txt_nom.Text);
            }
            else
            {
                //editar
                obj.RN_Editar_Categoria(Convert.ToInt32(txt_id.Text), txt_nom.Text);

                txt_id.Text = "";
                Editar = false;
            }

            txt_nom.Text = "";
            Cargar_Todos_categoria();
            pnl_add.Visible = false;

            btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_add.Enabled = true;
        }

        public bool Editar = false;

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (lsv_categoria.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecciona una categoria para editar", "Editar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                btn_remove.Enabled = false;
                btn_add.Enabled = false;

                var lsv = lsv_categoria.SelectedItems[0];
                txt_id.Text = lsv.SubItems[0].Text;
                txt_nom.Text = lsv.SubItems[1].Text;

                pnl_add.Visible = true;
                txt_nom.Focus();
                Editar = true;
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_categoria.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecciona una categoria para eliminar", "Eliminar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var lsv = lsv_categoria.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar categoria";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar esta categoria?";
                sino.ShowDialog();

                if (sino.Tag.ToString() == "Si")
                {
                    RN_Marcas obj = new RN_Marcas();
                    obj.RN_Eliminar_Marca(Convert.ToInt32(lsv.SubItems[0].Text));

                    Cargar_Todos_categoria();
                }
            }
        }
    }
}
