create procedure sp_InsertarTelefono
    @entidad_id int,
    @numero nvarchar(20),
    @usuario_creacion nvarchar(50)
as
begin
    insert into Telefonos (
        entidad_id,
        numero,
        usuario_creacion
    )
    values (
        @entidad_id,
        @numero,
        @usuario_creacion
    );
end;
go

create procedure sp_ListarTelefonos
as
begin
    select *
    from Telefonos
    where estado = 'A';
end;
go

create procedure sp_ActualizarTelefono
    @id int,
    @entidad_id int,
    @numero nvarchar(20),
    @usuario_modificacion nvarchar(50)
as
begin
    update Telefonos
    set
        entidad_id = @entidad_id,
        numero = @numero,
        fecha_modificacion = GETDATE(),
        usuario_modificacion = @usuario_modificacion
    where id = @id
      AND estado = 'A';
end;
go

create procedure sp_EliminarTelefono
    @id int
as
begin
    update Telefonos
    set
        estado = 'I',
        fecha_modificacion = GETDATE(),
        usuario_modificacion = 'Jack Cambronero'
    WHERE id = @id
      AND estado = 'A';
end;
go