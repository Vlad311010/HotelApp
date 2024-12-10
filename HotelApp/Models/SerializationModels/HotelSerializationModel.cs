namespace HotelApp.Models.SerializationModels
{
    internal class HotelSerializationModel
    {
        internal class RoomType
        {
            public string? Code { get; set; }
            public string? Description { get; set; }
        }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public RoomType[]? RoomTypes { get; set; }
        public RoomSerializationModel[]? Rooms { get; set; }

        public Hotel AsHotel()
        {
            Dictionary<string, string> roomTypes = new Dictionary<string, string>();
            foreach (RoomType type in RoomTypes!)
            {
                roomTypes[type.Code!] = type.Description!;
            }

            return new Hotel(Id!, Name!, roomTypes, Rooms!.Select(roomSM => roomSM.AsRoom()));
        }
    }
}
