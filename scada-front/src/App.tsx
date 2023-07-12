import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import ReportsPage from './features/ReportManager/pages/ReportsPage/ReportsPage';
import LatestAlarmsPage from './features/AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage';
import TrendingPage from './features/Trending/pages/TrendingPage';


function App() {
  return (
    <div>
      <Menu></Menu>
      <TrendingPage></TrendingPage>
    </div>
  );
}

export default App;
