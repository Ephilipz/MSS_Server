using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Reservation
{
    class ReservationDataAccess : IReservationDataAccess
    {
        private readonly ApplicationContext _context;
        public ReservationDataAccess(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Reservation> DeleteReservation(int id)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(id);

            //if the room exists, delete it
            if (reservation != null)
            {
                _context.Remove(reservation);
            }

            //save changes
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Client> GetClient(int reservationID, int clientID)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(reservationID);

            //if the client is the User, return it. If not, system will check the list of participants
            if (reservation.User.Billing.Id == clientID)
                return await reservation.User;
            else 
            {
                for (IdentityUser user: reservation.Participants) 
                {
                    if (user.Billing.Id == clientID)
                        return await reservation.User;
                }
            }
        }

        public async Task<List<Client>> GetClients(int reservationID)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(reservationID);

            return await reservation.Participants;
        }

        public async Task<DateTime> GetEndDateTime(int idreservationID)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(reservationID);

            return await reservation.EndDateTime;
        }

        public async Task<DateTime> GetStartDateTime(int reservationID)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(reservationID);

            return await reservation.StartDateTime;
        }

        public async Task<Reservation> PostReservation(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> PutReservation(int id, List<Client> addClients, List<Client> removeClients, 
            DateTime StartDateTime, DateTime EndDateTime)
        {
            //get the reservation from the database
            Reservation reservation = await _context.Reservations.FindAsync(id);

            //if addClients / removeClients is not null, the list of participants will be modify
            if (addClients != null) 
            {
                foreach (Client client in addClients)
                    reservation.Participants.Add(client);
            }
            if (removeClients != null) 
            {
                foreach (Client client in removeClients) {
                    if (reservation.Participants.Contains(client))
                        reservation.Participants.Remove(client);
                }
            }

            reservation.StartDateTime = StartDateTime;
            reservation.EndDateTime = EndDateTime;

            _context.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }
}
