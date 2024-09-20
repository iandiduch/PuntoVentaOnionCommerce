use test_onion;

#--insert:
delimiter $$;
create Procedure Sp_Registrar_Pedido(
in _id_Ped char (11),
in _Id_Cliente char (10),
in _SubTotal real,
in _IgvPed real,
in _TotalPed real,
in _id_Usu int,
in _TotalGancia real
)
begin
Insert into Pedido values
(
_id_Ped ,
_Id_Cliente ,
now(),
_SubTotal ,
_IgvPed ,
_TotalPed ,
_id_Usu ,
_TotalGancia,
'Pendiente'
);
end $$;

#--detalle:
delimiter $$;
create procedure sp_Registrar_detalle_Pedido(
in _id_Ped char (11),
in _Id_Pro char (20),
in _Precio real,
in _Cantidad real,
in _Importe real,
in _Tipo_Prod varchar (20),
in _Und_Medida varchar (10),
in _Utilidad_Unit real,
in _TotalUtilidad real
)
begin
insert into Detalle_Pedido values
(
_id_Ped ,
_Id_Pro ,
_Precio ,
_Cantidad,
_Importe ,
_Tipo_Prod ,
_Und_Medida ,
_Utilidad_Unit ,
_TotalUtilidad 
);
end $$;


#--borrar Detalle:
delimiter $$;
Create procedure sp_eliminar_detalle_Pedido (
in _id_Ped char (11)
)
begin
Delete from Detalle_Pedido where
id_Ped =_id_Ped ;
end $$;

#--update:
delimiter $$;
Create Procedure Sp_Editar_Pedido(
in _id_Ped char (11),
in _Id_Cliente char (10),
in _fechaPed datetime,
in _SubTotal real,
in _IgvPed real,
in _TotalPed real,
in _id_Usu int,
in _TotalGancia real
)
begin
update Pedido set
Id_Cliente =_Id_Cliente ,
Fecha_Ped =_fechaPed ,
SubTotal =_SubTotal ,
IgvPed=_IgvPed,
TotalPed =_TotalPed ,
TotalGancia =_TotalGancia 
where
id_Ped =_id_Ped ;
end $$;


delimiter $$;
Create Procedure Sp_Verificar_Id_Pedido(
in _Id_Ped char (11)
)
begin
Select count(*) from Pedido 
Where id_Ped = _Id_Ped ;
end $$;

#--cambiar Estado:
delimiter $$;
create Procedure Sp_Pedido_Atendido(
in _Id_Ped char(11)
)
begin
	UPDATE Pedido  SET
	Estado_Ped  ='Atendido'
	WHERE Id_Ped=_Id_Ped;
end $$;


#--Cambiar solo el Cliente:
delimiter $$;
Create Procedure Sp_Actu_clien_Ped(
	in _Id_Ped char (11),
	in _Id_cli char(10)
	
)
begin
	UPDATE Pedido SET
		Id_Cliente =_Id_cli 
	WHERE Id_Ped=_Id_Ped;
end $$;

#--Eliminar Todo el Pedido:
delimiter $$;
Create Procedure Sp_Eliminar_Pedido_Completo(
	in _Id_Ped char(11)
)
begin
Delete from Detalle_Pedido WHERE Id_Ped=_Id_Ped;
Delete from Pedido WHERE Id_Ped=_Id_Ped;
end $$;

#--consultas:
CREATE VIEW V_Listado_Pedido_Detalle AS
SELECT 
    Ped.Id_Ped,
    Cli.id_cliente,
    Cli.Razon_Social_Nombres,
    Cli.DNI,
    Cli.Direccion,
    Cli.Telefono,
    Cli.E_mail,
    Ped.SubTotal,
    Ped.Fecha_Ped,
    Ped.TotalPed,
    Ped.Estado_Ped,
    Ped.TotalGancia,
    Ped.id_Usu,
    Det.Precio,
    Det.Cantidad,
    Det.Importe,
    Det.Tipo_Prod,
    Det.Und_Medida,
    Det.Utilidad_Unit,
    Det.TotalUtilidad,
    Pro.Descripcion_Larga,
    Pro.Id_Pro,
    Pro.Stock_Actual     
FROM 
    Pedido Ped
INNER JOIN 
    Detalle_Pedido Det ON Ped.Id_Ped = Det.Id_Ped
INNER JOIN 
    Cliente Cli ON Ped.id_cliente = Cli.id_cliente
INNER JOIN 
    Productos Pro ON Det.Id_Pro = Pro.Id_Pro;


#--Buscar pedido completo con detalle:
delimiter $$;
Create  Procedure Sp_Buscar_Pedido_Para_Editar (
in _Id_Ped char(11)
)
begin
	Select * from V_Listado_Pedido_Detalle
	Where Id_Ped = _Id_Ped;
end $$;

#--PEdidos para el Explorador:
CREATE VIEW V_Pedidos_Cliente_General AS
SELECT 
    P.id_Ped, 
    P.SubTotal, 
    P.TotalPed,  
    P.Fecha_Ped, 
    P.Estado_Ped, 
    P.TotalGancia,
    C.Id_Cliente, 
    C.Razon_Social_Nombres, 
    C.DNI,
    C.Estado_Cli,
    U.Id_Usu, 
    U.Nombres 
FROM 
    Pedido P
INNER JOIN 
    Cliente C ON P.Id_Cliente = C.Id_Cliente
INNER JOIN 
    Usuarios U ON P.id_Usu = U.Id_Usu;



#--Buscadir de Pedidos:
delimiter $$;
Create Procedure Sp_buscar_Pedidos_porValor(
in _valor varchar(250)
)
begin
	Select  * from V_Pedidos_Cliente_General
	Where
	Razon_Social_Nombres like concat( _valor , '%') or
	Razon_Social_Nombres like concat( '%' , _valor) or
	id_Ped=_valor or
	Id_Cliente=_valor or
	DNI=_valor 	
	Order by Fecha_Ped  desc ;
end $$;

#--pedidos por Fecha:
delimiter $$;
create Procedure Sp_Listar_Pedidos_porFecha (
in _tipo varchar (5),
in _fecha date
)
begin
if _tipo ='dia' then
	Select * from V_Pedidos_Cliente_General
	where	   
	DATE(Fecha_Ped) = _fecha 
    Order by Fecha_Ped Asc;
else
Select * from V_Pedidos_Cliente_General
	where
	YEAR (Fecha_Ped)= YEAR(_fecha)  and
	MONTH (Fecha_Ped)= MONTH (_fecha) 
	order by Fecha_Ped Asc;
    end if;
end $$;


#--Ver Pedidos PEndiente de atencion:
delimiter $$;
create Procedure Sp_Leer_Pedidos_PorAtender ()
begin
select * from V_Pedidos_Cliente_General
where 
Estado_Ped ='Pendiente' and
YEAR (Fecha_Ped)= YEAR(_fecha)  and
dayofyear(Fecha_Ped)= dayofyear(_fecha);
end $$;