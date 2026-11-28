USE PAPELITOSCR;
GO

CREATE OR ALTER PROCEDURE sp_CrearParametro
    @Clave           NVARCHAR(50),
    @Valor           NVARCHAR(255),
    @UsuarioCreacion NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Parametros
                   WHERE clave = @Clave AND estado != 'E')
        BEGIN
            RAISERROR('Ya existe un parámetro con la clave indicada.', 16, 1);
        END

        INSERT INTO Parametros (clave, valor, usuario_creacion, fecha_creacion, estado)
        VALUES (@Clave, @Valor, @UsuarioCreacion, GETDATE(), 'A');

        SELECT SCOPE_IDENTITY() AS id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @MensajeError NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@MensajeError, 16, 1);
    END CATCH;
END;
GO
CREATE OR ALTER PROCEDURE sp_ObtenerParametros
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id, clave, valor,
           fecha_creacion, usuario_creacion,
           fecha_modificacion, usuario_modificacion,
           estado
    FROM Parametros
    WHERE estado != 'E'
      AND (@Id IS NULL OR id = @Id);
END;
GO
CREATE OR ALTER PROCEDURE sp_BuscarParametroPorClave
    @Clave NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id, clave, valor,
           fecha_creacion, usuario_creacion,
           fecha_modificacion, usuario_modificacion,
           estado
    FROM Parametros
    WHERE clave LIKE '%' + @Clave + '%'
      AND estado != 'E';
END;
GO

CREATE OR ALTER PROCEDURE sp_ModificarParametro
    @Id                   INT,
    @Clave                NVARCHAR(50),
    @Valor                NVARCHAR(255),
    @UsuarioModificacion  NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Parametros
        SET clave                = @Clave,
            valor                = @Valor,
            usuario_modificacion = @UsuarioModificacion,
            fecha_modificacion   = GETDATE()
        WHERE id = @Id
          AND estado != 'E';

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('El parámetro no existe o fue eliminado.', 16, 1);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @MensajeError NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@MensajeError, 16, 1);
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE sp_EliminarParametro
    @Id                   INT,
    @UsuarioModificacion  NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE Parametros
        SET estado               = 'E',
            usuario_modificacion = @UsuarioModificacion,
            fecha_modificacion   = GETDATE()
        WHERE id = @Id
          AND estado != 'E';

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('El parámetro no existe o ya fue eliminado.', 16, 1);
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @MensajeError NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@MensajeError, 16, 1);
    END CATCH;
END;
GO