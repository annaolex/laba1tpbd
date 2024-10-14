-- Створення бази даних (заміна CoffeeShop на ваше ім'я бази даних)
CREATE DATABASE CoffeeShop;
GO

-- Використання бази даних
USE CoffeeShop;
GO

-- Створення таблиці "Client"
CREATE TABLE Clients (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);

-- Створення таблиці "Drink"
CREATE TABLE Drinks (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Price DECIMAL(10, 2)
);

-- Створення таблиці "Employee"
CREATE TABLE Employees (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Position NVARCHAR(50)
);

-- Створення таблиці "Supplier"
CREATE TABLE Suppliers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Phone NVARCHAR(15)
);

-- Створення таблиці "Order"
CREATE TABLE [Orders] (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME,
    ClientID INT FOREIGN KEY REFERENCES Client(ID),
    EmployeeID INT FOREIGN KEY REFERENCES Employee(ID)
);

-- Наповнення таблиць даними
-- Додавання клієнтів
INSERT INTO Clients (FirstName, LastName, Phone) VALUES 
('Іван', 'Петренко', '123456789'),
('Марія', 'Іваненко', '111222333'),
('Олег', 'Сидоренко', '222333444');

-- Додавання напоїв
INSERT INTO Drinks (Name, Price) VALUES 
('Кава', 50.00),
('Чай', 30.00),
('Сік', 40.00);

-- Додавання співробітників
INSERT INTO Employees (FirstName, LastName, Position) VALUES 
('Олексій', 'Коваленко', 'Бариста'),
('Светлана', 'Григоренко', 'Менеджер');

-- Додавання постачальників
INSERT INTO Suppliers (Name, Phone) VALUES 
('Кава Постачальник', '987654321'),
('Чай Постачальник', '654321987');

-- Додавання замовлень
INSERT INTO [Orders] (Date, ClientID, EmployeeID) VALUES 
(GETDATE(), 1, 1),  -- Іван, Олексій
(GETDATE(), 2, 1),  -- Марія, Олексій
(GETDATE(), 3, 2);  -- Олег, Светлана
