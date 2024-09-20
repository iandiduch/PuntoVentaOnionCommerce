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

namespace Onion_Commerce.Proveedores
{
    public partial class Frm_Explor_Prove : BaseForm
    {
        public Frm_Explor_Prove()
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
            var lis = lsv_proveedores;

            lsv_proveedores.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //1211px
            //configurar columnas
            lis.Columns.Add("ID", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Cuit/Dni", 200, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre", 200, HorizontalAlignment.Left);
            lis.Columns.Add("Dirección", 200, HorizontalAlignment.Left);
            lis.Columns.Add("Celular", 200, HorizontalAlignment.Left);
            lis.Columns.Add("Razón Social", 200, HorizontalAlignment.Left);
            lis.Columns.Add("Correo", 200, HorizontalAlignment.Left);
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_proveedores.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["IDPROVEE"].ToString());
                list.SubItems.Add(dr["RUC"].ToString());
                list.SubItems.Add(dr["NOMBRE"].ToString());
                list.SubItems.Add(dr["DIRECCION"].ToString());
                list.SubItems.Add(dr["TELEFONO"].ToString());
                list.SubItems.Add(dr["RUBRO"].ToString());
                list.SubItems.Add(dr["CORREO"].ToString());
                lsv_proveedores.Items.Add(list);
            }

            PintarFilas();
            pnl_BusquedaSinRes.Visible = false;
            lbl_totalItems.Text = lsv_proveedores.Items.Count.ToString();
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_proveedores.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_proveedores.Items[i].BackColor = Color.AliceBlue;

                cont++;
            }
        }

        private async Task Cargar_Prov_PorValorAsync(string val)
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
                        await Cargar_Prov_PorValorAsync(busqueda);
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }

        }

        private async Task Cargar_Todos_ProvAsync()
        {
            RN_Proveedor obj = new RN_Proveedor();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy cargando los proveedores...";

            dato = await Task.Run(() => obj.RN_Listar());
            if (dato.Rows.Count > 0)
            {
                Llenar_ListView(dato);
            }
            else
            {
                lsv_proveedores.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
            }

            pnl_load.Visible = false;
        }

        private async void btn_verTodo_Click(object sender, EventArgs e)
        {
            await Cargar_Todos_ProvAsync();
        }

        private void btn_vistaPrevia_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un proveedor para ver.", null);
                return;
            }
            else
            {
                var lsv = lsv_proveedores.SelectedItems[0];
                string idProv = lsv.SubItems[0].Text;

                CargarVistaPrevia(idProv);
            }
        }

        private async void CargarVistaPrevia(string valor)
        {
            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Cargando la vista previa...";

            string idProv = valor;

            RN_Proveedor obj = new RN_Proveedor();
            DataTable dato = new DataTable();

            try
            {

                dato = await Task.Run(() => obj.RN_Listar_PorValor(idProv));
                if (dato.Rows.Count > 0)
                {
                    vp_descripcion.Text = Convert.ToString(dato.Rows[0]["NOMBRE"]);
                    vp_cuit.Text = "Cuit/Dni: " + Convert.ToString(dato.Rows[0]["RUC"]);
                    vp_direccion.Text = "Dirección: " + Convert.ToString(dato.Rows[0]["DIRECCION"]);
                    vp_Rubro.Text = "Rubro: " + Convert.ToString(dato.Rows[0]["RUBRO"]);
                    vp_telefono.Text = "Celular: " + Convert.ToString(dato.Rows[0]["TELEFONO"]);
                    vp_correo.Text = "Correo: " + Convert.ToString(dato.Rows[0]["CORREO"]);


                    string xFotoRuta = Convert.ToString(dato.Rows[0]["FOTO_LOGO"]);

                    // Verifica si el archivo especificado existe
                    if (System.IO.File.Exists(xFotoRuta))
                    {
                        vp_foto.Load(xFotoRuta); // Carga la imagen si existe
                    }
                    else
                    {
                        vp_foto.Image = Properties.Resources.sin_imagen; // Carga la imagen por defecto si no existe
                    }


                    pnl_VistaPrevia.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al cargar el proveedor.", ex);
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
            Frm_Add_Prov add = new Frm_Add_Prov();

            fil.Show();
            add.modo = "Nuevo";
            add.ShowDialog();
            fil.Hide();

            if (add.Tag.ToString() == "A")
            {
                lsv_proveedores.Items.Clear();
                lbl_totalItems.Text = "0";
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Error!", "Selecciona un proveedor para editar.", null);
                return;
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Add_Prov edit = new Frm_Add_Prov();

                var lsv = lsv_proveedores.SelectedItems[0];
                string id = lsv.SubItems[0].Text;

                fil.Show();
                edit.idProveedor = id;
                edit.modo = "Editar";
                edit.ShowDialog();
                fil.Hide();

                //despues que se guarda o cancela la editacion se recarga la tabla

                if (edit.Tag.ToString() == "A")
                {
                    CargarVistaPrevia(id);
                    lsv_proveedores.Items.Clear();
                    lbl_totalItems.Text = "0";
                }

            }
        }

        private async void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un proveedor para eliminar.", null);
                return;
            }
            else
            {
                var lsv = lsv_proveedores.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar proveedor";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este proveedor?";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy borrando el proveedor...";

                    RN_Proveedor obj = new RN_Proveedor();
                    bool ok = await obj.RN_Eliminar(lsv.SubItems[0].Text);

                    if (ok)
                    {

                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El proveedor se elimino exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_proveedores.Items.Clear();
                        lbl_totalItems.Text = "0";
                    }

                    pnl_load.Visible = false;

                }
            }
        }

       

        private void copiarCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un proveedor para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_proveedores.SelectedItems[0];

                string dato = lsv.SubItems[1].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void copiarDescripPrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_proveedores.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un proveedor para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_proveedores.SelectedItems[0];

                string dato = lsv.SubItems[4].Text;

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


        private void verProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_vistaPrevia_Click(sender, e);
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_edit_Click(sender, e);
        }


        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_remove_Click(sender, e);
        }

        private void Frm_Explor_Prove_KeyDown(object sender, KeyEventArgs e)
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

        private void lsv_proveedores_KeyDown(object sender, KeyEventArgs e)
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
