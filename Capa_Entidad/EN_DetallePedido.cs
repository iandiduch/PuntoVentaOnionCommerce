using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_DetallePedido
    {
        // Campos privados
        private string _id_Ped;
        private string _Id_Pro;
        private double _Precio;
        private double _Cantidad;
        private double _Importe;
        private string _Tipo_Prod;
        private string _Und_Medida;
        private double _Utilidad_Unit;
        private double _TotalUtilidad;

        // Propiedades públicas con operadores lambda
        public string IdPed
        {
            get => _id_Ped;
            set => _id_Ped = value;
        }

        public string IdPro
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

        public string TipoProd
        {
            get => _Tipo_Prod;
            set => _Tipo_Prod = value;
        }

        public string UndMedida
        {
            get => _Und_Medida;
            set => _Und_Medida = value;
        }

        public double UtilidadUnit
        {
            get => _Utilidad_Unit;
            set => _Utilidad_Unit = value;
        }

        public double TotalUtilidad
        {
            get => _TotalUtilidad;
            set => _TotalUtilidad = value;
        }
    }
}
