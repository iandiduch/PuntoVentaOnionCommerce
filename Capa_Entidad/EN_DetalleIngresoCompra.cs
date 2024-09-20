using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_DetalleIngresoCompra
    {

        private string _Id_ingreso;
        private string _Id_Pro;
        private double _Precio;
        private double _Cantidad;
        private double _Importe;

        public string Id_ingreso
        {
            get => _Id_ingreso;
            set => _Id_ingreso = value;
        }

        public string Id_Pro
        {
            get => _Id_Pro;
            set => _Id_Pro = value;
        }

        public double Precio
        {
            get => _Precio;
            set => _Precio = value;
        }

        public double Cantidad
        {
            get => _Cantidad;
            set => _Cantidad = value;
        }

        public double Importe
        {
            get => _Importe;
            set => _Importe = value;
        }
    }
}
