using Anixe.API.Controllers;
using Anixe.Core.Models;
using AnixeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Anixe.Tests.Controllers
{
    [TestClass]
    public class HotelControllerTests
    {
        private readonly Mock<IHotelService> _mockHotelService;

        public HotelControllerTests()
        {
            _mockHotelService = new Mock<IHotelService>();
        }

        [TestMethod]
        public async Task Given_IRequestForAllHotels_When_GetAll_Then_ReturnsOkWithListOfHotels()
        {
            //Arrange
            var response = new List<HotelModel>
            {
                new HotelModel { Id = 1, Address = "Address 1", Name = "Name 1", StarRating = 5 },
                new HotelModel { Id = 2, Address = "Address 2", Name = "Name 2", StarRating = 4 },
                new HotelModel { Id = 3, Address = "Address 3", Name = "Name 3", StarRating = default },
            };
            _mockHotelService.Setup(ts => ts.GetAll()).ReturnsAsync(response);
            var controller = new HotelController(_mockHotelService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as IList<HotelModel>;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(3, values.Count);
        }

        [TestMethod]
        public async Task Given_IRequestForHotelsByInput_When_Get_Then_ReturnsOkWithEmptyListOfHotels()
        {
            //Arrange
            var response = new List<HotelModel>();
            _mockHotelService.Setup(ts => ts.Get(It.IsAny<string>())).ReturnsAsync(response);
            var controller = new HotelController(_mockHotelService.Object);

            //Act
            var result = await controller.GetByInput("input");

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as IList<HotelModel>;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(0, values.Count);
        }

        [TestMethod]
        public async Task Given_ICreateNewHotel_When_Create_Then_ReturnsOkWithNewHotel()
        {
            //Arrange
            var createHotelModel = new HotelCreateModel
            {
                Address = "Address",
                Name = "Name",
                StarRating = 1
            };
            var response = new HotelModel
            {
                Id = 1,
                Address = "Address",
                Name = "Name",
                StarRating = 1
            };

            _mockHotelService.Setup(ts => ts.Create(It.IsAny<HotelCreateModel>())).ReturnsAsync(response);
            var controller = new HotelController(_mockHotelService.Object);

            //Act
            var result = await controller.Create(createHotelModel);

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var values = objectResult.Value as HotelModel;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("Address", values.Address);
            Assert.AreEqual("Name", values.Name);
            Assert.AreEqual(1, values.StarRating);
            Assert.AreEqual(1, values.Id);
        }
    }
}
