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

namespace Onion_Commerce.Compras
{
    public partial class Frm_Cantidad : BaseForm
    {
        public Frm_Cantidad()
        {
            InitializeComponent();
        }

        public string undMedida;
        public string modo;
        public string tipoProd;
        
        private void Frm_Cantidad_Load(object sender, EventArgs e)
        {
            
            if(undMedida.Trim() == "Unidad" || undMedida.Trim() == "Und.")
            {
                txt_cantidad.Visible = false;
                txt_cantidadEntera.Visible = true;
                txt_cantidadEntera.Focus();
            } else
            {
                txt_cantidad.Visible = true;
                txt_cantidadEntera.Visible = false;
                txt_cantidad.Focus();
            }

            if(tipoProd.Trim() != "Producto")
            {
                lbl_stock.Visible = false;
                lbl_stock.Text = "0";
            }
        }

        private void Frm_Cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Tag = "";
                this.Close();
            } else if (e.KeyCode == Keys.Enter)
            {
                if (txt_cantidad.Text.Trim() == "") return;
                if (txt_cantidadEntera.Text.Trim() == "") return;
                if (Convert.ToDouble(txt_cantidad.Text) <= 0 || Convert.ToDouble(txt_cantidadEntera.Text) <= 0)
                {
                    ErrorLog.MensajeError("Error", "La cantidad debe ser mayor a 0!", null); return;
                }
                if(modo == "venta" && tipoProd.Trim() == "Producto")
                {
                    if(Convert.ToDouble(txt_cantidad.Text) > Convert.ToDouble(lbl_stock.Text) || Convert.ToDouble(txt_cantidadEntera.Text) > Convert.ToDouble(lbl_stock.Text))
                    {
                        ErrorLog.MensajeError("Error", "La cantidad no puede ser mayor al stock!", null); return;
                    }
                }

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
