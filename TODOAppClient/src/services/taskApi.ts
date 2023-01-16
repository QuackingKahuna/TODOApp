import InputTaskDto from "./contracts/inputTaskDto";
import TaskDto from "./contracts/taskDto"

const _url = "https://localhost:7121/api/Task/"

interface CallResult {
    success: boolean,
    data?: any
}

async function call(url:string, options?: any){
    var result: CallResult = { success: false }
    try {
        const response = await fetch(url, options)
        result.success = response.status < 300
        if(response.status !== 204)
            result.data = await response.json()
    }
    catch(e){
        alert(`Communication with server throwed an exception`)
    }
    return result
}

export const GetAllTasks = async () => {
    const tasks : TaskDto[] = (await call(_url + "GetAll")).data
    return tasks
}

export const InsertTask = async (input: InputTaskDto) => {
    const taskId: string = (await call(_url + "Insert", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(input)
    })).data
    return taskId
}

export const UpdateTask = async (input: TaskDto) => {
    return (await call(_url + "Edit", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(input)
    })).success
}

export const DeleteTask = async (taskId: string) => {
    return (await call(_url + `Delete/${taskId}`, {
        method: "Delete"
    })).success
}