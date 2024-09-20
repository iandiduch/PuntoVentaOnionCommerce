using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Util
{
    public static class Error
    {
        public static void MensajeError(string title, string msg, Exception ex = null)
        {
            Frm_Fil fil = new Frm_Fil();
            frm_adver warning = new frm_adver();
            fil.Show();
            warning.lbl_title.Text = title;
            warning.lbl_msm.Text = ex != null ? msg + ": " + ex.Message : msg;
            warning.ShowDialog();
            fil.Hide();
        }
    }
}
