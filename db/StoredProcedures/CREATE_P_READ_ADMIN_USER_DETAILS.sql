SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS P_READ_ADMIN_USER_DETAILS
GO

CREATE PROCEDURE [P_READ_ADMIN_USER_DETAILS]
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [USER_ID]
      ,[USER_NAME]
      ,[PASSWORD]
      ,[IS_LOGGED_IN]
      ,[LAST_LOGIN_DATE]
      ,[ACTIVE]
      ,[CREATE_DATE]
      ,[LAST_SCH_NO]
      ,[STEP]
      ,[TYPE]
      ,[IS_ADMIN]
      ,[TBL_USER].[DEPT_ID]
      ,[IS_DDO]
      ,[IS_SUPER]
      ,[TBL_USER].[Email]
      ,[IS_RCO]
      ,[ddocode]
	FROM [TBL_USER] 
	WHERE [TBL_USER].[ACTIVE] = 1 
		AND ([TBL_USER].[IS_ADMIN] = 1 OR [TBL_USER].[IS_SUPER] = 1)
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