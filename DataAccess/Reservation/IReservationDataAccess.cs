using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace DataAccess
{
    public interface IReservationDataAccess
    {
        Task<Entities.Reservation> PostReservation(Entities.Reservation reservation);
        Task<Entities.Reservation> PutReservation(Entities.Reservation reservation);
        Task<Entities.Reservation> DeleteReservation(int id);
        Task<Entities.Reservation> GetReservation(int id);
        Task<List<Entities.Reservation>> GetReservations();
    }
}
