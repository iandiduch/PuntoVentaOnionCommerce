using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Cliente
    {
        private string _idcliente;
        public string Idcliente
        {
            get => _idcliente;
            set => _idcliente = value;
        }

        private string _razonsocial;
        public string RazonSocial
        {
            get => _razonsocial;
            set => _razonsocial = value;
        }

        private string _dni;
        public string Dni
        {
            get => _dni;
            set => _dni = value;
        }

        private string _direccion;
        public string Direccion
        {
            get => _direccion;
            set => _direccion = value;
        }

        private string _telefono;
        public string Telefono
        {
            get => _telefono;
            set => _telefono = value;
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        private int _idDis;
        public int IdDis
        {
            get => _idDis;
            set => _idDis = value;
        }

        private DateTime _fechaAniver;
        public DateTime FechaAniver
        {
            get => _fechaAniver;
            set => _fechaAniver = value;
        }

        private string _contacto;
        public string Contacto
        {
            get => _contacto;
            set => _contacto = value;
        }

        private double _limiteCred;
        public double LimiteCred
        {
            get => _limiteCred;
            set => _limiteCred = value;
        }

        private string _apellido;
        public string Apellido
        {
            get => _apellido;
            set => _apellido = value;
        }

        private string _localidad;
        public string Localidad
        {
            get => _localidad;
            set => _localidad = value;
        }

        private string _CodigoPostal;
        public string CodigoPostal
        {
            get => _CodigoPostal;
            set => _CodigoPostal = value;
        }


    }
}
