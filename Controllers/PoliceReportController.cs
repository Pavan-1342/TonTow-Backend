using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Drawing2D;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Xml;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/PoliceReport")]
    public class PoliceReportController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;
        private readonly IConfiguration _configuration;

        public PoliceReportController(TonTowDbContext tonTowDbContext, IConfiguration configuration)
        {
            _tonTowDbContext = tonTowDbContext;
            _configuration = configuration;
        }

        [HttpGet("GetAllPoliceReport"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllPoliceReport()
        {
            var PoliceReport = from policereport in _tonTowDbContext.PoliceReport
                               join TU in _tonTowDbContext.TonTowUser
                                on policereport.TonTowRptId equals TU.TonTowRptId
                               where policereport.Status == true
                               select new
                               {
                                   policereport,
                                   Email = TU.EmailId,
                                   Phone = TU.Phone
                               };
            return Ok(PoliceReport);
        }


        [HttpGet("GetUserPoliceReport")]
        public async Task<IActionResult> GetUserPoliceReport(int TonTowRptId)
        {
            // var PoliceReport = from policereport in _tonTowDbContext.PoliceReport
            //                    where policereport.TonTowRptId == TonTowRptId && policereport.Status == true
            //                    select new
            //                    {
            //                        policereport
            //                   };
            var PoliceReport = _tonTowDbContext.PoliceReport
           .Include(pr => pr.PoliceReportVehicleDtl)
           .Include(pr => pr.PoliceReportOperatorDtls)
           .Include(pr => pr.PoliceReportWitness)
           .Include(pr => pr.PoliceReportPropertyDamage)
           .Include(pr => pr.PoliceReportTruckAndBusDtl)
           .Include(pr => pr.PoliceReportGeneral)
           .Include(pr => pr.PoliceReportOperatorOwnerVehicleDtls)
           .FirstOrDefault(pr => pr.TonTowRptId == TonTowRptId && pr.Status == true);
            return Ok(PoliceReport);
        }

        [HttpGet("CheckDuplicatePoliceReport"), Authorize(Roles = "A")]
        public async Task<IActionResult> CheckDuplicatePoliceReport(string JobNum)
        {
            var PoliceReport = from policereport in _tonTowDbContext.PoliceReport
                               where policereport.JobNum == JobNum
                               select new
                               {
                                   policereport
                               };
            return Ok(PoliceReport);
        }
        [HttpGet("CheckDuplicateEmail"), Authorize(Roles = "A")]
        public async Task<IActionResult> CheckDuplicateEmail(string Email)
        {
            var PoliceReport = from tontowuser in _tonTowDbContext.TonTowUser
                               where tontowuser.EmailId == Email
                               select new
                               {
                                   tontowuser
                               };
            return Ok(PoliceReport);
        }
        [HttpGet("CheckDuplicatePhone"), Authorize(Roles = "A")]
        public async Task<IActionResult> CheckDuplicatePhone(string Phone)
        {
            var PoliceReport = from tontowuser in _tonTowDbContext.TonTowUser
                               where  tontowuser.Phone == Phone
                               select new
                               {
                                   tontowuser
                               };
            return Ok(PoliceReport);
        }
        [HttpGet("CheckEditDuplicateEmail"), Authorize(Roles = "A")]
        public async Task<IActionResult> CheckEditDuplicateEmail(string Email, int TonTowRptId)
        {
            var PoliceReport = from tontowuser in _tonTowDbContext.TonTowUser
                               where (tontowuser.EmailId == Email) && tontowuser.TonTowRptId!= TonTowRptId
                               select new
                               {
                                   tontowuser
                               };
            return Ok(PoliceReport);
        }
        [HttpGet("CheckEditDuplicatePhone"), Authorize(Roles = "A")]
        public async Task<IActionResult> CheckEditDuplicatePhone(string Phone, int TonTowRptId)
        {
            var PoliceReport = from tontowuser in _tonTowDbContext.TonTowUser
                               where (tontowuser.Phone == Phone) && tontowuser.TonTowRptId != TonTowRptId
                               select new
                               {
                                   tontowuser
                               };
            return Ok(PoliceReport);
        }

        #region Add Police Report
        /// <summary>
        /// API to add the police report and their child tables
        /// </summary>
        /// <param name="AddPoliceReportRequest"></param>
        /// <returns></returns>
        [HttpPost("AddPoliceReport"), Authorize(Roles = "A")]
            public async Task<IActionResult> AddPoliceReport([FromBody] AddPoliceReportRequest AddPoliceReportRequest)
            {
                string strVehicle1Dob = "";
                using (var transaction = await _tonTowDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        #region PoliceReport
                            PoliceReport PoliceReportData = new PoliceReport();
                            PoliceReportData.JobNum = AddPoliceReportRequest.JobNum;
                            PoliceReportData.DateOfCrash = AddPoliceReportRequest.DateOfCrash;
                            PoliceReportData.TimeOfCrash = AddPoliceReportRequest.TimeOfCrash;
                            PoliceReportData.CityTown = AddPoliceReportRequest.CityTown;
                            PoliceReportData.VehicleNumber = AddPoliceReportRequest.VehicleNumber;
                            PoliceReportData.InjuredNumber = AddPoliceReportRequest.InjuredNumber;
                            PoliceReportData.SpeedLimit = AddPoliceReportRequest.SpeedLimit;
                            PoliceReportData.Latitude = AddPoliceReportRequest.Latitude;
                            PoliceReportData.Longitude = AddPoliceReportRequest.Longitude;
                            PoliceReportData.StatePolice = AddPoliceReportRequest.StatePolice;
                            PoliceReportData.LocalPolice = AddPoliceReportRequest.LocalPolice;
                            PoliceReportData.MBTAPolice = AddPoliceReportRequest.MBTAPolice;
                            PoliceReportData.CampusPolice = AddPoliceReportRequest.CampusPolice;
                            PoliceReportData.Other = AddPoliceReportRequest.Other;
                            PoliceReportData.AtIntersection = AddPoliceReportRequest.AtIntersection;
                            PoliceReportData.AIRoute1 = AddPoliceReportRequest.AIRoute1;
                            PoliceReportData.AIDirection1 = AddPoliceReportRequest.AIDirection1;
                            PoliceReportData.AIRoadwayStName1 = AddPoliceReportRequest.AIRoadwayStName1;
                            PoliceReportData.AIRoute2 = AddPoliceReportRequest.AIRoute2;
                            PoliceReportData.AIDirection2 = AddPoliceReportRequest.AIDirection2;
                            PoliceReportData.AIRoadwayStName2 = AddPoliceReportRequest.AIRoadwayStName2;
                            PoliceReportData.AIRoute3 = AddPoliceReportRequest.AIRoute3;
                            PoliceReportData.AIDirection3 = AddPoliceReportRequest.AIDirection3;
                            PoliceReportData.AIRoadwayStName3 = AddPoliceReportRequest.AIRoadwayStName3;
                            PoliceReportData.NAIRoute = AddPoliceReportRequest.NAIRoute;
                            PoliceReportData.NAIDirection = AddPoliceReportRequest.NAIDirection;
                            PoliceReportData.NAIAddress = AddPoliceReportRequest.NAIAddress;
                            PoliceReportData.NAIRoadwayStName = AddPoliceReportRequest.NAIRoadwayStName;
                            PoliceReportData.NAIFeet1 = AddPoliceReportRequest.NAIFeet1;
                            PoliceReportData.NAIDirection1 = AddPoliceReportRequest.NAIDirection1;
                            PoliceReportData.NAIMile = AddPoliceReportRequest.NAIMile;
                            PoliceReportData.NAIExitNo = AddPoliceReportRequest.NAIExitNo;
                            PoliceReportData.NAIFeet2 = AddPoliceReportRequest.NAIFeet2;
                            PoliceReportData.NAIDirection2 = AddPoliceReportRequest.NAIDirection2;
                            PoliceReportData.NAIRoute1 = AddPoliceReportRequest.NAIRoute1;
                            PoliceReportData.NAIRoadwaySt = AddPoliceReportRequest.NAIRoadwaySt;
                            PoliceReportData.NAIFeet3 = AddPoliceReportRequest.NAIFeet3;
                            PoliceReportData.NAIDirection3 = AddPoliceReportRequest.NAIDirection3;
                            PoliceReportData.NAILandmark = AddPoliceReportRequest.NAILandmark;
                            PoliceReportData.CrashReportId = AddPoliceReportRequest.CrashReportId;
                            PoliceReportData.CrashNarrative = AddPoliceReportRequest.CrashNarrative;
                            PoliceReportData.Status = true;
                            PoliceReportData.CreatedBy = AddPoliceReportRequest.CreatedBy;
                            PoliceReportData.CreatedDate = DateTime.Now;
                            PoliceReportData.ModifiedBy = "";
                            await _tonTowDbContext.PoliceReport.AddAsync(PoliceReportData);
                            await _tonTowDbContext.SaveChangesAsync();
                        #endregion

                        #region Police Report Vehcile Dtls
                            foreach (AddPoliceReportVehicleDtlsRequest PoliceReportVehicleDtlsRequest in AddPoliceReportRequest.AddPoliceReportVehicleDtlsRequest)
                            {
                                PoliceReportVehicleDtls PoliceReportVehicleDtlsData = new PoliceReportVehicleDtls();
                                if (strVehicle1Dob == "")
                                {
                                    strVehicle1Dob = PoliceReportVehicleDtlsRequest.DOBAge.ToString().Replace("/","");
                                }
                                PoliceReportVehicleDtlsData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportVehicleDtlsData.VehicleNo = PoliceReportVehicleDtlsRequest.VehicleNo;
                                PoliceReportVehicleDtlsData.CrashType = PoliceReportVehicleDtlsRequest.CrashType;
                                PoliceReportVehicleDtlsData.Type = PoliceReportVehicleDtlsRequest.Type;
                                PoliceReportVehicleDtlsData.Action = PoliceReportVehicleDtlsRequest.Action;
                                PoliceReportVehicleDtlsData.Location = PoliceReportVehicleDtlsRequest.Location;
                                PoliceReportVehicleDtlsData.Condition = PoliceReportVehicleDtlsRequest.Condition;
                                PoliceReportVehicleDtlsData.Occupants = PoliceReportVehicleDtlsRequest.Occupants;
                                PoliceReportVehicleDtlsData.License = PoliceReportVehicleDtlsRequest.License;
                                PoliceReportVehicleDtlsData.Street = PoliceReportVehicleDtlsRequest.Street;
                                PoliceReportVehicleDtlsData.DOBAge = PoliceReportVehicleDtlsRequest.DOBAge;
                                PoliceReportVehicleDtlsData.Sex = PoliceReportVehicleDtlsRequest.Sex;
                                PoliceReportVehicleDtlsData.LicClass = PoliceReportVehicleDtlsRequest.LicClass;
                                PoliceReportVehicleDtlsData.LicRestrictions = PoliceReportVehicleDtlsRequest.LicRestrictions;
                                PoliceReportVehicleDtlsData.CDLEndorsement = PoliceReportVehicleDtlsRequest.CDLEndorsement;
                                PoliceReportVehicleDtlsData.OperatorLastName = PoliceReportVehicleDtlsRequest.OperatorLastName;
                                PoliceReportVehicleDtlsData.OperatorFirstName = PoliceReportVehicleDtlsRequest.OperatorFirstName;
                                PoliceReportVehicleDtlsData.OperatorMiddleName = PoliceReportVehicleDtlsRequest.OperatorMiddleName;
                                PoliceReportVehicleDtlsData.OperatorAddress = PoliceReportVehicleDtlsRequest.OperatorAddress;
                                PoliceReportVehicleDtlsData.OperatorCity = PoliceReportVehicleDtlsRequest.OperatorCity;
                                PoliceReportVehicleDtlsData.OperatorState = PoliceReportVehicleDtlsRequest.OperatorState;
                                PoliceReportVehicleDtlsData.OperatorZip = PoliceReportVehicleDtlsRequest.OperatorZip;
                                PoliceReportVehicleDtlsData.InsuranceCompany = PoliceReportVehicleDtlsRequest.InsuranceCompany;
                                PoliceReportVehicleDtlsData.VehicleTravelDirection = PoliceReportVehicleDtlsRequest.VehicleTravelDirection;
                                PoliceReportVehicleDtlsData.RespondingToEmergency = PoliceReportVehicleDtlsRequest.RespondingToEmergency;
                                PoliceReportVehicleDtlsData.CitationIssued = PoliceReportVehicleDtlsRequest.CitationIssued;
                                PoliceReportVehicleDtlsData.Viol1 = PoliceReportVehicleDtlsRequest.Viol1;
                                PoliceReportVehicleDtlsData.Viol2 = PoliceReportVehicleDtlsRequest.Viol2;
                                PoliceReportVehicleDtlsData.Viol3 = PoliceReportVehicleDtlsRequest.Viol3;
                                PoliceReportVehicleDtlsData.Viol4 = PoliceReportVehicleDtlsRequest.Viol4;
                                PoliceReportVehicleDtlsData.Reg = PoliceReportVehicleDtlsRequest.Reg;
                                PoliceReportVehicleDtlsData.RegType = PoliceReportVehicleDtlsRequest.RegType;
                                PoliceReportVehicleDtlsData.RegState = PoliceReportVehicleDtlsRequest.RegState;
                                PoliceReportVehicleDtlsData.VehicleYear = PoliceReportVehicleDtlsRequest.VehicleYear;
                                PoliceReportVehicleDtlsData.VehicleMake = PoliceReportVehicleDtlsRequest.VehicleMake;
                                PoliceReportVehicleDtlsData.VehicleConfig = PoliceReportVehicleDtlsRequest.VehicleConfig;
                                PoliceReportVehicleDtlsData.OwnerLastName = PoliceReportVehicleDtlsRequest.OwnerLastName;
                                PoliceReportVehicleDtlsData.OwnerFirstName = PoliceReportVehicleDtlsRequest.OwnerFirstName;
                                PoliceReportVehicleDtlsData.OwnerMiddleName = PoliceReportVehicleDtlsRequest.OwnerMiddleName;
                                PoliceReportVehicleDtlsData.OwnerAddress = PoliceReportVehicleDtlsRequest.OwnerAddress;
                                PoliceReportVehicleDtlsData.OwnerCity = PoliceReportVehicleDtlsRequest.OwnerCity;
                                PoliceReportVehicleDtlsData.OwnerState = PoliceReportVehicleDtlsRequest.OwnerState;
                                PoliceReportVehicleDtlsData.OwnerZip = PoliceReportVehicleDtlsRequest.OwnerZip;
                                PoliceReportVehicleDtlsData.VehicleActionPriortoCrash = PoliceReportVehicleDtlsRequest.VehicleActionPriortoCrash;
                                PoliceReportVehicleDtlsData.EventSequence1 = PoliceReportVehicleDtlsRequest.EventSequence1;
                                PoliceReportVehicleDtlsData.EventSequence2 = PoliceReportVehicleDtlsRequest.EventSequence2;
                                PoliceReportVehicleDtlsData.EventSequence3 = PoliceReportVehicleDtlsRequest.EventSequence3;
                                PoliceReportVehicleDtlsData.EventSequence4 = PoliceReportVehicleDtlsRequest.EventSequence4;
                                PoliceReportVehicleDtlsData.MostHarmfulEvent = PoliceReportVehicleDtlsRequest.MostHarmfulEvent;
                                PoliceReportVehicleDtlsData.DriverContributingCode1 = PoliceReportVehicleDtlsRequest.DriverContributingCode1;
                                PoliceReportVehicleDtlsData.DriverContributingCode2 = PoliceReportVehicleDtlsRequest.DriverContributingCode2;
                                PoliceReportVehicleDtlsData.DriverDistractedBy = PoliceReportVehicleDtlsRequest.DriverDistractedBy;
                                PoliceReportVehicleDtlsData.DamagedAreaCode1 = PoliceReportVehicleDtlsRequest.DamagedAreaCode1;
                                PoliceReportVehicleDtlsData.DamagedAreaCode2 = PoliceReportVehicleDtlsRequest.DamagedAreaCode2;
                                PoliceReportVehicleDtlsData.DamagedAreaCode3 = PoliceReportVehicleDtlsRequest.DamagedAreaCode3;
                                PoliceReportVehicleDtlsData.TestStatus = PoliceReportVehicleDtlsRequest.TestStatus;
                                PoliceReportVehicleDtlsData.TypeofTest = PoliceReportVehicleDtlsRequest.TypeofTest;
                                PoliceReportVehicleDtlsData.BacTestResult = PoliceReportVehicleDtlsRequest.BacTestResult;
                                PoliceReportVehicleDtlsData.SuspectedAlcohol = PoliceReportVehicleDtlsRequest.SuspectedAlcohol;
                                PoliceReportVehicleDtlsData.SuspectedDrug = PoliceReportVehicleDtlsRequest.SuspectedDrug;
                                PoliceReportVehicleDtlsData.TowedFromScene = PoliceReportVehicleDtlsRequest.TowedFromScene;
                                await _tonTowDbContext.PoliceReportVehicleDtls.AddAsync(PoliceReportVehicleDtlsData);
                            }
                        #endregion

                        #region Police Report Operator Dtls
                            foreach (AddPoliceReportOperatorDtlsRequest AddPoliceReportOperatorDtlsRequest in AddPoliceReportRequest.AddPoliceReportOperatorDtlsRequest)
                            {

                                PoliceReportOperatorDtls PoliceReportOperatorOwnerVehicleDtlsData = new PoliceReportOperatorDtls();
                                PoliceReportOperatorOwnerVehicleDtlsData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehicleNo = AddPoliceReportOperatorDtlsRequest.VehicleNo;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorLastName = AddPoliceReportOperatorDtlsRequest.OperatorLastName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorFirstName = AddPoliceReportOperatorDtlsRequest.OperatorFirstName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorMiddleName = AddPoliceReportOperatorDtlsRequest.OperatorMiddleName;
                                PoliceReportOperatorOwnerVehicleDtlsData.Address = AddPoliceReportOperatorDtlsRequest.Address;
                                PoliceReportOperatorOwnerVehicleDtlsData.City = AddPoliceReportOperatorDtlsRequest.City;
                                PoliceReportOperatorOwnerVehicleDtlsData.State = AddPoliceReportOperatorDtlsRequest.State;
                                PoliceReportOperatorOwnerVehicleDtlsData.Zip = AddPoliceReportOperatorDtlsRequest.Zip;
                                PoliceReportOperatorOwnerVehicleDtlsData.DOB = AddPoliceReportOperatorDtlsRequest.DOB;
                                PoliceReportOperatorOwnerVehicleDtlsData.Sex = AddPoliceReportOperatorDtlsRequest.Sex;
                                PoliceReportOperatorOwnerVehicleDtlsData.SeatPosition = AddPoliceReportOperatorDtlsRequest.SeatPosition;
                                PoliceReportOperatorOwnerVehicleDtlsData.SafetySystem = AddPoliceReportOperatorDtlsRequest.SafetySystem;
                                PoliceReportOperatorOwnerVehicleDtlsData.AirbagStatus = AddPoliceReportOperatorDtlsRequest.AirbagStatus;
                                PoliceReportOperatorOwnerVehicleDtlsData.EjectCode = AddPoliceReportOperatorDtlsRequest.EjectCode;
                                PoliceReportOperatorOwnerVehicleDtlsData.TrapCode = AddPoliceReportOperatorDtlsRequest.TrapCode;
                                PoliceReportOperatorOwnerVehicleDtlsData.InjuryStatus = AddPoliceReportOperatorDtlsRequest.InjuryStatus;
                                PoliceReportOperatorOwnerVehicleDtlsData.TranspCode = AddPoliceReportOperatorDtlsRequest.TranspCode;
                                PoliceReportOperatorOwnerVehicleDtlsData.MedicalFacility = AddPoliceReportOperatorDtlsRequest.MedicalFacility;
                                await _tonTowDbContext.PoliceReportOperatorDtls.AddAsync(PoliceReportOperatorOwnerVehicleDtlsData);
                            }
                        #endregion

                        #region Police Report Witness
                            foreach (AddPoliceReportWitnessRequest AddPoliceReportWitnessRequest in AddPoliceReportRequest.AddPoliceReportWitnessRequest)
                            {

                                PoliceReportWitness PoliceReportWitnessData = new PoliceReportWitness();
                                PoliceReportWitnessData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportWitnessData.LastName = AddPoliceReportWitnessRequest.LastName;
                                PoliceReportWitnessData.FirstName = AddPoliceReportWitnessRequest.FirstName;
                                PoliceReportWitnessData.MiddleName = AddPoliceReportWitnessRequest.MiddleName;
                                PoliceReportWitnessData.Address = AddPoliceReportWitnessRequest.Address;
                                PoliceReportWitnessData.City = AddPoliceReportWitnessRequest.City;
                                PoliceReportWitnessData.State = AddPoliceReportWitnessRequest.State;
                                PoliceReportWitnessData.Zip = AddPoliceReportWitnessRequest.Zip;
                                PoliceReportWitnessData.Phone = AddPoliceReportWitnessRequest.Phone;
                                PoliceReportWitnessData.Statement = AddPoliceReportWitnessRequest.Statement;
                                await _tonTowDbContext.PoliceReportWitness.AddAsync(PoliceReportWitnessData);
                            }
                        #endregion

                        #region Police Report Property Damage
                            foreach (AddPoliceReportPropertyDamageRequest AddPoliceReportPropertyDamageRequest in AddPoliceReportRequest.AddPoliceReportPropertyDamageRequest)
                            {

                                PoliceReportPropertyDamage PoliceReportPropertyDamageData = new PoliceReportPropertyDamage();
                                PoliceReportPropertyDamageData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportPropertyDamageData.OwnerLastName = AddPoliceReportPropertyDamageRequest.OwnerLastName;
                                PoliceReportPropertyDamageData.OwnerFirstName = AddPoliceReportPropertyDamageRequest.OwnerFirstName;
                                PoliceReportPropertyDamageData.OwnerMiddleName = AddPoliceReportPropertyDamageRequest.OwnerMiddleName;
                                PoliceReportPropertyDamageData.Address = AddPoliceReportPropertyDamageRequest.Address;
                                PoliceReportPropertyDamageData.City = AddPoliceReportPropertyDamageRequest.City;
                                PoliceReportPropertyDamageData.State = AddPoliceReportPropertyDamageRequest.State;
                                PoliceReportPropertyDamageData.Zip = AddPoliceReportPropertyDamageRequest.Zip;
                                PoliceReportPropertyDamageData.Phone = AddPoliceReportPropertyDamageRequest.Phone;
                                PoliceReportPropertyDamageData.FourOneType = AddPoliceReportPropertyDamageRequest.FourOneType;
                                PoliceReportPropertyDamageData.Description = AddPoliceReportPropertyDamageRequest.Description;
                                await _tonTowDbContext.PoliceReportPropertyDamage.AddAsync(PoliceReportPropertyDamageData);
                            }
                        #endregion

                        #region Police Report TruckAndBusDtls
                            foreach (AddPoliceReportTruckAndBusDtlsRequest AddPoliceReportTruckAndBusDtlsRequest in AddPoliceReportRequest.AddPoliceReportTruckAndBusDtlsRequest)
                            {
                                PoliceReportTruckAndBusDtls PoliceReportTruckAndBusDtlData = new PoliceReportTruckAndBusDtls();
                                PoliceReportTruckAndBusDtlData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportTruckAndBusDtlData.VehicleNo = AddPoliceReportTruckAndBusDtlsRequest.VehicleNo;
                                PoliceReportTruckAndBusDtlData.Registration = AddPoliceReportTruckAndBusDtlsRequest.Registration;
                                PoliceReportTruckAndBusDtlData.CarrierName = AddPoliceReportTruckAndBusDtlsRequest.CarrierName;
                                PoliceReportTruckAndBusDtlData.BusUse = AddPoliceReportTruckAndBusDtlsRequest.BusUse;
                                PoliceReportTruckAndBusDtlData.Address = AddPoliceReportTruckAndBusDtlsRequest.Address;
                                PoliceReportTruckAndBusDtlData.City = AddPoliceReportTruckAndBusDtlsRequest.City;
                                PoliceReportTruckAndBusDtlData.St = AddPoliceReportTruckAndBusDtlsRequest.St;
                                PoliceReportTruckAndBusDtlData.Zip = AddPoliceReportTruckAndBusDtlsRequest.Zip;
                                PoliceReportTruckAndBusDtlData.UsDot = AddPoliceReportTruckAndBusDtlsRequest.UsDot;
                                PoliceReportTruckAndBusDtlData.StateNumber = AddPoliceReportTruckAndBusDtlsRequest.StateNumber;
                                PoliceReportTruckAndBusDtlData.IssuingState = AddPoliceReportTruckAndBusDtlsRequest.IssuingState;
                                PoliceReportTruckAndBusDtlData.MCMXICC = AddPoliceReportTruckAndBusDtlsRequest.MCMXICC;
                                PoliceReportTruckAndBusDtlData.InterState = AddPoliceReportTruckAndBusDtlsRequest.InterState;
                                PoliceReportTruckAndBusDtlData.CargoBodyType = AddPoliceReportTruckAndBusDtlsRequest.CargoBodyType;
                                PoliceReportTruckAndBusDtlData.GVGCWR = AddPoliceReportTruckAndBusDtlsRequest.GVGCWR;
                                PoliceReportTruckAndBusDtlData.TrailerReg = AddPoliceReportTruckAndBusDtlsRequest.TrailerReg;
                                PoliceReportTruckAndBusDtlData.RegType = AddPoliceReportTruckAndBusDtlsRequest.RegType;
                                PoliceReportTruckAndBusDtlData.RegState = AddPoliceReportTruckAndBusDtlsRequest.RegState;
                                PoliceReportTruckAndBusDtlData.RegYear = AddPoliceReportTruckAndBusDtlsRequest.RegYear;
                                PoliceReportTruckAndBusDtlData.TrailerLength = AddPoliceReportTruckAndBusDtlsRequest.TrailerLength;
                                PoliceReportTruckAndBusDtlData.Placard = AddPoliceReportTruckAndBusDtlsRequest.Placard;
                                PoliceReportTruckAndBusDtlData.Material1 = AddPoliceReportTruckAndBusDtlsRequest.Material1;
                                PoliceReportTruckAndBusDtlData.MaterialName = AddPoliceReportTruckAndBusDtlsRequest.MaterialName;
                                PoliceReportTruckAndBusDtlData.MaterialDigit = AddPoliceReportTruckAndBusDtlsRequest.MaterialDigit;
                                PoliceReportTruckAndBusDtlData.ReleaseCode = AddPoliceReportTruckAndBusDtlsRequest.ReleaseCode;
                                PoliceReportTruckAndBusDtlData.OfficerName = AddPoliceReportTruckAndBusDtlsRequest.OfficerName;
                                PoliceReportTruckAndBusDtlData.IDBadge = AddPoliceReportTruckAndBusDtlsRequest.IDBadge;
                                PoliceReportTruckAndBusDtlData.Department = AddPoliceReportTruckAndBusDtlsRequest.Department;
                                PoliceReportTruckAndBusDtlData.PrecinctBarracks = AddPoliceReportTruckAndBusDtlsRequest.PrecinctBarracks;
                                PoliceReportTruckAndBusDtlData.Date = AddPoliceReportTruckAndBusDtlsRequest.Date;
                                await _tonTowDbContext.PoliceReportTruckAndBusDtls.AddAsync(PoliceReportTruckAndBusDtlData);
                            }
                        #endregion

                        #region Police Report General
                            PoliceReportGeneral PoliceReportGeneralData = new PoliceReportGeneral();
                            PoliceReportGeneralData.TonTowRptId = PoliceReportData.TonTowRptId;
                            PoliceReportGeneralData.AccidentDate = AddPoliceReportRequest.AddPoliceReportGeneralRequest.AccidentDate;
                            PoliceReportGeneralData.AccidentTime = AddPoliceReportRequest.AddPoliceReportGeneralRequest.AccidentTime;
                            PoliceReportGeneralData.ReportingOfficer = AddPoliceReportRequest.AddPoliceReportGeneralRequest.ReportingOfficer;
                            PoliceReportGeneralData.Location = AddPoliceReportRequest.AddPoliceReportGeneralRequest.Location;
                            PoliceReportGeneralData.City = AddPoliceReportRequest.AddPoliceReportGeneralRequest.City;
                            PoliceReportGeneralData.State = AddPoliceReportRequest.AddPoliceReportGeneralRequest.State;
                            PoliceReportGeneralData.Zip = AddPoliceReportRequest.AddPoliceReportGeneralRequest.Zip;
                            await _tonTowDbContext.PoliceReportGeneral.AddAsync(PoliceReportGeneralData);
                        #endregion

                        #region Police Report OperatorOwnerVehicleDtls
                            foreach (AddPoliceReportOperatorOwnerVehicleDtlsRequest AddPoliceReportOperatorOwnerVehicleDtlsRequest in AddPoliceReportRequest.AddPoliceReportOperatorOwnerVehicleDtlsRequest)
                            {
                                PoliceReportOperatorOwnerVehicleDtls PoliceReportOperatorOwnerVehicleDtlsData = new PoliceReportOperatorOwnerVehicleDtls();
                                PoliceReportOperatorOwnerVehicleDtlsData.TonTowRptId = PoliceReportData.TonTowRptId;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorLastName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorLastName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorFirstName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorFirstName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorMiddleName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorMiddleName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorSuffixName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorSuffixName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorVeh = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorVeh;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorInjured = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorInjured;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorFatality = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorFatality;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreet = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreet;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreetSuffix = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreetSuffix;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreetApt = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreetApt;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorCity = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorCity;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorState = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorState;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorZip = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorZip;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorDOB = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorDOB;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorHomePhone = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorHomePhone;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorWorkPhone = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorWorkPhone;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorLic = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorLic;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorStateNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStateNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorInsuranceComp = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorInsuranceComp;
                                PoliceReportOperatorOwnerVehicleDtlsData.OperatorPolicyNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OperatorPolicyNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerLastName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerLastName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerFirstName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerFirstName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerMiddleName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerMiddleName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerSuffixName = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerSuffixName;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerHomePhone = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerHomePhone;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerWorkPhone = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerWorkPhone;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreet = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreet;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreetSuffix = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreetSuffix;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreetApt = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreetApt;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerCity = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerCity;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerState = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerState;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerZip = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerZip;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerInsuranceComp = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerInsuranceComp;
                                PoliceReportOperatorOwnerVehicleDtlsData.OwnerPolicyNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.OwnerPolicyNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehYear = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehYear;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehMake = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehMake;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehModel = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehModel;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehVin = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehVin;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehReg = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehReg;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehStateNumber = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehStateNumber;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehTowedBy = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehTowedBy;
                                PoliceReportOperatorOwnerVehicleDtlsData.VehTowedTo = AddPoliceReportOperatorOwnerVehicleDtlsRequest.VehTowedTo;
                                await _tonTowDbContext.PoliceReportOperatorOwnerVehicleDtls.AddAsync(PoliceReportOperatorOwnerVehicleDtlsData);
                            }
                    #endregion

                        #region Create new user
                        TonTowUser userLoginInfo = new TonTowUser();
                        CreatePasswordHash(AddPoliceReportRequest.JobNum + strVehicle1Dob.Split(' ')[0], out byte[] passwordHash, out byte[] passwordSalt);
                        userLoginInfo.Username = AddPoliceReportRequest.JobNum;
                        userLoginInfo.TonTowRptId = PoliceReportData.TonTowRptId;
                        userLoginInfo.PasswordHash = passwordHash;
                        userLoginInfo.PasswordSalt = passwordSalt;
                        userLoginInfo.Role = "U";
                        userLoginInfo.RefreshToken = CreateToken(userLoginInfo);
                        var refreshToken = GenerateRefreshToken();
                        SetRefreshToken(refreshToken);
                        userLoginInfo.TokenCreated = refreshToken.Created;
                        userLoginInfo.TokenExpires = refreshToken.Expires;
                        userLoginInfo.EmailId = AddPoliceReportRequest.EmailId;
                        userLoginInfo.Phone= AddPoliceReportRequest.Phone;
                        await _tonTowDbContext.TonTowUser.AddAsync(userLoginInfo);
                    #endregion

                        #region Create TonTow Report File Link
                        for (int i = 0; i < 2; i++)
                        {
                            TonTowFileUpload tontowFileUploadLink = new TonTowFileUpload();
                            tontowFileUploadLink.TonTowRptId = PoliceReportData.TonTowRptId;
                            if (i == 0)
                            {
                                tontowFileUploadLink.FileName = AddPoliceReportRequest.JobNum + ".pdf";
                                tontowFileUploadLink.FileType = "TonTowReport-Entire";
                            }
                            if (i == 1)
                            {
                                tontowFileUploadLink.FileName = AddPoliceReportRequest.JobNum + ".pdf";
                                tontowFileUploadLink.FileType = "TonTowReport-Mandatory";
                            }
                            tontowFileUploadLink.CreatedBy = AddPoliceReportRequest.CreatedBy;
                            tontowFileUploadLink.CreatedDate = DateTime.Now;
                            tontowFileUploadLink.ModifiedBy = null;
                            await _tonTowDbContext.TonTowFileUpload.AddAsync(tontowFileUploadLink);
                        }
                    #endregion

                        await _tonTowDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Ok(new { Result = "Success" });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Ok(new { Result = "Failed" });
                    throw ex;
                }
                return Ok(new { Result = "Success" });
            }
            }
        #endregion

        [HttpPost("UpdatePoliceReport"), Authorize(Roles = "A")]
        public async Task<IActionResult> UpdatePoliceReport([FromBody] UpdatePoliceReportRequest UpdatePoliceReportRequest)
        {   
            using (var transaction = await _tonTowDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    #region PoliceReport
                    var UpdatePoliceReportData = _tonTowDbContext.PoliceReport.SingleOrDefault(
                    p => p.TonTowRptId == UpdatePoliceReportRequest.TonTowRptId
                    );
                    if (UpdatePoliceReportData != null)
                    {
                        UpdatePoliceReportData.DateOfCrash = UpdatePoliceReportRequest.DateOfCrash;
                        UpdatePoliceReportData.TimeOfCrash = UpdatePoliceReportRequest.TimeOfCrash;
                        UpdatePoliceReportData.CityTown = UpdatePoliceReportRequest.CityTown;
                        UpdatePoliceReportData.VehicleNumber = UpdatePoliceReportRequest.VehicleNumber;
                        UpdatePoliceReportData.InjuredNumber = UpdatePoliceReportRequest.InjuredNumber;
                        UpdatePoliceReportData.SpeedLimit = UpdatePoliceReportRequest.SpeedLimit;
                        UpdatePoliceReportData.Latitude = UpdatePoliceReportRequest.Latitude;
                        UpdatePoliceReportData.Longitude = UpdatePoliceReportRequest.Longitude;
                        UpdatePoliceReportData.StatePolice = UpdatePoliceReportRequest.StatePolice;
                        UpdatePoliceReportData.LocalPolice = UpdatePoliceReportRequest.LocalPolice;
                        UpdatePoliceReportData.MBTAPolice = UpdatePoliceReportRequest.MBTAPolice;
                        UpdatePoliceReportData.CampusPolice = UpdatePoliceReportRequest.CampusPolice;
                        UpdatePoliceReportData.Other = UpdatePoliceReportRequest.Other;
                        UpdatePoliceReportData.AtIntersection = UpdatePoliceReportRequest.AtIntersection;
                        UpdatePoliceReportData.AIRoute1 = UpdatePoliceReportRequest.AIRoute1;
                        UpdatePoliceReportData.AIDirection1 = UpdatePoliceReportRequest.AIDirection1;
                        UpdatePoliceReportData.AIRoadwayStName1 = UpdatePoliceReportRequest.AIRoadwayStName1;
                        UpdatePoliceReportData.AIRoute2 = UpdatePoliceReportRequest.AIRoute2;
                        UpdatePoliceReportData.AIDirection2 = UpdatePoliceReportRequest.AIDirection2;
                        UpdatePoliceReportData.AIRoadwayStName2 = UpdatePoliceReportRequest.AIRoadwayStName2;
                        UpdatePoliceReportData.AIRoute3 = UpdatePoliceReportRequest.AIRoute3;
                        UpdatePoliceReportData.AIDirection3 = UpdatePoliceReportRequest.AIDirection3;
                        UpdatePoliceReportData.AIRoadwayStName3 = UpdatePoliceReportRequest.AIRoadwayStName3;
                        UpdatePoliceReportData.NAIRoute = UpdatePoliceReportRequest.NAIRoute;
                        UpdatePoliceReportData.NAIDirection = UpdatePoliceReportRequest.NAIDirection;
                        UpdatePoliceReportData.NAIAddress = UpdatePoliceReportRequest.NAIAddress;
                        UpdatePoliceReportData.NAIRoadwayStName = UpdatePoliceReportRequest.NAIRoadwayStName;
                        UpdatePoliceReportData.NAIFeet1 = UpdatePoliceReportRequest.NAIFeet1;
                        UpdatePoliceReportData.NAIDirection1 = UpdatePoliceReportRequest.NAIDirection1;
                        UpdatePoliceReportData.NAIMile = UpdatePoliceReportRequest.NAIMile;
                        UpdatePoliceReportData.NAIExitNo = UpdatePoliceReportRequest.NAIExitNo;
                        UpdatePoliceReportData.NAIFeet2 = UpdatePoliceReportRequest.NAIFeet2;
                        UpdatePoliceReportData.NAIDirection2 = UpdatePoliceReportRequest.NAIDirection2;
                        UpdatePoliceReportData.NAIRoute1 = UpdatePoliceReportRequest.NAIRoute1;
                        UpdatePoliceReportData.NAIRoadwaySt = UpdatePoliceReportRequest.NAIRoadwaySt;
                        UpdatePoliceReportData.NAIFeet3 = UpdatePoliceReportRequest.NAIFeet3;
                        UpdatePoliceReportData.NAIDirection3 = UpdatePoliceReportRequest.NAIDirection3;
                        UpdatePoliceReportData.NAILandmark = UpdatePoliceReportRequest.NAILandmark;
                        UpdatePoliceReportData.CrashReportId = UpdatePoliceReportRequest.CrashReportId;
                        UpdatePoliceReportData.CrashNarrative = UpdatePoliceReportRequest.CrashNarrative;
                        UpdatePoliceReportData.ModifiedBy = UpdatePoliceReportRequest.ModifiedBy;
                        UpdatePoliceReportData.ModifiedDate = DateTime.Now;
                        _tonTowDbContext.PoliceReport.UpdateRange(UpdatePoliceReportData);
                    }
                    #endregion

                    #region Police Report VehicleDtls


                    foreach (UpdatePoliceReportVehicleDtlsRequest PoliceReportVehicleDtlsRequest in UpdatePoliceReportRequest.UpdatePoliceReportVehicleDtlsRequest)
                    {
                        var PoliceReportVehicleDtlsData = _tonTowDbContext.PoliceReportVehicleDtls.SingleOrDefault(
                            p => p.Id == PoliceReportVehicleDtlsRequest.Id
                            );
                        if (PoliceReportVehicleDtlsData != null)
                        {
                            //PoliceReportVehicleDtlsData.VehicleNo = PoliceReportVehicleDtlsRequest.VehicleNo;
                            PoliceReportVehicleDtlsData.CrashType = PoliceReportVehicleDtlsRequest.CrashType;
                            PoliceReportVehicleDtlsData.Type = PoliceReportVehicleDtlsRequest.Type;
                            PoliceReportVehicleDtlsData.Action = PoliceReportVehicleDtlsRequest.Action;
                            PoliceReportVehicleDtlsData.Location = PoliceReportVehicleDtlsRequest.Location;
                            PoliceReportVehicleDtlsData.Condition = PoliceReportVehicleDtlsRequest.Condition;
                            PoliceReportVehicleDtlsData.Occupants = PoliceReportVehicleDtlsRequest.Occupants;
                            PoliceReportVehicleDtlsData.License = PoliceReportVehicleDtlsRequest.License;
                            PoliceReportVehicleDtlsData.Street = PoliceReportVehicleDtlsRequest.Street;
                            PoliceReportVehicleDtlsData.DOBAge = PoliceReportVehicleDtlsRequest.DOBAge;
                            PoliceReportVehicleDtlsData.Sex = PoliceReportVehicleDtlsRequest.Sex;
                            PoliceReportVehicleDtlsData.LicClass = PoliceReportVehicleDtlsRequest.LicClass;
                            PoliceReportVehicleDtlsData.LicRestrictions = PoliceReportVehicleDtlsRequest.LicRestrictions;
                            PoliceReportVehicleDtlsData.CDLEndorsement = PoliceReportVehicleDtlsRequest.CDLEndorememt;
                            PoliceReportVehicleDtlsData.OperatorLastName = PoliceReportVehicleDtlsRequest.OperatorLastName;
                            PoliceReportVehicleDtlsData.OperatorFirstName = PoliceReportVehicleDtlsRequest.OperatorFirstName;
                            PoliceReportVehicleDtlsData.OperatorMiddleName = PoliceReportVehicleDtlsRequest.OperatorMiddleName;
                            PoliceReportVehicleDtlsData.OperatorAddress = PoliceReportVehicleDtlsRequest.OperatorAddress;
                            PoliceReportVehicleDtlsData.OperatorCity = PoliceReportVehicleDtlsRequest.OperatorCity;
                            PoliceReportVehicleDtlsData.OperatorState = PoliceReportVehicleDtlsRequest.OperatorState;
                            PoliceReportVehicleDtlsData.OperatorZip = PoliceReportVehicleDtlsRequest.OperatorZip;
                            PoliceReportVehicleDtlsData.InsuranceCompany = PoliceReportVehicleDtlsRequest.InsuranceCompany;
                            PoliceReportVehicleDtlsData.VehicleTravelDirection = PoliceReportVehicleDtlsRequest.VehicleTravelDirection;
                            PoliceReportVehicleDtlsData.RespondingToEmergency = PoliceReportVehicleDtlsRequest.RespondingtoEmergency;
                            PoliceReportVehicleDtlsData.CitationIssued = PoliceReportVehicleDtlsRequest.CitationIssued;
                            PoliceReportVehicleDtlsData.Viol1 = PoliceReportVehicleDtlsRequest.Viol1;
                            PoliceReportVehicleDtlsData.Viol2 = PoliceReportVehicleDtlsRequest.Viol2;
                            PoliceReportVehicleDtlsData.Viol3 = PoliceReportVehicleDtlsRequest.Viol3;
                            PoliceReportVehicleDtlsData.Viol4 = PoliceReportVehicleDtlsRequest.Viol4;
                            PoliceReportVehicleDtlsData.Reg = PoliceReportVehicleDtlsRequest.Reg;
                            PoliceReportVehicleDtlsData.RegType = PoliceReportVehicleDtlsRequest.RegType;
                            PoliceReportVehicleDtlsData.RegState = PoliceReportVehicleDtlsRequest.RegState;
                            PoliceReportVehicleDtlsData.VehicleYear = PoliceReportVehicleDtlsRequest.VehicleYear;
                            PoliceReportVehicleDtlsData.VehicleMake = PoliceReportVehicleDtlsRequest.VehicleMake;
                            PoliceReportVehicleDtlsData.VehicleConfig = PoliceReportVehicleDtlsRequest.VehicleConfig;
                            PoliceReportVehicleDtlsData.OwnerLastName = PoliceReportVehicleDtlsRequest.OwnerLastName;
                            PoliceReportVehicleDtlsData.OwnerFirstName = PoliceReportVehicleDtlsRequest.OwnerFirstName;
                            PoliceReportVehicleDtlsData.OwnerMiddleName = PoliceReportVehicleDtlsRequest.OwnerMiddleName;
                            PoliceReportVehicleDtlsData.OwnerAddress = PoliceReportVehicleDtlsRequest.OwnerAddress;
                            PoliceReportVehicleDtlsData.OwnerCity = PoliceReportVehicleDtlsRequest.OwnerCity;
                            PoliceReportVehicleDtlsData.OwnerState = PoliceReportVehicleDtlsRequest.OwnerState;
                            PoliceReportVehicleDtlsData.OwnerZip = PoliceReportVehicleDtlsRequest.OwnerZip;
                            PoliceReportVehicleDtlsData.VehicleActionPriortoCrash = PoliceReportVehicleDtlsRequest.VehicleActionPriortoCrash;
                            PoliceReportVehicleDtlsData.EventSequence1 = PoliceReportVehicleDtlsRequest.EventSequence1;
                            PoliceReportVehicleDtlsData.EventSequence2 = PoliceReportVehicleDtlsRequest.EventSequence2;
                            PoliceReportVehicleDtlsData.EventSequence3 = PoliceReportVehicleDtlsRequest.EventSequence3;
                            PoliceReportVehicleDtlsData.EventSequence4 = PoliceReportVehicleDtlsRequest.EventSequence4;
                            PoliceReportVehicleDtlsData.MostHarmfulEvent = PoliceReportVehicleDtlsRequest.MostHarmfulEvent;
                            PoliceReportVehicleDtlsData.DriverContributingCode1 = PoliceReportVehicleDtlsRequest.DriverContributingCode1;
                            PoliceReportVehicleDtlsData.DriverContributingCode2 = PoliceReportVehicleDtlsRequest.DriverContributingCode2;
                            PoliceReportVehicleDtlsData.DriverDistractedBy = PoliceReportVehicleDtlsRequest.DriverDistractedBy;
                            PoliceReportVehicleDtlsData.DamagedAreaCode1 = PoliceReportVehicleDtlsRequest.DamagedAreaCode1;
                            PoliceReportVehicleDtlsData.DamagedAreaCode2 = PoliceReportVehicleDtlsRequest.DamagedAreaCode2;
                            PoliceReportVehicleDtlsData.DamagedAreaCode3 = PoliceReportVehicleDtlsRequest.DamagedAreaCode3;
                            PoliceReportVehicleDtlsData.TestStatus = PoliceReportVehicleDtlsRequest.TestStatus;
                            PoliceReportVehicleDtlsData.TypeofTest = PoliceReportVehicleDtlsRequest.TypeofTest;
                            PoliceReportVehicleDtlsData.BacTestResult = PoliceReportVehicleDtlsRequest.BacTestResult;
                            PoliceReportVehicleDtlsData.SuspectedAlcohol = PoliceReportVehicleDtlsRequest.SuspectedAlcohol;
                            PoliceReportVehicleDtlsData.SuspectedDrug = PoliceReportVehicleDtlsRequest.SuspectedDrug;
                            PoliceReportVehicleDtlsData.TowedFromScene = PoliceReportVehicleDtlsRequest.TowedFromScene;
                            _tonTowDbContext.PoliceReportVehicleDtls.UpdateRange(PoliceReportVehicleDtlsData);
                        }
                    }
                    #endregion

                    #region Police Report OperatorDtls

                    foreach (UpdatePoliceReportOperatorDtlsRequest UpdatePoliceReportOperatorDtlsRequest in UpdatePoliceReportRequest.UpdatePoliceReportOperatorDtlsRequest)
                    {
                        var PoliceReportOperatorDtls = _tonTowDbContext.PoliceReportOperatorDtls.SingleOrDefault(
                              p => p.Id == UpdatePoliceReportOperatorDtlsRequest.Id
                              );
                        if (PoliceReportOperatorDtls != null)
                        {
                            //PoliceReportOperatorDtls.VehicleNo = UpdatePoliceReportOperatorDtlsRequest.VehicleNo;
                            PoliceReportOperatorDtls.OperatorLastName = UpdatePoliceReportOperatorDtlsRequest.OperatorLastName;
                            PoliceReportOperatorDtls.OperatorFirstName = UpdatePoliceReportOperatorDtlsRequest.OperatorFirstName;
                            PoliceReportOperatorDtls.OperatorMiddleName = UpdatePoliceReportOperatorDtlsRequest.OperatorMiddleName;
                            PoliceReportOperatorDtls.Address = UpdatePoliceReportOperatorDtlsRequest.Address;
                            PoliceReportOperatorDtls.City = UpdatePoliceReportOperatorDtlsRequest.City;
                            PoliceReportOperatorDtls.State = UpdatePoliceReportOperatorDtlsRequest.State;
                            PoliceReportOperatorDtls.Zip = UpdatePoliceReportOperatorDtlsRequest.Zip;
                            PoliceReportOperatorDtls.DOB = UpdatePoliceReportOperatorDtlsRequest.DOB;
                            PoliceReportOperatorDtls.Sex = UpdatePoliceReportOperatorDtlsRequest.Sex;
                            PoliceReportOperatorDtls.SeatPosition = UpdatePoliceReportOperatorDtlsRequest.SeatPosition;
                            PoliceReportOperatorDtls.SafetySystem = UpdatePoliceReportOperatorDtlsRequest.SafetySystem;
                            PoliceReportOperatorDtls.AirbagStatus = UpdatePoliceReportOperatorDtlsRequest.AirbagStatus;
                            PoliceReportOperatorDtls.EjectCode = UpdatePoliceReportOperatorDtlsRequest.EjectCode;
                            PoliceReportOperatorDtls.TrapCode = UpdatePoliceReportOperatorDtlsRequest.TrapCode;
                            PoliceReportOperatorDtls.InjuryStatus = UpdatePoliceReportOperatorDtlsRequest.InjuryStatus;
                            PoliceReportOperatorDtls.TranspCode = UpdatePoliceReportOperatorDtlsRequest.TranspCode;
                            PoliceReportOperatorDtls.MedicalFacility = UpdatePoliceReportOperatorDtlsRequest.MedicalFacility;
                            _tonTowDbContext.PoliceReportOperatorDtls.UpdateRange(PoliceReportOperatorDtls);
                        }
                        else if(UpdatePoliceReportOperatorDtlsRequest !=null && UpdatePoliceReportOperatorDtlsRequest.Id==0)
                        {
                            PoliceReportOperatorDtls PoliceReportOperatorVehicleDtlsAddData = new PoliceReportOperatorDtls();
                            PoliceReportOperatorVehicleDtlsAddData.TonTowRptId = UpdatePoliceReportRequest.TonTowRptId;
                            PoliceReportOperatorVehicleDtlsAddData.VehicleNo = UpdatePoliceReportOperatorDtlsRequest.VehicleNo;
                            PoliceReportOperatorVehicleDtlsAddData.OperatorLastName = UpdatePoliceReportOperatorDtlsRequest.OperatorLastName;
                            PoliceReportOperatorVehicleDtlsAddData.OperatorFirstName = UpdatePoliceReportOperatorDtlsRequest.OperatorFirstName;
                            PoliceReportOperatorVehicleDtlsAddData.OperatorMiddleName = UpdatePoliceReportOperatorDtlsRequest.OperatorMiddleName;
                            PoliceReportOperatorVehicleDtlsAddData.Address = UpdatePoliceReportOperatorDtlsRequest.Address;
                            PoliceReportOperatorVehicleDtlsAddData.City = UpdatePoliceReportOperatorDtlsRequest.City;
                            PoliceReportOperatorVehicleDtlsAddData.State = UpdatePoliceReportOperatorDtlsRequest.State;
                            PoliceReportOperatorVehicleDtlsAddData.Zip = UpdatePoliceReportOperatorDtlsRequest.Zip;
                            PoliceReportOperatorVehicleDtlsAddData.DOB = UpdatePoliceReportOperatorDtlsRequest.DOB;
                            PoliceReportOperatorVehicleDtlsAddData.Sex = UpdatePoliceReportOperatorDtlsRequest.Sex;
                            PoliceReportOperatorVehicleDtlsAddData.SeatPosition = UpdatePoliceReportOperatorDtlsRequest.SeatPosition;
                            PoliceReportOperatorVehicleDtlsAddData.SafetySystem = UpdatePoliceReportOperatorDtlsRequest.SafetySystem;
                            PoliceReportOperatorVehicleDtlsAddData.AirbagStatus = UpdatePoliceReportOperatorDtlsRequest.AirbagStatus;
                            PoliceReportOperatorVehicleDtlsAddData.EjectCode = UpdatePoliceReportOperatorDtlsRequest.EjectCode;
                            PoliceReportOperatorVehicleDtlsAddData.TrapCode = UpdatePoliceReportOperatorDtlsRequest.TrapCode;
                            PoliceReportOperatorVehicleDtlsAddData.InjuryStatus = UpdatePoliceReportOperatorDtlsRequest.InjuryStatus;
                            PoliceReportOperatorVehicleDtlsAddData.TranspCode = UpdatePoliceReportOperatorDtlsRequest.TranspCode;
                            PoliceReportOperatorVehicleDtlsAddData.MedicalFacility = UpdatePoliceReportOperatorDtlsRequest.MedicalFacility;
                            await _tonTowDbContext.PoliceReportOperatorDtls.AddAsync(PoliceReportOperatorVehicleDtlsAddData);
                        }
                    }
                    #endregion

                    #region Police Report Witness

                    foreach (UpdatePoliceReportWitnessRequest UpdatePoliceReportWitnessRequest in UpdatePoliceReportRequest.UpdatePoliceReportWitnessRequest)
                    {
                        var PoliceReportWitnessData = _tonTowDbContext.PoliceReportWitness.SingleOrDefault(
                        p => p.Id == UpdatePoliceReportWitnessRequest.Id
                        );
                        if (PoliceReportWitnessData != null)
                        {
                            PoliceReportWitnessData.LastName = UpdatePoliceReportWitnessRequest.LastName;
                            PoliceReportWitnessData.FirstName = UpdatePoliceReportWitnessRequest.FirstName;
                            PoliceReportWitnessData.MiddleName = UpdatePoliceReportWitnessRequest.MiddleName;
                            PoliceReportWitnessData.Address = UpdatePoliceReportWitnessRequest.Address;
                            PoliceReportWitnessData.City = UpdatePoliceReportWitnessRequest.City;
                            PoliceReportWitnessData.State = UpdatePoliceReportWitnessRequest.State;
                            PoliceReportWitnessData.Zip = UpdatePoliceReportWitnessRequest.Zip;
                            PoliceReportWitnessData.Phone = UpdatePoliceReportWitnessRequest.Phone;
                            PoliceReportWitnessData.Statement = UpdatePoliceReportWitnessRequest.Statement;
                            _tonTowDbContext.PoliceReportWitness.UpdateRange(PoliceReportWitnessData);
                        }
                        else if(UpdatePoliceReportWitnessRequest!=null && UpdatePoliceReportWitnessRequest.Id==0)
                        {
                            PoliceReportWitness PoliceReportWitnessAddData = new PoliceReportWitness();
                            PoliceReportWitnessAddData.TonTowRptId = UpdatePoliceReportRequest.TonTowRptId;
                            PoliceReportWitnessAddData.LastName = UpdatePoliceReportWitnessRequest.LastName;
                            PoliceReportWitnessAddData.FirstName = UpdatePoliceReportWitnessRequest.FirstName;
                            PoliceReportWitnessAddData.MiddleName = UpdatePoliceReportWitnessRequest.MiddleName;
                            PoliceReportWitnessAddData.Address = UpdatePoliceReportWitnessRequest.Address;
                            PoliceReportWitnessAddData.City = UpdatePoliceReportWitnessRequest.City;
                            PoliceReportWitnessAddData.State = UpdatePoliceReportWitnessRequest.State;
                            PoliceReportWitnessAddData.Zip = UpdatePoliceReportWitnessRequest.Zip;
                            PoliceReportWitnessAddData.Phone = UpdatePoliceReportWitnessRequest.Phone;
                            PoliceReportWitnessAddData.Statement = UpdatePoliceReportWitnessRequest.Statement;
                            await _tonTowDbContext.PoliceReportWitness.AddAsync(PoliceReportWitnessAddData);
                        }
                    }
                    #endregion

                    #region Police Report PropertyDamage
                    foreach (UpdatePoliceReportPropertyDamageRequest UpdatePoliceReportPropertyDamageRequest in UpdatePoliceReportRequest.UpdatePoliceReportPropertyDamageRequest)
                    {

                        var PoliceReportPropertyDamageData = _tonTowDbContext.PoliceReportPropertyDamage.SingleOrDefault(
                        p => p.Id == UpdatePoliceReportPropertyDamageRequest.Id
                        );
                        if (PoliceReportPropertyDamageData != null)
                        {

                            PoliceReportPropertyDamageData.OwnerLastName = UpdatePoliceReportPropertyDamageRequest.OwnerLastName;
                            PoliceReportPropertyDamageData.OwnerFirstName = UpdatePoliceReportPropertyDamageRequest.OwnerFirstName;
                            PoliceReportPropertyDamageData.OwnerMiddleName = UpdatePoliceReportPropertyDamageRequest.OwnerMiddleName;
                            PoliceReportPropertyDamageData.Address = UpdatePoliceReportPropertyDamageRequest.Address;
                            PoliceReportPropertyDamageData.City = UpdatePoliceReportPropertyDamageRequest.City;
                            PoliceReportPropertyDamageData.State = UpdatePoliceReportPropertyDamageRequest.State;
                            PoliceReportPropertyDamageData.Zip = UpdatePoliceReportPropertyDamageRequest.Zip;
                            PoliceReportPropertyDamageData.Phone = UpdatePoliceReportPropertyDamageRequest.Phone;
                            PoliceReportPropertyDamageData.FourOneType = UpdatePoliceReportPropertyDamageRequest.FourOneType;
                            PoliceReportPropertyDamageData.Description = UpdatePoliceReportPropertyDamageRequest.Description;
                            _tonTowDbContext.PoliceReportPropertyDamage.UpdateRange(PoliceReportPropertyDamageData);
                        }
                        else if(UpdatePoliceReportPropertyDamageRequest!=null && UpdatePoliceReportPropertyDamageRequest.Id==0)
                        {
                            PoliceReportPropertyDamage PoliceReportPropertyDamageAddData = new PoliceReportPropertyDamage();
                            PoliceReportPropertyDamageAddData.TonTowRptId = UpdatePoliceReportRequest.TonTowRptId;
                            PoliceReportPropertyDamageAddData.OwnerLastName = UpdatePoliceReportPropertyDamageRequest.OwnerLastName;
                            PoliceReportPropertyDamageAddData.OwnerFirstName = UpdatePoliceReportPropertyDamageRequest.OwnerFirstName;
                            PoliceReportPropertyDamageAddData.OwnerMiddleName = UpdatePoliceReportPropertyDamageRequest.OwnerMiddleName;
                            PoliceReportPropertyDamageAddData.Address = UpdatePoliceReportPropertyDamageRequest.Address;
                            PoliceReportPropertyDamageAddData.City = UpdatePoliceReportPropertyDamageRequest.City;
                            PoliceReportPropertyDamageAddData.State = UpdatePoliceReportPropertyDamageRequest.State;
                            PoliceReportPropertyDamageAddData.Zip = UpdatePoliceReportPropertyDamageRequest.Zip;
                            PoliceReportPropertyDamageAddData.Phone = UpdatePoliceReportPropertyDamageRequest.Phone;
                            PoliceReportPropertyDamageAddData.FourOneType = UpdatePoliceReportPropertyDamageRequest.FourOneType;
                            PoliceReportPropertyDamageAddData.Description = UpdatePoliceReportPropertyDamageRequest.Description;
                            await _tonTowDbContext.PoliceReportPropertyDamage.AddAsync(PoliceReportPropertyDamageAddData);
                        }
                    }
                    #endregion

                    #region Police Report TruckAndBusDtls


                    foreach (UpdatePoliceReportTruckAndBusDtlsRequest UpdatePoliceReportTruckAndBusDtlsRequest in UpdatePoliceReportRequest.UpdatePoliceReportTruckAndBusDtlsRequest)
                    {
                        var PoliceReportTruckAndBusDtlData = _tonTowDbContext.PoliceReportTruckAndBusDtls.SingleOrDefault(
                        p => p.Id == UpdatePoliceReportTruckAndBusDtlsRequest.Id
                        );
                        if (PoliceReportTruckAndBusDtlData != null)
                        {
                            //PoliceReportTruckAndBusDtlData.VehicleNo = UpdatePoliceReportTruckAndBusDtlsRequest.VehicleNo;
                            PoliceReportTruckAndBusDtlData.Registration = UpdatePoliceReportTruckAndBusDtlsRequest.Registration;
                            PoliceReportTruckAndBusDtlData.CarrierName = UpdatePoliceReportTruckAndBusDtlsRequest.CarrierName;
                            PoliceReportTruckAndBusDtlData.BusUse = UpdatePoliceReportTruckAndBusDtlsRequest.BusUse;
                            PoliceReportTruckAndBusDtlData.Address = UpdatePoliceReportTruckAndBusDtlsRequest.Address;
                            PoliceReportTruckAndBusDtlData.City = UpdatePoliceReportTruckAndBusDtlsRequest.City;
                            PoliceReportTruckAndBusDtlData.St = UpdatePoliceReportTruckAndBusDtlsRequest.St;
                            PoliceReportTruckAndBusDtlData.Zip = UpdatePoliceReportTruckAndBusDtlsRequest.Zip;
                            PoliceReportTruckAndBusDtlData.UsDot = UpdatePoliceReportTruckAndBusDtlsRequest.UsDot;
                            PoliceReportTruckAndBusDtlData.StateNumber = UpdatePoliceReportTruckAndBusDtlsRequest.StateNumber;
                            PoliceReportTruckAndBusDtlData.IssuingState = UpdatePoliceReportTruckAndBusDtlsRequest.IssuingState;
                            PoliceReportTruckAndBusDtlData.MCMXICC = UpdatePoliceReportTruckAndBusDtlsRequest.MCMXICC;
                            PoliceReportTruckAndBusDtlData.InterState = UpdatePoliceReportTruckAndBusDtlsRequest.InterState;
                            PoliceReportTruckAndBusDtlData.CargoBodyType = UpdatePoliceReportTruckAndBusDtlsRequest.CargoBodyType;
                            PoliceReportTruckAndBusDtlData.GVGCWR = UpdatePoliceReportTruckAndBusDtlsRequest.GVGCWR;
                            PoliceReportTruckAndBusDtlData.TrailerReg = UpdatePoliceReportTruckAndBusDtlsRequest.TrailerReg;
                            PoliceReportTruckAndBusDtlData.RegType = UpdatePoliceReportTruckAndBusDtlsRequest.RegType;
                            PoliceReportTruckAndBusDtlData.RegState = UpdatePoliceReportTruckAndBusDtlsRequest.RegState;
                            PoliceReportTruckAndBusDtlData.RegYear = UpdatePoliceReportTruckAndBusDtlsRequest.RegYear;
                            PoliceReportTruckAndBusDtlData.TrailerLength = UpdatePoliceReportTruckAndBusDtlsRequest.TrailerLength;
                            PoliceReportTruckAndBusDtlData.Placard = UpdatePoliceReportTruckAndBusDtlsRequest.Placard;
                            PoliceReportTruckAndBusDtlData.Material1 = UpdatePoliceReportTruckAndBusDtlsRequest.Material1;
                            PoliceReportTruckAndBusDtlData.MaterialName = UpdatePoliceReportTruckAndBusDtlsRequest.MaterialName;
                            PoliceReportTruckAndBusDtlData.MaterialDigit = UpdatePoliceReportTruckAndBusDtlsRequest.MaterialDigit;
                            PoliceReportTruckAndBusDtlData.ReleaseCode = UpdatePoliceReportTruckAndBusDtlsRequest.ReleaseCode;
                            PoliceReportTruckAndBusDtlData.OfficerName = UpdatePoliceReportTruckAndBusDtlsRequest.OfficerName;
                            PoliceReportTruckAndBusDtlData.IDBadge = UpdatePoliceReportTruckAndBusDtlsRequest.IDBadge;
                            PoliceReportTruckAndBusDtlData.Department = UpdatePoliceReportTruckAndBusDtlsRequest.Department;
                            PoliceReportTruckAndBusDtlData.PrecinctBarracks = UpdatePoliceReportTruckAndBusDtlsRequest.PrecinctBarracks;
                            PoliceReportTruckAndBusDtlData.Date = UpdatePoliceReportTruckAndBusDtlsRequest.Date;
                            _tonTowDbContext.PoliceReportTruckAndBusDtls.UpdateRange(PoliceReportTruckAndBusDtlData);
                        }
                        else if (UpdatePoliceReportTruckAndBusDtlsRequest != null && UpdatePoliceReportTruckAndBusDtlsRequest.Id==0)
                        {
                            PoliceReportTruckAndBusDtls PoliceReportTruckAndBusDtlAddData = new PoliceReportTruckAndBusDtls();
                            PoliceReportTruckAndBusDtlAddData.TonTowRptId = UpdatePoliceReportRequest.TonTowRptId;
                            PoliceReportTruckAndBusDtlAddData.VehicleNo = UpdatePoliceReportTruckAndBusDtlsRequest.VehicleNo;
                            PoliceReportTruckAndBusDtlAddData.Registration = UpdatePoliceReportTruckAndBusDtlsRequest.Registration;
                            PoliceReportTruckAndBusDtlAddData.CarrierName = UpdatePoliceReportTruckAndBusDtlsRequest.CarrierName;
                            PoliceReportTruckAndBusDtlAddData.BusUse = UpdatePoliceReportTruckAndBusDtlsRequest.BusUse;
                            PoliceReportTruckAndBusDtlAddData.Address = UpdatePoliceReportTruckAndBusDtlsRequest.Address;
                            PoliceReportTruckAndBusDtlAddData.City = UpdatePoliceReportTruckAndBusDtlsRequest.City;
                            PoliceReportTruckAndBusDtlAddData.St = UpdatePoliceReportTruckAndBusDtlsRequest.St;
                            PoliceReportTruckAndBusDtlAddData.Zip = UpdatePoliceReportTruckAndBusDtlsRequest.Zip;
                            PoliceReportTruckAndBusDtlAddData.UsDot = UpdatePoliceReportTruckAndBusDtlsRequest.UsDot;
                            PoliceReportTruckAndBusDtlAddData.StateNumber = UpdatePoliceReportTruckAndBusDtlsRequest.StateNumber;
                            PoliceReportTruckAndBusDtlAddData.IssuingState = UpdatePoliceReportTruckAndBusDtlsRequest.IssuingState;
                            PoliceReportTruckAndBusDtlAddData.MCMXICC = UpdatePoliceReportTruckAndBusDtlsRequest.MCMXICC;
                            PoliceReportTruckAndBusDtlAddData.InterState = UpdatePoliceReportTruckAndBusDtlsRequest.InterState;
                            PoliceReportTruckAndBusDtlAddData.CargoBodyType = UpdatePoliceReportTruckAndBusDtlsRequest.CargoBodyType;
                            PoliceReportTruckAndBusDtlAddData.GVGCWR = UpdatePoliceReportTruckAndBusDtlsRequest.GVGCWR;
                            PoliceReportTruckAndBusDtlAddData.TrailerReg = UpdatePoliceReportTruckAndBusDtlsRequest.TrailerReg;
                            PoliceReportTruckAndBusDtlAddData.RegType = UpdatePoliceReportTruckAndBusDtlsRequest.RegType;
                            PoliceReportTruckAndBusDtlAddData.RegState = UpdatePoliceReportTruckAndBusDtlsRequest.RegState;
                            PoliceReportTruckAndBusDtlAddData.RegYear = UpdatePoliceReportTruckAndBusDtlsRequest.RegYear;
                            PoliceReportTruckAndBusDtlAddData.TrailerLength = UpdatePoliceReportTruckAndBusDtlsRequest.TrailerLength;
                            PoliceReportTruckAndBusDtlAddData.Placard = UpdatePoliceReportTruckAndBusDtlsRequest.Placard;
                            PoliceReportTruckAndBusDtlAddData.Material1 = UpdatePoliceReportTruckAndBusDtlsRequest.Material1;
                            PoliceReportTruckAndBusDtlAddData.MaterialName = UpdatePoliceReportTruckAndBusDtlsRequest.MaterialName;
                            PoliceReportTruckAndBusDtlAddData.MaterialDigit = UpdatePoliceReportTruckAndBusDtlsRequest.MaterialDigit;
                            PoliceReportTruckAndBusDtlAddData.ReleaseCode = UpdatePoliceReportTruckAndBusDtlsRequest.ReleaseCode;
                            PoliceReportTruckAndBusDtlAddData.OfficerName = UpdatePoliceReportTruckAndBusDtlsRequest.OfficerName;
                            PoliceReportTruckAndBusDtlAddData.IDBadge = UpdatePoliceReportTruckAndBusDtlsRequest.IDBadge;
                            PoliceReportTruckAndBusDtlAddData.Department = UpdatePoliceReportTruckAndBusDtlsRequest.Department;
                            PoliceReportTruckAndBusDtlAddData.PrecinctBarracks = UpdatePoliceReportTruckAndBusDtlsRequest.PrecinctBarracks;
                            PoliceReportTruckAndBusDtlAddData.Date = UpdatePoliceReportTruckAndBusDtlsRequest.Date;
                            await _tonTowDbContext.PoliceReportTruckAndBusDtls.AddAsync(PoliceReportTruckAndBusDtlAddData);
                        }
                    }
                    #endregion

                    #region Police Report General

                    var PoliceReportGeneralData = _tonTowDbContext.PoliceReportGeneral.SingleOrDefault(
                        p => p.Id == UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.Id
                        );
                    if (PoliceReportGeneralData != null)
                    {
                        PoliceReportGeneralData.AccidentDate = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.AccidentDate;
                        PoliceReportGeneralData.AccidentTime = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.AccidentTime;
                        PoliceReportGeneralData.ReportingOfficer = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.ReportingOfficer;
                        PoliceReportGeneralData.Location = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.Location;
                        PoliceReportGeneralData.City = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.City;
                        PoliceReportGeneralData.State = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.State;
                        PoliceReportGeneralData.Zip = UpdatePoliceReportRequest.UpdatePoliceReportGeneralRequest.Zip;
                        _tonTowDbContext.PoliceReportGeneral.UpdateRange(PoliceReportGeneralData);
                    }
                    #endregion

                    #region Police Report OperatorOwnerVehicleDtls


                    foreach (UpdatePoliceReportOperatorOwnerVehicleDtlsRequest UpdatePoliceReportOperatorOwnerVehicleDtlsRequest in UpdatePoliceReportRequest.UpdatePoliceReportOperatorOwnerVehicleDtlsRequest)
                    {
                        var PoliceReportOperatorOwnerVehicleDtlsData = _tonTowDbContext.PoliceReportOperatorOwnerVehicleDtls.SingleOrDefault(
                       p => p.Id == UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.Id
                       );
                        if (PoliceReportOperatorOwnerVehicleDtlsData != null)
                        {
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorLastName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorLastName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorFirstName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorFirstName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorMiddleName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorMiddleName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorSuffixName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorSuffixName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorVeh = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorVeh;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorInjured = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorInjured;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorFatality = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorFatality;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreet = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreet;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreetSuffix = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreetSuffix;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorStreetApt = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStreetApt;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorCity = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorCity;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorState = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorState;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorZip = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorZip;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorDOB = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorDOB;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorHomePhone = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorHomePhone;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorWorkPhone = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorWorkPhone;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorLic = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorLic;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorStateNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorStateNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorInsuranceComp = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorInsuranceComp;
                            PoliceReportOperatorOwnerVehicleDtlsData.OperatorPolicyNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OperatorPolicyNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerLastName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerLastName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerFirstName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerFirstName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerMiddleName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerMiddleName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerSuffixName = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerSuffixName;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerHomePhone = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerHomePhone;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerWorkPhone = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerWorkPhone;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreet = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreet;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreetSuffix = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreetSuffix;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerStreetApt = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerStreetApt;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerCity = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerCity;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerState = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerState;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerZip = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerZip;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerInsuranceComp = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerInsuranceComp;
                            PoliceReportOperatorOwnerVehicleDtlsData.OwnerPolicyNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.OwnerPolicyNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehYear = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehYear;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehMake = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehMake;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehModel = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehModel;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehVin = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehVin;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehReg = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehReg;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehStateNumber = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehStateNumber;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehTowedBy = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehTowedBy;
                            PoliceReportOperatorOwnerVehicleDtlsData.VehTowedTo = UpdatePoliceReportOperatorOwnerVehicleDtlsRequest.VehTowedTo;
                            _tonTowDbContext.PoliceReportOperatorOwnerVehicleDtls.UpdateRange(PoliceReportOperatorOwnerVehicleDtlsData);
                        }
                    }
                    #endregion

                    #region TonTowUser
                        var userLoginInfo = _tonTowDbContext.TonTowUser.SingleOrDefault(
                         p => p.TonTowRptId == UpdatePoliceReportRequest.TonTowRptId
                         );
                        if (userLoginInfo != null)
                        {
                            userLoginInfo.EmailId = UpdatePoliceReportRequest.EmailId;
                            userLoginInfo.Phone = UpdatePoliceReportRequest.Phone;
                            _tonTowDbContext.TonTowUser.UpdateRange(userLoginInfo);
                        }
                    #endregion


                    await _tonTowDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok(new { Result = "Success" });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Ok(new { Result = "Failed" });

                   throw ex;
                }
            }
            return Ok(new { Result = "Success" });
        }
       
        [HttpPost("DeletePoliceReport"), Authorize(Roles = "A")]
        public async Task<IActionResult> DeletePoliceReport([FromBody] DeletePoliceReportRequest DeletePoliceReportRequest)
        {
            if(DeletePoliceReportRequest.TonTowRptId != 0)
            {
                var UpdatePoliceReportData = _tonTowDbContext.PoliceReport.SingleOrDefault(
                p => p.TonTowRptId == DeletePoliceReportRequest.TonTowRptId
                );
                if (UpdatePoliceReportData != null)
                {
                    UpdatePoliceReportData.Status = false;
                    UpdatePoliceReportData.ModifiedBy = DeletePoliceReportRequest.ModifiedBy;
                    UpdatePoliceReportData.ModifiedDate = DateTime.Now;
                    _tonTowDbContext.PoliceReport.UpdateRange(UpdatePoliceReportData);
                }
            }           
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(DeletePoliceReportRequest);
        }

        [HttpGet("GetTonTowPoliceReportDropDown"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetTonTowPoliceReportDropDown()
        {
            var PoliceReport = from policeReport in _tonTowDbContext.PoliceReport
                               where policeReport.Status == true orderby policeReport.TonTowRptId descending
                               select new
                               {
                                   policeReport.TonTowRptId,
                                   policeReport.JobNum
                               };
            return Ok(PoliceReport);
        }

        #region Delete Police report child tables

            [HttpPost("DeletePoliceReportOperatorDtls"), Authorize(Roles = "A")]
            public async Task<IActionResult> DeletePoliceReportOperatorDtls(int Id)
            {
                if (Id != 0)
                {
                var DeletePoliceReportOperatorData = _tonTowDbContext.PoliceReportOperatorDtls.SingleOrDefault(
                    p => p.Id == Id
                    );
                    if (DeletePoliceReportOperatorData != null)
                    {
                        _tonTowDbContext.PoliceReportOperatorDtls.Remove(DeletePoliceReportOperatorData);
                    }
                }
                await _tonTowDbContext.SaveChangesAsync();
                return Ok(new { Result = "Success" });
            }

            [HttpPost("DeletePoliceReportWitnessDtls"), Authorize(Roles = "A")]
            public async Task<IActionResult> DeletePoliceReportWitnessDtls(int Id)
            {
                if (Id != 0)
                {
                    var DeletePoliceReportWitnessData = _tonTowDbContext.PoliceReportWitness.SingleOrDefault(
                        p => p.Id == Id
                        );
                    if (DeletePoliceReportWitnessData != null)
                    {
                        _tonTowDbContext.PoliceReportWitness.Remove(DeletePoliceReportWitnessData);
                    }
                }
                await _tonTowDbContext.SaveChangesAsync();
                return Ok(new { Result = "Success" });
            }

            [HttpPost("DeletePoliceReportPropertyDamageDtls"), Authorize(Roles = "A")]
            public async Task<IActionResult> DeletePoliceReportPropertyDamageDtls(int Id)
            {
                if (Id != 0)
                {
                    var DeletePoliceReportPropertyDamageData = _tonTowDbContext.PoliceReportPropertyDamage.SingleOrDefault(
                        p => p.Id == Id
                        );
                    if (DeletePoliceReportPropertyDamageData != null)
                    {
                        _tonTowDbContext.PoliceReportPropertyDamage.Remove(DeletePoliceReportPropertyDamageData);
                    }
                }
                await _tonTowDbContext.SaveChangesAsync();
                return Ok(new { Result = "Success" });
            }
        #endregion

        [HttpGet("GetTonTowPoliceReportStatus")]
        public async Task<IActionResult> GetTonTowPoliceReportStatus([FromQuery] int TonTowRptId)
        {
            var PoliceReport = from PR in _tonTowDbContext.PoliceReport
                               join FC in _tonTowDbContext.FileClaims
                      on PR.TonTowRptId equals FC.TonTowRptId into PRFC
                               from FCSubSet in PRFC.DefaultIfEmpty()
                               join Adj in _tonTowDbContext.Adjuster
                      on PR.TonTowRptId equals Adj.TonTowRptId into PRAdj
                               from AdjSubSet in PRAdj.DefaultIfEmpty()
                               join Appr in _tonTowDbContext.Appraiser
                      on PR.TonTowRptId equals Appr.TonTowRptId into PRAppr
                               from ApprSubSet in PRAppr.DefaultIfEmpty()
                               join CI in _tonTowDbContext.CustInsuranceCompUpdate
                      on PR.TonTowRptId equals CI.TonTowRptId into PRCI
                               from CISubSet in PRCI.DefaultIfEmpty()
                               join CP in _tonTowDbContext.CustomerPaymentDtl
                      on PR.TonTowRptId equals CP.TonTowRptId into PRCP
                               from CPSubSet in PRCP.DefaultIfEmpty()
                               where PR.TonTowRptId == TonTowRptId
                               select new
                               {
                                   PR.TonTowRptId,
                                   PR.JobNum,
                                   //Status
                                   FileClaimStatus = (bool?)FCSubSet.Status,
                                   AdjStatus = (bool?)AdjSubSet.Status,
                                   AppStatus = (bool?)ApprSubSet.Status,
                                   CIStatus = (bool?)CISubSet.Status,
                                   CPStatus = (bool?)CPSubSet.Status,                                  
                                   //FileClaim
                                   FCSubSet.FileClaimNumber,
                                   //Adjuster
                                   AdjName = AdjSubSet.AdjusterName,
                                   AdjAppraisedDate = (DateTime?) AdjSubSet.AppraisedDate,
                                   AdjCompanyName = AdjSubSet.CompanyName,
                                   AdjClaim = AdjSubSet.Claim,
                                   AdjContactAddress = AdjSubSet.ContactAddress,
                                   AdjContactPhone = AdjSubSet.ContactPhone,
                                   //Appraiser
                                   AppName = ApprSubSet.AppraiserName,
                                   AppAppraisedDate = (DateTime?)ApprSubSet.AppraisedDate,
                                   AppCompanyName = ApprSubSet.CompanyName,
                                   AppClaim = ApprSubSet.Claim,
                                   AppContactAddress = ApprSubSet.ContactAddress,
                                   AppContactPhone = ApprSubSet.ContactPhone,
                                   AppVehCondition = ApprSubSet.VehicleCondition,
                                   //Customer Insurance Status
                                   CIVehCondition = CISubSet.VehicleCondition,
                                   CIRepairableNotes1 = CISubSet.RepairableNotes1,
                                   CIRepairableNotes2 = CISubSet.RepairableNotes2,
                                   CITotaledNotes = CISubSet.TotaledNotes,
                                   //Customer Payment Details
                                   CPPaymentType = CPSubSet.PaymentType,
                                   CPFullPaymentAmt = CPSubSet.FullPaymentAmt,
                                   CPFullPaymentDate = CPSubSet.FullPaymentDate,
                                   CPFullPaymentType = CPSubSet.PaymentType,
                                   CPFullPaymentCardDtls = CPSubSet.FullPaymentCardDtls,
                                   CPFullPaymentInvNum = CPSubSet.FullPaymentInvNum,
                                   CPPartialPayment1Amt = CPSubSet.PartialPayment1Amt,
                                   CPPartialPayment1Date = CPSubSet.PartialPayment1Date,
                                   CPPartialPayment1Type = CPSubSet.PartialPayment1Type,
                                   CPPartialPayment1CardDtls = CPSubSet.ParitalPayment1CardDtls,
                                   CPPartialPayment1InvNum = CPSubSet.ParitalPayment1InvNum,
                                   CPPartialPayment2Amt = CPSubSet.PartialPayment2Amt,
                                   CPPartialPayment2Date = CPSubSet.PartialPayment2Date,
                                   CPPartialPayment2Type = CPSubSet.PartialPayment2Type,
                                   CPPartialPayment2CardDtls = CPSubSet.ParitalPayment2CardDtls,
                                   CPPartialPayment2InvNum = CPSubSet.ParitalPayment2InvNum,
                                   CPPartialPayment3Amt = CPSubSet.PartialPayment3Amt,
                                   CPPartialPayment3Date = CPSubSet.PartialPayment3Date,
                                   CPPartialPayment3Type = CPSubSet.PartialPayment3Type,
                                   CPPartialPayment3CardDtls = CPSubSet.ParitalPayment3CardDtls,
                                   CPPartialPayment3InvNum = CPSubSet.ParitalPayment3InvNum,
                                   CPPartialPayment4Amt = CPSubSet.PartialPayment4Amt,
                                   CPPartialPayment4Date = CPSubSet.PartialPayment4Date,
                                   CPPartialPayment4Type = CPSubSet.PartialPayment4Type,
                                   CPPartialPayment4CardDtls = CPSubSet.ParitalPayment4CardDtls,
                                   CPPartialPayment4InvNum = CPSubSet.ParitalPayment4InvNum,
                                   CPPartialPayment5Amt = CPSubSet.PartialPayment5Amt,
                                   CPPartialPayment5Date = CPSubSet.PartialPayment5Date,
                                   CPPartialPayment5Type = CPSubSet.PartialPayment5Type,
                                   CPPartialPayment5CardDtls = CPSubSet.ParitalPayment5CardDtls,
                                   CPPartialPayment5InvNum = CPSubSet.ParitalPayment5InvNum,
                                   CPPartialPayment6Amt = CPSubSet.PartialPayment6Amt,
                                   CPPartialPayment6Date = CPSubSet.PartialPayment6Date,
                                   CPPartialPayment6Type = CPSubSet.PartialPayment6Type,
                                   CPPartialPayment6CardDtls = CPSubSet.ParitalPayment6CardDtls,
                                   CPPartialPayment6InvNum = CPSubSet.ParitalPayment6InvNum
                               };
            return Ok(PoliceReport);
        }

        [HttpGet("GetPoliceReportEmailPhone"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetPoliceReportEmailPhone(int TonTowRptId)
        {
            var user = from TU in _tonTowDbContext.TonTowUser
                       where TU.TonTowRptId == TonTowRptId
                       select new
                       {
                           Email = TU.EmailId,
                           Phone = TU.Phone
                       };
            return Ok(user);
        }

        [HttpPost("registeruser"), Authorize(Roles = "A")]
        public async Task<ActionResult<TonTowUser>> RegisterUser(LoginRequest request)
        {
            var mailId = _configuration.GetSection("MailSettings:MailId").Value;
            var phone = _configuration.GetSection("MailSettings:Phone").Value;
            TonTowUser userLoginInfo = new TonTowUser();
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userLoginInfo.Username = request.Username;
            userLoginInfo.PasswordHash = passwordHash;
            userLoginInfo.PasswordSalt = passwordSalt;
            userLoginInfo.Role = "A";
            userLoginInfo.EmailId = mailId;
            userLoginInfo.Phone = phone;
            userLoginInfo.RefreshToken = CreateToken(userLoginInfo);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            userLoginInfo.TokenCreated = refreshToken.Created;
            userLoginInfo.TokenExpires = refreshToken.Expires;
            await _tonTowDbContext.TonTowUser.AddAsync(userLoginInfo);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(request);
        }

        #region Create User Tokens
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
            private string CreateToken(TonTowUser user)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }

            private RefreshToken GenerateRefreshToken()
            {
                var refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expires = DateTime.Now.AddDays(7),
                    Created = DateTime.Now
                };

                return refreshToken;
            }

            private void SetRefreshToken(RefreshToken newRefreshToken)
            {
                TonTowUser userLoginInfo = new TonTowUser();
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.Expires
                };
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

                userLoginInfo.RefreshToken = newRefreshToken.Token;
                userLoginInfo.TokenCreated = newRefreshToken.Created;
                userLoginInfo.TokenExpires = newRefreshToken.Expires;
            }
        #endregion
    }
}
