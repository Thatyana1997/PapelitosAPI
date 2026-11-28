use PAPELITOSCR;
go
--1. CREATE (insertar)
CREATE PROCEDURE sp_Producto_Insertar
@nombre	NVARCHAR(100),
@precio decimal (18,2),
@id_categoria int,
@usauario_creacion NVARCHAR(50)
AS
BEGIN 
	INSERT INTO Producto(nombre,precio,id_categoria,usuario_creacion,fecha_creacion,estado)
	VALUES(@nombre,@precio,@id_categoria,@usauario_creacion,GETDATE(),'A');
	END;
	GO
	-- 2. READ (Consultar)
CREATE PROCEDURE sp_Productos_Consultar
@id int = null
as
BEGIN
	IF @id is null 
		SELECT * FROM Producto WHERE estado !='E';
	ELSE 
		SELECT * FROM Producto WHERE id= @id AND estado !='E';
END;
GO
-- 3. UPDATE (Actualizar)
CREATE PROCEDURE sp_Productos_Actualizar
	@id int,
	@nombre NVARCHAR (100),
	@precio DECIMAL (18,2),
	@usuario_modificacion NVARCHAR(50)
AS
BEGIN
	UPDATE Producto
	SET nombre = @nombre,
		precio= @precio,
		usuario_creacion= @usuario_modificacion,
		fecha_modificacion = GETDATE()
	WHERE id = @id;
END;
GO
-- 4. DELETE (Lógico - Cambiar estado a 'E')
CREATE PROCEDURE sp_Productos_Eliminar
	@id int,
	@usuario_modificacion NVARCHAR(50)
AS
BEGIN
	UPDATE Producto
	set estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO


