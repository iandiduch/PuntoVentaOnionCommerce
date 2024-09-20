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
    public class RN_Proveedor
    {
        public async Task<bool> RN_Registrar(EN_Proveedor en)
        {
            BD_Proveedor obj = new BD_Proveedor();
            bool ok = await obj.BD_Registrar(en);
            return ok;
        }

        public async Task<bool> RN_Editar(EN_Proveedor en)
        {
            BD_Proveedor obj = new BD_Proveedor();
            bool ok = await obj.BD_Editar(en);
            return ok;
        }

        public async Task<bool> RN_Eliminar(string val)
        {
            BD_Proveedor obj = new BD_Proveedor();
            bool ok = await obj.BD_Eliminar(val);
            return ok;
        }

        public DataTable RN_Listar()
        {
            BD_Proveedor obj = new BD_Proveedor();
            return obj.BD_Listar();
        }

        public bool RN_Verificar_NroDni(string dni)
        {
            BD_Proveedor obj = new BD_Proveedor();
            bool ok = obj.BD_Verificar_NroDni(dni);
            return ok;
        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_Proveedor obj = new BD_Proveedor();
            return obj.BD_Listar_PorValor(val);
        }
    }
}
