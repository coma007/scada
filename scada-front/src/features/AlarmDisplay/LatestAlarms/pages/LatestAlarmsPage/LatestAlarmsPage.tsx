import React, { useEffect } from 'react'
import { AlarmHistoryRecord } from '../../../../../types/AlarmHistoryRecord';
import AlarmRecordsList from '../../../../../components/AlarmRecordsList/AlarmRecordsList';

const LatestAlarmsPage = () => {

    const [alarmRecords, setAlarmRecords] = React.useState<AlarmHistoryRecord[]>([]);

    const dummyAlarmRecords: AlarmHistoryRecord[] = [
        new AlarmHistoryRecord("Alarm 1", new Date(), 10, "Value of tag tag_1 (10) is critically lower than limit (100)"),
        new AlarmHistoryRecord("Alarm 2", new Date(), 15, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 3", new Date(), 20, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 4", new Date(), 25, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
        new AlarmHistoryRecord("Alarm 5", new Date(), 30, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})"),
    ];

    useEffect(() => {
        setAlarmRecords(dummyAlarmRecords)
    }, [])



    return (
        <div>
            <div>
                <h5>Alarm Display</h5>
            </div>
            <AlarmRecordsList alarmRecords={alarmRecords} setAlarmRecords={setAlarmRecords} canSnooze={false}></AlarmRecordsList>
        </div >
    );
}

export default LatestAlarmsPage