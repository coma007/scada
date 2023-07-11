import React, { useEffect, useState } from 'react';
import AlarmRecordsList from '../../../../components/AlarmRecordsList/AlarmRecordsList';
import { AlarmHistoryRecord } from '../../../../types/AlarmHistoryRecord';
import { Button, Form } from 'react-bootstrap';
import { formatDate } from '../../../../utils/DateFormatter';
import TimespanFilter from '../../../../components/TimespanFilter/TimespanFilter';
const AlarmPriorityReport = () => {
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


    const [selectedPriority, setSelectedPriority] = useState<number | undefined>();

    const handlePriorityChange = (event: any) => {

        setSelectedPriority(event.target.value);
    };

    const handleSubmit = () => {
        if (selectedPriority !== undefined) {
            // API request here ...

            // setAlarmRecords(...)
        }
    }

    return (
        <div>

            <div className="row align-items-center margin-bottom">
                <div className="col-md-3">
                    <select className="form-select" value={selectedPriority} onChange={handlePriorityChange}>
                        <option value="">Choose Priority</option>
                        <option value={0}>Low (1)</option>
                        <option value={1}>Medium (2)</option>
                        <option value={2}>High (3)</option>
                    </select>
                </div>


                <div className='col-md-8'></div>

                <div className="col-md-1">
                    <Button variant="primary">
                        Filter
                    </Button>
                </div>
            </div>

            <AlarmRecordsList
                alarmRecords={alarmRecords}
                setAlarmRecords={setAlarmRecords}
                canSnooze={false}
            />
        </div>
    );
};

export default AlarmPriorityReport;
