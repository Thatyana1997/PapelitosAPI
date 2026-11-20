-- Tablas de Seguridad y Gestión de Acceso
create database PAPELITOSCR;
go
use PAPELITOSCR;
go
CREATE TABLE Roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    descripcion NVARCHAR(255),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_rol INT FOREIGN KEY REFERENCES Roles(id),
    nombre_usuario NVARCHAR(50) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    nombre_completo NVARCHAR(100),
    email NVARCHAR(150),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE TiposID (
    id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion NVARCHAR(100) NOT NULL,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Categorias (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Producto (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(150) NOT NULL,
    precio DECIMAL(18,2),
    id_categoria INT FOREIGN KEY REFERENCES Categorias(id),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Cliente (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    id_tipo_id INT FOREIGN KEY REFERENCES TiposID(id),
    identificacion NVARCHAR(50),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Emisor (
    id INT IDENTITY(1,1) PRIMARY KEY,
    razon_social NVARCHAR(200),
    identificacion NVARCHAR(50),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Factura (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT FOREIGN KEY REFERENCES Cliente(id),
    id_emisor INT FOREIGN KEY REFERENCES Emisor(id),
    fecha_emision DATETIME,
    total DECIMAL(18,2),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE DetalleFactura (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_factura INT FOREIGN KEY REFERENCES Factura(id),
    id_producto INT FOREIGN KEY REFERENCES Producto(id),
    cantidad INT,
    precio_unitario DECIMAL(18,2),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE FormasPago (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE DesgloseDetallePago (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_factura INT FOREIGN KEY REFERENCES Factura(id),
    id_forma_pago INT FOREIGN KEY REFERENCES FormasPago(id),
    monto DECIMAL(18,2),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Inventario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_producto INT FOREIGN KEY REFERENCES Producto(id),
    cantidad_existencia INT,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Provincia (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Canton (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_provincia INT FOREIGN KEY REFERENCES Provincia(id),
    nombre NVARCHAR(100),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Distrito (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_canton INT FOREIGN KEY REFERENCES Canton(id),
    nombre NVARCHAR(100),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE DireccionExacta (
    id INT IDENTITY(1,1) PRIMARY KEY,
    id_distrito INT FOREIGN KEY REFERENCES Distrito(id),
    detalle NVARCHAR(255),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Telefonos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    entidad_id INT,
    numero NVARCHAR(20),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Correos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    entidad_id INT,
    direccion NVARCHAR(150),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
go
CREATE TABLE Parametros (
    id INT IDENTITY(1,1) PRIMARY KEY,
    clave NVARCHAR(50),
    valor NVARCHAR(255),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    usuario_creacion NVARCHAR(50),
    fecha_modificacion DATETIME,
    usuario_modificacion NVARCHAR(50),
    estado CHAR(1) DEFAULT 'A' CHECK (estado IN ('A', 'E', 'I'))
);
