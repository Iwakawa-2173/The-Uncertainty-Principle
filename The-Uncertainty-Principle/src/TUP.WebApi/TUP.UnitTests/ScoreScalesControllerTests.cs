using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TUP.WebApi.Controllers;
using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Xunit;

namespace TUP.WebApi.Tests.Controllers
{
    public class ScoreScalesControllerTests
    {
        private readonly Mock<IRepository<ScoreScale>> _mockRepo;
        private readonly ScoreScalesController _controller;

        public ScoreScalesControllerTests()
        {
            _mockRepo = new Mock<IRepository<ScoreScale>>();
            _controller = new ScoreScalesController(new Mock<ILogger<ScoreScalesController>>().Object, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllScoreScales()
        {
            // Arrange
            var scales = new List<ScoreScale> { new ScoreScale { ScaleId = 1 } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(scales);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnScales = Assert.IsType<List<ScoreScale>>(okResult.Value);
            Assert.Equal(1, returnScales.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenScaleDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ScoreScale)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Insert_ReturnsOk_WhenScaleIsAdded()
        {
            // Arrange
            var scale = new ScoreScale { Scale1 = 0, Scale2 = 0, Scale3 = 0 };
            _mockRepo.Setup(repo => repo.InsertAsync(scale)).ReturnsAsync(scale);

            // Act
            var result = await _controller.Insert(scale);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(scale, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenScaleDoesNotExist()
        {
            // Arrange
            var scale = new ScoreScale { Scale1 = 10 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ScoreScale)null);

            // Act
            var result = await _controller.Update(1, scale);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenScaleIsDeleted()
        {
            // Arrange
            var scaleId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(scaleId)).ReturnsAsync(new ScoreScale());

            // Act
            var result = await _controller.Delete(scaleId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}