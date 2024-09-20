using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RN_Usuario
    {
        public async Task<bool> RN_Login(string user, string pass)
        {
            BD_Usuario obj = new BD_Usuario();
            bool ok = await obj.BD_Login(user, pass);
            return ok;
        }

        public DataTable RN_Buscar(string user)
        {
            BD_Usuario bus = new BD_Usuario();
            return bus.BD_Buscar(user);
        }
    }
}
