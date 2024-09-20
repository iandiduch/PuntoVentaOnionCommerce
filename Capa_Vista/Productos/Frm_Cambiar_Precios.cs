using Onion_Commerce.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Productos
{
    public partial class Frm_Cambiar_Precios : BaseForm
    {
        public Frm_Cambiar_Precios()
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
                btn_listo_Click(sender, e);
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void btn_listo_Click(object sender, EventArgs e)
        {
            if (txt_precio.Text.Trim() == "") return;
            if (txt_precio.Text.Trim().Length == 0) { ErrorLog.MensajeError("Advertencia", "Ingrese el valor", null); txt_precio.Focus(); return; }
            if (Convert.ToDouble(txt_precio.Text) == 0) { ErrorLog.MensajeError("Advertencia", "El valor debe ser mayor a cero.", null); txt_precio.Focus(); return; }

            this.Tag = "A";
            this.Close();
        }

        private void check_precioCosto_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (check_precioCosto.Checked == true)
            {
                check_actualizarPrecios.Enabled = true;
            }
            else
            {
                check_actualizarPrecios.Enabled = false;
            }
        }

        private void check_precioVenta_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if(check_precioVenta.Checked == true)
            {
                check_actualizarPrecios.Enabled = false;
            }
        }

        private void check_Porcentaje_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if(check_Porcentaje.Checked == true)
            {
                txt_precio.Focus();
            }
        }

        private void check_montoFijo_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if(check_montoFijo.Checked == true)
            {
                txt_precio.Focus();
            }
        }
    }
}
