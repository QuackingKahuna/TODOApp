using ErrorOr;

namespace TODOAppBE.ErrorsCollection
{
    public static class Errors
    {
        public static class Task
        {
            public static Error NotFound => Error.NotFound("Task.NotFound", "Task was not found.");
            public static Error UncompletedTaskToDelete => Error.Validation("Task.UncompletedTaskToDelete", "Only completed task can be deleted.");
            public static Error TaskAlreadyExists => Error.Validation("Task.TaskAlreadyExists", "This task already exists.");
        }
    }
}
