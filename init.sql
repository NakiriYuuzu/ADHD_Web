CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
                                                       `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
                                                       `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
                                                       CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Players` (
                           `Id` char(36) COLLATE ascii_general_ci NOT NULL,
                           `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
                           `Age` int NOT NULL,
                           `Gender` longtext CHARACTER SET utf8mb4 NOT NULL,
                           `CreatedAt` datetime(6) NOT NULL,
                           CONSTRAINT `PK_Players` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `LevelRecords` (
                                `Id` char(36) COLLATE ascii_general_ci NOT NULL,
                                `PlayerId` char(36) COLLATE ascii_general_ci NOT NULL,
                                `LevelNumber` int NOT NULL,
                                `Difficulty` longtext CHARACTER SET utf8mb4 NOT NULL,
                                `StartTime` datetime(6) NOT NULL,
                                `EndTime` datetime(6) NOT NULL,
                                `CreatedAt` datetime(6) NOT NULL,
                                CONSTRAINT `PK_LevelRecords` PRIMARY KEY (`Id`),
                                CONSTRAINT `FK_LevelRecords_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `Players` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_LevelRecords_PlayerId` ON `LevelRecords` (`PlayerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240415172109_InitialCreate', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE `LevelRecords` DROP COLUMN `EndTime`;

ALTER TABLE `LevelRecords` DROP COLUMN `StartTime`;

ALTER TABLE `LevelRecords` ADD `Score` int NOT NULL DEFAULT 0;

ALTER TABLE `LevelRecords` ADD `Time` int NOT NULL DEFAULT 0;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240417082157_UpdateLevelRecordModel', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` ADD `Password` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240506065512_User', '8.0.4');

COMMIT;

