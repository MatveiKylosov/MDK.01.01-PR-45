CREATE TABLE `Users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Password` varchar(1000) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Token` varchar(16) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci