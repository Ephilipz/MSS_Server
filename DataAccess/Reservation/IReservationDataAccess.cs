using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Reservations;
using Entities.Users;

namespace DataAccess
{
    interface IReservationDataAccess
    {
        Task<Reservation> PostReservation(Reservation reservation);
        Task<Reservation> PutReservation(int id, List<Client> addClients, List<Client> removeClients, 
            DateTime StartDateTime, DateTime EndDateTime);
        Task<Client> GetClient(int reservationID, int clientID);
        Task<List<Client>> GetClients(int reservationID);
        Task<Reservation> DeleteReservation(int id);
        Task<DateTime> GetStartDateTime(int reservationID);
        Task<DateTime> GetEndDateTime(int idreservationID);
    }
}
