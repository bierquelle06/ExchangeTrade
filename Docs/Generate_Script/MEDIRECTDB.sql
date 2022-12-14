USE [MEDIRECTDB]
GO
/****** Object:  Schema [app]    Script Date: 23.11.2022 06:58:19 ******/
CREATE SCHEMA [app]
GO
/****** Object:  Schema [banks]    Script Date: 23.11.2022 06:58:19 ******/
CREATE SCHEMA [banks]
GO
/****** Object:  Table [app].[InternalCommands]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[InternalCommands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnqueueDate] [datetime2](7) NOT NULL,
	[Type] [varchar](255) NOT NULL,
	[Data] [varchar](max) NOT NULL,
	[ProcessedDate] [datetime2](7) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_InternalCommands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [banks].[Bank]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[BankAccount]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[BankAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[BankAccountTypeId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[IntegratorId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[OpenDate] [datetime2](7) NULL,
	[Name] [nvarchar](100) NULL,
	[Code] [nvarchar](100) NULL,
	[Number] [nvarchar](100) NULL,
	[Iban] [nvarchar](50) NULL,
	[TotalBalance] [decimal](18, 2) NULL,
	[TotalCreditBalance] [decimal](18, 2) NULL,
	[TotalCreditCardBalance] [decimal](18, 2) NULL,
	[BlockBalanceLimit] [decimal](18, 2) NULL,
	[BlockCreditLimit] [decimal](18, 2) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[BankAccountActivity]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[BankAccountActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankAccountId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NULL,
	[CurrencyCode] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[ProcessID] [int] NULL,
	[ProcessName] [nvarchar](150) NULL,
	[ProcessDate] [datetime2](7) NULL,
	[Balance] [decimal](18, 2) NULL,
	[ReceiptNo] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_BankAccountActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [banks].[BankAccountActivityLog]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[BankAccountActivityLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankAccountId] [int] NULL,
	[ProcessId] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_BankAccountActivityLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [banks].[BankAccountType]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[BankAccountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_BankAccountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[BankBranch]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[BankBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_BankBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[Currency]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[CurrencyActivity]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[CurrencyActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyId] [int] NULL,
	[Date] [nvarchar](50) NULL,
	[TimeStamp] [nvarchar](50) NULL,
	[SymbolBase] [nvarchar](50) NULL,
	[Symbol] [nvarchar](50) NULL,
	[Rate] [decimal](18, 2) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_CurrencyActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[Customer]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [banks].[Integrator]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [banks].[Integrator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Code] [nvarchar](150) NULL,
	[Host] [nvarchar](150) NULL,
	[Port] [nvarchar](50) NULL,
	[Url] [nvarchar](200) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[DeleteDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Integrator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23.11.2022 06:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [banks].[Bank] ON 

INSERT [banks].[Bank] ([Id], [Name], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, N'HSBC BANK', CAST(N'2022-08-29T02:57:13.3349476' AS DateTime2), NULL, NULL, 0)
INSERT [banks].[Bank] ([Id], [Name], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (2, N'APS BANK', CAST(N'2022-08-29T03:01:41.8650245' AS DateTime2), NULL, NULL, 0)
SET IDENTITY_INSERT [banks].[Bank] OFF
GO
SET IDENTITY_INSERT [banks].[BankAccount] ON 

INSERT [banks].[BankAccount] ([Id], [CustomerId], [BankAccountTypeId], [CurrencyId], [IntegratorId], [BankId], [OpenDate], [Name], [Code], [Number], [Iban], [TotalBalance], [TotalCreditBalance], [TotalCreditCardBalance], [BlockBalanceLimit], [BlockCreditLimit], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, 1, 1, 1, 3, 1, CAST(N'2022-08-29T11:15:09.5040000' AS DateTime2), N'Malta Usd Test', N'101', N'0012345MTLCAST001S', N'MT50KMOY22822895465547546474464', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-08-29T14:03:02.2025704' AS DateTime2), CAST(N'2022-08-29T14:16:06.2608374' AS DateTime2), NULL, 0)
INSERT [banks].[BankAccount] ([Id], [CustomerId], [BankAccountTypeId], [CurrencyId], [IntegratorId], [BankId], [OpenDate], [Name], [Code], [Number], [Iban], [TotalBalance], [TotalCreditBalance], [TotalCreditCardBalance], [BlockBalanceLimit], [BlockCreditLimit], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (2, 1, 1, 2, 3, 1, CAST(N'2022-08-29T11:15:09.5040000' AS DateTime2), N'Malta Euro Test', N'102', N'0678910MTLCAST002S', N'MT73USFG58927493911958193114612', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-08-29T14:03:02.2025704' AS DateTime2), CAST(N'2022-08-29T14:16:06.2608374' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [banks].[BankAccount] OFF
GO
SET IDENTITY_INSERT [banks].[BankAccountActivity] ON 

INSERT [banks].[BankAccountActivity] ([Id], [BankAccountId], [Quantity], [CurrencyCode], [Description], [Source], [ProcessID], [ProcessName], [ProcessDate], [Balance], [ReceiptNo], [Note], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, 1, CAST(0.00 AS Decimal(18, 2)), N'string2', N'string', N'string', 1, N'string', CAST(N'2022-08-29T14:27:37.8960000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), N'string', N'string', CAST(N'2022-08-29T17:26:44.4656359' AS DateTime2), CAST(N'2022-08-29T17:27:48.6301387' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [banks].[BankAccountActivity] OFF
GO
SET IDENTITY_INSERT [banks].[BankAccountType] ON 

INSERT [banks].[BankAccountType] ([Id], [Name], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, N'CASH', CAST(N'2022-08-28T23:16:49.6212656' AS DateTime2), CAST(N'2022-08-28T23:45:05.8983904' AS DateTime2), NULL, 0)
INSERT [banks].[BankAccountType] ([Id], [Name], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (2, N'CREDIT', CAST(N'2022-08-28T23:16:49.6212656' AS DateTime2), CAST(N'2022-08-28T23:45:05.8983904' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [banks].[BankAccountType] OFF
GO
SET IDENTITY_INSERT [banks].[Currency] ON 

INSERT [banks].[Currency] ([Id], [Name], [Code], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, N'American Dollar', N'USD', CAST(N'2022-08-29T00:42:07.2236495' AS DateTime2), CAST(N'2022-08-29T00:51:31.5135567' AS DateTime2), NULL, 0)
INSERT [banks].[Currency] ([Id], [Name], [Code], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (2, N'Euro', N'EUR', CAST(N'2022-08-29T00:48:57.4719167' AS DateTime2), NULL, NULL, 0)
SET IDENTITY_INSERT [banks].[Currency] OFF
GO
SET IDENTITY_INSERT [banks].[CurrencyActivity] ON 

INSERT [banks].[CurrencyActivity] ([Id], [CurrencyId], [Date], [TimeStamp], [SymbolBase], [Symbol], [Rate], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, 1, N'2022-11-23', N'1669169763', N'USD', N'USD', CAST(1.00 AS Decimal(18, 2)), CAST(N'2022-11-23T05:16:27.8458219' AS DateTime2), NULL, NULL, 0)
INSERT [banks].[CurrencyActivity] ([Id], [CurrencyId], [Date], [TimeStamp], [SymbolBase], [Symbol], [Rate], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (2, 2, N'2022-11-23', N'1669169763', N'USD', N'EUR', CAST(0.97 AS Decimal(18, 2)), CAST(N'2022-11-23T05:16:27.8459577' AS DateTime2), NULL, NULL, 0)
INSERT [banks].[CurrencyActivity] ([Id], [CurrencyId], [Date], [TimeStamp], [SymbolBase], [Symbol], [Rate], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (3, 2, N'2022-11-23', N'1669169763', N'EUR', N'EUR', CAST(1.00 AS Decimal(18, 2)), CAST(N'2022-11-23T05:16:51.6599513' AS DateTime2), NULL, NULL, 0)
INSERT [banks].[CurrencyActivity] ([Id], [CurrencyId], [Date], [TimeStamp], [SymbolBase], [Symbol], [Rate], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (4, 1, N'2022-11-23', N'1669169763', N'EUR', N'USD', CAST(1.03 AS Decimal(18, 2)), CAST(N'2022-11-23T05:16:51.6599632' AS DateTime2), NULL, NULL, 0)
SET IDENTITY_INSERT [banks].[CurrencyActivity] OFF
GO
SET IDENTITY_INSERT [banks].[Customer] ON 

INSERT [banks].[Customer] ([Id], [Name], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (1, N'AYKUT AKTAS', CAST(N'2022-08-29T11:15:09.5040000' AS DateTime2), CAST(N'2022-08-29T11:15:09.5040000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [banks].[Customer] OFF
GO
SET IDENTITY_INSERT [banks].[Integrator] ON 

INSERT [banks].[Integrator] ([Id], [Name], [Code], [Host], [Port], [Url], [UserName], [Password], [CreateDate], [UpdateDate], [DeleteDate], [IsDelete]) VALUES (3, N'Exchange Rates Data API
', N'EXCHANGEAPILAYER', N'', N'', N'https://api.apilayer.com', N'', N'KJi6cNsdf9nSLQnpp3XoHmHCAOuqUjrb', CAST(N'2022-11-21T23:25:44.0700000' AS DateTime2), NULL, NULL, 0)
SET IDENTITY_INSERT [banks].[Integrator] OFF
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
ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_Customer] FOREIGN KEY([CustomerId])
REFERENCES [banks].[Customer] ([Id])
GO
ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Customer]
GO
ALTER TABLE [banks].[BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_BankAccount_Integrator] FOREIGN KEY([IntegratorId])
REFERENCES [banks].[Integrator] ([Id])
GO
ALTER TABLE [banks].[BankAccount] CHECK CONSTRAINT [FK_BankAccount_Integrator]
GO
ALTER TABLE [banks].[BankBranch]  WITH CHECK ADD  CONSTRAINT [FK_BankBranch_Bank] FOREIGN KEY([BankId])
REFERENCES [banks].[Bank] ([Id])
GO
ALTER TABLE [banks].[BankBranch] CHECK CONSTRAINT [FK_BankBranch_Bank]
GO
ALTER TABLE [banks].[CurrencyActivity]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyActivity_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [banks].[Currency] ([Id])
GO
ALTER TABLE [banks].[CurrencyActivity] CHECK CONSTRAINT [FK_CurrencyActivity_Currency]
GO
