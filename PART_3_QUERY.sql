INSERT INTO dbo.Task(Title,Description,ReminderDate,IsCompleted)
VALUES('Enable 2fa', 'Remind me to enable 2fa for GitHub',NULL,0);

SELECT * FROM dbo.Task;
