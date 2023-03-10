using TODOAppBE.Common;
using TODOAppBE.Extensions;

namespace TODOAppBE.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Priority { get; private set; }
        public Status Status { get; private set; }

        public TaskEntity(string nameInput, int priority = 1, Status status = Status.NotStarted)
        {
            var name = nameInput.HasValue() ? nameInput : throw new Exception("Task without a name");

            Id = Guid.NewGuid();
            Name = name;
            Priority = priority;
            Status = status;
        }

        public void Rename(string newName)
        {
            Name = newName;
        }

        public void ChangePriority(int newPriority)
        {
            Priority = newPriority;
        }

        public void NotStarted()
        {
            Status = Status.NotStarted;
        }

        public void InProgress()
        {
            Status = Status.InProgress;
        }

        public void Completed()
        {
            Status = Status.Completed;
        }
    }
}
