import React from "react";
import "./task.css";
import { Counter } from "../counter/counter";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faFloppyDisk, faPencil, faTrashCan } from '@fortawesome/free-solid-svg-icons'

export const enum Status {
    NotStarted = 10,
    InProgress = 20,
    Completed = 30
}

interface TaskData{
    name: string,
    priority: number,
    status: Status
}

export const enum TaskAction {
    Save = 1,
    Edit = 2
}

interface Props{
    taskAction: TaskAction
    status?: Status
}

export const Task: React.FC<Props> = ({taskAction, status}) => {
    const [taskData, setTaskData] = React.useState<TaskData>({
        name: "",
        priority: 1,
        status: status ? status : Status.NotStarted
    })
    
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
    
    var className = "task";
    var actionIcon = faPencil
    if(taskAction === TaskAction.Save){
        className += " addTask";
        actionIcon = faFloppyDisk
    }

    console.log(taskData, actionIcon)
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
                >
                    <option value="10">Not started</option>
                    <option value="20">In Progress</option>
                    <option value="30">Completed</option> 
                </select>
            </div>
            {!(taskAction === TaskAction.Edit && taskData.status == Status.Completed) &&
                <button className="delete">
                </button>}
            <button className="action">
                <FontAwesomeIcon icon={actionIcon} size="2x"/>
            </button>
            {taskAction === TaskAction.Edit && taskData.status == Status.Completed &&
                <button className="delete">
                    <FontAwesomeIcon icon={faTrashCan} size="2x"/>
                </button>}
        </div>
    )
}