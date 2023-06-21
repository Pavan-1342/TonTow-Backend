using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TonTow.API.Models
{
    #region PoliceReport Model
    public class PoliceReport
    {
        [Key]
        public int TonTowRptId { get; set; }
        [StringLength(50)]
        public string JobNum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateOfCrash { get; set; }
        [StringLength(10)]
        public string TimeOfCrash { get; set; }
        [StringLength(20)]
        public string CityTown { get; set; }
        public int VehicleNumber { get; set; }
        public int InjuredNumber { get; set; }
        public int? SpeedLimit { get; set; }
        [StringLength(20)]
        public string? Latitude { get; set; }
        [StringLength(20)]
        public string? Longitude { get; set; }
        public bool? StatePolice { get; set; }
        public bool? LocalPolice { get; set; }
        public bool? MBTAPolice { get; set; }
        public bool? CampusPolice { get; set; }
        [StringLength(20)]
        public string? Other { get; set; }
        public bool AtIntersection { get; set; }
        [StringLength(30)]
        public string? AIRoute1 { get; set; }
        [StringLength(30)]
        public string? AIDirection1 { get; set; }
        [StringLength(50)]
        public string? AIRoadwayStName1 { get; set; }
        [StringLength(30)]
        public string? AIRoute2 { get; set; }
        [StringLength(30)]
        public string? AIDirection2 { get; set; }
        [StringLength(50)]
        public string? AIRoadwayStName2 { get; set; }
        [StringLength(30)]
        public string? AIRoute3 { get; set; }
        [StringLength(30)]
        public string? AIDirection3 { get; set; }
        [StringLength(50)]
        public string? AIRoadwayStName3 { get; set; }
        [StringLength(30)]
        public string? NAIRoute { get; set; }
        [StringLength(30)]
        public string? NAIDirection { get; set; }
        [StringLength(50)]
        public string? NAIAddress { get; set; }
        [StringLength(50)]
        public string? NAIRoadwayStName { get; set; }
        [StringLength(10)]
        public string? NAIFeet1 { get; set; }
        [StringLength(10)]
        public string? NAIDirection1 { get; set; }
        [StringLength(10)]
        public string? NAIMile { get; set; }
        [StringLength(15)]
        public string? NAIExitNo { get; set; }
        [StringLength(10)]
        public string? NAIFeet2 { get; set; }
        [StringLength(10)]
        public string? NAIDirection2 { get; set; }
        [StringLength(30)]
        public string? NAIRoute1 { get; set; }
        [StringLength(50)]
        public string? NAIRoadwaySt { get; set; }
        [StringLength(10)]
        public string? NAIFeet3 { get; set; }
        [StringLength(10)]
        public string? NAIDirection3 { get; set; }
        [StringLength(50)]
        public string? NAILandmark { get; set; }
        [StringLength(15)]
        public string CrashReportId { get; set; }
        [StringLength(250)]
        public string CrashNarrative { get; set; }
        public bool Status { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public ICollection<PoliceReportVehicleDtls> PoliceReportVehicleDtl { get; set; }
        public ICollection<PoliceReportOperatorDtls> PoliceReportOperatorDtls { get; set; }
        public ICollection<PoliceReportWitness> PoliceReportWitness { get; set; }
        public ICollection<PoliceReportPropertyDamage> PoliceReportPropertyDamage { get; set; }
        public ICollection<PoliceReportTruckAndBusDtls> PoliceReportTruckAndBusDtl { get; set; }
        public ICollection<PoliceReportGeneral> PoliceReportGeneral { get; set; }
        public ICollection<PoliceReportOperatorOwnerVehicleDtls> PoliceReportOperatorOwnerVehicleDtls { get; set; }
    }
    public class PoliceReportVehicleDtls
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        public int VehicleNo { get; set; }
        [StringLength(15)]
        public string CrashType { get; set; }
        [StringLength(15)]
        public string? Type { get; set; }
        [StringLength(15)]
        public string? Action { get; set; }
        [StringLength(20)]
        public string? Location { get; set; }
        [StringLength(20)]
        public string? Condition { get; set; }
        public int Occupants { get; set; }
        [StringLength(20)]
        public string License { get; set; }
        [StringLength(20)]
        public string? Street { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DOBAge { get; set; }
        [StringLength(2)]
        public string? Sex { get; set; }
        public string? LicClass { get; set; }
        public int? LicRestrictions { get; set; }
        [StringLength(20)]
        public string? CDLEndorsement { get; set; }
        [StringLength(15)]
        public string OperatorLastName { get; set; }
        [StringLength(15)]
        public string? OperatorFirstName { get; set; }
        [StringLength(15)]
        public string? OperatorMiddleName { get; set; }
        [StringLength(50)]
        public string OperatorAddress { get; set; }
        [StringLength(20)]
        public string OperatorCity { get; set; }
        [StringLength(20)]
        public string OperatorState { get; set; }
        [StringLength(15)]
        public string OperatorZip { get; set; }
        [StringLength(50)]
        public string InsuranceCompany { get; set; }
        [StringLength(10)]
        public string? VehicleTravelDirection { get; set; }
        public int RespondingToEmergency { get; set; }
        public string? CitationIssued { get; set; }
        [StringLength(20)]
        public string? Viol1 { get; set; }
        [StringLength(20)]
        public string? Viol2 { get; set; }
        [StringLength(20)]
        public string? Viol3 { get; set; }
        [StringLength(20)]
        public string? Viol4 { get; set; }
        [StringLength(20)]
        public string Reg { get; set; }
        [StringLength(10)]
        public string RegType { get; set; }
        [StringLength(20)]
        public string RegState { get; set; }
        [StringLength(10)]
        public string VehicleYear { get; set; }
        [StringLength(20)]
        public string VehicleMake { get; set; }
        public int? VehicleConfig { get; set; }
        [StringLength(15)]
        public string OwnerLastName { get; set; }
        [StringLength(15)]
        public string? OwnerFirstName { get; set; }
        [StringLength(15)]
        public string? OwnerMiddleName { get; set; }
        [StringLength(50)]
        public string OwnerAddress { get; set; }
        [StringLength(20)]
        public string OwnerCity { get; set; }
        [StringLength(20)]
        public string OwnerState { get; set; }
        [StringLength(15)]
        public string OwnerZip { get; set; }
        public int? VehicleActionPriortoCrash { get; set; }
        public int? EventSequence1 { get; set; }
        public int? EventSequence2 { get; set; }
        public int? EventSequence3 { get; set; }
        public int? EventSequence4 { get; set; }
        public int? MostHarmfulEvent { get; set; }
        public int? DriverContributingCode1 { get; set; }
        public int? DriverContributingCode2 { get; set; }
        public int? DriverDistractedBy { get; set; }
        public int? DamagedAreaCode1 { get; set; }
        public int? DamagedAreaCode2 { get; set; }
        public int? DamagedAreaCode3 { get; set; }
        [StringLength(10)]
        public string? TestStatus { get; set; }
        [StringLength(10)]
        public string? TypeofTest { get; set; }
        [StringLength(10)]
        public string? BacTestResult { get; set; }
        [StringLength(10)]
        public string? SuspectedAlcohol { get; set; }
        [StringLength(10)]
        public string? SuspectedDrug { get; set; }
        public int? TowedFromScene { get; set; }
    }
    public class PoliceReportOperatorDtls
{
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        public int VehicleNo { get; set; }
        [StringLength(15)]
        public string OperatorLastName { get; set; }
        [StringLength(15)]
        public string? OperatorFirstName { get; set; }
        [StringLength(15)]
        public string? OperatorMiddleName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        [StringLength(15)]
        public string Zip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DOB { get; set; }
        [StringLength(2)]
        public string Sex { get; set; }
        public int SeatPosition { get; set; }
        public int SafetySystem { get; set; }
        public int AirbagStatus { get; set; }
        public int EjectCode { get; set; }
        public int TrapCode { get; set; }
        public int InjuryStatus { get; set; }
        public int TranspCode { get; set; }
        [StringLength(100)]
        public string MedicalFacility { get; set; }
    }
    public class PoliceReportWitness
{
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        [StringLength(15)]
        public string LastName { get; set; }
        [StringLength(15)]
        public string? FirstName { get; set; }
        [StringLength(15)]
        public string? MiddleName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }       
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        [StringLength(20)]
        public string Zip { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Statement { get; set; }
    }
    public class PoliceReportPropertyDamage
{
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        [StringLength(15)]
        public string OwnerLastName { get; set; }
        [StringLength(15)]
        public string? OwnerFirstName { get; set; }
        [StringLength(15)]
        public string? OwnerMiddleName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        [StringLength(20)]
        public string Zip { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string FourOneType { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }
    public class PoliceReportTruckAndBusDtls
{
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        public int VehicleNo { get; set; }
        [StringLength(30)]
        public string Registration { get; set; }
        [StringLength(50)]
        public string CarrierName { get; set; }
        [StringLength(10)]
        public string BusUse { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string St { get; set; }
        [StringLength(15)]
        public string Zip { get; set; }
        [StringLength(30)]
        public string UsDot { get; set; }
        [StringLength(20)]
        public string StateNumber { get; set; }
        [StringLength(15)]
        public string IssuingState { get; set; }
        [StringLength(30)]
        public string MCMXICC { get; set; }
        [StringLength(20)]
        public string InterState { get; set; }
        [StringLength(20)]
        public string CargoBodyType { get; set; }
        [StringLength(20)]
        public string GVGCWR { get; set; }
        [StringLength(30)]
        public string TrailerReg { get; set; }
        [StringLength(30)]
        public string RegType { get; set; }
        [StringLength(30)]
        public string RegState { get; set; }
        [StringLength(30)]
        public string RegYear { get; set; }
        [StringLength(15)]
        public string TrailerLength { get; set; }
        [StringLength(15)]
        public string Placard { get; set; }
        public int Material1 { get; set; }
        [StringLength(30)]
        public string MaterialName { get; set; }
        [StringLength(10)]
        public string MaterialDigit { get; set; }
        public int ReleaseCode { get; set; }
        [StringLength(30)]
        public string OfficerName { get; set; }
        public int IDBadge { get; set; }
        [StringLength(30)]
        public string Department { get; set; }
        [StringLength(30)]
        public string PrecinctBarracks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
    }
    public class PoliceReportGeneral
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AccidentDate { get; set; }
        [StringLength(10)]
        public string AccidentTime { get; set; }
        [StringLength(50)]
        public string ReportingOfficer { get; set; }
        [StringLength(30)]
        public string Location { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        [StringLength(15)]
        public string Zip { get; set; }
    }
    public class PoliceReportOperatorOwnerVehicleDtls
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        public PoliceReport policeReport { get; set; }
        [StringLength(15)]
        public string OperatorLastName { get; set; }
        [StringLength(15)]
        public string? OperatorFirstName { get; set; }
        [StringLength(15)]
        public string? OperatorMiddleName { get; set; }
        [StringLength(15)]
        public string? OperatorSuffixName { get; set; }
        public int OperatorVeh { get; set; }
        public bool OperatorInjured { get; set; }
        public bool OperatorFatality { get; set; }
        public int OperatorNumber { get; set; }
        [StringLength(20)]
        public string OperatorStreet { get; set; }
        [StringLength(15)]
        public string OperatorStreetSuffix { get; set; }
        [StringLength(15)]
        public string OperatorStreetApt { get; set; }
        [StringLength(15)]
        public string OperatorCity { get; set; }
        [StringLength(15)]
        public string OperatorState { get; set; }
        [StringLength(15)]
        public string OperatorZip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OperatorDOB { get; set; }
        [StringLength(15)]
        public string OperatorHomePhone { get; set; }
        [StringLength(15)]
        public string OperatorWorkPhone { get; set; }
        [StringLength(15)]
        public string OperatorLic{ get; set; }
        [StringLength(15)]
        public string OperatorStateNumber { get; set; }
        [StringLength(50)]
        public string OperatorInsuranceComp { get; set; }
        [StringLength(30)]
        public string OperatorPolicyNumber { get; set; }
        [StringLength(15)]
        public string OwnerLastName { get; set; }
        [StringLength(15)]
        public string? OwnerFirstName { get; set; }
        [StringLength(15)]
        public string? OwnerMiddleName { get; set; }
        [StringLength(15)]
        public string? OwnerSuffixName { get; set; }
        [StringLength(15)]
        public string OwnerHomePhone { get; set; }
        [StringLength(15)]
        public string OwnerWorkPhone { get; set; }
        public int OwnerNumber { get; set; }
        [StringLength(20)]
        public string OwnerStreet { get; set; }
        [StringLength(15)]
        public string OwnerStreetSuffix { get; set; }
        [StringLength(15)]
        public string OwnerStreetApt { get; set; }
        [StringLength(15)]
        public string OwnerCity { get; set; }
        [StringLength(15)]
        public string OwnerState { get; set; }
        [StringLength(15)]
        public string OwnerZip { get; set; }
        [StringLength(50)]
        public string OwnerInsuranceComp { get; set; }
        [StringLength(30)]
        public string OwnerPolicyNumber { get; set; }
        [StringLength(15)]
        public string VehYear { get; set; }
        [StringLength(20)]
        public string VehMake { get; set; }
        [StringLength(15)]
        public string VehModel { get; set; }
        [StringLength(30)]
        public string VehVin { get; set; }
        [StringLength(15)]
        public string VehReg { get; set; }
        [StringLength(15)]
        public string VehStateNumber { get; set; }
        [StringLength(20)]
        public string VehTowedBy { get; set; }
        [StringLength(20)]
        public string VehTowedTo { get; set; }
    }
    public class TonTowCrashDropDown
    {
        public int TonTowRptId { get; set; }
        public string JobNum { get; set; }
    }

    #endregion

    #region Add

    public class AddPoliceReportRequest
    {
        public string JobNum { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfCrash { get; set; }
        public string TimeOfCrash { get; set; }
        public string CityTown { get; set; }
        public int VehicleNumber { get; set; }
        public int InjuredNumber { get; set; }
        public int? SpeedLimit { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public bool? StatePolice { get; set; }
        public bool? LocalPolice { get; set; }
        public bool? MBTAPolice { get; set; }
        public bool? CampusPolice { get; set; }
        public string? Other { get; set; }
        public bool AtIntersection { get; set; }
        public string? AIRoute1 { get; set; }
        public string? AIDirection1 { get; set; }
        public string? AIRoadwayStName1 { get; set; }
        public string? AIRoute2 { get; set; }
        public string? AIDirection2 { get; set; }
        public string? AIRoadwayStName2 { get; set; }
        public string? AIRoute3 { get; set; }
        public string? AIDirection3 { get; set; }
        public string? AIRoadwayStName3 { get; set; }
        public string? NAIRoute { get; set; }
        public string? NAIDirection { get; set; }
        public string? NAIAddress { get; set; }
        public string? NAIRoadwayStName { get; set; }
        public string? NAIFeet1 { get; set; }
        public string? NAIDirection1 { get; set; }
        public string? NAIMile { get; set; }
        public string? NAIExitNo { get; set; }
        public string? NAIFeet2 { get; set; }
        public string? NAIDirection2 { get; set; }
        public string? NAIRoute1 { get; set; }
        public string? NAIRoadwaySt { get; set; }
        public string? NAIFeet3 { get; set; }
        public string? NAIDirection3 { get; set; }
        public string? NAILandmark { get; set; }
        public string CrashReportId { get; set; }
        public string CrashNarrative { get; set; }      
        public string CreatedBy { get; set; }
        public  List<AddPoliceReportVehicleDtlsRequest> AddPoliceReportVehicleDtlsRequest { get; set; }
        public  List<AddPoliceReportOperatorDtlsRequest> AddPoliceReportOperatorDtlsRequest { get; set; }        
        public List<AddPoliceReportWitnessRequest> AddPoliceReportWitnessRequest { get; set; }
        public List<AddPoliceReportPropertyDamageRequest> AddPoliceReportPropertyDamageRequest { get; set; }
        public List<AddPoliceReportTruckAndBusDtlsRequest> AddPoliceReportTruckAndBusDtlsRequest { get; set; }
        public AddPoliceReportGeneralRequest AddPoliceReportGeneralRequest { get; set; }
        public List<AddPoliceReportOperatorOwnerVehicleDtlsRequest> AddPoliceReportOperatorOwnerVehicleDtlsRequest { get; set; }
    }
    public class AddPoliceReportVehicleDtlsRequest
    {       
        public int VehicleNo { get; set; }
        public string CrashType { get; set; }
        public string? Type { get; set; }
        public string? Action { get; set; }
        public string? Location { get; set; }
        public string? Condition { get; set; }
        public int Occupants { get; set; }
        public string License { get; set; }
        public string? Street { get; set; }
        public DateTime DOBAge { get; set; }
        public string? Sex { get; set; }
        public string? LicClass { get; set; }
        public int? LicRestrictions { get; set; }
        public string? CDLEndorsement { get; set; }
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string OperatorAddress { get; set; }
        public string OperatorCity { get; set; }
        public string OperatorState { get; set; }
        public string OperatorZip { get; set; }
        public string InsuranceCompany { get; set; }
        public string? VehicleTravelDirection { get; set; }
        public int RespondingToEmergency { get; set; }
        public string? CitationIssued { get; set; }
        public string? Viol1 { get; set; }
        public string? Viol2 { get; set; }
        public string? Viol3 { get; set; }
        public string? Viol4 { get; set; }
        public string Reg { get; set; }
        public string RegType { get; set; }
        public string RegState { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public int? VehicleConfig { get; set; }
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public int? VehicleActionPriortoCrash { get; set; }
        public int? EventSequence1 { get; set; }
        public int? EventSequence2 { get; set; }
        public int? EventSequence3 { get; set; }
        public int? EventSequence4 { get; set; }
        public int? MostHarmfulEvent { get; set; }
        public int? DriverContributingCode1 { get; set; }
        public int? DriverContributingCode2 { get; set; }
        public int? DriverDistractedBy { get; set; }
        public int? DamagedAreaCode1 { get; set; }
        public int? DamagedAreaCode2 { get; set; }
        public int? DamagedAreaCode3 { get; set; }
        public string? TestStatus { get; set; }
        public string? TypeofTest { get; set; }
        public string? BacTestResult { get; set; }
        public string? SuspectedAlcohol { get; set; }
        public string? SuspectedDrug { get; set; }
        public int? TowedFromScene { get; set; }

    }
    public class AddPoliceReportOperatorDtlsRequest
    {       
        public int VehicleNo { get; set; }
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
        public int SeatPosition { get; set; }
        public int SafetySystem { get; set; }
        public int AirbagStatus { get; set; }
        public int EjectCode { get; set; }
        public int TrapCode { get; set; }
        public int InjuryStatus { get; set; }
        public int TranspCode { get; set; }
        public string MedicalFacility { get; set; }
    }
    public class AddPoliceReportWitnessRequest
    {       
        public string LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Statement { get; set; }

    }
    public class AddPoliceReportPropertyDamageRequest
    {       
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string FourOneType { get; set; }
        public string Description { get; set; }

    }
    public class AddPoliceReportTruckAndBusDtlsRequest
    {        
        public int VehicleNo { get; set; }
        public string Registration { get; set; }
        public string CarrierName { get; set; }
        public string BusUse { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string St { get; set; }
        public string Zip { get; set; }
        public string UsDot { get; set; }
        public string StateNumber { get; set; }
        public string IssuingState { get; set; }
        public string MCMXICC { get; set; }
        public string InterState { get; set; }
        public string CargoBodyType { get; set; }
        public string GVGCWR { get; set; }
        public string TrailerReg { get; set; }
        public string RegType { get; set; }
        public string RegState { get; set; }
        public string RegYear { get; set; }
        public string TrailerLength { get; set; }
        public string Placard { get; set; }
        public int Material1 { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDigit { get; set; }
        public int ReleaseCode { get; set; }
        public string OfficerName { get; set; }
        public int IDBadge { get; set; }
        public string Department { get; set; }
        public string PrecinctBarracks { get; set; }
        public DateTime Date { get; set; }
    }
    public class AddPoliceReportGeneralRequest
    {       
        public DateTime AccidentDate { get; set; }
        public string AccidentTime { get; set; }
        public string ReportingOfficer { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class AddPoliceReportOperatorOwnerVehicleDtlsRequest
    {       
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string? OperatorSuffixName { get; set; }
        public int OperatorVeh { get; set; }
        public bool OperatorInjured { get; set; }
        public bool OperatorFatality { get; set; }
        public int OperatorNumber { get; set; }
        public string OperatorStreet { get; set; }
        public string OperatorStreetSuffix { get; set; }
        public string OperatorStreetApt { get; set; }
        public string OperatorCity { get; set; }
        public string OperatorState { get; set; }
        public string OperatorZip { get; set; }
        public DateTime OperatorDOB { get; set; }
        public string OperatorHomePhone { get; set; }
        public string OperatorWorkPhone { get; set; }
        public string OperatorLic { get; set; }
        public string OperatorStateNumber { get; set; }
        public string OperatorInsuranceComp { get; set; }
        public string OperatorPolicyNumber { get; set; }
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string? OwnerSuffixName { get; set; }
        public string OwnerHomePhone { get; set; }
        public string OwnerWorkPhone { get; set; }
        public int OwnerNumber { get; set; }
        public string OwnerStreet { get; set; }
        public string OwnerStreetSuffix { get; set; }
        public string OwnerStreetApt { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public string OwnerInsuranceComp { get; set; }
        public string OwnerPolicyNumber { get; set; }
        public string VehYear { get; set; }
        public string VehMake { get; set; }
        public string VehModel { get; set; }
        public string VehVin { get; set; }
        public string VehReg { get; set; }
        public string VehStateNumber { get; set; }
        public string VehTowedBy { get; set; }
        public string VehTowedTo { get; set; }
    }

    #endregion

    #region Update
    public class UpdatePoliceReportRequest
    {
        public int TonTowRptId { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfCrash { get; set; }
        public string TimeOfCrash { get; set; }
        public string CityTown { get; set; }
        public int VehicleNumber { get; set; }
        public int InjuredNumber { get; set; }
        public int? SpeedLimit { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public bool? StatePolice { get; set; }
        public bool? LocalPolice { get; set; }
        public bool? MBTAPolice { get; set; }
        public bool? CampusPolice { get; set; }
        public string? Other { get; set; }
        public bool AtIntersection { get; set; }
        public string? AIRoute1 { get; set; }
        public string? AIDirection1 { get; set; }
        public string? AIRoadwayStName1 { get; set; }
        public string? AIRoute2 { get; set; }
        public string? AIDirection2 { get; set; }
        public string? AIRoadwayStName2 { get; set; }
        public string? AIRoute3 { get; set; }
        public string? AIDirection3 { get; set; }
        public string? AIRoadwayStName3 { get; set; }
        public string? NAIRoute { get; set; }
        public string? NAIDirection { get; set; }
        public string? NAIAddress { get; set; }
        public string? NAIRoadwayStName { get; set; }
        public string? NAIFeet1 { get; set; }
        public string? NAIDirection1 { get; set; }
        public string? NAIMile { get; set; }
        public string? NAIExitNo { get; set; }
        public string? NAIFeet2 { get; set; }
        public string? NAIDirection2 { get; set; }
        public string? NAIRoute1 { get; set; }
        public string? NAIRoadwaySt { get; set; }
        public string? NAIFeet3 { get; set; }
        public string? NAIDirection3 { get; set; }
        public string? NAILandmark { get; set; }
        public string CrashReportId { get; set; }
        public string CrashNarrative { get; set; }
        public string ModifiedBy { get; set; }
        public List<UpdatePoliceReportVehicleDtlsRequest> UpdatePoliceReportVehicleDtlsRequest { get; set; }
        public List<UpdatePoliceReportOperatorDtlsRequest> UpdatePoliceReportOperatorDtlsRequest { get; set; }
        public List<UpdatePoliceReportWitnessRequest> UpdatePoliceReportWitnessRequest { get; set; }
        public List<UpdatePoliceReportPropertyDamageRequest> UpdatePoliceReportPropertyDamageRequest { get; set; }
        public List<UpdatePoliceReportTruckAndBusDtlsRequest> UpdatePoliceReportTruckAndBusDtlsRequest { get; set; }
        public UpdatePoliceReportGeneralRequest UpdatePoliceReportGeneralRequest { get; set; }
        public List<UpdatePoliceReportOperatorOwnerVehicleDtlsRequest> UpdatePoliceReportOperatorOwnerVehicleDtlsRequest { get; set; }
    }
    public class UpdatePoliceReportVehicleDtlsRequest
    {
        public int Id { get; set; }
        public int VehicleNo { get; set; }
        public string CrashType { get; set; }
        public string? Type { get; set; }
        public string? Action { get; set; }
        public string? Location { get; set; }
        public string? Condition { get; set; }
        public int Occupants { get; set; }
        public string License { get; set; }
        public string? Street { get; set; }
        public DateTime DOBAge { get; set; }
        public string? Sex { get; set; }
        public string? LicClass { get; set; }
        public int? LicRestrictions { get; set; }
        public string? CDLEndorememt { get; set; }
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string OperatorAddress { get; set; }
        public string OperatorCity { get; set; }
        public string OperatorState { get; set; }
        public string OperatorZip { get; set; }
        public string InsuranceCompany { get; set; }
        public string? VehicleTravelDirection { get; set; }
        public int RespondingtoEmergency { get; set; }
        public string? CitationIssued { get; set; }
        public string? Viol1 { get; set; }
        public string? Viol2 { get; set; }
        public string? Viol3 { get; set; }
        public string? Viol4 { get; set; }
        public string Reg { get; set; }
        public string RegType { get; set; }
        public string RegState { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public int? VehicleConfig { get; set; }
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public int? VehicleActionPriortoCrash { get; set; }
        public int? EventSequence1 { get; set; }
        public int? EventSequence2 { get; set; }
        public int? EventSequence3 { get; set; }
        public int? EventSequence4 { get; set; }
        public int? MostHarmfulEvent { get; set; }
        public int? DriverContributingCode1 { get; set; }
        public int? DriverContributingCode2 { get; set; }
        public int? DriverDistractedBy { get; set; }
        public int? DamagedAreaCode1 { get; set; }
        public int? DamagedAreaCode2 { get; set; }
        public int? DamagedAreaCode3 { get; set; }
        public string? TestStatus { get; set; }
        public string? TypeofTest { get; set; }
        public string? BacTestResult { get; set; }
        public string? SuspectedAlcohol { get; set; }
        public string? SuspectedDrug { get; set; }
        public int? TowedFromScene { get; set; }

    }
    public class UpdatePoliceReportOperatorDtlsRequest
    {
        public int Id { get; set; }       
        public int VehicleNo { get; set; }
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
        public int SeatPosition { get; set; }
        public int SafetySystem { get; set; }
        public int AirbagStatus { get; set; }
        public int EjectCode { get; set; }
        public int TrapCode { get; set; }
        public int InjuryStatus { get; set; }
        public int TranspCode { get; set; }
        public string MedicalFacility { get; set; }
    }
    public class UpdatePoliceReportWitnessRequest
    {
        public int Id { get; set; }       
        public string LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Statement { get; set; }

    }
    public class UpdatePoliceReportPropertyDamageRequest
    {
        public int Id { get; set; }      
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string FourOneType { get; set; }
        public string Description { get; set; }

    }
    public class UpdatePoliceReportTruckAndBusDtlsRequest
    {
        public int Id { get; set; }       
        public int VehicleNo { get; set; }
        public string Registration { get; set; }
        public string CarrierName { get; set; }
        public string BusUse { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string St { get; set; }
        public string Zip { get; set; }
        public string UsDot { get; set; }
        public string StateNumber { get; set; }
        public string IssuingState { get; set; }
        public string MCMXICC { get; set; }
        public string InterState { get; set; }
        public string CargoBodyType { get; set; }
        public string GVGCWR { get; set; }
        public string TrailerReg { get; set; }
        public string RegType { get; set; }
        public string RegState { get; set; }
        public string RegYear { get; set; }
        public string TrailerLength { get; set; }
        public string Placard { get; set; }
        public int Material1 { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDigit { get; set; }
        public int ReleaseCode { get; set; }
        public string OfficerName { get; set; }
        public int IDBadge { get; set; }
        public string Department { get; set; }
        public string PrecinctBarracks { get; set; }
        public DateTime Date { get; set; }
    }
    public class UpdatePoliceReportGeneralRequest
    {
        public int Id { get; set; }       
        public DateTime AccidentDate { get; set; }
        public string AccidentTime { get; set; }
        public string ReportingOfficer { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class UpdatePoliceReportOperatorOwnerVehicleDtlsRequest
    {
        public int Id { get; set; }      
        public string OperatorLastName { get; set; }
        public string? OperatorFirstName { get; set; }
        public string? OperatorMiddleName { get; set; }
        public string? OperatorSuffixName { get; set; }
        public int OperatorVeh { get; set; }
        public bool OperatorInjured { get; set; }
        public bool OperatorFatality { get; set; }
        public int OperatorNumber { get; set; }
        public string OperatorStreet { get; set; }
        public string OperatorStreetSuffix { get; set; }
        public string OperatorStreetApt { get; set; }
        public string OperatorCity { get; set; }
        public string OperatorState { get; set; }
        public string OperatorZip { get; set; }
        public DateTime OperatorDOB { get; set; }
        public string OperatorHomePhone { get; set; }
        public string OperatorWorkPhone { get; set; }
        public string OperatorLic { get; set; }
        public string OperatorStateNumber { get; set; }
        public string OperatorInsuranceComp { get; set; }
        public string OperatorPolicyNumber { get; set; }
        public string OwnerLastName { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerMiddleName { get; set; }
        public string? OwnerSuffixName { get; set; }
        public string OwnerHomePhone { get; set; }
        public string OwnerWorkPhone { get; set; }
        public int OwnerNumber { get; set; }
        public string OwnerStreet { get; set; }
        public string OwnerStreetSuffix { get; set; }
        public string OwnerStreetApt { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public string OwnerInsuranceComp { get; set; }
        public string OwnerPolicyNumber { get; set; }
        public string VehYear { get; set; }
        public string VehMake { get; set; }
        public string VehModel { get; set; }
        public string VehVin { get; set; }
        public string VehReg { get; set; }
        public string VehStateNumber { get; set; }
        public string VehTowedBy { get; set; }
        public string VehTowedTo { get; set; }
    }
    #endregion

    #region Delete
    public class DeletePoliceReportRequest
    {
        public int TonTowRptId { get; set; }
        public string ModifiedBy { get; set; }
    }
    #endregion

}
