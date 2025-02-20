using hotelManagementSystem.Dtos;

namespace hotelManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public HotelDto Hotel { get; set; }    
    }
}
