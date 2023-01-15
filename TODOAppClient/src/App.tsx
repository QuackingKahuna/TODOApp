import React from 'react';
import { Header } from './components/header/header';
import { Task, TaskAction } from './components/task/task';
import { GetAllTasks } from './services/taskApi';
import './App.css';
import TaskDto from './services/contracts/taskDto';

const App: React.FC = () => {
  const [databaseTasks, setDatabaseTasks] = React.useState<TaskDto[]>([])
  const [appState, setAppState] = React.useState({refreshTrigger: 0})
  
  React.useEffect(() => {
    const fetchData = async function t(){
      var tasks = (await GetAllTasks()).sort((a, b) => a.priority > b.priority ? -1 : 1)
      setDatabaseTasks(tasks)
    }
    fetchData()
  }, [appState.refreshTrigger])

  function refresh(){
    setAppState(x => {
      return {
        ...x, refreshTrigger: x.refreshTrigger + 1
      }
    })
  }

  const existingTaskNames = databaseTasks.map(x => x.name)
  return (
    <div className="App">
      <Header/>
      <div>
        <div className="new-task-title">Create a new task</div>
        <Task taskAction={TaskAction.Save} refreshHook={refresh} existingTaskNames={existingTaskNames}/>
        {databaseTasks.map(x => (<Task key={x.id} taskAction={TaskAction.Edit} databaseData={x} refreshHook={refresh}
          existingTaskNames={existingTaskNames.filter(y => y !== x.name)}/>))
        }
      </div>
    </div>
  );
}

export default App;
