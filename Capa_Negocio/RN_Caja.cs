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
    public class RN_Caja
    {
        public async Task<bool> RN_Registrar(EN_Caja caja)
        {
            BD_Caja obj = new BD_Caja();
            return await obj.BD_Registrar(caja);

        }

        public async Task<bool> RN_ActualizarTotalCaja(EN_Caja caja)
        {
            BD_Caja obj = new BD_Caja();
            return await obj.BD_ActualizarTotalCaja(caja);

        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_Caja obj = new BD_Caja();
            return obj.BD_Listar_PorValor(val);
        }

        public DataTable RN_Listar()
        {
            BD_Caja obj = new BD_Caja();
            return obj.BD_Listar();
        }

        public DataTable RN_ListarDia()
        {
            BD_Caja obj = new BD_Caja();
            return obj.BD_ListarDia();
        }

        public DataTable RN_ListarPorFecha(DateTime fecha)
        {
            BD_Caja obj = new BD_Caja();
            return obj.BD_ListarPorFecha(fecha);
        }
    }
}
