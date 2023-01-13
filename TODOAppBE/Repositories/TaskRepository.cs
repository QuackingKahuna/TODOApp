using TODOAppBE.Common;
using TODOAppBE.Entities;

namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {
        string Delete(string taskName);
        TaskEntity Get(string taskName);
        TaskEntity Insert(TaskEntity task);
    }

    public class TaskRepository : ITaskRepository
    {
        private IList<TaskEntity> TaskEntities = new List<TaskEntity>();

        public string Delete(string taskName)
        {
            var entityToRemove = Get(taskName);
            if (entityToRemove != null)
            {
                if (entityToRemove.Status == Status.Completed)
                    TaskEntities.Remove(entityToRemove);
                else throw new Exception("Task has not been completed yet.");
            }
            else throw new Exception($"There is not any task: {taskName}");

            return entityToRemove.Name;
        }

        public TaskEntity Get(string taskName)
        {
            return TaskEntities.FirstOrDefault(x => x.Name.Equals(taskName));
        }

        public TaskEntity Insert(TaskEntity task)
        {
            if (TaskEntities.Any(x => x.Name == task.Name)) 
                throw new Exception("This task already exists.");

            TaskEntities.Add(task);
            return task;
        }
    }
}
