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
    public class RN_Cliente
    {
        public async Task<bool> RN_Registrar(EN_Cliente en)
        {
            BD_Cliente obj = new BD_Cliente();
            bool ok = await obj.BD_Registrar(en);
            return ok;
        }

        public async Task<bool> RN_Editar(EN_Cliente en)
        {
            BD_Cliente obj = new BD_Cliente();
            bool ok = await obj.BD_Editar(en);
            return ok;
        }

        public DataTable RN_Listar(string estado)
        {
            BD_Cliente obj = new BD_Cliente();
            return obj.BD_Listar(estado);
        }

        public DataTable RN_ListarPorValor(string valor, string estado)
        {
            BD_Cliente obj = new BD_Cliente();
            return obj.BD_ListarPorValor(valor, estado);
        }
        public bool RN_Verificar_NroDni(string dni)
        {
            BD_Cliente obj = new BD_Cliente();
            bool ok = obj.BD_Verificar_NroDni(dni);
            return ok;
        }

        public async Task<bool> RN_Eliminar(string val)
        {
            BD_Cliente obj = new BD_Cliente();
            bool ok = await obj.BD_Eliminar(val);
            return ok;
        }

        public async Task<bool> RN_Baja(string val)
        {
            BD_Cliente obj = new BD_Cliente();
            bool ok = await obj.BD_Baja(val);
            return ok;
        }

    }
}
