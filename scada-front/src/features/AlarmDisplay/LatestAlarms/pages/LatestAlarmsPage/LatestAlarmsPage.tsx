import React, { useEffect } from 'react'
import style from './LatestAlarms.module.css';
import { Button, OverlayTrigger, Table, Tooltip } from 'react-bootstrap';
import { AlarmHistoryRecord } from '../../types/AlarmHistoryRecord';
import AllAlarmsOfTagModal from '../../../../DatabaseManager/Alarms/components/AllAlarmsOfTagModal/AllAlarmsOfTagModal';
import TagCreateModal from '../../../../DatabaseManager/Tags/components/TagCreateModal/TagCreateModal';
import TagDetailsModal from '../../../../DatabaseManager/Tags/components/TagDetailsModal/TagDetailsModal';
import { formatDate } from '../../../../../utils/DateFormatter';
import { Alarm } from '../../../../DatabaseManager/Alarms/types/Alarm';
import AlarmDetailsModal from '../../components/AlarmDetailsModal/AlarmDetailsModal';

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

    const [selectedRecord, setSelectedRecord] = React.useState<AlarmHistoryRecord | undefined>();
    const [showDetailsModal, setShowDetailsModal] = React.useState(false);

    const handleOpenDetailsModal = (record: AlarmHistoryRecord) => {
        setSelectedRecord(record);
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
    };


    const handleSnoozeAlarm = (snoozedRecord: AlarmHistoryRecord) => {
        // do logic for same alarm, differen priority if exists
        let updatedList = alarmRecords.filter(record => record != snoozedRecord);
        setAlarmRecords(updatedList);
    };

    return (
        <div className={style.content}>
            <div className={style.titleLine}>
                <h3>Alarm Display</h3>
            </div>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th className={style.nameColumn}>Alarm Name</th>
                        <th className={style.timestampColumn}>Timestamp</th>
                        <th className={style.valueColumn}>Value</th>
                        <th className={style.messageColumn}>Message</th>
                        <th >Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {alarmRecords.map((alarmRecord) => (
                        <tr key={alarmRecord.alarmName}>
                            <td >{alarmRecord.alarmName}</td>
                            <td >{formatDate(alarmRecord.timestamp)}</td>
                            <td >{alarmRecord.tagValue}</td>
                            <td >{alarmRecord.message}</td>
                            <td >
                                <Button variant="danger" size="sm" onClick={() => handleSnoozeAlarm(alarmRecord)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="info-tooltip">Snooze alarm</Tooltip>}
                                    >
                                        <i className="bi bi-alarm"></i>
                                    </OverlayTrigger>
                                </Button>{' '}
                                <Button variant="info" size="sm" onClick={() => handleOpenDetailsModal(alarmRecord)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="remove-tooltip">Alarm details</Tooltip>}
                                    >
                                        <i className="bi bi-info-circle"></i>
                                    </OverlayTrigger>
                                </Button>{' '}

                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            {selectedRecord && (
                <AlarmDetailsModal
                    selectedAlarmRecord={selectedRecord}
                    showModal={showDetailsModal}
                    handleCloseModal={handleCloseDetailsModal} />
            )}


        </div >
    );
}

export default LatestAlarmsPage