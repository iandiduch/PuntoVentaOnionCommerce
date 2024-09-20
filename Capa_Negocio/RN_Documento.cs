using Capa_Datos;
using Capa_Datos.Util;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RN_Documento
    {
        public async Task<bool> RN_Registrar(EN_Documento documento)
        {
            BD_Documento bd = new BD_Documento();
            return await bd.BD_Registrar(documento);

        }

        public async Task<bool> RN_ActualizarTotalDoc(EN_Documento documento)
        {
            BD_Documento bd = new BD_Documento();
            return await bd.BD_ActualizarTotalDoc(documento);

        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_Listar_PorValor(val);
        }

        public DataTable RN_Listar()
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_Listar();
        }

        public DataTable RN_ListarPorFechaHoy(DateTime fecha)
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_ListarPorFechaHoy(fecha);
        }

        public DataTable RN_ListarPorFechaMes(DateTime fecha)
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_ListarPorFechaMes(fecha);
        }

        public DataTable RN_LeerPorFechaMesComprobantes(DateTime fecha, int doc)
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_LeerPorFechaMesComprobantes(fecha, doc);
        }

        public DataTable RN_ListarDetalle_PorNroDoc(string val)
        {
            BD_Documento bd = new BD_Documento();
            return bd.BD_ListarDetalle_PorNroDoc(val);
        }

        public async Task<bool> RN_Anular(string id, string estado)
        {
            BD_Documento bd = new BD_Documento();
            return await bd.BD_Anular(id, estado);
        }

        public async Task<bool> RN_CambiarTipoPago(string id, string tipo)
        {
            BD_Documento bd = new BD_Documento();
            return await bd.BD_CambiarTipoPago(id, tipo);

        }
    }
}
