use PAPELITOSCR;
go
-- 1. CREATE (Insertar)
CREATE PROCEDURE sp_Provincia_Insertar
    @nombre NVARCHAR(100),
    @usuario_creacion NVARCHAR(50)
AS
BEGIN
    INSERT INTO Provincia (nombre, usuario_creacion, fecha_creacion, estado)
    VALUES (@nombre, @usuario_creacion, GETDATE(), 'A');
END;
GO

-- 2. READ (Consultar)
CREATE PROCEDURE sp_Provincia_Consultar
    @id INT = NULL
AS
BEGIN
    IF @id IS NULL
        SELECT * FROM Provincia WHERE estado != 'E';
    ELSE
        SELECT * FROM Provincia WHERE id = @id AND estado != 'E';
END;
GO

-- 3. UPDATE (Actualizar)
CREATE PROCEDURE sp_Provincia_Actualizar
    @id INT,
    @nombre NVARCHAR(100),
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Provincia
    SET nombre = @nombre,
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO

-- 4. DELETE (Logico - Cambiar estado a 'E')
CREATE PROCEDURE sp_Provincia_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Provincia
    SET estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO