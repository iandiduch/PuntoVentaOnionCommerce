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
    public class RN_Producto
    {
        public async Task<bool> RN_Registrar(EN_Producto en)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Registrar(en);
            return ok;
        }

        public async Task<bool> RN_Editar(EN_Producto en, string nuevoIdProducto)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Editar(en, nuevoIdProducto);
            return ok;
        }

        public async Task<bool> RN_Eliminar(string val)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Eliminar(val);
            return ok;
        }

        public async Task<bool> RN_DarBaja(string val)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_DarBaja(val);
            return ok;
        }

        public bool RN_Buscar_porID(string Id_Prod)
        {
            BD_Producto obj = new BD_Producto();
            return obj.BD_Buscar_porID(Id_Prod);
        }

        public DataTable RN_Listar()
        {
            BD_Producto obj = new BD_Producto();
            return obj.BD_Listar();
        }

        public DataTable RN_Listar_PorValor(string val)
        {
            BD_Producto obj = new BD_Producto();
            return obj.BD_Listar_PorValor(val);
        }


        //STOCK

        public async Task<bool> RN_SumarStock(string val, double val2)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_SumarStock(val, val2);
            return ok;
        }

        public async Task<bool> RN_RestarStock(string val, double val2)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_RestarStock(val, val2);
            return ok;
        }

        public async Task<bool> RN_CalcularValorStock(string val)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_CalcularValorStock(val);
            return ok;
        }

        public async Task<bool> RN_Actulizar_Precios_CompraVenta_Producto(EN_ActualizarPrecios en)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Actulizar_Precios_CompraVenta_Producto(en);
            return ok;
        }

        public async Task<bool> RN_Actulizar_Precios_Compra_Producto(EN_ActualizarPrecios en)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Actulizar_Precios_Compra_Producto(en);
            return ok;
        }

        public async Task<bool> RN_Actulizar_Precios_Venta_Producto(EN_ActualizarPrecios en)
        {
            BD_Producto obj = new BD_Producto();
            bool ok = await obj.BD_Actulizar_Precios_Venta_Producto(en);
            return ok;
        }
    }
}
