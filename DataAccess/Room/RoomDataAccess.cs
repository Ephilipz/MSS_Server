using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomDataAccess : IRoomDataAccess
    {
        private readonly ApplicationContext _context;

        public RoomDataAccess(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Room> DeleteRoom(int id)
        {
            //get the room from the database
            Room room = await _context.Rooms.FindAsync(id);

            bool isInUse = _context.Reservations.Any(reservation => reservation.Room.Id == id);

            if (isInUse)
            {
                throw new InvalidOperationException("Room is in use");
            }

            if (room == null)
            {
                throw new KeyNotFoundException("Room Not Found. It may have been deleted");
            }

            _context.Remove(room);

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> PostRoom(Room room)
        {
            var roomInserted = await _context.AddAsync(room);
            await _context.SaveChangesAsync();
            return roomInserted.Entity;
        }

        public async Task<Room> PutRoom(Room room)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
