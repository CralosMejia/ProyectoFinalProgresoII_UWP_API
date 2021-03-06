USE [master]
GO
/****** Object:  Database [ProyectoFinalDB]    Script Date: 12/23/2021 5:11:27 PM ******/
CREATE DATABASE [ProyectoFinalDB]
GO
ALTER DATABASE [ProyectoFinalDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoFinalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoFinalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProyectoFinalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoFinalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoFinalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoFinalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoFinalDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ProyectoFinalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoFinalDB] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoFinalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoFinalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoFinalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoFinalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoFinalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoFinalDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProyectoFinalDB] SET QUERY_STORE = OFF
GO
USE [ProyectoFinalDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contactos]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contactos](
	[ContactoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[NombreContacto] [nvarchar](max) NULL,
	[CorreoContacto] [nvarchar](max) NULL,
	[TelefonoContacto] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Contactoes] PRIMARY KEY CLUSTERED 
(
	[ContactoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FechasImportantes]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FechasImportantes](
	[FechasImportantesID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[Titulo] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaLimite] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.FechasImportantes] PRIMARY KEY CLUSTERED 
(
	[FechasImportantesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notas]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notas](
	[NotaID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[Titulo] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Notas] PRIMARY KEY CLUSTERED 
(
	[NotaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pendientes]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pendientes](
	[PendienteID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[FechaLimite] [datetime] NOT NULL,
	[Prioridad] [int] NOT NULL,
	[ColorPrioridad] [nvarchar](max) NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Pendientes] PRIMARY KEY CLUSTERED 
(
	[PendienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/23/2021 5:11:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](max) NOT NULL,
	[Correo] [nvarchar](max) NOT NULL,
	[Contrasena] [nvarchar](max) NOT NULL,
	[ConfirmarContrasena] [nvarchar](max) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202112010000210_AutomaticMigration', N'VP_CM_CN_ProyectoProgresoI.Migrations.Configuration', 0x1F8B0800000000000400ED5CDB6EE42618BEAFD477B07CD556D938C9DEB4D14CABEC64538DBA39682759F52E22363341B5B10B384A54F5C97AD147EA2B149FC1808F734A53EDC526063E7EE0FB7FFFE08FFCF3D7DF939F9E03DF7A8284A2104FEDE3C323DB82D80D3D8457533B66CB77DFDB3FFDF8F557938F5EF06C7D29EABD4FEAF196984EED47C6A253C7A1EE230C003D0C904B421A2ED9A11B060EF042E7E4E8E807E7F8D8811CC2E6589635F91C63860298FEC27F9D85D885118B817F197AD0A7F9735EB24851AD2B10401A01174EED2F37F7B3CBFBD9D5FD0D095FA0CB42FEFF8A401ACE0FB3C6B675E623C00D5B407F695B00E39001C6CD3EBDA370C14888578B883F00FEED4B0479BD25F029CC87735A55EF3AB2A39364644ED5B0807263CAC2A027E0F1FB7CAA9C7AF341136E9753C927F3239F74F6928C3A9DD0A9CDE79D013E85B655EFEC74E693A46287F93E2C500E2C73DD83923B9C62C9BF036B16FB2C26708A61CC08F00FAC9BF8C147EE2FF0E536FC0DE2298E7D5F349F0F8097490FF823DE4304097BF90C97B541CDCF6DCB91DB3B7580B2B9A66D36FC3966EF4F6CEB8A1B031E7C58B24598AA050B09FC1962480083DE0D600C12BED8730FA6F3AD5851EBF38EC680A00E5D36C35C85C10381D58A66589CEFDC936DEB123C7F8278C51EA736FFD1B62ED033F48A2739FE1D46DCF1792346E2D6EE66212130DC5A77B7D087CB106FB0C32BF08456E982EAD7C7B63E433F2DA78F28CAA2CB615E765F997541C2E073E857EDCAB2FB5B40569071CB434385451813B766D9C4A9BCB6D1974B33C7B8720EB2279E2C78465F47EEEC54EBF3E3CC01CB6518CDCF9EFE9F39E42EFAC57CD129C460277D2F110900D9A511674F8001B2C58054059BB111A90838C6885484ACAEA65D715FD29A9514DC97BE5199243E5702A454A80B8E4DA6DC40EC21EEB9506B4F59AA334A29542C536B8C8ADDD9B48D09DC09C29E44EDC4942121BB68F7CAF2AE5BC4627FF309D039A42E41919B6E04F622F7E9E4D2F508A3F5F7413E23F8F718C72961F6C47B4A7B86B890D4F8ADFA51CF7ED7EB583D3BBF80EE23F8840294D038EBFC9C2FC22D0AFA63DDF04520C803DEB87598857E4814AC8DC5B58F9401AF5CF60F218F1200B71B3D2862F57CE9D76397392D1814C0D2B5A7F3200A09031C958E0B640ADC9E0434C5AE21814D0BF25603DC0E13857586AF412EAC1041E7CAC64A8A4B9B6BF671ED334A4317A5A6CA43A87661F2D03F62CF6ADB92652B276CE9F8FA71174511774A6EC3D43EB6EBCE778DCFA10F19B4CEDCECBC7906A80B3C75EAF938BCCE16957B9FCAA26A73299BF49DD2138F0390246E067CDE88F2C88230538306C22E8A80DF3229B576FDCE8C9261971DD54BCE61C4833BB7B36512465B5076545B90B6799A3802C99AB927E5E5A655D627E9D50A673BD2B6D535E06918B30D0E0FE29ACEF0EDF14CB70CAF81636A26652246435A55B143D8CB75A75CC329CD2BE09DD1FAED91CFB834AF8181E644C0C4970E5941C51B4D72DE9D99ED59C56B6068EB28B6C7D4D6A5DB3FC66699629A3F20BEF12892F30F672BDE8FEEB0F78EC27CB747F3E4BB4EAF04700199F215A04A4A95F44CA1A88C51724F81284B5A10921718D5B4CFF28796C642D8570084B216148DAB2A689A3A3554611995F9113EA608D54C1F5CEADCEA90EC9743129644E16887145DC03172C09147DA6116E4835E7506CC09677BCA29589C53A961DCDA24B37DEE060C597352A48EBB2509EA980609F61B39DF31EFD9C85434ECB8D529E9F856EEF95E16C6D5EAC83D5FC4A3A6AC380F28A37C59367132E15AFE60E218146E934B104508AF04C55BFEC45A6472B7D9BB457FE1579061382ED5E8BF4A6BCB9E5848C00AD64A9310E2C10B44283B070C3C80E40867E6054AB5F29D6608D34537B5D796BA6A45CC2E1A243F678DBA6BD03499400E77C1C7182429457A1AA88B944A532BD11F021F900695D82CF4E300B729CF9AD084BC44046B4857CC5875119808582FEB8E5AD77AC96396CBBAA3AA922E11572D5591274E6D6D95B44E21959229CB24ED446173481BC360036A07021B5B6E8F7165EC5409674C68DAF8A6E3593F944A1354F7D2E2792F34556A5483552B74C72F54442264F16C6F989FA568EBE57DBA53E84F7A7D33334B336D894C4F9D4EA509659D7E537C0D92625EFEAC3B8AF4B14784920AF6863F5582BB5E0E19713B10A9A1AD69D625998538EB0DE28DB7C12B139EF4A150C4930ABAE309FA0469FEABC77D22BB2C789083BA5CD61DB5D0358868C5B3BDF1477537B55EBF6CC5EFE09F1D301A5957570D28EC6BD62634E1FFEFB726BFDD289F956D77BD4AD97BB9FDAE6DB327F996B7FDB699B207CEAAD8169FA827E425FBDFC50B6530384C2A1C2E7EF7677EF206A82A5C028C9690B24C45639F1C1D9FD46EA8EDCF6D318752CFD71C19E8AE8CC9EBB5839B592899E556694F4F918EA2F5497B510EEBE7D883CF53FB8FB4D5A935FFF5BE6C78605D13BEECA7D691F5E79A2E71E12740B8BB916F02F0FCAD8839FCA2D62848D365AC1EA0BDAF350DA0DB3AAF0F6D846CDA1B43C6491C700B682C56FD52CD683CD36D9D71C0F20D9C4DB050DD6D6EF32EC47F31D2C96AC851E148A378DC04070C1BC5DDC8FADF302586AAFAC7016AA4AE1E9F75B616A57E3ACF6BD1E98F7224598BFF80DA8C1A21751FE0429B1692BF6197DA7C945DA7436D4E86BD43D9B5F2870DB6A7B3DEA290ABE133EDAE055B0385D4BB674E26B5EAADDD7E258C317C1EDA3FB6749744EF9E3282D068980AFB9590A7E9BBD0FE31A8BFA479F74CD2E8B1C6A9A85F09B3BA7CD9D835C3F4126455D565F854D447679C9D8AF30CEE21216396B915A51AB1ED001DB2AE87BCB01D3F7B231A44CA3A64AD1675A87E59D74159DCDECB407DB3AED7BD9241F7963DD799B8A73AE7A1C313495A495CF650CB3C74808ABB348B9EF751AB3C74E8461FEE266A5E8F0659FDE8C95F2EC21FE2E42F398A561544F267393174A5D74A59678E9761F182AB595454A96DF82F21037C870FCE08434BEEB2BCD88594A637D3BF003F4E0EA08207E8CDF175CCA298F121C3E0C197AEE7276FC9A6FE53A1B56CF3E43A4AEF90AF6308DC4C941C525CE30F31F2BDD2EE0BCD2185012279FDE6E74BC95AB2E49C69F552225D85B823503E7D65D6700B83C8E760F41A2FC0131C62DB1D859FE00AB82FC5B76B3348FB42C8D33E394760454040738CAA3DFF9573D80B9E7FFC17DC06A7928F560000, N'6.1.3-40302')
GO
SET IDENTITY_INSERT [dbo].[Contactos] ON 

INSERT [dbo].[Contactos] ([ContactoID], [UsuarioID], [NombreContacto], [CorreoContacto], [TelefonoContacto]) VALUES (1, 1, N'Tio Juan', N'jua@udla.com', N'0997928764')
INSERT [dbo].[Contactos] ([ContactoID], [UsuarioID], [NombreContacto], [CorreoContacto], [TelefonoContacto]) VALUES (2, 2, N'Juan el mecanico', N'mecaJuan@udla.edu.ec', N'0992787312')
INSERT [dbo].[Contactos] ([ContactoID], [UsuarioID], [NombreContacto], [CorreoContacto], [TelefonoContacto]) VALUES (3, 3, N'Caleb', N'calbv@udla.edu.ec', N'0992927123')
SET IDENTITY_INSERT [dbo].[Contactos] OFF
GO
SET IDENTITY_INSERT [dbo].[FechasImportantes] ON 

INSERT [dbo].[FechasImportantes] ([FechasImportantesID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite]) VALUES (1, 1, N'cumpleaños de mi abuela', N'fiesta en la casa de mi abuela', CAST(N'2022-11-30T19:14:00.000' AS DateTime))
INSERT [dbo].[FechasImportantes] ([FechasImportantesID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite]) VALUES (2, 2, N'Presentacion de tesis', N'defiendo la tesis', CAST(N'2022-11-30T19:14:00.000' AS DateTime))
INSERT [dbo].[FechasImportantes] ([FechasImportantesID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite]) VALUES (3, 3, N'Ir a misa', N'Tengo que ir a la misa de una año de mi abuelo', CAST(N'2022-11-30T19:14:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[FechasImportantes] OFF
GO
SET IDENTITY_INSERT [dbo].[Notas] ON 

INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (1, 1, N'Limpiar cas', N'Hay que limpiar la casa')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (2, 2, N'sacar perros', N'Hay que sacar los perros')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (3, 3, N'Depositar el arriendo', N'Tengo que depositar el arriendo')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (4, 8, N'Jugar con mi primo', N'jugar con mi primo')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (5, 9, N'Lavar platos', N'Tengo que lavar los platos')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (6, 1, N'Llamar abuelos', N'tengo que llamar a mis abuelos')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (7, 1, N'Comprar cola', N'hay que tener algo que temoar para el almuerzo')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (8, 1, N'Hacer deberes Ingles', N'Mandaron muchos deberes de ingles')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (9, 1, N'Estudiar ', N'Estudiar para base de datos')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (10, 1, N'Entrevista', N'Mañana tengo una entrevista')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (11, 1, N'Leer ', N'tengo que leer 5 libros en una semana para ganar una apuesta')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (12, 1, N'Comprar disfraz', N'tengo qeu comprar un disfraz para el programa del colegio')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (13, 1, N'limpiar cuarto', N'mañana tengo visitas asi que hay que tener limpio el cuarto')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (14, 1, N'Lavar ropa', N'no he lavado la ropa 3 semanas')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (15, 1, N'Llevar materiales', N'mañana tengo prueba y necesito llevar los materiales')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (16, 1, N'comprar circuitos', N'tengo que comprar los circuitos para el examen de ubicua')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (17, 1, N'descargar programas', N'descargar todos los programas necesarios para programacion')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (18, 1, N'hacer el proyecto', N'tengo que terminar el proyecto de marketing')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (19, 1, N'llamar tia susy', N'le operaron y tengo que llamar')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (20, 1, N'hacer mercado', N'ya no hay nada en la casa hay que hacer mercado.')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (21, 2, N'Hacer deberes', N'deberes de progra')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (22, 2, N'Lavar los platos', N'Labar los plato')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (23, 2, N'bañar al perro', N'tengo que bañar al bruno')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (24, 2, N'sacar la basura', N'tengo que sacar la basura')
INSERT [dbo].[Notas] ([NotaID], [UsuarioID], [Titulo], [Descripcion]) VALUES (25, 2, N'Ver pelicula', N'quiero ver una pelicula que me recomendaron')
SET IDENTITY_INSERT [dbo].[Notas] OFF
GO
SET IDENTITY_INSERT [dbo].[Pendientes] ON 

INSERT [dbo].[Pendientes] ([PendienteID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite], [Prioridad], [ColorPrioridad], [Estado]) VALUES (1, 1, N'Prueba pendiente 1cdd', N'sss', CAST(N'2021-11-30T19:14:00.000' AS DateTime), 2, N'#FBAB62', 0)
INSERT [dbo].[Pendientes] ([PendienteID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite], [Prioridad], [ColorPrioridad], [Estado]) VALUES (6, 2, N'Deber Progra', N'es el deber del edy', CAST(N'2021-11-30T19:14:00.000' AS DateTime), 2, N'#FBAB62', 0)
INSERT [dbo].[Pendientes] ([PendienteID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite], [Prioridad], [ColorPrioridad], [Estado]) VALUES (7, 3, N'Deber Ciencias', N'Es el deber de ciencias', CAST(N'2021-11-30T19:14:00.000' AS DateTime), 2, N'#FBAB62', 0)
INSERT [dbo].[Pendientes] ([PendienteID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite], [Prioridad], [ColorPrioridad], [Estado]) VALUES (8, 8, N'Tarea quimica', N'Es un deber de quimica', CAST(N'2021-11-30T19:14:00.000' AS DateTime), 2, N'#FBAB62', 0)
INSERT [dbo].[Pendientes] ([PendienteID], [UsuarioID], [Titulo], [Descripcion], [FechaLimite], [Prioridad], [ColorPrioridad], [Estado]) VALUES (9, 9, N'Deber de ingles', N'Es un deber de ingles', CAST(N'2021-11-30T19:14:00.000' AS DateTime), 2, N'#FBAB62', 0)
SET IDENTITY_INSERT [dbo].[Pendientes] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (1, N'CarlosME1', N'camh@udla.edu.ec', N'Camh123@', N'Camh123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (2, N'Victor', N'vip@udla.com', N'Abc123@', N'Abc123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (3, N'BenitoAntonio', N'benan@udla.com', N'Bam123@', N'Bam123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (8, N'Ozuna', N'ozu@udla.com', N'Ozu1234@', N'Ozu1234@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (9, N'LariCaboa', N'lari@udla.edu.com', N'Lari123@', N'Lari123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (10, N'EdyCito', N'edi@udla.edu.com', N'Edi123@', N'Edi123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (11, N'BerniZan', N'bern@udla.com', N'Bern123@', N'Bern123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
INSERT [dbo].[Usuarios] ([UsuarioID], [NombreUsuario], [Correo], [Contrasena], [ConfirmarContrasena], [Avatar]) VALUES (13, N'SimonaLamona', N'sim@udla.edu.ec', N'Sim123@', N'Simo123@', N'https://i.ibb.co/v1QQ7Kd/profile.png')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_UsuarioID]    Script Date: 12/23/2021 5:11:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioID] ON [dbo].[Contactos]
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuarioID]    Script Date: 12/23/2021 5:11:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioID] ON [dbo].[FechasImportantes]
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuarioID]    Script Date: 12/23/2021 5:11:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioID] ON [dbo].[Notas]
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuarioID]    Script Date: 12/23/2021 5:11:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsuarioID] ON [dbo].[Pendientes]
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contactos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Contactoes_dbo.Usuarios_UsuarioID] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contactos] CHECK CONSTRAINT [FK_dbo.Contactoes_dbo.Usuarios_UsuarioID]
GO
ALTER TABLE [dbo].[FechasImportantes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FechasImportantes_dbo.Usuarios_UsuarioID] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FechasImportantes] CHECK CONSTRAINT [FK_dbo.FechasImportantes_dbo.Usuarios_UsuarioID]
GO
ALTER TABLE [dbo].[Notas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Notas_dbo.Usuarios_UsuarioID] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notas] CHECK CONSTRAINT [FK_dbo.Notas_dbo.Usuarios_UsuarioID]
GO
ALTER TABLE [dbo].[Pendientes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pendientes_dbo.Usuarios_UsuarioID] FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pendientes] CHECK CONSTRAINT [FK_dbo.Pendientes_dbo.Usuarios_UsuarioID]
GO
USE [master]
GO
ALTER DATABASE [ProyectoFinalDB] SET  READ_WRITE 
GO
