import React from "react";
import "./task.css";
import { Counter } from "../counter/counter";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faFloppyDisk, faPencil, faTrashCan } from '@fortawesome/free-solid-svg-icons'
import { Status } from "../../services/contracts/enums"
import { InsertTask, UpdateTask, DeleteTask } from "../../services/taskApi";
import TaskDto from "../../services/contracts/taskDto";

export const enum TaskAction {
    Save = 1,
    Edit = 2
}

export interface TaskData{
    id?: string, //Guid on BE
    name: string,
    priority: number,
    status: Status,
    insertTrigger: number,
    updateTrigger: number,
    deleteTrigger: number,
    updateAndDeleteTrigger: number
}

interface Props{
    databaseData?: TaskDto
    taskAction: TaskAction
    refreshHook:() => void
    existingTaskNames: string[]
}

export const Task: React.FC<Props> = ({taskAction, databaseData, refreshHook, existingTaskNames}) => {
    const initData: TaskData = {
        name: databaseData ? databaseData?.name : "",
        priority: databaseData ? databaseData.priority : 1,
        status: databaseData ? databaseData.status : Status.NotStarted,
        id: databaseData && databaseData.id? databaseData.id : undefined,
        insertTrigger: 0,
        updateTrigger: 0,
        deleteTrigger: 0,
        updateAndDeleteTrigger: 0,
    }
    const [taskData, setTaskData] = React.useState(initData)
    
    function handleChange(event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>) {
        const {name, value} = event.target
        const valueInput: string | number = name === "status" ? +value : value
        setTaskData(previousData => {
            return {
                ...previousData,
                [name]: valueInput
            }
        })
    }

    function increasePriority(){
        setTaskData(prevData => {
            return {
                ...prevData,
                priority: prevData.priority + 1
            }
        })
    }

    function decreasePriority(){
        setTaskData(prevData => {
            return {
                ...prevData,
                priority: prevData.priority > 0 ? prevData.priority - 1 : 0
            }
        })
    }
    React.useEffect(() => {
        if(taskData.insertTrigger){
            const insertTask = async () => {
                await InsertTask({...taskData})
                setTaskData(initData)
                refreshHook()
            }
            if(existingTaskNames.some(name => name === taskData.name))
                alert("This task already exists")
            else
                insertTask()
        }
        else if(taskData.updateTrigger){
            const updateTask = async () => {
                await UpdateTask({...taskData, id: taskData.id ? taskData.id : ""})
                setTaskData(x => {
                    return { ...x, updateTrigger: 0}
                })
                refreshHook()
            }
            if(existingTaskNames.some(name => name === taskData.name))
                alert("This task already exists")
            else
                updateTask()
        }
        else if(taskData.deleteTrigger){
            const deleteTask = async () => {
                try{
                    var taskName = await DeleteTask(taskData.id ? taskData.id : "")
                    refreshHook()
                    alert(`Task ${taskName} was deleted`)
                }
                catch(e){
                    setTaskData(x => {
                        return { ...x, deleteTrigger: 0}
                    })
                }
            }
            deleteTask()
        }
        else if(taskData.updateAndDeleteTrigger){
            const updateAndDeleteTask = async () => {
                await UpdateTask({...taskData, id: taskData.id ? taskData.id : ""})
                try{
                    var taskName = await DeleteTask(taskData.id ? taskData.id : "")
                    refreshHook()
                    alert(`Task ${taskName} was deleted`)
                }
                catch(e){
                    setTaskData(x => {
                        return { ...x, updateAndDeleteTrigger: 0}
                    })
                }
                refreshHook()
            }
            updateAndDeleteTask()
        }
      }, [taskData.insertTrigger, taskData.updateTrigger, taskData.deleteTrigger, taskData.updateAndDeleteTrigger])

    function upsertTask(){
        if(taskData.name){
            if(taskAction === TaskAction.Save){
                setTaskData(x => {
                    return {
                        ...x, 
                        insertTrigger: x.insertTrigger + 1
                    }
                })
            }
            else if (changesToSave()) {
                setTaskData(x => {
                    return {
                        ...x, 
                        updateTrigger: x.updateTrigger + 1
                    }
                })
            }
        }
        else
            alert("Please provide a task name")
    }

    function deleteTask(){
        if(taskData.status === Status.Completed){
            if(changesToSave()){
                setTaskData(x => {
                    return {
                        ...x, updateAndDeleteTrigger: x.updateAndDeleteTrigger + 1
                    }
                })
            }
            else{
                setTaskData(x => {
                    return {
                        ...x, deleteTrigger: x.deleteTrigger + 1
                    }
                })
            }
        }
        else
            alert("Only completed tasks can be deleted")
    }

    function changesToSave(): boolean{
        if(databaseData){
            if (databaseData.name !== taskData.name 
                || databaseData.priority !== taskData.priority
                || databaseData.status !== taskData.status){
                return true
            }
        }
        return false
    }

    var className = "task";
    var actionIcon = faPencil
    if(taskAction === TaskAction.Save){
        className += " addTask";
        actionIcon = faFloppyDisk
    }
    if(changesToSave()){
        className += " change"
    }

    return (
        <div className={className}>
            <input 
                type="text"
                className="name"
                placeholder="Task name"
                onChange={handleChange}
                name="name"
                value={taskData.name}/>
            <Counter 
                increase={increasePriority} 
                decrease={decreasePriority} 
                value={taskData.priority}/>
            <div className="status">
                <select 
                    value={taskData.status}
                    onChange={handleChange}
                    name="status"
                    className="status--select"
                >
                    <option value="10">Not started</option>
                    <option value="20">In Progress</option>
                    <option value="30">Completed</option> 
                </select>
            </div>
            {!(taskAction === TaskAction.Edit && taskData.status === Status.Completed) &&
                // Visual placeholder for delete action
                <button className="delete">
                </button>
            }
            <button className="action" onClick={upsertTask}>
                <FontAwesomeIcon icon={actionIcon} size="2x"/>
            </button>
            {taskAction === TaskAction.Edit && taskData.status === Status.Completed &&
                <button className="delete" onClick={deleteTask}>
                    <FontAwesomeIcon icon={faTrashCan} size="2x"/>
                </button>
            }
        </div>
    )
}