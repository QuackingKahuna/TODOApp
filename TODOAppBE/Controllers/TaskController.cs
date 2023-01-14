using Microsoft.AspNetCore.Mvc;
using TODOAppBE.Common;
using TODOAppBE.Contracts;
using TODOAppBE.Entities;
using TODOAppBE.Repositories;

namespace TODOAppBE.Controllers
{
    public interface ITaskController
    {
        string Delete(Guid id);
        bool Edit(TaskDto dto);
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

        [HttpDelete]
        public string Delete(Guid id)
        {
            var name = _taskRepository.Delete(id);
            return name;
        }

        [HttpPost]
        public bool Edit(TaskDto dto)
        {
            var entity = _taskRepository.Get(dto.Id);
            if(entity != null)
            {
                if(dto.Status != entity.Status)
                {
                    switch(dto.Status)
                    {
                        case Status.NotStarted:
                            entity.NotStarted();
                            break;
                        case Status.InProgress:
                            entity.InProgress(); 
                            break;
                        case Status.Completed: 
                            entity.Completed(); 
                            break;
                    }
                }
                if(dto.Priority != entity.Priority)
                    entity.ChangePriority(dto.Priority);
                if(dto.Name != entity.Name)
                    entity.Rename(dto.Name);
            }
            return true;
        }

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