using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace TonTow.API.Models
{
    [Index(nameof(TonTowRptId), IsUnique = true)]
    public class Adjuster
    {
        
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        [StringLength(50)]
        public string AdjusterName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppraisedDate { get; set;}
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string Claim { get; set; }
        [StringLength(150)]
        public string ContactAddress { get; set; }
        [StringLength(15)]
        public string ContactPhone { get; set; }
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

    public class AddAdjusterRequest
    {

        public int TonTowRptId { get; set; }
        public string AdjusterName { get; set; }
        public DateTime AppraisedDate { get; set; }
        public string CompanyName { get; set; }
        public string Claim { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string CreatedBy { get; set; }

    }
    public class UpdateAdjusterRequest
    {

        public int Id { get; set; }
        public string AdjusterName { get; set; }
        public DateTime AppraisedDate { get; set; }
        public string CompanyName { get; set; }
        public string Claim { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string ModifiedBy { get; set; }

    }
    public class DeleteAdjusterRequest
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
