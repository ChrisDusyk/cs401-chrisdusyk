CREATE TABLE Customers
(
    CustomerID      INT AUTO_INCREMENT,
    CustomerName    VARCHAR(200) NOT NULL,
    Address         VARCHAR(200),
    City            VARCHAR(100),
    Province        VARCHAR(30),
    PostalCode      VARCHAR(7),

    PRIMARY KEY(CustomerID),
    INDEX(CustomerID)
);

CREATE TABLE Employees
(
    EmployeeID      INT AUTO_INCREMENT,
    FirstName       VARCHAR(50),
    LastName        VARCHAR(50),
    Email           VARCHAR(100),
    AspNetUserID    VARCHAR(128),

    PRIMARY KEY(EmployeeID),
    INDEX(EmployeeID),

    FOREIGN KEY(AspNetUserID)
        REFERENCES AspNetUsers(Id)
);

CREATE TABLE Products
(
    ProductID       INT AUTO_INCREMENT,
    ProductCode     VARCHAR(5),
    ProductName     VARCHAR(200),

    PRIMARY KEY (ProductID),
    INDEX (ProductID)
);

CREATE TABLE CustomerProducts
(
    CustomerProductID   INT AUTO_INCREMENT,
    ProductID           INT,
    SoldByID            INT,
    CustomerID          INT,
    SoldDate            DATETIME,

    PRIMARY KEY (CustomerProductID),
    INDEX (CustomerProductID, ProductID, CustomerID),

    FOREIGN KEY (ProductID)
        REFERENCES Products(ProductID),

    FOREIGN KEY (SoldByID)
        REFERENCES Employees(EmployeeID),

    FOREIGN KEY (CustomerID)
        REFERENCES Customers(CustomerID)
);