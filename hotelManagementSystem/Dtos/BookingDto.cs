namespace hotelManagementSystem.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
