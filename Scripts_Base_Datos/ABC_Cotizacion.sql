use test_onion;

#--Cotizaciones:

#--Vamos a Crear una Tabla LLamada Cotizacion:
delimiter $$;
create Table Cotizacion (
Id_Cotiza char (11) not null,
Id_Ped char (11) not null,
FechaCoti datetime,
Vigencia datetime,
TotalCotiza real,
Condiciones varchar (450),
PrecioconIgv char (4),
EstadoCoti varchar (15),
primary key (Id_Cotiza)
);
end $$;

#--Relaciones:

ALTER TABLE Cotizacion ADD 
	CONSTRAINT FK_coti_cli FOREIGN KEY 
	(
		Id_Ped
	) REFERENCES Pedido (
		Id_Ped
	);

#--Ahora los Insert:
delimiter $$;
create procedure Sp_Registrar_Cotizacion (
in _Id_Cotiza char (11) ,
in _Id_Ped char (11),
#in _FechaCoti datetime,
in _Vigencia datetime,
in _TotalCotiza real,
in _Condiciones varchar (450),
in _PrecioconIgv char (4)
)
begin
Insert into Cotizacion  values (

_Id_Cotiza  ,
_Id_Ped ,
now() ,
_Vigencia ,
_TotalCotiza ,
_Condiciones ,
_PrecioconIgv ,
'Pendiente'
);
end $$;

#--editar:
delimiter $$;
create procedure Sp_Editar_Cotizacion (
in _Id_Cotiza char (11) ,
in _Id_Ped char (11),
in _FechaCoti datetime,
in _Vigencia datetime,
in _TotalCotiza real,
in _Condiciones varchar (450),
in _PrecioconIgv char (4)
)
begin
update Cotizacion set
Id_Ped =_Id_Ped ,
FechaCoti =_FechaCoti ,
Vigencia =_Vigencia ,
TotalCotiza =_TotalCotiza ,
Condiciones =_Condiciones ,
PrecioconIgv =_PrecioconIgv 
where
Id_Cotiza =_Id_Cotiza; 
end $$;

#--Cambiamos de estado la cotizacion
delimiter $$;
Create Procedure Sp_Cambiar_Estado_Cotizacion (
in _Id_coti char (11),
in _Estadocoti varchar (15)
)
begin
Update Cotizacion
set
EstadoCoti =_Estadocoti 
where
Id_Cotiza =_Id_coti ;
end $$;


#--Vista:
create VIEW v_Vista_Cotizacion_Pedido_Detalle AS
SELECT 
    c.Id_Cotiza,
    c.FechaCoti,
    c.TotalCotiza,
    c.EstadoCoti,
    c.Vigencia,
    c.Condiciones,
    p.id_Ped,
    p.Estado_Ped,
    p.TotalGancia,
    p.SubTotal,
    cl.Id_Cliente,
    cl.Razon_Social_Nombres,
    cl.Apellido,
    cl.DNI,
    cl.Direccion,
    dp.Cantidad,
    dp.Und_Medida,
    dp.Precio,
    dp.Importe,
    dp.Utilidad_Unit,
    dp.TotalUtilidad,
    dp.Tipo_Prod,
    pr.Id_Pro,
    pr.Descripcion_Larga,
    pr.Stock_Actual,
    u.Id_Usu,
    u.Nombres,
    u.Apellidos
FROM 
    Cotizacion c
INNER JOIN 
    Pedido p ON c.Id_Ped = p.id_Ped
INNER JOIN 
    Cliente cl ON p.Id_Cliente = cl.Id_Cliente
INNER JOIN 
    Usuarios u ON p.id_Usu = u.Id_Usu
INNER JOIN 
    Detalle_Pedido dp ON p.id_Ped = dp.id_Ped
INNER JOIN 
    Productos pr ON dp.Id_Pro = pr.Id_Pro;
    
    
    
    
alter VIEW vw_informe_cotizacion AS
SELECT 
    c.Id_Cotiza,
    c.FechaCoti,
    c.TotalCotiza,
    c.Vigencia,
    c.Condiciones,
    p.id_Ped,
    p.SubTotal,
    cl.Id_Cliente,
    cl.Razon_Social_Nombres,
    cl.Apellido,
    cl.DNI,
    cl.Direccion,
    dp.Cantidad,
    dp.Und_Medida,
    dp.Precio,
    dp.Importe,
    pr.Id_Pro,
    pr.Descripcion_Larga,
    u.Id_Usu,
    u.Nombres,
    u.Apellidos,
    u.Correo,
    d.Distrito
FROM 
    Cotizacion c
INNER JOIN 
    Pedido p ON c.Id_Ped = p.id_Ped
INNER JOIN 
    Cliente cl ON p.Id_Cliente = cl.Id_Cliente
INNER JOIN 
    Usuarios u ON p.id_Usu = u.Id_Usu
INNER JOIN 
    Detalle_Pedido dp ON p.id_Ped = dp.id_Ped
INNER JOIN 
    Productos pr ON dp.Id_Pro = pr.Id_Pro
JOIN 
	distrito d ON u.Id_Dis = d.Id_Dis;


delimiter $$;
Create Procedure Sp_ImprimirCotizacion (
in _Nro_coti char (11)
)
begin
Select * from  vw_informe_cotizacion
Where
Id_Cotiza =_Nro_coti or
id_Ped =_Nro_coti ;
end $$;

#--Ahra creamos un buscador para las cotizaciones
delimiter $$;
Create Procedure Sp_Buscar_Cotizaciones_yDetalle (
in _Nro_coti char (11)
)
begin
Select * from  v_Vista_Cotizacion_Pedido_Detalle
Where
Id_Cotiza =_Nro_coti or
id_Ped =_Nro_coti ;
end $$;

#--=========Ahroa otra vista mas personalizada, para el explorador de cotizaciones
CREATE VIEW v_Vista_Cotizacion_Pedido_Cliente AS
SELECT 
    c.Id_Cotiza,
    c.FechaCoti,
    c.TotalCotiza,
    c.EstadoCoti,
    c.Vigencia,
    c.PrecioconIgv,
    c.Condiciones,
    p.id_Ped,
    p.Estado_Ped,
    p.TotalPed,
    p.SubTotal,
    cl.Id_Cliente,
    cl.Razon_Social_Nombres,
    cl.DNI,
    cl.Direccion,
    u.Id_Usu,
    u.Nombres,
    u.Apellidos
FROM 
    Cotizacion c
INNER JOIN 
    Pedido p ON c.Id_Ped = p.id_Ped
INNER JOIN 
    Cliente cl ON p.Id_Cliente = cl.Id_Cliente
INNER JOIN 
    Usuarios u ON p.id_Usu = u.Id_Usu;





#--Todos:;
delimiter $$;
Create Procedure Sp_Cargar_todas_Cotizaciones ()
begin
Select * from v_Vista_Cotizacion_Pedido_Cliente
order by FechaCoti Desc ;
end $$;

delimiter $$;
Create Procedure Sp_Buscador_Gnral_de_Cotizaciones (
in _valor varchar (50)
)
begin
Select * from v_Vista_Cotizacion_Pedido_Cliente
where
Id_Cotiza=_valor or
Id_Ped=_valor or
Razon_Social_Nombres like  concat( _valor , '%') or
Razon_Social_Nombres like concat(  '%' , _valor );
end $$;

#--Eliminar una Cotizacion
delimiter $$;
Create Procedure Sp_Eliminar_cotizacion (
in _NroCotiza nchar (11)
)
begin
Delete from Cotizaciones
where Id_Cotiza=_NroCotiza ;
end $$;


delimiter $$;
create Procedure Sp_VerSiHay_porIDCotiza (
in _Id_Prod char (20)
)
begin
select count(*) from cotizacion
where
Id_Cotiza =_Id_Prod ;
end $$;

#--pedidos por Fecha:
delimiter $$;
Create Procedure Sp_Listar_Cotizacion_porFecha (
in _tipo varchar (5),
in _fecha date
)
begin
if _tipo ='dia' then
	Select * from v_Vista_Cotizacion_Pedido_Cliente
	WHERE 
            DATE(FechaCoti) = _fecha
        ORDER BY 
            FechaCoti ASC;
else
Select * from v_Vista_Cotizacion_Pedido_Cliente
	where
	YEAR (FechaCoti)= YEAR(_fecha)  and
	MONTH (FechaCoti)=MONTH(_fecha) 
	order by FechaCoti Asc;
end if;
end $$;