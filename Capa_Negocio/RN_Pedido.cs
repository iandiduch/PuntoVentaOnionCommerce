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
    public class RN_Pedido
    {
        public async Task<bool> RN_Registrar(EN_Pedido en)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_Registrar(en);
        }

        public async Task<bool> RN_RegistrarDetalle(EN_DetallePedido en)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_RegistrarDetalle(en);

        }

        public async Task<bool> RN_Editar(EN_Pedido en)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_Editar(en);

        }

        public async Task<bool> RN_Eliminar(string val)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_Eliminar(val);
        }

        public async Task<bool> RN_EliminarDetalle(string val)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_EliminarDetalle(val);
        }

        public DataTable RN_ListarPendiente()
        {
            BD_Pedido obj = new BD_Pedido();
            return obj.BD_ListarPendiente();
        }

        public DataTable RN_ListarPorValor(string val)
        {
            BD_Pedido obj = new BD_Pedido();
            return obj.BD_ListarPorValor(val);
        }

        public DataTable RN_ListarPorDiaMes(string tipo, DateTime val)
        {
            BD_Pedido obj = new BD_Pedido();
            return obj.BD_ListarPorDiaMes(tipo, val);
        }

        public DataTable RN_BuscarPedidoDetalle(string val)
        {
            BD_Pedido obj = new BD_Pedido();
            return obj.BD_BuscarPedidoDetalle(val);
        }

        public async Task<bool> RN_EditarCliente(string idPed, string idCliente)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_EditarCliente(idPed, idCliente);

        }

        public async Task<bool> RN_EstadoAtendido(string idPed)
        {
            BD_Pedido obj = new BD_Pedido();
            return await obj.BD_EstadoAtendido(idPed);

        }

        public bool RN_Verificar_IdPedido(string val)
        {
            BD_Pedido obj = new BD_Pedido();
            return obj.BD_Verificar_IdPedido(val);

        }
    }
}
