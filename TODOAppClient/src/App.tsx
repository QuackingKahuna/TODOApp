import React from 'react';
import { Header } from './components/header/header';
import './App.css';

const App: React.FC = () => {
  return (
    <div className="App">
      <Header/>
      <body>
        
          <p>
            Edit <code>src/App.tsx</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
      </body>
    </div>
  );
}

export default App;
