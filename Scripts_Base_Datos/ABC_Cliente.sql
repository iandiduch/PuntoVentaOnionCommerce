use test_onion;

#--Validar no ingresar doble Cliente :
delimiter $$;
create procedure sp_Validar_NroDNI (
in _dni char (18)
)
begin
select COUNT(*) from Cliente 
where
DNI =_dni;
end $$;



#--insert:
delimiter $$;
create Procedure Sp_Registrar_Cliente (
in _idcliente char (10),
in _razonsocial varchar (250),
in _apellido varchar (100),
in _dni char (18),
in _direccion varchar (150),
in _localidad varchar (100),
in _cp varchar (10),
in _telefono char (10),
in _email varchar (150),
in _idDis int,
in _fechaAniver date,
in _contacto varchar (50),
in _limiteCred real
)
begin
Insert into Cliente values
(
_idcliente ,
_razonsocial ,
_apellido,
_dni ,
_direccion ,
_localidad,
_cp,
_telefono,
_email ,
_idDis ,
_fechaAniver ,
_contacto ,
_limiteCred ,
'Activo'
);
end $$;


#--update:
delimiter $$;
create Procedure Sp_Modificar_Cliente (
in _idcliente char (10),
in _razonsocial varchar (250),
in _apellido varchar (100),
in _dni char (18),
in _direccion varchar (150),
in _localidad varchar (100),
in _cp varchar (10),
in _telefono char (10),
in _email varchar (150),
in _idDis int,
in _fechaAniver date,
in _contacto varchar (50),
in _limiteCred real
)
Begin
update cliente set
Razon_Social_Nombres = _razonsocial ,
Apellido = _apellido,
DNI=_dni ,
Direccion=_direccion ,
Localidad = _localidad,
Codigo_Postal= _cp,
Telefono=_telefono ,
E_Mail=_email ,
Id_Dis=_idDis ,
Fcha_Ncmnto_Anivsrio=_fechaAniver ,
Contacto=_contacto ,
Limit_Credit=_limiteCred 
where
Id_Cliente =_idcliente ;
end $$;


#vistas:
create View V_Clientes_Distritos
As
Select Id_Cliente,Razon_Social_Nombres,Apellido,DNI ,
Direccion,Localidad, Codigo_Postal, telefono, e_mail,Cliente.Id_Dis,Distrito, Fcha_Ncmnto_Anivsrio ,Contacto,Limit_Credit,Estado_cli
From  Cliente
	INNER JOIN Distrito  On Cliente.Id_Dis = Distrito .Id_Dis 
Where 
	Cliente.Estado_cli ='Activo';

#--Listamos todos los clientes:
delimiter $$;
create Procedure sp_Listar_Todos_Clientes (
in _estado varchar (12)
)
begin
Select * from V_Clientes_Distritos
where
Estado_Cli =_estado 
order by Razon_Social_Nombres Asc;
end $$;

#--Buscamos por Nombre:
delimiter $$;
create Procedure Sp_Buscar_Cliente_porValor (
in _Valor varchar (250),
in _estado varchar (12)
)
begin
Select * from V_Clientes_Distritos
where
Estado_Cli =_estado and
DNI =_Valor or
Id_Cliente =_Valor or
Razon_Social_Nombres like concat( '%' , _Valor) or
Razon_Social_Nombres like concat(_Valor , '%' );
end $$;

#--Eliminar:
delimiter $$;
Create Procedure Sp_DarBajar_Cliente (
in _idcliente char (10)
)
begin
Update Cliente set
Estado_Cli ='De_Baja'
where
Id_Cliente =@idcliente ;
end $$;


delimiter $$;
Create Procedure Sp_Eliminar_Cliente(
in _idcliente char (10)
)
begin
Delete from Cliente 
where
Id_Cliente =_idcliente;
end $$;

ALTER TABLE Cliente
ADD COLUMN Localidad VARCHAR(100) AFTER Direccion,       -- Agrega la columna 'Localidad' después de 'Direccion'
ADD COLUMN Codigo_Postal CHAR(10) AFTER Localidad,       -- Agrega la columna 'Codigo_Postal' después de 'Localidad'
ADD COLUMN Apellido VARCHAR(100) AFTER Razon_Social_Nombres;  -- Agrega la columna 'Apellido' después de 'Razon_Social_Nombres'



