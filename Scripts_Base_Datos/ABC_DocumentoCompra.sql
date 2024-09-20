USE test_onion;

#--Insert:
delimiter $$;
Create procedure Sp_Registrar_Compra(
in _idCom char (11),
in _Nro_Fac_Fisico char (20),
in _IdProvee char (6),
in _SubTotal_Com real,
in _FechaIngre datetime,
in _TotalCompra real,
in _IdUsu int,
in _ModalidadPago varchar (50),
in _TiempoEspera int,
in _FechaVence date,
in _EstadoIngre varchar (20),
in _RecibiConforme bit,
in _Datos_Adicional varchar (150),
in _Tipo_Doc_Compra varchar (12)
)
begin
insert into DocumentoCompras values (
_idCom,
_Nro_Fac_Fisico ,
_IdProvee ,
_SubTotal_Com ,
_FechaIngre ,
_TotalCompra ,
_IdUsu ,
_ModalidadPago ,
_TiempoEspera ,
_FechaVence ,
_EstadoIngre ,
_RecibiConforme ,
_Datos_Adicional ,
_Tipo_Doc_Compra 
);
end $$;

#--Detalle:
delimiter $$;
Create Procedure Sp_Insert_Detalle_ingreso (
in	_Id_ingreso char(11),
in	_Id_Pro char(20),
in	_Precio real,	
in	_Cantidad real,
in	_Importe real
)
begin
INSERT INTO Detalle_DocumCompra
VALUES
(
_Id_ingreso ,
_Id_Pro,
_Precio,
_Cantidad,
_Importe
);	
end $$;

#--Vista:
create View V_Documentos_Compra_Detalle
As
	Select 
	c.Id_DocComp , c.NroFac_Fisico ,c.SubTotal_ingre , c.Fecha_Ingre , c.Total_Ingre , c.ModalidadPago, c.TiempoEspera , c.Fecha_Vencimiento ,
	c.Estado_Ingre ,c.Recibiconforme , c.Datos_Adicional , c.TipoDoc_Compra ,
	P.IDPROVEE ,P.NOMBRE ,P.RUC,
	Det.PrecioUnit , Det.Cantidad, Det.Importe,
	Pro.Id_Pro, Pro.Descripcion_Larga, pro.Stock_Actual,pro.Pre_Compra$ , pro.Pre_CompraS           
	From
	DocumentoCompras c, Detalle_DocumCompra Det, Productos Pro, Proveedor P
	where
	c.IDPROVEE =p.IDPROVEE and
	c.Id_DocComp =Det.Id_DocComp  And
	Det.Id_Pro=Pro.Id_Pro;


#--Buscar un Documento de Compra Completo:
delimiter $$;
Create Procedure Sp_Buscar_FacturasCompras_Detalle (
in _xvalor nchar (20)
)
begin
Select * from V_Documentos_Compra_Detalle
Where
Id_DocComp=_xvalor or
NroFac_Fisico=_xvalor;
end $$;

#--Una Vista solo de las Tablas Principales o Master:
create View V_Documentos_CompraPrincipal
As
	Select 
	c.Id_DocComp , c.NroFac_Fisico ,c.SubTotal_ingre , c.Fecha_Ingre , c.Total_Ingre , c.ModalidadPago, c.TiempoEspera , c.Fecha_Vencimiento ,
	c.Estado_Ingre ,c.Recibiconforme , c.Datos_Adicional , c.TipoDoc_Compra ,
	P.IDPROVEE ,P.NOMBRE ,P.RUC,
	u.Id_Usu , u.Nombres , u.Apellidos , u.Usuario , u.Ubicacion_Foto 	   
	From
	DocumentoCompras c, Proveedor P, Usuarios u
	where
	c.IDPROVEE =p.IDPROVEE and
	c.id_Usu = u.Id_Usu 
;

#--Consultas para el Explorador de Compras:
#--1) Ahora un Buscador General
delimiter $$;
Create Procedure Sp_Buscador_Gnral_deCompras (
in _xvalor varchar (150)
)
begin
Select * from V_Documentos_CompraPrincipal
Where
Id_DocComp=_xvalor or
NroFac_Fisico = _xvalor or
TipoDoc_Compra=_xvalor or
RUC = _xvalor or
NOMBRE like concat( _Xvalor +'%') or NOMBRE  like concat('%',  _Xvalor , '%')  ;
end $$;


#--cargamos todas las facturas ingresadas 
delimiter $$;
create Procedure Sp_Leer_Todas_Facturas_Compras ()
	begin
	Select * from V_Documentos_CompraPrincipal
	order by NOMBRE Asc ;
    end $$;

##--facturas ingreadas en el dia
delimiter $$;
create Procedure Sp_Facturas_Ingresadas_alDia (
in _tipo varchar (20),
in _fecha date
)
begin
if _tipo ='dia' then
	Select * from V_Documentos_CompraPrincipal
	where
	YEAR (Fecha_Ingre)= YEAR(_fecha)  and
	dayofyear(Fecha_Ingre)= dayofyear(_fecha) 
	order by Fecha_Ingre Asc;
else
Select * from V_Documentos_CompraPrincipal
	where
	YEAR (Fecha_Ingre)= YEAR(_fecha)  and
	MONTH (Fecha_Ingre)= MONTH(_fecha) 
	order by Fecha_Ingre Asc;
end if ;
end $$;

call Sp_Facturas_Ingresadas_alDia ('Mes','2020/01/01') ;

#--Actualmente utilizando para eliminar la factura
delimiter $$;
Create Procedure SP_Borrar_Factura_Ingresada (
in _Id_Fac char (11)
)
begin
Delete from Detalle_DocumCompra
where Id_DocComp =_Id_Fac; 
Delete from DocumentoCompras
where Id_DocComp =_Id_Fac;
end $$; 


delimiter $$;
create procedure sp_validar_NroFisico_Compra (
in _Nro_Doc_fisico char  (20)
)
begin
select COUNT(*) from DocumentoCompras 
where
NroFac_Fisico =_Nro_Doc_fisico ;
end $$;