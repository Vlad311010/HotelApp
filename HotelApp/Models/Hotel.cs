
namespace HotelApp.Models
{
    internal class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<Room> Rooms { get; set; }


        private Room[] _rooms;
        private Dictionary<string, string> _roomTypes;

        /*public Hotel()
        {

        }*/
    }
}   
