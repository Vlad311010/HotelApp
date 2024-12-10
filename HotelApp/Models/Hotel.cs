namespace HotelApp.Models
{
    public class Hotel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Room> Rooms => _rooms;


        private Room[] _rooms;
        private Dictionary<string, string> _roomTypes;
        private List<Booking> _bookings = new List<Booking>();

        public Hotel(string id, string name, Dictionary<string, string> roomTypes, IEnumerable<Room> rooms)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id, nameof(id));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentNullException.ThrowIfNull(roomTypes, nameof(roomTypes));
            ArgumentNullException.ThrowIfNull(rooms, nameof(rooms));

            Id = id;
            Name = name;
            _roomTypes = roomTypes;
            _rooms = rooms.ToArray();
        }

        public void AddBooking(Booking booking)
        {
            ArgumentNullException.ThrowIfNull(booking, nameof(booking));

            _bookings.Add(booking);
        }

        public bool IsValidRoomType(string roomType)
        {
            return _roomTypes.ContainsKey(roomType);
        }

        public int GetAvailableRoomCount(string roomType, DateOnly arrival, DateOnly departure)
        {
            if (!IsValidRoomType(roomType)) 
                throw new ArgumentException($"Undefined room type: {roomType}");

            if (arrival > departure)
                throw new ArgumentException("Arrival date can't be later than departure date");

            int matchingRoomsCount = _rooms.Where(room => room.Type == roomType).Count();
            if (matchingRoomsCount == 0)
                return 0;

            int bookedRooms = _bookings.Where(booking => booking.RoomType == roomType && DatesOverlaps(booking.Arrival, booking.Departure, arrival, departure)).Count();
            return matchingRoomsCount - bookedRooms;
        }

        private bool DatesOverlaps(DateOnly arrival01, DateOnly departure01, DateOnly arrival02, DateOnly departure02)
        {
            return departure01 >= arrival02 && arrival01 <= departure02;
        }
    }
}   
