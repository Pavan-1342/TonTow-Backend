

/****** Object:  StoredProcedure [dbo].[GetOperationInformationSheet]    Script Date: 5/4/2023 5:12:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[GetOperationInformationSheet]
@TonTowRptId BIGINT 

as

SELECT     PR.TonTowRptId,  PRG.AccidentDate, PRG.AccidentTime, PRG.ReportingOfficer, PRG.Location, PRG.City, PRG.State, PRG.Zip,   PROO.OperatorLastName, PROO.OperatorFirstName, PROO.OperatorMiddleName, 
                  PROO.OperatorSuffixName, PROO.OperatorVeh, PROO.OperatorInjured, PROO.OperatorFatality, PROO.OperatorNumber, PROO.OperatorStreet, PROO.OperatorStreetSuffix, PROO.OperatorStreetApt, PROO.OperatorCity, PROO.OperatorState, PROO.OperatorZip, PROO.OperatorDOB, 
                  PROO.OperatorHomePhone, PROO.OperatorWorkPhone, PROO.OperatorLic, PROO.OperatorStateNumber, PROO.OperatorInsuranceComp, PROO.OperatorPolicyNumber, PROO.OwnerLastName, PROO.OwnerFirstName, PROO.OwnerMiddleName, PROO.OwnerSuffixName, 
                  PROO.OwnerHomePhone, PROO.OwnerWorkPhone, PROO.OwnerNumber, PROO.OwnerStreet, PROO.OwnerStreetSuffix, PROO.OwnerStreetApt, PROO.OwnerCity, PROO.OwnerState, PROO.OwnerZip, PROO.OwnerInsuranceComp, PROO.OwnerPolicyNumber, PROO.VehYear, PROO.VehMake, 
                  PROO.VehModel, PROO.VehVin, PROO.VehReg, PROO.VehStateNumber, PROO.VehTowedBy, PROO.VehTowedTo
FROM        dbo.PoliceReport AS PR INNER JOIN
                  dbo.PoliceReportGeneral AS PRG ON PR.TonTowRptId = PRG.TonTowRptId LEFT OUTER JOIN
                  dbo.PoliceReportOperatorOwnerVehicleDtls AS PROO ON PR.TonTowRptId = PROO.TonTowRptId
WHERE     (PR.TonTowRptId = @TonTowRptId)

GO

