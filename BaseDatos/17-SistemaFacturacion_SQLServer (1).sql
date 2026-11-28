
USE PAPELITOSCR;
GO

-- =============================================
-- CRUD DE LA TABLA USUARIOS
-- PROYECTO: PAPELITOSCR
-- =============================================


-- =============================================
-- 1. CREATE - INSERTAR USUARIO
-- =============================================

CREATE OR ALTER PROCEDURE sp_Usuario_Insertar
    @IdRol INT = NULL,
    @NombreUsuario NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @NombreCompleto NVARCHAR(100) = NULL,
    @Email NVARCHAR(150) = NULL,
    @UsuarioCreacion NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuarios
    (
        id_rol,
        nombre_usuario,
        password_hash,
        nombre_completo,
        email,
        usuario_creacion
    )
    VALUES
    (
        @IdRol,
        @NombreUsuario,
        @PasswordHash,
        @NombreCompleto,
        @Email,
        @UsuarioCreacion
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END;
GO


-- =============================================
-- 2. READ - CONSULTAR TODOS LOS USUARIOS
-- =============================================

CREATE OR ALTER PROCEDURE sp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Usuarios;
END;
GO


-- =============================================
-- 3. READ - CONSULTAR USUARIO POR ID
-- =============================================

CREATE OR ALTER PROCEDURE sp_Usuario_Consultar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Usuarios
    WHERE id = @Id;
END;
GO


-- =============================================
-- 4. UPDATE - ACTUALIZAR USUARIO
-- =============================================

CREATE OR ALTER PROCEDURE sp_Usuario_Actualizar
    @Id INT,
    @IdRol INT = NULL,
    @NombreUsuario NVARCHAR(50),
    @NombreCompleto NVARCHAR(100) = NULL,
    @Email NVARCHAR(150) = NULL,
    @UsuarioModificacion NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET
        id_rol = @IdRol,
        nombre_usuario = @NombreUsuario,
        nombre_completo = @NombreCompleto,
        email = @Email,
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @UsuarioModificacion
    WHERE id = @Id;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO


-- =============================================
-- 5. DELETE - CAMBIAR ESTADO DEL USUARIO
-- ELIMINACION LOGICA
-- =============================================

CREATE OR ALTER PROCEDURE sp_Usuario_CambiarEstado
    @Id INT,
    @Estado CHAR(1),
    @UsuarioModificacion NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET
        estado = @Estado,
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @UsuarioModificacion
    WHERE id = @Id;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO


-- =============================================
-- FIN DEL CRUD DE USUARIOS
-- =============================================
