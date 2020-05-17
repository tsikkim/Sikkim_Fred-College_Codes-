SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE [dbo].[P_RCO_REG_DETAILS_READ_BY_STATUS]  
 @STATUS BIT = NULL
AS  
BEGIN TRY
	SET NOCOUNT ON;

	IF @STATUS IS NULL
	BEGIN

		SELECT [RCO_Name_Of_Administrator]
			  ,[Type_Of_Reg]
			  ,[RCO_Department]
			  ,[RCO_Designation]
			  ,[RCO_District]
			  ,[RCO_Office_Add1]
			  ,[RCO_Office_Add2]
			  ,[RCO_Tin_No]
			  ,[RCO_Tan_No]
			  ,[RCO_Email]
			  ,[RCO_Contact]
			  ,[RCO_Entry_Time]
			  ,[REG_ID]
			  ,[CUR_STATUS]
			  ,CASE 
					WHEN CUR_STATUS = 0 THEN 'PENDING'
					ELSE 'APPROVED'	
			END						  AS [STATUS_NAME]
			  ,[PASSED_BY]
			  ,[PASSING_TIME]
		FROM [RCO_Registration]
	END
	ELSE
	BEGIN

		SELECT [RCO_Name_Of_Administrator]
			  ,[Type_Of_Reg]
			  ,[RCO_Department]
			  ,[RCO_Designation]
			  ,[RCO_District]
			  ,[RCO_Office_Add1]
			  ,[RCO_Office_Add2]
			  ,[RCO_Tin_No]
			  ,[RCO_Tan_No]
			  ,[RCO_Email]
			  ,[RCO_Contact]
			  ,[RCO_Entry_Time]
			  ,[REG_ID]
			  ,[CUR_STATUS]
			  ,CASE 
					WHEN [CUR_STATUS] = 0 THEN 'PENDING'
					ELSE 'APPROVED'	
				END							AS [STATUS_NAME]
			  ,[PASSED_BY]
			  ,[PASSING_TIME]
		FROM [RCO_Registration]
		WHERE [CUR_STATUS] = @STATUS
	END
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