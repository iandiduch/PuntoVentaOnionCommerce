using Onion_Commerce.Productos;
using Onion_Commerce.Utilitarios;
using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Syncfusion.XlsIO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace Onion_Commerce.Productos
{
    public partial class Frm_Explor_Prod : BaseForm
    {
        public Frm_Explor_Prod()
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

            CargarPrecioDolar();
        }

        public string precioVentaBlue;

        private bool CargarPrecioDolar()
        {
            if (Convert.ToDouble(Frm_Principal.precioVentaBlue) > 0 && Convert.ToDouble(Frm_Principal.precioCompraBlue) > 0)
            {

                precioVentaBlue = Frm_Principal.precioVentaBlue;
                return true;
            }

            return false;
        }

        private void Configurar_ListView()
        {
            var lis = lsv_productos;

            lsv_productos.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.CheckBoxes = true;

            //1211px
            //configurar columnas
            lis.Columns.Add("Codigo", 147, HorizontalAlignment.Left);//0
            lis.Columns.Add("Marca", 113, HorizontalAlignment.Left);//1
            lis.Columns.Add("Descripción del Producto", 300, HorizontalAlignment.Left); //2
            lis.Columns.Add("Categoria", 106, HorizontalAlignment.Left);//3

            lis.Columns.Add("Stock", 52, HorizontalAlignment.Left);//4
            lis.Columns.Add("Margen %", 84, HorizontalAlignment.Left);//5
            lis.Columns.Add("Ganancia $", 113, HorizontalAlignment.Left);//6
            lis.Columns.Add("Precio Costo", 115, HorizontalAlignment.Left);//7
            lis.Columns.Add("Precio Venta", 115, HorizontalAlignment.Left);//8
            lis.Columns.Add("Valor Total", 0, HorizontalAlignment.Left);//9
            lis.Columns.Add("Estado", 67, HorizontalAlignment.Left);//10
            lis.Columns.Add("Tipo", 0, HorizontalAlignment.Left);//11
            lis.Columns.Add("Precio USD", 0, HorizontalAlignment.Left);//12
            lis.Columns.Add("Precio Costo USD", 0, HorizontalAlignment.Left);//13
        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_productos.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Pro"].ToString());
                list.SubItems.Add(dr["Marca"].ToString());
                list.SubItems.Add(dr["Descripcion_Larga"].ToString());
                list.SubItems.Add(dr["Categoria"].ToString());
                list.SubItems.Add(dr["Stock_Actual"].ToString());
                list.SubItems.Add(dr["Frank"].ToString());
                list.SubItems.Add(Convert.ToDouble(dr["UtilidadUnit"]).ToString("N2"));
                list.SubItems.Add(Convert.ToDouble(dr["Pre_CompraS"]).ToString("N2"));
                list.SubItems.Add(Convert.ToDouble(dr["Pre_vntaxMenor"]).ToString("N2"));
                list.SubItems.Add(Convert.ToDouble(dr["Valor_porCant"]).ToString("N2"));
                list.SubItems.Add(dr["Estado_Pro"].ToString());
                list.SubItems.Add(dr["TipoProdcto"].ToString());
                list.SubItems.Add(Convert.ToDouble(dr["Pre_Vntadolar"]).ToString("N2"));
                list.SubItems.Add(Convert.ToDouble(dr["Pre_Compra$"]).ToString("N2"));
                lsv_productos.Items.Add(list);
            }

            PintarFilas();
            pnl_BusquedaSinRes.Visible = false;
            lbl_totalItems.Text = lsv_productos.Items.Count.ToString();
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_productos.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_productos.Items[i].BackColor = Color.AliceBlue;

                cont++;
            }
        }

        private async Task Cargar_Prod_PorValorAsync(string val)
        {
            RN_Producto obj = new RN_Producto();
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
                lsv_productos.Items.Clear();
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
                        await Cargar_Prod_PorValorAsync(busqueda);
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }

        }

        private async Task Cargar_Todos_ProdAsync()
        {
            RN_Producto obj = new RN_Producto();
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
                lsv_productos.Items.Clear();
                pnl_BusquedaSinRes.Visible = true;
                lbl_totalItems.Text = "0";
            }

            pnl_load.Visible = false;
        }

        private async void btn_verTodo_Click(object sender, EventArgs e)
        {
            await Cargar_Todos_ProdAsync();
        }

        private void btn_vistaPrevia_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para ver.", null);
                return;
            }
            else
            {
                var lsv = lsv_productos.SelectedItems[0];
                string idProducto = lsv.SubItems[0].Text;

                CargarVistaPrevia(idProducto);
            }
        }

        private async void CargarVistaPrevia(string valor)
        {
            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Cargando la vista previa...";

            string idProducto = valor;

            RN_Producto obj = new RN_Producto();
            DataTable dato = new DataTable();

            try
            {

                dato = await Task.Run(() => obj.RN_Listar_PorValor(idProducto));
                if (dato.Rows.Count > 0)
                {
                    vp_descripcion.Text = Convert.ToString(dato.Rows[0]["Descripcion_Larga"]);

                    string xFotoRuta = Convert.ToString(dato.Rows[0]["Foto"]);

                    // Verifica si el archivo especificado existe
                    if (System.IO.File.Exists(xFotoRuta))
                    {
                        vp_foto.Load(xFotoRuta); // Carga la imagen si existe
                    }
                    else
                    {
                        vp_foto.Image = Properties.Resources.producto_sin_imagen; // Carga la imagen por defecto si no existe
                    }

                    vp_stock.Text = "Stock: " + Convert.ToString(dato.Rows[0]["Stock_Actual"]);
                    vp_unidadMedida.Text = "Se vende por: " + Convert.ToString(dato.Rows[0]["UndMedida"]);

                    if (Convert.ToDouble(dato.Rows[0]["PesoUnit"]) > 0)
                    {
                        vp_peso.Visible = true;
                        vp_peso.Text = "Peso Unitario: " + Convert.ToString(dato.Rows[0]["PesoUnit"]);
                    }
                    else { vp_peso.Visible = false; }

                    vp_estado.Text = "Estado: " + Convert.ToString(dato.Rows[0]["Estado_Pro"]);


                    vp_costo.Text = "Costo: $ " + Convert.ToDouble(dato.Rows[0]["Pre_CompraS"]).ToString("N2");

                    if (Convert.ToDouble(dato.Rows[0]["Pre_Compra$"]) > 0)
                    {
                        vp_costoDolar.Visible = true;
                        vp_costoDolar.Text = "Costo en Dolar: $ " + Convert.ToDouble(dato.Rows[0]["Pre_Compra$"]).ToString("N2");
                    }
                    else { vp_costoDolar.Visible = false; }


                    vp_margenFrank.Text = "Margen de Ganancia: " + Convert.ToString(dato.Rows[0]["Frank"]) + " %";
                    vp_utilidad.Text = "Ganancia: $ " + Convert.ToDouble(dato.Rows[0]["UtilidadUnit"]).ToString("N2");

                    vp_precioMenor.Text = "Precio Minorista: $ " + Convert.ToDouble(dato.Rows[0]["Pre_vntaxMenor"]).ToString("N2");

                    if (Convert.ToDouble(dato.Rows[0]["Pre_vntaxMayor"]) > 0)
                    {
                        vp_precioMayor.Visible = true;
                        vp_precioMayor.Text = "Precio Mayorista: $ " + Convert.ToDouble(dato.Rows[0]["Pre_vntaxMayor"]).ToString("N2");
                    }
                    else { vp_precioMayor.Visible = false; }

                    if (Convert.ToDouble(dato.Rows[0]["Pre_Vntadolar"]) > 0)
                    {
                        vp_precioDolar.Visible = true;
                        vp_precioDolar.Text = "Precio Dolar: $ " + Convert.ToDouble(dato.Rows[0]["Pre_Vntadolar"]).ToString("N2");
                    }
                    else { vp_precioDolar.Visible = false; }


                    vp_ValorGeneral.Text = "Valor Total del Stock: $ " + Convert.ToDouble(dato.Rows[0]["Valor_porCant"]).ToString("N2");

                    pnl_VistaPrevia.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al cargar el producto.", ex);
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
            Frm_Add_Prod add = new Frm_Add_Prod();

            fil.Show();
            add.modo = "Nuevo";
            add.ShowDialog();
            fil.Hide();

            if (add.Tag.ToString() == "A")
            {
                lsv_productos.Items.Clear();
                lbl_totalItems.Text = "0";
            }
        }

        private async void btn_edit_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para editar.", null);
                return;
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Add_Prod edit = new Frm_Add_Prod();

                var lsv = lsv_productos.SelectedItems[0];
                string id = lsv.SubItems[0].Text;

                fil.Show();
                edit.idProducto = id;
                edit.modo = "Editar";
                edit.ShowDialog();
                fil.Hide();

                //despues que se guarda o cancela la editacion se recarga la tabla
                if (edit.Tag.ToString() == "A")
                {
                    CargarVistaPrevia(id);
                    await Cargar_Prod_PorValorAsync(id);
                }


            }
        }

        private async void btn_remove_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para eliminar.", null);
                return;
            }
            else
            {
                var lsv = lsv_productos.SelectedItems[0];

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar producto";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este producto?";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy borrando el producto...";

                    RN_Producto obj = new RN_Producto();
                    bool ok = await obj.RN_Eliminar(lsv.SubItems[0].Text);

                    if (ok)
                    {

                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El producto se elimino exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_productos.Items.Clear();
                        lbl_totalItems.Text = "0";
                    }

                    pnl_load.Visible = false;

                }
            }
        }



        private void copiarCodigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_productos.SelectedItems[0];

                string dato = lsv.SubItems[0].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void copiarDescripPrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para copiar sus datos.", null);
                return;
            }
            else
            {
                var lsv = lsv_productos.SelectedItems[0];

                string dato = lsv.SubItems[2].Text + " - $ " + lsv.SubItems[8].Text;

                Clipboard.Clear();
                Clipboard.SetText(dato);
            }
        }

        private void btn_cerrarVistaPrevia_Click(object sender, EventArgs e)
        {
            pnl_VistaPrevia.Visible = false;
        }

        private void vp_foto_Click(object sender, EventArgs e)
        {
            vp_fotoGrande.Image = vp_foto.Image;
            pnl_foto.Visible = true;

        }

        private void btn_cerrarFotoGrande_Click(object sender, EventArgs e)
        {
            pnl_foto.Visible = false;
        }

        private void lsv_productos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_vistaPrevia_Click(sender, e);
        }

        private void btn_mas_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(btn_mas, new Point(0, btn_mas.Height));
        }

        private async void btn_baja_Click(object sender, EventArgs e)
        {
            if (lsv_productos.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para dar de baja.", null);
                return;
            }
            else
            {
                var lsv = lsv_productos.SelectedItems[0];
                string idProducto = lsv.SubItems[0].Text;

                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "¿Dar de baja producto?";
                sino.lbl_msm.Text = "El producto no sera visible pero seguira guardado.";
                sino.ShowDialog();


                if (sino.Tag.ToString() == "Si")
                {
                    pnl_load.Visible = true;
                    lbl_load.Text = "Espere... Estoy dando de baja el producto...";

                    RN_Producto obj = new RN_Producto();
                    bool ok = await obj.RN_DarBaja(idProducto);
                    if (ok)
                    {
                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El producto se dio de baja exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        lsv_productos.Items.Clear();
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

        private async void calcularValorTotalStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idProd;
            string tipo;
            int cont = 0;
            RN_Producto obj = new RN_Producto();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy calculando...";

            try
            {
                for (int i = 0; i < lsv_productos.Items.Count; i++)
                {
                    idProd = lsv_productos.Items[i].SubItems[0].Text;
                    tipo = lsv_productos.Items[i].SubItems[11].Text;

                    if (tipo.Trim() == "Producto")
                    {
                        bool ok = await obj.RN_CalcularValorStock(idProd.Trim());

                        if (ok)
                        {
                            cont++;
                        }
                    }
                }

                Frm_Filtro fil = new Frm_Filtro();
                frm_listo listo = new frm_listo();
                fil.Show();
                listo.lbl_title.Text = "¡Listo!";
                listo.lbl_msm.Text = "Se calculo el valor total del stock de " + cont.ToString() + " productos.";
                listo.ShowDialog();
                fil.Hide();

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error", ex);
            }
            finally
            {
                pnl_load.Visible = false;
            }
        }

        private void Frm_Explor_Prod_KeyDown(object sender, KeyEventArgs e)
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

        private async void modificarPreciosGlobalStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Cambiar_Precios pre = new Frm_Cambiar_Precios();

            fil.Show();
            pre.ShowDialog();
            fil.Hide();

            if (pre.Tag.ToString() == "A")
            {
                foreach (ListViewItem selectedItem in lsv_productos.Items)
                {
                    if (selectedItem.Checked || check_selectAll.Checked)
                    {
                        // El checkbox está marcado, puedes trabajar con este elemento.
                        // Acceder a las subcolumnas específicas usando sus índices
                        string frank = selectedItem.SubItems[5].Text;
                        string preCompraS = selectedItem.SubItems[7].Text;
                        string preVntaxMenor = selectedItem.SubItems[8].Text;
                        string preVntadolar = selectedItem.SubItems[12].Text;
                        string preCompraDolar = selectedItem.SubItems[13].Text;

                        bool preCosto = pre.check_precioCosto.Checked;
                        bool preVenta = pre.check_precioVenta.Checked;
                        bool porcentaje = pre.check_Porcentaje.Checked;
                        bool montoFijo = pre.check_montoFijo.Checked;
                        bool actualizar = pre.check_actualizarPrecios.Checked;
                        string valor = pre.txt_precio.Text;



                        //actualizar precios
                        EN_ActualizarPrecios act = new EN_ActualizarPrecios();
                        act.IdPro = selectedItem.SubItems[0].Text;
                        bool ok = false;

                        pnl_load.Visible = true;
                        lbl_load.Text = "Espere... Estoy actualizando los precios...";

                        if (preCosto)
                        {
                            try
                            {
                                if (porcentaje)
                                {
                                    act.PreCompraS = Convert.ToDouble(preCompraS) * (Convert.ToDouble(valor) / 100 + 1);
                                    act.PreCompraS = Math.Round(act.PreCompraS, 2);

                                    if (Convert.ToDouble(preCompraDolar) > 0)
                                    {
                                        act.PreCompraUsd = act.PreCompraS / Convert.ToDouble(precioVentaBlue);
                                        act.PreCompraUsd = Math.Round(act.PreCompraUsd, 2);

                                        ok = true;
                                    }
                                    else
                                    {
                                        act.PreCompraUsd = 0;
                                        ok = true;
                                    }
                                }
                                else if (montoFijo)
                                {
                                    act.PreCompraS = Convert.ToDouble(preCompraS) + Convert.ToDouble(valor);
                                    act.PreCompraS = Math.Round(act.PreCompraS, 2);

                                    if (Convert.ToDouble(preCompraDolar) > 0)
                                    {
                                        act.PreCompraUsd = act.PreCompraS / Convert.ToDouble(precioVentaBlue);
                                        act.PreCompraUsd = Math.Round(act.PreCompraUsd, 2);
                                        ok = true;
                                    }
                                    else
                                    {
                                        act.PreCompraUsd = 0;
                                        ok = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al calcular el precio de costo.", ex);
                                ok = false;
                            }


                            RN_Producto objPro = new RN_Producto();

                            try
                            {
                                if (ok)
                                {

                                    if (actualizar)
                                    {
                                        if (act.PreCompraUsd > 0)
                                        {
                                            act.PreVntaxUsd = act.PreCompraUsd * ((Convert.ToDouble(frank) / 100) + 1);
                                            act.PreVntaxUsd = Math.Round(act.PreVntaxUsd, 2);
                                        }
                                        else
                                        {
                                            act.PreVntaxUsd = 0;
                                        }

                                        act.PreVntaxMenor = act.PreCompraS * ((Convert.ToDouble(frank) / 100) + 1);
                                        act.PreVntaxMenor = Math.Round(act.PreVntaxMenor, 2);

                                        act.Utilidad = act.PreVntaxMenor - act.PreCompraS;
                                        act.Utilidad = Math.Round(act.Utilidad, 2);


                                        bool ok3 = await objPro.RN_Actulizar_Precios_CompraVenta_Producto(act);
                                        
                                    }
                                    else
                                    {
                                        bool ok4 = await objPro.RN_Actulizar_Precios_Compra_Producto(act);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al actualizar el precio costo o la actualizacion.", ex);
                            }
                        }

                        if (preVenta)
                        {
                            try
                            {
                                if (porcentaje)
                                {
                                    act.PreVntaxMenor = Convert.ToDouble(preVntaxMenor) * ((Convert.ToDouble(valor) / 100) + 1);
                                    act.PreVntaxMenor = Math.Round(act.PreVntaxMenor, 2);

                                    if (Convert.ToDouble(preVntadolar) > 0)
                                    {
                                        act.PreVntaxUsd = act.PreVntaxMenor / Convert.ToDouble(precioVentaBlue);
                                        act.PreVntaxUsd = Math.Round(act.PreVntaxUsd, 2);
                                        ok = true;
                                    }
                                    else
                                    {
                                        act.PreVntaxUsd = 0;
                                        ok = true;
                                    }
                                }
                                else if (montoFijo)
                                {
                                    act.PreVntaxMenor = Convert.ToDouble(preVntaxMenor) + Convert.ToDouble(valor);
                                    act.PreVntaxMenor = Math.Round(act.PreVntaxMenor, 2);

                                    if (Convert.ToDouble(preVntadolar) > 0)
                                    {
                                        act.PreVntaxUsd = act.PreVntaxMenor / Convert.ToDouble(precioVentaBlue);
                                        act.PreVntaxUsd = Math.Round(act.PreVntaxUsd, 2);
                                        ok = true;
                                    }
                                    else
                                    {
                                        act.PreVntaxUsd = 0;
                                        ok = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al calcular el precio de venta.", ex);
                                ok = false;
                            }


                            //actualizar en bd
                            RN_Producto objPro = new RN_Producto();

                            try
                            {
                                bool ok4 = false;
                                if (ok)
                                {
                                    ok4 = await objPro.RN_Actulizar_Precios_Venta_Producto(act);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al actualizar el precio de venta.", ex);
                            }


                        }

                        pnl_load.Visible = false;
                    }
                }
            }

        }

        private void lsv_productos_MouseClick(object sender, EventArgs e)
        {
            if (pnl_VistaPrevia.Visible == true)
            {
                pnl_VistaPrevia.Visible = false;
            }
        }

        private void lsv_productos_KeyDown(object sender, KeyEventArgs e)
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

        private void check_selectAll_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if(check_selectAll.Checked)
            {
                lsv_productos.CheckBoxes = false;
            }
            else
            {
                lsv_productos.CheckBoxes = true;
            }
        }
    }
}
