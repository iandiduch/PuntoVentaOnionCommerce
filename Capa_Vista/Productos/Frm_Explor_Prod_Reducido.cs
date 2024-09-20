using Onion_Commerce.Compras;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Onion_Commerce.Productos
{
    public partial class Frm_Explor_Prod_Reducido : BaseForm
    {
        public string modo;
        public Frm_Explor_Prod_Reducido()
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
            this.Tag = "";
            this.Close();
        }
        private void Frm_Explor_Prov_Load(object sender, EventArgs e)
        {
            productos.Clear();

            Configurar_ListViewCarrito();

            if (modo == "cotizacion")
            {
                check_cotizacion.Checked = true;
                check_cotizacion.Enabled = false;
            }
            else
            {
                check_cotizacion.Checked = false;
                check_cotizacion.Enabled = false;
            }

            if (modo == "compra" || modo == "cotizacion")
            {
                check_verTodoSinExc.Checked = true;
            }
            else
            {
                check_verTodoSinExc.Checked = false;
            }

            Configurar_ListView();

            txt_buscar.Focus();
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

            //1211px
            //configurar columnas
            lis.Columns.Add("Codigo", 135, HorizontalAlignment.Left);
            lis.Columns.Add("Marca", 113, HorizontalAlignment.Left);
            lis.Columns.Add("Descripción del Producto", 300, HorizontalAlignment.Left);
            lis.Columns.Add("Stock", 52, HorizontalAlignment.Left);

            if (modo == "compra")
            {
                lis.Columns.Add("Precio Costo", 226, HorizontalAlignment.Left); //4
                lis.Columns.Add("Precio", 0, HorizontalAlignment.Left); //4
                lis.Columns.Add("Precio Mayor.", 0, HorizontalAlignment.Left); //5
            }
            else
            {
                lis.Columns.Add("Precio Costo", 0, HorizontalAlignment.Left); //4
                lis.Columns.Add("Precio", 113, HorizontalAlignment.Left); //4
                lis.Columns.Add("Precio Mayor.", 118, HorizontalAlignment.Left); //5
            }

            lis.Columns.Add("Estado", 67, HorizontalAlignment.Left);
            lis.Columns.Add("Tipo", 0, HorizontalAlignment.Left);
            lis.Columns.Add("UndMedida", 0, HorizontalAlignment.Left);
            lis.Columns.Add("PesoUnit", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Frank", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Pre_CompraUsd", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Pre_VentaUsd", 0, HorizontalAlignment.Left);
            lis.Columns.Add("UtilidadUnit", 0, HorizontalAlignment.Left);
        }

        private void Configurar_ListViewCarrito()
        {
            var lis = lsv_carrito;

            lsv_carrito.Items.Clear();
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            //1211px
            //configurar columnas
            lis.Columns.Add("Codigo", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Descripción del Producto", 150, HorizontalAlignment.Left);
            lis.Columns.Add("Cantidad", 50, HorizontalAlignment.Left);
            lis.Columns.Add("Importe", 75, HorizontalAlignment.Right);
        }

        private void Llenar_ListView(DataTable data)
        {
            try
            {
                string idProd;
                double stockR;

                lsv_productos.Items.Clear();

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow dr = data.Rows[i];
                    idProd = dr["Id_Pro"].ToString();
                    stockR = Convert.ToDouble(dr["Stock_Actual"]);

                    if (check_verTodoSinExc.Checked == true)
                    {
                        ListViewItem list = new ListViewItem(dr["Id_Pro"].ToString());
                        list.SubItems.Add(dr["Marca"].ToString());
                        list.SubItems.Add(dr["Descripcion_Larga"].ToString());
                        list.SubItems.Add(dr["Stock_Actual"].ToString());
                        list.SubItems.Add(Convert.ToDouble(dr["Pre_CompraS"]).ToString("N2"));
                        list.SubItems.Add(Convert.ToDouble(dr["Pre_vntaxMenor"]).ToString("N2"));
                        if (Convert.ToDouble(dr["Pre_vntaxMayor"]) == 0)
                        {
                            list.SubItems.Add("No Aplica");
                        }
                        else
                        {
                            list.SubItems.Add(Convert.ToDouble(dr["Pre_vntaxMayor"]).ToString("N2"));
                        }
                        list.SubItems.Add(dr["Estado_Pro"].ToString());
                        list.SubItems.Add(dr["TipoProdcto"].ToString());

                        list.SubItems.Add(dr["UndMedida"].ToString());
                        list.SubItems.Add(dr["PesoUnit"].ToString());
                        list.SubItems.Add(dr["Frank"].ToString());
                        list.SubItems.Add(dr["Pre_Compra$"].ToString());
                        list.SubItems.Add(dr["Pre_Vntadolar"].ToString());
                        list.SubItems.Add(dr["UtilidadUnit"].ToString());
                        lsv_productos.Items.Add(list);
                    }
                    else
                    {
                        if (stockR > 0)
                        {
                            ListViewItem list = new ListViewItem(dr["Id_Pro"].ToString());
                            list.SubItems.Add(dr["Marca"].ToString());
                            list.SubItems.Add(dr["Descripcion_Larga"].ToString());
                            list.SubItems.Add(dr["Stock_Actual"].ToString());
                            list.SubItems.Add(Convert.ToDouble(dr["Pre_CompraS"]).ToString("N2"));
                            list.SubItems.Add(Convert.ToDouble(dr["Pre_vntaxMenor"]).ToString("N2"));
                            if (Convert.ToDouble(dr["Pre_vntaxMayor"]) == 0)
                            {
                                list.SubItems.Add("No Aplica");
                            }
                            else
                            {
                                list.SubItems.Add(Convert.ToDouble(dr["Pre_vntaxMayor"]).ToString("N2"));
                            }
                            list.SubItems.Add(dr["Estado_Pro"].ToString());
                            list.SubItems.Add(dr["TipoProdcto"].ToString());

                            list.SubItems.Add(dr["UndMedida"].ToString());
                            list.SubItems.Add(dr["PesoUnit"].ToString());
                            list.SubItems.Add(dr["Frank"].ToString());
                            list.SubItems.Add(dr["Pre_Compra$"].ToString());
                            list.SubItems.Add(dr["Pre_Vntadolar"].ToString());
                            list.SubItems.Add(dr["UtilidadUnit"].ToString());
                            lsv_productos.Items.Add(list);
                        }
                    }

                }

                PintarFilas();
                pnl_BusquedaSinRes.Visible = false;

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("Ha ocurrido un error al cargar los productos.", "Intente nuevamente.", ex);
            }
        }

        private void PintarFilas()
        {
            int cont = 1;

            for (int i = 0; i < lsv_productos.Items.Count; i++)
            {
                if (cont % 2 == 0) lsv_productos.Items[i].BackColor = Color.AliceBlue;

                cont++;
            }

            //for (int i = 0; i < lsv_productos.Items.Count; i++)
            //{
            //    lsv_productos.Items[i].SubItems[2].BackColor = Color.Linen;
            //    lsv_productos.Items[i].SubItems[3].BackColor = Color.Beige;
            //    lsv_productos.Items[i].SubItems[5].BackColor = Color.MintCream;
            //    lsv_productos.Items[i].SubItems[6].BackColor = Color.AliceBlue;
            //    lsv_productos.Items[i].UseItemStyleForSubItems = false;

            //}

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
            }
            pnl_load.Visible = false;
        }

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void txt_buscar_TextChange(object sender, EventArgs e)
        {
            if (txt_buscar.Text.Length > 0)
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
            }

            pnl_load.Visible = false;
        }

        private async void btn_verTodo_Click(object sender, EventArgs e)
        {
            await Cargar_Todos_ProdAsync();
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


        public string idProducto;
        public string marca;
        public string descripcion;
        public string precioCosto;
        public string cantidad;
        public string importeTotal;
        public string stock;
        public string peso;
        public string undMedida;
        public string tipo;
        public string frank;
        public string precioCostoUsd;
        public string precioMenor;
        public string precioMayor;
        public string precioUSD;
        public string utilidad;

        private void Seleccionar_ProductoModoCompra()
        {

            if (lsv_productos.SelectedIndices.Count != 0)
            {

                Frm_Filtro fil = new Frm_Filtro();
                Frm_Cantidad frmCant = new Frm_Cantidad();

                double importe;
                string estado = "";

                var lis = lsv_productos.SelectedItems[0];
                tipo = lis.SubItems[8].Text;

                idProducto = lis.SubItems[0].Text;
                marca = lis.SubItems[1].Text;
                descripcion = lis.SubItems[2].Text;

                precioCosto = Convert.ToDouble(lis.SubItems[4].Text).ToString("N2");
                precioCostoUsd = Convert.ToDouble(lis.SubItems[12].Text).ToString("N2");

                stock = lis.SubItems[3].Text;
                peso = lis.SubItems[10].Text;
                undMedida = lis.SubItems[9].Text;
                frank = lis.SubItems[11].Text;
                utilidad = lis.SubItems[14].Text;

                estado = lis.SubItems[7].Text;
                if (estado == "Eliminado")
                {
                    ErrorLog.MensajeError("¡Advertencia!", "El producto seleccionado esta eliminado, no se puede seleccionar.", null);
                    return;
                }

                if (tipo.Trim() == "Producto")
                {
                    fil.Show();
                    frmCant.lbl_stock.Text = stock;
                    frmCant.lbl_descripcion.Text = descripcion;
                    frmCant.undMedida = undMedida;
                    frmCant.ShowDialog();
                    fil.Hide();

                    if (frmCant.Tag.ToString() == "A")
                    {
                        if (undMedida.Trim() == "Unidad")
                        {
                            cantidad = frmCant.txt_cantidadEntera.Text;
                        }
                        else
                        {
                            cantidad = frmCant.txt_cantidad.Text;
                        }

                        importe = Convert.ToDouble(cantidad) * Convert.ToDouble(precioCosto);
                        importeTotal = importe.ToString("N2");

                        var seleccion = new
                        {
                            Id_Pro = idProducto,
                            Marca = marca,
                            Descripcion = descripcion,
                            Cantidad = cantidad,
                            Precio = precioCosto,
                            PrecioMayor = 0,
                            PrecioVentaUSD = 0,
                            Importe = importeTotal,
                            Tipo = tipo,
                            Stock = stock,
                            UndMedida = undMedida,
                            Peso = peso,
                            Frank = frank,
                            PrecioCompraUsd = precioCostoUsd,
                            UtilidadUnit = utilidad
                        };
                        AgregarProducto(seleccion);
                    }
                }
                else
                {
                    cantidad = "1";
                    importe = Convert.ToDouble(cantidad) * Convert.ToDouble(precioCosto);
                    importeTotal = importe.ToString("N2");

                    var seleccion = new
                    {
                        Id_Pro = idProducto,
                        Marca = marca,
                        Descripcion = descripcion,
                        Cantidad = cantidad,
                        Precio = precioCosto,
                        PrecioMayor = 0,
                        PrecioVentaUSD = 0,
                        Importe = importeTotal,
                        Tipo = tipo,
                        Stock = stock,
                        UndMedida = undMedida,
                        Peso = peso,
                        Frank = frank,
                        PrecioCompraUsd = precioCostoUsd,
                        UtilidadUnit = utilidad
                    };
                    AgregarProducto(seleccion);
                }



            }
        }

        public List<dynamic> productos = new List<dynamic>();

        private void AgregarProducto(dynamic producto)
        {
            bool ok = true;
            if (check_llenarSinSalir.Checked == true)
            {
                if (lsv_carrito.Items.Count != 0)
                {
                    //validar que el producto no se ingrese 2 veces
                    for (int i = 0; i < lsv_carrito.Items.Count; i++)
                    {
                        if (lsv_carrito.Items[i].Text.Trim() == producto.Id_Pro.ToString().Trim())
                        {
                            double cantidad = Convert.ToDouble(lsv_carrito.Items[i].SubItems[2].Text);
                            cantidad += Convert.ToDouble(producto.Cantidad);
                            lsv_carrito.Items[i].SubItems[2].Text = cantidad.ToString();
                            lsv_carrito.Items[i].SubItems[3].Text = (cantidad * Convert.ToDouble(producto.Precio)).ToString("N2");
                            Calcular();
                            ok = false;
                        }
                    }
                }

                if (ok)
                {
                    ListViewItem lista = new ListViewItem(producto.Id_Pro);
                    lista.SubItems.Add(producto.Descripcion);
                    lista.SubItems.Add(producto.Cantidad);
                    lista.SubItems.Add(producto.Importe);
                    lsv_carrito.Items.Add(lista);

                    Calcular();
                }

                productos.Add(producto);

            }
            else
            {
                productos.Add(producto);
                this.Tag = "A";
                this.Close();
            }
        }

        private void Calcular()
        {
            try
            {
                double total = 0;
                double totalCant = 0;
                for (int i = 0; i < lsv_carrito.Items.Count; i++)
                {
                    total += Convert.ToDouble(lsv_carrito.Items[i].SubItems[3].Text);
                    totalCant += Convert.ToDouble(lsv_carrito.Items[i].SubItems[2].Text);
                }

                lbl_totalImporte.Text = total.ToString("N2");
                lbl_cantTotal.Text = totalCant.ToString();
                btn_carrito.Text = totalCant.ToString();
            }
            catch (Exception ex)
            {

                ErrorLog.MensajeError("Error al calcular", "Ocurrio un error al calcular el total de la compra", ex);
            }
        }

        private void Seleccionar_ProductoModoVenta()
        {

            if (lsv_productos.SelectedIndices.Count != 0)
            {
                Frm_Filtro fil = new Frm_Filtro();
                Frm_Cantidad frmCant = new Frm_Cantidad();

                double importe;
                string estado;

                var lis = lsv_productos.SelectedItems[0];

                tipo = lis.SubItems[8].Text;

                idProducto = lis.SubItems[0].Text;
                marca = lis.SubItems[1].Text;
                descripcion = lis.SubItems[2].Text;

                precioMenor = Convert.ToDouble(lis.SubItems[5].Text).ToString("N2");
                if (lis.SubItems[6].Text == "No Aplica")
                {
                    precioMayor = "0";
                }
                else
                {
                    precioMayor = Convert.ToDouble(lis.SubItems[6].Text).ToString("N2");
                }
                precioUSD = Convert.ToDouble(lis.SubItems[13].Text).ToString("N2");

                stock = lis.SubItems[3].Text;
                peso = lis.SubItems[10].Text;
                undMedida = lis.SubItems[9].Text;
                frank = lis.SubItems[11].Text;
                utilidad = lis.SubItems[14].Text;

                estado = lis.SubItems[7].Text;
                if (estado == "Eliminado")
                {
                    ErrorLog.MensajeError("¡Advertencia!", "El producto seleccionado esta eliminado, no se puede seleccionar.", null);
                    return;
                }
                if (stock == "0" && modo != "cotizacion" && tipo.Trim() == "Producto")
                {
                    ErrorLog.MensajeError("¡Advertencia!", "El producto seleccionado no tiene stock, no se puede seleccionar.", null);
                    return;
                }

                if (tipo.Trim() == "Producto")
                {
                    if (check_llenarSinSalir.Checked == false)
                    {
                        fil.Show();
                        frmCant.lbl_stock.Text = stock;
                        frmCant.lbl_descripcion.Text = descripcion;
                        frmCant.undMedida = undMedida;
                        frmCant.ShowDialog();
                        fil.Hide();

                        if (frmCant.Tag.ToString() == "A")
                        {
                            if (undMedida.Trim() == "Unidad")
                            {
                                cantidad = frmCant.txt_cantidadEntera.Text;
                            }
                            else
                            {
                                cantidad = frmCant.txt_cantidad.Text;
                            }

                            importe = Convert.ToDouble(cantidad) * Convert.ToDouble(precioMenor);
                            importeTotal = importe.ToString("N2");

                            var seleccion = new
                            {
                                Id_Pro = idProducto,
                                Marca = marca,
                                Descripcion = descripcion,
                                Cantidad = cantidad,
                                Precio = precioMenor,
                                PrecioMayor = precioMayor,
                                PrecioVentaUSD = precioUSD,
                                Importe = importeTotal,
                                Tipo = tipo,
                                Stock = stock,
                                UndMedida = undMedida,
                                Peso = peso,
                                Frank = frank,
                                PrecioCompraUsd = 0,
                                UtilidadUnit = utilidad
                            };
                            AgregarProducto(seleccion);
                        }
                    }
                }
                else
                {

                    cantidad = "1";
                    importe = Convert.ToDouble(cantidad) * Convert.ToDouble(precioMenor);
                    importeTotal = importe.ToString("N2");

                    var seleccion = new
                    {
                        Id_Pro = idProducto,
                        Marca = marca,
                        Descripcion = descripcion,
                        Cantidad = cantidad,
                        Precio = precioMenor,
                        PrecioMayor = precioMayor,
                        PrecioVentaUSD = precioUSD,
                        Importe = importeTotal,
                        Tipo = tipo,
                        Stock = stock,
                        UndMedida = undMedida,
                        Peso = peso,
                        Frank = frank,
                        PrecioCompraUsd = 0,
                        UtilidadUnit = utilidad
                    };
                    AgregarProducto(seleccion);
                }



            }
        }

        private void lsv_productos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btn_ok_Click(sender, e);
        }





        private void Frm_Explor_Prod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Tag = "";
                this.Close();
            }
            else if (e.KeyCode == Keys.F1)
            {
                btn_Terminar_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txt_buscar.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btn_carrito_Click(sender, e);
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (modo == "compra")
            {
                Seleccionar_ProductoModoCompra();
            }
            else
            {
                Seleccionar_ProductoModoVenta();
            }
        }


        private void lsv_productos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(sender, e);
            }
        }

        private void txt_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (lsv_productos.Items.Count > 0)
                {
                    lsv_productos.Focus();
                    lsv_productos.Items[0].Selected = true;
                }
                else
                {
                    btn_verTodo_Click(sender, e);
                }
            }
        }


        private void lbl_checkVerTodosSinExc_Click(object sender, EventArgs e)
        {
            if (check_verTodoSinExc.Checked == true)
            {
                check_verTodoSinExc.Checked = false;
            }
            else
            {
                check_verTodoSinExc.Checked = true;
            }
        }

        private void check_verTodoSinExc_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (check_verTodoSinExc.Checked == true)
            {
                btn_verTodo_Click(sender, e);
            }
            else
            {
                btn_verTodo_Click(sender, e);
            }

        }

        private void lbl_checkLlenarSinSalir_Click(object sender, EventArgs e)
        {
            if (check_llenarSinSalir.Checked == true)
            {
                check_llenarSinSalir.Checked = false;
            }
            else
            {
                check_llenarSinSalir.Checked = true;
            }
        }

        private void check_llenarSinSalir_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (check_llenarSinSalir.Checked == true)
            {
                lsv_carrito.Items.Clear();
                productos.Clear();
                lbl_cantTotal.Text = "0";
                lbl_totalImporte.Text = "0,00";
                btn_carrito.Text = "0";
            }
            else
            {
                lsv_carrito.Items.Clear();
                productos.Clear();
                lbl_cantTotal.Text = "0";
                lbl_totalImporte.Text = "0,00";
                btn_carrito.Text = "0";
            }
        }

        private void btn_Terminar_Click(object sender, EventArgs e)
        {
            this.Tag = "A";
            this.Close();
        }

        private void btn_carrito_Click(object sender, EventArgs e)
        {
            if (pnl_carrito.Visible == true)
            {
                pnl_carrito.Visible = false;
            }
            else
            {
                pnl_carrito.Visible = true;
            }
        }

        private void btn_Terminarr_Click(object sender, EventArgs e)
        {
            btn_Terminar_Click(sender, e);
        }

        private void lsv_carrito_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (lsv_carrito.SelectedIndices.Count != 0)
                {
                    // Obtener el índice del elemento seleccionado en el ListView
                    int index = lsv_carrito.SelectedIndices[0];

                    // Obtener el Id_Pro del producto seleccionado en el ListView
                    string idProducto = lsv_carrito.Items[index].Text;

                    // Eliminar el producto del ListView
                    lsv_carrito.Items.RemoveAt(index);

                    // Buscar y eliminar el producto de la lista `productos`
                    dynamic productoAEliminar = productos.FirstOrDefault(p => p.Id_Pro.ToString() == idProducto);
                    if (productoAEliminar != null)
                    {
                        productos.Remove(productoAEliminar);
                    }

                    // Recalcular los totales
                    Calcular();
                }
            }
        }

        private void Frm_Explor_Prod_Reducido_Shown(object sender, EventArgs e)
        {
            txt_buscar.Focus();
        }
    }
}
