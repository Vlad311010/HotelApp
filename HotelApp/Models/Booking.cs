namespace HotelApp.Models
{
    internal class Booking
    {
        public DateOnly Arrival { get; private set; }
        public DateOnly Departure { get; private set; }
        public string RoomType { get; private set; }
        public string RoomRate { get; private set; }

        public Booking(DateOnly arrival, DateOnly departure, string roomType, string roomRate)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(roomType, nameof(roomType));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(roomRate, nameof(roomRate));
            if (arrival > departure)
                throw new ArgumentException("Arrival date can't be later than departure date");

            Arrival = arrival;
            Departure = departure;
            RoomType = new string(roomType);
            RoomRate = roomRate;
        }
    }
}
