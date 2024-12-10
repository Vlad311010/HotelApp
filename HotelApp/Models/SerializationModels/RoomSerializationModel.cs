using System.Text.Json.Serialization;

namespace HotelApp.Models.SerializationModels
{
    internal class RoomSerializationModel
    {
        [JsonPropertyName("roomId")] public string Id { get; set; }
        [JsonPropertyName("roomType")] public string Type { get; set; }
    }
}
