import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import ReportsPage from './features/ReportManager/pages/ReportsPage/ReportsPage';
import LatestAlarmsPage from './features/AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage';
import TrendingPage from './features/Trending/pages/TrendingPage';
import AllTagsPage from './features/DatabaseManager/Tags/pages/AllTagsPage/AllTagsPage';
import LoginComponent from './features/DatabaseManager/Login/components/LoginComponent/LoginComponent';


function App() {
  return (
    <div>
      <Menu></Menu>
      <AllTagsPage></AllTagsPage>
    </div>
  );
}

export default App;
