using TODOAppBE.Entities;

namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {
        TaskEntity Insert(TaskEntity task);
    }

    public class TaskRepository : ITaskRepository
    {
        private List<TaskEntity> TaskEntities = new List<TaskEntity>();

        public TaskEntity Insert(TaskEntity task)
        {
            TaskEntities.Add(task);
            return task;
        }
    }
}
