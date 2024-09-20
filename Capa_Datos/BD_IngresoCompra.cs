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
    public class BD_IngresoCompra : BD_Conexion
    {
        public async Task<bool> BD_Registrar(EN_IngresoCompra en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Registrar_Compra", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_idCom", en.IdCom);
                    cmd.Parameters.AddWithValue("_Nro_Fac_Fisic", en.Nro_Fac_Fisico);
                    cmd.Parameters.AddWithValue("_IdProvee", en.IdProvee);
                    cmd.Parameters.AddWithValue("_SubTotal_Com", en.SubTotal_Com);
                    cmd.Parameters.AddWithValue("_FechaIngre", en.FechaIngre);
                    cmd.Parameters.AddWithValue("_TotalCompra", en.TotalCompra);
                    cmd.Parameters.AddWithValue("_IdUsu", en.IdUsu);
                    cmd.Parameters.AddWithValue("_ModalidadPago", en.ModalidadPago);
                    cmd.Parameters.AddWithValue("_TiempoEspera", en.TiempoEspera);
                    cmd.Parameters.AddWithValue("_FechaVence", en.FechaVence);
                    cmd.Parameters.AddWithValue("_EstadoIngre", en.EstadoIngre);
                    cmd.Parameters.AddWithValue("_RecibiConforme", en.RecibiConforme);
                    cmd.Parameters.AddWithValue("_Datos_Adicional", en.Datos_Adicional);
                    cmd.Parameters.AddWithValue("_Tipo_Doc_Compra", en.Tipo_Doc_Compra);

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

                    Error.MensajeError("Capa Datos IngresoCompra", "Error al guardar", ex);

                    return false;
                }
            }

        }

        public async Task<bool> BD_RegistrarDetalle(EN_DetalleIngresoCompra en)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("Sp_Insert_Detalle_ingreso", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_ingreso", en.Id_ingreso);
                    cmd.Parameters.AddWithValue("_Id_Pro", en.Id_Pro);
                    cmd.Parameters.AddWithValue("_Precio", en.Precio);
                    cmd.Parameters.AddWithValue("_Cantidad", en.Cantidad);
                    cmd.Parameters.AddWithValue("_Importe", en.Importe);

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
                    Error.MensajeError("Capa Datos IngresoCompra", "Error al guardar", ex);
                    return false;
                }
            }

        }

        public bool BD_Verificar_NroFisicoCompra(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("sp_validar_NroFisico_Compra", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Nro_Doc_fisico", val);


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
                    Error.MensajeError("Capa Datos IngresoCompra", "Error al validar", ex);
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
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscador_Gnral_deCompras", con);
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
                Error.MensajeError("Capa Datos IngresoCompra", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_Listar()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Leer_Todas_Facturas_Compras", con);
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
                Error.MensajeError("Capa Datos IngresoCompra", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_ListarDetalle(string id)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscar_FacturasCompras_Detalle", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_xvalor", id);

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
                Error.MensajeError("Capa Datos IngresoCompra", "Error al consultar", ex);
                return null;
            }
        }

        public DataTable BD_ListarPorFecha(string tipo, DateTime fecha)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                con.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Facturas_Ingresadas_alDia", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_tipo", tipo);
                da.SelectCommand.Parameters.AddWithValue("_fecha", fecha);

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
                Error.MensajeError("Capa Datos IngresoCompra", "Error al consultar", ex);
                return null;
            }
        }

        public async Task<bool> BD_Borrar(string val)
        {
            using (MySqlConnection connection = new MySqlConnection(Conectar()))
            using (MySqlCommand cmd = new MySqlCommand("SP_Borrar_Factura_Ingresada", connection))
            {
                try
                {
                    cmd.CommandTimeout = 20;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_Id_Fac", val);

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

                    Error.MensajeError("Capa Datos IngresoCompra", "Error al borrar", ex);

                    return false;
                }
            }

        }
    }
}
