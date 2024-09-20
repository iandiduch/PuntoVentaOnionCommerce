using Onion_Commerce.Productos;
using Onion_Commerce.Utilitarios;
using Capa_Datos;
using Capa_Negocio;
using Syncfusion.Compression;
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

namespace Onion_Commerce.Cliente
{
    public partial class Frm_Explor_Cliente : BaseForm
    {
        public Frm_Explor_Cliente()
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
            var lis = lsv_clientes;

            lsv_clientes.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //1211px
            //configurar columnas
            lis.Columns.Add("ID", 0, HorizontalAlignment.Left);
            lis.Columns.Add("CUIT/DNI", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Razón Social", 300, HorizontalAlignment.Left);
            lis.Columns.Add("Dirección", 300, HorizontalAlignment.Left); //2
            lis.Columns.Add("Credito", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Celular", 120, HorizontalAlignment.Left);

            lis.Columns.Add("Correo", 180, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 75, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_clientes.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Cliente"].ToString());
                list.SubItems.Add(dr["DNI"].ToString());
                list.SubItems.Add(dr["Razon_Social_Nombres"].ToString() + " " + dr["Apellido"].ToString());
                list.SubItems.Add(dr["Direccion"].ToString() + " - " + dr["Localidad"].ToString() + " - " + dr["Codigo_Postal"].ToString());
                list.SubItems.Add(dr["Limit_Credit"].ToString());
                list.SubItems.Add(dr["telefono"].ToString());
                list.SubItems.Add(dr["e_mail"].ToString());
                list.SubItems.Add(dr["Estado_cli"].ToString());
                lsv_clientes.Items.Add(list);
            }

            PintarFilas();
            pnl_BusquedaSinRes.Visible = false;
            lbl_totalItems.Text = lsv_clientes.Items.Count.ToString();
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_clientes.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_clientes.Items[i].BackColor = Color.AliceBlue;

                cont++;
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
                        await Cargar_Clientes_PorValorAsync(busqueda);
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }

        }

        private async Task Cargar_Todos_ClientesAsync()
        {
            RN_Cliente obj = new RN_Cliente();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy cargando los clientes...";

            dato = await Task.Run(() => obj.RN_Listar("Activo"));
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_clientes.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
            }

            pnl_load.Visible = false;
        }

        private async void btn_verTodo_Click(object sender, EventArgs e)
        {
            await Cargar_Todos_ClientesAsync();
        }

        private void btn_vistaPrevia_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para ver.", null);
                return;
            }
            else
            {
                var lsv = lsv_clientes.SelectedItems[0];
                string idCliente = lsv.SubItems[0].Text;

                CargarVistaPrevia(idCliente);
            }
        }

        private async void CargarVistaPrevia(string valor)
        {
            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Cargando la vista previa...";

            string idCliente = valor;

            RN_Cliente obj = new RN_Cliente();
            DataTable dato = new DataTable();

            try
            {

                dato = await Task.Run(() => obj.RN_ListarPorValor(idCliente, "Activo"));
                if (dato.Rows.Count > 0)
                {
                    vp_descripcion.Text = Convert.ToString(dato.Rows[0]["Razon_Social_Nombres"] + " " + dato.Rows[0]["Apellido"]);
                    vp_cuit.Text = "Cuit/Dni: " + Convert.ToString(dato.Rows[0]["DNI"]);
                    vp_direccion.Text = "Dirección: " + Convert.ToString(dato.Rows[0]["Direccion"] + " - " + dato.Rows[0]["Localidad"] + " - " + dato.Rows[0]["Codigo_Postal"]);
                    vp_Provincia.Text = "Provincia: " + Convert.ToString(dato.Rows[0]["Distrito"]);
                    vp_fechaNac.Text = "Fecha Nac.: " + Convert.ToDateTime(dato.Rows[0]["Fcha_Ncmnto_Anivsrio"]).ToString("dd/MM/yyyy");
                    vp_telefono.Text = "Celular: " + Convert.ToString(dato.Rows[0]["telefono"]);
                    vp_correo.Text = "Correo: " + Convert.ToString(dato.Rows[0]["e_mail"]);
                    vp_limiteCredito.Text = "Limite de Credito: $ " + Convert.ToString(dato.Rows[0]["Limit_Credit"]);


                    pnl_VistaPrevia.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al cargar el cliente.", ex);
                pnl_VistaPrevia.Visible = false;
            }
            finally
            {
                pnl_load.Visible = false;
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
                lsv_clientes.Items.Clear();
                lbl_totalItems.Text = "0";
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para editar.", null);
                return;
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Add_Cliente edit = new Frm_Add_Cliente();

                var lsv = lsv_clientes.SelectedItems[0];
                string id = lsv.SubItems[0].Text;

                edit.idCliente = id;
                edit.modo = "Editar";

                fil.Show();
                edit.ShowDialog();
                fil.Hide();

                //despues que se guarda o cancela la editacion se recarga la tabla
                if (edit.Tag.ToString() == "A")
                {
                    CargarVistaPrevia(id);
                    lsv_clientes.Items.Clear();
                    lbl_totalItems.Text = "0";
                }


            }
        }

        private async void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para eliminar.", null);
                return;
            }
            else
            {
                var lsv = lsv_clientes.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar cliente";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este cliente?";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy borrando el cliente...";

                    RN_Cliente obj = new RN_Cliente();
                    bool ok = await obj.RN_Eliminar(lsv.SubItems[0].Text);

                    if (ok)
                    {

                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El cliente se elimino exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_clientes.Items.Clear();
                        lbl_totalItems.Text = "0";
                    }

                    pnl_load.Visible = false;

                }
            }
        }



        private void copiarCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_clientes.SelectedItems[0];

                string dato = lsv.SubItems[1].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void copiarDescripPrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_clientes.SelectedItems[0];

                string dato = lsv.SubItems[2].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void btn_cerrarVistaPrevia_Click(object sender, EventArgs e)
        {
            pnl_VistaPrevia.Visible = false;
        }

        private void lsv_productos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_vistaPrevia_Click(sender, e);
        }

        private async void btn_baja_Click(object sender, EventArgs e)
        {
            if (lsv_clientes.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente para dar de baja.", null);
                return;
            }
            else
            {
                var lsv = lsv_clientes.SelectedItems[0];
                string idCliente = lsv.SubItems[0].Text;

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "¿Dar de baja cliente?";
                sino.lbl_msm.Text = "El cliente no sera visible pero seguira guardado.";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy dando de baja el cliente...";

                    RN_Cliente obj = new RN_Cliente();
                    bool ok = await obj.RN_Baja(idCliente);
                    if (ok)
                    {
                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El cliente se dio de baja exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_clientes.Items.Clear();
                        lbl_totalItems.Text = "0";
                    }

                    pnl_load.Visible = false;

                }

            }
        }

        private void verProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_vistaPrevia_Click(sender, e);
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_edit_Click(sender, e);
        }

        private void darDeBajaProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_baja_Click(sender, e);
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_remove_Click(sender, e);
        }

        private void Frm_Explor_Cliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_verTodo_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F1)
            {
                btn_add_Click(sender, e);
            }
        }

        private void lsv_clientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btn_remove_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btn_vistaPrevia_Click(sender, e);
            }
        }
    }
}
