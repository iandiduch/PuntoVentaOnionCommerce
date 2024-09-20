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

namespace Onion_Commerce.Informe
{
    public partial class Frm_Print : Form
    {
        public Frm_Print()
        {
            InitializeComponent();
        }

        public string id;
        public string tipo;

        private void Frm_Print_Load(object sender, EventArgs e)
        { 
            switch(tipo)
            {
                case "cotizacion":
                    Crear_Impresion_Cotizacion();
                    break;
                case "cotizacionSinIVA":
                    Crear_Impresion_CotizacionSinIva();
                    break;
                default:
                    ErrorLog.MensajeError("Error", tipo + " no es un tipo de impresión valido", null);
                    this.Close();
                    break;
            }
        }

        private async void Crear_Impresion_Cotizacion()
        {
            RN_Cotizacion obj = new RN_Cotizacion();
            DataTable da = new DataTable();

            da = await Task.Run( ()=>obj.RN_ImprimirCotizacion(id));
            IF_Cotizacion rpt = new IF_Cotizacion();
            this.crystalReportViewer1.ReportSource = rpt;
            rpt.SetDataSource(da);
            rpt.Refresh();
            crystalReportViewer1.ReportSource = rpt;
        }

        private async void Crear_Impresion_CotizacionSinIva()
        {
            RN_Cotizacion obj = new RN_Cotizacion();
            DataTable da = new DataTable();

            da = await Task.Run(() => obj.RN_ImprimirCotizacion(id));
            IF_CotizacionSinIVA rpt = new IF_CotizacionSinIVA();
            this.crystalReportViewer1.ReportSource = rpt;
            rpt.SetDataSource(da);
            rpt.Refresh();
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
