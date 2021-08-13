using DataAccess;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Reservation
{
    public class ReservationDataService : IReservationDataService
    {
        private IReservationDataAccess _IReservationDataAccess;

        public ReservationDataService(IReservationDataAccess iReservationDataAccess)
        {
            _IReservationDataAccess = iReservationDataAccess;
        }
        public async Task<Entities.Reservation> DeleteReservation(int id)
        {
            return await _IReservationDataAccess.DeleteReservation(id);
        }

        public async Task<Entities.Reservation> GetReservation(int id)
        {
            return await _IReservationDataAccess.GetReservation(id);
        }

        public async Task<List<Entities.Reservation>> GetReservations()
        {
            return await _IReservationDataAccess.GetReservations();
        }

        public async Task<Entities.Reservation> PostReservation(Entities.Reservation reservation)
        {
            return await _IReservationDataAccess .PostReservation(reservation);
        }

        public async Task<Entities.Reservation> PutReservation(Entities.Reservation reservation)
        {
            return await _IReservationDataAccess.PutReservation(reservation);
        }
    }
}
