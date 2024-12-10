using System.Text.Json;
using System.Text.RegularExpressions;
using HotelApp.Models.SerializationModels;

namespace HotelApp
{
    internal class Program
    {

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


            DateOnly startDate;
            DateOnly endDate;
            if (!DateOnly.TryParseExact(startDateStr, dateFormat, out startDate))
            {
                Console.WriteLine("Error: invalid start date parameter");
                return;
            }

            if (string.IsNullOrEmpty(endDateStr))
            {
                endDate = startDate;
            }
            else if (!DateOnly.TryParseExact(endDateStr, dateFormat, out endDate))
            {
                Console.WriteLine("Error: invalid end date parameter");
                return;
            }



            Availability(hotelId, roomType, startDate, endDate);
        }

        private static void Availability(string hotelId, string roomType, DateOnly startDate, DateOnly endDate) => Console.WriteLine($"SUCC {startDate} - {endDate}");


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
            HotelsJsonRepository.Deserialize();
            // ProgramLoop();
        }
    }
}
