using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RN_TipoDoc
    {
        public static string RN_NroID(int idTipo)
        {
            return BD_TipoDoc.BD_NroID(idTipo);
        }
        public static async void RN_Actualizar_Sig_NroCorrelativo(int idTipo)
        {
            await BD_TipoDoc.BD_Actualizar_Sig_NroCorrelativo(idTipo);
        }

        public static async void RN_Actualizar_Sig_NroCorrelativo_Producto(int idTipo)
        {
            await BD_TipoDoc.BD_Actualizar_Sig_NroCorrelativo_Producto(idTipo);
        }

    }
}
