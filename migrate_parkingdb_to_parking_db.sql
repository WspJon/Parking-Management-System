-- =====================================================================
-- Migration Script: parkingdb -> parking_db
-- Run this if you have an existing 'parkingdb' database and want to migrate
-- all existing data to 'parking_db' safely without data loss.
-- Compatible with both 'Slot' and 'Parking Slot' column naming conventions.
-- =====================================================================

CREATE DATABASE IF NOT EXISTS `parking_db`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE `parking_db`;

-- 1. Migrate Admin accounts (Preserve existing or set default)
INSERT IGNORE INTO `parking_db`.`tbladmin` (`FullName`, `Username`, `Password`, `Attempts`, `Status`)
SELECT `FullName`, `Username`, `Password`, `Attempts`, `Status`
FROM `parkingdb`.`tbladmin`
WHERE `Username` NOT IN (SELECT `Username` FROM `parking_db`.`tbladmin`);

-- Ensure default admin exists with 1234
INSERT INTO `parking_db`.`tbladmin` (`AdminID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`)
VALUES (1, 'Administrator', 'admin', '1234', 0, '1')
ON DUPLICATE KEY UPDATE
  `Password` = IF(`Username` = 'admin' AND `Password` = 'admin123', '1234', `Password`);

-- 2. Migrate Customers
INSERT IGNORE INTO `parking_db`.`tblcustomer` (`CustomerID`, `Fullname`, `Username`, `Password`, `Attempts`, `Status`)
SELECT `CustomerID`, `Fullname`, `Username`, `Password`, `Attempts`, `Status`
FROM `parkingdb`.`tblcustomer`;

-- 3. Migrate Tellers
INSERT IGNORE INTO `parking_db`.`tblteller` (`TellerID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`)
SELECT `TellerID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`
FROM `parkingdb`.`tblteller`;

-- 4. Migrate Rates
INSERT IGNORE INTO `parking_db`.`tblrates` (`RateID`, `CarBaseRate`, `MotorBaseRate`, `CarSucceedingRate`, `MotorSucceedingRate`, `FreeHours`, `OverstayPenaltyFee`)
SELECT 1, `CarBaseRate`, `MotorBaseRate`, `CarSucceedingRate`, `MotorSucceedingRate`, `FreeHours`, 200.00
FROM `parkingdb`.`tblrates`
LIMIT 1;

-- 5. Standardize column name in old parkingdb if it used Slot instead of Parking Slot
SET @has_slot := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='parkingdb' AND TABLE_NAME='tblparkingrecord' AND COLUMN_NAME='Slot');
SET @has_paidstatus := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='parkingdb' AND TABLE_NAME='tblparkingrecord' AND COLUMN_NAME='PaidStatus');

-- Migrate records dynamically handling column variances safely
DROP PROCEDURE IF EXISTS `parking_db`.`sp_migrate_parkingdb`;
DELIMITER $$
CREATE PROCEDURE `parking_db`.`sp_migrate_parkingdb`()
BEGIN
  IF EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA='parkingdb' AND TABLE_NAME='tblparkingrecord') THEN
    -- Check if column is 'Slot' or 'Parking Slot'
    IF EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='parkingdb' AND TABLE_NAME='tblparkingrecord' AND COLUMN_NAME='Slot') THEN
      -- tblparkingrecord uses `Slot` and `PaidStatus`
      INSERT INTO `parking_db`.`tblreservation` 
        (`CustomerID`, `CustomerName`, `ParkingSlot`, `VehiclePlate`, `VehicleType`, `ReservationDate`, `Status`, `CreatedAt`)
      SELECT 
        p.`CustomerID`,
        COALESCE(c.`Fullname`, 'Customer') AS `CustomerName`,
        p.`Slot` AS `ParkingSlot`,
        p.`PlateNumber` AS `VehiclePlate`,
        COALESCE(p.`VehicleType`, 'Four Wheels') AS `VehicleType`,
        COALESCE(DATE(p.`ReservationDate`), DATE(p.`CheckIn`), CURDATE()) AS `ReservationDate`,
        'Reserved' AS `Status`,
        COALESCE(p.`CheckIn`, NOW()) AS `CreatedAt`
      FROM `parkingdb`.`tblparkingrecord` p
      LEFT JOIN `parkingdb`.`tblcustomer` c ON p.`CustomerID` = c.`CustomerID`
      WHERE p.`PaidStatus` = 'Reserved'
        AND p.`PlateNumber` NOT IN ('', 'RESERVED')
        AND NOT EXISTS (
          SELECT 1 FROM `parking_db`.`tblreservation` r
          WHERE r.`VehiclePlate` = p.`PlateNumber` AND r.`Status` = 'Reserved'
        );

      INSERT INTO `parking_db`.`tblparkingrecord`
        (`code`, `PlateNumber`, `VehicleType`, `Parking Slot`, `CheckIn`, `CheckOut`, `RateName`, `Rate`, `Duration`, `TotalAmount`, `Paid Status`, `CustomerID`, `TellerID`)
      SELECT
        p.`code`,
        p.`PlateNumber`,
        p.`VehicleType`,
        p.`Slot`,
        p.`CheckIn`,
        p.`CheckOut`,
        p.`RateName`,
        p.`Rate`,
        p.`Duration`,
        p.`TotalAmount`,
        p.`PaidStatus`,
        p.`CustomerID`,
        p.`TellerID`
      FROM `parkingdb`.`tblparkingrecord` p
      WHERE p.`PaidStatus` != 'Reserved'
        AND NOT EXISTS (
          SELECT 1 FROM `parking_db`.`tblparkingrecord` pr
          WHERE pr.`code` = p.`code`
        );
    ELSE
      -- tblparkingrecord uses `Parking Slot` and `Paid Status`
      INSERT INTO `parking_db`.`tblreservation` 
        (`CustomerID`, `CustomerName`, `ParkingSlot`, `VehiclePlate`, `VehicleType`, `ReservationDate`, `Status`, `CreatedAt`)
      SELECT 
        p.`CustomerID`,
        COALESCE(c.`Fullname`, 'Customer') AS `CustomerName`,
        p.`Parking Slot` AS `ParkingSlot`,
        p.`PlateNumber` AS `VehiclePlate`,
        COALESCE(p.`VehicleType`, 'Four Wheels') AS `VehicleType`,
        COALESCE(DATE(p.`ReservationDate`), DATE(p.`CheckIn`), CURDATE()) AS `ReservationDate`,
        'Reserved' AS `Status`,
        COALESCE(p.`CheckIn`, NOW()) AS `CreatedAt`
      FROM `parkingdb`.`tblparkingrecord` p
      LEFT JOIN `parkingdb`.`tblcustomer` c ON p.`CustomerID` = c.`CustomerID`
      WHERE p.`Paid Status` = 'Reserved'
        AND p.`PlateNumber` NOT IN ('', 'RESERVED')
        AND NOT EXISTS (
          SELECT 1 FROM `parking_db`.`tblreservation` r
          WHERE r.`VehiclePlate` = p.`PlateNumber` AND r.`Status` = 'Reserved'
        );

      INSERT INTO `parking_db`.`tblparkingrecord`
        (`code`, `PlateNumber`, `VehicleType`, `Parking Slot`, `CheckIn`, `CheckOut`, `RateName`, `Rate`, `Duration`, `TotalAmount`, `Paid Status`, `CustomerID`, `TellerID`)
      SELECT
        p.`code`,
        p.`PlateNumber`,
        p.`VehicleType`,
        p.`Parking Slot`,
        p.`CheckIn`,
        p.`CheckOut`,
        p.`RateName`,
        p.`Rate`,
        p.`Duration`,
        p.`TotalAmount`,
        p.`Paid Status`,
        p.`CustomerID`,
        p.`TellerID`
      FROM `parkingdb`.`tblparkingrecord` p
      WHERE p.`Paid Status` != 'Reserved'
        AND NOT EXISTS (
          SELECT 1 FROM `parking_db`.`tblparkingrecord` pr
          WHERE pr.`code` = p.`code`
        );
    END IF;
  END IF;
END$$
DELIMITER ;

CALL `parking_db`.`sp_migrate_parkingdb`();
DROP PROCEDURE `parking_db`.`sp_migrate_parkingdb`;
