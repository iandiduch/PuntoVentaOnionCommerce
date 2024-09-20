using Bunifu.Framework.Lib;
using Bunifu.Framework.UI;
using Bunifu.UI.WinForms;
using Onion_Commerce.Proveedores;
using Onion_Commerce.Utilitarios;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Onion_Commerce.Cliente
{
    public partial class Frm_Add_Cliente : BaseForm
    {

        public Frm_Add_Cliente()
        {
            InitializeComponent();
        }
        public string idCliente;
        public string modo;
        private async void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            CargarDistritos();

            if (modo == "Nuevo")
            {
                idCliente = RN_TipoDoc.RN_NroID(8);
                lbl_title.Text = "Registro de Cliente";
                txt_cuit.Enabled = true;

            }
            else if (modo == "Editar")
            {
                await Cargar_Cliente(idCliente);
                lbl_title.Text = "Modificación de Cliente";
                txt_cuit.Enabled = false;
            }

        }

        public string cuitCliente;
        private async Task Cargar_Cliente(string val)
        {
            RN_Cliente obj = new RN_Cliente();
            DataTable dato = new DataTable();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy cargando el cliente...";

            try
            {
                dato = await Task.Run(() => obj.RN_ListarPorValor(val, "Activo"));
                if (dato.Rows.Count > 0)
                {
                    cuitCliente = Convert.ToString(dato.Rows[0]["DNI"]);
                    txt_cuit.Text = Convert.ToString(dato.Rows[0]["DNI"]);
                    txt_nom.Text = Convert.ToString(dato.Rows[0]["Razon_Social_Nombres"]);
                    txt_ape.Text = Convert.ToString(dato.Rows[0]["Apellido"]);
                    txt_dire.Text = Convert.ToString(dato.Rows[0]["Direccion"]);
                    txt_provincia.Text = Convert.ToString(dato.Rows[0]["Distrito"]);
                    txt_localidad.Text = Convert.ToString(dato.Rows[0]["Localidad"]);
                    txt_CP.Text = Convert.ToString(dato.Rows[0]["Codigo_Postal"]);
                    txt_celular.Text = Convert.ToString(dato.Rows[0]["telefono"]);
                    txt_Correo.Text = Convert.ToString(dato.Rows[0]["e_mail"]);
                    time_FechaNac.Text = Convert.ToString(dato.Rows[0]["Fcha_Ncmnto_Anivsrio"]);
                    txt_limiteCredito.Text = Convert.ToString(dato.Rows[0]["Limit_Credit"]);
                }
                else
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al cargar el cliente", null);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al cargar el cliente", ex);
            }
            finally
            {
                pnl_load.Visible = false;
            }
        }

        private void CargarDistritos()
        {
            RN_Distrito obj = new RN_Distrito();
            DataTable dato = new DataTable();

            dato = obj.RN_Cargar_Todas_Distrito();
            if (dato.Rows.Count > 0)
            {
                txt_provincia.DataSource = dato;
                txt_provincia.DisplayMember = "Distrito";
                txt_provincia.ValueMember = "Id_Dis";
                txt_provincia.SelectedIndex = -1;
            }
        }

        private void pnl_titu_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitario obj = new Utilitario();

            if (e.Button == MouseButtons.Left)
            {
                obj.Mover_formulario(this);

            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }



        private async void Registrar_Cliente()
        {
            RN_Cliente obj = new RN_Cliente();
            EN_Cliente eN = new EN_Cliente();

            try
            {
                pnl_load.Visible = true;
                lbl_load.Text = "Espere... Estoy guardando el cliente...";

                eN.Idcliente = RN_TipoDoc.RN_NroID(8);
                idCliente = eN.Idcliente;

                eN.RazonSocial = txt_nom.Text;
                eN.Apellido = txt_ape.Text;
                eN.Dni = txt_cuit.Text;
                eN.Direccion = txt_dire.Text;
                eN.Localidad = txt_localidad.Text;
                eN.CodigoPostal = txt_CP.Text;
                eN.Telefono = txt_celular.Text;
                eN.Email = txt_Correo.Text;
                eN.IdDis = (int)txt_provincia.SelectedValue;
                eN.FechaAniver = time_FechaNac.Value;
                eN.Contacto = txt_nom.Text;
                eN.LimiteCred = Convert.ToDouble(txt_limiteCredito.Text);


                bool ok = await obj.RN_Registrar(eN);

                if (ok)
                {

                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(8);


                    pnl_load.Visible = false;

                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "El cliente se guardo exitosamente.";
                    listo.ShowDialog();
                    fil.Hide();

                    LimpiarForm();

                    this.Tag = "A";
                    this.Close();


                }
                else
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el cliente", null);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al guardar el cliente", ex);
            }

            pnl_load.Visible = false;
        }

        private async void Editar_Cliente()
        {
            RN_Cliente obj = new RN_Cliente();
            EN_Cliente eN = new EN_Cliente();

            try
            {
                pnl_load.Visible = true;
                lbl_load.Text = "Espere... Estoy modificando el cliente...";


                eN.Idcliente = idCliente;
                eN.RazonSocial = txt_nom.Text;
                eN.Apellido = txt_ape.Text;
                eN.Dni = txt_cuit.Text;
                eN.Direccion = txt_dire.Text;
                eN.Localidad = txt_localidad.Text;
                eN.CodigoPostal = txt_CP.Text;
                eN.Telefono = txt_celular.Text;
                eN.Email = txt_Correo.Text;
                eN.IdDis = (int)txt_provincia.SelectedValue;
                eN.FechaAniver = time_FechaNac.Value;
                eN.Contacto = txt_nom.Text;
                eN.LimiteCred = Convert.ToDouble(txt_limiteCredito.Text);


                bool ok = await obj.RN_Editar(eN);

                if (ok)
                {

                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(8);


                    pnl_load.Visible = false;

                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "El cliente se modifico exitosamente.";
                    listo.ShowDialog();
                    fil.Hide();

                    LimpiarForm();

                    this.Tag = "A";
                    this.Close();


                }
                else
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al modificar el cliente", null);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ocurrio un error al modificar el cliente", ex);
            }
            finally
            {
                pnl_load.Visible = false;
            }


        }



        private void LimpiarForm()
        {

            idCliente = null;

            txt_cuit.Text = "";
            txt_nom.Text = "";
            txt_ape.Text = "";
            txt_dire.Text = "";
            txt_provincia.SelectedIndex = -1;
            txt_localidad.Text = "";
            txt_CP.Text = "";
            txt_celular.Text = "";
            txt_Correo.Text = "";
            txt_limiteCredito.Text = "0";


        }

        private void btn_listo_Click(object sender, EventArgs e)
        {
            if (Validar_Txt() == true)
            {
                if (modo == "Nuevo")
                {
                    Registrar_Cliente();

                }
                else if (modo == "Editar")
                {
                    Editar_Cliente();
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        #region VALIDACIONES

        private bool Validar_Txt()
        {
            Frm_Filtro fil = new Frm_Filtro();
            frm_advertencia warning = new frm_advertencia();

            if (txt_cuit.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el dni o cuit!", null);

                txt_cuit.Focus();

                return false;
            }

            if (modo == "Nuevo")
            {
                RN_Cliente obj = new RN_Cliente();

                if (obj.RN_Verificar_NroDni(txt_cuit.Text) == true)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este cliente ya existe!", null);

                    txt_cuit.Focus();

                    return false;
                }
            } else if (modo == "Editar")
            {
                RN_Cliente obj = new RN_Cliente();

                if (obj.RN_Verificar_NroDni(txt_cuit.Text) == true && txt_cuit.Text != cuitCliente)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este cliente ya existe!", null);

                    txt_cuit.Focus();

                    return false;
                }
            }

            if (txt_nom.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el nombre del cliente!", null);

                txt_nom.Focus();

                return false;
            }

            if (txt_ape.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el apellido del cliente!", null);

                txt_ape.Focus();

                return false;
            }

            if (txt_provincia.SelectedIndex == -1)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa la Provincia!", null);

                txt_provincia.Focus();

                return false;
            }

            if (txt_localidad.Text.Trim().Length < 2)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa la Localidad!", null);

                txt_provincia.Focus();

                return false;
            }

            return true;
        }

        private void txt_Enteros_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilitario codigoASCII = new Utilitario();

            e.KeyChar = Convert.ToChar(codigoASCII.Solo_NumerosEnteros(e.KeyChar));
        }



        #endregion FIN VALIDACIONES

    }
}
