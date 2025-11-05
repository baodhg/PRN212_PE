USE master;
GO

CREATE DATABASE SU25JLPTMockTestDB;
GO

USE SU25JLPTMockTestDB;
GO

/*
JLPTAccount Table:
- AccountID: Unique identifier for each user.
- Password: Password for authentication.
- Email: Unique email for user login.
- Role: Defines user roles (1: Admin, 2: Manager, 3: Staff, 4: Member).
- CreatedDate: Timestamp of account creation.
*/
CREATE TABLE JLPTAccount (
  AccountID INT PRIMARY KEY,
  Password NVARCHAR(60) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL,
  Role INT CHECK (Role BETWEEN 1 AND 4),
  CreatedDate SMALLDATETIME DEFAULT GETDATE()
);
GO

INSERT INTO JLPTAccount VALUES (101, N'@1', 'admin@jlpt.com', 1, GETDATE());
INSERT INTO JLPTAccount VALUES (102, N'@1', 'manager@jlpt.com', 2, GETDATE());
INSERT INTO JLPTAccount VALUES (103, N'@1', 'staff@jlpt.com', 3, GETDATE());
INSERT INTO JLPTAccount VALUES (104, N'@1', 'member@jlpt.com', 4, GETDATE());
GO

/*
Candidate Table:
- CandidateID: Unique identifier for each candidate.
- FullName: Candidate's full name.
- JLPTLevel: JLPT level (N1–N5).
- StudyGoal: Candidate's personal study goal.
*/
CREATE TABLE Candidate (
  CandidateID INT PRIMARY KEY,
  FullName NVARCHAR(120) NOT NULL,
  JLPTLevel CHAR(2) NOT NULL,
  StudyGoal NVARCHAR(255) NOT NULL
);
GO

INSERT INTO Candidate VALUES (201, N'Emma Johansson', 'N3', N'Pass JLPT N3 in December');
INSERT INTO Candidate VALUES (202, N'Liam Smith', 'N2', N'Achieve high score for N2');
INSERT INTO Candidate VALUES (203, N'Sofia Muller', 'N1', N'Become a certified translator');
INSERT INTO Candidate VALUES (204, N'Carlos Martinez', 'N4', N'Improve conversation fluency');
GO

/*
MockTest Table:
- TestID: Unique identifier for the test.
- TestTitle: Name of the test.
- SkillArea: Skill area (Vocabulary, Grammar, etc.).
- StartTime: Start time of the test.
- EndTime: End time of the test.
- CandidateID: Reference to Candidate table.
- Score: Test score (0–180).
*/
CREATE TABLE MockTest (
  TestID INT PRIMARY KEY,
  TestTitle NVARCHAR(150) NOT NULL,
  SkillArea NVARCHAR(50) NOT NULL,
  StartTime TIME NOT NULL,
  EndTime TIME NOT NULL,
  CandidateID INT FOREIGN KEY REFERENCES Candidate(CandidateID) ON DELETE CASCADE ON UPDATE CASCADE,
  Score FLOAT CHECK (Score BETWEEN 0 AND 180)
);
GO

INSERT INTO MockTest VALUES (301, N'Vocabulary Practice Test N3', N'Vocabulary', '08:00', '09:00', 201, 125.5);
INSERT INTO MockTest VALUES (302, N'Grammar Challenge N2', N'Grammar', '10:00', '11:30', 202, 138.0);
INSERT INTO MockTest VALUES (303, N'Reading Comprehension N1', N'Reading', '13:00', '14:00', 203, 158.75);
INSERT INTO MockTest VALUES (304, N'Listening Drill N4', N'Listening', '15:00', '16:00', 204, 110.25);
INSERT INTO MockTest VALUES (305, N'N3 Mock Test Full Skills', N'Mixed Skills', '09:00', '11:00', 201, 132.0);
INSERT INTO MockTest VALUES (306, N'N2 Listening Speed Practice', N'Listening', '14:00', '15:00', 202, 145.5);
INSERT INTO MockTest VALUES (307, N'N1 Kanji and Grammar Session', N'Kanji', '10:30', '12:00', 203, 172.0);
INSERT INTO MockTest VALUES (308, N'N4 Basic Vocabulary Check', N'Vocabulary', '08:30', '09:30', 204, 98.0);
GO
