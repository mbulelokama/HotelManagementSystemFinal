using hotelManagementSystem.Dtos;
using hotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hotelManagementSystem.Context
{
    public class HotelBookingContext : DbContext
    {
        public HotelBookingContext(DbContextOptions<HotelBookingContext> options) : base(options) { }

        public DbSet<HotelDto> Hotels { get; set; }
        public DbSet<RoomDto> Rooms { get; set; }
        public DbSet<BookingDto> Bookings { get; set; }
    }
}
