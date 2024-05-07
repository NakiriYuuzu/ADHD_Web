CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
                                                       `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
                                                       `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
                                                       CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

START TRANSACTION;

-- MariaDB might require specific privileges to alter database character set.
-- This command sets the default character set for the database.
ALTER DATABASE CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `Players` (
                           `Id` char(36) CHARACTER SET ascii NOT NULL,
                           `Name` longtext CHARACTER SET utf8mb4 NULL,
                           `Age` int NOT NULL,
                           `Gender` longtext CHARACTER SET utf8mb4 NULL,
                           `CreatedAt` datetime(6) NOT NULL,
                           CONSTRAINT `PK_Players` PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `Users` (
                         `Id` char(36) CHARACTER SET ascii NOT NULL,
                         `Username` longtext CHARACTER SET utf8mb4 NOT NULL,
                         `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
                         `Token` longtext CHARACTER SET utf8mb4 NULL,
                         `CreatedAt` datetime(6) NOT NULL,
                         CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `LevelRecords` (
                                `Id` char(36) CHARACTER SET ascii NOT NULL,
                                `PlayerId` char(36) CHARACTER SET ascii NOT NULL,
                                `LevelNumber` int NOT NULL,
                                `Difficulty` longtext CHARACTER SET utf8mb4 NOT NULL,
                                `Score` int NOT NULL,
                                `Time` int NOT NULL,
                                `CreatedAt` datetime(6) NOT NULL,
                                CONSTRAINT `PK_LevelRecords` PRIMARY KEY (`Id`),
                                CONSTRAINT `FK_LevelRecords_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Players` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE INDEX `IX_LevelRecords_PlayerId` ON `LevelRecords` (`PlayerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240507014137_InitialCreate', '8.0.4');

COMMIT;
