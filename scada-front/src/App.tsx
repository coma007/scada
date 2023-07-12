import './App.css';
import Menu from './components/Menu/Menu';
import ReportsPage from './features/ReportManager/pages/ReportsPage/ReportsPage';
import LatestAlarmsPage from './features/AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage';
import TrendingPage from './features/Trending/pages/TrendingPage';
import AllTagsPage from './features/DatabaseManager/Tags/pages/AllTagsPage/AllTagsPage';
import LoginComponent from './features/DatabaseManager/Login/components/LoginComponent/LoginComponent';
import Router from './routes/Router';


function App() {
  return (
    <div>
      <Menu></Menu>
      <Router></Router>
    </div>
  );
}

export default App;
