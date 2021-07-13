﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataService
{
    public interface IRoomDataService
    {
        public Task<Room> GetRoom(int id);
        public Task<List<Room>> GetRooms();
        public Task<Room> PutRoom(Room room);
        public Task<Room> PostRoom(Room room);
        public Task<Room> DeleteRoom(int id);
    }
}
