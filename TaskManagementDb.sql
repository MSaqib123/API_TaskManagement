CREATE DATABASE Api_TaskManagement;
GO
USE Api_TaskManagement;
GO

-- Tasks Table
CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsCompleted BIT DEFAULT 0,
    Priority INT DEFAULT 1, -- 0:Low, 1:Medium, 2:High, 3:Urgent
    Category NVARCHAR(50) NULL,
    Status NVARCHAR(50) NULL, -- e.g., 'Open', 'InProgress', 'Completed'
    Recurrence NVARCHAR(50) NULL, -- e.g., 'Daily', 'Weekly'
    DueDate DATETIME NULL,
    OrderIndex INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);
GO

-- Routines Table
CREATE TABLE Routines (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(255) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    StartTime NVARCHAR(5) NULL, -- HH:mm
    EndTime NVARCHAR(5) NULL, -- HH:mm
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);
GO

-- Stored Procedures for Tasks
CREATE PROCEDURE sp_CreateTask
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX) = NULL,
    @Priority INT = 1,
    @Category NVARCHAR(50) = NULL,
    @Status NVARCHAR(50) = 'Open',
    @Recurrence NVARCHAR(50) = NULL,
    @DueDate DATETIME = NULL,
    @OrderIndex INT = 0,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET @Id = NEWID();
    INSERT INTO Tasks (Id, Title, Description, Priority, Category, Status, Recurrence, DueDate, OrderIndex)
    VALUES (@Id, @Title, @Description, @Priority, @Category, @Status, @Recurrence, @DueDate, @OrderIndex);
END
GO

CREATE PROCEDURE sp_UpdateTask
    @Id UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Priority INT,
    @Category NVARCHAR(50),
    @Status NVARCHAR(50),
    @Recurrence NVARCHAR(50),
    @DueDate DATETIME
AS
BEGIN
    UPDATE Tasks
    SET Title = @Title, Description = @Description, Priority = @Priority,
        Category = @Category, Status = @Status, Recurrence = @Recurrence, DueDate = @DueDate
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_ToggleTaskComplete
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Tasks
    SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_DeleteTask
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Tasks WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_GetAllTasks
    @SearchQuery NVARCHAR(255) = NULL,
    @Category NVARCHAR(50) = NULL,
    @Status NVARCHAR(50) = NULL,
    @Priority INT = NULL,
    @Recurrence NVARCHAR(50) = NULL
AS
BEGIN
    SELECT * FROM Tasks
    WHERE (@SearchQuery IS NULL OR Title LIKE '%' + @SearchQuery + '%')
    AND (@Category IS NULL OR Category = @Category)
    AND (@Status IS NULL OR Status = @Status)
    AND (@Priority IS NULL OR Priority = @Priority)
    AND (@Recurrence IS NULL OR Recurrence = @Recurrence)
    ORDER BY OrderIndex ASC;
END
GO

CREATE PROCEDURE sp_UpdateTaskOrder
    @Id UNIQUEIDENTIFIER,
    @NewOrderIndex INT
AS
BEGIN
    UPDATE Tasks SET OrderIndex = @NewOrderIndex WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_ClearCompletedTasks
AS
BEGIN
    DELETE FROM Tasks WHERE IsCompleted = 1;
END
GO

-- Stored Procedures for Routines
CREATE PROCEDURE sp_CreateRoutine
    @Title NVARCHAR(255),
    @Notes NVARCHAR(MAX) = NULL,
    @StartTime NVARCHAR(5) = NULL,
    @EndTime NVARCHAR(5) = NULL,
    @Id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    SET @Id = NEWID();
    INSERT INTO Routines (Id, Title, Notes, StartTime, EndTime)
    VALUES (@Id, @Title, @Notes, @StartTime, @EndTime);
END
GO

CREATE PROCEDURE sp_DeleteRoutine
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Routines WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_GetAllRoutines
AS
BEGIN
    SELECT * FROM Routines ORDER BY CreatedAt DESC;
END
GO

CREATE PROCEDURE sp_GetDueRoutines
    @CurrentTime NVARCHAR(5)
AS
BEGIN
    SELECT * FROM Routines WHERE StartTime = @CurrentTime;
END
GO

