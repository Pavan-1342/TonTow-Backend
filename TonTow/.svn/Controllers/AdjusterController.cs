using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/Adjuster")]
    public class AdjusterController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;       

        public AdjusterController(TonTowDbContext tonTowDbContext)
        {
            _tonTowDbContext = tonTowDbContext;
        }
        [HttpGet("GetAllAdjuster"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllAdjuster()
        {
            var Adjuster = from adjuster in _tonTowDbContext.Adjuster
                           join PR in _tonTowDbContext.PoliceReport
                           on adjuster.TonTowRptId equals PR.TonTowRptId
                           where adjuster.Status == true
                           select new
                           {
                               adjuster,
                               PR.JobNum
                           };
            return Ok(Adjuster);
        }
        [HttpGet("GetUserAdjuster")]
        public async Task<IActionResult> GetUserAdjuster(int TonTowReportId)
        {
            var Adjuster = from adjuster in _tonTowDbContext.Adjuster
                           join PR in _tonTowDbContext.PoliceReport
                           on adjuster.TonTowRptId equals PR.TonTowRptId
                           where adjuster.TonTowRptId == TonTowReportId && adjuster.Status == true
                           select new
                           {
                               adjuster,
                               PR.JobNum
                           };
            return Ok(Adjuster);
        }
        [HttpGet("GetDuplicateAdjuster"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetDuplicateAdjuster(int TonTowReportId)
        {
            var Adjuster = from adjuster in _tonTowDbContext.Adjuster
                           where adjuster.TonTowRptId == TonTowReportId
                           select new
                           {
                               adjuster
                           };
            return Ok(Adjuster);
        }
        [HttpPost("AddAdjuster"), Authorize(Roles = "A")]
        public async Task<IActionResult> AddAdjuster([FromBody] AddAdjusterRequest adjusterRequest )
        {
            Adjuster Adjuster = new Adjuster();
            Adjuster.TonTowRptId = adjusterRequest.TonTowRptId;
            Adjuster.AdjusterName = adjusterRequest.AdjusterName;
            Adjuster.AppraisedDate = adjusterRequest.AppraisedDate;
            Adjuster.CompanyName = adjusterRequest.CompanyName;
            Adjuster.Claim = adjusterRequest.Claim;
            Adjuster.ContactAddress = adjusterRequest.ContactAddress;
            Adjuster.ContactPhone = adjusterRequest.ContactPhone;
            Adjuster.CreatedBy = adjusterRequest.CreatedBy;
            Adjuster.Status = true;
            Adjuster.CreatedDate=DateTime.Now;
            Adjuster.ModifiedBy = "";
            await _tonTowDbContext.Adjuster.AddAsync(Adjuster);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(adjusterRequest);
        }
        [HttpPost("UpdateAdjuster"), Authorize(Roles = "A")]
        public async Task<IActionResult> UpdateAdjuster([FromBody] UpdateAdjusterRequest adjusterRequest)
        {
            var UpdateAdjusterData = _tonTowDbContext.Adjuster.SingleOrDefault(
                p => p.Id == adjusterRequest.Id
                );
            if (UpdateAdjusterData != null)
            {
                UpdateAdjusterData.AdjusterName = adjusterRequest.AdjusterName;
                UpdateAdjusterData.AppraisedDate = adjusterRequest.AppraisedDate;
                UpdateAdjusterData.CompanyName = adjusterRequest.CompanyName;
                UpdateAdjusterData.Claim = adjusterRequest.Claim;
                UpdateAdjusterData.ContactAddress = adjusterRequest.ContactAddress;
                UpdateAdjusterData.ContactPhone = adjusterRequest.ContactPhone;
                UpdateAdjusterData.ModifiedBy = adjusterRequest.ModifiedBy;
                UpdateAdjusterData.ModifiedDate = DateTime.Now;               
                _tonTowDbContext.Adjuster.UpdateRange(UpdateAdjusterData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(adjusterRequest);
        }
        [HttpPost("DeleteAdjuster"), Authorize(Roles = "A")]
        public async Task<IActionResult> DeleteAdjuster([FromBody] DeleteAdjusterRequest adjusterRequest)
        {
            var UpdateAdjusterData = _tonTowDbContext.Adjuster.SingleOrDefault(
                p => p.Id == adjusterRequest.Id
                );
            if (UpdateAdjusterData != null)
            {
                UpdateAdjusterData.Status = false;
                UpdateAdjusterData.ModifiedBy = adjusterRequest.ModifiedBy;
                UpdateAdjusterData.ModifiedDate = DateTime.Now;                     
                _tonTowDbContext.Adjuster.UpdateRange(UpdateAdjusterData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(adjusterRequest);
        }
    }
}
 