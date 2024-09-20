using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_ActualizarPrecios
    {
        // Campos privados
        private string _idPro;
        private double _preCompraS;
        private double _preCompraUsd;
        private double _preVntaxMenor;
        private double _preVntaxUsd;
        private double _utilidad;

        // Propiedades públicas
        public string IdPro
        {
            get => _idPro;
            set => _idPro = value;
        }

        public double PreCompraS
        {
            get => _preCompraS;
            set => _preCompraS = value;
        }

        public double PreCompraUsd
        {
            get => _preCompraUsd;
            set => _preCompraUsd = value;
        }

        public double PreVntaxMenor
        {
            get => _preVntaxMenor;
            set => _preVntaxMenor = value;
        }

        public double PreVntaxUsd
        {
            get => _preVntaxUsd;
            set => _preVntaxUsd = value;
        }

        public double Utilidad
        {
            get => _utilidad;
            set => _utilidad = value;
        }

    }
}
