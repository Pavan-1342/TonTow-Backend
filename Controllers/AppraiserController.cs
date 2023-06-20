using MessagePack.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/Appraiser")]
    public class AppraiserController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;

        public AppraiserController(TonTowDbContext tonTowDbContext)
        {
            _tonTowDbContext = tonTowDbContext;
            
        }
        [HttpGet("GetAllAppraiser"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllAppraiser()
        {
            var appraiser = from Appraiser in _tonTowDbContext.Appraiser
                            join PR in _tonTowDbContext.PoliceReport
                            on Appraiser.TonTowRptId equals PR.TonTowRptId
                            where Appraiser.Status == true
                            select new
                            {
                                Appraiser,
                                PR.JobNum
                            };
            return Ok(appraiser);
        }
        [HttpGet("GetUserAppraiser")]
        public async Task<IActionResult> GetUserAppraiser(int TonTowReportId)
        {
            var appraiser = from Appraiser in _tonTowDbContext.Appraiser
                            join PR in _tonTowDbContext.PoliceReport
                            on Appraiser.TonTowRptId equals PR.TonTowRptId
                            where Appraiser.TonTowRptId == TonTowReportId && Appraiser.Status==true
                           select new
                           {
                               Appraiser,
                               PR.JobNum
                           };
            return Ok(appraiser);
        }
        [HttpGet("GetDuplicateAppraiser"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetDuplicateAppraiser(int TonTowReportId)
        {
            var appraiser = from Appraiser in _tonTowDbContext.Appraiser
                            where Appraiser.TonTowRptId == TonTowReportId 
                            select new
                            {
                                Appraiser
                            };
            return Ok(appraiser);
        }
        [HttpPost("AddAppraiser"), Authorize(Roles = "A")]
        public async Task<IActionResult> AddAppraiser([FromBody] AddAppraiserRequest appraiserRequest)
        {
            Appraiser AppraiserData = new Appraiser();
            AppraiserData.TonTowRptId = appraiserRequest.TonTowRptId;
            AppraiserData.AppraiserName = appraiserRequest.AppraiserName;
            AppraiserData.AppraisedDate = appraiserRequest.AppraisedDate;
            AppraiserData.CompanyName = appraiserRequest.CompanyName;
            AppraiserData.Claim = appraiserRequest.Claim;
            AppraiserData.ContactAddress = appraiserRequest.ContactAddress;
            AppraiserData.ContactPhone = appraiserRequest.ContactPhone;
            AppraiserData.VehicleCondition = appraiserRequest.VehicleCondition;
            AppraiserData.Status = true;            
            AppraiserData.CreatedBy = appraiserRequest.CreatedBy;
            AppraiserData.CreatedDate = DateTime.Now;
            AppraiserData.ModifiedBy = "";
            await _tonTowDbContext.Appraiser.AddAsync(AppraiserData);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(appraiserRequest);
        }
        [HttpPost("UpdateAppraiser"), Authorize(Roles = "A")]
        public async Task<IActionResult> UpdateAppraiser([FromBody] UpdateAppraiserRequest appraiserRequest)
        {
            var UpdateAppraiserData = _tonTowDbContext.Appraiser.SingleOrDefault(
                p => p.Id == appraiserRequest.Id
                );
            if (UpdateAppraiserData != null)
            {
                UpdateAppraiserData.AppraiserName = appraiserRequest.AppraiserName;
                UpdateAppraiserData.AppraisedDate = appraiserRequest.AppraisedDate;
                UpdateAppraiserData.CompanyName = appraiserRequest.CompanyName;
                UpdateAppraiserData.Claim = appraiserRequest.Claim;
                UpdateAppraiserData.ContactAddress = appraiserRequest.ContactAddress;
                UpdateAppraiserData.ContactPhone = appraiserRequest.ContactPhone;
                UpdateAppraiserData.VehicleCondition = appraiserRequest.VehicleCondition;
                UpdateAppraiserData.ModifiedBy = appraiserRequest.ModifiedBy;
                UpdateAppraiserData.ModifiedDate = DateTime.Now;                
                _tonTowDbContext.Appraiser.UpdateRange(UpdateAppraiserData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(appraiserRequest);
        }
        [HttpPost("DeleteAppraiser"), Authorize(Roles = "A")]
        public async Task<IActionResult> DeleteAppraiser([FromBody] DeleteAppraiserRequest appraiserRequest)
        {
            var UpdateAppraiserData = _tonTowDbContext.Appraiser.SingleOrDefault(
                p => p.Id == appraiserRequest.Id
                );
            if (UpdateAppraiserData != null)
            {
                UpdateAppraiserData.Status = false;
                UpdateAppraiserData.ModifiedBy = appraiserRequest.ModifiedBy;
                UpdateAppraiserData.ModifiedDate = DateTime.Now;                           
                _tonTowDbContext.Appraiser.UpdateRange(UpdateAppraiserData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(appraiserRequest);
        }
    }
}
