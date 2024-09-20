using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Onion_Commerce.Cliente;
using Onion_Commerce.Compras;
using Onion_Commerce.Cotizacion;
using Onion_Commerce.Productos;
using Onion_Commerce.Proveedores;
using Onion_Commerce.Utilitarios;
using Onion_Commerce.Ventas;
using Capa_Entidad;
using static Syncfusion.XlsIO.Parser.Biff_Records.AutoFilterRecord;

namespace Onion_Commerce
{
    public partial class Frm_Principal : BaseForm
    {
        public Frm_Principal()
        {
            InitializeComponent();
        }

        private async void Frm_Principal_Load(object sender, EventArgs e)
        {
            bool ok = await CargarPrecioDolar();
            if (!ok)
            {
                precioCompraBlue = "0";
                precioVentaBlue = "0";
            }
        }

        public static string precioCompraBlue;
        public static string precioVentaBlue;
        private async Task<bool> CargarPrecioDolar()
        {
            ApiResponseDolar dolar = new ApiResponseDolar();
            GetPrecioDolar getApi = new GetPrecioDolar();

            dolar = await Task.Run(() => getApi.Get());

            if (dolar != null)
            {
                precioCompraBlue = dolar.compra.ToString();
                precioVentaBlue = dolar.venta.ToString();

                //DateTime fecha = DateTime.ParseExact(dolar.fechaActualizacion, "yyyy-MM-ddTHH:mm:ss.fffZ", null);

                //lbl_fechaDolarBlue.Text = "Actualizado el \n" + fecha.ToString("dd MMMM yyyy 'a las' HH:mm", new System.Globalization.CultureInfo("es-ES"));

                return true;
            }

            return false;
        }

        private void bt_almacen_Click(object sender, EventArgs e)
        {
            
            Frm_Explor_Prod pro = new Frm_Explor_Prod();

            pro.MdiParent = this;
            pro.Show();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Pnl_Menu_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitario obj = new Utilitario();
            if (e.Button == MouseButtons.Left)
            {
                obj.Mover_formulario(this);
            }
        }

        private void btn_minimi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {

            if (PanelLateral.Width == 247)
            {
                PanelLateral.Width = 40;
                PicUser_2.Visible = true;
            }
            else
            {
                PanelLateral.Width = 247;
                PicUser.Visible = true;
                PicUser_2.Visible = false;
            }
        }

        private void Bt_ventas_Click(object sender, EventArgs e)
        {
            Frm_Ventas ven = new Frm_Ventas();

            ven.MdiParent = this;
            ven.Show();


        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Restaurar el tamaño normal del formulario
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                // Maximizar el formulario
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void bt_cliente_Click(object sender, EventArgs e)
        {
            Frm_Explor_Cliente obj = new Frm_Explor_Cliente();


            obj.MdiParent = this;
            obj.Show();

        }

        private void Bt_AbrirExploradorDeProveedores_Click(object sender, EventArgs e)
        {
            Frm_Explor_Prove obj = new Frm_Explor_Prove();

            obj.MdiParent = this;
            obj.Show();
        }

        private void bt_VerExploradorDeProductos_Click(object sender, EventArgs e)
        {
            bt_almacen_Click(sender, e);
        }

        private void bt_compras_Click(object sender, EventArgs e)
        {
            Frm_Compras frm_Compras = new Frm_Compras();
            frm_Compras.MdiParent = this;
            frm_Compras.Show();
        }

        private void Bt_RegistrarUnaCompra_Click(object sender, EventArgs e)
        {
            bt_compras_Click(sender, e);
        }

        private void Bt_AbrirExploradorDeCompras_Click(object sender, EventArgs e)
        {
            Frm_Explor_Compras frm_Explor_Compras = new Frm_Explor_Compras();
            frm_Explor_Compras.MdiParent = this;
            frm_Explor_Compras.Show();
        }

        private void Bt_cotizar_Click(object sender, EventArgs e)
        {
            Frm_Cotizacion frm_Cotizacion = new Frm_Cotizacion();
            frm_Cotizacion.MdiParent = this;
            frm_Cotizacion.Show();
        }
    }
}
