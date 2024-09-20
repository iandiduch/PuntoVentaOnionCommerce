using Onion_Commerce.Productos;
using Onion_Commerce.Utilitarios;
using Capa_Datos;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace Onion_Commerce.Compras
{
    public partial class Frm_Explor_Compras : BaseForm
    {
        public Frm_Explor_Compras()
        {
            InitializeComponent();
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

        private void btn_minimi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Frm_Explor_Prov_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
        }

        private void Configurar_ListView()
        {
            var lis = lsv_compras;

            lsv_compras.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //1211px
            //configurar columnas
            lis.Columns.Add("Cod. Interno", 115, HorizontalAlignment.Left);
            lis.Columns.Add("Nro. Factura", 143, HorizontalAlignment.Left);
            lis.Columns.Add("Tipo Doc.", 109, HorizontalAlignment.Left);

            lis.Columns.Add("Proveedor", 260, HorizontalAlignment.Left); //2
            lis.Columns.Add("Fecha", 106, HorizontalAlignment.Left);

            lis.Columns.Add("Importe $", 123, HorizontalAlignment.Left);
            lis.Columns.Add("Forma Pago", 113, HorizontalAlignment.Left);
            lis.Columns.Add("Observaciones", 223, HorizontalAlignment.Left);
            
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_compras.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_DocComp"].ToString());
                list.SubItems.Add(dr["NroFac_Fisico"].ToString());
                list.SubItems.Add(dr["TipoDoc_Compra"].ToString());
                list.SubItems.Add(dr["NOMBRE"].ToString());
                list.SubItems.Add(Convert.ToDateTime(dr["Fecha_Ingre"]).ToString("dd/MM/yyyy"));

                list.SubItems.Add(dr["Total_Ingre"].ToString());
                list.SubItems.Add(dr["ModalidadPago"].ToString());
                list.SubItems.Add(dr["Datos_Adicional"].ToString());
                lsv_compras.Items.Add(list);
            }

            PintarFilas();
            pnl_BusquedaSinRes.Visible = false;
            lbl_totalItems.Text = lsv_compras.Items.Count.ToString();
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_compras.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_compras.Items[i].BackColor = Color.AliceBlue;

                cont++;
            }
        }

        private async Task Cargar_Compras_PorValorAsync(string val)
        {
            RN_IngresoCompra obj = new RN_IngresoCompra();
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
                lsv_compras.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
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
                        await Cargar_Compras_PorValorAsync(busqueda);
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }

        }

        private async Task Cargar_Todos_ComprasAsync()
        {
            RN_IngresoCompra obj = new RN_IngresoCompra();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy cargando los productos...";

            dato = await Task.Run(() => obj.RN_Listar());
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_compras.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
            }

            pnl_load.Visible = false;
        }

        private async void btn_verTodo_Click(object sender, EventArgs e)
        {
            await Cargar_Todos_ComprasAsync();
        }

        private async void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_compras.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un documento para eliminar.", null);
                return;
            }
            else
            {
                var lsv = lsv_compras.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar documento";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este documento?";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy borrando el documento...";

                    RN_IngresoCompra obj = new RN_IngresoCompra();
                    bool ok = await obj.RN_Borrar(lsv.SubItems[0].Text);

                    if (ok)
                    {

                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El documento se elimino exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_compras.Items.Clear();
                    }

                    pnl_load.Visible = false;

                }
            }
        }

        

        private void copiarCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_compras.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un documento para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_compras.SelectedItems[0];

                string dato = lsv.SubItems[0].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void copiarDescripPrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_compras.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un documento para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_compras.SelectedItems[0];

                string dato = lsv.SubItems[1].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void btn_mas_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(btn_mas, new Point(0, btn_mas.Height));
        }


        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_remove_Click(sender, e);
        }


        private void Frm_Explor_Prod_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F2)
            {
                btn_verTodo_Click(sender, e);
            }
        }

        private void addCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Compras frm = new Frm_Compras();
            frm.Show();
            this.Close();
        }

        private void verComprasDiatoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_SoloFecha frm = new Frm_SoloFecha();

            fil.Show();
            frm.ShowDialog();
            fil.Hide();

            if (frm.Tag.ToString() == "A")
            {
                BuscarPorDiaMes("dia", frm.dtp_fecha.Value);
            }
        }

        private async void BuscarPorDiaMes(string tipo, DateTime time)
        {
            RN_IngresoCompra obj = new RN_IngresoCompra();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy buscando...";

            dato = await Task.Run(() => obj.RN_ListarPorFecha(tipo, time));
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_compras.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
            }
            pnl_load.Visible = false;
        }

        private void verComprasMestoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_SoloFecha frm = new Frm_SoloFecha();

            fil.Show();
            frm.ShowDialog();
            fil.Hide();

            if (frm.Tag.ToString() == "A")
            {
                BuscarPorDiaMes("mes", frm.dtp_fecha.Value);
            }
        }

        private void lsv_compras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btn_remove_Click(sender, e);
            }
        }
    }
}
