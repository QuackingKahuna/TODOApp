using TODOAppBE.Entities;
using System.Linq;

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
                TaskEntities.Remove(entityToRemove);

            return entityToRemove?.Name;
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
