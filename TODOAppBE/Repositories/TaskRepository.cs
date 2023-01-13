namespace TODOAppBE.Repositories
{
    public interface ITaskRepository
    {

    }

    public class TaskRepository : ITaskRepository
    {
        public List<Task> Tasks = new List<Task>();

    }
}
