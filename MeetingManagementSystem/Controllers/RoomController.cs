using DataService;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomDataService _IRoomDataService;

        public RoomController(IRoomDataService iRoomDataService)
        {
            _IRoomDataService = iRoomDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetRooms()
        {
            List<Room> rooms = await _IRoomDataService.GetRooms();
            return rooms;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            Room room = await _IRoomDataService.GetRoom(id);
            return room;
        }

        [HttpPut]
        public async Task<ActionResult<Room>> PutRoom(Room room)
        {
            return await _IRoomDataService.PutRoom(room);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            Room updatedRoom = await _IRoomDataService.PostRoom(room);
            if (updatedRoom == null)
            {
                return BadRequest("Unable To Update Room");
            }
            return updatedRoom;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            try
            {
                Room deletedRoom = await _IRoomDataService.DeleteRoom(id);
                if (deletedRoom == null)
                {
                    return BadRequest("Unable To Delete Room");
                }
                return deletedRoom;
            }
            catch (Exception e)
            {
                ModelState.TryAddModelError("ModelError", e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
