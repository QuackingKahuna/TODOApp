import TaskDto from "./contracts/taskDto"

const _url = "https://localhost:7121/api/Task/"



export const GetAllTasks = async () => {
    const response = await fetch(_url + "GetAll")
    const tasks: TaskDto[] = await response.json();
    return tasks
}