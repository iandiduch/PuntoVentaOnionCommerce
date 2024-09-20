using MySql.Data.MySqlClient;
using Capa_Datos.Util;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Capa_Datos
{
    public class BD_Documento : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Documento documento)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Insert_Documento", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_id_Doc", documento.IdDoc);
                    cmd.Parameters.AddWithValue("_id_Ped", documento.IdPed);
                    cmd.Parameters.AddWithValue("_Id_Tipo", documento.IdTipo);
                    cmd.Parameters.AddWithValue("_Fecha_Emi", documento.FechaEmi);
                    cmd.Parameters.AddWithValue("_Importe", documento.Importe);
                    cmd.Parameters.AddWithValue("_TipoPago", documento.TipoPago);
                    cmd.Parameters.AddWithValue("_NroOpera", documento.NroOpera);
                    cmd.Parameters.AddWithValue("_id_Usu", documento.IdUsu);
                    cmd.Parameters.AddWithValue("_Igv", documento.Igv);
                    cmd.Parameters.AddWithValue("_son", documento.Son);
                    cmd.Parameters.AddWithValue("_TotalGanancia", documento.TotalGanancia);

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

                    Error.MensajeError("Capa Datos Documento", "Error al guardar", ex);

                    return false;
                }
            }

        }

        public async Task<bool> BD_ActualizarTotalDoc(EN_Documento documento)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actualizar_documento", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Doc", documento.IdDoc);
                    cmd.Parameters.AddWithValue("_importe", documento.Importe);
                    cmd.Parameters.AddWithValue("_Igv", documento.Igv);
                    cmd.Parameters.AddWithValue("_son", documento.Son);

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

                    Error.MensajeError("Capa Datos Documento", "Error al actualizar total de Documento", ex);

                    return false;
                }
            }

        }

        public DataTable BD_Listar_PorValor(string val)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscador_Documentos_xValor", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_Xvalor", val);

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
                Error.MensajeError("Capa Datos Documentos", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_Listar()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Todas_Cajas", con);
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
                Error.MensajeError("Capa Datos Caja", "Error al listar", ex);
                return null;
            }
        }

        public DataTable BD_ListarPorFechaHoy(DateTime fecha)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Doc_emitoshoy", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_FechaActual", fecha);

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
                Error.MensajeError("Capa Datos Documentos", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_ListarPorFechaMes(DateTime fecha)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Leer_Fcturas_Emtidas_EnunMes", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_Fecha_Mes", fecha);

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
                Error.MensajeError("Capa Datos Documentos", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_LeerPorFechaMesComprobantes(DateTime fecha, int doc)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Leer_Comprobantes_Emtidas_EnunMes", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_Fecha_Mes", fecha);
                da.SelectCommand.Parameters.AddWithValue("_Docu", doc);

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
                Error.MensajeError("Capa Datos Documentos", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_ListarDetalle_PorNroDoc(string val)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscar_Documento_yDetalle", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_Nro_Doc", val);

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
                Error.MensajeError("Capa Datos Documentos", "Error al consultar", ex);
                return null;
            }
        }

        public async Task<bool> BD_Anular(string id, string estado)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Anular_Documento", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Doc", id);
                    cmd.Parameters.AddWithValue("_estado", estado);

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

                    Error.MensajeError("Capa Datos Documento", "Error al anular", ex);

                    return false;
                }
            }

        }

        public async Task<bool> BD_CambiarTipoPago(string id, string tipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Cambiar_TipoPago_Documento", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Doc", id);
                    cmd.Parameters.AddWithValue("_tipoPago", tipo);

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

                    Error.MensajeError("Capa Datos Documento", "Error al cambiar tipo de pago", ex);

                    return false;
                }
            }

        }
    }
}
