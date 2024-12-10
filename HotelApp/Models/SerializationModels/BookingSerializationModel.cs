namespace HotelApp.Models.SerializationModels
{
    internal class BookingSerializationModel
    {
        public string HotelId { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public string RoomType { get; set; }
        public string RoomRate { get; set; }

        private const string DateFormat = "yyyyMMdd";

        public Booking AsBooking()
        {
            DateOnly arrivalDate;
            DateOnly departureDate;

            if (!DateOnly.TryParseExact(Arrival, DateFormat, out arrivalDate))
                throw new ArgumentException($"Can't parse arrivale date: {Arrival}");
            if (!DateOnly.TryParseExact(Departure, DateFormat, out departureDate))
                throw new ArgumentException($"Can't parse departure date: {departureDate}");

            return new Booking(arrivalDate, departureDate, RoomType, RoomRate);
        }
    }
}
