using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Utilitarios
{
    public partial class frm_delete : BaseForm
    {
        public frm_delete()
        {
            InitializeComponent();
        }

        private void btn_si_Click(object sender, EventArgs e)
        {
            this.Tag = "Si";
            this.Close();
            bunifuTransition1.HideSync(this);
        }

        private void Frm_delete_Load(object sender, EventArgs e)
        {
            bunifuTransition1.ShowSync(this);
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
            bunifuTransition1.HideSync(this);
        }

        private void frm_delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btn_no_Click(sender, e);
            }
            
            if (e.KeyCode == Keys.Enter)
            {
                btn_si_Click(sender, e);
            }
        }
    }
}
