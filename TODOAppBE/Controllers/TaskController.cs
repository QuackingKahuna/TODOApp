using Microsoft.AspNetCore.Mvc;
using TODOAppBE.Contracts;
using TODOAppBE.Entities;
using TODOAppBE.Repositories;

namespace TODOAppBE.Controllers
{
    public interface ITaskController
    {
        Guid Insert(InsertTaskDto dto);
        TaskDto Get(Guid id);
        IEnumerable<TaskDto> GetAll();
    }
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TaskController : ControllerBase, ITaskController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TaskController( 
            ILogger<TaskController> logger
            , ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        //Note: Should be endpoint async if there is nothing to await?
        [HttpGet]
        public TaskDto Get(Guid id)
        {
            var task = _taskRepository.Get(id);
            return task == null ? null : new TaskDto().Map(task);
        }

        [HttpGet]
        public IEnumerable<TaskDto> GetAll()
        {
            return _taskRepository.GetAll().Select(x => new TaskDto().Map(x));
        }

        [HttpPost]
        public Guid Insert(InsertTaskDto dto)
        {
            var entity = new TaskEntity(dto.Name, dto.Priority, dto.Status);
            entity = _taskRepository.Insert(entity);
            return entity.Id;
        }
    }
}