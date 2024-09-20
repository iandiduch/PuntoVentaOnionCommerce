using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RN_Kardex
    {
        public async Task<bool> RN_Registrar(string Id_KrDx, string Id_Prod, string Id_Prov)
        {
            BD_Kardex obj = new BD_Kardex();
            bool ok = await obj.BD_Registrar(Id_KrDx, Id_Prod, Id_Prov);
            return ok;
        }

        public async Task<bool> RN_RegistrarDetalle(EN_Kardex en)
        {
            BD_Kardex obj = new BD_Kardex();
            bool ok = await obj.BD_RegistrarDetalle(en);
            return ok;
        }

        public bool RN_Verificar_Producto_siTieneKardex(string Id_Prod)
        {
            BD_Kardex obj = new BD_Kardex();
            return obj.BD_Verificar_Producto_siTieneKardex(Id_Prod);
        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_Kardex obj = new BD_Kardex();
            return obj.BD_Listar_PorValor(val);
        }

    }
}
