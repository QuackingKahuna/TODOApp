using Shouldly;
using System;
using TODOAppBE.Entities;
using TODOAppBE.Repositories;
using Xunit;

namespace TODOAppBeTests
{
    public class TaskRepoTests
    {
        private readonly TaskRepository _sut;

        public TaskRepoTests()
        {
            _sut = new TaskRepository();
        }

        [Fact]
        public void InsertTask_ExistingName()
        {
            var input = new TaskEntity("Name", 1, TODOAppBE.Common.Status.NotStarted);
            _sut.Insert(input);
            var input2 = new TaskEntity("Name", 2, TODOAppBE.Common.Status.NotStarted);
            
            Should.Throw<Exception>(() => _sut.Insert(input2));
        }

        [Fact]
        public void InsertAndGetTask_ValidName()
        {
            var input = new TaskEntity("Name", 1, TODOAppBE.Common.Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Get("Name");

            Assert.Equivalent(input, res);
        }

        [Fact]
        public void GetTask_InvalidName_ReturnNull()
        {
            var input = new TaskEntity("Name", 1, TODOAppBE.Common.Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Get("Name2");

            Assert.Equivalent(null, res);
        }

        [Fact]
        public void DeleteTask_ValidName()
        {
            var input = new TaskEntity("Name", 1, TODOAppBE.Common.Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Delete("Name");

            Assert.Equivalent("Name", res);
        }

        [Fact]
        public void DeleteTask_InvalidName()
        {
            var input = new TaskEntity("Name", 1, TODOAppBE.Common.Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Delete("Name2");

            Assert.Equivalent(null, res);
        }
    }
}