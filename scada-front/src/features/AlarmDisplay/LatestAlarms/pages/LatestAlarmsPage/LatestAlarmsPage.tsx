import React, { useEffect } from 'react'
import { AlarmHistoryRecord, AllAlarms } from '../../../../../types/AlarmHistoryRecord';
import style from './LatestAlarms.module.css';
import AlarmRecordsList from '../../../../../components/AlarmRecordsList/AlarmRecordsList';
import { WebSocketService } from '../../../../../api/services/WebSocketService';

const LatestAlarmsPage = () => {

    const [alarmRecords, setAlarmRecords] = React.useState<AlarmHistoryRecord[]>([]);
    const [allAlarmRecords, setAllAlarmRecords] = React.useState<AllAlarms[]>([]);
    const [socket, setSocket] = React.useState<WebSocket | null>(null);

    const dummyAlarmRecords: AlarmHistoryRecord[] = [
        new AlarmHistoryRecord("Alarm 1", new Date(), 10, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})", "Ime"),
        new AlarmHistoryRecord("Alarm 2", new Date(), 15, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})", "Ime"),
        new AlarmHistoryRecord("Alarm 3", new Date(), 20, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})", "Ime"),
        new AlarmHistoryRecord("Alarm 4", new Date(), 25, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})", "Ime"),
        new AlarmHistoryRecord("Alarm 5", new Date(), 30, "Value of tag {tagName} ({tagValue}) is critically {relation} than limit ({alarmLimit})", "Ime"),
    ];


    useEffect(()=>{
        setAllAlarmRecords([{
            tagName: "Ime",
            tagAlarms: dummyAlarmRecords}])
    }, [])

    useEffect(() => {
        let alarms : AlarmHistoryRecord[] = [];
        for (let tag of allAlarmRecords) {
            alarms.push(tag.tagAlarms[0])
        }
        setAlarmRecords(alarms)
    }, [allAlarmRecords])

    const processAlarm = (message : any) => {
        let allAlarms : AllAlarms[] = [];
        let tagName : string = message[0].TagName;
        console.log(tagName)
        let newAlarms : AlarmHistoryRecord[] = []
        for (let alarm of message) {
            newAlarms.push(new AlarmHistoryRecord(alarm.AlarmName, alarm.Timestamp, alarm.TagValue, alarm.Message, tagName))
        }
        console.log(newAlarms)
        let found = false;
        for (let tag of allAlarmRecords) {
            if (tag.tagName === tagName) {
                tag.tagAlarms = newAlarms;
                found = true;
            }
            if (tag.tagAlarms.length > 0) {
                allAlarms.push({
                    tagName: tag.tagName,
                    tagAlarms: tag.tagAlarms
                })
            }
        }
        if (!found) {
            allAlarms.push({
                tagName: tagName,
                tagAlarms: newAlarms
            })
        }
        setAllAlarmRecords(allAlarms);
    }

    useEffect(() => {
      WebSocketService.createSocket(setSocket);
    }, []);
    useEffect(() => {
        WebSocketService.defineSocket(socket, "NewAlarmRecordsCreated", processAlarm);
    }, [socket]);

    let snoozeAlarm = (tagName: string) => {
        let allAlarms : AllAlarms[] = [];
        for (let tag of allAlarmRecords) {
            if (tag.tagName === tagName) {
                tag.tagAlarms.splice(0,1);
            }
            if (tag.tagAlarms.length > 0) {
                allAlarms.push({
                    tagName: tag.tagName,
                    tagAlarms: tag.tagAlarms
                })
            }
        }
        setAllAlarmRecords(allAlarms)
    }

    return (
        <div className="content">
            <div className="titleLine">
                <h3>Alarm Display</h3>
            </div>
            <AlarmRecordsList alarmRecords={alarmRecords} setAlarmRecords={setAlarmRecords} canSnooze={true} snoozeCallback={snoozeAlarm}></AlarmRecordsList>
        </div >
    );
}

export default LatestAlarmsPage