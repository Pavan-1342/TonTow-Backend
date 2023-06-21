using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/Fileclaims")]
    public class FileClaimsController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;

        public FileClaimsController(TonTowDbContext tonTowDbContext)
        {
            _tonTowDbContext = tonTowDbContext;
        }
        [HttpGet("GetAllFileClaims"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllFileClaims()
        {
            var fileclaim = from FileClaims in _tonTowDbContext.FileClaims
                            join PR in _tonTowDbContext.PoliceReport
                            on FileClaims.TonTowRptId equals PR.TonTowRptId
                            where FileClaims.Status == true
                            select new
                            {
                                FileClaims,
                                PR.JobNum
                            };
            return Ok(fileclaim);
        }
        [HttpGet("GetUserFileClaim")]
        public async Task<IActionResult> GetUserFileClaim(int TonTowReportId)
        {
            var fileclaim = from FileClaims in _tonTowDbContext.FileClaims
                            join PR in _tonTowDbContext.PoliceReport
                            on FileClaims.TonTowRptId equals PR.TonTowRptId
                            where FileClaims.TonTowRptId == TonTowReportId && FileClaims.Status == true
                            select new
                            {
                                FileClaims,
                                PR.JobNum
                            };
            return Ok(fileclaim);
        }
        [HttpGet("GetDuplicateFileClaim"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetDuplicateFileClaim(int TonTowReportId)
        {
            var fileclaim = from FileClaims in _tonTowDbContext.FileClaims
                            where FileClaims.TonTowRptId == TonTowReportId
                            select new
                            {
                                FileClaims
                            };
            return Ok(fileclaim);
        }
        [HttpPost("Addfileclaim"), Authorize(Roles = "A")]
        public async Task<IActionResult> Addfileclaim([FromBody] AddFileClaimRequest fileclaimRequest)
        {
            FileClaims FileClaims = new FileClaims();
            FileClaims.FileClaimNumber = fileclaimRequest.FileClaimNumber;
            FileClaims.TonTowRptId = fileclaimRequest.TonTowRptId;
            FileClaims.Status = true;
            FileClaims.CreatedDate = DateTime.Now;
            FileClaims.CreatedBy = fileclaimRequest.CreatedBy;
            FileClaims.ModifiedBy = "";
            await _tonTowDbContext.FileClaims.AddAsync(FileClaims);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(fileclaimRequest);
        }
        [HttpPost("Updatefileclaim"), Authorize(Roles = "A")]
        public async Task<IActionResult> Updatefileclaim([FromBody] UpdateFileClaimRequest UpdateFileClaimRequest)
        {
            var UpdatefileclaimData = _tonTowDbContext.FileClaims.SingleOrDefault(
                p => p.Id == UpdateFileClaimRequest.Id
                );
            if (UpdatefileclaimData != null)
            {
                UpdatefileclaimData.FileClaimNumber = UpdateFileClaimRequest.FileClaimNumber;
                UpdatefileclaimData.ModifiedDate = DateTime.Now;
                UpdatefileclaimData.ModifiedBy = UpdateFileClaimRequest.ModifiedBy;
                _tonTowDbContext.FileClaims.UpdateRange(UpdatefileclaimData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(UpdateFileClaimRequest);
        }
        [HttpPost("Deletefileclaim"), Authorize(Roles = "A")]
        public async Task<IActionResult> Deletefileclaim([FromQuery] DeleteFileClaimRequest fileclaimRequest)
        {
            var UpdatefileclaimData = _tonTowDbContext.FileClaims.SingleOrDefault(
                p => p.Id == fileclaimRequest.Id
                );
            if (UpdatefileclaimData != null)
            {
                UpdatefileclaimData.Status = false;
                UpdatefileclaimData.ModifiedDate = DateTime.Now;
                UpdatefileclaimData.ModifiedBy = fileclaimRequest.ModifiedBy;                
                _tonTowDbContext.FileClaims.UpdateRange(UpdatefileclaimData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(UpdatefileclaimData);
        }
    }
}
