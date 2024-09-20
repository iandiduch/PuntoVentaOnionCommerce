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
    public class BD_Distrito : BD_Conexion
    {
        public void BD_Registrar_Distrito(string nomDistrito)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_addDistrito", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_distrito", nomDistrito);

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
                Error.MensajeError("Capa Datos Distrito", "Error al guardar", ex);
            }
        }

        public void BD_Editar_Distrito(int idDistrito, string nomDistrito)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_Editar_Distrito", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idDis", idDistrito);
                cmd.Parameters.AddWithValue("_nomdis", nomDistrito);

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
                Error.MensajeError("Capa Datos Distrito", "Error al guardar", ex);
            }
        }

        //Consultar
        public DataTable BD_Cargar_Todas_Distrito()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Todos_Distritos", con);
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
                Error.MensajeError("Capa Datos Distrito", "Error al consultar", ex);
                return null;
            }
        }

        //eliminar
        public void BD_Eliminar_Distrito(int idDistrito)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_eliminar_distrito", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idDis", idDistrito);

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
                Error.MensajeError("Capa Datos Distrito", "Error al eliminar", ex);
            }
        }

    }
}
