using HotelApp.Models;

namespace HotelAppTests
{
    [TestClass]
    public class HotelTests
    {
        Hotel _hotel;

        [TestInitialize]
        public void Init()
        {

            Room _sglRoom01 = new Room("101", "SGL");
            Room _sglRoom02 = new Room("102", "SGL");
            Room _dblRoom01 = new Room("201", "DBL");
            Room _dblRoom02 = new Room("202", "DBL");

            Dictionary<string, string> roomTypes = new Dictionary<string, string>()
            {
                { "SGL", "Single Room"},
                { "DBL", "Double Room"}
            };

            _hotel = new Hotel("H1", "Hotel California", roomTypes, new[] { _sglRoom01, _sglRoom02, _dblRoom01, _dblRoom02 });

            Booking _booking01 = new Booking(new DateOnly(2024, 09, 01), new DateOnly(2024, 09, 03), "DBL", "Prepaid");
            Booking _booking02 = new Booking(new DateOnly(2024, 09, 02), new DateOnly(2024, 09, 05), "SGL", "Standart");
            Booking _booking03 = new Booking(new DateOnly(2024, 09, 02), new DateOnly(2024, 09, 05), "SGL", "Standart");
            Booking _booking04 = new Booking(new DateOnly(2024, 09, 01), new DateOnly(2024, 09, 03), "SGL", "Standart");
            Booking _booking05 = new Booking(new DateOnly(2024, 09, 07), new DateOnly(2024, 09, 09), "SGL", "Standart");

            _hotel.AddBooking(_booking01);
            _hotel.AddBooking(_booking02);
            _hotel.AddBooking(_booking03);
            _hotel.AddBooking(_booking04);
            _hotel.AddBooking(_booking05);
        }

        [TestMethod]
        [DataRow("DBL")]
        [DataRow("SGL")]
        public void IsValidRoomTypePositive(string roomType)
        {
            bool isValid = _hotel.IsValidRoomType(roomType);
            Assert.IsTrue(isValid, $"Expected: true for {roomType}");
        }

        [TestMethod]
        [DataRow("DBLL")]
        [DataRow("")]
        public void IsValidRoomTypeNegative(string roomType)
        {
            bool isValid = _hotel.IsValidRoomType(roomType);
            
            Assert.IsFalse(isValid, $"Expected: false for {roomType}");
        }

        [TestMethod] 
        public void RoomCountDBL()
        {
            DateOnly startDate = new DateOnly(2024, 09, 02);
            DateOnly endDate = new DateOnly(2024, 09, 03);
            
            int result = _hotel.GetAvailableRoomCount("DBL", startDate, endDate);
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RoomCountSGLFrom02To03()
        {
            DateOnly startDate = new DateOnly(2024, 09, 02);
            DateOnly endDate = new DateOnly(2024, 09, 03);

            int result = _hotel.GetAvailableRoomCount("SGL", startDate, endDate);

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void RoomCountSGLFrom06To08()
        {
            DateOnly startDate = new DateOnly(2024, 09, 06);
            DateOnly endDate = new DateOnly(2024, 09, 08);

            int result = _hotel.GetAvailableRoomCount("SGL", startDate, endDate);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RoomCountSGLFrom09To09()
        {
            DateOnly startDate = new DateOnly(2024, 09, 09);
            DateOnly endDate = new DateOnly(2024, 09, 09);

            int result = _hotel.GetAvailableRoomCount("SGL", startDate, endDate);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RoomCountSGLFrom10To10()
        {
            DateOnly startDate = new DateOnly(2024, 09, 10);
            DateOnly endDate = new DateOnly(2024, 09, 10);

            int result = _hotel.GetAvailableRoomCount("SGL", startDate, endDate);

            Assert.AreEqual(2, result);
        }
    }
}