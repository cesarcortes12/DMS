--CREACION TABLAS  
CREATE DATABASE pruebaDMS4

use pruebaDMS4

--creacion de tablas

CREATE TABLE Clientes (
IdCliente int identity (1,1) primary key,
NombreCliente varchar(48),
ApellidoCliente varchar(48),
Telefono varchar(48)
)

CREATE TABLE Productos(
IdProducto int identity(1,1) primary key,
NombreProducto varchar(48),
PrecioProducto int,
Cantidad int,
fkIdCliente int
FOREIGN KEY (fkIdCliente) REFERENCES Clientes(IdCliente)
)	 


--crecion tipo de dato estrucutrado para la insercion dinamica

CREATE TYPE dbo.ClientesType AS TABLE
(
    NombreCliente NVARCHAR(48),
    ApellidoCliente NVARCHAR(48),
    Telefono NVARCHAR(48)
);



--CREACION PROCEDIMIENTOS CLIENTES

 --creacion sp_mostrarTodo

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_mostrarTodo] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT * FROM Clientes ORDER BY IdCliente 
END


--creacion sp_mostratCliente

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_mostrarCliente] 
@IdCliente INT	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Clientes WHERE IdCliente=@IdCliente 
END

exec sp_mostrarCliente 2



--sp insertarCliente

GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_insertarCliente] 
@NombreCliente varchar(48),
@ApellidoCliente varchar(48),
@Telefono varchar(48)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Clientes(NombreCliente,ApellidoCliente,Telefono)
	VALUES (@NombreCliente,@ApellidoCliente,@Telefono); 
END
GO


--creacion sp_delete

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_eliminarCliente] 
@IdCliente INT	
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Clientes
	WHERE IdCliente = @IdCliente; 
END
GO


--creacion sp_editarCliente

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_editarCliente]
@IdCliente INT,
@NombreCliente varchar(48),
@ApellidoCliente varchar(48),
@Telefono varchar(48)

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Clientes
	SET
		NombreCliente = @NombreCliente,
		ApellidoCliente = @ApellidoCliente,
		Telefono = @Telefono
	WHERE 
		IdCliente = @IdCliente;
END
GO



--creacion procedimiento para insertar 5 clientes de manera dinamica


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_insertarclientesDinami]
@Clientes dbo.ClientesType READONLY

AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO dbo.Clientes (NombreCliente,ApellidoCliente,Telefono)
	SELECT	NombreCliente,ApellidoCliente,Telefono
	FROM @Clientes;
END
GO








--CREACION PROCEDIMIENTOS PRODUCTOS

--creacion sp_mostratTodoProductos

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_mostrarTodoProductos] 
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT * FROM Productos ORDER BY IdProducto 
END



--creacion sp_mostrarProducto

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_mostrarProductobyId 
@IdProducto INT	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Productos WHERE IdProducto=@IdProducto 
END
GO


-- creacion sp insertarProducto

GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_insertarProducto] 
@NombreProducto varchar(48),
@PrecioProducto int,
@Cantidad int,
@fkIdCliente int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Productos(NombreProducto,PrecioProducto,Cantidad,fkIdCliente)
	VALUES (@NombreProducto,@PrecioProducto,@Cantidad,@fkIdCliente); 
END
GO



--creacion sp_editarProducto

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_editarProducto
@IdProducto INT,	
@NombreProducto varchar(48),
@PrecioProducto int,
@Cantidad int,
@fkIdCliente int

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Productos
	SET
		NombreProducto = @NombreProducto,
		PrecioProducto = @PrecioProducto,
		Cantidad = @Cantidad,
		fkIdCliente= @fkIdCliente
	WHERE 
		IdProducto = @IdProducto;
END
GO



--creacion sp_eliminarProducto

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_eliminarProducto] 
@IdProducto INT	
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Productos
	WHERE IdProducto = @IdProducto; 
END
GO


--exec sp_eliminarProducto 2
--exec sp_mostrarTodoProductos
--exec sp_insertarCliente 'isabel','cortes','87456'

--select *from Productos
--select * from Clientes

--exec sp_eliminarCliente 7

--exec sp_editarCliente 4 ,'pepeeditado','zapataeditado','55555'
--exec sp_editarProducto 1 ,'tveditado',1500000,7,2
--exec sp_insertarProducto 'celular',781000,5,1