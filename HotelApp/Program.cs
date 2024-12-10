using System.Text.RegularExpressions;
using HotelApp.Models;

namespace HotelApp
{
    internal class Program
    {
        private static HotelsCatalog catalog = default!;

        private static void ParseAvailabilityCommand(string input)
        {
            /// (\w+) - first group matches hotel id
            /// (\d{8}) - second group matches start date
            /// (\d{8}) - fourth group (optional) matches end date
            /// (\w+) - fifth group matches room type
            string availabilityRegex = @"^Availability\((\w+)\s*,\s*(\d{8})\s*(-\s*(\d{8}))?\s*,\s*(\w*)\)\s*$";
            string dateFormat = "yyyyMMdd";

            var match = Regex.Match(input, availabilityRegex);
            if (match.Captures.Count == 0)
            {
                Console.WriteLine("Unrecognized command");
                return;
            }

            string? hotelId = match.Groups[1].Success ? match.Groups[1].Value : null;
            string? startDateStr = match.Groups[2].Success ? match.Groups[2].Value : null;
            string? endDateStr = match.Groups[4].Success ? match.Groups[4].Value : null;
            string? roomType = match.Groups[5].Success ? match.Groups[5].Value : null;


            DateOnly arrivalDate;
            DateOnly departureDate;
            if (!DateOnly.TryParseExact(startDateStr, dateFormat, out arrivalDate))
            {
                Console.WriteLine("Error: invalid start date parameter");
                return;
            }

            if (string.IsNullOrEmpty(endDateStr))
            {
                departureDate = arrivalDate;
            }
            else if (!DateOnly.TryParseExact(endDateStr, dateFormat, out departureDate))
            {
                Console.WriteLine("Error: invalid end date parameter");
                return;
            }

            if (hotelId == null)
            {
                Console.WriteLine("Error: can't parse hotelId");
                return;
            }

            if (roomType == null)
            {
                Console.WriteLine("Error: can't parse roomType");
                return;
            }


            Availability(hotelId, roomType, arrivalDate, departureDate);
        }

        private static void Availability(string hotelId, string roomType, DateOnly arrivalDate, DateOnly departureDate)
        {
            if (!catalog.HasHotel(hotelId))
            {
                Console.WriteLine($"Error: No hotel with id \"{hotelId}\"");
                return;
            }

            if (!catalog[hotelId].IsValidRoomType(roomType))
            {
                Console.WriteLine($"Error: Unknown room type \"{roomType}\"");
                return;
            }

            if (arrivalDate > departureDate)
            {
                Console.WriteLine($"Error: Invalid booking dates range {arrivalDate} - {departureDate}");
                return;
            }

            Console.WriteLine(catalog[hotelId].GetAvailableRoomCount(roomType, arrivalDate, departureDate));
        }


        private static void ProgramLoop()
        {
            Console.WriteLine("Enter command:");
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;

                ParseAvailabilityCommand(input);
            }
        }

        static void Main(string[] args)
        {
            // Availability(H1, 20240901 - 20240903, DBL)
            // Availability(H1, 20240901, SGL)

            catalog = JsonRepository.Deserialize();
            ProgramLoop();
        }
    }
}
