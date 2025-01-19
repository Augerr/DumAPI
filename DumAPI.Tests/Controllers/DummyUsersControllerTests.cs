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
    }
}
