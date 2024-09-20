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
    public class BD_Cliente : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Cliente en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Registrar_Cliente", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_idcliente", en.Idcliente);
                    cmd.Parameters.AddWithValue("_razonsocial", en.RazonSocial);
                    cmd.Parameters.AddWithValue("_apellido", en.Apellido);
                    cmd.Parameters.AddWithValue("_dni", en.Dni);
                    cmd.Parameters.AddWithValue("_direccion", en.Direccion);
                    cmd.Parameters.AddWithValue("_localidad", en.Localidad);
                    cmd.Parameters.AddWithValue("_cp", en.CodigoPostal);
                    cmd.Parameters.AddWithValue("_telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("_email", en.Email);
                    cmd.Parameters.AddWithValue("_idDis", en.IdDis);
                    cmd.Parameters.AddWithValue("_fechaAniver", en.FechaAniver);
                    cmd.Parameters.AddWithValue("_contacto", en.Contacto);
                    cmd.Parameters.AddWithValue("_limiteCred", en.LimiteCred);



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
                    Error.MensajeError("Capa Datos Cliente", "Error al guardar", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_Editar(EN_Cliente en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Modificar_Cliente", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("_idcliente", en.Idcliente);
                    cmd.Parameters.AddWithValue("_razonsocial", en.RazonSocial);
                    cmd.Parameters.AddWithValue("_apellido", en.Apellido);
                    cmd.Parameters.AddWithValue("_dni", en.Dni);
                    cmd.Parameters.AddWithValue("_direccion", en.Direccion);
                    cmd.Parameters.AddWithValue("_localidad", en.Localidad);
                    cmd.Parameters.AddWithValue("_cp", en.CodigoPostal);
                    cmd.Parameters.AddWithValue("_telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("_email", en.Email);
                    cmd.Parameters.AddWithValue("_idDis", en.IdDis);
                    cmd.Parameters.AddWithValue("_fechaAniver", en.FechaAniver);
                    cmd.Parameters.AddWithValue("_contacto", en.Contacto);
                    cmd.Parameters.AddWithValue("_limiteCred", en.LimiteCred);



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
                    Error.MensajeError("Capa Datos Cliente", "Error al editar", ex);
                    return false;
                }
            }

        }

        public DataTable BD_Listar(string estado)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Todos_Clientes", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_estado", estado);

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
                Error.MensajeError("Capa Datos Cliente", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_ListarPorValor(string valor, string estado)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscar_Cliente_porValor", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_Valor", valor);
                da.SelectCommand.Parameters.AddWithValue("_estado", estado);

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
                Error.MensajeError("Capa Datos Cliente", "Error al consultar", ex);
                return null;
            }
        }
        public bool BD_Verificar_NroDni(string dni)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Validar_NroDNI", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_dni", dni);


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

        public async Task<bool> BD_Eliminar(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Eliminar_Cliente", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idcliente", val);

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
                    Error.MensajeError("Capa Datos Cliente", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_Baja(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_DarBajar_Cliente", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idcliente", val);

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
                    Error.MensajeError("Capa Datos Cliente", "Error al dar baja", ex);
                    return false;
                }
            }
        }
    }
}
