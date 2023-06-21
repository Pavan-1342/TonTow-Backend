using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonTow.API.Models
{
    [Index(nameof(TonTowRptId), IsUnique = true)]
    public class CustInsuranceCompUpdate
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        [StringLength(25)]
        public string VehicleCondition { get; set; }
        [StringLength(50)]
        public string? RepairableNotes1 { get; set; }
        [StringLength(50)]
        public string? RepairableNotes2 { get; set; }
        [StringLength(50)]
        public string? TotaledNotes { get; set; }
        public bool Status { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
    }

    public class AddCustInsuranceCompUpdateRequest
    {
        public int TonTowRptId { get; set; }
        public string VehicleCondition { get; set; }
        public string? RepairableNotes1 { get; set; }
        public string? RepairableNotes2 { get; set; }
        public string? TotaledNotes { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateCustInsuranceCompUpdateRequest
    {
        public int Id { get; set; }
        public string VehicleCondition { get; set; }
        public string? RepairableNotes1 { get; set; }
        public string? RepairableNotes2 { get; set; }
        public string? TotaledNotes { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class DeleteCustInsuranceCompUpdateRequest
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
