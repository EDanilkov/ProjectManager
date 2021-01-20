CREATE DATABASE ProjectManager
GO

USE ProjectManager
GO

CREATE TABLE dbo.[User](
  Id uniqueidentifier not null DEFAULT newid() primary key,
  photoBase64 text,
  Username nvarchar(50) NOT NULL,
  Password nvarchar(50) NOT NULL,
  RefreshToken nvarchar(50),
  RefreshTokenExpiryTime datetime
)
GO

CREATE TABLE dbo.Project(
  Id uniqueidentifier not null DEFAULT newid() primary key,
  Name nvarchar(50) NOT NULL,
  AdminId uniqueidentifier NOT NULL
)
GO

CREATE TABLE dbo.UserProject(
  Id uniqueidentifier not null DEFAULT newid() primary key,
  ProjectId uniqueidentifier NOT NULL,
  UserId uniqueidentifier NOT NULL,
  RoleId uniqueidentifier NOT NULL
)
GO

CREATE TABLE dbo.[Role](
  Id uniqueidentifier not null DEFAULT newid() primary key,
  Name nvarchar(50) NOT NULL
)
GO

ALTER TABLE [User]
ADD CONSTRAINT TM_User_Username_Unique UNIQUE (Username)
GO

ALTER TABLE UserProject
WITH CHECK ADD CONSTRAINT FK_UserProject_User FOREIGN KEY(UserId)
REFERENCES [User](Id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE UserProject
WITH CHECK ADD CONSTRAINT FK_UserProject_Project FOREIGN KEY(ProjectId)
REFERENCES Project (Id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE UserProject
WITH CHECK ADD CONSTRAINT FK_UserProject_Role FOREIGN KEY(RoleId)
REFERENCES [Role] (Id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

INSERT INTO [Role]([Name]) 
VALUES ('admin'),
     ('guest')
GO