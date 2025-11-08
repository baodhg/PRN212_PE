USE master;
GO

CREATE DATABASE Sp25PlantInventoryDB;
GO

USE Sp25PlantInventoryDB;
GO

/*
SystemUser Table:
- UserID: Unique identifier for each user.
- UserPassword: Password for authentication.
- UserEmail: Unique email for user login.
- UserRole: Defines user roles (1: Admin, 2: Manager, 3: Staff, 4: Member).
- RegistrationDate: Timestamp of account creation.
*/
CREATE TABLE SystemUser (
  UserID INT PRIMARY KEY,
  UserPassword NVARCHAR(60) NOT NULL,
  UserEmail NVARCHAR(100) UNIQUE NOT NULL,
  UserRole INT CHECK (UserRole BETWEEN 1 AND 4),
  RegistrationDate DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO SystemUser VALUES (1, N'@2', 'admin@plants.com', 1, GETDATE());
INSERT INTO SystemUser VALUES (2, N'@2', 'manager@plants.com', 2, GETDATE());
INSERT INTO SystemUser VALUES (3, N'@2', 'staff@plants.com', 3, GETDATE());
INSERT INTO SystemUser VALUES (4, N'@2', 'member@plants.com', 4, GETDATE());
GO

/*
Plant Table:
- PlantID: Unique identifier for each plant.
- Name: Name of the plant.
- Type: Type or category of the plant.
- Origin: Country or region of origin.
*/
CREATE TABLE Plant (
  PlantID INT PRIMARY KEY,
  Name NVARCHAR(100) NOT NULL,
  Type NVARCHAR(100) NOT NULL,
  Origin NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Plant VALUES (1, N'Rose', N'Flower', N'France');
INSERT INTO Plant VALUES (2, N'Orchid', N'Flower', N'Thailand');
INSERT INTO Plant VALUES (3, N'Maple', N'Tree', N'Canada');
INSERT INTO Plant VALUES (4, N'Cactus', N'Succulent', N'Mexico');
GO

/*
Inventory Table:
- InventoryID: Unique identifier for each inventory record.
- Quantity: Number of items in stock.
- Price: Price per unit of the plant.
- ArrivalDate: Date when the inventory was received.
- ShelfLife: Number of days the plant remains viable.
- Supplier: Supplier name.
- PlantID: Foreign key reference to Plant table.
*/
CREATE TABLE Inventory (
  InventoryID INT PRIMARY KEY,
  Quantity INT NOT NULL CHECK (Quantity >= 0),
  Price DECIMAL(18,2) NOT NULL CHECK (Price >= 0),
  ArrivalDate DATE NOT NULL,
  ShelfLife INT NOT NULL CHECK (ShelfLife > 0),
  Supplier NVARCHAR(150) NOT NULL,
  PlantID INT FOREIGN KEY REFERENCES Plant(PlantID) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

INSERT INTO Inventory VALUES (1, 50, 15.99, '2024-02-01', 30, N'GreenWorld Supplies', 1);
INSERT INTO Inventory VALUES (2, 30, 25.50, '2024-02-05', 45, N'Tropical Blooms', 2);
INSERT INTO Inventory VALUES (3, 20, 45.75, '2024-02-10', 60, N'NatureGrow', 3);
INSERT INTO Inventory VALUES (4, 40, 10.00, '2024-02-15', 90, N'Desert Flora Co.', 4);
INSERT INTO Inventory VALUES (5, 25, 18.50, '2024-02-20', 30, N'FlowerLand', 1);
INSERT INTO Inventory VALUES (6, 35, 22.00, '2024-02-25', 45, N'Exotic Plants Inc.', 2);
INSERT INTO Inventory VALUES (7, 15, 50.00, '2024-03-01', 60, N'Forest Trees Ltd.', 3);
INSERT INTO Inventory VALUES (8, 60, 12.50, '2024-03-05', 90, N'Succulent Wonders', 4);
GO
