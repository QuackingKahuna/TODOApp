using TODOAppBE.Common;
using TODOAppBE.Extensions;

namespace TODOAppBE.Entities
{
    public class TaskEntity
    {
        public string Name { get; private set; }
        public int Priority { get; private set; }
        public Status Status { get; private set; }

        public TaskEntity(string nameInput, int priority = 1, Status status = Status.NotStarted)
        {
            var name = nameInput.HasValue() ? nameInput : throw new Exception("Task without a name");
            Name = name;
            Priority = priority;
            Status = status;
        }
    }
}
