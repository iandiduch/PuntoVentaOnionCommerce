using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Kardex
    {
        // Atributos de la clase

        // ID KrDx
        private string _Id_KrDx;
        public string Id_KrDx { get => _Id_KrDx; set => _Id_KrDx = value; }

        // Item
        private int _Item;
        public int Item { get => _Item; set => _Item = value; }

        // Doc Soport
        private string _Doc_Soport;
        public string Doc_Soport { get => _Doc_Soport; set => _Doc_Soport = value; }

        // Det Operacion
        private string _Det_Operacion;
        public string Det_Operacion { get => _Det_Operacion; set => _Det_Operacion = value; }

        // Cantidad In
        private double _Cantidad_In;
        public double Cantidad_In { get => _Cantidad_In; set => _Cantidad_In = value; }

        // Precio Unitario In
        private double _Precio_Unt_In;
        public double Precio_Unt_In { get => _Precio_Unt_In; set => _Precio_Unt_In = value; }

        // Costo Total In
        private double _Costo_Total_In;
        public double Costo_Total_In { get => _Costo_Total_In; set => _Costo_Total_In = value; }

        // Cantidad Out
        private double _Cantidad_Out;
        public double Cantidad_Out { get => _Cantidad_Out; set => _Cantidad_Out = value; }

        // Precio Unitario Out
        private double _Precio_Unt_Out;
        public double Precio_Unt_Out { get => _Precio_Unt_Out; set => _Precio_Unt_Out = value; }

        // Importe Total Out
        private double _Importe_Total_Out;
        public double Importe_Total_Out { get => _Importe_Total_Out; set => _Importe_Total_Out = value; }

        // Cantidad Saldo
        private double _Cantidad_Saldo;
        public double Cantidad_Saldo { get => _Cantidad_Saldo; set => _Cantidad_Saldo = value; }

        // Promedio
        private double _Promedio;
        public double Promedio { get => _Promedio; set => _Promedio = value; }

        // Costo Total Saldo
        private double _Costo_Total_Saldo;
        public double Costo_Total_Saldo { get => _Costo_Total_Saldo; set => _Costo_Total_Saldo = value; }

    }
}
