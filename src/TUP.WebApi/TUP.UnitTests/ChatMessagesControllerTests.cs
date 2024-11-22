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
    public class ChatMessagesControllerTests
    {
        private readonly Mock<IRepository<ChatMessage>> _mockRepo;
        private readonly ChatMessagesController _controller;

        public ChatMessagesControllerTests()
        {
            _mockRepo = new Mock<IRepository<ChatMessage>>();
            _controller = new ChatMessagesController(new Mock<ILogger<ChatMessagesController>>().Object, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllChatMessages()
        {
            // Arrange
            var messages = new List<ChatMessage> { new ChatMessage { MessageId = 1, Message = "Hello" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(messages);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnMessages = Assert.IsType<List<ChatMessage>>(okResult.Value);
            Assert.Equal(1, returnMessages.Count);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenMessageDoesNotExist()
        {
           // Arrange 
           _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ChatMessage)null); 

           // Act 
           var result = await _controller.Get(1); 

           // Assert 
           Assert.IsType<NotFoundResult>(result.Result); 
       }

       [Fact]
       public async Task Insert_ReturnsOk_WhenMessageIsAdded()
       {
           // Arrange 
           var message = new ChatMessage { Message = "New Message" }; 
           _mockRepo.Setup(repo => repo.InsertAsync(message)).ReturnsAsync(message); 

           // Act 
           var result = await _controller.Insert(message); 

           // Assert 
           var okResult = Assert.IsType<OkObjectResult>(result); 
           Assert.Equal(message, okResult.Value); 
       }

       [Fact]
       public async Task Update_ReturnsNotFound_WhenMessageDoesNotExist()
       {
           // Arrange 
           var message = new ChatMessage { MessageId = 1, Message = "Updated Message" }; 
           _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((ChatMessage)null); 

           // Act 
           var result = await _controller.Update(1, message); 

           // Assert 
           Assert.IsType<NotFoundResult>(result); 
       }

       [Fact]
       public async Task Delete_ReturnsOk_WhenMessageIsDeleted()
       {
           // Arrange 
           var messageId = 1; 
           _mockRepo.Setup(repo => repo.GetByIdAsync(messageId)).ReturnsAsync(new ChatMessage()); 

           // Act 
           var result = await _controller.Delete(messageId); 

           // Assert 
           Assert.IsType<OkResult>(result); 
       }
   }
}