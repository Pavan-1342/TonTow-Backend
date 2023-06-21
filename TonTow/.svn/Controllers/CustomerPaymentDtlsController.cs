using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/Customerpaymentdtls")]
    public class CustomerPaymentDtlsController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;       

        public CustomerPaymentDtlsController(TonTowDbContext tonTowDbContext)
        {
            _tonTowDbContext = tonTowDbContext;
        }
        [HttpGet("GetAllCustomerPaymentDtls"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllCustomerPaymentDtls()
        {
            var customerpaymentdtls = from CustomerPaymentDtls in _tonTowDbContext.CustomerPayment
                                      join PR in _tonTowDbContext.PoliceReport
                                      on CustomerPaymentDtls.TonTowRptId equals PR.TonTowRptId
                                      where  CustomerPaymentDtls.Status == true
                                      select new
                                      {
                                          CustomerPaymentDtls,
                                          PR.JobNum
                                      };
            return Ok(customerpaymentdtls);
        }
        [HttpGet("GetUserCustomerPaymentDtls"), Authorize]
        public async Task<IActionResult> GetUserCustomerPaymentDtls(int TonTowReportId)
        {
            var customerpaymentdtls = from CustomerPaymentDtls in _tonTowDbContext.CustomerPayment
                                      join PR in _tonTowDbContext.PoliceReport
                                    on CustomerPaymentDtls.TonTowRptId equals PR.TonTowRptId
                                      where CustomerPaymentDtls.TonTowRptId == TonTowReportId && CustomerPaymentDtls.Status == true
                                      select new
                                      {
                                         CustomerPaymentDtls,
                                         PR.JobNum
                                      };
            return Ok(customerpaymentdtls);
        }
        [HttpGet("GetDuplicateCustomerPaymentDtls"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetDuplicateCustomerPaymentDtls(int TonTowReportId)
        {
            var customerpaymentdtls = from CustomerPaymentDtls in _tonTowDbContext.CustomerPayment
                                      where CustomerPaymentDtls.TonTowRptId == TonTowReportId 
                                      select new
                                      {
                                          CustomerPaymentDtls
                                      };
            return Ok(customerpaymentdtls);
        }
        [HttpPost("AddCustomerPaymentDtls"), Authorize(Roles = "A")]
        public async Task<IActionResult> AddCustomerPaymentDtls([FromBody] AddCustomerPaymentDtlsRequest customerpaymentdtlsRequest)
        {
            CustomerPaymentDtls CustomerPaymentDtls = new CustomerPaymentDtls();
            CustomerPaymentDtls.TonTowRptId = customerpaymentdtlsRequest.TonTowRptId;
            CustomerPaymentDtls.PaymentType = customerpaymentdtlsRequest.PaymentType;
            CustomerPaymentDtls.FullPaymentAmt = customerpaymentdtlsRequest.FullPaymentAmt;
            CustomerPaymentDtls.FullPaymentDate = customerpaymentdtlsRequest.FullPaymentDate;
            CustomerPaymentDtls.FullPaymentType = customerpaymentdtlsRequest.FullPaymentType;
            CustomerPaymentDtls.FullPaymentCardDtls = customerpaymentdtlsRequest.FullPaymentCardDtls;
            CustomerPaymentDtls.FullPaymentInvNum = customerpaymentdtlsRequest.FullPaymentInvNum;
            CustomerPaymentDtls.PartialPayment1Amt = customerpaymentdtlsRequest.PartialPayment1Amt;
            CustomerPaymentDtls.PartialPayment1Date = customerpaymentdtlsRequest.PartialPayment1Date;
            CustomerPaymentDtls.PartialPayment1Type = customerpaymentdtlsRequest.PartialPayment1Type;
            CustomerPaymentDtls.ParitalPayment1CardDtls = customerpaymentdtlsRequest.ParitalPayment1CardDtls;
            CustomerPaymentDtls.ParitalPayment1InvNum = customerpaymentdtlsRequest.ParitalPayment1InvNum;
            CustomerPaymentDtls.PartialPayment2Amt = customerpaymentdtlsRequest.PartialPayment2Amt;
            CustomerPaymentDtls.PartialPayment2Date = customerpaymentdtlsRequest.PartialPayment2Date;
            CustomerPaymentDtls.PartialPayment2Type = customerpaymentdtlsRequest.PartialPayment2Type;
            CustomerPaymentDtls.ParitalPayment2CardDtls = customerpaymentdtlsRequest.ParitalPayment2CardDtls;
            CustomerPaymentDtls.ParitalPayment2InvNum = customerpaymentdtlsRequest.ParitalPayment2InvNum;
            CustomerPaymentDtls.PartialPayment3Amt = customerpaymentdtlsRequest.PartialPayment3Amt;
            CustomerPaymentDtls.PartialPayment3Date = customerpaymentdtlsRequest.PartialPayment3Date;
            CustomerPaymentDtls.PartialPayment3Type = customerpaymentdtlsRequest.PartialPayment3Type;
            CustomerPaymentDtls.ParitalPayment3CardDtls = customerpaymentdtlsRequest.ParitalPayment3CardDtls;
            CustomerPaymentDtls.ParitalPayment3InvNum = customerpaymentdtlsRequest.ParitalPayment3InvNum;
            CustomerPaymentDtls.PartialPayment4Amt = customerpaymentdtlsRequest.PartialPayment4Amt;
            CustomerPaymentDtls.PartialPayment4Date = customerpaymentdtlsRequest.PartialPayment4Date;
            CustomerPaymentDtls.PartialPayment4Type = customerpaymentdtlsRequest.PartialPayment4Type;
            CustomerPaymentDtls.ParitalPayment4CardDtls = customerpaymentdtlsRequest.ParitalPayment4CardDtls;
            CustomerPaymentDtls.ParitalPayment4InvNum = customerpaymentdtlsRequest.ParitalPayment4InvNum;
            CustomerPaymentDtls.PartialPayment5Amt = customerpaymentdtlsRequest.PartialPayment5Amt;
            CustomerPaymentDtls.PartialPayment5Date = customerpaymentdtlsRequest.PartialPayment5Date;
            CustomerPaymentDtls.PartialPayment5Type = customerpaymentdtlsRequest.PartialPayment5Type;
            CustomerPaymentDtls.ParitalPayment5CardDtls = customerpaymentdtlsRequest.ParitalPayment5CardDtls;
            CustomerPaymentDtls.ParitalPayment5InvNum = customerpaymentdtlsRequest.ParitalPayment5InvNum;
            CustomerPaymentDtls.PartialPayment6Amt = customerpaymentdtlsRequest.PartialPayment6Amt;
            CustomerPaymentDtls.PartialPayment6Date = customerpaymentdtlsRequest.PartialPayment6Date;
            CustomerPaymentDtls.PartialPayment6Type = customerpaymentdtlsRequest.PartialPayment6Type;
            CustomerPaymentDtls.ParitalPayment6CardDtls = customerpaymentdtlsRequest.ParitalPayment6CardDtls;
            CustomerPaymentDtls.ParitalPayment6InvNum = customerpaymentdtlsRequest.ParitalPayment6InvNum;
            CustomerPaymentDtls.Status = true;
            CustomerPaymentDtls.CreatedBy = customerpaymentdtlsRequest.CreatedBy;
            CustomerPaymentDtls.CreatedDate = DateTime.Now;            
            CustomerPaymentDtls.ModifiedBy = "";
            await _tonTowDbContext.CustomerPaymentDtl.AddAsync(CustomerPaymentDtls);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(customerpaymentdtlsRequest);
        }
        [HttpPost("UpdateCustomerPaymentDtls"), Authorize(Roles = "A")]
        public async Task<IActionResult> UpdateCustomerPaymentDtls([FromBody] UpdateCustomerPaymentDtlsRequest customerpaymentdtlsRequest)
        {
            var UpdateCustomerPaymentDtlsData = _tonTowDbContext.CustomerPaymentDtl.SingleOrDefault(
                p => p.Id == customerpaymentdtlsRequest.Id
                );
            if (UpdateCustomerPaymentDtlsData != null)
            {
                UpdateCustomerPaymentDtlsData.PaymentType = customerpaymentdtlsRequest.PaymentType;
                UpdateCustomerPaymentDtlsData.FullPaymentAmt = customerpaymentdtlsRequest.FullPaymentAmt;
                UpdateCustomerPaymentDtlsData.FullPaymentDate = customerpaymentdtlsRequest.FullPaymentDate;
                UpdateCustomerPaymentDtlsData.FullPaymentType = customerpaymentdtlsRequest.FullPaymentType;
                UpdateCustomerPaymentDtlsData.FullPaymentCardDtls = customerpaymentdtlsRequest.FullPaymentCardDtls;
                UpdateCustomerPaymentDtlsData.FullPaymentInvNum = customerpaymentdtlsRequest.FullPaymentInvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment1Amt = customerpaymentdtlsRequest.PartialPayment1Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment1Date = customerpaymentdtlsRequest.PartialPayment1Date;
                UpdateCustomerPaymentDtlsData.PartialPayment1Type = customerpaymentdtlsRequest.PartialPayment1Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment1CardDtls = customerpaymentdtlsRequest.ParitalPayment1CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment1InvNum = customerpaymentdtlsRequest.ParitalPayment1InvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment2Amt = customerpaymentdtlsRequest.PartialPayment2Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment2Date = customerpaymentdtlsRequest.PartialPayment2Date;
                UpdateCustomerPaymentDtlsData.PartialPayment2Type = customerpaymentdtlsRequest.PartialPayment2Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment2CardDtls = customerpaymentdtlsRequest.ParitalPayment2CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment2InvNum = customerpaymentdtlsRequest.ParitalPayment2InvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment3Amt = customerpaymentdtlsRequest.PartialPayment3Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment3Date = customerpaymentdtlsRequest.PartialPayment3Date;
                UpdateCustomerPaymentDtlsData.PartialPayment3Type = customerpaymentdtlsRequest.PartialPayment3Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment3CardDtls = customerpaymentdtlsRequest.ParitalPayment3CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment3InvNum = customerpaymentdtlsRequest.ParitalPayment3InvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment4Amt = customerpaymentdtlsRequest.PartialPayment4Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment4Date = customerpaymentdtlsRequest.PartialPayment4Date;
                UpdateCustomerPaymentDtlsData.PartialPayment4Type = customerpaymentdtlsRequest.PartialPayment4Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment4CardDtls = customerpaymentdtlsRequest.ParitalPayment4CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment4InvNum = customerpaymentdtlsRequest.ParitalPayment4InvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment5Amt = customerpaymentdtlsRequest.PartialPayment5Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment5Date = customerpaymentdtlsRequest.PartialPayment5Date;
                UpdateCustomerPaymentDtlsData.PartialPayment5Type = customerpaymentdtlsRequest.PartialPayment5Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment5CardDtls = customerpaymentdtlsRequest.ParitalPayment5CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment5InvNum = customerpaymentdtlsRequest.ParitalPayment5InvNum;
                UpdateCustomerPaymentDtlsData.PartialPayment6Amt = customerpaymentdtlsRequest.PartialPayment6Amt;
                UpdateCustomerPaymentDtlsData.PartialPayment6Date = customerpaymentdtlsRequest.PartialPayment6Date;
                UpdateCustomerPaymentDtlsData.PartialPayment6Type = customerpaymentdtlsRequest.PartialPayment6Type;
                UpdateCustomerPaymentDtlsData.ParitalPayment6CardDtls = customerpaymentdtlsRequest.ParitalPayment6CardDtls;
                UpdateCustomerPaymentDtlsData.ParitalPayment6InvNum = customerpaymentdtlsRequest.ParitalPayment6InvNum;
                UpdateCustomerPaymentDtlsData.ModifiedBy = customerpaymentdtlsRequest.ModifiedBy;
                UpdateCustomerPaymentDtlsData.ModifiedDate = DateTime.Now;                
                _tonTowDbContext.CustomerPaymentDtl.UpdateRange(UpdateCustomerPaymentDtlsData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(customerpaymentdtlsRequest);
        }
        [HttpPost("DeleteCustomerPaymentDtls"), Authorize(Roles = "A")]
        public async Task<IActionResult> DeleteCustomerPaymentDtls([FromBody] DeleteCustomerPaymentDtlsRequest customerpaymentdtlsRequest)
        {
            var UpdateCustomerPaymentDtlsData = _tonTowDbContext.CustomerPaymentDtl.SingleOrDefault(
                p => p.Id == customerpaymentdtlsRequest.Id
                );
            if (UpdateCustomerPaymentDtlsData != null)
            {
                UpdateCustomerPaymentDtlsData.Status = false;
                UpdateCustomerPaymentDtlsData.ModifiedBy = customerpaymentdtlsRequest.ModifiedBy;
                UpdateCustomerPaymentDtlsData.ModifiedDate = DateTime.Now;              
                _tonTowDbContext.CustomerPaymentDtl.UpdateRange(UpdateCustomerPaymentDtlsData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(customerpaymentdtlsRequest);
        }
    }
}