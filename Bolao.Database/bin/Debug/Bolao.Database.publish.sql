﻿/*
Deployment script for db_bolao

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "db_bolao"
:setvar DefaultFilePrefix "db_bolao"
:setvar DefaultDataPath "E:\LOCAL\"
:setvar DefaultLogPath "E:\LOCAL\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[PK_APOSTA]...';


GO
ALTER TABLE [dbo].[APOSTA]
    ADD CONSTRAINT [PK_APOSTA] PRIMARY KEY CLUSTERED ([COD_USUARIO] ASC, [COD_JOGO] ASC);


GO
PRINT N'Creating [dbo].[APOSTA].[IDX_APOSTA_001]...';


GO
CREATE NONCLUSTERED INDEX [IDX_APOSTA_001]
    ON [dbo].[APOSTA]([COD_JOGO] ASC);


GO
PRINT N'Creating [dbo].[APOSTA].[IDX_APOSTA_002]...';


GO
CREATE NONCLUSTERED INDEX [IDX_APOSTA_002]
    ON [dbo].[APOSTA]([COD_CRITERIO] ASC);


GO
PRINT N'Creating [dbo].[PK_CRITERIO]...';


GO
ALTER TABLE [dbo].[CRITERIO]
    ADD CONSTRAINT [PK_CRITERIO] PRIMARY KEY CLUSTERED ([COD_CRITERIO] ASC);


GO
PRINT N'Creating [dbo].[PK_JOGO]...';


GO
ALTER TABLE [dbo].[JOGO]
    ADD CONSTRAINT [PK_JOGO] PRIMARY KEY CLUSTERED ([COD_JOGO] ASC);


GO
PRINT N'Creating [dbo].[JOGO].[IDX_JOGO_001]...';


GO
CREATE NONCLUSTERED INDEX [IDX_JOGO_001]
    ON [dbo].[JOGO]([COD_FASE] ASC);


GO
PRINT N'Creating [dbo].[JOGO].[IDX_JOGO_002]...';


GO
CREATE NONCLUSTERED INDEX [IDX_JOGO_002]
    ON [dbo].[JOGO]([COD_BOLAO] ASC);


GO
PRINT N'Creating [dbo].[JOGO].[IDX_JOGO_003]...';


GO
CREATE NONCLUSTERED INDEX [IDX_JOGO_003]
    ON [dbo].[JOGO]([COD_GRUPO] ASC);


GO
PRINT N'Creating [dbo].[JOGO].[IDX_JOGO_004]...';


GO
CREATE NONCLUSTERED INDEX [IDX_JOGO_004]
    ON [dbo].[JOGO]([COD_TIME_A] ASC);


GO
PRINT N'Creating [dbo].[JOGO].[IDX_JOGO_005]...';


GO
CREATE NONCLUSTERED INDEX [IDX_JOGO_005]
    ON [dbo].[JOGO]([COD_TIME_B] ASC);


GO
PRINT N'Creating [dbo].[GRUPO].[IDX_GRUPO_001]...';


GO
CREATE NONCLUSTERED INDEX [IDX_GRUPO_001]
    ON [dbo].[GRUPO]([COD_FASE] ASC);


GO
PRINT N'Creating [dbo].[GRUPO].[IDX_GRUPO_002]...';


GO
CREATE NONCLUSTERED INDEX [IDX_GRUPO_002]
    ON [dbo].[GRUPO]([COD_BOLAO] ASC);


GO
PRINT N'Creating [dbo].[PARTICIPANTE].[IDX_PARTICIPANTE_001]...';


GO
CREATE NONCLUSTERED INDEX [IDX_PARTICIPANTE_001]
    ON [dbo].[PARTICIPANTE]([COD_BOLAO] ASC);


GO
PRINT N'Creating [dbo].[TIME_GRUPO].[IDX_TIMEGRUPO_001]...';


GO
CREATE NONCLUSTERED INDEX [IDX_TIMEGRUPO_001]
    ON [dbo].[TIME_GRUPO]([COD_GRUPO] ASC);


GO
PRINT N'Creating [dbo].[TIME_GRUPO].[IDX_TIMEGRUPO_002]...';


GO
CREATE NONCLUSTERED INDEX [IDX_TIMEGRUPO_002]
    ON [dbo].[TIME_GRUPO]([COD_TIME] ASC);


GO
PRINT N'Creating unnamed constraint on [dbo].[APOSTA]...';


GO
ALTER TABLE [dbo].[APOSTA]
    ADD DEFAULT 'N' FOR [IND_APURADA];


GO
PRINT N'Creating unnamed constraint on [dbo].[BOLAO]...';


GO
ALTER TABLE [dbo].[BOLAO]
    ADD DEFAULT 'N' FOR [IND_STATUS];


GO
PRINT N'Creating unnamed constraint on [dbo].[FASE]...';


GO
ALTER TABLE [dbo].[FASE]
    ADD DEFAULT 'N' FOR [IND_STATUS];


GO
PRINT N'Creating unnamed constraint on [dbo].[JOGO]...';


GO
ALTER TABLE [dbo].[JOGO]
    ADD DEFAULT 'N' FOR [IND_REALIZADO];


GO
PRINT N'Creating unnamed constraint on [dbo].[PARTICIPANTE]...';


GO
ALTER TABLE [dbo].[PARTICIPANTE]
    ADD DEFAULT 'N' FOR [IND_ADMINISTRADOR];


GO
PRINT N'Creating unnamed constraint on [dbo].[PARTICIPANTE]...';


GO
ALTER TABLE [dbo].[PARTICIPANTE]
    ADD DEFAULT 'N' FOR [IND_BOLAO_PAGO];


GO
PRINT N'Creating unnamed constraint on [dbo].[TIME_GRUPO]...';


GO
ALTER TABLE [dbo].[TIME_GRUPO]
    ADD DEFAULT 'N' FOR [IND_CLASSIFICADO];


GO
PRINT N'Creating [dbo].[FK_CRITERIO_APOSTA_01]...';


GO
ALTER TABLE [dbo].[APOSTA] WITH NOCHECK
    ADD CONSTRAINT [FK_CRITERIO_APOSTA_01] FOREIGN KEY ([COD_CRITERIO]) REFERENCES [dbo].[CRITERIO] ([COD_CRITERIO]);


GO
PRINT N'Creating [dbo].[FK_JOGO_APOSTA_01]...';


GO
ALTER TABLE [dbo].[APOSTA] WITH NOCHECK
    ADD CONSTRAINT [FK_JOGO_APOSTA_01] FOREIGN KEY ([COD_JOGO]) REFERENCES [dbo].[JOGO] ([COD_JOGO]);


GO
PRINT N'Creating [dbo].[FK_USUARIO_APOSTA_01]...';


GO
ALTER TABLE [dbo].[APOSTA] WITH NOCHECK
    ADD CONSTRAINT [FK_USUARIO_APOSTA_01] FOREIGN KEY ([COD_USUARIO]) REFERENCES [dbo].[USUARIO] ([COD_USUARIO]);


GO
PRINT N'Creating [dbo].[FK_BOLAO_JOGO_01]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_BOLAO_JOGO_01] FOREIGN KEY ([COD_BOLAO]) REFERENCES [dbo].[BOLAO] ([COD_BOLAO]);


GO
PRINT N'Creating [dbo].[FK_FASE_JOGO_01]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_FASE_JOGO_01] FOREIGN KEY ([COD_FASE]) REFERENCES [dbo].[FASE] ([COD_FASE]);


GO
PRINT N'Creating [dbo].[FK_GRUPO_JOGO_01]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_GRUPO_JOGO_01] FOREIGN KEY ([COD_GRUPO]) REFERENCES [dbo].[GRUPO] ([COD_GRUPO]);


GO
PRINT N'Creating [dbo].[FK_TIME_JOGO_01]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_TIME_JOGO_01] FOREIGN KEY ([COD_TIME_A]) REFERENCES [dbo].[TIME] ([COD_TIME]);


GO
PRINT N'Creating [dbo].[FK_TIME_JOGO_02]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_TIME_JOGO_02] FOREIGN KEY ([COD_TIME_B]) REFERENCES [dbo].[TIME] ([COD_TIME]);


GO
PRINT N'Creating [dbo].[CK_APOSTA_IND_APURADA]...';


GO
ALTER TABLE [dbo].[APOSTA] WITH NOCHECK
    ADD CONSTRAINT [CK_APOSTA_IND_APURADA] CHECK (IND_APURADA IN ('S', 'N'));


GO
PRINT N'Creating [dbo].[FK_BOLAO_IND_STATUS]...';


GO
ALTER TABLE [dbo].[BOLAO] WITH NOCHECK
    ADD CONSTRAINT [FK_BOLAO_IND_STATUS] CHECK (IND_STATUS IN ('N', 'A', 'F'));


GO
PRINT N'Creating [dbo].[CK_FASE_IND_STATUS]...';


GO
ALTER TABLE [dbo].[FASE] WITH NOCHECK
    ADD CONSTRAINT [CK_FASE_IND_STATUS] CHECK (IND_STATUS IN ('N', 'A', 'F'));


GO
PRINT N'Creating [dbo].[CK_FASE_IND_TIPO_FASE]...';


GO
ALTER TABLE [dbo].[FASE] WITH NOCHECK
    ADD CONSTRAINT [CK_FASE_IND_TIPO_FASE] CHECK (IND_TIPO_FASE IN ('G', 'E'));


GO
PRINT N'Creating [dbo].[FK_JOGO_IND_REALIZADO]...';


GO
ALTER TABLE [dbo].[JOGO] WITH NOCHECK
    ADD CONSTRAINT [FK_JOGO_IND_REALIZADO] CHECK (IND_REALIZADO IN ('S', 'N'));


GO
PRINT N'Creating [dbo].[CK_BOLAO_IND_ADMINISTRADOR]...';


GO
ALTER TABLE [dbo].[PARTICIPANTE] WITH NOCHECK
    ADD CONSTRAINT [CK_BOLAO_IND_ADMINISTRADOR] CHECK (IND_ADMINISTRADOR IN ('S', 'N'));


GO
PRINT N'Creating [dbo].[CK_BOLAO_IND_BOLAO_PAGO]...';


GO
ALTER TABLE [dbo].[PARTICIPANTE] WITH NOCHECK
    ADD CONSTRAINT [CK_BOLAO_IND_BOLAO_PAGO] CHECK (IND_BOLAO_PAGO IN ('S', 'N'));


GO
PRINT N'Creating [dbo].[CK_TIMEGRUPO_IND_CLASSIFICADO]...';


GO
ALTER TABLE [dbo].[TIME_GRUPO] WITH NOCHECK
    ADD CONSTRAINT [CK_TIMEGRUPO_IND_CLASSIFICADO] CHECK (IND_CLASSIFICADO IN ('N', 'S'));


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[APOSTA] WITH CHECK CHECK CONSTRAINT [FK_CRITERIO_APOSTA_01];

ALTER TABLE [dbo].[APOSTA] WITH CHECK CHECK CONSTRAINT [FK_JOGO_APOSTA_01];

ALTER TABLE [dbo].[APOSTA] WITH CHECK CHECK CONSTRAINT [FK_USUARIO_APOSTA_01];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_BOLAO_JOGO_01];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_FASE_JOGO_01];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_GRUPO_JOGO_01];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_TIME_JOGO_01];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_TIME_JOGO_02];

ALTER TABLE [dbo].[APOSTA] WITH CHECK CHECK CONSTRAINT [CK_APOSTA_IND_APURADA];

ALTER TABLE [dbo].[BOLAO] WITH CHECK CHECK CONSTRAINT [FK_BOLAO_IND_STATUS];

ALTER TABLE [dbo].[FASE] WITH CHECK CHECK CONSTRAINT [CK_FASE_IND_STATUS];

ALTER TABLE [dbo].[FASE] WITH CHECK CHECK CONSTRAINT [CK_FASE_IND_TIPO_FASE];

ALTER TABLE [dbo].[JOGO] WITH CHECK CHECK CONSTRAINT [FK_JOGO_IND_REALIZADO];

ALTER TABLE [dbo].[PARTICIPANTE] WITH CHECK CHECK CONSTRAINT [CK_BOLAO_IND_ADMINISTRADOR];

ALTER TABLE [dbo].[PARTICIPANTE] WITH CHECK CHECK CONSTRAINT [CK_BOLAO_IND_BOLAO_PAGO];

ALTER TABLE [dbo].[TIME_GRUPO] WITH CHECK CHECK CONSTRAINT [CK_TIMEGRUPO_IND_CLASSIFICADO];


GO
PRINT N'Update complete.';


GO
