USE PAPELITOSCR;
GO
-- Asignacion #3: solo Cliente. Ejecutar despues de 1-BD y tablas.sql.
-- Consultas con columnas explicitas, parametros, auditoria y borrado logico.

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Insertar
    @nombre NVARCHAR(100),
    @id_tipo_id INT = NULL,
    @identificacion NVARCHAR(50) = NULL,
    @usuario_creacion NVARCHAR(50) = NULL,
    @estado CHAR(1) = 'A'
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NULLIF(LTRIM(RTRIM(@nombre)), N'') IS NULL
        THROW 50001, N'El nombre es obligatorio.', 1;
    IF NULLIF(LTRIM(RTRIM(@usuario_creacion)), N'') IS NULL
        THROW 50001, N'El usuario de creacion es obligatorio.', 1;
    IF @estado IS NULL OR @estado NOT IN ('A', 'I')
        THROW 50001, N'Para crear un cliente el estado debe ser A o I.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO dbo.Cliente
            (nombre, id_tipo_id, identificacion, fecha_creacion, usuario_creacion, estado)
        VALUES
            (@nombre, @id_tipo_id, @identificacion, GETDATE(), @usuario_creacion, @estado);
        DECLARE @id INT = CONVERT(INT, SCOPE_IDENTITY());
        SELECT id, nombre, id_tipo_id, identificacion, fecha_creacion, usuario_creacion,
               fecha_modificacion, usuario_modificacion, estado
        FROM dbo.Cliente WHERE id = @id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Consultar
    @id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id, nombre, id_tipo_id, identificacion, fecha_creacion, usuario_creacion,
               fecha_modificacion, usuario_modificacion, estado
    FROM dbo.Cliente
    WHERE (@id IS NULL OR id = @id) AND (estado IS NULL OR estado <> 'E')
    ORDER BY id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Actualizar
    @id INT,
    @nombre NVARCHAR(100),
    @id_tipo_id INT = NULL,
    @identificacion NVARCHAR(50) = NULL,
    @usuario_modificacion NVARCHAR(50) = NULL,
    @estado CHAR(1) = 'A',
    @actualizado BIT = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @actualizado = 0;
    IF @id <= 0 OR NULLIF(LTRIM(RTRIM(@nombre)), N'') IS NULL
        THROW 50001, N'El id debe ser positivo y el nombre obligatorio.', 1;
    IF NULLIF(LTRIM(RTRIM(@usuario_modificacion)), N'') IS NULL
        THROW 50001, N'El usuario de modificacion es obligatorio.', 1;
    IF @estado IS NULL OR @estado NOT IN ('A', 'I')
        THROW 50001, N'Para actualizar el estado debe ser A o I. Use eliminar para E.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE dbo.Cliente
        SET nombre = @nombre, id_tipo_id = @id_tipo_id, identificacion = @identificacion,
            fecha_modificacion = GETDATE(), usuario_modificacion = @usuario_modificacion,
            estado = @estado
        WHERE id = @id AND (estado IS NULL OR estado <> 'E');
        SET @actualizado = CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50),
    @actualizado BIT = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET @actualizado = 0;
    IF @id <= 0 OR NULLIF(LTRIM(RTRIM(@usuario_modificacion)), N'') IS NULL
        THROW 50001, N'El id debe ser positivo y el usuario de modificacion obligatorio.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Se conserva Cliente y sus referencias desde Factura.
        UPDATE dbo.Cliente
        SET estado = 'E', fecha_modificacion = GETDATE(),
            usuario_modificacion = @usuario_modificacion
        WHERE id = @id AND (estado IS NULL OR estado <> 'E');
        SET @actualizado = CASE WHEN @@ROWCOUNT = 1 THEN 1 ELSE 0 END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
