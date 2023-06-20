using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonTow.API.Models
{
    [Index(nameof(TonTowRptId), IsUnique = true)]
    public class CustomerPaymentDtls
    {
        public int Id { get; set; }
        public int TonTowRptId { get; set; }
        [StringLength(20)]
        public string PaymentType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FullPaymentAmt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FullPaymentDate { get; set; }
        [StringLength(50)]
        public string? FullPaymentType { get; set; }
        [StringLength(20)]
        public string? FullPaymentCardDtls { get; set; }
        [StringLength(30)]
        public string? FullPaymentInvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment1Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment1Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment1Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment1CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment1InvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment2Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment2Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment2Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment2CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment2InvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment3Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment3Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment3Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment3CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment3InvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment4Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment4Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment4Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment4CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment4InvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment5Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment5Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment5Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment5CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment5InvNum { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PartialPayment6Amt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PartialPayment6Date { get; set; }
        [StringLength(50)]
        public string? PartialPayment6Type { get; set; }
        [StringLength(20)]
        public string? ParitalPayment6CardDtls { get; set; }
        [StringLength(30)]
        public string? ParitalPayment6InvNum { get; set; }
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

    public class AddCustomerPaymentDtlsRequest
    {
        public int TonTowRptId { get; set; }
        public string PaymentType { get; set; }
        public decimal? FullPaymentAmt { get; set; }
        public DateTime? FullPaymentDate { get; set; }
        public string? FullPaymentType { get; set; }
        public string? FullPaymentCardDtls { get; set; }
        public string? FullPaymentInvNum { get; set; }
        public decimal? PartialPayment1Amt { get; set; }
        public DateTime? PartialPayment1Date { get; set; }
        public string? PartialPayment1Type { get; set; }
        public string? ParitalPayment1CardDtls { get; set; }
        public string? ParitalPayment1InvNum { get; set; }
        public decimal? PartialPayment2Amt { get; set; }
        public DateTime? PartialPayment2Date { get; set; }
        public string? PartialPayment2Type { get; set; }
        public string? ParitalPayment2CardDtls { get; set; }
        public string? ParitalPayment2InvNum { get; set; }
        public decimal? PartialPayment3Amt { get; set; }
        public DateTime? PartialPayment3Date { get; set; }
        public string? PartialPayment3Type { get; set; }
        public string? ParitalPayment3CardDtls { get; set; }
        public string? ParitalPayment3InvNum { get; set; }
        public decimal? PartialPayment4Amt { get; set; }
        public DateTime? PartialPayment4Date { get; set; }
        public string? PartialPayment4Type { get; set; }
        public string? ParitalPayment4CardDtls { get; set; }
        public string? ParitalPayment4InvNum { get; set; }
        public decimal? PartialPayment5Amt { get; set; }
        public DateTime? PartialPayment5Date { get; set; }
        public string? PartialPayment5Type { get; set; }
        public string? ParitalPayment5CardDtls { get; set; }
        public string? ParitalPayment5InvNum { get; set; }
        public decimal? PartialPayment6Amt { get; set; }
        public DateTime? PartialPayment6Date { get; set; }
        public string? PartialPayment6Type { get; set; }
        public string? ParitalPayment6CardDtls { get; set; }
        public string? ParitalPayment6InvNum { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UpdateCustomerPaymentDtlsRequest
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public decimal? FullPaymentAmt { get; set; }
        public DateTime? FullPaymentDate { get; set; }
        public string? FullPaymentType { get; set; }
        public string? FullPaymentCardDtls { get; set; }
        public string? FullPaymentInvNum { get; set; }
        public decimal? PartialPayment1Amt { get; set; }
        public DateTime? PartialPayment1Date { get; set; }
        public string? PartialPayment1Type { get; set; }
        public string? ParitalPayment1CardDtls { get; set; }
        public string? ParitalPayment1InvNum { get; set; }
        public decimal? PartialPayment2Amt { get; set; }
        public DateTime? PartialPayment2Date { get; set; }
        public string? PartialPayment2Type { get; set; }
        public string? ParitalPayment2CardDtls { get; set; }
        public string? ParitalPayment2InvNum { get; set; }
        public decimal? PartialPayment3Amt { get; set; }
        public DateTime? PartialPayment3Date { get; set; }
        public string? PartialPayment3Type { get; set; }
        public string? ParitalPayment3CardDtls { get; set; }
        public string? ParitalPayment3InvNum { get; set; }
        public decimal? PartialPayment4Amt { get; set; }
        public DateTime? PartialPayment4Date { get; set; }
        public string? PartialPayment4Type { get; set; }
        public string? ParitalPayment4CardDtls { get; set; }
        public string? ParitalPayment4InvNum { get; set; }
        public decimal? PartialPayment5Amt { get; set; }
        public DateTime? PartialPayment5Date { get; set; }
        public string? PartialPayment5Type { get; set; }
        public string? ParitalPayment5CardDtls { get; set; }
        public string? ParitalPayment5InvNum { get; set; }
        public decimal? PartialPayment6Amt { get; set; }
        public DateTime? PartialPayment6Date { get; set; }
        public string? PartialPayment6Type { get; set; }
        public string? ParitalPayment6CardDtls { get; set; }
        public string? ParitalPayment6InvNum { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class DeleteCustomerPaymentDtlsRequest
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
}
