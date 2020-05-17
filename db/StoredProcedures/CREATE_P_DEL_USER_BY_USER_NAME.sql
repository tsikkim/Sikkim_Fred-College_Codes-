SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS P_DEL_USER_BY_USER_NAME
GO

CREATE PROCEDURE [P_DEL_USER_BY_USER_NAME]
	@USER_NAME NVARCHAR(50)
AS

BEGIN TRY
	
	DELETE FROM TBL_USER WHERE [USER_NAME] = @USER_NAME
	
END TRY 

BEGIN CATCH
	DECLARE @ERR  VARCHAR(1000)
	SELECT @ERR=ERROR_MESSAGE()
	IF	ERROR_SEVERITY () > 12
		BEGIN
			RAISERROR (@ERR, 16, 20)
		END
		--
		ELSE
		--
		BEGIN
			RAISERROR (@ERR, 12, 2)
		END
END CATCH

GO