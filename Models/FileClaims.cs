using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonTow.API.Models
{
    [Index(nameof(TonTowRptId), IsUnique = true)]
    public class FileClaims
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        [StringLength(100)]
        public string FileClaimNumber { get; set; }
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
    public class AddFileClaimRequest
    {
        public int TonTowRptId { get; set; }
        public string FileClaimNumber { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateFileClaimRequest
    {
        public int Id { get; set; }
        public string FileClaimNumber { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class DeleteFileClaimRequest
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
