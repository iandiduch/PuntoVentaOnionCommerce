using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Cotizacion
    {
        // Campos privados
        private string _Id_Cotiza;
        private string _Id_Ped;
        private DateTime _FechaCoti;
        private DateTime _Vigencia;
        private double _TotalCotiza;
        private string _Condiciones;
        private string _PrecioconIgv;

        // Propiedades públicas con operadores lambda
        public string IdCotiza
        {
            get => _Id_Cotiza;
            set => _Id_Cotiza = value;
        }

        public string IdPed
        {
            get => _Id_Ped;
            set => _Id_Ped = value;
        }

        public DateTime FechaCoti
        {
            get => _FechaCoti;
            set => _FechaCoti = value;
        }

        public DateTime Vigencia
        {
            get => _Vigencia;
            set => _Vigencia = value;
        }

        public double TotalCotiza
        {
            get => _TotalCotiza;
            set => _TotalCotiza = value;
        }

        public string Condiciones
        {
            get => _Condiciones;
            set => _Condiciones = value;
        }

        public string PrecioconIgv
        {
            get => _PrecioconIgv;
            set => _PrecioconIgv = value;
        }
    }
}
