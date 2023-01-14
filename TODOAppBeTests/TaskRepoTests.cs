using Shouldly;
using System;
using System.Collections.Generic;
using TODOAppBE.Common;
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
        public void DeleteTask_StatusCompleted_ReturnsName()
        {
            var input = new TaskEntity("Name", 1, Status.Completed);
            _sut.Insert(input);

            var res = _sut.Delete(input.Id);

            Assert.Equivalent("Name", res);
        }

        [Fact]
        public void DeleteTask_StatusIsNotCompleted_Fails()
        {
            var input = new TaskEntity("Name", 1, Status.NotStarted);
            _sut.Insert(input);

            Should.Throw<Exception>(() => _sut.Delete(input.Id));
        }

        [Fact]
        public void DeleteTask_FakeId_ReturnsNull()
        {
            var input = new TaskEntity("Name", 1, Status.Completed);
            _sut.Insert(input);

            var res = _sut.Delete(Guid.NewGuid());

            Assert.Equivalent(null, res);
        }

        [Fact]
        public void InsertTask_ExistingName_Fails()
        {
            var input = new TaskEntity("Name", 1, Status.NotStarted);
            _sut.Insert(input);
            var input2 = new TaskEntity("Name", 2, Status.NotStarted);
            
            Should.Throw<Exception>(() => _sut.Insert(input2));
        }

        [Fact]
        public void InsertAndGetTask()
        {
            var input = new TaskEntity("Name", 1, Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Get(input.Id);

            Assert.Equivalent(input, res);
        }

        [Fact]
        public void GetTask_FakeId_ReturnsNull()
        {
            var input = new TaskEntity("Name", 1, Status.NotStarted);
            _sut.Insert(input);

            var res = _sut.Get(Guid.NewGuid());

            Assert.Equivalent(null, res);
        }

        [Fact]
        public void GetAll_ReturnsAllTasks()
        {
            var input = new TaskEntity("Name", 1, Status.NotStarted);
            var input2 = new TaskEntity("Name2", 2, Status.NotStarted);
            var allTasks = new List<TaskEntity>
            {
                input, input2
            };
            _sut.Insert(input);
            _sut.Insert(input2);

            var res = _sut.GetAll();

            Assert.Equivalent(allTasks, res);
        }
    }
}