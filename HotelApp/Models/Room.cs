namespace HotelApp.Models
{
    internal class Room
    {
        public string Id { get; private set; }
        public string Type { get; private set; }

        public Room(string id, string type)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id, nameof(id));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(type, nameof(type));

            Id = id;
            Type = type;
        }
    }
}
