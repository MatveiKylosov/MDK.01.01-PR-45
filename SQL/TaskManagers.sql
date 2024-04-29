CREATE TABLE `Tasks` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Priority` varchar(30) DEFAULT NULL,
  `DateExecute` date DEFAULT NULL,
  `Comment` varchar(1000) DEFAULT NULL,
  `Done` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4