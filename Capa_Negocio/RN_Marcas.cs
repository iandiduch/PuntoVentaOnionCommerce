using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{
    public class RN_Marcas
    {
        public void RN_Registrar_Marca(string nomMarca)
        {
            BD_Marcas obj = new BD_Marcas();
            obj.BD_Registrar_Marca(nomMarca);
        }

        public void RN_Editar_Marca(int idMarca, string nomMarca)
        {
            BD_Marcas obj = new BD_Marcas();
            obj.BD_Editar_Marca(idMarca, nomMarca);
        }

        public DataTable RN_Cargar_Todas_Marca()
        {
            BD_Marcas obj = new BD_Marcas();
            return obj.BD_Cargar_Todas_Marca();
        }
        public void RN_Eliminar_Marca(int idMarca)
        {
            BD_Marcas obj = new BD_Marcas();
            obj.BD_Eliminar_Marca(idMarca);
        }
    }
}
