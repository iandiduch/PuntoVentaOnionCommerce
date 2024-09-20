use test_onion;

#--insert:
delimiter $$;
Create Procedure sp_registrar_Caja (

in _Fecha_Caja datetime,
in _Tipo_Caja varchar (50),
in _Concepto varchar (190),
in _De_Para varchar (180),
in _Nro_Doc char (20),
in _ImporteCaja real,
in _Id_Usu int,
in _TotalUti real,
in _TipoPago varchar (13),
in _GeneradoPor varchar (15)
)
begin
Insert into Caja Values (
_Fecha_Caja ,
_Tipo_Caja ,
_Concepto ,
_De_Para ,
_Nro_Doc ,
_ImporteCaja ,
_Id_Usu,
_TotalUti ,
_TipoPago ,
_GeneradoPor ,
'Activo'
);
end $$;

#--acutalizar importe caja
delimiter $$;
create procedure Sp_Actualizar_Total_Caja (
in _Nro_doc char (11),
in _total Real,
in _TotalUtilidad real,
in _TipoPago varchar (12)
)
begin
update caja set
ImporteCaja =_total ,
TotalUti =_TotalUtilidad 
where 
Nro_Doc =_Nro_doc and
TipoPago =_TipoPago ;
end $$;


CREATE VIEW V_Caja_Usuario AS
SELECT 
    Cj.Idcaja,
    Cj.Fecha_Caja,
    Cj.Tipo_Caja,
    Cj.Concepto,
    Cj.De_Para,
    Cj.Nro_Doc,
    Cj.ImporteCaja,
    Cj.TipoPago,
    Cj.TotalUti,
    Cj.EstadoCaja,
    Cj.GeneradoPor,
    Ui.Id_Usu,
    Ui.Nombres,
    Ui.Apellidos
FROM 
    Caja Cj
    INNER JOIN Usuarios Ui ON Cj.Id_Usu = Ui.Id_Usu;


/* CONSULTAS PARA EL EXPLORADOR DE CAJA Y OTRAS VENTANAS */
/* ======================================================*/
delimiter $$;
create Procedure Sp_Listar_Todas_Cajas ()
	begin
	Select * from V_Caja_Usuario
	order by Fecha_Caja Asc;
end $$;

#--Cajas del dia
#---------select mostrar caja-----
Create  Procedure  Sp_Listar_Cajas_delDia ()
begin
select * from V_Caja_Usuario
where
YEAR (Fecha_Caja)= YEAR (GETDATE()) AND
DAYOFYEAR (Fecha_Caja)= DAYOFYEAR (GETDATE())
Order By Nro_Doc Asc;
end $$;

#--delmes
delimiter $$;
Create  Procedure  Sp_Listar_Cajas_del_Mes (
in _fechas date
)
begin
select * from V_Caja_Usuario
where
YEAR (Fecha_Caja)= YEAR (_fechas) AND
MONTH (Fecha_Caja)= MONTH(_fechas)
order by Fecha_Caja ASc;
end $$;

#--Buscar movimiento de Caja por Cliente
delimiter $$;
create procedure Sp_Buscador_MoviCaja_xValor (
in _xvalor varchar (150)
)
begin
Select * from V_Caja_Usuario
Where
Nro_Doc=_xvalor or
TipoPago=_xvalor or
Tipo_caja=_xvalor or
Nombres=_xvalor or
GeneradoPor=_xvalor or
De_Para like concat( '%', _xvalor)  or De_Para  like concat( '%' , _xvalor  , '%') or
De_Para like concat(_xvalor, '%') or
Nro_Doc like concat( '%', _xvalor) or Nro_Doc  like concat( '%' , _xvalor, '%') or Nro_Doc   like concat( _xvalor, '%')
Order by Fecha_Caja Asc ;
end $$;