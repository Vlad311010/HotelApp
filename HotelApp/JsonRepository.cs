using HotelApp.Models;
using HotelApp.Models.SerializationModels;
using System.Text.Json;

namespace HotelApp
{
    internal static class JsonRepository
    {
        private static readonly string _hotelsFilePath = "./Resources/hotels.json";
        private static readonly string _bookingFilePath = "./Resources/bookings.json";

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static HotelsCatalog Deserialize()
        {
            HotelsCatalog hotelsCatalog;

            // read hotels data
            using (var reader = new StreamReader(_hotelsFilePath))
            {
                string json = reader.ReadToEnd();
                HotelSerializationModel[]? hotelSerializationModels = JsonSerializer.Deserialize<HotelSerializationModel[]>(json, _options);
                if (hotelSerializationModels == null)
                    throw new JsonException("Can't parse hotels data");

                hotelsCatalog = new HotelsCatalog(hotelSerializationModels.Select(hotelSM => hotelSM.AsHotel()));
            }
            
            // read booking data
            using (var reader = new StreamReader(_bookingFilePath))
            {
                string json = reader.ReadToEnd();
                BookingSerializationModel[]? bookingsSerializationModels = JsonSerializer.Deserialize<BookingSerializationModel[]>(json, _options);
                if (bookingsSerializationModels == null)
                    throw new JsonException("Can't parse booking data");

                foreach (var bookingSM in bookingsSerializationModels)
                {
                    if (!hotelsCatalog.HasHotel(bookingSM.HotelId))
                        throw new ArgumentException($"Undefined hotel id \"{bookingSM.HotelId}\"");

                    hotelsCatalog[bookingSM.HotelId].AddBooking(bookingSM.AsBooking()); 
                }

            }

            return hotelsCatalog;
        }
    }
}
