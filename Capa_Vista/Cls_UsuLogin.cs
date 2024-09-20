using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion_Commerce
{
public	class Cls_Libreria
	{
		//public  struct  SesionUsuario
		//{
		//	public static string  IdUsu;
		//	public  string Usuario;
		//	public static string Nombres;
		//	public string Apellidos;
		//	public string  IdRol;
		//	public string Rol;
		//	public string Foto;
		//}

		//public SesionUsuario ObjSesionUsuario;



        private static string _idUsu;
        private static string _usuario;
        //private static string _nombres;
        private static string _nombre;
        private static string _idRol;
        private static string _rol;
        private static string _foto;

       
        public static string IdUsu { get => _idUsu; set => _idUsu = value; }
        public static string Usuario { get => _usuario; set => _usuario = value; }
      
        public static string Nombre { get => _nombre; set => _nombre = value; }
        public static string Foto { get => _foto; set => _foto = value; }
        public static  string Rol { get => _rol; set => _rol = value; }
        public static string IdRol { get => _idRol; set => _idRol = value; }
        //public static string xNombres { get => _nombres; set => _nombres = value; }
    }
}
