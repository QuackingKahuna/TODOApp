import React from 'react';
import { Header } from './components/header/header';
import { Task, TaskAction, Status, TaskData } from './components/task/task';
import { GetAllTasks } from './services/taskApi';
import './App.css';

const App: React.FC = () => {
  GetAllTasks()

  var databaseData: TaskData = {
    name: "Task 1",
    priority: 1,
    status: Status.InProgress
  }
  
  return (
    <div className="App">
      <Header/>
      <div>
        <div className="new-task-title">Create a new task</div>
        <Task taskAction={TaskAction.Save}/>
        <Task taskAction={TaskAction.Edit} databaseData={databaseData}/>
        <Task taskAction={TaskAction.Edit} databaseData={databaseData}/>
        <Task taskAction={TaskAction.Edit} databaseData={databaseData}/>
      </div>
    </div>
  );
}

export default App;
