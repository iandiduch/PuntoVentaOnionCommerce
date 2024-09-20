using Onion_Commerce.Utilitarios;
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

namespace Onion_Commerce
{
    public partial class Frm_Login : BaseForm
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btn_NoVerContraseña_Click(object sender, EventArgs e)
        {
            btn_VerContraseña.Visible = true;
            btn_NoVerContraseña.Visible = false;
            txt_pass.PasswordChar = '*';
        }

        private void btn_VerContraseña_Click(object sender, EventArgs e)
        {
            btn_VerContraseña.Visible = false;
            btn_NoVerContraseña.Visible = true;
            txt_pass.PasswordChar = '\0';
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnl_title_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitario obj = new Utilitario();
            if(e.Button == MouseButtons.Left)
            {
                obj.Mover_formulario(this);
            }
        }


        private bool Validar_Txt()
        {
            if(txt_usuario.Text.Trim().Length < 1) { ErrorLog.MensajeError("Error", "Ingresa el nombre de usuario", null); txt_usuario.Focus(); return false; }

            if (txt_pass.Text.Trim().Length < 1) { ErrorLog.MensajeError("Error", "Ingresa la contraseña", null); txt_pass.Focus(); return false; }

            return true;
        }
        int veces = 0;
        private async void Login()
        {
            RN_Usuario obj = new RN_Usuario();
            DataTable dato = new DataTable();

            string usu = txt_usuario.Text;
            string pass = txt_pass.Text;

            if (Validar_Txt() == false) return;

            pnl_load.Visible = true;

            if(await obj.RN_Login(usu, pass) == true)
            {
                dato = await Task.Run(() => obj.RN_Buscar(usu));

                if(dato.Rows.Count > 0 )
                {
                    DataRow dr = dato.Rows[0];
                    Cls_Libreria.IdRol = dr["Id_Rol"].ToString();
                    Cls_Libreria.IdUsu = dr["Id_Usu"].ToString();
                    Cls_Libreria.Rol = dr["Rol"].ToString();
                    Cls_Libreria.Nombre = dr["Nombres"].ToString();
                    Cls_Libreria.Foto = dr["Ubicacion_Foto"].ToString();
                    Cls_Libreria.Usuario = dr["Usuario"].ToString();
                }

                pnl_load.Visible = false;
                this.Hide();
                Frm_Principal pri = new Frm_Principal();
                pri.ShowDialog();
            }
            else
            {
                veces++;

                pnl_load.Visible = false;

                txt_pass.Text = "";
                txt_usuario.Text = "";
                txt_pass.BorderColorIdle = Color.Red;
                txt_usuario.BorderColorIdle = Color.Red;

                ErrorLog.MensajeError("Error", "¡Usuario o Contraseña Incorrectos!", null);

                if(veces > 2 )
                {
                    ErrorLog.MensajeError("Error", "Has superado los intentos permitidos!", null);
                    Application.Exit();
                } else
                {
                    txt_usuario.Focus();
                    txt_pass.BorderColorIdle = Color.DodgerBlue;
                    txt_usuario.BorderColorIdle = Color.DodgerBlue;
                }
                
            }

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txt_usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txt_pass.Focus();
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ingresar_Click(sender, e);
            }
        }
    }
}
