using DataService;
using DataService.Complaint;
using Entities;
using Entities.Complaints;
using Entities.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Complaint")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private IComplaintDataService _IComplaintDataService;

        public ComplaintController(IComplaintDataService iComplaintDataService)
        {
            _IComplaintDataService = iComplaintDataService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            Complaint Complaint = await _IComplaintDataService.GetComplaint(id);
            return Complaint;
        }

        [HttpPost]
        public async Task<ActionResult<Complaint>> PostComplaint(Complaint complaint)
        {
            Complaint updatedComplaint = await _IComplaintDataService.PostComplaint(complaint);
            if (updatedComplaint == null)
            {
                return BadRequest("Unable To Update Complaint");
            }
            return updatedComplaint;
        }

    }
}
