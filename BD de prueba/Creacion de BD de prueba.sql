CREATE TABLE Productos
(
	ProductID INT IDENTITY(1,1)
	CONSTRAINT pk_producto PRIMARY KEY(ProductID),
	Nombre VARCHAR(100),
	Precio DECIMAL(8,2),
	Stock INT 
)
GO

INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 1','5.2',100)
INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 2','4.3',20)
INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 3','22',80)
INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 4','7',400)
INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 5','14.2',300)
INSERT INTO Productos(Nombre, Precio, Stock) VALUES ('Producto 6','1.4',500)
go

CREATE PROC dbo.uspGetProductos
AS
BEGIN
	SELECT ProductID, Nombre, Precio, Stock FROM dbo.Productos
END
