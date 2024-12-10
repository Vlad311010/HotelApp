using HotelApp.Models.SerializationModels;
using System.Text.Json;

namespace HotelApp
{
    internal static class HotelsJsonRepository
    {
        private const string _filePath = "./Resources/hotels.json";

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static void Deserialize()
        {
            using (var reader = new StreamReader(_filePath))
            {
                string json = reader.ReadToEnd();
                HotelSerializationModel[] hotelSerializationModels = JsonSerializer.Deserialize<HotelSerializationModel[]>(json, _options);

                Console.WriteLine(hotelSerializationModels);
            }
        }
    }
}
