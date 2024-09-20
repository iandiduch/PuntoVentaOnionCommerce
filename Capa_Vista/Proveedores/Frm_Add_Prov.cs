using Onion_Commerce.Cliente;
using Onion_Commerce.Utilitarios;
using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Proveedores
{
    public partial class Frm_Add_Prov : BaseForm
    {
        public Frm_Add_Prov()
        {
            InitializeComponent();
        }
        public string idProveedor;
        public string modo;
        private string cuitProveedor;
        private async void Frm_Reg_Prov_Load(object sender, EventArgs e)
        {
            if (modo == "Nuevo")
            {
                idProveedor = RN_TipoDoc.RN_NroID(5);
                lbl_title.Text = "Registro de Proveedor";
                txt_cuit.Enabled = true;
            }
            else if (modo == "Editar")
            {
                await Buscar_Proveedor(idProveedor);
                lbl_title.Text = "Modificación de Proveedor";
                txt_cuit.Enabled = false;
            }
            

        }

        private async Task Buscar_Proveedor(string id)
        {
            RN_Proveedor obj = new RN_Proveedor();
            DataTable dato = new DataTable();

            try
            {
                dato = await Task.Run(() => obj.RN_Listar_PorValor(id));
                if (dato.Rows.Count > 0)
                {
                    idProveedor = Convert.ToString(dato.Rows[0]["IDPROVEE"]);
                    txt_nom.Text = Convert.ToString(dato.Rows[0]["NOMBRE"]);
                    txt_dire.Text = Convert.ToString(dato.Rows[0]["DIRECCION"]);
                    txt_tel.Text = Convert.ToString(dato.Rows[0]["TELEFONO"]);
                    txt_razon.Text = Convert.ToString(dato.Rows[0]["RUBRO"]);
                    txt_cuit.Text = Convert.ToString(dato.Rows[0]["RUC"]);
                    cuitProveedor = Convert.ToString(dato.Rows[0]["RUC"]);
                    txt_correo.Text = Convert.ToString(dato.Rows[0]["CORREO"]);
                    txt_contacto.Text = Convert.ToString(dato.Rows[0]["CONTACTO"]);
                    xFotoRuta = Convert.ToString(dato.Rows[0]["FOTO_LOGO"]);

                    img_fotologo.Load(xFotoRuta);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Error al cargar el proveedor", ex);
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

        string xFotoRuta = Application.StartupPath + @"\user.png";
        private void lbl_examinar_Click(object sender, EventArgs e)
        {
            var FilePath = string.Empty;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    xFotoRuta = openFileDialog1.FileName;
                    img_fotologo.Load(xFotoRuta);
                }
            }
            catch (Exception ex)
            {
                img_fotologo.Load(Application.StartupPath + @"\user.png");
                xFotoRuta = Application.StartupPath + @"\user.png";
                ErrorLog.MensajeError("¡Advertencia!", "Error al guardar la imagen", ex);
            }
        }

        private bool Validar_Txt()
        {
            Frm_Filtro fil = new Frm_Filtro();
            frm_advertencia warning = new frm_advertencia();

            if (modo == "Nuevo")
            {
                RN_Proveedor obj = new RN_Proveedor();

                if (obj.RN_Verificar_NroDni(txt_cuit.Text) == true)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este cliente ya existe!", null);

                    txt_cuit.Focus();

                    return false;
                }
            }
            else if (modo == "Editar")
            {
                RN_Proveedor obj = new RN_Proveedor();

                if (obj.RN_Verificar_NroDni(txt_cuit.Text) == true && txt_cuit.Text != cuitProveedor)
                {
                    ErrorLog.MensajeError("¡Advertencia!", "Este cliente ya existe!", null);

                    txt_cuit.Focus();

                    return false;
                }
            }

            if (txt_nom.Text.Trim().Length < 2)
            {

                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el nombre del proveedor", null);

                return false;
            }

            if (txt_cuit.Text.Trim().Length < 8)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Ingresa el cuit/dni del proveedor", null);
                return false;
            }

            return true;
        }



        

        private async void Registrar_Prov()
        {
            RN_Proveedor obj = new RN_Proveedor();
            EN_Proveedor eN = new EN_Proveedor();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy guardando el proveedor...";

            try
            {
                eN.Idproveedor = RN_TipoDoc.RN_NroID(5);
                idProveedor = eN.Idproveedor;

                eN.Nombre = txt_nom.Text;
                eN.Direccion = txt_dire.Text;
                eN.Telefono = txt_tel.Text;
                eN.Razonsocial = txt_razon.Text;
                eN.Cuit = txt_cuit.Text;
                eN.Correo = txt_correo.Text;
                eN.Contacto = txt_contacto.Text;
                eN.Fotologo = xFotoRuta;

                bool ok = await obj.RN_Registrar(eN);

                if (ok)
                {
                    RN_TipoDoc.RN_Actualizar_Sig_NroCorrelativo(5);


                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "El proveedor se registro exitosamente.";
                    listo.ShowDialog();
                    fil.Hide();

                    LimpiarForm();

                    this.Tag = "A";
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Error al guardar el proveedor", ex);
            }
            finally
            {
                pnl_load.Visible = false;
            }
        }

        private async void Editar_Prov()
        {
            RN_Proveedor obj = new RN_Proveedor();
            EN_Proveedor eN = new EN_Proveedor();

            pnl_load.Visible = true;
            lbl_load.Text = "Espere... Estoy guardando el proveedor...";

            try
            {
                eN.Idproveedor = idProveedor;
                eN.Nombre = txt_nom.Text;
                eN.Direccion = txt_dire.Text;
                eN.Telefono = txt_tel.Text;
                eN.Razonsocial = txt_razon.Text;
                eN.Cuit = txt_cuit.Text;
                eN.Correo = txt_correo.Text;
                eN.Contacto = txt_contacto.Text;
                eN.Fotologo = xFotoRuta;

                bool ok = await obj.RN_Editar(eN);

                if (ok)
                {
                    Frm_Filtro fil = new Frm_Filtro();
                    frm_listo listo = new frm_listo();
                    fil.Show();
                    listo.lbl_title.Text = "¡Listo!";
                    listo.lbl_msm.Text = "El proveedor se edito exitosamente.";
                    listo.ShowDialog();
                    fil.Hide();

                    LimpiarForm();

                    this.Tag = "A";
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.MensajeError("¡Advertencia!", "Error al guardar el proveedor", ex);
            }
            finally
            {
                pnl_load.Visible = false;
            }
        }

        private void LimpiarForm()
        {
            txt_tel.Text = "";
            txt_razon.Text = "";
            txt_nom.Text = "";
            idProveedor = "";
            txt_dire.Text = "";
            txt_cuit.Text = "";
            txt_correo.Text = "";
            txt_contacto.Text = "";

        }

        private void btn_listo_Click(object sender, EventArgs e)
        {
            if (Validar_Txt() == true)
            {
                if (modo == "Nuevo")
                {
                    Registrar_Prov();
                }
                else if (modo == "Editar")
                {
                    Editar_Prov();
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void txt_Enteros_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilitario codigoASCII = new Utilitario();

            e.KeyChar = Convert.ToChar(codigoASCII.Solo_NumerosEnteros(e.KeyChar));
        }

    }
}
