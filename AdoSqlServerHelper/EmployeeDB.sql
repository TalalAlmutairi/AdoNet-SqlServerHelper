CREATE TABLE [dbo].[Employees](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NULL,
	[Age] [int] NULL,
	[CountryID] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Country](
	[CountryID] [int] NOT NULL,
	[CountryDesc] [varchar](50) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO

INSERT [dbo].[Country] ([CountryID], [CountryDesc]) VALUES (1, N'Saudi Arabia')
INSERT [dbo].[Country] ([CountryID], [CountryDesc]) VALUES (2, N'Kuwait')
INSERT [dbo].[Country] ([CountryID], [CountryDesc]) VALUES (3, N'United Arab Emirates')

SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmpID], [FirstName], [LastName], [Age], [CountryID]) VALUES (1, N'Naser', N'Ahmad', 30, 1)
INSERT [dbo].[Employees] ([EmpID], [FirstName], [LastName], [Age], [CountryID]) VALUES (2, N'Fahad', N'Majed', 28, 2)
INSERT [dbo].[Employees] ([EmpID], [FirstName], [LastName], [Age], [CountryID]) VALUES (3, N'Amal', N'Khaled', 27, 1)
INSERT [dbo].[Employees] ([EmpID], [FirstName], [LastName], [Age], [CountryID]) VALUES (4, N'New F Name', N'New L Name', 30, 2)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO

