using Onion_Commerce.Productos;
using Onion_Commerce.Proveedores;
using Onion_Commerce.Utilitarios;
using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Syncfusion.XlsIO.Parser.Biff_Records.AutoFilterRecord;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Onion_Commerce.Compras
{
    public partial class Frm_Compras : BaseForm
    {
        public Frm_Compras()
        {
            InitializeComponent();
        }

        private void Frm_Ventana_Ventas_Load(object sender, EventArgs e)
        {
            Configurar_ListView();
            pnl_sinProd.Size = new Size(1236, 550);
            pnl_eliminar.Location = new Point(514, 252);
            pnl_load.Location = new Point(0, 528);

            txt_PK.Text = RN_TipoDoc.RN_NroID(9);
            txt_PK.Enabled = false;
        }

        private void Configurar_ListView()
        {
            var lis = lsv_Det;

            lsv_Det.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.HeaderStyle = ColumnHeaderStyle.None;

            //1211px
            //configurar columnas

            lis.Columns.Add("Codigo", 145, HorizontalAlignment.Left);
            lis.Columns.Add("Descripción del Producto", 388, HorizontalAlignment.Left); //2

            lis.Columns.Add("Cantidad", 83, HorizontalAlignment.Left);
            lis.Columns.Add("", 56, HorizontalAlignment.Left);
            lis.Columns.Add("Precio Unit.", 103, HorizontalAlignment.Right);
            lis.Columns.Add("Importe", 120, HorizontalAlignment.Right);
            lis.Columns.Add("Tipo", 0, HorizontalAlignment.Right);
            lis.Columns.Add("Stock", 0, HorizontalAlignment.Right);
            lis.Columns.Add("Peso", 0, HorizontalAlignment.Right);
            lis.Columns.Add("Frank", 0, HorizontalAlignment.Right);
            lis.Columns.Add("PrecioCostoUsd", 0, HorizontalAlignment.Right);
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_Det.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_Det.Items[i].BackColor = Color.AliceBlue;

                cont++;
            }
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
            WindowState = FormWindowState.Minimized;
        }

        private void Calcular()
        {
            try
            {
                double total = 0;
                for (int i = 0; i < lsv_Det.Items.Count; i++)
                {
                    total += Convert.ToDouble(lsv_Det.Items[i].SubItems[5].Text);
                }

                lbl_TotalPagar.Text = total.ToString("N2");

                //calcular iva
                double iva = total * 0.21;
                lbl_igv.Text = iva.ToString("N2");

                lbl_subtotal.Text = (total - iva).ToString("N2");
            }
            catch (Exception ex)
            {

                ErrorLog.MensajeError("Error al calcular", "Ocurrio un error al calcular el total de la compra", ex);
            }
        }

        private void AgregarProducto(List<dynamic> listaProductos)
        {
            try
            {
                bool ok;

                foreach (var compra in listaProductos)
                {
                    ok = true;

                    if (lsv_Det.Items.Count != 0)
                    {
                        //validar que el producto no se ingrese 2 veces
                        for (int i = 0; i < lsv_Det.Items.Count; i++)
                        {
                            if (lsv_Det.Items[i].Text.Trim() == compra.Id_Pro.ToString().Trim())
                            {
                                double cantidad = Convert.ToDouble(lsv_Det.Items[i].SubItems[2].Text);
                                cantidad += Convert.ToDouble(compra.Cantidad);
                                lsv_Det.Items[i].SubItems[2].Text = cantidad.ToString();
                                lsv_Det.Items[i].SubItems[5].Text = (cantidad * Convert.ToDouble(lsv_Det.Items[i].SubItems[4].Text)).ToString("N2");

                                Calcular();
                                ok = false;
                                ErrorLog.MensajeError("Cantidad sumada", "El producto " + compra.Id_Pro + " ya se encontraba en la lista de compras, se aumento su cantidad.");
                                
                            }
                        }
                    }
                    else
                    {
                        pnl_sinProd.Visible = false;
                    }

                    if (ok)
                    {
                        ListViewItem item = new ListViewItem();
                        item = lsv_Det.Items.Add(compra.Id_Pro);
                        item.SubItems.Add(compra.Descripcion);
                        item.SubItems.Add(compra.Cantidad);
                        if (compra.UndMedida == "Unidad")
                        {
                            item.SubItems.Add("Und.");
                        }
                        else
                        {
                            item.SubItems.Add(compra.UndMedida);
                        }
                        item.SubItems.Add(compra.Precio);
                        item.SubItems.Add(compra.Importe);
                        item.SubItems.Add(compra.Tipo);
                        item.SubItems.Add(compra.Stock);
                        item.SubItems.Add(compra.Peso);
                        item.SubItems.Add(compra.Frank);
                        item.SubItems.Add(compra.PrecioCompraUsd);


                        Calcular();

                        lsv_Det.Focus();
                        lsv_Det.Items[0].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorLog.MensajeError("Error al agregar producto", "Ocurrio un error al agregar el producto al carrito", ex);
            }
        }



        private void btn_Nuevo_buscarProd_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Explor_Prod_Reducido pro = new Frm_Explor_Prod_Reducido();

            fil.Show();
            pro.modo = "compra";
            pro.txt_buscar.Focus();
            pro.ShowDialog();
            fil.Hide();

            if (pro.Tag.ToString() == "A")
            {
                //agregamos el producto al carrito
                //var compra = new
                //{
                //    Id_Pro = pro.idProducto,
                //    Descripcion = pro.marca + " " + pro.descripcion,
                //    Cantidad = pro.cantidad,
                //    Precio = pro.precioCosto,
                //    Importe = pro.importeTotal,
                //    Tipo = pro.tipo,
                //    Stock = pro.stock,
                //    UndMedida = pro.undMedida,
                //    Peso = pro.peso,
                //    Frank = pro.frank,
                //    PrecioUsd = pro.precioCostoUsd
                //};
                AgregarProducto(pro.productos);
            }

            pro.Dispose();
        }

        private void Frm_Compras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btn_Nuevo_buscarProd_Click(sender, e);
            }

            if (e.KeyCode == Keys.Delete)
            {
                bt_Delete_Click(sender, e);
            }
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            btn_Nuevo_buscarProd_Click(sender, e);
        }

        private void bt_editPre_Click(object sender, EventArgs e)
        {
            if (lsv_Det.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para editar su precio");
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Solo_Precio precio = new Frm_Solo_Precio();

                fil.Show();
                precio.txt_precio.Text = lsv_Det.SelectedItems[0].SubItems[4].Text;
                precio.lbl_descripcion.Text = lsv_Det.SelectedItems[0].SubItems[1].Text;
                precio.ShowDialog();
                fil.Hide();

                if (precio.Tag.ToString() == "A")
                {
                    if (precio.check_unit.Checked == true && precio.check_total.Checked == false)
                    {
                        lsv_Det.SelectedItems[0].SubItems[4].Text = precio.txt_precio.Text;
                        lsv_Det.SelectedItems[0].SubItems[5].Text = (Convert.ToDouble(precio.txt_precio.Text) * Convert.ToDouble(lsv_Det.SelectedItems[0].SubItems[2].Text)).ToString("N2");

                    }
                    else if (precio.check_total.Checked == true && precio.check_unit.Checked == false)
                    {
                        lsv_Det.SelectedItems[0].SubItems[5].Text = precio.txt_precio.Text;
                        lsv_Det.SelectedItems[0].SubItems[4].Text = (Convert.ToDouble(precio.txt_precio.Text) / Convert.ToDouble(lsv_Det.SelectedItems[0].SubItems[2].Text)).ToString("N2");
                    }

                    Calcular();
                }

            }
        }

        private void bt_editCant_Click(object sender, EventArgs e)
        {
            if (lsv_Det.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un producto para editar su cantidad");
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Cantidad frm = new Frm_Cantidad();

                fil.Show();

                if (lsv_Det.SelectedItems[0].SubItems[3].Text == "Unidad" || lsv_Det.SelectedItems[0].SubItems[3].Text == "Und.")
                {
                    frm.txt_cantidadEntera.Text = lsv_Det.SelectedItems[0].SubItems[2].Text;
                }
                else
                {
                    frm.txt_cantidad.Text = lsv_Det.SelectedItems[0].SubItems[2].Text;
                }
                frm.lbl_descripcion.Text = lsv_Det.SelectedItems[0].SubItems[1].Text;
                frm.lbl_stock.Text = lsv_Det.SelectedItems[0].SubItems[7].Text;
                frm.undMedida = lsv_Det.SelectedItems[0].SubItems[3].Text;
                frm.ShowDialog();
                fil.Hide();

                if (frm.Tag.ToString() == "A")
                {
                    string cantidad;

                    if (lsv_Det.SelectedItems[0].SubItems[3].Text == "Unidad" || lsv_Det.SelectedItems[0].SubItems[3].Text == "Und.")
                    {
                        cantidad = frm.txt_cantidadEntera.Text;
                    }
                    else
                    {
                        cantidad = frm.txt_cantidad.Text;
                    }

                    lsv_Det.SelectedItems[0].SubItems[2].Text = cantidad;
                    lsv_Det.SelectedItems[0].SubItems[5].Text = (Convert.ToDouble(cantidad) * Convert.ToDouble(lsv_Det.SelectedItems[0].SubItems[4].Text)).ToString("N2");

                    Calcular();
                }

            }
        }


        private async void bt_Delete_Click(object sender, EventArgs e)
        {
            if (lsv_Det.SelectedIndices.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona producto/s para quitar.", null);
                return;
            }
            else
            {
                var lsv = lsv_Det.SelectedItems[0];

                Frm_Filtro fil = new Frm_Filtro();
                frm_delete sino = new frm_delete();
                sino.lbl_title.Text = "Eliminar producto/os";
                sino.lbl_msm.Text = "¿Estas seguro que deseas eliminar este/os producto/s?";
                fil.Show();
                sino.ShowDialog();
                fil.Hide();


                if (sino.Tag.ToString().Trim() == "Si")
                {


                    for (int i = lsv_Det.Items.Count - 1; i >= 0; i--)
                    {
                        if (lsv_Det.Items[i].Selected)
                        {
                            lsv_Det.Items[i].Remove();
                        }
                    }

                    Calcular();

                    pnl_eliminar.Visible = true;
                    await Task.Delay(1000);
                    pnl_eliminar.Visible = false;


                    if (lsv_Det.Items.Count == 0)
                    {
                        pnl_sinProd.Visible = true;
                    }
                }
            }
        }
        #region BUSQUEDA DE PROVEEDORES
        private CancellationTokenSource ctsProveedores = new CancellationTokenSource();

        private async void txt_proveedor_TextChange(object sender, EventArgs e)
        {
            ctsProveedores.Cancel(); // Cancel previous search
            ctsProveedores = new CancellationTokenSource();

            try
            {
                await Task.Delay(1000, ctsProveedores.Token); // Wait for 1 second, but cancel if new input comes

                if (!ctsProveedores.Token.IsCancellationRequested)
                {
                    string busqueda = txt_proveedor.Text;
                    await buscarProv.Buscar(busqueda);
                }
            }
            catch (TaskCanceledException)
            {
                // Handle cancellation if necessary
            }

        }

        private Frm_Explor_Prov_Reducido buscarProv;
        public string idProveedor;

        private void txt_proveedor_Enter(object sender, EventArgs e)
        {

            if (buscarProv == null || buscarProv.IsDisposed)
            {
                buscarProv = new Frm_Explor_Prov_Reducido();

                // Calcular la posición relativa al formulario principal (Form1)
                var formPosition = this.PointToScreen(new Point(0, 0)); // Ubicación absoluta de Form1 en la pantalla
                int x = formPosition.X + 703;
                int y = formPosition.Y + 198;

                buscarProv.StartPosition = FormStartPosition.Manual;
                buscarProv.Location = new Point(x, y); // Establece la ubicación calculada
                buscarProv.TopMost = true; // Asegura que el formulario esté en la parte superior


                buscarProv.FormClosed += BuscarProv_FormClosed; // Subscribe to FormClosed event


                lbl_flecha_Proveedores.Visible = true;
                buscarProv.Show();
                txt_proveedor.Focus();

            }
            //else
            //{
            //    txt_proveedor.Focus();
            //}

        }

        private void BuscarProv_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (buscarProv.Tag?.ToString() == "A")
            {
                txt_proveedor.Text = buscarProv.nombre;
                idProveedor = buscarProv.id;

            }

            lbl_flecha_Proveedores.Visible = false;
        }
        #endregion

        private void txt_proveedor_Click(object sender, EventArgs e)
        {
            txt_proveedor_Enter(sender, e);
        }

        private bool ValidarCompra()
        {
            if (lsv_Det.Items.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "No hay productos en la lista de compras");
                return false;
            }
            if (txt_proveedor.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un proveedor para la compra");
                return false;
            }
            if (txt_nrofisico.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el número de factura de la compra");
                return false;
            }
            if (time_fechaVencimiento.Value == null)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona la fecha de vencimiento de la compra");
                return false;
            }
            if (time_fechaCompra.Value == null)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona la fecha de compra");
                return false;
            }
            if (txt_tipoPago.SelectedIndex == -1)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona el tipo de pago de la compra");
                return false;
            }
            if (txt_tipoDoc.SelectedIndex == -1)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona el tipo de documento de la compra");
                return false;
            }
            if (txt_PK.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el PK de la compra");
                return false;
            }

            return true;
        }

        private async void RegistrarCompra()
        {
            EN_IngresoCompra comp = new EN_IngresoCompra();
            EN_DetalleIngresoCompra det = new EN_DetalleIngresoCompra();
            RN_IngresoCompra obj = new RN_IngresoCompra();
            RN_Producto objPro = new RN_Producto();

            int contador = 0;

            try
            {
                LoadTrue("Registrando compra...");

                comp.IdCom = RN_TipoDoc.RN_NroID(9);
                txt_PK.Text = comp.IdCom;

                comp.Nro_Fac_Fisico = txt_nrofisico.Text;
                comp.IdProvee = idProveedor;
                comp.SubTotal_Com = Convert.ToDouble(lbl_subtotal.Text);
                comp.FechaIngre = time_fechaCompra.Value;
                comp.TotalCompra = Convert.ToDouble(lbl_TotalPagar.Text);
                comp.IdUsu = Convert.ToInt32(Cls_Libreria.IdUsu);
                comp.ModalidadPago = txt_tipoPago.Text;
                comp.TiempoEspera = 0;
                comp.FechaVence = time_fechaVencimiento.Value;
                comp.EstadoIngre = "Activo";
                comp.RecibiConforme = check_conforme.Checked;
                comp.Datos_Adicional = txt_obs.Text;
                comp.Tipo_Doc_Compra = txt_tipoDoc.Text;

                bool ok = await obj.RN_Registrar(comp);
                if (ok)
                {
                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(9);

                    //guardar detalle
                    for (int i = 0; i < lsv_Det.Items.Count; i++)
                    {
                        var item = lsv_Det.Items[i];

                        det.Id_ingreso = txt_PK.Text;
                        det.Id_Pro = item.SubItems[0].Text;
                        det.Precio = Convert.ToDouble(item.SubItems[4].Text);
                        det.Cantidad = Convert.ToDouble(item.SubItems[2].Text);
                        det.Importe = Convert.ToDouble(item.SubItems[5].Text);

                        LoadTrue("Registrando detalle...");
                        bool ok2 = await obj.RN_RegistrarDetalle(det);
                        if (ok2)
                        {
                            RegistrarMovKardex(det.Id_Pro.Trim(), det.Cantidad, det.Precio);

                            //actualizar precios
                            EN_ActualizarPrecios act = new EN_ActualizarPrecios();
                            act.IdPro = det.Id_Pro.Trim();
                            act.PreCompraS = det.Precio;

                            double Frank = Convert.ToDouble(item.SubItems[9].Text);
                            act.PreCompraUsd = Convert.ToDouble(item.SubItems[10].Text);

                            if (act.PreCompraUsd > 0)
                            {
                                double valorBlue = CargarPrecioDolar();
                                act.PreCompraUsd = det.Precio / valorBlue;
                                act.PreCompraUsd = Math.Round(act.PreCompraUsd, 2);
                            }
                            else
                            {
                                act.PreCompraUsd = 0;
                            }

                            if (check_actualizarPrecios.Checked == true)
                            {
                                LoadTrue("Actualizando Precios...");

                                if (act.PreCompraUsd > 0)
                                {
                                    act.PreVntaxUsd = act.PreCompraUsd * ((Frank / 100) + 1);
                                    act.PreVntaxUsd = Math.Round(act.PreVntaxUsd, 2);
                                }
                                else
                                {
                                    act.PreVntaxUsd = 0;
                                }

                                act.PreVntaxMenor = det.Precio * ((Frank / 100) + 1);
                                act.PreVntaxMenor = Math.Round(act.PreVntaxMenor, 2);
                                act.Utilidad = act.PreVntaxMenor - det.Precio;
                                act.Utilidad = Math.Round(act.Utilidad, 2);

                                bool ok3 = await objPro.RN_Actulizar_Precios_CompraVenta_Producto(act);
                                if (ok3)
                                {
                                    RN_Producto objP = new RN_Producto();
                                    bool ok4 = await objP.RN_CalcularValorStock(det.Id_Pro);

                                    if (ok4) contador++;
                                }

                            }
                            else
                            {
                                bool ok4 = await objPro.RN_Actulizar_Precios_Compra_Producto(act);
                                if (ok4) contador++;
                            }


                        }
                    }
                }

                if (contador == lsv_Det.Items.Count)
                {
                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "La compra se proceso correctamente.";
                    listo.ShowDialog();
                    fil.Hide();
                }
                else
                {
                    ErrorLog.MensajeError("Advertencia", "Algunos Productos no se actualizaron bien.");
                }

                LimpiarForm();

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("Error al registrar compra", "Ocurrio un error al registrar la compra", ex);
            }
            finally
            {
                LoadFalse();
            }
        }

        private async void LimpiarForm()
        {
            lsv_Det.Items.Clear();
            txt_proveedor.Text = "";
            txt_nrofisico.Text = "";
            time_fechaCompra.Value = DateTime.Now;
            time_fechaVencimiento.Value = DateTime.Now;
            txt_tipoPago.SelectedIndex = -1;
            txt_obs.Text = "";
            txt_tipoDoc.SelectedIndex = -1;
            lbl_TotalPagar.Text = "0.00";
            lbl_igv.Text = "0.00";
            lbl_subtotal.Text = "0.00";
            txt_PK.Text = RN_TipoDoc.RN_NroID(9);

            await Task.Delay(1500);

            if (lsv_Det.Items.Count == 0)
            {
                pnl_sinProd.Visible = true;
            }
        }


        #region PRECIO DOLAR

        private double CargarPrecioDolar()
        {
            if (Convert.ToDouble(Frm_Principal.precioVentaBlue) > 0)
            {
                return Convert.ToDouble(Frm_Principal.precioVentaBlue);
            }

            return 0;
        }



        #endregion

        private async void RegistrarMovKardex(string id, double cantidad, double precioCompra)
        {
            RN_Kardex obj = new RN_Kardex();
            EN_Kardex kardex = new EN_Kardex();
            RN_Producto objProd = new RN_Producto();
            DataTable data = new DataTable();
            DataTable dataProd = new DataTable();

            string idKardex = "";
            int xitem = 0;
            double stockProd = 0;
            double precioCompraProd = 0;

            try
            {
                LoadTrue("Registrando kardex...");

                if (obj.RN_Verificar_Producto_siTieneKardex(id.Trim()) == true)
                {
                    //si tiene kardex
                    data = await Task.Run(() => obj.RN_Listar_PorValor(id.Trim()));
                    if (data.Rows.Count > 0)
                    {
                        idKardex = data.Rows[0]["Id_krdx"].ToString();
                        xitem = data.Rows.Count;

                        //leemos datos de prod
                        dataProd = await Task.Run(() => objProd.RN_Listar_PorValor(id.Trim()));
                        stockProd = Convert.ToDouble(dataProd.Rows[0]["Stock_Actual"]);
                        precioCompraProd = Convert.ToDouble(dataProd.Rows[0]["Pre_CompraS"]);

                        //registrar kardex
                        kardex.Id_KrDx = idKardex;
                        kardex.Item = xitem + 1;
                        kardex.Doc_Soport = txt_nrofisico.Text;
                        kardex.Det_Operacion = "Compra de productos";
                        //entradas
                        kardex.Cantidad_In = cantidad;
                        kardex.Precio_Unt_In = precioCompra;
                        kardex.Costo_Total_In = cantidad * precioCompra;
                        //salidas
                        kardex.Cantidad_Out = 0;
                        kardex.Precio_Unt_Out = 0;
                        kardex.Importe_Total_Out = 0;
                        //saldos
                        kardex.Cantidad_Saldo = stockProd + cantidad;
                        kardex.Promedio = (stockProd * precioCompraProd + cantidad * precioCompra) / (stockProd + cantidad);
                        kardex.Costo_Total_Saldo = kardex.Cantidad_Saldo * kardex.Promedio;

                        bool ok = await obj.RN_RegistrarDetalle(kardex);

                        if (ok)
                        {
                            LoadTrue("Actualizando stock...");
                            //actualizamos stock de la tabla prod
                            bool ok2 = await objProd.RN_SumarStock(id.Trim(), cantidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("Error al registrar kardex", "Ocurrio un error al registrar el kardex", ex);
            }
            finally
            {
                LoadFalse();
            }
        }

        private void LoadTrue(string msg)
        {

            pnl_load.Visible = true;
            lbl_load.Text = msg;

            //deshabilitar
            pnl_importes.Enabled = false;
            gru_det.Enabled = false;
            txt_proveedor.Enabled = false;
            txt_tipoDoc.Enabled = false;
            txt_nrofisico.Enabled = false;
            time_fechaCompra.Enabled = false;
            time_fechaVencimiento.Enabled = false;
            txt_tipoPago.Enabled = false;
            txt_obs.Enabled = false; //11
        }

        private void LoadFalse()
        {
            pnl_load.Visible = false;

            pnl_importes.Enabled = true;
            gru_det.Enabled = true;
            txt_proveedor.Enabled = true;
            txt_tipoDoc.Enabled = true;
            txt_nrofisico.Enabled = true;
            time_fechaCompra.Enabled = true;
            time_fechaVencimiento.Enabled = true;
            txt_tipoPago.Enabled = true;
            txt_obs.Enabled = true;
        }

        private void btn_procesar_Click(object sender, EventArgs e)
        {
            if (ValidarCompra() == true)
            {
                RegistrarCompra();
            }
        }
    }
}
