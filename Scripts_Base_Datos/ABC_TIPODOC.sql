 USE test_onion;

INSERT INTO Tipo_Doc values (1, 'Factura','F00','0000001','Activo');
INSERT INTO Tipo_Doc values (2, 'Boleta','B00','0000001','Activo');
INSERT INTO Tipo_Doc values (3, 'Nota Venta','NV0','0000001','Activo');
INSERT INTO Tipo_Doc values (4, 'Productos','P','00001','Activo');
INSERT INTO Tipo_Doc values (5, 'Proveedor','X','00001','Activo');
INSERT INTO Tipo_Doc values (6, 'Kardex','KRD','0000001','Activo');



delimiter $$;
create procedure Sp_Editar_Tipo_Doc
(
in _idtipo int,
in _documento nvarchar(50),
in _serie nvarchar(3),
in _numero nvarchar (7)
)
begin
update tipo_doc set 
Documento  = _documento ,
Serie = _serie ,
Numero = _numero ,
estado_TiDoc = _estado   
where Id_Tipo = _idtipo  ;  
end $$;


##--1) QUERY
delimiter $$;
create Procedure Sp_Listado_Tipo (
in	_Id_Tipo int
)
begin
	Select concat( Serie , '-' , Numero) as Nro from Tipo_Doc 
	Where Id_Tipo=_Id_Tipo;
end $$;

call Sp_Listado_Tipo (4);

#En caso de error de Code 1418: This Function has none of DETERMINISTIC, Ejecutar Esta Linea:
SET GLOBAL log_bin_trust_function_creators=1;

#--Funcion para Generar los ID Correlativos:
delimiter $$;
drop FUNCTION Fnc_Generar_Correlativo (_idtipo int)
RETURNS char (7)
BEGIN
declare _nro char (7);
select cast(Numero as SIGNED)+1  into _nro from tipo_doc Where Id_Tipo=_idtipo;
if length(_nro)=1 then
RETURN concat('000000',_nro);
end if;
IF length(_nro)=2 then
RETURN concat('00000',_nro);
end if;
if length(_nro)=3 then
RETURN concat('0000',_nro);
end if;
if length(_nro)=4 then
RETURN concat('000',_nro);
end if;
if length(_nro)=5 then
RETURN concat('00',_nro);
end if;
if length(_nro)=6 then
RETURN concat('0',_nro);
else
RETURN _nro;
end if;
END $$;

delimiter $$;
create FUNCTION Fnc_Generar_Correlativo (_idtipo int)
RETURNS char(7)
BEGIN
    declare _nro char(7);
    declare _numero int;
    
    -- Obtener el número actual y sumarle 1
    select cast(Numero as SIGNED) + 1 into _numero from tipo_doc Where Id_Tipo = _idtipo;

    -- Asegurar que el número no exceda 13 dígitos
    if length(_numero) > 7 then
        set _numero = 9999999; -- O cualquier otro valor manejable, dependiendo de tu lógica de negocio
    end if;
    
    -- Formatear el número como un string de 13 dígitos con ceros a la izquierda
    set _nro = lpad(_numero, 7, '0');
    
    RETURN _nro;
END $$;

delimiter $$;
create FUNCTION Fnc_Generar_Correlativo_Producto (_idtipo int)
RETURNS char(13)
BEGIN
    declare _nro char(13);
    declare _numero int;
    
    -- Obtener el número actual y sumarle 1
    select cast(Numero as SIGNED) + 1 into _numero from tipo_doc Where Id_Tipo = _idtipo;

    -- Asegurar que el número no exceda 13 dígitos
    if length(_numero) > 13 then
        set _numero = 9999999999999; -- O cualquier otro valor manejable, dependiendo de tu lógica de negocio
    end if;
    
    -- Formatear el número como un string de 13 dígitos con ceros a la izquierda
    set _nro = lpad(_numero, 13, '0');
    
    RETURN _nro;
END $$;

##--Ahora un procedimineto Almacenado para Actualizar el Siguiente Numero Correlativo:
delimiter $$;
create procedure Sp_Actualiza_Tipo_Doc(
in	_Id_Tipo Int
)
begin
Declare _NuevoNum char(7);
Set _NuevoNum = Fnc_Generar_Correlativo(_Id_Tipo);
update tipo_doc set
Numero=_NuevoNum
where
Id_Tipo=_Id_Tipo;
end $$;

##para producto
delimiter $$;
create PROCEDURE Sp_Actualiza_Tipo_Prodcto(
    IN _Id_Tipo INT
)
BEGIN
    DECLARE _NuevoNum CHAR(13);
    
    -- Generar el nuevo número correlativo de 13 dígitos
    SET _NuevoNum = Fnc_Generar_Correlativo_Producto(_Id_Tipo);
    
    -- Actualizar la tabla tipo_doc con el nuevo número
    UPDATE tipo_doc 
    SET Numero = _NuevoNum
    WHERE Id_Tipo = _Id_Tipo;
END $$;

ALTER TABLE tipo_doc MODIFY COLUMN Numero char(13);


#-- Listado Especial de Documentos:
delimiter $$;
create Procedure Sp_Tipod_Doc_Spcial ()
begin
Select * from Tipo_Doc 
Where Id_Tipo = '1' or Id_Tipo ='2' or Id_Tipo ='3'
order by Id_Tipo desc ;
end $$;








