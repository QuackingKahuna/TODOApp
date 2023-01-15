import InputTaskDto from "./contracts/inputTaskDto";
import TaskDto from "./contracts/taskDto"

const _url = "https://localhost:7121/api/Task/"

async function call(url:string, options?: any){
    var result
    try {
        const response = await fetch(url, options)
        result = await response.json()
    }
    catch(e){
        alert(`Communication with server throwed an exception`)
    }
    return result
}

export const GetAllTasks = async () => {
    const tasks : TaskDto[] = await call(_url + "GetAll")
    return tasks
}

export const InsertTask = async (input: InputTaskDto) => {
    const taskId: string = await call(_url + "Insert", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(input)
    });
    return taskId
}

export const UpdateTask = async (input: TaskDto) => {
    const success: boolean = await call(_url + "Edit", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(input)
    })
    return success
}

export const DeleteTask = async (taskId: string) => {
    const task: TaskDto = await call(_url + `Delete/${taskId}`, {
        method: "Delete"
    })
    return task.name
}