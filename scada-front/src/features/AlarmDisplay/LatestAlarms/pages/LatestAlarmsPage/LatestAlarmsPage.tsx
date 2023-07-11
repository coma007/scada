import React, { useEffect } from 'react'
import { AlarmHistoryRecord } from '../../types/AlarmHistoryRecord';
import style from './LatestAlarms.module.css';
import AlarmRecordsList from '../../../../../components/AlarmRecordsList/AlarmRecordsList';

const LatestAlarmsPage = () => {

    const [alarmRecords, setAlarmRecords] = React.useState<AlarmHistoryRecord[]>([]);

    const dummyAlarmRecords: AlarmHistoryRecord[] = [
        new AlarmHistoryRecord("Alarm 1", new Date(), 10, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 2", new Date(), 15, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 3", new Date(), 20, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 4", new Date(), 25, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 5", new Date(), 30, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
    ];

    useEffect(() => {
        setAlarmRecords(dummyAlarmRecords)
    }, [])

    

    return (
        <div className="content">
            <div className="titleLine">
                <h3>Alarm Display</h3>
            </div>
            <AlarmRecordsList alarmRecords={alarmRecords} setAlarmRecords={setAlarmRecords} canSnooze={false}></AlarmRecordsList>
        </div >
    );
}

export default LatestAlarmsPage