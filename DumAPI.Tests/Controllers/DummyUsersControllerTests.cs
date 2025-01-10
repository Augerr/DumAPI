using DumAPI.Controllers;
using DumAPI.Persistence.Models;
using DumAPI.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DumAPI.Tests.Controllers
{
    public class DummyUsersControllerTests
    {
        private readonly Mock<UserService> _serviceMock;

        public DummyUsersControllerTests()
        {
            _serviceMock = new Mock<UserService>();
        }

        [Fact]
        public async void GetUser_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new DummyUser { Id = userId, FirstName = "John", LastName = "Doe" };
            _serviceMock.Setup(x => x.Get(userId)).ReturnsAsync(expectedUser);

            var controller = new DummyUsersController(_serviceMock.Object);
            
            // Act
            var result = (await controller.GetDummyUser(userId)).Result as OkObjectResult;
            DummyUser? user = result != null ? result.Value as DummyUser : null;

            // Assert
            Assert.NotNull(user);
            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async void GetUser_ReturnsNull_WhenUserNotExists()
        {
            // Arrange
            var existingUserId = 1;
            var userId = 2;
            var existingUser = new DummyUser { Id = existingUserId, FirstName = "John", LastName = "Doe" };
            _serviceMock.Setup(x => x.Get(existingUserId)).ReturnsAsync(existingUser);

            var controller = new DummyUsersController(_serviceMock.Object);

            // Act
            var result = (await controller.GetDummyUser(userId)).Result as OkObjectResult;
            DummyUser? user = result != null ? result.Value as DummyUser : null;

            // Assert
            Assert.Null(user);
        }
    }
}
