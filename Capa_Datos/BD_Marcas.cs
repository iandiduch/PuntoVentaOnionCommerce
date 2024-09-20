using MySql.Data.MySqlClient;
using Capa_Datos.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class BD_Marcas : BD_Conexion
    {
        public void BD_Registrar_Marca(string nomMarca)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_addMarca", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_marca", nomMarca);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Error.MensajeError("Capa Datos Marca", "Error al guardar", ex);
            }
        }

        public void BD_Editar_Marca(int idMarca, string nomMarca)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_Editar_Marca", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idmar", idMarca);
                cmd.Parameters.AddWithValue("_nom_marca", nomMarca);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Error.MensajeError("Capa Datos Marca", "Error al guardar", ex);
            }
        }

        //Consultar
        public DataTable BD_Cargar_Todas_Marca()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Todos_Marcas", con);
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
                Error.MensajeError("Capa Datos Marca", "Error al consultar", ex);
                return null;
            }
        }

        //eliminar
        public void BD_Eliminar_Marca(int idMarca)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_eliminar_Marca", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idmar", idMarca);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Error.MensajeError("Capa Datos Marca", "Error al eliminar", ex);
            }
        }

    }
}
