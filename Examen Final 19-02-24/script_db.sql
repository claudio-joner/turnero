CREATE  DATABASE db_turnos
GO
USE [db_turnos]
GO
/****** Object:  Table [dbo].[T_TURNOS]    Script Date: 02/18/2024 21:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_TURNOS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[paciente] [varchar](100) NOT NULL,
 CONSTRAINT [PK_T_TURNOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_MEDICOS]    Script Date: 02/18/2024 21:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_MEDICOS](
	[matricula] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[especialidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_T_MEDICOS] PRIMARY KEY CLUSTERED 
(
	[matricula] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[T_MEDICOS] ([matricula], [nombre], [apellido], [especialidad]) VALUES (7778, N'Marcos', N'Francos', N'Medicina General')
INSERT [dbo].[T_MEDICOS] ([matricula], [nombre], [apellido], [especialidad]) VALUES (8895, N'María', N'Castellanos', N'Ginecología')
INSERT [dbo].[T_MEDICOS] ([matricula], [nombre], [apellido], [especialidad]) VALUES (9997, N'José', N'Sottorello', N'Traumatología')
INSERT [dbo].[T_MEDICOS] ([matricula], [nombre], [apellido], [especialidad]) VALUES (12456, N'Lucía', N'Cuevas', N'Cardiología')
/****** Object:  Table [dbo].[T_DETALLES_TURNO]    Script Date: 02/18/2024 21:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_DETALLES_TURNO](
	[id_turno] [int] NOT NULL,
	[matricula] [int] NOT NULL,
	[motivo_consulta] [varchar](200) NULL,
	[fecha] [varchar](10) NOT NULL,
	[hora] [varchar](10) NOT NULL,
 CONSTRAINT [PK_T_DETALLES_TURNO] PRIMARY KEY CLUSTERED 
(
	[id_turno] ASC,
	[matricula] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLES]    Script Date: 02/18/2024 21:02:22 ******/

/*SP */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLES] 
	@id_turno int,
	@matricula int, 
	@motivo varchar(200),
	@fecha varchar(10),
	@hora varchar(10)
AS
BEGIN
	INSERT INTO T_DETALLES_TURNO(id_turno,matricula, motivo_consulta, fecha, hora)
    VALUES (@id_turno,@matricula, @motivo, @fecha, @hora);
  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONTAR_TURNOS]    Script Date: 02/18/2024 21:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONTAR_TURNOS]
    @fecha VARCHAR(10),
    @hora VARCHAR(8),
    @matricula int, 
    @ctd_turnos INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @ctd_turnos = COUNT(*)
    FROM T_DETALLES_TURNO t
    WHERE t.fecha = @fecha AND t.hora = @hora AND t.matricula = @matricula;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_MEDICOS]    Script Date: 02/18/2024 21:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CONSULTAR_MEDICOS]
AS
BEGIN
	
	SELECT * from T_MEDICOS ORDER BY 4;
END
GO
/****** Object:  StoredProcedure [dbo].[INSERTAR_MAESTRO]    Script Date: 02/18/2024 21:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERTAR_MAESTRO] 
	@paciente varchar(100), 
	@id int output   
AS
BEGIN
	INSERT INTO T_TURNOS (paciente) VALUES(@paciente);
	SET @id = SCOPE_IDENTITY(); /*DEVUELVE EL ID COMO PARAMETRO DE SALIDA*/
END
GO


/*	CONSULTAS  CREADA POR MI */

SELECT * FROM T_MEDICOS 
SELECT * FROM T_TURNOS
SELECT * FROM T_DETALLES_TURNO

--SP PARA OBTENER EL ULTIMO ID
SELECT * FROM T_TURNOS tur ORDER BY 1 

CREATE PROCEDURE OBTENER_ULTIMO_ID
	@ultimoId int output
AS
BEGIN
	SELECT @ultimoId = tur.id FROM T_TURNOS tur ORDER BY 1 
END

