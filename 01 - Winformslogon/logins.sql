CREATE DATABASE LOGINS

use logins

CREATE table USUARIOS
(
ID INT IDENTITY(1,1) PRIMARY KEY,
USUARIO NVARCHAR(25) NOT NULL,
CLAVE NVARCHAR(25) NOT NULL,
ESTADO NVARCHAR(25) NOT NULL,
ROL NVARCHAR(25) NOT NULL
)

select * from usuarios;
insert into usuarios values ('edward', '123', 'Activo', 'Administrador');
insert into usuarios values ('angela', '111', 'Activo', 'Privilegiado');
go
ALTER PROC sp_USUARIOSS

@USUARIO NVARCHAR(25),
@CLAVE NVARCHAR(25)

AS
BEGIN
	SELECT U.USUARIO, U.CLAVE 
	FROM USUARIOS U
	WHERE U.USUARIO = @USUARIO
	AND U.CLAVE = @CLAVE
END
--por que
exec sp_USUARIOSS 'angela','111'

CREATE PROC sp_ListaUSUARIOS1
AS
BEGIN
	SELECT * 
	FROM USUARIOS 
	order by usuario
END

create procedure sp_registrodeusuarios
(
@USUARIO NVARCHAR(25),
@CLAVE NVARCHAR(25), 
@ESTADO NVARCHAR(25), 
@ROL NVARCHAR(25)
)
as
begin
insert into usuarios values (@usuario, @clave, @estado, @rol);
end

create PROC sp_rol
(
@usuario NVARCHAR(25)
)
AS
BEGIN
	SELECT rol 
	FROM USUARIOS 
	WHERE USUARIO = @USUARIO
END

CREATE PROC sp_ActualizarUsuarios
(
@USUARIO NVARCHAR(25),
@CLAVE NVARCHAR(25), 
@ESTADO NVARCHAR(25), 
@ROL NVARCHAR(25)
)
AS
BEGIN
	UPDATE USUARIOS
	SET USUARIO = @USUARIO,
	CLAVE = @CLAVE,
	ESTADO = @ESTADO,
	ROL = @ROL
	WHERE USUARIO = @USUARIO
END

CREATE PROC sp_Ultimocodigousuarios
AS
BEGIN
	SELECT TOP 1 id
	FROM USUARIOS
	ORDER BY ID DESC
END

create PROC sp_comprobarUsuario
(
@usuario NVARCHAR(25)
)
AS
BEGIN
	SELECT usuario 
	FROM USUARIOS 
	WHERE USUARIO = @USUARIO
END