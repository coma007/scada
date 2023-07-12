import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { AuthGuard, NonAuthGuard } from './GuardedRoute';
import LoginComponent from '../features/DatabaseManager/Login/components/LoginComponent/LoginComponent';
import AllTagsPage from '../features/DatabaseManager/Tags/pages/AllTagsPage/AllTagsPage';
import TrendingPage from '../features/Trending/pages/TrendingPage';
import LatestAlarmsPage from '../features/AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage';
import ReportsPage from '../features/ReportManager/pages/ReportsPage/ReportsPage';

const Router = () => {

    return (
        <BrowserRouter>
            <Routes>
                <Route element={<NonAuthGuard />}>
                    <Route index element={<LoginComponent />} />
                </Route>
                <Route element={<AuthGuard />}>
                    <Route path="/database-manager" element={<AllTagsPage />} />
                    <Route path="/trending" element={<TrendingPage />} />
                    <Route path="/alarm-display" element={<LatestAlarmsPage />} />
                    <Route path="/report-manager" element={<ReportsPage />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
};

export default Router;