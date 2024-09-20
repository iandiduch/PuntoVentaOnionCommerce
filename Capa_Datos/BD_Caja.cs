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
    public class BD_Caja : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Caja caja)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_registrar_Caja", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Fecha_Caja", caja.FechaCaja);
                    cmd.Parameters.AddWithValue("_Tipo_Caja", caja.TipoCaja);
                    cmd.Parameters.AddWithValue("_Concepto", caja.Concepto);
                    cmd.Parameters.AddWithValue("_De_Para", caja.DePara);
                    cmd.Parameters.AddWithValue("_Nro_Doc", caja.NroDoc);
                    cmd.Parameters.AddWithValue("_ImporteCaja", caja.ImporteCaja);
                    cmd.Parameters.AddWithValue("_Id_Usu", caja.IdUsu);
                    cmd.Parameters.AddWithValue("_TotalUti", caja.TotalUti);
                    cmd.Parameters.AddWithValue("_TipoPago", caja.TipoPago);
                    cmd.Parameters.AddWithValue("_GeneradoPor", caja.GeneradoPor);

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

                    Error.MensajeError("Capa Datos Caja", "Error al guardar", ex);

                    return false;
                }
            }

        }

        public async Task<bool> BD_ActualizarTotalCaja(EN_Caja caja)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actualizar_Total_Caja", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Nro_doc", caja.NroDoc);
                    cmd.Parameters.AddWithValue("_total", caja.ImporteCaja);
                    cmd.Parameters.AddWithValue("_TotalUtilidad", caja.TotalUti);
                    cmd.Parameters.AddWithValue("_TipoPago", caja.TipoPago);

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

                    Error.MensajeError("Capa Datos Caja", "Error al actualizar total de caja", ex);

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
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscador_MoviCaja_xValor", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_xvalor", val);

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
                Error.MensajeError("Capa Datos Caja", "Error al consultar", ex);
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

        public DataTable BD_ListarDia()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Cajas_delDia", con);
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
                Error.MensajeError("Capa Datos Caja", "Error al listar del dia", ex);
                return null;
            }
        }

        public DataTable BD_ListarPorFecha(DateTime fecha)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Cajas_del_Mes", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_fechas", fecha);

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
                Error.MensajeError("Capa Datos Caja", "Error al consultar", ex);
                return null;
            }
        }
    }
}
