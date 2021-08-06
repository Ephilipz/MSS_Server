using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Reservation
{
    public class ReservationDataAccess : IReservationDataAccess
    {
        private readonly ApplicationContext _context;
        public ReservationDataAccess(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Entities.Reservation> DeleteReservation(int id)
        {
            //get the reservation from the database
            Entities.Reservation reservation = await _context.Reservations.FindAsync(id);

            //if the room exists, delete it
            if (reservation != null)
            {
                _context.Remove(reservation);
            }

            //save changes
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Entities.Reservation> GetReservation(int id)
        {
            //get the reservation from database
            Entities.Reservation reservation = await _context.Reservations.FindAsync(id);
            return reservation;
        }

        public async Task<List<Entities.Reservation>> GetReservations()
        {
            //get the all reservations from database
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Entities.Reservation> PostReservation(Entities.Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            return reservation;
        }

        public async Task<Entities.Reservation> PutReservation(Entities.Reservation reservation)
        {
            _context.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }
}
