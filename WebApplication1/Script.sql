CREATE TABLE [Departaments]
(
 [DepartamentId] int PRIMARY KEY,
 [Name] nvarchar(100) NOT NULL,

)
INSERT INTO [Departaments] ([DepartamentId], [Name])
VALUES (1, 'Programming'), (2,'Design')

CREATE TABLE [Employees]
(
 [EmployeeId] int PRIMARY KEY,
 [Name] nvarchar(100) NOT NULL,
 [DateOfBirth] date NOT NULL,
 [Description] nvarchar(max),
 [DepartmentId] int NOT NULL,
 [SupervisorId] int,
 [IsSupervisor] bit DEFAULT 0,
  FOREIGN KEY ([DepartmentId])  REFERENCES [Departaments] ([DepartamentId])
)