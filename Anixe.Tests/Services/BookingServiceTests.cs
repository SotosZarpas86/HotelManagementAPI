using Anixe.Business;
using Anixe.Business.Services;
using Anixe.Core.Entities;
using Anixe.Core.Interfaces;
using Anixe.Core.Models;
using AutoMapper;
using Moq;

namespace Anixe.Tests.Services
{
    [TestClass]
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly IMapper _mapper;
        private const string _hotelName = "Hilton";

        public BookingServiceTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mapper = MapperHelper.GetMapper();
        }

        [TestMethod]
        public async Task Given_IRequestForBookingsForAHotel_When_GetAllByHotel_Then_ReturnsListOfBookings()
        {
            //Arrange
            var response = new List<Booking>
            {
                new Booking { Id = 1, CustomerName = "Customer 1", HotelId = 1, NumberOfPAX = 3 },
                new Booking { Id = 2, CustomerName = "Customer 2", HotelId = 1, NumberOfPAX = 2 },
                new Booking { Id = 3, CustomerName = "Customer 3", HotelId = 1, NumberOfPAX = 6 }
            };
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task Given_IRequestForBookingsForAHotelWithNoBookings_When_GetAllByHotel_Then_ReturnsEmptyListOfBookings()
        {
            //Arrange
            var response = new List<Booking>();
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Given_IRequestForBookingsForANonExistingHotel_When_GetAllByHotel_Then_ReturnsNull()
        {
            //Arrange
            var response = new List<Booking>();
            _mockBookingRepository.Setup(ts => ts.GetAllByHotel(_hotelName)).ReturnsAsync(response);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.GetAllByHotel(_hotelName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task Given_ICreateABookingForAnExistingHotel_When_Create_Then_ReturnsNewBooking()
        {
            //Arrange
            var bookingModel = new BookingCreateModel
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            var bookingEntity = new Booking
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            _mockBookingRepository.Setup(ts => ts.Create(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Create(bookingModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingModel.HotelId, result.HotelId);
            Assert.AreEqual(bookingModel.CustomerName, result.CustomerName);
            Assert.AreEqual(bookingModel.NumberOfPAX, result.NumberOfPAX);
            Assert.AreEqual(bookingEntity.Id, result.Id);
        }

        [TestMethod]
        public async Task Given_ICreateABookingForANonExistingHotel_When_Create_Then_ReturnsNull()
        {
            //Arrange
            var bookingModel = new BookingCreateModel
            {
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            Booking booking = null;
            _mockBookingRepository.Setup(ts => ts.Create(It.IsAny<Booking>())).ReturnsAsync(booking);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Create(bookingModel);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Given_IUpdateAnExistingBookingForAnExistingHotel_When_Update_Then_ReturnsUpdatedHotel()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            var bookingEntity = new Booking
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookingModel.HotelId, result.HotelId);
            Assert.AreEqual(bookingModel.CustomerName, result.CustomerName);
            Assert.AreEqual(bookingModel.NumberOfPAX, result.NumberOfPAX);
            Assert.AreEqual(bookingEntity.Id, result.Id);
        }

        [TestMethod]
        public async Task Given_IUpdateABookingForANonExistingHotel_When_Update_Then_ReturnsNull()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            Booking bookingEntity = null;
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Given_IUpdateANonExistingBookingForAnExistingHotel_When_Update_Then_ReturnsNull()
        {
            //Arrange
            var bookingModel = new BookingModel
            {
                Id = 1,
                HotelId = 1,
                CustomerName = "Customer Name",
                NumberOfPAX = 4
            };
            Booking bookingEntity = null;
            _mockBookingRepository.Setup(ts => ts.Update(It.IsAny<Booking>())).ReturnsAsync(bookingEntity);
            var service = new BookingService(_mockBookingRepository.Object, _mapper);

            //Act
            var result = await service.Update(bookingModel);

            //Assert
            Assert.IsNull(result);
        }
    }
}
