namespace HotelApp.Models.SerializationModels
{
    internal class HotelSerializationModel
    {
        internal class RoomType
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public RoomSerializationModel[] Rooms { get; set; }
        public RoomType[] RoomTypes { get; set; }
    }
}
