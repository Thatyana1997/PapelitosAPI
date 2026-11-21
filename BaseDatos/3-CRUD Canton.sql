USE PAPELITOSCR;
GO

-- Ejecutar después de 1-BD y tablas.sql. Solo modifica registros de Canton.
CREATE OR ALTER PROCEDURE dbo.sp_Canton_Insertar
    @id_provincia INT,
    @nombre NVARCHAR(100),
    @usuario_creacion NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NULLIF(LTRIM(RTRIM(@usuario_creacion)), N'') IS NULL
        THROW 50001, 'El usuario de creacion es requerido.', 1;

    INSERT INTO dbo.Canton (id_provincia, nombre, fecha_creacion, usuario_creacion, estado)
    OUTPUT inserted.id, inserted.id_provincia, inserted.nombre,
           inserted.fecha_creacion, inserted.usuario_creacion,
           inserted.fecha_modificacion, inserted.usuario_modificacion, inserted.estado
    VALUES (@id_provincia, @nombre, GETDATE(), @usuario_creacion, 'A');
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Canton_Consultar
    @id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id, id_provincia, nombre, fecha_creacion, usuario_creacion,
           fecha_modificacion, usuario_modificacion, estado
    FROM dbo.Canton
    WHERE (estado IS NULL OR estado <> 'E')
      AND (@id IS NULL OR id = @id)
    ORDER BY id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Canton_Actualizar
    @id INT,
    @id_provincia INT,
    @nombre NVARCHAR(100),
    @usuario_modificacion NVARCHAR(50),
    @filas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NULLIF(LTRIM(RTRIM(@usuario_modificacion)), N'') IS NULL
        THROW 50001, 'El usuario de modificacion es requerido.', 1;

    UPDATE dbo.Canton
    SET id_provincia = @id_provincia,
        nombre = @nombre,
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @usuario_modificacion
    WHERE id = @id AND (estado IS NULL OR estado <> 'E');

    SET @filas = @@ROWCOUNT;
END;
GO

-- Eliminación lógica: conserva el registro y su auditoría.
CREATE OR ALTER PROCEDURE dbo.sp_Canton_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50),
    @filas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NULLIF(LTRIM(RTRIM(@usuario_modificacion)), N'') IS NULL
        THROW 50001, 'El usuario de modificacion es requerido.', 1;

    UPDATE dbo.Canton
    SET estado = 'E',
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @usuario_modificacion
    WHERE id = @id AND (estado IS NULL OR estado <> 'E');

    SET @filas = @@ROWCOUNT;
END;
GO
