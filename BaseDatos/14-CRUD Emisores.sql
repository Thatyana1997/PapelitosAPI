use PAPELITOSCR;
go

-- 1. CREATE (Insertar) - EMISOR   //razon social nombre de la empresa // identificacion cedula juridica
CREATE PROCEDURE sp_Emisor_Insertar
    @razon_social NVARCHAR(100),
    @usuario_creacion NVARCHAR(50)
AS
BEGIN
    INSERT INTO Emisor(razon_social, usuario_creacion, fecha_creacion, estado)
    VALUES ( @razon_social, @usuario_creacion, GETDATE(), 'A');
END;
GO

-- 2. READ (Consultar)
CREATE PROCEDURE sp_Emisor_Consultar
    @id INT = NULL
AS
BEGIN
    IF @id IS NULL
        SELECT * FROM Emisor WHERE estado != 'E';
    ELSE
        SELECT * FROM Emisor WHERE id = @id AND estado != 'E';
END;
GO

-- 3. UPDATE (Actualizar)
CREATE PROCEDURE sp_Emisor_Actualizar
    @id INT,
    @razon_social NVARCHAR(100), 
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Emisor
    SET razon_social = @razon_social, 
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO

-- 4. DELETE (Lógico - Cambiar estado a 'E')
CREATE PROCEDURE sp_Emisor_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Emisor
    SET estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO
