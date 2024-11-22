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
    public class PlayersControllerTests
    {
        private readonly Mock<IRepository<Player>> _mockRepo;
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _mockRepo = new Mock<IRepository<Player>>();
            _controller = new PlayersController(new Mock<ILogger<PlayersController>>().Object, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllPlayers()
        {
            // Arrange
            var players = new List<Player> { new Player { PlayerId = 1, UniquePlayerName = "Player1" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(players);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPlayers = Assert.IsType<List<Player>>(okResult.Value);
            Assert.Equal(1, returnPlayers.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Player)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Insert_ReturnsOk_WhenPlayerIsAdded()
        {
            // Arrange
            var player = new Player { UniquePlayerName = "Player1" };
            _mockRepo.Setup(repo => repo.InsertAsync(player)).ReturnsAsync(player);

            // Act
            var result = await _controller.Insert(player);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(player, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            var player = new Player { UniquePlayerName = "UpdatedPlayer" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Player)null);

            // Act
            var result = await _controller.Update(1, player);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenPlayerIsDeleted()
        {
            // Arrange
            var playerId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(playerId)).ReturnsAsync(new Player());
            
            // Act
            var result = await _controller.Delete(playerId);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}