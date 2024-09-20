using MySql.Data.MySqlClient;
using Capa_Datos.Util;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class BD_Pedido : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Pedido en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Registrar_Pedido", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_id_Ped", en.IdPed);
                    cmd.Parameters.AddWithValue("_Id_Cliente", en.IdCliente);
                    //cmd.Parameters.AddWithValue("_Fecha", en.Fecha);
                    cmd.Parameters.AddWithValue("_SubTotal", en.SubTotal);
                    cmd.Parameters.AddWithValue("_IgvPed", en.IgvPed);
                    cmd.Parameters.AddWithValue("_TotalPed", en.TotalPed);
                    cmd.Parameters.AddWithValue("_id_Usu", en.IdUsu);
                    cmd.Parameters.AddWithValue("_TotalGancia", en.TotalGancia);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al guardar", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_RegistrarDetalle(EN_DetallePedido en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Registrar_detalle_Pedido", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_id_Ped", en.IdPed);
                    cmd.Parameters.AddWithValue("_Id_Pro", en.IdPro);
                    cmd.Parameters.AddWithValue("_Precio", en.Precio);
                    cmd.Parameters.AddWithValue("_Cantidad", en.Cantidad);
                    cmd.Parameters.AddWithValue("_Importe", en.Importe);
                    cmd.Parameters.AddWithValue("_Tipo_Prod", en.TipoProd);
                    cmd.Parameters.AddWithValue("_Und_Medida", en.UndMedida);
                    cmd.Parameters.AddWithValue("_Utilidad_Unit", en.UtilidadUnit);
                    cmd.Parameters.AddWithValue("_TotalUtilidad", en.TotalUtilidad);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al guardar detalle", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_Editar(EN_Pedido en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Editar_Pedido", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_id_Ped", en.IdPed);
                    cmd.Parameters.AddWithValue("_Id_Cliente", en.IdCliente);
                    cmd.Parameters.AddWithValue("_fechaPed", en.Fecha);
                    cmd.Parameters.AddWithValue("_SubTotal", en.SubTotal);
                    cmd.Parameters.AddWithValue("_IgvPed", en.IgvPed);
                    cmd.Parameters.AddWithValue("_TotalPed", en.TotalPed);
                    cmd.Parameters.AddWithValue("_id_Usu", en.IdUsu);
                    cmd.Parameters.AddWithValue("_TotalGancia", en.TotalGancia);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al editar", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_Eliminar(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Eliminar_Pedido_Completo", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Ped", val);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_EliminarDetalle(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_eliminar_detalle_Pedido", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_id_Ped", val);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al eliminar detalle", ex);
                    return false;
                }
            }
        }

        public DataTable BD_ListarPendiente()
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Leer_Pedidos_PorAtender", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Pedido", "Error al listar", ex);
                    return null;
                }
            }
        }

        public DataTable BD_ListarPorValor(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_buscar_Pedidos_porValor", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_valor", val);

                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Pedido", "Error al listar por valor", ex);
                    return null;
                }
            }
        }

        public DataTable BD_ListarPorDiaMes(string tipo, DateTime val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Listar_Pedidos_porFecha", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_tipo", val);
                    cmd.Parameters.AddWithValue("_fecha", val);

                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Pedido", "Error al listar por fecha", ex);
                    return null;
                }
            }
        }

        public DataTable BD_BuscarPedidoDetalle(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Buscar_Pedido_Para_Editar", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Ped", val);

                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos Pedido", "Error al buscar pedidos en detalle", ex);
                    return null;
                }
            }
        }

        public async Task<bool> BD_EditarCliente(string idPed, string idCliente)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actu_clien_Ped", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Ped", idPed);
                    cmd.Parameters.AddWithValue("_Id_cli", idCliente);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_EstadoAtendido(string idPed)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actu_clien_Ped", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Ped", idPed);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public bool BD_Verificar_IdPedido(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Verificar_Id_Pedido", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Ped", val);


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
                    Error.MensajeError("Capa Datos Cliente", "Error al validar", ex);
                    return false;
                }
            }

        }

    }
}





