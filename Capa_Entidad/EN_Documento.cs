using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Documento
    {
        // Campos privados
        private string _idDoc;
        private string _idPed;
        private int _idTipo;
        private DateTime _fechaEmi;
        private double _importe;
        private string _tipoPago;
        private string _nroOpera;
        private int _idUsu;
        private double _igv;
        private string _son;
        private double _totalGanancia;

        // Propiedades públicas con lambdas
        public string IdDoc
        {
            get => _idDoc;
            set => _idDoc = value;
        }

        public string IdPed
        {
            get => _idPed;
            set => _idPed = value;
        }

        public int IdTipo
        {
            get => _idTipo;
            set => _idTipo = value;
        }

        public DateTime FechaEmi
        {
            get => _fechaEmi;
            set => _fechaEmi = value;
        }

        public double Importe
        {
            get => _importe;
            set => _importe = value;
        }

        public string TipoPago
        {
            get => _tipoPago;
            set => _tipoPago = value;
        }

        public string NroOpera
        {
            get => _nroOpera;
            set => _nroOpera = value;
        }

        public int IdUsu
        {
            get => _idUsu;
            set => _idUsu = value;
        }

        public double Igv
        {
            get => _igv;
            set => _igv = value;
        }

        public string Son
        {
            get => _son;
            set => _son = value;
        }

        public double TotalGanancia
        {
            get => _totalGanancia;
            set => _totalGanancia = value;
        }
    }
}
