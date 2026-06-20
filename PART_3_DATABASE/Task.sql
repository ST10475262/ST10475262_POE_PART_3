CREATE TABLE [dbo].[Task]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(100) NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [ReminderDate] DATE NULL DEFAULT NULL, 
    [IsCompleted] BIT NOT NULL DEFAULT 0
)
