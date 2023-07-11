using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core;
using Xunit;

namespace TodoList.Api.UnitTests
{
    public class TodoItemConstructor
    {
        private string _testDescription = "test name";
        private TodoItem? _testItem;

        private TodoItem CreateItem()
        {
            return new TodoItem(_testDescription);
        }

        [Fact]
        public void InitializesName()
        {
            _testItem = CreateItem();

            Assert.Equal(_testDescription, _testItem.Description);
        }

        [Fact]
        public void InitializesPriority()
        {
            _testItem = CreateItem();

            Assert.False(_testItem.IsCompleted);
        }

        [Fact]
        public void InitializesDescriptionNotNull()
        {
            _testItem = CreateItem();

            Assert.NotNull(_testItem.Description);
            Assert.NotNull(_testItem.Id);
        }
    }
}
