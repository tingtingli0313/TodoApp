using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Core;
using TodoList.Core.Interfaces;
using TodoList.Core.Services;
using Xunit;
using static TodoList.Api.UnitTests.Helper.EfHelper;

namespace TodoList.Api.UnitTests.Services
{
    public class TodoItemService
    {
        private readonly Mock<IToDoContext> _mockRepo = new Mock<IToDoContext>();
        private readonly ToDoItemService _service;

        public TodoItemService()
        {
            _service = new ToDoItemService(_mockRepo.Object);
        }

        [Fact]
        public async Task SetDefaultDataGetItem_Success_Async()
        {
            var item1 = new TodoItem("test1");
            var item2 = new TodoItem("test2");
            var item3 = new TodoItem("test3");

            var mockDbSet = new Mock<DbSet<TodoItem>>();

            // Set up the mock DbSet to return specific data
            var items = new List<TodoItem>{ item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<TodoItem>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<TodoItem>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<TodoItem>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<TodoItem>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);
              
            //mock service
            var result = await _service.GetAllItemsAsync();

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }


        [Fact]
        public async Task SetDefaultData_GetItemById_Success_Async()
        {
            var item1 = new TodoItem("test1") { Id = new Guid()};
            var item2 = new TodoItem("test2");
            var item3 = new TodoItem("test3");

            var mockDbSet = new Mock<DbSet<TodoItem>>();

            // Set up the mock DbSet to return specific data
            var items = new List<TodoItem> { item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<TodoItem>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<TodoItem>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<TodoItem>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<TodoItem>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);

            //mock service
            var result = await _service.GetItemByIdAsyc(item1.Id);

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }


        [Fact]
        public async Task GivenInvalidaId_WithDefaultData_GetItemById_NotFound()
        {
            var item1 = new TodoItem("test1") { };
            var item2 = new TodoItem("test2");
            var item3 = new TodoItem("test3");

            var mockDbSet = new Mock<DbSet<TodoItem>>();

            // Set up the mock DbSet to return specific data
            var items = new List<TodoItem> { item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<TodoItem>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<TodoItem>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<TodoItem>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<TodoItem>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);

            //mock service
            var result = await _service.GetItemByIdAsyc(new Guid());
            Assert.Equal(Ardalis.Result.ResultStatus.NotFound, result.Status);
        }
    }
}
