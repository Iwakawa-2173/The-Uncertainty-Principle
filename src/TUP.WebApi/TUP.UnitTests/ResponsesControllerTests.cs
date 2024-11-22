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
    public class ResponsesControllerTests
    {
        private readonly Mock<IRepository<Response>> _mockRepo;
        private readonly ResponsesController _controller;

        public ResponsesControllerTests()
        {
          _mockRepo = new Mock<IRepository<Response>>();
          _controller = new ResponsesController(new Mock<ILogger<ResponsesController>>().Object, _mockRepo.Object);
      }

      [Fact]
      public async Task Get_ReturnsAllResponses()
      {
          // Arrange 
          var responses = new List<Response> { new Response { ResponseId = 1, ResponseOption = 2 } }; 
          _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(responses); 

          // Act 
          var result = await _controller.Get(); 

          // Assert 
          var okResult = Assert.IsType<OkObjectResult>(result.Result); 
          var returnResponses = Assert.IsType<List<Response>>(okResult.Value); 
          Assert.Equal(1, returnResponses.Count); 
      }

      [Fact]
      public async Task Get_ReturnsNotFound_WhenResponseDoesNotExist()
      {
          // Arrange 
          _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Response)null); 

          // Act 
          var result = await _controller.Get(1); 

          // Assert 
          Assert.IsType<NotFoundResult>(result.Result); 
      }

      [Fact]
      public async Task Insert_ReturnsOk_WhenResponseIsAdded()
      {
          // Arrange 
          var response = new Response { PlayerId = 1, EventId = 1, ResponseOption = 2 }; 
          _mockRepo.Setup(repo => repo.InsertAsync(response)).ReturnsAsync(response); 

          // Act 
          var result = await _controller.Insert(response); 

          // Assert 
          var okResult = Assert.IsType<OkObjectResult>(result); 
          Assert.Equal(response, okResult.Value); 
      }

      [Fact]
      public async Task Update_ReturnsNotFound_WhenResponseDoesNotExist()
      {
          // Arrange 
          var response = new Response { ResponseOption = 3 }; 
          _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Response)null); 

          // Act 
          var result = await _controller.Update(1, response); 

          // Assert 
          Assert.IsType<NotFoundResult>(result); 
      }

      [Fact]
      public async Task Delete_ReturnsOk_WhenResponseIsDeleted()
      {
          // Arrange 
          var responseId = 1; 
          _mockRepo.Setup(repo => repo.GetByIdAsync(responseId)).ReturnsAsync(new Response()); 

          // Act 
          var result = await _controller.Delete(responseId); 

          // Assert 
          Assert.IsType<OkResult>(result); 
      }
   }
}