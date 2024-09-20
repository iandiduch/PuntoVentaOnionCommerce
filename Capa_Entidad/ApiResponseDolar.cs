using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class ApiResponseDolar
    {
        public decimal compra { get; set; }
        public decimal venta { get; set; }
        public string casa { get; set; }
        public string nombre { get; set; }
        public string moneda { get; set; }
        public string fechaActualizacion { get; set; }
    }
}
