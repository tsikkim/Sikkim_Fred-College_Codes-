SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE P_RCO_REG_READ_BY_ID  
 @RCO_REG_ID BIGINT  
AS  
BEGIN TRY

 SET NOCOUNT ON;

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
      ,[PASSED_BY]
      ,[PASSING_TIME]
  FROM [RCO_Registration]
  WHERE REG_ID = @RCO_REG_ID  

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