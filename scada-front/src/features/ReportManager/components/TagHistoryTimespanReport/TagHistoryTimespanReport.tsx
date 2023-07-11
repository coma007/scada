import React, { useEffect, useState } from 'react'
import TimespanFilter from '../../../../components/TimespanFilter/TimespanFilter';
import TagRecordsList from '../../../../components/TagRecordsList/TagRecordsList';
import { TagHistoryRecord } from '../../../../types/TagHistoryRecord';

const TagHistoryTimespanReport = () => {
    const [tagRecords, setTagRecords] = useState<TagHistoryRecord[]>([]);

    const dummyTagRecords: TagHistoryRecord[] = [
        new TagHistoryRecord(
            'Tag 1',
            new Date(),
            10,
        ),
        new TagHistoryRecord(
            'Tag 2',
            new Date(),
            15,
        ),
        new TagHistoryRecord(
            'Tag 3',
            new Date(),
            20,
        ),
        new TagHistoryRecord(
            'Tag 4',
            new Date(),
            25,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
    ];

    useEffect(() => {
        setTagRecords(dummyTagRecords);
    }, []);


    const handleSubmit = (dateFrom: Date, dateTo: Date) => {

        // Api request here
        const filteredAlarmRecords = dummyTagRecords.filter((record) => {
            return (
                record.timestamp >= dateFrom && record.timestamp <= dateTo
            );
        });

        setTagRecords(filteredAlarmRecords);
    };
    return (
        <div>

            <TimespanFilter handleSubmit={handleSubmit}></TimespanFilter>

            <TagRecordsList
                tagRecords={tagRecords}
                setTagRecords={setTagRecords}
            />
        </div>
    );
}

export default TagHistoryTimespanReport