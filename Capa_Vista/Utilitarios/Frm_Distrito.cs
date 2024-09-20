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
    public partial class Frm_Distrito : BaseForm
    {
        public Frm_Distrito()
        {
            InitializeComponent();
        }

        private void Frm_Reg_Prod_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
            Cargar_Todos_Distrito();
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
            var lis = lsv_marca;

            lsv_marca.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //configurar columnas
            lis.Columns.Add("ID", 40, HorizontalAlignment.Left);
            lis.Columns.Add("Distrito", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 80, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_marca.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Dis"].ToString());
                list.SubItems.Add(dr["Distrito"].ToString());
                list.SubItems.Add(dr["Estado_Dis"].ToString());
                lsv_marca.Items.Add(list);
            }
        }

        private void Cargar_Todos_Distrito()
        {
            RN_Distrito obj = new RN_Distrito();
            DataTable dato = new DataTable();

            dato = obj.RN_Cargar_Todas_Distrito();
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_marca.Items.Clear();
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
            RN_Distrito obj = new RN_Distrito();

            if (txt_nom.Text.Trim().Length < 0)
            {
                MessageBox.Show("Ingresa el nombre del distrito", "Registrar distrito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Editar == false)
            {
                //nuevo
                obj.RN_Registrar_Distrito(txt_nom.Text);
            }
            else
            {
                //editar
                obj.RN_Editar_Distrito(Convert.ToInt32(txt_id.Text), txt_nom.Text);

                txt_id.Text = "";
                Editar = false;
            }

            txt_nom.Text = "";
            Cargar_Todos_Distrito();
            pnl_add.Visible = false;

            btn_edit.Enabled = true;
            btn_remove.Enabled = true;
            btn_add.Enabled = true;
        }

        public bool Editar = false;

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (lsv_marca.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecciona un distrito para editar", "Editar distrito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                btn_remove.Enabled = false;
                btn_add.Enabled = false;

                var lsv = lsv_marca.SelectedItems[0];
                txt_id.Text = lsv.SubItems[0].Text;
                txt_nom.Text = lsv.SubItems[1].Text;

                pnl_add.Visible = true;
                txt_nom.Focus();
                Editar = true;
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_marca.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecciona un distrito para eliminar", "Editar distrito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var lsv = lsv_marca.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar distrito";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este distrito?";
                sino.ShowDialog();  

                if(sino.Tag.ToString() == "Si")
                {
                    RN_Distrito obj = new RN_Distrito();
                    obj.RN_Eliminar_Distrito(Convert.ToInt32(lsv.SubItems[0].Text));

                    Cargar_Todos_Distrito();
                }

            }
        }
    }
}
