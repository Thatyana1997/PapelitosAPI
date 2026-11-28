USE PAPELITOSCR;
GO

-- 1. CREATE (Insertar)
CREATE PROCEDURE sp_Roles_Insertar
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255),
    @usuario_creacion NVARCHAR(50)
AS
BEGIN
    INSERT INTO Roles (nombre, descripcion, usuario_creacion, fecha_creacion, estado)
    VALUES (@nombre, @descripcion, @usuario_creacion, GETDATE(), 'A');
END;
GO

-- 2. READ (Consultar)
CREATE PROCEDURE sp_Roles_Consultar
    @id INT = NULL
AS
BEGIN
    IF @id IS NULL
        SELECT * FROM Roles WHERE estado != 'E';
    ELSE
        SELECT * FROM Roles WHERE id = @id AND estado != 'E';
END;
GO

-- 3. UPDATE (Actualizar)
CREATE PROCEDURE sp_Roles_Actualizar
    @id INT,
    @nombre NVARCHAR(50),
    @descripcion NVARCHAR(255),
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Roles
    SET nombre = @nombre,
        descripcion = @descripcion,
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO

-- 4. DELETE (Lógico - Cambiar estado a 'E')
CREATE PROCEDURE sp_Roles_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Roles
    SET estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO