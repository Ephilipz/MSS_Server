using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Reservation
{
    public interface IReservationDataService
    {
        Task<Entities.Reservation> PostReservation(Entities.Reservation reservation);
        Task<Entities.Reservation> PutReservation(Entities.Reservation reservation);
        Task<Entities.Reservation> DeleteReservation(int id);
        Task<Entities.Reservation> GetReservation(int id);
        Task<List<Entities.Reservation>> GetReservations();
        Task<List<Entities.Reservation>> GetReservationsForUser(string userId);
    }
}
