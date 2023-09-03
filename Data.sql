-- SQL (SEQUEL)
-- * domänspecifikt programmerinngsspråk för att hantera RDBMS:en + för att
-- skapa upp tabeller, jobba med data, etc.
-- * uppdelat i subspråk
-- DDL = Data Definition Language (CREATE DATABASE, CREATE TABLE)
-- DML = Data Manipulation Language (INSERT INTO, UPDATE, DELETE)
-- DQL = Data Query Language (SELECT)



CREATE DATABASE StudentManager_v2
GO

USE StudentManager_v2
GO

DROP TABLE Student

-- Primärnyckel
-- * Måste vara unik (värdet)
-- * Får inte vara null (värdet)
-- * Får inte förändras vid någon tidpunkt (värdet)

-- Surogatnyckel
-- * fiktivt värde som är unikt

-- UNIQUE

CREATE TABLE Student
(
    -- Surogatnyckel = fiktiv nyckel/värde som vi använder som
    -- primärnyckel då det saknas en bra naturlig nyckel i detta fallet
    -- IDENTITY = genererar och sätter automatiskt ett heltal med start från 1 och
    -- uppåt
    Id INT IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    -- Type INT NOT NULL,
    SocialSecurityNumber NVARCHAR(20) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Program NVARCHAR(50) NOT NULL,
    -- Specificerar att Id ska vara primärnyckel
    PRIMARY KEY (Id),
    -- Specificerar att det enbart får finnas en rad i tabellen som har
    -- ett specifikt registreringsnummer
    UNIQUE (SocialSecurityNumber)
)

-- DML (Data Manipulation Language) (INSERT INTO, UPDATE, DELETE)
-- DQL (Data Query Language) (SELECT)

INSERT INTO Student
    (FirstName, LastName, SocialSecurityNumber, PhoneNumber, Email, Program)
VALUES
    ('Adam', 'Algren', '19901010-1020', '0707-455365', 'Adam@mail.com', 'Frontend-utvecklare');

UPDATE Student SET PhoneNumber = 'BBB222'
WHERE PhoneNumber = 'AAA111'

DELETE FROM Student 
WHERE PhoneNumber = 'BBB222'

DELETE FROM Student 
WHERE PhoneNumber = 'ABC123'

-- Select allting
SELECT *
FROM Student

-- Select specifics
SELECT FirstName, PhoneNumber
FROM Student

-- Select even more specific
SELECT FirstName, LastName
FROM Student
WHERE Program = 'AAA111'

