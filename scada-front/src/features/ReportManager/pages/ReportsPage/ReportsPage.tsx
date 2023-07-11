import React, { useEffect, useState } from 'react'
import AlarmRecordsList from '../../../../components/AlarmRecordsList/AlarmRecordsList'
import { AlarmHistoryRecord } from '../../../AlarmDisplay/LatestAlarms/types/AlarmHistoryRecord';
import AlarmHistoryTimespanReport from '../../components/AlarmHistoryTimespanReport/AlarmHistoryTimespanReport';

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
                        <option value="digitalInputTags">Tag Type Report</option>
                        <option value="analogInputTags">Analog Input Tags Report</option>
                        <option value="tagHistory">Tag History Report</option>
                    </select>
                </span>
            </div>
            {selectedReport === 'alarmsTimespan' && <AlarmHistoryTimespanReport />}
            {selectedReport === 'alarmsPriority' && <AlarmHistoryTimespanReport />}
            {/* {selectedReport === 'tagsTimespan' && <TagsTimespanReport />} */}
            {/* {selectedReport === 'digitalInputTags' && <DigitalInputTagsReport />} */}
            {/* {selectedReport === 'analogInputTags' && <AnalogInputTagsReport />} */}
            {/* {selectedReport === 'tagHistory' && <TagHistoryReport />} */}
        </div>
    )
}

export default ReportsPage