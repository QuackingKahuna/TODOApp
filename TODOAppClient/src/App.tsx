import React from 'react';
import { Header } from './components/header/header';
import { Task, TaskAction, Status } from './components/task/task';
import './App.css';

const App: React.FC = () => {
  return (
    <div className="App">
      <Header/>
      <div>
        <div className="new-task-title">Create a new task</div>
        <Task taskAction={TaskAction.Save}/>
        <Task taskAction={TaskAction.Edit}/>
        <Task taskAction={TaskAction.Edit}/>
        <Task taskAction={TaskAction.Edit} status={Status.Completed}/>
      </div>
    </div>
  );
}

export default App;
