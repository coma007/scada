import React from 'react'
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import AlarmDetailsModal from '../../features/AlarmDisplay/LatestAlarms/components/AlarmDetailsModal/AlarmDetailsModal';
import { AlarmHistoryRecord } from '../../features/AlarmDisplay/LatestAlarms/types/AlarmHistoryRecord';
import { formatDate } from '../../utils/DateFormatter';
import style from './AlarmRecordsList.module.css';

const AlarmRecordsList = (props: { alarmRecords: AlarmHistoryRecord[], setAlarmRecords: any }) => {

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
        let updatedList = props.alarmRecords.filter(record => record != snoozedRecord);
        props.setAlarmRecords(updatedList);
    };

    return (
        <div>
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
                    {props.alarmRecords.map((alarmRecord) => (
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

        </div>
    );
}

export default AlarmRecordsList