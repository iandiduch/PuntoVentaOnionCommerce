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
    public class BD_Proveedor : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_Proveedor en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_registrar_Proveedor", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idproveedor", en.Idproveedor);
                    cmd.Parameters.AddWithValue("_nombre", en.Nombre);
                    cmd.Parameters.AddWithValue("_direccion", en.Direccion);
                    cmd.Parameters.AddWithValue("_telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("_rubro", en.Razonsocial);
                    cmd.Parameters.AddWithValue("_ruc", en.Cuit);
                    cmd.Parameters.AddWithValue("_correo", en.Correo);
                    cmd.Parameters.AddWithValue("_contacto", en.Contacto);
                    cmd.Parameters.AddWithValue("_fotologo", en.Fotologo);

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
                    Error.MensajeError("Capa Datos Proveedor", "Error al guardar", ex);
                    return false;
                }
            }

        }

        public async Task<bool> BD_Editar(EN_Proveedor en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Modificar_Proveedor", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idproveedor", en.Idproveedor);
                    cmd.Parameters.AddWithValue("_nombre", en.Nombre);
                    cmd.Parameters.AddWithValue("_direccion", en.Direccion);
                    cmd.Parameters.AddWithValue("_telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("_rubro", en.Razonsocial);
                    cmd.Parameters.AddWithValue("_ruc", en.Cuit);
                    cmd.Parameters.AddWithValue("_correo", en.Correo);
                    cmd.Parameters.AddWithValue("_contacto", en.Contacto);
                    cmd.Parameters.AddWithValue("_fotologo", en.Fotologo);

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
                    Error.MensajeError("Capa Datos Proveedor", "Error al modificar", ex);
                    return false;
                }
            }
        }

        public async Task<bool> BD_Eliminar(string val)
        {

            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_eliminar_Proveedor", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idprov", val);

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
                    Error.MensajeError("Capa Datos Proveedor", "Error al eliminar", ex);
                    return false;
                }
            }
        }

        public bool BD_Verificar_NroDni(string dni)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_Validar_NroDNI_Prov", connection))
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
                    Error.MensajeError("Capa Datos Proveedor", "Error al validar", ex);
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
                MySqlDataAdapter da = new MySqlDataAdapter("sp_listar_Todos_Proveedores", con);
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
                Error.MensajeError("Capa Datos Proveedor", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_Listar_PorValor(string val)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_buscar_proveedor_porvalor", con);
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
                Error.MensajeError("Capa Datos Proveedor", "Error al consultar", ex);
                return null;
            }
        }

    }
}
