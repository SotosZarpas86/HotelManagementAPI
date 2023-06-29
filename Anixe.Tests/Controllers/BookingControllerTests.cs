using Anixe.API.Controllers;
using Anixe.Core.Models;
using AnixeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Anixe.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        private readonly Mock<IBookingService> _mockBookingService;

        public BookingControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
        }

        [TestMethod]
        public async Task Given_IRequestForAllBookingForAHotel_When_GetAll_Then_ReturnsOkWithListOfBookings()
        {
            //Arrange
            var response = new List<BookingModel>
            {
                new BookingModel { Id = 1, CustomerName = "Customer 1", HotelId = 1, NumberOfPAX = 5 },
                new BookingModel { Id = 2, CustomerName = "Customer 2", HotelId = 1, NumberOfPAX = 3 },
            };
            _mockBookingService.Setup(ts => ts.GetAllByHotel(It.IsAny<string>())).ReturnsAsync(response);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.GetAllByHotel("hotel");

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as IList<BookingModel>;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(2, values.Count);
        }

        [TestMethod]
        public async Task Given_IRequestForAllBookingForANonExistingHotel_When_GetAll_Then_ReturnsNotFound()
        {
            //Arrange
            List<BookingModel> response = null;
            _mockBookingService.Setup(ts => ts.GetAllByHotel(It.IsAny<string>())).ReturnsAsync(response);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.GetAllByHotel("hotel");

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Hotel not found", objectResult.Value);
        }

        [TestMethod]
        public async Task Given_ICreateABookingForAHotel_When_Create_Then_ReturnsOkWithNewBooking()
        {
            //Arrange
            var createBooking = new BookingCreateModel
            {
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            var booking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            _mockBookingService.Setup(ts => ts.Create(It.IsAny<BookingCreateModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Create(createBooking);

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as BookingModel;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Customer 1", values.CustomerName);
            Assert.AreEqual(1, values.HotelId);
            Assert.AreEqual(5, values.NumberOfPAX);
            Assert.AreEqual(1, values.Id);
        }

        [TestMethod]
        public async Task Given_ICreateABookingForNonExistingHotel_When_Create_Then_ReturnsNotFound()
        {
            //Arrange
            var createBooking = new BookingCreateModel
            {
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Create(It.IsAny<BookingCreateModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Create(createBooking);

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Hotel not found", objectResult.Value);
        }

        [TestMethod]
        public async Task Given_IUpdateBookingWithDifferentId_When_Update_Then_ReturnsBadRequest()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(2, updateBooking);

            //Assert
            var objectResult = result.Result as BadRequestResult;
            Assert.AreEqual(400, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Given_IUpdateABookingForAHotel_When_Update_Then_ReturnsOkWithUpdatedBooking()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            var booking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(1, updateBooking);

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as BookingModel;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Customer 1", values.CustomerName);
            Assert.AreEqual(1, values.HotelId);
            Assert.AreEqual(5, values.NumberOfPAX);
            Assert.AreEqual(1, values.Id);
        }

        [TestMethod]
        public async Task Given_IUpdateBookingForNonExistinghotel_When_Update_Then_ReturnsNotFound()
        {
            //Arrange
            var updateBooking = new BookingModel
            {
                Id = 1,
                CustomerName = "Customer 1",
                HotelId = 1,
                NumberOfPAX = 5
            };
            BookingModel booking = null;
            _mockBookingService.Setup(ts => ts.Update(It.IsAny<BookingModel>())).ReturnsAsync(booking);
            var controller = new BookingController(_mockBookingService.Object);

            //Act
            var result = await controller.Update(1, updateBooking);

            //Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, objectResult.StatusCode);
            Assert.AreEqual("Resource not found", objectResult.Value);
        }
    }
}
