use PAPELITOSCR;
go
-- 1. CREATE (Insertar) - devuelve el id generado
CREATE OR ALTER PROCEDURE sp_Facturas_Insertar
    @id_cliente INT,
    @id_emisor INT,
    @total DECIMAL(18,2) = NULL,
    @usuario_creacion NVARCHAR(50),
    @id INT OUTPUT
AS
BEGIN
    INSERT INTO Factura (id_cliente, id_emisor, fecha_emision, total, usuario_creacion, fecha_creacion, estado)
    VALUES (@id_cliente, @id_emisor, GETDATE(), @total, @usuario_creacion, GETDATE(), 'A');

    SET @id = CAST(SCOPE_IDENTITY() AS INT);
END;
GO

-- 2. READ (Consultar)
CREATE OR ALTER PROCEDURE sp_Facturas_Consultar
    @id INT = NULL
AS
BEGIN
    IF @id IS NULL
        SELECT * FROM Factura WHERE estado != 'E';
    ELSE
        SELECT * FROM Factura WHERE id = @id AND estado != 'E';
END;
GO

-- 3. UPDATE (Actualizar)
CREATE OR ALTER PROCEDURE sp_Facturas_Actualizar
    @id INT,
    @total DECIMAL(18,2),
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Factura
    SET total = @total,
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id AND estado != 'E';
END;
GO

-- 4. DELETE (Lógico - Cambiar estado a 'E')
CREATE OR ALTER PROCEDURE sp_Facturas_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Factura
    SET estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id AND estado != 'E';
END;
GO
