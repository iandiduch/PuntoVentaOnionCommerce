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
    public class RN_IngresoCompra
    {
        public async Task<bool> RN_Registrar(EN_IngresoCompra en)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            bool ok = await obj.BD_Registrar(en);
            return ok;

        }

        public async Task<bool> RN_RegistrarDetalle(EN_DetalleIngresoCompra en)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            bool ok = await obj.BD_RegistrarDetalle(en);
            return ok;
        }

        public bool RN_Verificar_NroFisicoCompra(string val)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            return obj.BD_Verificar_NroFisicoCompra(val);
        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            return obj.BD_Listar_PorValor(val);
        }

        public DataTable RN_Listar()
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            return obj.BD_Listar();
        }

        public DataTable RN_ListarDetalle(string id)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            return obj.BD_ListarDetalle(id);
        }

        public DataTable RN_ListarPorFecha(string tipo, DateTime fecha)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            return obj.BD_ListarPorFecha(tipo, fecha);
        }

        public async Task<bool> RN_Borrar(string val)
        {
            BD_IngresoCompra obj = new BD_IngresoCompra();
            bool ok = await obj.BD_Borrar(val);
            return ok;
        }
    }
}
