using Shouldly;
using System;
using TODOAppBE.Common;
using TODOAppBE.Entities;
using Xunit;

namespace TODOAppBeTests
{
    public class TaskEntityTest
    {
        [Fact]
        public void CreateEntity_Valid()
        {
            var entity = new TaskEntity("Name", 2, Status.InProgress);
            
            Assert.Equal("Name", entity.Name);
            Assert.Equal(2, entity.Priority);
            Assert.Equal(Status.InProgress, entity.Status);
        }

        [Fact]
        public void CreateEntity_Invalid_String()
        {
            Should.Throw<Exception>(() => new TaskEntity(""));
        }

        [Fact]
        public void CreateEntity_Invalid_Null()
        {
            Should.Throw<Exception>(() => new TaskEntity(null));
        }
    }
}
