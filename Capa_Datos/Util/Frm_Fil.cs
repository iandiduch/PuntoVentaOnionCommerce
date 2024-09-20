using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos.Util
{
    public partial class Frm_Fil : Form
    {
        public Frm_Fil()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;



        }

        public DialogResult ShowDialogWithOwner(Form owner)
        {
            this.Owner = owner;
            this.ShowInTaskbar = false;
            this.Show();
            return this.DialogResult;
        }
    }
}
