use test_onion;

insert into Distrito values (1,'La Tinguiña', 'Activo');
select * from Distrito;

insert into Usuarios values (1, 'Ian','Diduch',1, 'admin','admin','-','2000/01/05',1,'-','Activo');



create VIEW V_Usuarios_Roles AS
SELECT 
    U.Id_Usu, 
    U.Nombres, 
    U.Apellidos, 
    U.Usuario, 
    U.Contraseñax, 
    U.Ubicacion_Foto,
    U.Id_Rol, 
    R.Rol, 
    U.Estado_usu
FROM 
    Usuarios U
INNER JOIN 
    Roles R ON U.Id_Rol = R.Id_Rol;
                

delimiter $$;
create Procedure Sp_Usuario_Login(
	in _Usuario varchar(50)
)
begin
	Select * from V_Usuarios_Roles
	Where
	Usuario=_Usuario and Estado_usu = 'Activo';
end $$;

call Sp_Usuario_Login ('admin');



#--login
delimiter $$;
create Procedure Sp_Login (
in _Usuario varchar(20),
in _Contraseña varchar(12)
)
begin 
	Select Count(*)from Usuarios 
	Where Usuario =_Usuario and Contraseñax =_Contraseña;
end $$;