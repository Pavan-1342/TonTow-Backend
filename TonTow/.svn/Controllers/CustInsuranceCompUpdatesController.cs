using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TonTow.API.Data;
using TonTow.API.Models;

namespace TonTow.API.Controllers
{
    [ApiController, Authorize]
    [Route("api/CustInsurance")]
    public class CustInsuranceCompUpdatesController : Controller
    {
        private readonly TonTowDbContext _tonTowDbContext;

        public CustInsuranceCompUpdatesController(TonTowDbContext context)
        {
            _tonTowDbContext = context;
        }

        [HttpGet("GetAllCustInsuranceCompUpdates"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetAllCustInsuranceCompUpdates()
        {
            var CustInsuranceCompUpdates = from CustInsuranceCompUpdate in _tonTowDbContext.CustInsuranceCompUpdate
                                           join PR in _tonTowDbContext.PoliceReport
                                            on CustInsuranceCompUpdate.TonTowRptId equals PR.TonTowRptId
                                           where  CustInsuranceCompUpdate.Status == true
                                           select new
                                           {
                                               CustInsuranceCompUpdate,
                                               PR.JobNum
                                           };
            return Ok(CustInsuranceCompUpdates);
        }
        [HttpGet("GetUserCustInsuranceCompUpdates")]
        public async Task<IActionResult> GetUserCustInsuranceCompUpdates(int TonTowReportId)
        {
            var CustInsuranceCompUpdates = from CustInsuranceCompUpdate in _tonTowDbContext.CustInsuranceCompUpdate
                                           join PR in _tonTowDbContext.PoliceReport
                                           on CustInsuranceCompUpdate.TonTowRptId equals PR.TonTowRptId
                                           where CustInsuranceCompUpdate.TonTowRptId == TonTowReportId && CustInsuranceCompUpdate.Status == true
                                           select new
                                          {
                                              CustInsuranceCompUpdate,
                                              PR.JobNum
                                          };
            return Ok(CustInsuranceCompUpdates);
        }

        [HttpGet("GetDuplicateCustInsuranceCompUpdates"), Authorize(Roles = "A")]
        public async Task<IActionResult> GetDuplicateCustInsuranceCompUpdates(int TonTowReportId)
        {
            var CustInsuranceCompUpdates = from CustInsuranceCompUpdate in _tonTowDbContext.CustInsuranceCompUpdate
                                           where CustInsuranceCompUpdate.TonTowRptId == TonTowReportId 
                                           select new
                                           {
                                               CustInsuranceCompUpdate
                                           };
            return Ok(CustInsuranceCompUpdates);
        }
        [HttpPost("AddCustInsuranceCompUpdates"), Authorize(Roles = "A")]
        public async Task<IActionResult> AddCustInsuranceCompUpdates([FromBody] AddCustInsuranceCompUpdateRequest CustInsuranceCompUpdatesRequest)
        {
            CustInsuranceCompUpdate CustInsuranceCompUpdate = new CustInsuranceCompUpdate();
            CustInsuranceCompUpdate.TonTowRptId = CustInsuranceCompUpdatesRequest.TonTowRptId;
            CustInsuranceCompUpdate.VehicleCondition = CustInsuranceCompUpdatesRequest.VehicleCondition;
            if (CustInsuranceCompUpdatesRequest.VehicleCondition == "Repairable")
            {
                CustInsuranceCompUpdate.RepairableNotes1 = CustInsuranceCompUpdatesRequest.RepairableNotes1;
                CustInsuranceCompUpdate.RepairableNotes2 = CustInsuranceCompUpdatesRequest.RepairableNotes2;
                CustInsuranceCompUpdate.TotaledNotes = null;
            }
            else if (CustInsuranceCompUpdatesRequest.VehicleCondition == "Totaled")
            {
                CustInsuranceCompUpdate.RepairableNotes1 = null;
                CustInsuranceCompUpdate.RepairableNotes2 = null;
                CustInsuranceCompUpdate.TotaledNotes = CustInsuranceCompUpdatesRequest.TotaledNotes;
            }
           
            CustInsuranceCompUpdate.Status = true;
            CustInsuranceCompUpdate.CreatedBy = CustInsuranceCompUpdatesRequest.CreatedBy;
            CustInsuranceCompUpdate.CreatedDate = DateTime.Now;
            CustInsuranceCompUpdate.ModifiedBy = "";
            await _tonTowDbContext.CustInsuranceCompUpdate.AddAsync(CustInsuranceCompUpdate);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(CustInsuranceCompUpdatesRequest);
        }
        [HttpPost("UpdateCustInsuranceCompUpdates"), Authorize(Roles = "A")]
        public async Task<IActionResult> UpdateCustInsuranceCompUpdates([FromBody] UpdateCustInsuranceCompUpdateRequest CustInsuranceCompUpdatesRequest)
        {
            var UpdateCustInsuranceCompUpdatesData = _tonTowDbContext.CustInsuranceCompUpdate.SingleOrDefault(
                p => p.Id == CustInsuranceCompUpdatesRequest.Id
                );
            if (UpdateCustInsuranceCompUpdatesData != null)
            {
                UpdateCustInsuranceCompUpdatesData.VehicleCondition = CustInsuranceCompUpdatesRequest.VehicleCondition;
                if (CustInsuranceCompUpdatesRequest.VehicleCondition == "Repairable")
                {
                    UpdateCustInsuranceCompUpdatesData.RepairableNotes1 = CustInsuranceCompUpdatesRequest.RepairableNotes1;
                    UpdateCustInsuranceCompUpdatesData.RepairableNotes2 = CustInsuranceCompUpdatesRequest.RepairableNotes2;
                    UpdateCustInsuranceCompUpdatesData.TotaledNotes = null;
                }
                else if (CustInsuranceCompUpdatesRequest.VehicleCondition == "Totaled")
                {
                    UpdateCustInsuranceCompUpdatesData.RepairableNotes1 = null;
                    UpdateCustInsuranceCompUpdatesData.RepairableNotes2 = null;
                    UpdateCustInsuranceCompUpdatesData.TotaledNotes = CustInsuranceCompUpdatesRequest.TotaledNotes;
                }
                UpdateCustInsuranceCompUpdatesData.ModifiedBy = CustInsuranceCompUpdatesRequest.ModifiedBy;
                UpdateCustInsuranceCompUpdatesData.ModifiedDate = DateTime.Now;               
                _tonTowDbContext.CustInsuranceCompUpdate.UpdateRange(UpdateCustInsuranceCompUpdatesData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(CustInsuranceCompUpdatesRequest);
        }
        [HttpPost("DeleteCustInsuranceCompUpdates"), Authorize(Roles = "A")]
        public async Task<IActionResult> DeleteCustInsuranceCompUpdates([FromBody] DeleteCustInsuranceCompUpdateRequest CustInsuranceCompUpdatesRequest)
        {
            var UpdateCustInsuranceCompUpdatesData = _tonTowDbContext.CustInsuranceCompUpdate.SingleOrDefault(
                p => p.Id == CustInsuranceCompUpdatesRequest.Id
                );
            if (UpdateCustInsuranceCompUpdatesData != null)
            {
                UpdateCustInsuranceCompUpdatesData.Status = false;
                UpdateCustInsuranceCompUpdatesData.ModifiedBy = CustInsuranceCompUpdatesRequest.ModifiedBy;
                UpdateCustInsuranceCompUpdatesData.ModifiedDate = DateTime.Now;               
                _tonTowDbContext.CustInsuranceCompUpdate.UpdateRange(UpdateCustInsuranceCompUpdatesData);
                await _tonTowDbContext.SaveChangesAsync();
            }
            return Ok(CustInsuranceCompUpdatesRequest);
        }
    }
}
