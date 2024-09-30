-- ��������� ���� ����� (����� CoffeeShop �� ���� ��'� ���� �����)
CREATE DATABASE CoffeeShop;
GO

-- ������������ ���� �����
USE CoffeeShop;
GO

-- ��������� ������� "Client"
CREATE TABLE Client (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);

-- ��������� ������� "Drink"
CREATE TABLE Drink (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Price DECIMAL(10, 2)
);

-- ��������� ������� "Employee"
CREATE TABLE Employee (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Position NVARCHAR(50)
);

-- ��������� ������� "Supplier"
CREATE TABLE Supplier (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Phone NVARCHAR(15)
);

-- ��������� ������� "Order"
CREATE TABLE [Order] (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME,
    ClientID INT FOREIGN KEY REFERENCES Client(ID),
    EmployeeID INT FOREIGN KEY REFERENCES Employee(ID)
);

-- ���������� ������� ������
-- ��������� �볺���
INSERT INTO Client (FirstName, LastName, Phone) VALUES 
('����', '��������', '123456789'),
('����', '��������', '111222333'),
('����', '���������', '222333444');

-- ��������� �����
INSERT INTO Drink (Name, Price) VALUES 
('����', 50.00),
('���', 30.00),
('ѳ�', 40.00);

-- ��������� �����������
INSERT INTO Employee (FirstName, LastName, Position) VALUES 
('������', '���������', '�������'),
('��������', '����������', '��������');

-- ��������� �������������
INSERT INTO Supplier (Name, Phone) VALUES 
('���� ������������', '987654321'),
('��� ������������', '654321987');

-- ��������� ���������
INSERT INTO [Order] (Date, ClientID, EmployeeID) VALUES 
(GETDATE(), 1, 1),  -- ����, ������
(GETDATE(), 2, 1),  -- ����, ������
(GETDATE(), 3, 2);  -- ����, ��������
