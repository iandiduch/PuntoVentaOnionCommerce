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
    public class RN_Cotizacion
    {
        public async Task<bool> RN_Registrar(EN_Cotizacion en)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return await obj.BD_Registrar(en);

        }

        public DataTable RN_ImprimirCotizacion(string IdCotiza)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return obj.BD_ImprimirCotizacion(IdCotiza);
        }

        public bool RN_Buscar_porID(string Id_Prod)
        {
            BD_Cotizacion bD_Cotizacion = new BD_Cotizacion();
            return bD_Cotizacion.BD_Buscar_porID(Id_Prod);
        }
        public async Task<bool> RN_Editar(EN_Cotizacion en)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return await obj.BD_Editar(en);

        }

        public async Task<bool> RN_Eliminar(string IdCotiza)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return await obj.BD_Eliminar(IdCotiza);

        }

        public async Task<bool> RN_CambiarEstado(string IdCotiza, string estado)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return await obj.BD_CambiarEstado(IdCotiza, estado);

        }

        public DataTable RN_Listar()
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return obj.BD_Listar();
        }

        public DataTable RN_ListarPorValor(string val)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return obj.BD_ListarPorValor(val);
        }

        public DataTable RN_ListarPorDiaMes(string val, DateTime val2)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return obj.BD_ListarPorDiaMes(val, val2);
        }

        public DataTable RN_ListarPorValorDetalle(string val)
        {
            BD_Cotizacion obj = new BD_Cotizacion();
            return obj.BD_ListarPorValorDetalle(val);
        }
    }
}
