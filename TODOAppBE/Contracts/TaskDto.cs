using System.ComponentModel.DataAnnotations;
using TODOAppBE.Common;
using TODOAppBE.Entities;

namespace TODOAppBE.Contracts
{
    public class TaskDto
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }

        //Note: In enterprise code I would definitely use automapper. I wouldn't unit test it
        public TaskDto Map(TaskEntity entity)
        {
            Id= entity.Id;
            Name= entity.Name;
            Priority= entity.Priority;
            Status = entity.Status;
            return this;
        }
    }

    public class InsertTaskDto
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public Status Status { get; set; }
    }
}
