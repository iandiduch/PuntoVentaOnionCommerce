using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_Datos;

namespace Capa_Negocio
{
    public class RN_Categoria
    {

        public void RN_Registrar_Categoria(string nomCateg)
        {
            BD_Categoria obj = new BD_Categoria();
            obj.BD_Registrar_Categoria(nomCateg);
        }

        public void RN_Editar_Categoria(int idCateg, string nomCateg)
        {
            BD_Categoria obj = new BD_Categoria();
            obj.BD_Editar_Categoria(idCateg, nomCateg);
        }

        public DataTable RN_Mostrar_Todas_Categoria()
        {
            BD_Categoria obj = new BD_Categoria();
            return obj.BD_Cargar_Todas_Categorias();
        }

        public void RN_Eliminar_Categoria(int idCateg)
        {
            BD_Categoria obj = new BD_Categoria();
            obj.BD_Eliminar_Categoria(idCateg);
        }
    }
}
