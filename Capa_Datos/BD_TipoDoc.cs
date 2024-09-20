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
    public class BD_TipoDoc : BD_Conexion
    {
        public static string BD_NroID(int idTipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar2()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Listado_Tipo", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Tipo", idTipo);


                    connection.Open();
                    string NroDoc = Convert.ToString(cmd.ExecuteScalar());
                    connection.Close();

                    return NroDoc;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos TipoDoc", "Error al listar", ex);
                    return "";
                }
            }
        }


        public static async Task BD_Actualizar_Sig_NroCorrelativo(int idTipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar2()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actualiza_Tipo_Doc", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Tipo", idTipo);


                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos TipoDoc", "Error al modificar", ex);

                }
            }
        }

        //sig nro correlativo producto
        public static async Task BD_Actualizar_Sig_NroCorrelativo_Producto(int idTipo)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar2()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Actualiza_Tipo_Prodcto", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Tipo", idTipo);


                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Error.MensajeError("Capa Datos TipoDoc", "Error al modificar", ex);

                }
            }
        }

        //public async Task<bool> BD_Registrar(string Id_KrDx, string Id_Prod, string Id_Prov)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Conectar()))
        //    using (MySqlCommand cmd = new MySqlCommand("sp_crear_kardex", connection))
        //    {
        //        try
        //        {
        //            cmd.CommandTimeout = 20;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("_idkardex", Id_KrDx);
        //            cmd.Parameters.AddWithValue("_idprod", Id_Prod);
        //            cmd.Parameters.AddWithValue("_idprovee", Id_Prov);


        //            connection.Open();
        //            await cmd.ExecuteNonQueryAsync();
        //            connection.Close();

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            if (connection.State == ConnectionState.Open)
        //            {
        //                connection.Close();
        //            }
        //            ErrorLog.MensajeError
        //            return false;
        //        }
        //    }

        //}




    }
}
