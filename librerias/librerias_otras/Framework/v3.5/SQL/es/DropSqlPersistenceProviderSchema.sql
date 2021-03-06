SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

declare @localized_string_DropRole_Failed nvarchar(256)
set @localized_string_DropRole_Failed = N'Error al anular la función ''''persistenceUsers'''''

DECLARE @ret int, @Error int
IF EXISTS( SELECT 1 FROM [dbo].[sysusers] WHERE name=N'persistenceUsers' and issqlrole=1 )
 BEGIN

	EXEC @ret = sp_droprole N'persistenceUsers'

	SELECT @Error = @@ERROR

	IF @ret <> 0 or @Error <> 0
		RAISERROR( @localized_string_DropRole_Failed, 16, -1 )
 END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstanceData]') AND type in (N'U'))
	DROP TABLE [dbo].[InstanceData]
GO
