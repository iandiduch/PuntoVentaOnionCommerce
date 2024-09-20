using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Caja
    {
        // Campos privados
        private DateTime _fechaCaja;
        private string _tipoCaja;
        private string _concepto;
        private string _dePara;
        private string _nroDoc;
        private double _importeCaja;
        private int _idUsu;
        private double _totalUti;
        private string _tipoPago;
        private string _generadoPor;

        // Propiedades públicas con lambdas
        public DateTime FechaCaja
        {
            get => _fechaCaja;
            set => _fechaCaja = value;
        }

        public string TipoCaja
        {
            get => _tipoCaja;
            set => _tipoCaja = value;
        }

        public string Concepto
        {
            get => _concepto;
            set => _concepto = value;
        }

        public string DePara
        {
            get => _dePara;
            set => _dePara = value;
        }

        public string NroDoc
        {
            get => _nroDoc;
            set => _nroDoc = value;
        }

        public double ImporteCaja
        {
            get => _importeCaja;
            set => _importeCaja = value;
        }

        public int IdUsu
        {
            get => _idUsu;
            set => _idUsu = value;
        }

        public double TotalUti
        {
            get => _totalUti;
            set => _totalUti = value;
        }

        public string TipoPago
        {
            get => _tipoPago;
            set => _tipoPago = value;
        }

        public string GeneradoPor
        {
            get => _generadoPor;
            set => _generadoPor = value;
        }
    }
}
