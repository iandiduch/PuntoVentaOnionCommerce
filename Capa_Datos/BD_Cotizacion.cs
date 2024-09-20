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
    public class BD_Cotizacion : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Cotizacion en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Registrar_Cotizacion", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Cotiza", en.IdCotiza);
                    cmd.Parameters.AddWithValue("_Id_Ped", en.IdPed);
                    cmd.Parameters.AddWithValue("_Vigencia", en.Vigencia);
                    cmd.Parameters.AddWithValue("_TotalCotiza", en.TotalCotiza);
                    cmd.Parameters.AddWithValue("_Condiciones", en.Condiciones);
                    cmd.Parameters.AddWithValue("_PrecioconIgv", en.PrecioconIgv);




                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    Error.MensajeError("Capa Datos Pedido", "Error al guardar", ex);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public DataTable BD_ImprimirCotizacion(string IdCotiza)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_ImprimirCotizacion", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Nro_coti", IdCotiza);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al consultar para impresión", ex);
                    return null;
                }
            }
        }

        public async Task<bool> BD_Editar(EN_Cotizacion en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Editar_Cotizacion", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Cotiza", en.IdCotiza);
                    cmd.Parameters.AddWithValue("_Id_Ped", en.IdPed);
                    cmd.Parameters.AddWithValue("_FechaCoti", en.FechaCoti);
                    cmd.Parameters.AddWithValue("_Vigencia", en.Vigencia);
                    cmd.Parameters.AddWithValue("_TotalCotiza", en.TotalCotiza);
                    cmd.Parameters.AddWithValue("_Condiciones", en.Condiciones);
                    cmd.Parameters.AddWithValue("_PrecioconIgv", en.PrecioconIgv);

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

        public async Task<bool> BD_Eliminar(string IdCotiza)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Eliminar_cotizacion", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_NroCotiza", IdCotiza);

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

        public async Task<bool> BD_CambiarEstado(string IdCotiza, string estado)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Cambiar_Estado_Cotizacion", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_coti", IdCotiza);
                    cmd.Parameters.AddWithValue("_Estadocoti", estado);

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

        public DataTable BD_Listar()
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Cargar_todas_Cotizaciones", connection))
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
            using (MySqlCommand cmd = new MySqlCommand("Sp_Buscador_Gnral_de_Cotizaciones", connection))
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

        public DataTable BD_ListarPorDiaMes(string val, DateTime val2)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Listar_Cotizacion_porFecha", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_tipo", val);
                    cmd.Parameters.AddWithValue("_fecha", val2);

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

        public DataTable BD_ListarPorValorDetalle(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Buscar_Cotizaciones_yDetalle", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Nro_coti", val);

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
                    Error.MensajeError("Capa Datos Pedido", "Error al listar por valor en detalle", ex);
                    return null;
                }
            }
        }

        public bool BD_Buscar_porID(string Id_Prod)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_VerSiHay_porIDCotiza", connection))
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
                    Error.MensajeError("Capa Datos Cotizacion", "Error al buscar", ex);
                    return false;
                }
            }
        }
    }
}
