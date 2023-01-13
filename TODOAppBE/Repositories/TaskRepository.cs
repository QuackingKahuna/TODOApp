using TODOAppBE.Entities;

namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {
        TaskEntity Get(string taskName);
        TaskEntity Insert(TaskEntity task);
    }

    public class TaskRepository : ITaskRepository
    {
        private List<TaskEntity> TaskEntities = new List<TaskEntity>();

        public TaskEntity Get(string taskName)
        {
            return TaskEntities.FirstOrDefault(x => x.Name.Equals(taskName));
        }

        public TaskEntity Insert(TaskEntity task)
        {
            TaskEntities.Add(task);
            return task;
        }
    }
}
