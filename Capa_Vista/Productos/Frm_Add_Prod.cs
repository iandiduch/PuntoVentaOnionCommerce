using Bunifu.Framework.Lib;
using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using Onion_Commerce.Proveedores;
using Onion_Commerce.Utilitarios;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Onion_Commerce.Productos
{
    public partial class Frm_Add_Prod : BaseForm
    {
        public Frm_Add_Prod()
        {
            InitializeComponent();
        }
        bool okDolar;
        public string idProducto;
        public string modo;

        private async void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            img_fotologo.Image = Properties.Resources.producto_sin_imagen;

            okDolar = CargarPrecioDolar();
            if (!okDolar)
            {
                pnl_dolarHoy.Visible = false;
                check_CompraDolar.Enabled = false;
            }

            if (modo.Trim() == "Nuevo")
            {
                txt_id.Text = GenerarCodigoProd();
                lbl_title.Text = "Registro de Producto";
            }
            else if (modo.Trim() == "Editar")
            {
                lbl_title.Text = "Modificación de Producto";
                _actualizando = true;
                await Buscar_Producto(idProducto);
            }
        }

        private async Task Buscar_Producto(string idProducto)
        {
            RN_Producto obj = new RN_Producto();
            DataTable dato = new DataTable();

            try
            {
                //para cancelar los eventos textchange de los textboxs
                _actualizando = true;

                dato = await Task.Run(() => obj.RN_Listar_PorValor(idProducto));
                if (dato.Rows.Count > 0)
                {
                    idProveedor = Convert.ToString(dato.Rows[0]["IDPROVEE"]);
                    idCategoria = Convert.ToString(dato.Rows[0]["Id_Cat"]);
                    idMarca = Convert.ToString(dato.Rows[0]["Id_Marca"]);

                    txt_id.Text = Convert.ToString(dato.Rows[0]["Id_Pro"]);
                    txt_nom.Text = Convert.ToString(dato.Rows[0]["Descripcion_Larga"]);
                    txt_marca.Text = Convert.ToString(dato.Rows[0]["Marca"]);
                    txt_proveedor.Text = Convert.ToString(dato.Rows[0]["NOMBRE"]);
                    txt_cate.Text = Convert.ToString(dato.Rows[0]["Categoria"]);
                    txt_tipo.Text = Convert.ToString(dato.Rows[0]["TipoProdcto"]);
                    txt_unidadMedida.Text = Convert.ToString(dato.Rows[0]["UndMedida"]);
                    txt_pesounit.Text = Convert.ToString(dato.Rows[0]["PesoUnit"]);


                    txt_precioCompraDolar.Text = Convert.ToString(dato.Rows[0]["Pre_Compra$"]);
                    if (Convert.ToDouble(dato.Rows[0]["Pre_Compra$"]) < 1)
                    {
                        check_CompraDolar.Checked = false;
                    }
                    else { check_CompraDolar.Checked = true; }

                    txt_precioCompra.Text = Convert.ToString(dato.Rows[0]["Pre_CompraS"]);

                    txt_frank.Text = Convert.ToString(dato.Rows[0]["Frank"]);
                    txt_utilidad.Text = Convert.ToString(dato.Rows[0]["UtilidadUnit"]);

                    txt_ventaMenor.Text = Convert.ToString(dato.Rows[0]["Pre_vntaxMenor"]);
                    txt_ventaMayor.Text = Convert.ToString(dato.Rows[0]["Pre_vntaxMayor"]);
                    txt_VentaDolar.Text = Convert.ToString(dato.Rows[0]["Pre_Vntadolar"]);


                    string xFotoRuta = Convert.ToString(dato.Rows[0]["Foto"]);

                    // Verifica si el archivo especificado existe
                    if (System.IO.File.Exists(xFotoRuta))
                    {
                        img_fotologo.Load(xFotoRuta); // Carga la imagen si existe
                    }
                    else
                    {
                        img_fotologo.Image = Properties.Resources.producto_sin_imagen; // Carga la imagen por defecto si no existe
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al cargar el producto, vuelve atras.", ex);
            }
            finally
            {
                _actualizando = false;
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
            this.Tag = "";
            this.Close();
        }

        string xFotoRuta;
        private void lbl_examinar_Click(object sender, EventArgs e)
        {
            var FilePath = string.Empty;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    xFotoRuta = openFileDialog1.FileName;
                    img_fotologo.Load(xFotoRuta);
                }
            }
            catch (Exception ex)
            {
                img_fotologo.Image = Properties.Resources.producto_sin_imagen;
                xFotoRuta = "-";
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error al guardar la imagen", ex);
            }
        }

        private void img_fotologo_Click(object sender, EventArgs e)
        {
            lbl_examinar_Click(sender, e);
        }

        private async void Registrar_Prod()
        {
            RN_Producto obj = new RN_Producto();
            EN_Producto eN = new EN_Producto();

            try
            {
                pnl_load.Visible = true;
                lbl_load.Text = "Espere... Estoy guardando el producto...";


                eN.Id = GenerarCodigoProd();
                txt_id.Text = eN.Id;

                eN.Idproveedor = idProveedor;
                eN.Descripcion = txt_nom.Text;
                eN.Frank = Convert.ToDouble(txt_frank.Text);
                eN.Precompra_pesos = Convert.ToDouble(txt_precioCompra.Text);
                eN.Precompra_dlls = Convert.ToDouble(txt_precioCompraDolar.Text);
                eN.Stock_actual = 0;
                eN.Id_cat = Convert.ToInt32(idCategoria);
                if (idMarca == null)
                {
                    eN.Id_marca = 0;
                }
                else
                {
                    eN.Id_marca = Convert.ToInt32(idMarca);
                }

                if (xFotoRuta.Trim().Length < 5)
                {
                    eN.Foto = "-";
                }
                else
                {
                    eN.Foto = xFotoRuta;
                }
                eN.Preventa_menor = Convert.ToDouble(txt_ventaMenor.Text);
                eN.Preventa_mayor = 0;
                eN.Preventa_dolar = Convert.ToDouble(txt_VentaDolar.Text);
                eN.Unidadmedida = txt_unidadMedida.Text;
                eN.Pesounit = Convert.ToDouble(txt_pesounit.Text);
                eN.Utilidad = Convert.ToDouble(txt_utilidad.Text);
                eN.Tipoproducto = txt_tipo.Text;
                eN.Valor_general = 0;

                bool ok = await obj.RN_Registrar(eN);

                if (ok)
                {
                    //para registro de kardex
                    bool ok2;

                    if (txt_tipo.SelectedIndex == 0) //solo se hace el kardex para el tipo "Producto"
                    {
                        ok2 = await RegistrarKardex(txt_id.Text);
                    }
                    else
                    {
                        ok2 = true; //Si el tipo no es "Producto" no se hace el kardex y se pasa al mensaje de guardado exitoso.
                    }

                    if (eN.Id == RN_TipoDoc.RN_NroID(4)) RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo_Producto(4);

                    if (ok2)
                    {
                        pnl_load.Visible = false;

                        Frm_Filtro fil = new Frm_Filtro();
                        frm_listo listo = new frm_listo();
                        fil.Show();
                        listo.lbl_title.Text = "¡Listo!";
                        listo.lbl_msm.Text = "El producto se guardo exitosamente.";
                        listo.ShowDialog();
                        fil.Hide();

                        LimpiarForm();

                        this.Tag = "A";
                        this.Close();
                    }
                    else
                    {
                        ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al registrar el kardex", null);
                    }

                }
                else
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el producto", null);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el producto", ex);
            }

            pnl_load.Visible = false;
        }

        private async void Editar_Prod()
        {
            RN_Producto obj = new RN_Producto();
            EN_Producto eN = new EN_Producto();

            try
            {
                pnl_load.Visible = true;
                lbl_load.Text = "Espere... Estoy guardando el producto...";


                eN.Id = idProducto;
                string nuevoIdProducto = txt_id.Text;
                eN.Idproveedor = idProveedor;
                eN.Descripcion = txt_nom.Text;
                eN.Frank = Convert.ToDouble(txt_frank.Text);
                eN.Precompra_pesos = Convert.ToDouble(txt_precioCompra.Text);
                eN.Precompra_dlls = Convert.ToDouble(txt_precioCompraDolar.Text);
                eN.Id_cat = Convert.ToInt32(idCategoria);
                if (idMarca == null)
                {
                    eN.Id_marca = 0;
                }
                else
                {
                    eN.Id_marca = Convert.ToInt32(idMarca);
                }

                if (xFotoRuta.Trim().Length < 5)
                {
                    eN.Foto = "-";
                }
                else
                {
                    eN.Foto = xFotoRuta;
                }
                eN.Preventa_menor = Convert.ToDouble(txt_ventaMenor.Text);
                eN.Preventa_mayor = 0;
                eN.Preventa_dolar = Convert.ToDouble(txt_VentaDolar.Text);
                eN.Unidadmedida = txt_unidadMedida.Text;
                eN.Pesounit = Convert.ToDouble(txt_pesounit.Text);
                eN.Utilidad = Convert.ToDouble(txt_utilidad.Text);
                eN.Tipoproducto = txt_tipo.Text;

                bool ok = await obj.RN_Editar(eN, nuevoIdProducto);

                if (ok)
                {
                    if (nuevoIdProducto == RN_TipoDoc.RN_NroID(4)) { RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo_Producto(4); }

                    pnl_load.Visible = false;

                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "El producto se modifico exitosamente.";
                    listo.ShowDialog();
                    fil.Hide();

                    LimpiarForm();

                    this.Tag = "A";
                    this.Close();


                }
                else
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el producto", null);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el producto", ex);
            }

            pnl_load.Visible = false;
        }


        

        private async Task<bool> RegistrarKardex(string idProd)
        {
            RN_Kardex obj = new RN_Kardex();
            EN_Kardex en = new EN_Kardex();

            lbl_load.Text = "Espere... Estoy registrando el kardex...";

            try
            {
                if (obj.RN_Verificar_Producto_siTieneKardex(idProd) == true)
                {
                    return true;
                }
                else
                {
                    string idKardex = RN_TipoDoc.RN_NroID(6);

                    bool ok = await obj.RN_Registrar(idKardex, idProd, idProveedor);
                    if (ok == true)
                    {
                        //actualizar sig nro correlativo
                        RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(6);

                        //ahora con el detalle
                        en.Id_KrDx = idKardex;
                        en.Item = 1;
                        en.Doc_Soport = "000";
                        en.Det_Operacion = "Inicio de Kardex";

                        //entradas
                        en.Cantidad_In = 0;
                        en.Precio_Unt_In = 0;
                        en.Costo_Total_In = 0;

                        //salidas
                        en.Cantidad_Out = 0;
                        en.Precio_Unt_Out = 0;
                        en.Importe_Total_Out = 0;

                        //saldos
                        en.Cantidad_Saldo = 0;
                        en.Promedio = 0;
                        en.Costo_Total_Saldo = 0;

                        bool ok2 = await obj.RN_RegistrarDetalle(en);

                        if (ok2 == true) { return true; } else { return false; }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al registrar el kardex", ex);
                return false;
            }


        }

        private void LimpiarForm()
        {
            try
            {
                _actualizando = true;

                idProveedor = null;
                idCategoria = null;
                idMarca = null;

                txt_id.Text = "";
                txt_nom.Text = "";
                txt_marca.Text = "";
                txt_proveedor.Text = "";
                txt_cate.Text = "";
                txt_tipo.Text = "";
                txt_unidadMedida.Text = "";
                txt_pesounit.Text = "";

                txt_precioCompra.Text = "";
                txt_precioCompraDolar.Text = "";
                check_CompraDolar.Checked = false;

                txt_frank.Text = "";
                txt_utilidad.Text = "";

                txt_ventaMenor.Text = "";
                txt_ventaMayor.Text = "";
                txt_VentaDolar.Text = "";

                img_fotologo.Image = Properties.Resources.producto_sin_imagen;
            }
            finally
            {
                _actualizando = false;
            }

        }

        private void btn_listo_Click(object sender, EventArgs e)
        {
            if (Validar_Txt() == true)
            {
                if (modo == "Nuevo")
                {
                    Registrar_Prod();
                }
                else if (modo == "Editar")
                {
                    Editar_Prod();
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }


        #region BUSQUEDA DE PROVEEDORES

        private CancellationTokenSource ctsProveedores = new CancellationTokenSource();

        private async void txt_proveedor_TextChange(object sender, EventArgs e)
        {
            if (_actualizando == true) return;

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

        //private void txt_proveedor_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (buscarProv == null || buscarProv.IsDisposed)
        //    {
        //        buscarProv = new Frm_Explor_Prov_Reducido();

        //        // Calcular la posición relativa al formulario principal (Form1)
        //        var formPosition = this.PointToScreen(new Point(0, 0)); // Ubicación absoluta de Form1 en la pantalla
        //        int x = formPosition.X + 230;
        //        int y = formPosition.Y + 216;

        //        buscarProv.StartPosition = FormStartPosition.Manual;
        //        buscarProv.Location = new Point(x, y); // Establece la ubicación calculada
        //        buscarProv.TopMost = true; // Asegura que el formulario esté en la parte superior


        //        buscarProv.FormClosed += BuscarProv_FormClosed; // Subscribe to FormClosed event

        //        buscarProv.Show();
        //        txt_proveedor.Focus();
        //    }
        //    else
        //    {
        //        txt_proveedor.Focus();
        //    }

        //}

        private void txt_proveedor_Enter(object sender, EventArgs e)
        {
            if (buscarProv == null || buscarProv.IsDisposed)
            {
                buscarProv = new Frm_Explor_Prov_Reducido();

                // Calcular la posición relativa al formulario principal (Form1)
                var formPosition = this.PointToScreen(new Point(0, 0)); // Ubicación absoluta de Form1 en la pantalla
                int x = formPosition.X + 204;
                int y = formPosition.Y + 188;

                buscarProv.StartPosition = FormStartPosition.Manual;
                buscarProv.Location = new Point(x, y); // Establece la ubicación calculada
                buscarProv.TopMost = true; // Asegura que el formulario esté en la parte superior


                buscarProv.FormClosed += BuscarProv_FormClosed; // Subscribe to FormClosed event

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
        }

        #endregion FIN BUSQUEDA DE PROVEEDORES

        #region BUSQUEDA DE CATEGORIA


        private Frm_Explor_CatMar_Reducido buscarCategoria;
        public string idCategoria;

        private void txt_cate_MouseClick(object sender, MouseEventArgs e)
        {
            if (buscarCategoria == null || buscarCategoria.IsDisposed)
            {
                buscarCategoria = new Frm_Explor_CatMar_Reducido();

                // Calcular la posición relativa al formulario principal (Form1)
                var formPosition = this.PointToScreen(new Point(0, 0)); // Ubicación absoluta de Form1 en la pantalla
                int x = formPosition.X + 27;
                int y = formPosition.Y + 236;

                buscarCategoria.StartPosition = FormStartPosition.Manual;
                buscarCategoria.Location = new Point(x, y); // Establece la ubicación calculada
                buscarCategoria.TopMost = true; // Asegura que el formulario esté en la parte superior


                buscarCategoria.FormClosed += buscarCategoria_FormClosed; // Subscribe to FormClosed event

                buscarCategoria.modo = "Categoria";

                buscarCategoria.Show();

                txt_cate.Focus();
            }
            else
            {
                txt_cate.Focus();
            }

        }

        private void buscarCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (buscarCategoria.Tag?.ToString() == "A")
            {
                txt_cate.Text = buscarCategoria.nombre;
                idCategoria = buscarCategoria.id;
            }
        }

        private void txt_cate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion FIN BUSQUEDA DE CATEGORIA

        #region BUSQUEDA DE MARCA


        private Frm_Explor_CatMar_Reducido buscarMarca;
        public string idMarca;

        private void txt_marca_MouseClick(object sender, MouseEventArgs e)
        {
            if (buscarMarca == null || buscarMarca.IsDisposed)
            {
                buscarMarca = new Frm_Explor_CatMar_Reducido();

                // Calcular la posición relativa al formulario principal (Form1)
                var formPosition = this.PointToScreen(new Point(0, 0)); // Ubicación absoluta de Form1 en la pantalla
                int x = formPosition.X + 27;
                int y = formPosition.Y + 188;

                buscarMarca.StartPosition = FormStartPosition.Manual;
                buscarMarca.Location = new Point(x, y); // Establece la ubicación calculada
                buscarMarca.TopMost = true; // Asegura que el formulario esté en la parte superior


                buscarMarca.FormClosed += buscarMarca_FormClosed; // Subscribe to FormClosed event

                buscarMarca.modo = "Marca";

                buscarMarca.Show();
                txt_marca.Focus();
            }
            else
            {
                txt_marca.Focus();
            }

        }

        private void buscarMarca_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (buscarMarca.Tag?.ToString() == "A")
            {
                txt_marca.Text = buscarMarca.nombre;
                idCategoria = buscarMarca.id;
            }
        }

        private void txt_marca_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }




        #endregion FIN BUSQUEDA DE MARCA

        #region VALIDACIONES

        private bool _actualizando = false;

        private bool Validar_Txt()
        {
            Frm_Filtro fil = new Frm_Filtro();
            frm_advertencia warning = new frm_advertencia();

            if (txt_id.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa o genera el codigo de barras!", null);

                txt_id.Focus();

                return false;
            }

            if (modo == "Nuevo")
            {
                RN_Producto obj = new RN_Producto();

                if (obj.RN_Buscar_porID(txt_id.Text) == true)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este codigo de barras ya existe!", null);

                    txt_id.Focus();

                    return false;
                }
            }

            if (modo == "Nuevo")
            {
                RN_Producto obj = new RN_Producto();

                if (obj.RN_Buscar_porID(txt_id.Text) == true)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este codigo de barras ya existe!", null);

                    txt_id.Focus();

                    return false;
                }
            }
            else if (modo == "Editar")
            {
                RN_Producto obj = new RN_Producto();

                if (obj.RN_Buscar_porID(txt_id.Text) == true && txt_id.Text != idProducto)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este codigo de barras ya existe!", null);

                    txt_id.Focus();

                    return false;
                }
            }


            if (txt_nom.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa la descripcion del producto!", null);

                txt_nom.Focus();

                return false;
            }

            if (txt_cate.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa la categoria del producto!", null);

                txt_cate.Focus();

                return false;
            }

            if (txt_tipo.SelectedIndex == -1)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el tipo de producto!", null);

                txt_tipo.Focus();

                return false;
            }

            if (txt_unidadMedida.SelectedIndex == -1)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa la unidad de medida del producto!", null);

                txt_unidadMedida.Focus();

                return false;
            }

            if (txt_precioCompra.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el precio de compra del producto!", null);

                txt_precioCompra.Focus();

                return false;
            }

            if (txt_ventaMenor.Text.Trim() == "")
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el precio de venta del producto!", null);

                txt_ventaMenor.Focus();

                return false;
            }


            return true;
        }

        private void txt_precioCompra_TextChange(object sender, EventArgs e)
        {
            //string texto = txt_precioCompra.Text.Replace(".", ",");

            //// Validar que solo haya un punto decimal
            //int puntoDecimalIndex = texto.IndexOf(',');
            //if (puntoDecimalIndex != -1)
            //{
            //    // Asegúrate de que solo haya un punto decimal
            //    texto = texto.Substring(0, puntoDecimalIndex + 1) + texto.Substring(puntoDecimalIndex + 1).Replace(",", "");
            //}

            //// Actualiza el texto del TextBox
            //if (txt_precioCompra.Text != texto)
            //{
            //    txt_precioCompra.Text = texto;
            //    // Mantener el cursor al final del texto
            //    txt_precioCompra.SelectionStart = texto.Length;
            //}

            if (_actualizando == true) return;

            try
            {
                _actualizando = true;

                if (string.IsNullOrWhiteSpace(txt_precioCompra.Text))
                    return;

                double frank = Convert.ToDouble(txt_frank.Text);
                string textoPrecioCompraBlue = lbl_precioCompraBlue.Text.Replace("$", "").Replace(".", ",");

                if (okDolar && check_CompraDolar.Checked == true)
                {
                    double precioCompraDolar = Convert.ToDouble(txt_precioCompra.Text) / Convert.ToDouble(textoPrecioCompraBlue);
                    txt_precioCompraDolar.Text = precioCompraDolar.ToString("###0.00");

                    double precioVentaDolar = precioCompraDolar * ((frank / 100) + 1);
                    txt_VentaDolar.Text = precioVentaDolar.ToString("###0.00");
                }


                // Calcular PrecioVenta
                double precioVenta = Convert.ToDouble(txt_precioCompra.Text) * ((frank / 100) + 1);
                txt_ventaMenor.Text = precioVenta.ToString("###0.00");

                // Calcular Utilidad
                double utilidadUnitario = precioVenta - Convert.ToDouble(txt_precioCompra.Text);
                txt_utilidad.Text = utilidadUnitario.ToString("###0.00");


            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error", ex);
            }
            finally
            {
                _actualizando = false;
            }
        }

        private void txt_precioCompraDolar_TextChange(object sender, EventArgs e)
        {
            //string texto = txt_precioCompraDolar.Text.Replace(".", ",");

            //// Validar que solo haya un punto decimal
            //int puntoDecimalIndex = texto.IndexOf(',');
            //if (puntoDecimalIndex != -1)
            //{
            //    // Asegúrate de que solo haya un punto decimal
            //    texto = texto.Substring(0, puntoDecimalIndex + 1) + texto.Substring(puntoDecimalIndex + 1).Replace(",", "");
            //}

            //// Actualiza el texto del TextBox
            //if (txt_precioCompraDolar.Text != texto)
            //{
            //    txt_precioCompraDolar.Text = texto;
            //    // Mantener el cursor al final del texto
            //    txt_precioCompraDolar.SelectionStart = texto.Length;
            //}

            if (_actualizando == true) return;

            try
            {
                _actualizando = true;

                if (string.IsNullOrWhiteSpace(txt_precioCompraDolar.Text))
                    return;

                double frank = Convert.ToDouble(txt_frank.Text);
                string textoPrecioCompraBlue = lbl_precioCompraBlue.Text.Replace("$", "").Replace(".", ",");

                double precioCompra = Convert.ToDouble(txt_precioCompraDolar.Text) * Convert.ToDouble(textoPrecioCompraBlue);
                txt_precioCompra.Text = precioCompra.ToString("###0.00");

                // Calcular PrecioVenta
                double precioVenta = precioCompra * ((frank / 100) + 1);
                txt_ventaMenor.Text = precioVenta.ToString("###0.00");

                double precioVentaDolar = Convert.ToDouble(txt_precioCompraDolar.Text) * ((frank / 100) + 1);
                txt_VentaDolar.Text = precioVentaDolar.ToString("###0.00");

                // Calcular Utilidad
                double utilidadUnitario = precioVenta - precioCompra;
                txt_utilidad.Text = utilidadUnitario.ToString("###0.00");


            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error", ex);
            }
            finally
            {
                _actualizando = false;
            }


        }

        private void txt_frank_TextChange(object sender, EventArgs e)
        {

            //string texto = txt_frank.Text.Replace(".", ",");

            //// Validar que solo haya un punto decimal
            //int puntoDecimalIndex = texto.IndexOf(',');
            //if (puntoDecimalIndex != -1)
            //{
            //    // Asegúrate de que solo haya un punto decimal
            //    texto = texto.Substring(0, puntoDecimalIndex + 1) + texto.Substring(puntoDecimalIndex + 1).Replace(",", "");
            //}

            //txt_frank.Text = texto;
            //txt_frank.SelectionStart = texto.Length;

            if (_actualizando == true) return;

            try
            {
                _actualizando = true;

                if (string.IsNullOrWhiteSpace(txt_frank.Text) || string.IsNullOrWhiteSpace(txt_precioCompra.Text))
                    return;

                double precioCompra = Convert.ToDouble(txt_precioCompra.Text);
                double frank = Convert.ToDouble(txt_frank.Text);

                // Calcular PrecioVenta
                double precioVenta = precioCompra * ((frank / 100) + 1);
                txt_ventaMenor.Text = precioVenta.ToString("###0.00");

                if (okDolar && check_CompraDolar.Checked == true)
                {
                    double precioVentaDolar = Convert.ToDouble(txt_precioCompraDolar.Text) * ((frank / 100) + 1);
                    txt_VentaDolar.Text = precioVentaDolar.ToString("###0.00");
                }

                // Calcular Utilidad
                double utilidadUnitario = precioVenta - precioCompra;
                txt_utilidad.Text = utilidadUnitario.ToString("###0.00");
            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error", ex);
            }
            finally
            {
                _actualizando = false;
            }
        }

        private void txt_ventaMenor_TextChange(object sender, EventArgs e)
        {
            //string texto = txt_ventaMenor.Text.Replace(".", ",");

            //// Validar que solo haya un punto decimal
            //int puntoDecimalIndex = texto.IndexOf(',');
            //if (puntoDecimalIndex != -1)
            //{
            //    // Asegúrate de que solo haya un punto decimal
            //    texto = texto.Substring(0, puntoDecimalIndex + 1) + texto.Substring(puntoDecimalIndex + 1).Replace(",", "");
            //}

            //txt_ventaMenor.Text = texto;
            //txt_ventaMenor.SelectionStart = texto.Length;

            if (_actualizando == true) return;

            try
            {
                _actualizando = true;

                if (string.IsNullOrWhiteSpace(txt_ventaMenor.Text) || string.IsNullOrWhiteSpace(txt_precioCompra.Text))
                    return;

                double precioCompra = Convert.ToDouble(txt_precioCompra.Text);
                double precioVenta = Convert.ToDouble(txt_ventaMenor.Text);

                // Calcular Utilidad
                double utilidadUnitario = precioVenta - precioCompra;
                txt_utilidad.Text = utilidadUnitario.ToString("###0.00");

                // Calcular % de ganancia
                double frank = (utilidadUnitario * 100) / precioCompra;
                txt_frank.Text = frank.ToString("###0.00");

                if (okDolar && check_CompraDolar.Checked == true)
                {
                    //Precio venta dolar
                    double precioVentaDolar = Convert.ToDouble(txt_precioCompraDolar.Text) * ((frank / 100) + 1);
                    txt_VentaDolar.Text = precioVentaDolar.ToString("###0.00");
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Error!", "Ocurrio un error", ex);
            }
            finally
            {
                _actualizando = false;
            }
        }

        //private void txt_Numeros_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    soloNumeros(e);
        //}

        //private void txt_Reemplazar_Comas_TextChange(object sender, EventArgs e)
        //{
        //    Reemplazar_comas(sender);
        //}

        private void txt_Enteros_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumerosEnteros(e);
        }

        //private void Reemplazar_comas(object txtBox)
        //{
        //    if (txtBox == null)
        //        throw new ArgumentNullException(nameof(txtBox));

        //    var placeholderInputField = txtBox.GetType().GetField("placeholderInput", BindingFlags.NonPublic | BindingFlags.Instance);
        //    if (placeholderInputField != null)
        //    {
        //        var placeholderInput = placeholderInputField.GetValue(txtBox);
        //        var textProperty = placeholderInput.GetType().GetProperty("Text", BindingFlags.Public | BindingFlags.Instance);

        //        if (textProperty != null)
        //        {
        //            string texto = textProperty.GetValue(placeholderInput)?.ToString().Replace(".", ",");

        //            // Validar que solo haya un punto decimal
        //            int puntoDecimalIndex = texto.IndexOf(',');
        //            if (puntoDecimalIndex != -1)
        //            {
        //                // Asegúrate de que solo haya un punto decimal
        //                texto = texto.Substring(0, puntoDecimalIndex + 1) + texto.Substring(puntoDecimalIndex + 1).Replace(",", "");
        //            }

        //            // Actualiza el texto del TextBox
        //            if (textProperty.GetValue(placeholderInput)?.ToString() != texto)
        //            {
        //                textProperty.SetValue(placeholderInput, texto);

        //                // Mantener el cursor al final del texto
        //                var selectionStartProperty = placeholderInput.GetType().GetProperty("SelectionStart", BindingFlags.Public | BindingFlags.Instance);
        //                if (selectionStartProperty != null)
        //                {
        //                    selectionStartProperty.SetValue(placeholderInput, texto.Length);
        //                }
        //            }
        //        }
        //    }
        //}

        private void soloNumerosEnteros(KeyPressEventArgs e)

        {

            Utilitario codigoASCII = new Utilitario();

            e.KeyChar = Convert.ToChar(codigoASCII.Solo_NumerosEnteros(e.KeyChar));

        }

        //private void soloNumeros(KeyPressEventArgs e)

        //{

        //    Utilitario codigoASCII = new Utilitario();

        //    e.KeyChar = Convert.ToChar(codigoASCII.Solo_Numeros(e.KeyChar));

        //}

        private void check_CompraDolar_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (_actualizando == true) return;

            if (check_CompraDolar.Checked == true)
            {
                txt_precioCompraDolar.Enabled = true;
                txt_precioCompraDolar.Focus();
                txt_precioCompra.Text = "0.00";
                txt_frank.Text = "0.00";
            }
            else
            {
                txt_precioCompraDolar.Enabled = false;
                txt_precioCompraDolar.Text = "0.00";
                txt_VentaDolar.Text = "0.00";
                txt_frank.Text = "0.00";
            }
        }

        private void txt_id_TextChange(object sender, EventArgs e)
        {
            string prefijo = "P-";
            if (!txt_id.Text.StartsWith(prefijo))
            {
                txt_id.Text = prefijo + txt_id.Text;
                txt_id.SelectionStart = txt_id.Text.Length; // Mantener el cursor al final del texto
            }

            string texto = txt_id.Text;

            // Validar que solo haya un punto decimal
            int pIndex = texto.IndexOf('P');
            if (pIndex != -1)
            {
                // Asegúrate de que solo haya un punto decimal
                texto = texto.Substring(0, pIndex + 1) + texto.Substring(pIndex + 1).Replace("P", "");
            }

            // Actualiza el texto del TextBox
            if (txt_id.Text != texto)
            {
                txt_id.Text = texto;
                // Mantener el cursor al final del texto
                txt_id.SelectionStart = texto.Length;
            }
        }


        #endregion FIN VALIDACIONES


        #region PRECIO DOLAR

        private bool CargarPrecioDolar()
        {
            if(Convert.ToDouble(Frm_Principal.precioVentaBlue) > 0 && Convert.ToDouble(Frm_Principal.precioCompraBlue) > 0)
            {
                lbl_precioCompraBlue.Text = Frm_Principal.precioCompraBlue;
                lbl_precioVentaBlue.Text = Frm_Principal.precioVentaBlue;
                return true;
            }

            return false;
        }



        #endregion

        private void btn_RestablacerCodBarras_Click(object sender, EventArgs e)
        {
            txt_id.Text = GenerarCodigoProd();
        }

        private string GenerarCodigoProd()
        {
            RN_Producto obj = new RN_Producto();
            string codigo;
            bool existe;

            do
            {
                codigo = RN_TipoDoc.RN_NroID(4);
                existe = obj.RN_Buscar_porID(codigo);

                if (existe)
                {
                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo_Producto(4);
                }

            } while (existe);

            return codigo;
        }

    }
}
