using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class RoomDataService : IRoomDataService
    {
        private IRoomDataAccess _IRoomDataAccess;

        public RoomDataService(IRoomDataAccess iRoomDataAccess)
        {
            _IRoomDataAccess = iRoomDataAccess;
        }

        public async Task<Room> DeleteRoom(int id)
        {
            return await _IRoomDataAccess.DeleteRoom(id);
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _IRoomDataAccess.GetRoom(id);
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _IRoomDataAccess.GetRooms();
        }

        public async Task<Room> PostRoom(Room room)
        {
            return await _IRoomDataAccess.PostRoom(room);
        }

        public async Task<Room> PutRoom(Room room)
        {
            return await _IRoomDataAccess.PutRoom(room);
        }
    }
}
