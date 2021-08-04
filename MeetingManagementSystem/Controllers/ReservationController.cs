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
    [Route("api/Room")]
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
        public async Task<ActionResult<Reservation>> PutReservation(int id, List<Client> addClients, List<Client> removeClients, DateTime StartDateTime, DateTime EndDateTime)
        {
            return await _IReservationDataService.PutReservation(id, addClients, removeClients, StartDateTime, EndDateTime);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            Reservation updatedReservation = await _IReservationDataService.PostReservation(reservation);
            if (updatedReservation == null)
            {
                return BadRequest("Unable To Update Reservation");
            }
            return updatedReservation;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            Reservation deletedReservation = await _IReservationDataService.DeleteReservation(id);
            if (deletedReservation == null)
            {
                return BadRequest("Unable To Delete Reservation");
            }
            return deletedReservation;
        }
    }
}
