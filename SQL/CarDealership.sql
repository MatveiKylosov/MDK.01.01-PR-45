CREATE TABLE `CarBrands` (
  `BrandName` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `CountryOrigin` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ManufacturerFactory` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`BrandName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE `Cars` (
  `CarID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Stamp` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `YearProduction` int(11) DEFAULT NULL,
  `Colour` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Category` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`CarID`),
  KEY `Stamp` (`Stamp`),
  CONSTRAINT `cars_ibfk_1` FOREIGN KEY (`Stamp`) REFERENCES `CarBrands` (`BrandName`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE `Customers` (
  `CustomersID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PassportDetails` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `City` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `DateOfBirth` date DEFAULT NULL,
  `Gender` tinyint(1) DEFAULT NULL,
  `Password` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CustomersID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE `Employees` (
  `EmployeeID` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Experience` int(11) DEFAULT NULL,
  `Salary` decimal(10,2) DEFAULT NULL,
  `Password` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE `Sales` (
  `SaleID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomersID` int(11) DEFAULT NULL,
  `EmployeeID` int(11) DEFAULT NULL,
  `CarID` int(11) DEFAULT NULL,
  `DateSale` datetime DEFAULT NULL,
  PRIMARY KEY (`SaleID`),
  KEY `CustomersID` (`CustomersID`),
  KEY `EmployeeID` (`EmployeeID`),
  KEY `CarID` (`CarID`),
  CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`CustomersID`) REFERENCES `Customers` (`CustomersID`),
  CONSTRAINT `sales_ibfk_2` FOREIGN KEY (`EmployeeID`) REFERENCES `Employees` (`EmployeeID`),
  CONSTRAINT `sales_ibfk_3` FOREIGN KEY (`CarID`) REFERENCES `Cars` (`CarID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;