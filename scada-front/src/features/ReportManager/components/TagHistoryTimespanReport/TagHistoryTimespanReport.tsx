import React, { useEffect, useState } from 'react'
import TimespanFilter from '../../../../components/TimespanFilter/TimespanFilter';
import TagRecordsList from '../../../../components/TagRecordsList/TagRecordsList';
import { TagHistoryRecord } from '../../../../types/TagHistoryRecord';
import { ReportService } from '../../services/ReportsService';

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

    // useEffect(() => {
    //     setTagRecords(dummyTagRecords);
    // }, []);


    const handleSubmit = async (dateFrom: Date, dateTo: Date) => {

        let tagValues = await ReportService.getTagHistoryTimespan(dateFrom, dateTo)
        console.log(tagValues)
        // const filteredAlarmRecords = dummyTagRecords.filter((record) => {
        //     return (
        //         record.timestamp >= dateFrom && record.timestamp <= dateTo
        //     );
        // });

        setTagRecords([...tagValues]);
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