using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{
    public class RN_Distrito
    {
        public void RN_Registrar_Distrito(string nomDistrito)
        {
            BD_Distrito obj = new BD_Distrito();
            obj.BD_Registrar_Distrito(nomDistrito);
        }

        public void RN_Editar_Distrito(int idDistrito, string nomDistrito)
        {
            BD_Distrito obj = new BD_Distrito();
            obj.BD_Editar_Distrito(idDistrito, nomDistrito);
        }

        public DataTable RN_Cargar_Todas_Distrito()
        {
            BD_Distrito obj = new BD_Distrito();
            return obj.BD_Cargar_Todas_Distrito();
        }
        public void RN_Eliminar_Distrito(int idDistrito)
        {
            BD_Distrito  obj = new BD_Distrito();
            obj.BD_Eliminar_Distrito(idDistrito);
        }
    }
}
