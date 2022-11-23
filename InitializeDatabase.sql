--[banks] Schema
CREATE SCHEMA [banks] AUTHORIZATION dbo
GO

CREATE TABLE [banks].Branch
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Code] [nvarchar](150) NULL,
	[Type] [smallint] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
	CONSTRAINT [PK_banks_Branch_Id] PRIMARY KEY ([Id] ASC)
)
GO

INSERT INTO [banks].[Branch] VALUES ('C75399FC-CD06-4F61-93E8-0D2147B00557', 'Merkez', 'CNTR', 1, GETDATE(), GETDATE(), null, 0);
GO

CREATE TABLE [banks].[Bank]
(
	[Id] [uniqueidentifier] NOT NULL,
	[BranchId] [uniqueidentifier] NOT NULL,
	[UUID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](100) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
	CONSTRAINT [PK_banks_Bank_Id] PRIMARY KEY ([Id] ASC)
)
GO

ALTER TABLE [banks].[Bank]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Branch] FOREIGN KEY([BranchId])
REFERENCES [banks].[Branch] ([Id])
GO

ALTER TABLE [banks].[Bank] CHECK CONSTRAINT [FK_Bank_Branch]
GO

CREATE TABLE [banks].[BankAccountType]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
	CONSTRAINT [PK_banks_BankAccountType_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE [banks].[Integrator]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Code] [nvarchar](150) NULL,
	[Host] [nvarchar](150) NULL,
	[Port] [nvarchar](50) NULL,
	[FtpPath] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[AccCode] [nvarchar](150) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FtpSecure_Type] [smallint] NULL,
	[Type] [smallint] NULL,
	[IsActive] [bit] NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
	CONSTRAINT [PK_banks_Integrator_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE [banks].[Currency]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[Type] [smallint] NOT NULL,
	[Numerator] [decimal](18, 2) NULL,
	[Denominator] [decimal](18, 2) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
	[IsDefault] [bit] NULL,
	CONSTRAINT [PK_banks_Currency_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE [banks].[BankAccount]
(
	[Id] [uniqueidentifier] NOT NULL,
	[BankAccountTypeId] [uniqueidentifier] NOT NULL,
	[CurrencyId] [uniqueidentifier] NOT NULL,
	[IntegratorId] [uniqueidentifier] NOT NULL,
	[BankId] [uniqueidentifier] NOT NULL,
	[OpenDate] [datetime2](7) NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](100) NULL,
	[Number] [nvarchar](100) NULL,
	[IBAN] [nvarchar](50) NULL,
	[TotalBalance] [decimal](18, 2) NULL,
	[TotalCreditBalance] [decimal](18, 2) NULL,
	[TotalCreditCardBalance] [decimal](18, 2) NULL,
	[BlockBalanceLimit] [decimal](18, 2) NULL,
	[BlockCreditLimit] [decimal](18, 2) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
	CONSTRAINT [PK_banks_BankAccount_Id] PRIMARY KEY ([Id] ASC)
)
GO

ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_Bank] FOREIGN KEY([BankId])
REFERENCES [banks].[Bank] ([Id])
GO

ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Bank]
GO

ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_BankAccountType] FOREIGN KEY([BankAccountTypeId])
REFERENCES [banks].[BankAccountType] ([Id])
GO

ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_BankAccountType]
GO

ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [banks].[Currency] ([Id])
GO

ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Currency]
GO

ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_Integrator] FOREIGN KEY([IntegratorId])
REFERENCES [banks].[Integrator] ([Id])
GO

ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Integrator]
GO

CREATE TABLE [banks].[BankAccountActivity]
(
	[Id] [uniqueidentifier] NOT NULL,
	[BankAccountId] [uniqueidentifier] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Quantity] [decimal](18, 2) NULL,
	[CurrencyCode] [nvarchar](50) NULL,
	[TrxCode_01] [nvarchar](150) NULL,
	[TrxCode_02] [nvarchar](150) NULL,
	[Description] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[ProcessID] [nvarchar](max) NULL,
	[ProcessName] [nvarchar](150) NULL,
	[ProcessDate] [datetime2](7) NULL,
	[Balance] [decimal](18, 2) NULL,
	[OtherSideVKN] [nvarchar](max) NULL,
	[OtherAccNo] [nvarchar](max) NULL,
	[OtherSideIBAN] [nvarchar](max) NULL,
	[ReceiptNo] [nvarchar](max) NULL,
	[IsTransfer] [bit] NULL,
	[Transfer] [nvarchar](150) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
	[Note] [nvarchar](max) NULL,
	CONSTRAINT [PK_banks_BankAccountActivity_Id] PRIMARY KEY ([Id] ASC)
)
GO

ALTER TABLE [banks].[BankAccountActivity]  WITH CHECK ADD  CONSTRAINT [FK_BankAccountActivity_BankAccount] FOREIGN KEY([BankAccountId])
REFERENCES [banks].[BankAccount] ([Id])
GO

ALTER TABLE [banks].[BankAccountActivity] CHECK CONSTRAINT [FK_BankAccountActivity_BankAccount]
GO

CREATE TABLE [banks].[BankAccountActivityLog]
(
	[Id] [uniqueidentifier] NOT NULL,
	[BankAccountId] [uniqueidentifier] NULL,
	[ProcessId] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
	CONSTRAINT [PK_banks_BankAccountActivityLog_Id] PRIMARY KEY ([Id] ASC)
)
GO

--[app] Schema
CREATE SCHEMA [app] AUTHORIZATION dbo

CREATE TABLE [app].[InternalCommands]
(
	[Id] [uniqueidentifier] NOT NULL,
	[EnqueueDate] [datetime2](7) NOT NULL,
	[Type] [varchar](255) NOT NULL,
	[Data] [varchar](max) NOT NULL,
	[ProcessedDate] [datetime2](7) NULL,
	CONSTRAINT [PK_app_InternalCommands_Id] PRIMARY KEY ([Id] ASC)
)
GO