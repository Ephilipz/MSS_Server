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

            //if the room exists and is not in used, delete it
            if(room != null && !room.isInUse)
            {
                _context.Remove(room);
            }

            //save changes
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
            await _context.Rooms.AddAsync(room);
            return room;
        }

        public async Task<Room> PutRoom(Room room)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
