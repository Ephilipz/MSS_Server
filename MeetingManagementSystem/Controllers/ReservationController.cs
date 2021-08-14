using DataService;
using DataService.Reservation;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Authorize]
    [Route("api/Reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationDataService _IReservationDataService;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationController(IReservationDataService iReservationDataService, UserManager<IdentityUser> userManager)
        {
            _IReservationDataService = iReservationDataService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetReservations()
        {
            List<Reservation> reservations = await _IReservationDataService.GetReservations();
            return reservations;
        }

        [HttpGet("GetReservationsForUser")]
        public async Task<ActionResult<List<Reservation>>> GetReservationsForUser()
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest();
            }
            return await _IReservationDataService.GetReservationsForUser(userId);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            Reservation reservation = await _IReservationDataService.GetReservation(id);
            return reservation;
        }

        [HttpPut]
        public async Task<ActionResult<Reservation>> PutReservation(Reservation reservation)
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized();
            reservation.User = user;
            return await _IReservationDataService.PutReservation(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized();
            Reservation updatedReservation;
            try
            {
                updatedReservation = await _IReservationDataService.PostReservation(reservation);
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError("ModelError", e.Message);
                return BadRequest(ModelState);
            }
            return updatedReservation;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            Reservation deletedReservation;
            try
            {
                deletedReservation = await _IReservationDataService.DeleteReservation(id);
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError("ModelError", e.Message);
                return BadRequest(ModelState);
            }
            return deletedReservation;
        }
    }
}
