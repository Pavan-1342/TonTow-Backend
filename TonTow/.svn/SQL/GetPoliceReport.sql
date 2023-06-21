
/****** Object:  StoredProcedure [dbo].[GetPoliceReport]    Script Date: 5/4/2023 5:12:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[GetPoliceReport]  --16
@TonTowRptId bigint
as
 
SELECT     PR.TonTowRptId, PR.JobNum, CONVERT(DATETIME, PR.DateOfCrash) AS DateOfCrash, CONVERT(DATETIME, PR.TimeOfCrash) AS TimeOfCrash, PR.CityTown, PR.VehicleNumber, PR.InjuredNumber, PR.SpeedLimit, PR.Latitude, PR.Longitude, PR.PoliceType, PR.Other, PR.AtIntersection, 
                  PR.AIRoute1, PR.AIDirection1, PR.AIRoadwayStName1, PR.AIRoute2, PR.AIDirection2, PR.AIRoadwayStName2, PR.AIRoute3, PR.NAIRoadwayStName, PR.AIRoadwayStName3, PR.NAIRoute1, PR.NAIDirection1, PR.NAIFeet1, PR.NAIMile, PR.NAIExitNo, PR.NAIFeet2, PR.NAIRoute, 
                  PR.NAIRoadwaySt, PR.NAIFeet3, PR.NAILandmark, PR.CrashReportId, PR.CrashNarrative, PR.Status, PR.CreatedBy, PR.CreatedDate, PR.ModifiedBy, PR.ModifiedDate, PRVD.TonTowRptId AS Expr2, PRVD.VehicleNo, PRVD.CrashType, PRVD.Type, PRVD.Action, PRVD.Location, 
                  PRVD.Condition, PRVD.Occupants, PRVD.License, PRVD.Street, PRVD.DOBAge, PRVD.Sex, PRVD.LicClass, PRVD.LicRestrictions, PRVD.CDLEndorsement, PRVD.OperatorLastName, PRVD.OperatorFirstName, PRVD.OperatorMiddleName, PRVD.OperatorAddress, PRVD.OperatorCity, 
                  PRVD.OperatorState, PRVD.OperatorZip, PRVD.InsuranceCompany, PRVD.VehicleTravelDirection, PRVD.Viol1, PRVD.Viol2, PRVD.Viol3, PRVD.Viol4, PRVD.Reg, PRVD.RegType, PRVD.RegState, PRVD.VehicleYear, PRVD.VehicleMake, PRVD.VehicleConfig, PRVD.OwnerLastName, 
                  PRVD.OwnerFirstName, PRVD.OwnerMiddleName, PRVD.OwnerAddress, PRVD.OwnerCity, PRVD.OwnerState, PR.AIDirection3, PR.NAIDirection, PR.NAIAddress, PR.NAIDirection2, PR.NAIDirection3, PRVD.RespondingToEmergency, PRVD.CitationIssued, PRVD.OwnerZip, 
                  PRVD.VehicleActionPriortoCrash, PRVD.EventSequence, PRVD.MostHarmfulEvent, PRVD.DriverContributingCode, PRVD.DriverDistractedBy, PRVD.DamagedAreaCode, PRVD.TestStatus, PRVD.TypeofTest, PRVD.BacTestResult, PRVD.SuspectedAlcohol, PRVD.SuspectedDrug, 
                  PRVD.TowedFromScene
FROM        dbo.PoliceReport AS PR LEFT OUTER JOIN
                  dbo.PoliceReportVehicleDtls AS PRVD ON PR.TonTowRptId = PRVD.TonTowRptId
WHERE PR.TonTowRptId=@TonTowRptId
GO

