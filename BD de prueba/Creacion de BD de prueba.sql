CREATE TABLE Productos
(
	ProductID int identity(1,1)
	constraint pk_producto primary key(ProductID),
	Nombre varchar(100),
	Precio decimal(8,2),
	Stock int 
)
go

insert into Productos(Nombre, Precio, Stock) values ('Producto 1','5.2',100)
insert into Productos(Nombre, Precio, Stock) values ('Producto 2','4.3',20)
insert into Productos(Nombre, Precio, Stock) values ('Producto 3','22',80)
insert into Productos(Nombre, Precio, Stock) values ('Producto 4','7',400)
insert into Productos(Nombre, Precio, Stock) values ('Producto 5','14.2',300)
insert into Productos(Nombre, Precio, Stock) values ('Producto 6','1.4',500)
go

CREATE PROC dbo.uspGetProductos
AS
BEGIN
	SELECT ProductID, Nombre, Precio, Stock FROM dbo.Productos
END
