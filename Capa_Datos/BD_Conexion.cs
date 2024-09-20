using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class BD_Conexion
    {
        public string Conectar()
        {
            return "data source=localhost;Initial Catalog=test;uid=root;pwd=1234";
        }

        public static string Conectar2()
        {
            return "data source=localhost;Initial Catalog=test;uid=root;pwd=1234";
        }


    }
}
