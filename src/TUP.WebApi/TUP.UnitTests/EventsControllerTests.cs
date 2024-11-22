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
    public class EventsControllerTests
    {
        private readonly Mock<IRepository<Event>> _mockRepo;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _mockRepo = new Mock<IRepository<Event>>();
            _controller = new EventsController(new Mock<ILogger<EventsController>>().Object, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllEvents()
        {
            // Arrange
            var events = new List<Event> { new Event { EventId = 1, Description = "Event1" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(events);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnEvents = Assert.IsType<List<Event>>(okResult.Value);
            Assert.Equal(1, returnEvents.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Event)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Insert_ReturnsOk_WhenEventIsAdded()
        {
           // Arrange 
           var eventItem = new Event { Description = "New Event" }; 
           _mockRepo.Setup(repo => repo.InsertAsync(eventItem)).ReturnsAsync(eventItem); 

           // Act 
           var result = await _controller.Insert(eventItem); 

           // Assert 
           var okResult = Assert.IsType<OkObjectResult>(result); 
           Assert.Equal(eventItem, okResult.Value); 
       }

       [Fact]
       public async Task Update_ReturnsNotFound_WhenEventDoesNotExist()
       {
           // Arrange
           var eventItem = new Event { Description = "Updated Event" };
           _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Event)null);

           // Act
           var result = await _controller.Update(1, eventItem);

           // Assert
           Assert.IsType<NotFoundResult>(result);
       }

       [Fact]
       public async Task Delete_ReturnsOk_WhenEventIsDeleted()
       {
           // Arrange
           var eventId = 1;
           _mockRepo.Setup(repo => repo.GetByIdAsync(eventId)).ReturnsAsync(new Event());

           // Act
           var result = await _controller.Delete(eventId);

           // Assert
           Assert.IsType<OkResult>(result);
       }
   }
}