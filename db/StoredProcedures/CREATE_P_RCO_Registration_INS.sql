SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS P_RCO_Registration_INS
GO

CREATE PROCEDURE [P_RCO_Registration_INS]
		@RCO_Name_Of_Administrator NVARCHAR(30),
		@Type_Of_Reg  NVARCHAR(20),
		@RCO_Dept_Id INT,
		@RCO_Department NVARCHAR(100),
		@RCO_Designation NVARCHAR(20),
		@RCO_District NVARCHAR(10),
		@RCO_Office_Add1 NVARCHAR(250) ,
		@RCO_Office_Add2 NVARCHAR(250) = NULL,
		@RCO_Tin_No NVARCHAR(15),
		@RCO_Tan_No NVARCHAR(15),
		@RCO_Email NVARCHAR(50),
		@RCO_Contact NVARCHAR(15),
		@RCO_Entry_Time datetime = NULL,
		@RETURN_ID BIGINT = 0 OUTPUT
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO RCO_Registration
    (
        RCO_Name_Of_Administrator ,
		Type_Of_Reg ,
		RCO_Department ,
		RCO_Designation ,
		RCO_District ,
		RCO_Office_Add1  ,
		RCO_Office_Add2 ,
		RCO_Tin_No ,
		RCO_Tan_No ,
		RCO_Email ,
		RCO_Contact,
		RCO_Entry_Time,
		RCO_Dept_Id
	)
	
	VALUES
	(
		/* RCO_CODE	*/			@RCO_Name_Of_Administrator,
		/* DEPT_ID	*/			@Type_Of_Reg,
		/* DESIG_ID	*/			@RCO_Department,
		/* DISTRICT_ID	*/		@RCO_Designation,
								@RCO_District,
		/* OFFICE_ADD_1	*/		UPPER(@RCO_Office_Add1),
		/* OFFICE_ADD_2	*/		UPPER(ISNULL(@RCO_Office_Add2,'')),
		/* TIN_NO	*/			UPPER(@RCO_Tin_No),
		/* TAN_NO	*/			UPPER(@RCO_Tan_No),
		/* EMAIL	*/			LOWER(@RCO_Email),
		/* CONTACT_N0	*/		@RCO_Contact,
		
		/* ENTRY_TIME	*/		GETDATE(),
								@RCO_Dept_Id
	
	)
	
	IF @@IDENTITY > 0
		SET @RETURN_ID = @@IDENTITY
		ELSE
		SET @RETURN_ID = 0
	
END TRY

-- CATCH THE ERROR
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