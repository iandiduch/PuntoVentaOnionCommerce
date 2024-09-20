using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Capa_Datos.Util;

namespace Capa_Datos
{
    public class BD_Categoria : BD_Conexion
    {
        public void BD_Registrar_Categoria(string nomCateg)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_registrar_Categoria", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_nombrecat", nomCateg);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                

            } catch (Exception ex)
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close() ;
                }
                Error.MensajeError("Capa Datos Categoria", "Error al guardar", ex);
            }
        }

        public void BD_Editar_Categoria(int idCateg, string nomCateg)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_modificar_Categoria", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idcat", idCateg);
                cmd.Parameters.AddWithValue("_nombrecat", nomCateg);

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
                Error.MensajeError("Capa Datos Categoria", "Error al modificar", ex);
            }
        }

        //Consultar
        public DataTable BD_Cargar_Todas_Categorias()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_listar_todas_categorias", con);
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
                Error.MensajeError("Capa Datos Categoria", "Error al consultar", ex);
                return null;
            }
        }

        //eliminar

        public void BD_Eliminar_Categoria(int idCateg)
        {
            MySqlConnection conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("sp_eliminar_Categoria", conn);
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idcat", idCateg);

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
                Error.MensajeError("Capa Datos Categoria", "Error al eliminar", ex);
            }
        }

    }
}
