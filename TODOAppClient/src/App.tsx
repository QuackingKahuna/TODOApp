import React from 'react';
import { Header } from './components/header/header';
import { Task, TaskAction, TaskData } from './components/task/task';
import { GetAllTasks } from './services/taskApi';
import { Status } from './services/contracts/enums';
import './App.css';
import TaskDto from './services/contracts/taskDto';

const App: React.FC = () => {
  const [databaseTasks, setDatabaseTasks] = React.useState<TaskDto[]>([])
  
  React.useEffect(() => {
    const fetchData = async function t(){
      setDatabaseTasks(await GetAllTasks())
    }
    fetchData()
  }, [])
  console.log(databaseTasks)
  return (
    <div className="App">
      <Header/>
      <div>
        <div className="new-task-title">Create a new task</div>
        <Task taskAction={TaskAction.Save}/>
        {databaseTasks.map(x => (<Task key={x.id} taskAction={TaskAction.Edit} databaseData={x}/>))}
      </div>
    </div>
  );
}

export default App;
