using System.Threading.Tasks;
using TODOAppBE.Common;
using TODOAppBE.Entities;

namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {
        string Delete(Guid id);
        
        /// <inheritdoc cref="TaskRepository.Get"/>
        TaskEntity Get(Guid id);
        IList<TaskEntity> GetAll();
        TaskEntity Insert(TaskEntity task);

        bool validTaskName(string name);
    }

    public class TaskRepository : ITaskRepository
    {
        private IList<TaskEntity> TaskEntities = new List<TaskEntity>();

        public string Delete(Guid id)
        {
            var entityToRemove = Get(id);
            if (entityToRemove != null)
            {
                if (entityToRemove.Status == Status.Completed)
                    TaskEntities.Remove(entityToRemove);
                else throw new Exception("Task has not been completed yet.");
            }

            return entityToRemove?.Name;
        }

        /// <returns>TaskEntity or null</returns>
        public TaskEntity Get(Guid id)
        {
            return TaskEntities.FirstOrDefault(x => x.Id.Equals(id));
        }

        public IList<TaskEntity> GetAll()
        {
            return TaskEntities;
        }

        public TaskEntity Insert(TaskEntity task)
        {
            if (TaskEntities.Any(x => x.Name == task.Name)) 
                throw new Exception("This task already exists.");

            TaskEntities.Add(task);
            return task;
        }

        public bool validTaskName(string name)
        {
            if (TaskEntities.Any(x => x.Name == name))
                return false;
            return true;
        }
    }
}
