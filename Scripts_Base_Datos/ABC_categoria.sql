use test_onion ;

#insert:
delimiter $$;
create procedure sp_registrar_Categoria (
in _nombrecat varchar (50)
)
begin 
insert into categorias (Categoria) values
(
_nombrecat
);
end $$;

delimiter $$;
create procedure sp_modificar_Categoria (
in _idcat int,
in _nombrecat varchar (50)
)
begin 
update categorias set
Categoria=_nombrecat
where
Id_cat=_idcat;
end $$;

#--Create eliminar:
delimiter $$;
create procedure sp_eliminar_Categoria (
in _idcat int
)
begin
Delete from categorias
where
Id_Cat = _idcat; 
end $$;

#-- consulta:
delimiter $$;
create procedure sp_listar_todas_categorias ()
begin
select * from categorias
order by Categoria Asc;
end $$;