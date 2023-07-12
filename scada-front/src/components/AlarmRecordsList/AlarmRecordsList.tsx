import React from 'react'
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import AlarmDetailsModal from '../../features/AlarmDisplay/LatestAlarms/components/AlarmDetailsModal/AlarmDetailsModal';
import { AlarmHistoryRecord } from '../../types/AlarmHistoryRecord';
import { formatDate } from '../../utils/DateFormatter';
import { Alarm } from '../../types/Alarm';
import style from './AlarmRecordsList.module.css';
import { ReportService } from '../../features/ReportManager/services/ReportsService';

const AlarmRecordsList = (props: { alarmRecords: AlarmHistoryRecord[], setAlarmRecords: any, canSnooze: boolean, snoozeCallback?: any }) => {

    const [selectedRecord, setSelectedRecord] = React.useState<AlarmHistoryRecord | undefined>();
    const [selectedAlarm, setSelectedAlarm] = React.useState<Alarm | undefined>();
    const [showDetailsModal, setShowDetailsModal] = React.useState(false);

    const handleOpenDetailsModal = async (record: AlarmHistoryRecord) => {
        // find alarm from alarmName in record
        let alarm = await ReportService.getAlarm(record.alarmName);
        setSelectedAlarm(alarm)
        setSelectedRecord(record);
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
        setSelectedRecord(undefined);
        setSelectedAlarm(undefined);
    };


    const handleSnoozeAlarm = (snoozedRecord: AlarmHistoryRecord) => {
        // do logic for same alarm, differen priority if exists
        props.snoozeCallback(snoozedRecord.tag)
        //let updatedList = props.alarmRecords.filter(record => record != snoozedRecord);
        //props.setAlarmRecords(updatedList);
    };

    return (
        <div>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th className={style.nameColumn}>Name</th>
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
                                {props.canSnooze &&
                                    <>
                                        <Button variant="danger" size="sm" onClick={() => handleSnoozeAlarm(alarmRecord)}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="info-tooltip">Snooze alarm</Tooltip>}
                                            >
                                                <i className="bi bi-alarm"></i>
                                            </OverlayTrigger>
                                        </Button>{' '}
                                    </>
                                }
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
                    selectedAlarm={selectedAlarm!}
                    showModal={showDetailsModal}
                    handleCloseModal={handleCloseDetailsModal} />
            )}

        </div>
    );
}

export default AlarmRecordsList