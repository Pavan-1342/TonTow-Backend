using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TonTow.API.Models
{
    [Index(nameof(TonTowRptId), IsUnique = true)]
    public class Appraiser
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        [StringLength(50)]
        public string AppraiserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppraisedDate { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string Claim { get; set; }
        [StringLength(150)]
        public string ContactAddress { get; set; }
        [StringLength(15)]
        public string ContactPhone { get; set; }
        [StringLength(25)]
        public string VehicleCondition { get; set;}
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
    public class AddAppraiserRequest
    {
        public int TonTowRptId { get; set; }
        public string AppraiserName { get; set; }
        public DateTime AppraisedDate { get; set; }
        public string CompanyName { get; set; }
        public string Claim { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string VehicleCondition { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateAppraiserRequest
    {
        public int Id { get; set; }
        public string AppraiserName { get; set; }
        public DateTime AppraisedDate { get; set; }
        public string CompanyName { get; set; }
        public string Claim { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string VehicleCondition { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class DeleteAppraiserRequest
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
