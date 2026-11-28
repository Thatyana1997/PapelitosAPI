--DetalleFactura
use PAPELITOSCR;
go
-- 1. CREATE
CREATE PROCEDURE sp_DetalleFactura_Insertar
    @id_factura INT,
    @id_producto INT,
    @cantidad INT,
    @precio_unitario DECIMAL(18,2),
    @usuario_creacion NVARCHAR(50)
AS
BEGIN
    INSERT INTO DetalleFactura
    (id_factura, id_producto, cantidad, precio_unitario,
     usuario_creacion, fecha_creacion, estado)
    VALUES
    (@id_factura, @id_producto, @cantidad, @precio_unitario,
     @usuario_creacion, GETDATE(), 'A');
END;
GO

-- 2. READ
CREATE PROCEDURE sp_DetalleFactura_Consultar
    @id INT = NULL
AS
BEGIN
    IF @id IS NULL
        SELECT * FROM DetalleFactura WHERE estado != 'E';
    ELSE
        SELECT * FROM DetalleFactura WHERE id = @id AND estado != 'E';
END;
GO

-- 3. UPDATE
CREATE PROCEDURE sp_DetalleFactura_Actualizar
    @id INT,
    @id_factura INT,
    @id_producto INT,
    @cantidad INT,
    @precio_unitario DECIMAL(18,2),
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE DetalleFactura
    SET id_factura = @id_factura,
        id_producto = @id_producto,
        cantidad = @cantidad,
        precio_unitario = @precio_unitario,
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO

-- 4. DELETE
CREATE PROCEDURE sp_DetalleFactura_Eliminar
    @id INT,
    @usuario_modificacion NVARCHAR(50)
AS
BEGIN
    UPDATE DetalleFactura
    SET estado = 'E',
        usuario_modificacion = @usuario_modificacion,
        fecha_modificacion = GETDATE()
    WHERE id = @id;
END;
GO
