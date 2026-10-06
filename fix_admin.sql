CREATE TABLE IF NOT EXISTS tbladmin (
  AdminID int(11) NOT NULL AUTO_INCREMENT,
  FullName varchar(100) DEFAULT NULL,
  Username varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  Attempts int(12) NOT NULL DEFAULT 0,
  Status varchar(20) NOT NULL DEFAULT '1',
  PRIMARY KEY (AdminID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT IGNORE INTO tbladmin (FullName, Username, Password, Status) VALUES ('Admin User', 'admin', 'admin123', '1');
