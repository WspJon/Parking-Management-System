-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 02, 2026 at 07:50 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `parkingdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblcustomer`
--

CREATE TABLE `tblcustomer` (
  `CustomerID` int(11) NOT NULL,
  `Fullname` varchar(50) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Attempts` int(12) NOT NULL,
  `Status` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblcustomer`
--

INSERT INTO `tblcustomer` (`CustomerID`, `Fullname`, `Username`, `Password`, `Attempts`, `Status`) VALUES
(1, 'jancris tiu', 'chu', '1234', 0, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tblparkingrecord`
--

CREATE TABLE `tblparkingrecord` (
  `TransactionID` int(11) NOT NULL,
  `code` varchar(50) NOT NULL,
  `PlateNumber` varchar(20) NOT NULL,
  `VehicleType` varchar(50) DEFAULT NULL,
  `Parking Slot` varchar(50) DEFAULT NULL,
  `CheckIn` datetime DEFAULT NULL,
  `CheckOut` datetime DEFAULT NULL,
  `RateName` varchar(50) DEFAULT NULL,
  `Rate` decimal(10,2) DEFAULT NULL,
  `Duration` varchar(50) DEFAULT NULL,
  `TotalAmount` decimal(10,2) DEFAULT NULL,
  `Paid Status` varchar(50) DEFAULT 'Pending',
  `ReservationDate` datetime DEFAULT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `TellerID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tblteller`
--

CREATE TABLE `tblteller` (
  `TellerID` int(11) NOT NULL,
  `FullName` varchar(100) DEFAULT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Attempts` int(12) NOT NULL,
  `Status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblteller`
--

INSERT INTO `tblteller` (`TellerID`, `FullName`, `Username`, `Password`, `Attempts`, `Status`) VALUES
(1, 'shouno sabesaje', 'ichi', '1234', 0, '1'),
(2, 'joshua villacorte', 'josh', '1234', 0, '1');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblcustomer`
--
ALTER TABLE `tblcustomer`
  ADD PRIMARY KEY (`CustomerID`);

--
-- Indexes for table `tblparkingrecord`
--
ALTER TABLE `tblparkingrecord`
  ADD PRIMARY KEY (`TransactionID`),
  ADD KEY `fk_parking_customer` (`CustomerID`),
  ADD KEY `fk_parking_teller` (`TellerID`);

--
-- Indexes for table `tblteller`
--
ALTER TABLE `tblteller`
  ADD PRIMARY KEY (`TellerID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblcustomer`
--
ALTER TABLE `tblcustomer`
  MODIFY `CustomerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tblparkingrecord`
--
ALTER TABLE `tblparkingrecord`
  MODIFY `TransactionID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tblteller`
--
ALTER TABLE `tblteller`
  MODIFY `TellerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tblparkingrecord`
--
ALTER TABLE `tblparkingrecord`
  ADD CONSTRAINT `fk_parking_customer` FOREIGN KEY (`CustomerID`) REFERENCES `tblcustomer` (`CustomerID`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_parking_teller` FOREIGN KEY (`TellerID`) REFERENCES `tblteller` (`TellerID`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
