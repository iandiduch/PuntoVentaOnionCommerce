use test_onion ;

#--1
delimiter $$;
create procedure sp_addDistrito
(
in _distrito nvarchar (50)
)
begin
insert into Distrito 
values
(
_distrito ,
'Activo'
);
end $$;

delimiter $$;
create procedure sp_Listar_Todos_Distritos()
begin 
Select * from Distrito 
order by Distrito asc;
end $$;

##--Create eliminar:
delimiter $$;
create procedure sp_eliminar_distrito (
in _idDis int
)
begin 
Delete from Distrito 
where
Id_Dis =idDis; 
end $$;

#--editar:
delimiter $$;
Create procedure sp_Editar_Distrito (
in _idDis int,
in _nomdis varchar (50)
)
begin
Update Distrito set
Distrito =_nomdis 
where
Id_Dis =_idDis ;
end $$;