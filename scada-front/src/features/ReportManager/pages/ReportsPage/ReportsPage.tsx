import { useState } from 'react'
import AlarmHistoryTimespanReport from '../../components/AlarmHistoryTimespanReport/AlarmHistoryTimespanReport';
import AlarmPriorityReport from '../../components/AlarmPriorityReport/AlarmPriorityReport';
import TagHistoryTimespanReport from '../../components/TagHistoryTimespanReport/TagHistoryTimespanReport';
import TagTypeReport from '../../components/TagTypeReport/TagTypeReport';
import { TagHistoryRecord } from '../../../../types/TagHistoryRecord';
import TagHistoryReport from '../../components/TagHistoryReport/TagHistoryReport';

const ReportsPage = () => {

    const [selectedReport, setSelectedReport] = useState('');

    const handleReportChange = (event: any) => {
        setSelectedReport(event.target.value);
    };

    return (
        <div className="content">
            <div className="titleLine">
                <h3>Report Manager</h3>
                <span>
                    <select className="form-select" value={selectedReport} onChange={handleReportChange}>
                        <option value="">Choose Report</option>
                        <option value="alarmsTimespan">Alarms Timespan Report</option>
                        <option value="alarmsPriority">Alarms Priority Report</option>
                        <option value="tagsTimespan">Tags Timespan Report</option>
                        <option value="tagType">Tags Type Report</option>
                        <option value="tagHistory">Tag History Report</option>
                    </select>
                </span>
            </div>
            {selectedReport === 'alarmsTimespan' && <AlarmHistoryTimespanReport />}
            {selectedReport === 'alarmsPriority' && <AlarmPriorityReport />}
            {selectedReport === 'tagsTimespan' && <TagHistoryTimespanReport />}
            {selectedReport === 'tagType' && <TagTypeReport />}
            {selectedReport === 'tagHistory' && <TagHistoryReport />}
        </div>
    )
}

export default ReportsPage