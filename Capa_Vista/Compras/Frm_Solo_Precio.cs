using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Compras
{
    public partial class Frm_Solo_Precio : BaseForm
    {
        public Frm_Solo_Precio()
        {
            InitializeComponent();
        }

        private void Frm_Solo_Canti_Load(object sender, EventArgs e)
        {
            txt_precio.Focus();
        }

        private void Frm_Solo_Canti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Tag = "";
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (txt_precio.Text.Trim() == "") return;
                if (txt_precio.Text.Trim().Length == 0) { MessageBox.Show("Ingrese el Precio del Producto", "Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txt_precio.Focus(); return; }
                if (Convert.ToDouble(txt_precio.Text) == 0) { MessageBox.Show("El Precio debe ser Mayor a Cero", "Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txt_precio.Focus(); return; }

                this.Tag = "A";
                this.Close();
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }
    }
}
