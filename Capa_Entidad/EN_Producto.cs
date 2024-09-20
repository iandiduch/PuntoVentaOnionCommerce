using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class EN_Producto
    {
        private string _id;
        private string _idproveedor;
        private string _descripcion;
        private double _frank;
        private double _precompra_pesos;
        private double _precompra_dlls;
        private double _stock_actual;
        private int _id_cat;
        private int _id_marca;
        private string _foto;
        private double _preventa_menor;
        private double _preventa_mayor;
        private double _preventa_dolar;
        private string _unidadmedida;
        private double _pesounit;
        private double _utilidad;
        private string _tipoproducto;
        private double _valor_general;

        public string Id { get => _id; set => _id = value; }
        public string Idproveedor { get => _idproveedor; set => _idproveedor = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public double Frank { get => _frank; set => _frank = value; }
        public double Precompra_pesos { get => _precompra_pesos; set => _precompra_pesos = value; }
        public double Precompra_dlls { get => _precompra_dlls; set => _precompra_dlls = value; }
        public double Stock_actual { get => _stock_actual; set => _stock_actual = value; }
        public int Id_cat { get => _id_cat; set => _id_cat = value; }
        public int Id_marca { get => _id_marca; set => _id_marca = value; }
        public string Foto { get => _foto; set => _foto = value; }
        public double Preventa_menor { get => _preventa_menor; set => _preventa_menor = value; }
        public double Preventa_mayor { get => _preventa_mayor; set => _preventa_mayor = value; }
        public double Preventa_dolar { get => _preventa_dolar; set => _preventa_dolar = value; }
        public string Unidadmedida { get => _unidadmedida; set => _unidadmedida = value; }
        public double Utilidad { get => _utilidad; set => _utilidad = value; }
        public string Tipoproducto { get => _tipoproducto; set => _tipoproducto = value; }
        public double Valor_general { get => _valor_general; set => _valor_general = value; }
        public double Pesounit { get => _pesounit; set => _pesounit = value; }
    }
}
