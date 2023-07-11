import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import ReportsPage from './features/ReportManager/pages/ReportsPage/ReportsPage';


function App() {
  return (
    <div>
      <Menu></Menu>
      <AllTagsPage></AllTagsPage>
    </div>
  );
}

export default App;
