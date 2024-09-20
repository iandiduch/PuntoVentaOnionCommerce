using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Proveedor
    {
        private string _idproveedor;
        private string _nombre;
        private string _direccion;
        private string _telefono;
        private string _razonsocial;
        private string _cuit;
        private string _correo;
        private string _contacto;
        private string _fotologo;


        public string Idproveedor { get => _idproveedor; set => _idproveedor = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string Razonsocial { get => _razonsocial; set => _razonsocial = value; }
        public string Cuit { get => _cuit; set => _cuit = value; }
        public string Correo { get => _correo; set => _correo = value; }
        public string Contacto { get => _contacto; set => _contacto = value; }
        public string Fotologo { get => _fotologo; set => _fotologo = value; }
    }
}
