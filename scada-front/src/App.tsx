import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import AllTagsPage from './features/DatabaseManager/Tags/pages/AllTagsPage/AllTagsPage';
import LoginComponent from './features/DatabaseManager/Login/LoginComponent';
import LatestAlarmsPage from './features/AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage';


function App() {
  return (
    <div>
      <Menu></Menu>
      <AllTagsPage></AllTagsPage>
    </div>
  );
}

export default App;
