using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Pedido
    {
        // Campos privados
        private string _id_Ped;
        private string _Id_Cliente;
        private DateTime _Fecha;
        private double _SubTotal;
        private double _IgvPed;
        private double _TotalPed;
        private int _id_Usu;
        private double _TotalGancia;

        // Propiedades públicas con operadores lambda
        public string IdPed
        {
            get => _id_Ped;
            set => _id_Ped = value;
        }

        public string IdCliente
        {
            get => _Id_Cliente;
            set => _Id_Cliente = value;
        }

        public DateTime Fecha
        {
            get => _Fecha;
            set => _Fecha = value;
        }

        public double SubTotal
        {
            get => _SubTotal;
            set => _SubTotal = value;
        }

        public double IgvPed
        {
            get => _IgvPed;
            set => _IgvPed = value;
        }

        public double TotalPed
        {
            get => _TotalPed;
            set => _TotalPed = value;
        }

        public int IdUsu
        {
            get => _id_Usu;
            set => _id_Usu = value;
        }

        public double TotalGancia
        {
            get => _TotalGancia;
            set => _TotalGancia = value;
        }
    }

}
