using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Onion_Commerce.Utilitarios
{
    public partial class frm_advertencia : BaseForm
    {
        public frm_advertencia()
        {
            InitializeComponent();
        }

        private void btn_si_Click(object sender, EventArgs e)
        {
            this.Tag = "Si";
            this.Dispose();
            bunifuTransition1.HideSync(this);
        }

        private void Frm_delete_Load(object sender, EventArgs e)
        {
            bunifuTransition1.ShowSync(this);

        }

    }
}
