using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Onion_Commerce.Cliente;
using Onion_Commerce.Compras;
using Onion_Commerce.Productos;
using Onion_Commerce.Proveedores;
using Onion_Commerce.Utilitarios;

namespace Onion_Commerce
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Frm_Login());
        }
    }
}
