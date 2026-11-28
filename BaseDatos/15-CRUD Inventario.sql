USE PAPELITOSCR;
GO

-- Registra existencias y devuelve el ID generado.
CREATE OR ALTER PROCEDURE dbo.sp_InsertarInventario
    @id_producto INT,
    @cantidad_existencia INT,
    @usuario_creacion NVARCHAR(50),
    @id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @id_producto IS NULL OR @id_producto <= 0
       OR @cantidad_existencia IS NULL OR @cantidad_existencia < 0
       OR NULLIF(TRIM(@usuario_creacion), '') IS NULL
        THROW 51000, N'Datos de inventario inválidos.', 1;

    INSERT INTO dbo.Inventario (id_producto, cantidad_existencia, fecha_creacion, usuario_creacion, estado)
    VALUES (@id_producto, @cantidad_existencia, GETDATE(), @usuario_creacion, 'A');

    SET @id = CONVERT(INT, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ConsultarInventario
    @id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id, id_producto, cantidad_existencia, fecha_creacion, usuario_creacion,
           fecha_modificacion, usuario_modificacion, estado
    FROM dbo.Inventario
    WHERE (estado <> 'E' OR estado IS NULL)
      AND (@id IS NULL OR id = @id)
    ORDER BY id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_ActualizarInventario
    @id INT,
    @id_producto INT,
    @cantidad_existencia INT,
    @usuario_modificacion NVARCHAR(50),
    @filas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF @id_producto IS NULL OR @id_producto <= 0
       OR @cantidad_existencia IS NULL OR @cantidad_existencia < 0
       OR NULLIF(TRIM(@usuario_modificacion), '') IS NULL
        THROW 51000, N'Datos de inventario inválidos.', 1;

    UPDATE dbo.Inventario
    SET id_producto = @id_producto,
        cantidad_existencia = @cantidad_existencia,
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @usuario_modificacion
    WHERE id = @id AND (estado <> 'E' OR estado IS NULL);

    SET @filas = @@ROWCOUNT;
END;
GO

-- Baja lógica con auditoría.
CREATE OR ALTER PROCEDURE dbo.sp_EliminarInventario
    @id INT,
    @usuario_modificacion NVARCHAR(50),
    @filas INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NULLIF(TRIM(@usuario_modificacion), '') IS NULL
        THROW 51000, N'Indique el usuario de modificación.', 1;

    UPDATE dbo.Inventario
    SET estado = 'E',
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @usuario_modificacion
    WHERE id = @id AND (estado <> 'E' OR estado IS NULL);

    SET @filas = @@ROWCOUNT;
END;
GO
