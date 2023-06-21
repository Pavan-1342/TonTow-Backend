using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TonTow.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjuster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    AdjusterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppraisedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Claim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjuster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appraiser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    AppraiserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppraisedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Claim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehicleCondition = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraiser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustInsuranceCompUpdate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    VehicleCondition = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    RepairableNotes1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RepairableNotes2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotaledNotes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustInsuranceCompUpdate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPaymentDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullPaymentAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FullPaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FullPaymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullPaymentCardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullPaymentInvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment1Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment1Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment1Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment1CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment1InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment2Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment2Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment2Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment2CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment2InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment3Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment3Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment3Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment3CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment3InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment4Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment4Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment4Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment4CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment4InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment5Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment5Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment5Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment5CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment5InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PartialPayment6Amt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartialPayment6Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPayment6Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParitalPayment6CardDtls = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParitalPayment6InvNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPaymentDtls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    FileClaimNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReport",
                columns: table => new
                {
                    TonTowRptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfCrash = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeOfCrash = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CityTown = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleNumber = table.Column<int>(type: "int", nullable: false),
                    InjuredNumber = table.Column<int>(type: "int", nullable: false),
                    SpeedLimit = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StatePolice = table.Column<bool>(type: "bit", nullable: true),
                    LocalPolice = table.Column<bool>(type: "bit", nullable: true),
                    MBTAPolice = table.Column<bool>(type: "bit", nullable: true),
                    CampusPolice = table.Column<bool>(type: "bit", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AtIntersection = table.Column<bool>(type: "bit", nullable: false),
                    AIRoute1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIDirection1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIRoadwayStName1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AIRoute2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIDirection2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIRoadwayStName2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AIRoute3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIDirection3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AIRoadwayStName3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAIRoute = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NAIDirection = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NAIAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAIRoadwayStName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAIFeet1 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIDirection1 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIMile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIExitNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    NAIFeet2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIDirection2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIRoute1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NAIRoadwaySt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAIFeet3 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAIDirection3 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NAILandmark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CrashReportId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CrashNarrative = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReport", x => x.TonTowRptId);
                });

            migrationBuilder.CreateTable(
                name: "TonTowFileUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonTowFileUpload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TonTowUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonTowUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportGeneral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    AccidentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AccidentTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReportingOfficer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportGeneral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportGeneral_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportOperatorDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    VehicleNo = table.Column<int>(type: "int", nullable: false),
                    OperatorLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SeatPosition = table.Column<int>(type: "int", nullable: false),
                    SafetySystem = table.Column<int>(type: "int", nullable: false),
                    AirbagStatus = table.Column<int>(type: "int", nullable: false),
                    EjectCode = table.Column<int>(type: "int", nullable: false),
                    TrapCode = table.Column<int>(type: "int", nullable: false),
                    InjuryStatus = table.Column<int>(type: "int", nullable: false),
                    TranspCode = table.Column<int>(type: "int", nullable: false),
                    MedicalFacility = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportOperatorDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportOperatorDtls_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportOperatorOwnerVehicleDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    OperatorLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorSuffixName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorVeh = table.Column<int>(type: "int", nullable: false),
                    OperatorInjured = table.Column<bool>(type: "bit", nullable: false),
                    OperatorFatality = table.Column<bool>(type: "bit", nullable: false),
                    OperatorNumber = table.Column<int>(type: "int", nullable: false),
                    OperatorStreet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OperatorStreetSuffix = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorStreetApt = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorCity = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorState = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorZip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorDOB = table.Column<DateTime>(type: "datetime", nullable: false),
                    OperatorHomePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorWorkPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorLic = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorStateNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorInsuranceComp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperatorPolicyNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerSuffixName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerHomePhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerWorkPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerNumber = table.Column<int>(type: "int", nullable: false),
                    OwnerStreet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OwnerStreetSuffix = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerStreetApt = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerCity = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerState = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerZip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerInsuranceComp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerPolicyNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VehYear = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehMake = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehModel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehVin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VehReg = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehStateNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehTowedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehTowedTo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportOperatorOwnerVehicleDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportOperatorOwnerVehicleDtls_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportPropertyDamage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FourOneType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportPropertyDamage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportPropertyDamage_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportTruckAndBusDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    VehicleNo = table.Column<int>(type: "int", nullable: false),
                    Registration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CarrierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusUse = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    St = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UsDot = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IssuingState = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MCMXICC = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InterState = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CargoBodyType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GVGCWR = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrailerReg = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegState = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegYear = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrailerLength = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Placard = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Material1 = table.Column<int>(type: "int", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaterialDigit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReleaseCode = table.Column<int>(type: "int", nullable: false),
                    OfficerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IDBadge = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrecinctBarracks = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportTruckAndBusDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportTruckAndBusDtls_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportVehicleDtls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    VehicleNo = table.Column<int>(type: "int", nullable: false),
                    CrashType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Occupants = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DOBAge = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    LicClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicRestrictions = table.Column<int>(type: "int", nullable: true),
                    CDLEndorsement = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OperatorLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OperatorFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperatorAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OperatorCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OperatorState = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OperatorZip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    InsuranceCompany = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleTravelDirection = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RespondingToEmergency = table.Column<int>(type: "int", nullable: false),
                    CitationIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Viol1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Viol2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Viol3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Viol4 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Reg = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RegState = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VehicleMake = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleConfig = table.Column<int>(type: "int", nullable: true),
                    OwnerLastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerFirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerMiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OwnerAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OwnerState = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OwnerZip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    VehicleActionPriortoCrash = table.Column<int>(type: "int", nullable: true),
                    EventSequence1 = table.Column<int>(type: "int", nullable: true),
                    EventSequence2 = table.Column<int>(type: "int", nullable: true),
                    EventSequence3 = table.Column<int>(type: "int", nullable: true),
                    EventSequence4 = table.Column<int>(type: "int", nullable: true),
                    MostHarmfulEvent = table.Column<int>(type: "int", nullable: true),
                    DriverContributingCode1 = table.Column<int>(type: "int", nullable: true),
                    DriverContributingCode2 = table.Column<int>(type: "int", nullable: true),
                    DriverDistractedBy = table.Column<int>(type: "int", nullable: true),
                    DamagedAreaCode1 = table.Column<int>(type: "int", nullable: true),
                    DamagedAreaCode2 = table.Column<int>(type: "int", nullable: true),
                    DamagedAreaCode3 = table.Column<int>(type: "int", nullable: true),
                    TestStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TypeofTest = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BacTestResult = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SuspectedAlcohol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SuspectedDrug = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TowedFromScene = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportVehicleDtls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportVehicleDtls_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReportWitness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonTowRptId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReportWitness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReportWitness_PoliceReport_TonTowRptId",
                        column: x => x.TonTowRptId,
                        principalTable: "PoliceReport",
                        principalColumn: "TonTowRptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjuster_TonTowRptId",
                table: "Adjuster",
                column: "TonTowRptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appraiser_TonTowRptId",
                table: "Appraiser",
                column: "TonTowRptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustInsuranceCompUpdate_TonTowRptId",
                table: "CustInsuranceCompUpdate",
                column: "TonTowRptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDtls_TonTowRptId",
                table: "CustomerPaymentDtls",
                column: "TonTowRptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileClaims_TonTowRptId",
                table: "FileClaims",
                column: "TonTowRptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportGeneral_TonTowRptId",
                table: "PoliceReportGeneral",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportOperatorDtls_TonTowRptId",
                table: "PoliceReportOperatorDtls",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportOperatorOwnerVehicleDtls_TonTowRptId",
                table: "PoliceReportOperatorOwnerVehicleDtls",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportPropertyDamage_TonTowRptId",
                table: "PoliceReportPropertyDamage",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportTruckAndBusDtls_TonTowRptId",
                table: "PoliceReportTruckAndBusDtls",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportVehicleDtls_TonTowRptId",
                table: "PoliceReportVehicleDtls",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReportWitness_TonTowRptId",
                table: "PoliceReportWitness",
                column: "TonTowRptId");

            migrationBuilder.CreateIndex(
                name: "IX_TonTowUser_Username",
                table: "TonTowUser",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjuster");

            migrationBuilder.DropTable(
                name: "Appraiser");

            migrationBuilder.DropTable(
                name: "CustInsuranceCompUpdate");

            migrationBuilder.DropTable(
                name: "CustomerPaymentDtls");

            migrationBuilder.DropTable(
                name: "FileClaims");

            migrationBuilder.DropTable(
                name: "PoliceReportGeneral");

            migrationBuilder.DropTable(
                name: "PoliceReportOperatorDtls");

            migrationBuilder.DropTable(
                name: "PoliceReportOperatorOwnerVehicleDtls");

            migrationBuilder.DropTable(
                name: "PoliceReportPropertyDamage");

            migrationBuilder.DropTable(
                name: "PoliceReportTruckAndBusDtls");

            migrationBuilder.DropTable(
                name: "PoliceReportVehicleDtls");

            migrationBuilder.DropTable(
                name: "PoliceReportWitness");

            migrationBuilder.DropTable(
                name: "TonTowFileUpload");

            migrationBuilder.DropTable(
                name: "TonTowUser");

            migrationBuilder.DropTable(
                name: "PoliceReport");
        }
    }
}
