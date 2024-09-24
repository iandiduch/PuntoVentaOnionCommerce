use test_onion;

delimiter $$;
create Procedure Sp_Ver_sihay_Kardex (
in _Id_Prod char (20)
)
begin
select count(*) from KardexProducto
where
Id_Pro =_Id_Prod ;
end $$;

#--insert:
delimiter $$;
create procedure sp_crear_kardex (
in _idkardex char (11),
in _idprod char (20),
in _idprovee char (6)
)
begin
insert into KardexProducto values (
_idkardex ,
_idprod ,
_idprovee,
now() ,
'Activo'
);
end $$;

##--detalle del Kardex:
delimiter $$;
Create procedure Sp_registrar_detalle_kardex(
in _Id_Krdx char (11),
in _Item int,
in _Doc_Soport nchar (29),
in _Det_Operacion varchar (50),
#--entrada
in _Cantidad_In Real,
in _Precio_Unt_In Real,
in _Costo_Total_In Real,
#--salida
in _Cantidad_Out Real,
in _Precio_Unt_Out Real,
in _Importe_Total_Out Real,
#--saldo
in _Cantidad_Saldo Real,
in _Promedio Real,
in _Costo_Total_Saldo Real
)
begin
insert into Detalle_Kardex values (
_Id_Krdx ,
_Item ,
now() ,
_Doc_Soport ,
_Det_Operacion ,
#--entrada
_Cantidad_In ,
_Precio_Unt_In ,
_Costo_Total_In ,
#--salida
_Cantidad_Out ,
_Precio_Unt_Out ,
_Importe_Total_Out ,
#--saldo
_Cantidad_Saldo ,
_Promedio ,
_Costo_Total_Saldo 
);
end $$;


create VIEW V_Kardex_Detalle AS
SELECT 
    KR.Id_krdx, 
    KR.EstadoKrdx,
    x.IDPROVEE, 
    x.NOMBRE, 
    x.DIRECCION, 
    x.CONTACTO, 
    x.TELEFONO,
    DT.Item, 
    DT.Fecha_Krdx, 
    DT.Doc_Soporte, 
    DT.Det_Operacion, 
    DT.Cantidad_In, 
    DT.Precio_In, 
    DT.Total_In,
    DT.Cantidad_Out, 
    DT.Precio_Out, 
    DT.Total_Out, 
    DT.Cantidad_Saldo, 
    DT.Promedio, 
    DT.Costo_Total_Saldo,
    PR.Id_Pro, 
    PR.Descripcion_Larga, 
    PR.Stock_Actual 
FROM 
    KardexProducto KR
INNER JOIN 
    Detalle_Kardex DT ON KR.Id_krdx = DT.Id_krdx
INNER JOIN 
    Productos PR ON KR.Id_Pro = PR.Id_Pro
INNER JOIN 
    Proveedor x ON PR.IDPROVEE = x.IDPROVEE;



delimiter $$;
Create Procedure Sp_Buscador_DeKardex_Principal_yDetalle (
in _xvalor nvarchar (150) 
)
begin
Select * from V_Kardex_Detalle
Where
Id_Pro = _xvalor  or
Doc_Soporte = _xvalor  or
Id_krdx = _xvalor  or
Descripcion_Larga  like concat( _xvalor , '%')
or Descripcion_Larga like concat('%', _xvalor  ,'%')
Order by Item Asc;
end $$;

delimiter $$;
create Procedure Sp_Ver_Kardex_delDia (
in _Fecha date
)
begin
Select * from V_Kardex_Detalle
Where
DATEPART (YEAR,Fecha_Krdx)= DATEPART (YEAR,_Fecha) AND
DATEPART (DAYOFYEAR,Fecha_Krdx)= DATEPART (DAYOFYEAR,_Fecha);
end $$;



ALTER TABLE kardexproducto
DROP FOREIGN KEY FK_Kar_Prod;

ALTER TABLE kardexproducto
ADD CONSTRAINT FK_Kar_Prod FOREIGN KEY (Id_Pro) 
REFERENCES productos(Id_Pro) 
ON UPDATE CASCADE;









