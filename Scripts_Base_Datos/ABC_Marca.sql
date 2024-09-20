use test_onion ;

delimiter $$;
create procedure sp_addMarca
(
in _marca nvarchar (50)
)
begin
insert into Marcas (Marca) 
values
(
_marca
);
end $$;


delimiter $$;
create procedure sp_Listar_Todos_Marcas ()
begin
Select * from Marcas
order by Marca asc;
end $$;

#--Create eliminar:
delimiter $$;
create procedure sp_eliminar_Marca (
in _idmar int
)
begin
Delete from Marcas
where
Id_Marca = _idmar; 
end $$;

#--editar:
delimiter $$;
Create procedure sp_Editar_Marca (
in _idmar int,
in _nom_marca varchar (50)
)
begin
Update Marcas set
Marca =_nom_marca
where
Id_Marca=_idmar;
end $$;