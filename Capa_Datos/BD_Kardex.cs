using MySql.Data.MySqlClient;
using Capa_Datos.Util;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Capa_Datos
{
    public class BD_Kardex : BD_Conexion
    {
        //crear kardex
        public async Task<bool> BD_Registrar(string Id_KrDx, string Id_Prod, string Id_Prov)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_crear_kardex", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idkardex", Id_KrDx);
                    cmd.Parameters.AddWithValue("_idprod", Id_Prod);
                    cmd.Parameters.AddWithValue("_idprovee", Id_Prov);


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
                    Error.MensajeError("Capa Datos Kardex", "Error al guardar", ex);
                    return false;
                }
            }

        }

        //detalle de kardex
        public async Task<bool> BD_RegistrarDetalle(EN_Kardex en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_registrar_detalle_kardex", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Krdx", en.Id_KrDx);
                    cmd.Parameters.AddWithValue("_Item", en.Item);
                    cmd.Parameters.AddWithValue("_Doc_Soport", en.Doc_Soport);
                    cmd.Parameters.AddWithValue("_Det_Operacion", en.Det_Operacion);

                    // Entrada
                    cmd.Parameters.AddWithValue("_Cantidad_In", en.Cantidad_In);
                    cmd.Parameters.AddWithValue("_Precio_Unt_In", en.Precio_Unt_In);
                    cmd.Parameters.AddWithValue("_Costo_Total_In", en.Costo_Total_In);

                    // Salida
                    cmd.Parameters.AddWithValue("_Cantidad_Out", en.Cantidad_Out);
                    cmd.Parameters.AddWithValue("_Precio_Unt_Out", en.Precio_Unt_Out);
                    cmd.Parameters.AddWithValue("_Importe_Total_Out", en.Importe_Total_Out);

                    // Saldo
                    cmd.Parameters.AddWithValue("_Cantidad_Saldo", en.Cantidad_Saldo);
                    cmd.Parameters.AddWithValue("_Promedio", en.Promedio);
                    cmd.Parameters.AddWithValue("_Costo_Total_Saldo", en.Costo_Total_Saldo);


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
                    Error.MensajeError("Capa Datos Kardex", "Error al guardar", ex);
                    return false;
                }
            }

        }

        //validar
        public bool BD_Verificar_Producto_siTieneKardex(string Id_Prod)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Ver_sihay_Kardex", connection))
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
                    } else
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
                    Error.MensajeError("Capa Datos Kardex", "Error al validar", ex);
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
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscador_DeKardex_Principal_yDetalle", con);
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
                Error.MensajeError("Capa Datos Kardex", "Error al consultar", ex);
                return null;
            }
        }

    }
}
