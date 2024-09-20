use test_onion ;


ALTER TABLE Documento ADD 
	CONSTRAINT FK_doc_usux FOREIGN KEY 
	(
		Id_Usu
	) REFERENCES Usuarios (
		Id_Usu
	);


#--validar codigo de comprobante
delimiter $$;
create Procedure Sp_Validar_Id_Doc (
in _Id_Doc char (11)
)
begin
Select count(*) from Documento 
Where id_Doc =_Id_Doc ;
end $$;

#----=============================================================
#---- STORE PROCEDURE AGREGAR DOCUMENTO DE VENTA
#----=============================================================
delimiter $$;
Create Procedure Sp_Insert_Documento(
in	_id_Doc char(11),	
in	_id_Ped char(11),	
in	_Id_Tipo int,
in	_Fecha_Emi date,
in	_Importe real,
in	_TipoPago varchar (50),
in	_NroOpera char (20),	
in	_id_Usu int,
in	_Igv real,
in	_son varchar (180),
in	_TotalGanancia real
	
)
begin
INSERT INTO DOCUMENTO
VALUES(
_id_Doc,
_id_Ped,
_Id_Tipo, 
_Fecha_Emi, 
_Importe, 
_TipoPago , 
_NroOpera ,
_Igv ,
_son, 
_TotalGanancia,
'Activo'
);
end $$;

/*--Actualizar Totales del Documento:*/
delimiter $$;
Create Procedure Sp_Actualizar_documento(
in	_Id_Doc varchar(11),
in	_importe Real,
in	_Igv Real,
in	_son varchar (180)
)
begin
	UPDATE Documento SET
	ImporteDoc =_importe ,
	IgvDoc =_Igv ,
	TotalLetra=_son 
	WHERE
	Id_Doc=_Id_Doc;
end $$;

/*--Vista Genral de Todo Documento*/
CREATE VIEW V_Listado_Documento AS
SELECT 
    Doc.Id_Doc,
    Doc.TipoPago,
    Doc.ImporteDoc,
    Doc.Fecha_Emi,
    Doc.IgvDoc,
    Doc.Estado_doc,
    Doc.Nro_Operacion,
    Doc.TotalLetra,
    Doc.TotalGanancia,
    Ped.id_Ped,
    Cli.id_cliente,
    Cli.Razon_Social_Nombres,
    Cli.DNI,
    Cli.Direccion,
    Ped.Fecha_Ped,
    Ped.SubTotal,
    Ped.TotalPed,
    Ped.Estado_Ped,
    T.Id_Tipo,
    T.Documento,
    U.Id_Usu,
    U.Nombres,
    U.Apellidos,
    U.Nombres + ' ' + U.Apellidos AS NombreCompletoUsu
FROM 
    Documento Doc
    INNER JOIN Pedido Ped ON Doc.id_Ped = Ped.id_Ped
    INNER JOIN Cliente Cli ON Ped.id_cliente = Cli.id_cliente
    INNER JOIN Tipo_Doc T ON Doc.Id_Tipo = T.Id_Tipo
    INNER JOIN Usuarios U ON Ped.id_Usu = U.Id_Usu;


#--2
delimiter $$;
Create Procedure Sp_Buscador_Documentos_xValor (
in _Xvalor varchar (250)
)
begin
Select * from V_Listado_Documento
where
id_Doc =  _Xvalor or
TipoPago = _xvalor or
Documento = _xvalor or
id_Ped = _Xvalor or
Nombres = _Xvalor  or
Id_Cliente=_Xvalor or
DNI = _Xvalor or 
Estado_Doc=_Xvalor or
Razon_Social_Nombres like concat(_Xvalor ,'%') or Razon_Social_Nombres like concat( '%',_Xvalor , '%' )
order by Fecha_Emi Asc;
end $$;

delimiter $$;
Create Procedure Sp_Listar_Doc_emitoshoy (
in _FechaActual date
)
begin
	Select * from V_Listado_Documento 
	Where 
	YEAR(Fecha_emi)=YEAR (_FechaActual)AND
	DAYOFYEAR (Fecha_Emi )= DAYOFYEAR(_FechaActual)
	order by id_Doc Asc ;
end $$;

delimiter $$;
create Procedure Sp_Leer_Fcturas_Emtidas_EnunMes (
in _Fecha_Mes Date
)
begin
	Select * from V_Listado_Documento 
where 
YEAR( Fecha_emi)=YEAR( _Fecha_Mes )AND
MONTH (Fecha_Emi ) =MONTH(_Fecha_Mes )
ORDER BY Fecha_Emi ASC ;
end $$;

delimiter $$;
create Procedure Sp_Leer_Comprobantes_Emtidas_EnunMes (
in _Fecha_Mes Date,
in _Docu Int
)
begin
	Select * from V_Listado_Documento 
where 
YEAR( Fecha_emi)=YEAR( _Fecha_Mes )AND
MONTH (Fecha_Emi ) =MONTH(_Fecha_Mes ) and
Id_Tipo =_Docu
ORDER BY Fecha_Emi ASC;
end $$;



#--Vista del Documento con su Detalle:
CREATE VIEW V_Listado_Documento_Detalle AS
SELECT 
    Doc.Id_Doc,
    Ped.id_Ped,
    Cli.id_cliente,
    Cli.Razon_Social_Nombres,
    Cli.DNI,
    Cli.Direccion,
    Doc.ImporteDoc,
    Ped.Fecha_Ped,
    Ped.SubTotal,
    Ped.TotalPed,
    Ped.id_Usu,
    Ped.Estado_Ped,
    Ped.TotalGancia,
    Det.Precio,
    Det.Cantidad,
    Det.Importe,
    Det.Und_Medida,
    Det.Tipo_Prod,
    Det.Utilidad_Unit,
    Det.TotalUtilidad,
    Pro.Id_Pro,
    Pro.Descripcion_Larga,
    Pro.Stock_Actual,
    Doc.TotalLetra,
    Doc.IgvDoc,
    Doc.Estado_doc,
    Doc.Fecha_Emi,
    Doc.Nro_Operacion,
    Doc.TipoPago,
    Doc.totalganancia,
    Tp.Id_Tipo,
    Tp.Documento
FROM 
    Documento Doc
    INNER JOIN Pedido Ped ON Doc.id_Ped = Ped.id_Ped
    INNER JOIN Tipo_Doc Tp ON Doc.Id_Tipo = Tp.Id_Tipo
    INNER JOIN Detalle_Pedido Det ON Det.Id_Ped = Ped.Id_Ped
    INNER JOIN Cliente Cli ON Ped.id_cliente = Cli.id_cliente
    INNER JOIN Productos Pro ON Det.Id_Pro = Pro.Id_Pro;


delimiter $$;
Create Procedure Sp_Buscar_Documento_yDetalle (
in _Nro_Doc char (11)
)
begin
Select * from V_Listado_Documento_Detalle
where
Id_Doc=_Nro_Doc or
id_Ped =_Nro_Doc ;
end $$;


#----=============================================================
delimiter $$;
Create Procedure Sp_Anular_Documento(
in	_Id_Doc char(11),
in	_estado varchar (50)
)
begin
UPDATE Documento SET
		Estado_doc =_estado
	WHERE Id_Doc=_Id_Doc;
end $$;


##--Cambiar:
delimiter $$;
Create Procedure Sp_Cambiar_TipoPago_Documento(
in	_Id_Doc char(11),
	_tipoPago varchar (50)
)
begin
UPDATE Documento SET
		TipoPago  =_tipoPago 
WHERE
Id_Doc=_Id_Doc;
end $$;
