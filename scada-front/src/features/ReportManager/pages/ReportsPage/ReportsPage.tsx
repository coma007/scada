import React, { useEffect, useState } from 'react'
import AlarmRecordsList from '../../../../components/AlarmRecordsList/AlarmRecordsList'
import { AlarmHistoryRecord } from '../../../../types/AlarmHistoryRecord';
import AlarmHistoryTimespanReport from '../../components/AlarmHistoryTimespanReport/AlarmHistoryTimespanReport';
import AlarmPriorityReport from '../../components/AlarmPriorityReport/AlarmPriorityReport';
import TagHistoryTimespanReport from '../../components/TagHistoryTimespanReport/TagHistoryTimespanReport';

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
                        <option value="digitalInputTags">Tags Type Report</option>
                        <option value="tagHistory">Tag History Report</option>
                    </select>
                </span>
            </div>
            {selectedReport === 'alarmsTimespan' && <AlarmHistoryTimespanReport />}
            {selectedReport === 'alarmsPriority' && <AlarmPriorityReport />}
            {selectedReport === 'tagsTimespan' && <TagHistoryTimespanReport />}
            {/* {selectedReport === 'digitalInputTags' && <DigitalInputTagsReport />} */}
            {/* {selectedReport === 'analogInputTags' && <AnalogInputTagsReport />} */}
            {/* {selectedReport === 'tagHistory' && <TagHistoryReport />} */}
        </div>
    )
}

export default ReportsPage