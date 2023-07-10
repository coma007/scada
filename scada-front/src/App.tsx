import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import LoginComponent from './features/DatabaseManager/Login/LoginComponent';

function App() {
  return (
    <div>
      <Menu></Menu>
      <LoginComponent></LoginComponent>
    </div>
  );
}

export default App;
