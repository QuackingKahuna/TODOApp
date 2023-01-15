import React from "react";
import "./task.css";
import { Counter } from "../counter/counter";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faFloppyDisk, faPencil, faTrashCan } from '@fortawesome/free-solid-svg-icons'
import { Status } from "../../services/contracts/enums"

export const enum TaskAction {
    Save = 1,
    Edit = 2
}

export interface TaskData{
    id?: string, //Guid on BE
    name: string,
    priority: number,
    status: Status
}

interface Props{
    databaseData?: TaskData
    taskAction: TaskAction
    refreshHook?:() => void
}

export const Task: React.FC<Props> = ({taskAction, databaseData}) => {
    const [taskData, setTaskData] = React.useState<TaskData>({
        name: databaseData ? databaseData?.name : "",
        priority: databaseData ? databaseData.priority : 1,
        status: databaseData ? databaseData.status : Status.NotStarted
    })
    console.log(databaseData, taskData)
    function handleChange(event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>) {
        const {name, value} = event.target
        setTaskData(previousData => {
            return {
                ...previousData,
                [name]: value
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
    
    function upsertTask(){
        if(taskData.name){
            if(taskAction === TaskAction.Save){
                //TODO send insert
            }
            else if (changesToSave()) {
                //TODO send edit
            }
        }
        else
            alert("Please provide a task name")
    }

    function deleteTask(){
        if(taskData.status == Status.Completed){
            //TODO delete
        }
        else
            alert("Only completed tasks can be deleted")
    }

    function changesToSave(): boolean{
        if(databaseData){
            if (databaseData.name !== taskData.name 
                || databaseData.priority !== taskData.priority
                || databaseData.status != taskData.status){
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
            {!(taskAction === TaskAction.Edit && taskData.status == Status.Completed) &&
                // Visual placeholder for delete action
                <button className="delete">
                </button>
            }
            <button className="action" onClick={upsertTask}>
                <FontAwesomeIcon icon={actionIcon} size="2x"/>
            </button>
            {taskAction === TaskAction.Edit && taskData.status == Status.Completed &&
                <button className="delete" onClick={deleteTask}>
                    <FontAwesomeIcon icon={faTrashCan} size="2x"/>
                </button>
            }
        </div>
    )
}