use test_onion ;

#-- Relacionando las Tablas:

ALTER TABLE Cliente ADD 
	CONSTRAINT FK_Cli_Dis FOREIGN KEY 
	(
		Id_Dis
	) REFERENCES Distrito (
		Id_Dis
	);

ALTER TABLE Usuarios ADD 
	CONSTRAINT FK_Usu_Dis FOREIGN KEY 
	(
		Id_Dis
	) REFERENCES Distrito (
		Id_Dis
	);




ALTER TABLE Productos ADD 
	CONSTRAINT FK_Prod_Cat FOREIGN KEY 
	(
		Id_Cat
	) REFERENCES Categorias (
		Id_Cat
	);
    
 ALTER TABLE Productos ADD    
	CONSTRAINT FK_Prod_Mark FOREIGN KEY 
	(
		id_Marca
	) REFERENCES Marcas (
		id_Marca
	);
ALTER TABLE Productos ADD 	
	CONSTRAINT FK_provd FOREIGN KEY 
	(
		IDPROVEE
	) REFERENCES PROVEEDOR (
		IDPROVEE
	);

ALTER TABLE KardexProducto ADD 
	CONSTRAINT FK_Kar_Prod FOREIGN KEY 
	(
		Id_Pro
	) REFERENCES Productos (
		Id_Pro
	);
    
ALTER TABLE KardexProducto ADD 
	CONSTRAINT FK_Kar_Provee FOREIGN KEY 
	(
		IDPROVEE
	) REFERENCES Proveedor (
		IDPROVEE
	);


ALTER TABLE Detalle_Kardex ADD 
	CONSTRAINT FK_Kar_det FOREIGN KEY 
	(
		Id_krdx
	) REFERENCES KardexProducto (
		Id_krdx
	);

ALTER TABLE Pedido ADD 
	CONSTRAINT FK_Ped_cli FOREIGN KEY 
	(
		Id_Cliente
	) REFERENCES Cliente (
		Id_Cliente
	);
    
ALTER TABLE Pedido ADD 
	CONSTRAINT FK_Ped_usu FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);


ALTER TABLE Detalle_Pedido ADD 
	CONSTRAINT FK_det_ped FOREIGN KEY 
	(
		id_Ped
	) REFERENCES Pedido (
		id_Ped
	);
    
ALTER TABLE Detalle_Pedido ADD 
	CONSTRAINT FK_det_Prd FOREIGN KEY 
	(
		Id_Pro
	) REFERENCES Productos (
		Id_Pro
	);


ALTER TABLE Documento  ADD 
	CONSTRAINT FK_doc_ped FOREIGN KEY 
	(
		id_Ped
	) REFERENCES Pedido (
		id_Ped
	);
ALTER TABLE Documento  ADD
	CONSTRAINT FK_doc_tip FOREIGN KEY 
	(
		Id_Tipo
	) REFERENCES Tipo_Doc (
		Id_Tipo
	);

ALTER TABLE Credito ADD 
	CONSTRAINT FK_cre_doc FOREIGN KEY 
	(
		Id_Doc
	) REFERENCES Documento (
		Id_Doc
	);


ALTER TABLE Detalle_Credito ADD 
	CONSTRAINT FK_cre_det FOREIGN KEY 
	(
		IdNotaCred
	) REFERENCES Credito (
		IdNotaCred
	);
    
ALTER TABLE Detalle_Credito ADD 
	CONSTRAINT FK_cred_usudet FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);



ALTER TABLE DocumentoCompras ADD 
	CONSTRAINT FK_com_prov FOREIGN KEY 
	(
		IDPROVEE
	) REFERENCES Proveedor (
		IDPROVEE
	);
    
ALTER TABLE DocumentoCompras ADD 
	CONSTRAINT FK_com_usu FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);



ALTER TABLE Detalle_DocumCompra ADD 
	CONSTRAINT FK_detcom FOREIGN KEY 
	(
		Id_DocComp
	) REFERENCES DocumentoCompras (
		Id_DocComp
	);

ALTER TABLE Detalle_DocumCompra ADD 
	CONSTRAINT FK_detcom_prod FOREIGN KEY 
	(
		Id_Pro
	) REFERENCES Productos (
		Id_Pro
	);



ALTER TABLE Menu_xUsu ADD 
	CONSTRAINT FK_mnuusu FOREIGN KEY 
	(
		Id_usu
	) REFERENCES Usuarios (
		Id_usu
	);

ALTER TABLE Detalle_Temporal ADD 
	CONSTRAINT FK_tem_Det FOREIGN KEY 
	(
		codTem
	) REFERENCES Temporal(
		codTem
	);
    
# FAltaba esto:

ALTER TABLE menu_xusu ADD 
	
	CONSTRAINT FK_mnu_usu FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);




ALTER TABLE valecompra ADD 
	
	CONSTRAINT FK_val_cli FOREIGN KEY 
	(
		Id_Cliente
	) REFERENCES Cliente (
		Id_Cliente
	);



ALTER TABLE cierre_caja ADD 
	
	CONSTRAINT FK_cirre_usu FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);

ALTER TABLE usuarios ADD 
	
	CONSTRAINT FK_rol_usu FOREIGN KEY 
	(
		Id_Rol
	) REFERENCES Roles (
		Id_Rol
	);


ALTER TABLE caja ADD 
	CONSTRAINT FK_caja_usu FOREIGN KEY 
	(
		id_Usu
	) REFERENCES Usuarios (
		id_Usu
	);

    
    



