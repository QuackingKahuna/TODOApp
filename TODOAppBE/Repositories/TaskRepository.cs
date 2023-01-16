using ErrorOr;
using System.Threading.Tasks;
using TODOAppBE.Common;
using TODOAppBE.Entities;
using TODOAppBE.ErrorsCollection;

namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {
        ErrorOr<bool> Delete(Guid id);
        
        /// <inheritdoc cref="TaskRepository.Get"/>
        ErrorOr<TaskEntity> Get(Guid id);
        IList<TaskEntity> GetAll();
        ErrorOr<TaskEntity> Insert(TaskEntity task);

        bool validTaskName(string name);
    }

    public class TaskRepository : ITaskRepository
    {
        private IList<TaskEntity> TaskEntities = new List<TaskEntity>();

        public ErrorOr<bool> Delete(Guid id)
        {
            var entityToRemoveResult = Get(id);
            ErrorOr<bool> res;
            if (entityToRemoveResult.IsError) res = entityToRemoveResult.FirstError;
            else
            {
                if (entityToRemoveResult.Value.Status == Status.Completed)
                {
                    TaskEntities.Remove(entityToRemoveResult.Value);
                    res = true;
                }
                else res = Errors.Task.UncompletedTaskToDelete;
            }
            return res;
        }

        /// <returns>TaskEntity or null</returns>
        public ErrorOr<TaskEntity> Get(Guid id)
        {
            var entity = TaskEntities.FirstOrDefault(x => x.Id.Equals(id));
            return entity != null ? entity : Errors.Task.NotFound;
        }

        public IList<TaskEntity> GetAll()
        {
            return TaskEntities;
        }

        public ErrorOr<TaskEntity> Insert(TaskEntity task)
        {
            if (validTaskName(task.Name))
            {
                TaskEntities.Add(task);
                return task;
            }
            else return Errors.Task.TaskAlreadyExists;
        }

        public bool validTaskName(string name)
        {
            if (TaskEntities.Any(x => x.Name == name))
                return false;
            return true;
        }
    }
}
