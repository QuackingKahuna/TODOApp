using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TODOAppBE.Common;
using TODOAppBE.Contracts;
using TODOAppBE.Entities;
using TODOAppBE.ErrorsCollection;
using TODOAppBE.Repositories;

namespace TODOAppBE.Controllers
{
    public interface ITaskController
    {
        IActionResult Delete(Guid id);
        IActionResult Edit(TaskDto dto);
        IActionResult Insert(InsertTaskDto dto);
        IActionResult Get(Guid id);
        IActionResult GetAll();
    }
    
    public class TaskController : ApiController, ITaskController
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

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var deleteResult = _taskRepository.Delete(id);
            return deleteResult.Match(success => NoContent(), errors => Problem(errors));
        }

        [HttpPost]
        public IActionResult Edit(TaskDto dto)
        {
            var entityResult = _taskRepository.Get(dto.Id);
            return entityResult.Match(
                entity => 
                {
                    if (dto.Name != entity.Name)
                    {
                        if (_taskRepository.validTaskName(dto.Name))
                            entity.Rename(dto.Name);
                        else
                            return ProblemSingle(Errors.Task.TaskAlreadyExists);
                    }
                    if (dto.Status != entity.Status)
                    {
                        switch (dto.Status)
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
                    if (dto.Priority != entity.Priority)
                        entity.ChangePriority(dto.Priority);
                    return NoContent();
                },
                errors => Problem(errors));
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var getResult = _taskRepository.Get(id);
            return getResult.Match(
                entity => Ok(new TaskDto().Map(entity)),
                errors => Problem(errors));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_taskRepository.GetAll().Select(x => new TaskDto().Map(x)));
        }

        [HttpPost]
        public IActionResult Insert(InsertTaskDto dto)
        {
            var entity = new TaskEntity(dto.Name, dto.Priority, dto.Status);
            var insertResult = _taskRepository.Insert(entity);
            return insertResult.Match(
                entity => Ok(entity.Id),
                errors => Problem(errors));
        }
    }
}