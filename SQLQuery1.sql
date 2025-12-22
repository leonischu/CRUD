use DemoDB

CREATE TABLE Products(
ProductID INT IDENTITY(1,1) PRIMARY KEY,
ProductName VARCHAR(255) NOT NULL,
SupplierID INT,
CategoryID INT,
UnitPrice DECIMAL(18,2),
UnitsInStock INT,
UnitsOnOrder INT,
Discontinued BIT NOT NULL,
DiscontinuedDate DATETIME2
);


