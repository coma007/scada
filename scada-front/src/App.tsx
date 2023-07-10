import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import LoginComponent from './features/DatabaseManager/Login/LoginComponent';
import AllTagsPage from './features/DatabaseManager/Tags/AllTagsPage';

function App() {
  return (
    <div>
      <Menu></Menu>
      <AllTagsPage></AllTagsPage>
    </div>
  );
}

export default App;
