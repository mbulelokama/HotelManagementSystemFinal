using hotelManagementSystem.Context;
using hotelManagementSystem.Dtos;
using hotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hotelManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelBookingController : ControllerBase
    {
        private readonly HotelBookingContext _context;
        private readonly ILogger<HotelBookingController> _logger;

        public HotelBookingController(HotelBookingContext context, ILogger<HotelBookingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("rooms")]
        public async Task<ActionResult<RoomDto>> AddRoom(RoomDto room)
        {
            var hotelExists = await _context.Hotels.AnyAsync(h => h.Id == room.Id);
            if (!hotelExists)
            {
                return BadRequest("Invalid HotelId. The specified hotel does not exist.");
            }



            try
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error adding room");
                return StatusCode(500, "An error occurred while adding the room.");
            }

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpGet("hotels/{hotelId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms(int hotelId)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return rooms;
        }


        [HttpGet("hotels")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            return await _context.Hotels.Include(h => h.Rooms).ToListAsync();
        }

        [HttpGet("rooms/{hotelId}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoom(int hotelId)
        {
            return await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
        }

        [HttpGet("availability/{roomId}/{checkIn}/{checkOut}")]
        public async Task<ActionResult<bool>> CheckAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var isAvailable = !await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                b.CheckInDate <= checkOut &&
                b.CheckOutDate >= checkIn);
            return isAvailable;
        }

        [HttpPost("bookings")]
        public async Task<ActionResult<BookingDto>> CreateBooking(BookingDto booking)
        {
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == booking.RoomId);
            if (!roomExists)
            {
                return BadRequest("Invalid RoomId.  The specified room does not exist.");
            }

            var isAvailable = !await _context.Bookings.AnyAsync(b =>
                b.RoomId == booking.RoomId &&
                b.CheckInDate <= booking.CheckOutDate &&
                b.CheckOutDate >= booking.CheckInDate);
            if (!isAvailable)
            {
                return BadRequest("Room is not available for the selected dates.");
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpGet("bookings/{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        [HttpDelete("bookings/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
