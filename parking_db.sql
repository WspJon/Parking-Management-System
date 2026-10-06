-- =====================================================================
-- Parking Management System - Database Setup Script
-- Database: parking_db
-- Compatibility: MySQL 5.7+ / MariaDB 10.3+
-- Default Admin Account: admin / 1234
-- =====================================================================

CREATE DATABASE IF NOT EXISTS `parking_db`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE `parking_db`;

SET FOREIGN_KEY_CHECKS = 0;

-- ---------------------------------------------------------------------
-- 1. Table structure for `tbladmin`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tbladmin` (
  `AdminID` INT(11) NOT NULL AUTO_INCREMENT,
  `FullName` VARCHAR(100) DEFAULT 'Administrator',
  `Username` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  `Attempts` INT(12) NOT NULL DEFAULT 0,
  `Status` VARCHAR(20) NOT NULL DEFAULT '1',
  PRIMARY KEY (`AdminID`),
  UNIQUE KEY `uq_admin_username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Seed default admin account (Username: admin, Password: 1234)
INSERT INTO `tbladmin` (`AdminID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`)
VALUES (1, 'Administrator', 'admin', '1234', 0, '1')
ON DUPLICATE KEY UPDATE
  `Password` = VALUES(`Password`),
  `Status` = VALUES(`Status`);

-- ---------------------------------------------------------------------
-- 2. Table structure for `tblcustomer`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tblcustomer` (
  `CustomerID` INT(11) NOT NULL AUTO_INCREMENT,
  `Fullname` VARCHAR(100) NOT NULL,
  `Username` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  `Attempts` INT(12) NOT NULL DEFAULT 0,
  `Status` VARCHAR(20) NOT NULL DEFAULT '1',
  PRIMARY KEY (`CustomerID`),
  UNIQUE KEY `uq_customer_username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Sample customer seed
INSERT INTO `tblcustomer` (`CustomerID`, `Fullname`, `Username`, `Password`, `Attempts`, `Status`)
VALUES (1, 'jancris tiu', 'chu', '1234', 0, '1')
ON DUPLICATE KEY UPDATE `CustomerID` = `CustomerID`;

-- ---------------------------------------------------------------------
-- 3. Table structure for `tblteller`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tblteller` (
  `TellerID` INT(11) NOT NULL AUTO_INCREMENT,
  `FullName` VARCHAR(100) NOT NULL,
  `Username` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  `Attempts` INT(12) NOT NULL DEFAULT 0,
  `Status` VARCHAR(20) NOT NULL DEFAULT '1',
  PRIMARY KEY (`TellerID`),
  UNIQUE KEY `uq_teller_username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Sample teller seed
INSERT INTO `tblteller` (`TellerID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`)
VALUES
  (1, 'shouno sabesaje', 'ichi', '1234', 0, '1'),
  (2, 'joshua villacorte', 'josh', '1234', 0, '1')
ON DUPLICATE KEY UPDATE `TellerID` = `TellerID`;

-- ---------------------------------------------------------------------
-- 4. Table structure for `tblrates`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tblrates` (
  `RateID` INT(11) NOT NULL AUTO_INCREMENT,
  `CarBaseRate` DECIMAL(10,2) NOT NULL DEFAULT 50.00,
  `MotorBaseRate` DECIMAL(10,2) NOT NULL DEFAULT 50.00,
  `CarSucceedingRate` DECIMAL(10,2) NOT NULL DEFAULT 20.00,
  `MotorSucceedingRate` DECIMAL(10,2) NOT NULL DEFAULT 10.00,
  `FreeHours` INT(11) NOT NULL DEFAULT 1,
  `OverstayPenaltyFee` DECIMAL(10,2) NOT NULL DEFAULT 200.00,
  `UpdatedAt` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`RateID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Seed default rates row
INSERT INTO `tblrates` (`RateID`, `CarBaseRate`, `MotorBaseRate`, `CarSucceedingRate`, `MotorSucceedingRate`, `FreeHours`, `OverstayPenaltyFee`)
VALUES (1, 50.00, 50.00, 20.00, 10.00, 1, 200.00)
ON DUPLICATE KEY UPDATE `RateID` = `RateID`;

-- ---------------------------------------------------------------------
-- 5. Table structure for `tblreservation`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tblreservation` (
  `ReservationID` INT(11) NOT NULL AUTO_INCREMENT,
  `CustomerID` INT(11) DEFAULT NULL,
  `CustomerName` VARCHAR(100) DEFAULT NULL,
  `ParkingSlot` VARCHAR(20) NOT NULL,
  `VehiclePlate` VARCHAR(20) NOT NULL,
  `VehicleType` VARCHAR(50) DEFAULT 'Four Wheels',
  `ReservationDate` DATE NOT NULL,
  `StartTime` TIME DEFAULT NULL,
  `EndTime` TIME DEFAULT NULL,
  `Status` VARCHAR(30) NOT NULL DEFAULT 'Reserved',
  `CreatedAt` DATETIME DEFAULT CURRENT_TIMESTAMP,
  `CheckedInAt` DATETIME DEFAULT NULL,
  `ProcessedByTellerID` INT(11) DEFAULT NULL,
  PRIMARY KEY (`ReservationID`),
  KEY `idx_res_slot_date_status` (`ParkingSlot`, `ReservationDate`, `Status`),
  KEY `idx_res_plate_status` (`VehiclePlate`, `Status`),
  KEY `fk_res_customer` (`CustomerID`),
  KEY `fk_res_teller` (`ProcessedByTellerID`),
  CONSTRAINT `fk_res_customer` FOREIGN KEY (`CustomerID`) REFERENCES `tblcustomer` (`CustomerID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_res_teller` FOREIGN KEY (`ProcessedByTellerID`) REFERENCES `tblteller` (`TellerID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 6. Table structure for `tblparkingrecord`
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `tblparkingrecord` (
  `TransactionID` INT(11) NOT NULL AUTO_INCREMENT,
  `code` VARCHAR(50) NOT NULL,
  `PlateNumber` VARCHAR(20) NOT NULL,
  `VehicleType` VARCHAR(50) NOT NULL,
  `Parking Slot` VARCHAR(20) NOT NULL,
  `CheckIn` DATETIME NOT NULL,
  `CheckOut` DATETIME DEFAULT NULL,
  `RateName` VARCHAR(50) DEFAULT 'Standard',
  `Rate` DECIMAL(10,2) NOT NULL DEFAULT 50.00,
  `Duration` VARCHAR(50) DEFAULT '0 hour(s)',
  `TotalAmount` DECIMAL(10,2) DEFAULT NULL,
  `Paid Status` VARCHAR(50) NOT NULL DEFAULT 'Not Paid',
  `DiscountType` VARCHAR(50) DEFAULT 'None',
  `PenaltyAmount` DECIMAL(10,2) DEFAULT 0.00,
  `AmountPaid` DECIMAL(10,2) DEFAULT 0.00,
  `ChangeAmount` DECIMAL(10,2) DEFAULT 0.00,
  `ReservationID` INT(11) DEFAULT NULL,
  `CustomerID` INT(11) DEFAULT NULL,
  `TellerID` INT(11) DEFAULT NULL,
  `ProcessedByName` VARCHAR(100) DEFAULT NULL,
  PRIMARY KEY (`TransactionID`),
  UNIQUE KEY `uq_parking_code` (`code`),
  KEY `idx_parking_slot_status` (`Parking Slot`, `Paid Status`),
  KEY `idx_parking_plate_status` (`PlateNumber`, `Paid Status`),
  KEY `fk_parking_customer` (`CustomerID`),
  KEY `fk_parking_teller` (`TellerID`),
  KEY `fk_parking_reservation` (`ReservationID`),
  CONSTRAINT `fk_parking_customer` FOREIGN KEY (`CustomerID`) REFERENCES `tblcustomer` (`CustomerID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_parking_teller` FOREIGN KEY (`TellerID`) REFERENCES `tblteller` (`TellerID`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_parking_reservation` FOREIGN KEY (`ReservationID`) REFERENCES `tblreservation` (`ReservationID`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

SET FOREIGN_KEY_CHECKS = 1;
