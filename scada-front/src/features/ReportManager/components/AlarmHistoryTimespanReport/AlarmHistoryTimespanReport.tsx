import React, { useEffect, useState } from 'react';
import AlarmRecordsList from '../../../../components/AlarmRecordsList/AlarmRecordsList';
import { AlarmHistoryRecord } from '../../../AlarmDisplay/LatestAlarms/types/AlarmHistoryRecord';
import { Button, Form } from 'react-bootstrap';
import { formatDate } from '../../../../utils/DateFormatter';
import TimespanFilter from '../../../../components/TimespanFilter/TimespanFilter';
const AlarmHistoryTimespanReport = () => {
    const [alarmRecords, setAlarmRecords] = useState<AlarmHistoryRecord[]>([]);

    const dummyAlarmRecords: AlarmHistoryRecord[] = [
        new AlarmHistoryRecord(
            'Alarm 1',
            new Date(),
            10,
            'Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})'
        ),
        new AlarmHistoryRecord(
            'Alarm 2',
            new Date(),
            15,
            'Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})'
        ),
        new AlarmHistoryRecord(
            'Alarm 3',
            new Date(),
            20,
            'Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})'
        ),
        new AlarmHistoryRecord(
            'Alarm 4',
            new Date(),
            25,
            'Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})'
        ),
        new AlarmHistoryRecord(
            'Alarm 5',
            new Date(),
            30,
            'Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})'
        ),
    ];

    useEffect(() => {
        setAlarmRecords(dummyAlarmRecords);
    }, []);

    const [selectedReport, setSelectedReport] = useState('');

    const handleReportChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedReport(event.target.value);
    };

    const handleSubmit = (dateFrom: Date, dateTo: Date) => {

        console.log(dateFrom)
        console.log(dateTo)
        // Api request here
        const filteredAlarmRecords = dummyAlarmRecords.filter((record) => {
            return (
                record.timestamp >= dateFrom && record.timestamp <= dateTo
            );
        });

        setAlarmRecords(filteredAlarmRecords);
    };

    return (
        <div>

            <TimespanFilter handleSubmit={handleSubmit}></TimespanFilter>

            <AlarmRecordsList
                alarmRecords={alarmRecords}
                setAlarmRecords={setAlarmRecords}
                canSnooze={false}
            />
        </div>
    );
};

export default AlarmHistoryTimespanReport;
