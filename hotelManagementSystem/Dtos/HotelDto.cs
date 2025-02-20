namespace hotelManagementSystem.Dtos
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}
