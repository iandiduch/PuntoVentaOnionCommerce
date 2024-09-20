using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_Commerce.Utilitarios
{
    public static class ErrorLog
    {
        public static void MensajeError(string title, string msg, Exception ex = null)
        {
            Frm_Filtro fil = new Frm_Filtro();
            frm_advertencia warning = new frm_advertencia();
            fil.Show();
            warning.lbl_title.Text = title;
            warning.lbl_msm.Text = ex != null ? msg + ": " + ex.Message : msg;
            warning.ShowDialog();
            fil.Hide();
        }
    }
}
