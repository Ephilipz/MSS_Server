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
            Entities.Reservation reservation = await _context.Reservations.AsNoTracking().Where(reservation => reservation.Id == id)
                .Include(reservation => reservation.Room)
                .Include(reservation => reservation.User)
                .FirstAsync();
            return reservation;
        }

        public async Task<List<Entities.Reservation>> GetReservations()
        {
            //get the all reservations from database
            return await _context.Reservations.AsNoTracking().Include(reservation => reservation.Room).ToListAsync();
        }

        public async Task<List<Entities.Reservation>> GetReservationsForUser(string userId)
        {
            return await _context.Reservations.AsNoTracking().Where(reservation => reservation.User.Id == userId).Include(reservation => reservation.Room).AsSplitQuery().ToListAsync();
        }

        public async Task<Entities.Reservation> PostReservation(Entities.Reservation reservation)
        {
            bool existingReservationInTimeRange = await _context.Reservations
                .Where(res => res.Room == reservation.Room)
                .AnyAsync(res => res.StartDateTime == reservation.StartDateTime);
            if (existingReservationInTimeRange)
                throw new InvalidOperationException("A reservation has been made on this room during this period");
            _context.Entry(reservation.Room).State = EntityState.Unchanged;
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
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
