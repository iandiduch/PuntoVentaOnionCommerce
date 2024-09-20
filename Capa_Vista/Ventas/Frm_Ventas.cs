using Onion_Commerce.Cliente;
using Onion_Commerce.Informe;
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

namespace Onion_Commerce.Ventas
{
    public partial class Frm_Ventas : BaseForm
    {
        public Frm_Ventas()
        {
            InitializeComponent();
        }

        private void Frm_Ventana_Ventas_Load(object sender, EventArgs e)
        {

            Configurar_ListView();
            pnl_sinProd.Size = new Size(1236, 550);
            pnl_eliminar.Location = new Point(514, 252);
            pnl_load.Location = new Point(0, 528);

            txt_nroOp.Enabled = false;
        }

        private string GenerarCodigoProd()
        {
            RN_Cotizacion obj = new RN_Cotizacion();
            string codigo;
            bool existe;

            do
            {
                codigo = RN_TipoDoc.RN_NroID(10);
                existe = obj.RN_Buscar_porID(codigo);

                if (existe)
                {
                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo_Producto(10);
                }

            } while (existe);

            return codigo;
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
            lis.Columns.Add("Utilidad", 0, HorizontalAlignment.Right);
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

        private string ganancia;
        private void Calcular()
        {
            try
            {
                double total = 0;
                double cant = 0;
                double ganan = 0;
                for (int i = 0; i < lsv_Det.Items.Count; i++)
                {
                    total += Convert.ToDouble(lsv_Det.Items[i].SubItems[5].Text);
                    cant += Convert.ToDouble(lsv_Det.Items[i].SubItems[2].Text);
                    ganan += Convert.ToDouble(lsv_Det.Items[i].SubItems[11].Text) * Convert.ToDouble(lsv_Det.Items[i].SubItems[2].Text);
                }
                lbl_cantTotal.Text = cant.ToString("N2");

                lbl_TotalPagar.Text = total.ToString("N2");

                lbl_son.Text = Numalet.ToString(total.ToString());

                ganancia = ganan.ToString("N2");


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
                        item.SubItems.Add(compra.PrecioVentaUSD);
                        item.SubItems.Add(compra.UtilidadUnit);


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
            pro.modo = "cotizacion";
            pro.ShowDialog();
            pro.txt_buscar.Focus();
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

            if (e.KeyCode == Keys.F2)
            {
                txt_tipoPago.Focus();
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
                Compras.Frm_Solo_Precio precio = new Compras.Frm_Solo_Precio();

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
                Compras.Frm_Cantidad frm = new Compras.Frm_Cantidad();

                fil.Show();

                if (lsv_Det.SelectedItems[0].SubItems[3].Text == "Unidad" || lsv_Det.SelectedItems[0].SubItems[3].Text == "Und.")
                {
                    frm.txt_cantidadEntera.Text = lsv_Det.SelectedItems[0].SubItems[2].Text;
                }
                else
                {
                    frm.txt_cantidad.Text = lsv_Det.SelectedItems[0].SubItems[2].Text;
                }
                frm.modo = "venta";
                frm.tipoProd = lsv_Det.SelectedItems[0].SubItems[6].Text;
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
                sino.lbl_title.Text = "Eliminar producto/s";
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
        #region BUSQUEDA DE Clientes

        public string idCliente;

        private void txt_proveedor_Enter(object sender, EventArgs e)
        {
            Frm_Explor_Cliente_Reducido buscarCliente = new Frm_Explor_Cliente_Reducido();

            buscarCliente.StartPosition = FormStartPosition.CenterParent;
            buscarCliente.TopMost = true; // Asegura que el formulario esté en la parte superior

            Frm_Filtro fil = new Frm_Filtro();
            fil.Show();
            buscarCliente.ShowDialog();
            buscarCliente.txt_buscar.Focus();
            fil.Hide();

            if (buscarCliente.Tag.ToString() == "A")
            {
                txt_cliente.Text = buscarCliente.nombre;
                idCliente = buscarCliente.id;
                txt_cuit.Text = buscarCliente.cuit;
                txt_dire.Text = buscarCliente.direccion;
            }
        }

        #endregion

        private bool ValidarCompra()
        {
            if (lsv_Det.Items.Count == 0)
            {
                ErrorLog.MensajeError("¡Advertencia!", "No hay productos en la lista.");
                return false;
            }
            if (txt_cliente.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un cliente o ingresa 0");
                return false;
            }
            if (txt_tipoPago.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Selecciona un tipo de pago.");
                return false;
            }
            if (txt_monto.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el monto de pago.");
                return false;
            }

            return true;
        }

        private string idPedido;
        private async Task<bool> GuardarPedido()
        {
            RN_Pedido obj = new RN_Pedido();
            EN_Pedido en = new EN_Pedido();
            EN_DetallePedido det = new EN_DetallePedido();

            try
            {
                LoadTrue("Registrando pedido...");

                en.IdPed = RN_TipoDoc.RN_NroID(11);
                idPedido = en.IdPed;

                en.IdCliente = idCliente;
                en.SubTotal = Convert.ToDouble(lbl_subtotal.Text);
                en.IgvPed = Convert.ToDouble(lbl_igv.Text);
                en.TotalPed = Convert.ToDouble(lbl_TotalPagar.Text);
                en.IdUsu = Convert.ToInt32(Cls_Libreria.IdUsu);
                en.TotalGancia = Convert.ToDouble(ganancia);

                bool ok = await obj.RN_Registrar(en);
                if (ok)
                {
                    int cont = 0;
                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(11);

                    //guardar detalle
                    det.IdPed = idPedido;

                    for (int i = 0; i < lsv_Det.Items.Count; i++)
                    {
                        var item = lsv_Det.Items[i];

                        det.IdPro = item.SubItems[0].Text;
                        det.Precio = Convert.ToDouble(item.SubItems[4].Text);
                        det.Cantidad = Convert.ToDouble(item.SubItems[2].Text);
                        det.Importe = Convert.ToDouble(item.SubItems[5].Text);
                        det.TipoProd = item.SubItems[6].Text;
                        det.UndMedida = item.SubItems[3].Text;
                        det.UtilidadUnit = Convert.ToDouble(item.SubItems[11].Text);
                        det.TotalUtilidad = det.UtilidadUnit * det.Cantidad;

                        bool ok2 = await obj.RN_RegistrarDetalle(det);
                        if (ok2)
                        {
                            cont++;
                        }
                    }

                    if (cont != lsv_Det.Items.Count)
                    {
                        return false;
                        throw new ArgumentException("Error al guardar detalle de pedido.");
                    }

                    return true;

                }
                else return false; throw new ArgumentException("Error al guardar pedido.");
            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("Error al guardar pedido", "Ocurrio un error al guardar el pedido", ex);
                return false;
            }
            finally
            {
                LoadFalse();
            }
        }

        private async void RegistrarVenta()
        {
            EN_Cotizacion en = new EN_Cotizacion();
            RN_Cotizacion obj = new RN_Cotizacion();

            try
            {
                //guardar pedido
                //bool ok = await GuardarPedido();
                //if (ok)
                //{
                //    LoadTrue("Registrando cotización...");

                //    en.IdCotiza = RN_TipoDoc.RN_NroID(10);
                //    txt_nroOp.Text = en.IdCotiza;

                //    en.IdPed = idPedido;

                //    en.FechaCoti = time_fechaCompra.Value;
                //    en.TotalCotiza = Convert.ToDouble(lbl_TotalPagar.Text);
                //    en.PrecioconIgv = check_fiscal.Checked ? "Si" : "No";
                //    en.Condiciones = txt_dire.Text;

                //    bool ok2 = await obj.RN_Registrar(en);
                //    if (ok2)
                //    {
                //        RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(10);

                //        Frm_Filtro fil = new Frm_Filtro();
                //        frm_listo listo = new frm_listo();
                //        fil.Show();
                //        listo.lbl_title.Text = "¡Listo!";
                //        listo.lbl_msm.Text = "La cotizacion se proceso correctamente.";
                //        listo.ShowDialog();

                //        //imprimir
                //        Frm_Print print = new Frm_Print();
                //        print.id = en.IdCotiza;
                //        if(check_fiscal.Checked == true)
                //        {
                //            print.tipo = "cotizacion";
                //        }
                //        else
                //        {
                //            print.tipo = "cotizacionSinIVA";
                //        }
                //        print.ShowDialog();
                //        fil.Hide();

                //    } else throw new ArgumentException("Error al registrar cotizacion.");
                //}

                LimpiarForm();
            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("Error al registrar cotizacion", "Ocurrio un error al registrar la cotizacion", ex);
            }
            finally
            {
                LoadFalse();
            }
        }

        private async void LimpiarForm()
        {
            lsv_Det.Items.Clear();
            txt_cliente.Text = "0";
            time_fechaCompra.Value = DateTime.Now;
            txt_dire.Text = "";
            lbl_TotalPagar.Text = "0,00";
            lbl_igv.Text = "0,00";
            lbl_subtotal.Text = "0,00";
            lbl_vuelto.Text = "0,00";
            lbl_cantTotal.Text = "0";
            txt_monto.Text = "";
            txt_tipoPago.Text = "";
            txt_cuit.Text = "";
            txt_nroOp.Text = "";

            txt_cliente.Focus();


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



        private void LoadTrue(string msg)
        {

            pnl_load.Visible = true;
            lbl_load.Text = msg;

            //deshabilitar
            pnl_importes.Enabled = false;
            gru_det.Enabled = false;
            txt_cliente.Enabled = false;
            time_fechaCompra.Enabled = false;
            txt_dire.Enabled = false; //11
        }

        private void LoadFalse()
        {
            pnl_load.Visible = false;

            pnl_importes.Enabled = true;
            gru_det.Enabled = true;
            txt_cliente.Enabled = true;
            time_fechaCompra.Enabled = true;
            txt_dire.Enabled = true;
        }

        private void btn_procesar_Click(object sender, EventArgs e)
        {
            if (ValidarCompra() == true)
            {
                RegistrarVenta();
            }
            else
            {
                txt_tipoPago.Focus();
            }
        }

        private void txt_cliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
            {
                txt_cliente.Text = "No requerido";
                idCliente = "0";
                txt_dire.Text = "No requerido";
                txt_cuit.Text = "00000000000";
            }
            else
            {
                txt_proveedor_Enter(sender, e);
            }
        }

        private void txt_monto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_procesar_Click(sender, e);
            }
        }

        private void txt_tipoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_monto.Focus();
            }
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void txt_monto_TextChange(object sender, EventArgs e)
        {
            if (txt_monto.Text.Length > 0)
            {
                cts.Cancel(); // Cancel previous search
                cts = new CancellationTokenSource();

                try
                {
                    await Task.Delay(100, cts.Token); // Wait for 1 second, but cancel if new input comes

                    if (!cts.Token.IsCancellationRequested)
                    {
                        string valor = txt_monto.Text;

                        if (txt_tipoPago.Text == "Efectivo")
                        {
                            if (txt_monto.Text.Trim() != "")
                            {
                                if (Convert.ToDouble(txt_monto.Text) > 0)
                                {
                                    double vuelto = Convert.ToDouble(valor) - Convert.ToDouble(lbl_TotalPagar.Text);
                                    lbl_vuelto.Text = vuelto.ToString("N2");
                                }
                            }

                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle cancellation if necessary
                }
            }


        }
    }
}
