
use PapelitosCR;
go
		--FARITH	
		create procedure sp_TiposID_Insertar
		@descripcion NVARCHAR(100),
		@usuario_creacion NVARCHAR(50)

		as
		begin
			insert into TiposID (descripcion, usuario_creacion, fecha_creacion, estado)
			values (@descripcion, @usuario_creacion, getdate(), 'A');
		end;
		go

		create procedure sp_TiposID_Consultar
		@id INT = NULL

		as
		begin
			if @id is null
				select * from TiposID where estado != 'E';
			else
				select * from TiposID where id = @id and estado != 'E';
		end;
		go

		create procedure sp_TiposID_Actualizar
		@id INT,
		@descripcion NVARCHAR(100),
		@usuario_modificacion NVARCHAR(50)

		as
		begin
			update TiposID
			set descripcion = @descripcion,
				usuario_modificacion = @usuario_modificacion,
				fecha_modificacion = getdate()
			where id = @id;
		end;
		go

		create procedure sp_TiposID_Eliminar
		@id INT,
		@usuario_modificacion NVARCHAR(50)

		as
		begin
			update TiposID
			set estado = 'E',
				usuario_modificacion = @usuario_modificacion,
				fecha_modificacion = getdate()
			where id = @id;
		end;
		go
	






