use test_onion ;

#--1:
delimiter $$;
create procedure sp_registrar_Proveedor(
in _idproveedor char (6),
in _nombre varchar (50),
in _direccion varchar (150),
in _telefono char (15),
in _rubro varchar (50),
in _ruc char (20),
in _correo varchar (150),
in _contacto varchar (50),
in _fotologo varchar (180)
)
begin
insert into Proveedor values (
_idproveedor,
 _nombre ,
 _direccion ,
 _telefono ,
 _rubro ,
 _ruc ,
 _correo,
 _contacto,
 _fotologo,
'Activo'
);
end $$;

delimiter $$;
create procedure sp_Modificar_Proveedor(
in _idproveedor char (6),
in _nombre varchar (50),
in _direccion varchar (150),
in _telefono char (15),
in _rubro varchar (50),
in _ruc char (20),
in _correo varchar (150),
in _contacto varchar (50),
in _fotologo varchar (180)
)
begin
update Proveedor set 
NOMBRE=_nombre ,
DIRECCION=_direccion ,
TELEFONO=_telefono ,
RUBRO=_rubro ,
RUC=_ruc ,
CORREO=_correo ,
CONTACTO=_contacto ,
FOTO_LOGO=_fotologo
Where
IDPROVEE=_idproveedor; 
end $$;


delimiter $$;
create procedure sp_listar_Todos_Proveedores()
begin
select * from proveedor
order by NOMBRE Asc ;
end $$;


delimiter $$;
create procedure sp_buscar_proveedor_porvalor (
in _valor varchar (150)
)
begin
select * from proveedor 
where
IDPROVEE = _valor or
nombre like concat('%', _valor , '%' ) or
ruc like concat('%', _valor , '%' ) or
telefono like concat('%', _valor , '%' ) or
correo like concat('%', _valor , '%' )
order by NOMBRE Asc ;
end $$;

#--Create eliminar:
delimiter $$;
create procedure sp_eliminar_Proveedor (
in _idprov char (20)
)
begin
Delete from proveedor
where
IDPROVEE = _idprov; 
end $$;

#--Validar no ingresar doble prov :
delimiter $$;
create procedure sp_Validar_NroDNI_Prov (
in _dni char (18)
)
begin
select COUNT(*) from proveedor 
where
RUC =_dni;
end $$;


