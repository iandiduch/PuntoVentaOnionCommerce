use test_onion;

#--insert:
delimiter $$;
create procedure Sp_registrar_Producto (
in _idpro char (20),
in _idprove char (6),
in _descripcion varchar (150),
in _frank real,
in _Pre_compraSol real,
in _pre_CompraDolar real,
in _StockActual real,
in _idCat int,
in _idMar int,
in _Foto varchar (180),
in _Pre_Venta_Menor real,
in _Pre_Venta_Mayor real,
in _Pre_Venta_Dolar real,
in _UndMdida char (6),
in _PesoUnit real,
in _Utilidad real,
in _TipoProd varchar (12),
in _ValorporProd real
)
begin
Insert into Productos values (
_idpro ,
_idprove ,
_descripcion ,
_frank ,
_Pre_compraSol ,
_pre_CompraDolar ,
_StockActual ,
_idCat ,
_idMar ,
_Foto ,
_Pre_Venta_Menor ,
_Pre_Venta_Mayor ,
_Pre_Venta_Dolar ,
_UndMdida ,
_PesoUnit ,
_Utilidad ,
_TipoProd ,
_ValorporProd ,
'Activo'
);
end $$;



#--update:
delimiter $$;
create procedure Sp_Editar_Producto (
in _idpro char (20),
in _new_idpro char(20),
in _idprove char (6),
in _descripcion varchar (150),
in _frank real,
in _Pre_compraSol real,
in _pre_CompraDolar real,
in _idCat int,
in _idMar int,
in _Foto varchar (180),
in _Pre_Venta_Menor real,
in _Pre_Venta_Mayor real,
in _Pre_Venta_Dolar real,
in _UndMdida char (6),
in _PesoUnit real,
in _Utilidad real,
in _TipoProd varchar (12)
)
begin


Update productos set
IDPROVEE=_idprove ,
Descripcion_Larga=_descripcion ,
Frank=_frank ,
Pre_CompraS=_Pre_compraSol ,
Pre_Compra$=_pre_CompraDolar ,
Id_Cat=_idCat ,
Id_Marca=_idMar ,
Foto=_Foto ,
Pre_vntaxMenor=_Pre_Venta_Menor ,
Pre_vntaxMayor=_pre_venta_Mayor,
Pre_Vntadolar =_Pre_Venta_Dolar ,
UndMedida=_UndMdida ,
PesoUnit =_PesoUnit ,
UtilidadUnit =_Utilidad ,
TipoProdcto=_TipoProd 
where
Id_Pro =_idpro;

if _new_idpro <> _idpro then
	update productos set Id_Pro = _new_idpro where Id_Pro = _idpro;
    end if;
end $$;

#--Unimos Las Tablas en Vistas:
create VIEW v_Productos_yDependientes AS
SELECT 
    p.Id_Pro, 
    p.Descripcion_Larga, 
    p.Frank, 
    p.Pre_CompraS, 
    p.Pre_Compra$, 
    p.Stock_Actual, 
    p.Foto, 
    p.Pre_vntaxMenor, 
    p.Pre_vntaxMayor, 
    p.Pre_Vntadolar,
    p.UndMedida, 
    p.PesoUnit, 
    p.UtilidadUnit, 
    p.TipoProdcto, 
    p.Valor_porCant, 
    p.Estado_Pro,
    x.IDPROVEE, 
    x.NOMBRE, 
    x.DIRECCION, 
    x.TELEFONO,
    c.Id_Cat, 
    c.Categoria, 
    m.Id_Marca, 
    m.Marca 
FROM 
    Productos p
INNER JOIN 
    Proveedor x ON p.IDPROVEE = x.IDPROVEE
INNER JOIN 
    Categorias c ON p.Id_Cat = c.Id_Cat
INNER JOIN 
    Marcas m ON p.Id_Marca = m.Id_Marca;



#--Todoslos productos:
delimiter $$;
create procedure sp_cargar_Todos_Productos
()
begin
select * from v_Productos_yDependientes
where
Estado_Pro ='Activo'
order by Descripcion_Larga Asc;
end $$;

#--Busar:
delimiter $$;
create Procedure Sp_buscador_Productos(
in _valor varchar (150)
)
begin
Select * from v_Productos_yDependientes Where
Estado_Pro ='Activo' and
Id_Pro like concat('%', _valor) or
Id_Pro = _valor or
Marca like concat('%', _valor , '%' ) or
Categoria like concat('%', _valor , '%' ) or
Descripcion_Larga like concat('%', _valor , '%' )
order by Descripcion_Larga Asc;
end $$;

delimiter $$;
create Procedure Sp_VerSiHay_porID (
in _Id_Prod char (20)
)
begin
select count(*) from productos
where
Id_Pro like concat('P-', _Id_Prod) or
Id_Pro =_Id_Prod ;
end $$;

#--Eliminar:
delimiter $$;
Create Procedure Sp_Darbaja_Producto (
in _idpro char (20)
)
begin
update Productos set
Estado_Pro ='Eliminado'
where
Id_Pro =_idpro ;
end $$;

delimiter $$;
create procedure sp_Eliminar_Producto(
in _idpro char (20)
)
begin
Delete from Productos 
where
Id_Pro =_idpro ;
end $$;

#--Para el Control de Inventario:
delimiter $$;
Create procedure sp_SumarStock (
in _idpro char (20),
in _stock real
)
begin
update Productos set
Stock_Actual = Stock_Actual + _stock 
where
Id_Pro =_idpro ;
end $$;

delimiter $$;
Create procedure sp_Restar_Stock (
in _idpro char (20),
in _stock real
)
begin
update Productos set
Stock_Actual = Stock_Actual - _stock 
where
Id_Pro =_idpro; 
end $$;


#--Cuando hacemos el Ingreso de nuevos Productos..  Es Posible que el Precio de compra tenga variacioens.. y tenemos que hacer que
#--el sistema actualice esos precios..de forma automatica:


#actualizar solo precioCompra
delimiter $$;
create procedure Sp_Actulizar_Precios_Compra_Producto  
(
in _Id_Pro char (20),
in _Pre_CompraS real,
in _Pre_CompraUsd real
)
begin
update  Productos   set 
Pre_CompraS =_Pre_CompraS ,
Pre_Compra$ = _Pre_CompraUsd
where Id_Pro =_Id_Pro ;
end $$;

#actualizar solo precioVenta
delimiter $$;
create procedure Sp_Actulizar_Precios_Venta_Producto  
(
in _Id_Pro char (20),
in _Pre_Venta real,
in _Pre_VentaUsd real
)
begin
update  Productos   set 
Pre_vntaxMenor =_Pre_Venta ,
Pre_Vntadolar = _Pre_VentaUsd
where Id_Pro =_Id_Pro ;
end $$;


DELIMITER //
CREATE PROCEDURE sp_CalcularValorStock(
    IN _idpro CHAR(20)
)
BEGIN
    -- Actualiza la columna Valor_porCant con el cálculo de Stock_Actual * Pre_vntaxMenor
    UPDATE productos
    SET Valor_porCant = Stock_Actual * Pre_vntaxMenor
    WHERE Id_Pro = _idpro;
END //
DELIMITER ;









