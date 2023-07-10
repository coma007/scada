import React from 'react';
import './App.css';
import Menu from './components/Menu/Menu';
import AllTagsPage from './features/DatabaseManager/Tags/pages/AllTagsPage/AllTagsPage';


function App() {
  return (
    <div>
      <Menu></Menu>
      <AllTagsPage></AllTagsPage>
    </div>
  );
}

export default App;
