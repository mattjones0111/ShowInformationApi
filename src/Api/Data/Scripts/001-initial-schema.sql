CREATE TABLE [dbo].[Document]
(
	Id INT NOT NULL,
	TypeName VARCHAR(255) NOT NULL,
	[State] VARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_Document_Id_TypeName PRIMARY KEY (Id, TypeName)
)
