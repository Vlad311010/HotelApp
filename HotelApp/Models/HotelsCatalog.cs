namespace HotelApp.Models
{
    internal class HotelsCatalog
    {
        private Dictionary<string, Hotel> _catalog;

        public HotelsCatalog(IEnumerable<Hotel> hotels)
        {
            ArgumentNullException.ThrowIfNull(hotels, nameof(hotels));

            _catalog = new Dictionary<string, Hotel>();
            foreach (Hotel hotel in hotels)
            {
                _catalog[hotel.Id] = hotel;
            }
        }

        public Hotel this[string id]
        {
            get
            {
                if (_catalog.TryGetValue(id, out Hotel? hotel))
                    return hotel;

                throw new KeyNotFoundException($"Hotels catalog does not containe entry with {id} key.");
            }

            private set => _catalog[id] = value;
        }

        public bool HasHotel(string hotelId)
        {
            return _catalog.ContainsKey(hotelId);
        }
    }
}
