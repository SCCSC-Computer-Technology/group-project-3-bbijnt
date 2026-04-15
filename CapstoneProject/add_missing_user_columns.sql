-- Script to add missing columns to AspNetUsers table
-- This will add all the custom properties from CapstoneProjectUser model

SET QUOTED_IDENTIFIER ON;

-- Add the missing columns to AspNetUsers table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'FirstName')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added FirstName column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'LastName')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added LastName column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'DOB')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [DOB] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added DOB column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'PhoneNum')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [PhoneNum] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added PhoneNum column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthAfricanAmerican')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthAfricanAmerican] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthAfricanAmerican column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthAsian')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthAsian] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthAsian column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthCaucasian')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthCaucasian] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthCaucasian column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthLatino')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthLatino] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthLatino column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthMiddleEastern')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthMiddleEastern] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthMiddleEastern column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthNativeAmerican')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthNativeAmerican] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthNativeAmerican column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthPacificIslander')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthPacificIslander] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthPacificIslander column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EthOther')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EthOther] bit NOT NULL DEFAULT 0;
    PRINT 'Added EthOther column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'Gender')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Gender] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added Gender column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'StudentStatus')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [StudentStatus] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added StudentStatus column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'AttendsSpartanburg')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AttendsSpartanburg] bit NOT NULL DEFAULT 0;
    PRINT 'Added AttendsSpartanburg column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'AttendsDowntown')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AttendsDowntown] bit NOT NULL DEFAULT 0;
    PRINT 'Added AttendsDowntown column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'AttendsCherokee')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AttendsCherokee] bit NOT NULL DEFAULT 0;
    PRINT 'Added AttendsCherokee column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'AttendsTygerRiver')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AttendsTygerRiver] bit NOT NULL DEFAULT 0;
    PRINT 'Added AttendsTygerRiver column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'AttendsUnion')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AttendsUnion] bit NOT NULL DEFAULT 0;
    PRINT 'Added AttendsUnion column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HouseholdBabiesToddlers')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HouseholdBabiesToddlers] tinyint NOT NULL DEFAULT 0;
    PRINT 'Added HouseholdBabiesToddlers column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HouseholdBabiesChildren')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HouseholdBabiesChildren] tinyint NOT NULL DEFAULT 0;
    PRINT 'Added HouseholdBabiesChildren column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HouseholdTeens')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HouseholdTeens] tinyint NOT NULL DEFAULT 0;
    PRINT 'Added HouseholdTeens column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HouseholdAdults')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HouseholdAdults] tinyint NOT NULL DEFAULT 0;
    PRINT 'Added HouseholdAdults column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HasTransportation')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HasTransportation] bit NULL;
    PRINT 'Added HasTransportation column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'Employed')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Employed] nvarchar(max) NOT NULL DEFAULT '';
    PRINT 'Added Employed column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'EmployedHouseMembers')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [EmployedHouseMembers] tinyint NOT NULL DEFAULT 0;
    PRINT 'Added EmployedHouseMembers column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HasSNAP')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HasSNAP] bit NOT NULL DEFAULT 0;
    PRINT 'Added HasSNAP column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HasWIC')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HasWIC] bit NOT NULL DEFAULT 0;
    PRINT 'Added HasWIC column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'HasTANF')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [HasTANF] bit NOT NULL DEFAULT 0;
    PRINT 'Added HasTANF column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'IsInterestedInSNAP')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsInterestedInSNAP] bit NOT NULL DEFAULT 0;
    PRINT 'Added IsInterestedInSNAP column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'IsInterestedInWIC')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsInterestedInWIC] bit NOT NULL DEFAULT 0;
    PRINT 'Added IsInterestedInWIC column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'IsInterestedInTANF')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsInterestedInTANF] bit NOT NULL DEFAULT 0;
    PRINT 'Added IsInterestedInTANF column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'Points')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Points] int NOT NULL DEFAULT 0;
    PRINT 'Added Points column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'MaxPoints')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [MaxPoints] int NOT NULL DEFAULT 0;
    PRINT 'Added MaxPoints column';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AspNetUsers' AND COLUMN_NAME = 'IsRegistrationComplete')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsRegistrationComplete] bit NOT NULL DEFAULT 0;
    PRINT 'Added IsRegistrationComplete column';
END

PRINT 'All missing columns have been checked and added if necessary.';
