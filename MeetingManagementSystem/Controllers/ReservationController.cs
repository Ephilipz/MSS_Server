using DataService;
using DataService.Reservation;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationDataService _IReservationDataService;

        public ReservationController(IReservationDataService iReservationDataService)
        {
            _IReservationDataService = iReservationDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetReservations()
        {
            List<Reservation> reservations = await _IReservationDataService.GetReservations();
            return reservations;
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
            return await _IReservationDataService.PutReservation(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
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
            catch(Exception e)
            {
                ModelState.TryAddModelError("ModelError", e.Message);
                return BadRequest(ModelState);
            }
            return deletedReservation;
        }
    }
}
