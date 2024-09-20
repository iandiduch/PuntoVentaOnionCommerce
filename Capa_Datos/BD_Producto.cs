using MySql.Data.MySqlClient;
using Capa_Datos.Util;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class BD_Producto : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Producto en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_registrar_Producto", connection))
            {
                try
                {
                    //in _idpro char(20),
                    //in _idprove char(6),
                    //in _descripcion varchar(150),
                    //in _frank real,
                    //in _Pre_compraSol real,
                    //in _pre_CompraDolar real,
                    //in _StockActual real,
                    //in _idCat int,
                    //in _idMar int,
                    //in _Foto varchar(180),
                    //in _Pre_Venta_Menor real,
                    //in _Pre_Venta_Mayor real,
                    //in _Pre_Venta_Dolar real,
                    //in _UndMdida char(6),
                    //in _PesoUnit real,
                    //in _Utilidad real,
                    //in _TipoProd varchar(12),
                    //in _ValorporProd real
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", en.Id);
                    cmd.Parameters.AddWithValue("_idprove", en.Idproveedor);
                    cmd.Parameters.AddWithValue("_descripcion", en.Descripcion);
                    cmd.Parameters.AddWithValue("_frank", en.Frank);
                    cmd.Parameters.AddWithValue("_Pre_compraSol", en.Precompra_pesos);
                    cmd.Parameters.AddWithValue("_pre_CompraDolar", en.Precompra_dlls);
                    cmd.Parameters.AddWithValue("_StockActual", en.Stock_actual);
                    cmd.Parameters.AddWithValue("_idCat", en.Id_cat);
                    cmd.Parameters.AddWithValue("_idMar", en.Id_marca);
                    cmd.Parameters.AddWithValue("_Foto", en.Foto);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Menor", en.Preventa_menor);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Mayor", en.Preventa_mayor);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Dolar", en.Preventa_dolar);
                    cmd.Parameters.AddWithValue("_UndMdida", en.Unidadmedida);
                    cmd.Parameters.AddWithValue("_PesoUnit", en.Pesounit);
                    cmd.Parameters.AddWithValue("_Utilidad", en.Utilidad);
                    cmd.Parameters.AddWithValue("_TipoProd", en.Tipoproducto);
                    cmd.Parameters.AddWithValue("_ValorporProd", en.Valor_general);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    Error.MensajeError("Capa Datos Producto", "Error al registrar", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_Editar(EN_Producto en, string nuevoIdProducto)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Editar_Producto", connection))
            {
                try
                {
                    //in _idpro char(20),
                    //in _idprove char(6),
                    //in _descripcion varchar(150),
                    //in _frank real,
                    //in _Pre_compraSol real,
                    //in _pre_CompraDolar real,
                    //in _idCat int,
                    //in _idMar int,
                    //in _Foto varchar(180),
                    //in _Pre_Venta_Menor real,
                    //in _Pre_Venta_Mayor real,
                    //in _Pre_Venta_Dolar real,
                    //in _UndMdida char(6),
                    //in _PesoUnit real,
                    //in _Utilidad real,
                    //in _TipoProd varchar(12)
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", en.Id);
                    cmd.Parameters.AddWithValue("_new_idpro", nuevoIdProducto);
                    cmd.Parameters.AddWithValue("_idprove", en.Idproveedor);
                    cmd.Parameters.AddWithValue("_descripcion", en.Descripcion);
                    cmd.Parameters.AddWithValue("_frank", en.Frank);
                    cmd.Parameters.AddWithValue("_Pre_compraSol", en.Precompra_pesos);
                    cmd.Parameters.AddWithValue("_pre_CompraDolar", en.Precompra_dlls);
                    cmd.Parameters.AddWithValue("_idCat", en.Id_cat);
                    cmd.Parameters.AddWithValue("_idMar", en.Id_marca);
                    cmd.Parameters.AddWithValue("_Foto", en.Foto);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Menor", en.Preventa_menor);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Mayor", en.Preventa_mayor);
                    cmd.Parameters.AddWithValue("_Pre_Venta_Dolar", en.Preventa_dolar);
                    cmd.Parameters.AddWithValue("_UndMdida", en.Unidadmedida);
                    cmd.Parameters.AddWithValue("_PesoUnit", en.Pesounit);
                    cmd.Parameters.AddWithValue("_Utilidad", en.Utilidad);
                    cmd.Parameters.AddWithValue("_TipoProd", en.Tipoproducto);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al editar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_Eliminar(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Eliminar_Producto", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", val);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_DarBaja(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Darbaja_Producto", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", val);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al dar baja", ex);
                    return false;
                }
            }
        }

        public DataTable BD_Listar()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_cargar_Todos_Productos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable data = new DataTable();
                da.Fill(data);
                da = null;

                return data;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                Error.MensajeError("Capa Datos Producto", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_Listar_PorValor(string val)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_buscador_Productos", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_valor", val);

                DataTable data = new DataTable();
                da.Fill(data);
                da = null;

                return data;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                Error.MensajeError("Capa Datos Producto", "Error al buscar", ex);
                return null;
            }
        }

        public bool BD_Buscar_porID(string Id_Prod)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_VerSiHay_porID", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Prod", Id_Prod);

                    connection.Open();
                    Int32 getValue = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();

                    if (getValue > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al buscar", ex);
                    return false;
                }
            }
        }


        //STOCK

        public async Task<bool> BD_SumarStock(string val, double val2)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_SumarStock", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", val);
                    cmd.Parameters.AddWithValue("_stock", val2);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al sumar stock", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_RestarStock(string val, double val2)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Restar_Stock", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", val);
                    cmd.Parameters.AddWithValue("_stock", val2);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al restar stock", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_CalcularValorStock(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_CalcularValorStock", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idpro", val);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al calcular valor stock", ex);
                    return false;
                }
            }
        }


        public async Task<bool> BD_Actulizar_Precios_CompraVenta_Producto(EN_ActualizarPrecios en)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actulizar_Precios_CompraVenta_Producto", connection))
            {
                try
                {
                    //                    in _Id_Pro char(20),
                    //in _Pre_CompraS real,
                    //in _Pre_vntaxMenor real,
                    //in _Utilidad real,
                    //in _ValorAlmacen Real
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Pro", en.IdPro);
                    cmd.Parameters.AddWithValue("_Pre_CompraS", en.PreCompraS);
                    cmd.Parameters.AddWithValue("_Pre_CompraUsd", en.PreCompraUsd);
                    cmd.Parameters.AddWithValue("_Pre_vntaxMenor", en.PreVntaxMenor);
                    cmd.Parameters.AddWithValue("_Pre_vntaxUsd", en.PreVntaxUsd);
                    cmd.Parameters.AddWithValue("_Utilidad", en.Utilidad);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al actualizar precios", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_Actulizar_Precios_Compra_Producto(EN_ActualizarPrecios en)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actulizar_Precios_Compra_Producto", connection))
            {
                try
                {
                    //                    in _Id_Pro char(20),
                    //in _Pre_CompraS real,
                    //in _Pre_vntaxMenor real,
                    //in _Utilidad real,
                    //in _ValorAlmacen Real
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Pro", en.IdPro);
                    cmd.Parameters.AddWithValue("_Pre_CompraS", en.PreCompraS);
                    cmd.Parameters.AddWithValue("_Pre_CompraUsd", en.PreCompraUsd);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al actualizar precios", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_Actulizar_Precios_Venta_Producto(EN_ActualizarPrecios en)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actulizar_Precios_Venta_Producto", connection))
            {
                try
                {
                    //                    in _Id_Pro char(20),
                    //in _Pre_CompraS real,
                    //in _Pre_vntaxMenor real,
                    //in _Utilidad real,
                    //in _ValorAlmacen Real
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Pro", en.IdPro);
                    cmd.Parameters.AddWithValue("_Pre_Venta", en.PreVntaxMenor);
                    cmd.Parameters.AddWithValue("_Pre_VentaUsd", en.PreVntaxUsd);

                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Producto", "Error al actualizar precios", ex);
                    return false;
                }
            }
        }

    }
}
