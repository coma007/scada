import React, { useEffect, useState } from 'react'
import TagRecordsList from '../../../../components/TagRecordsList/TagRecordsList';
import TimespanFilter from '../../../../components/TimespanFilter/TimespanFilter';
import { TagHistoryRecord } from '../../../../types/TagHistoryRecord';
import { Button, Form } from 'react-bootstrap';

const TagHistoryReport = () => {
    const [tagRecords, setTagRecords] = useState<TagHistoryRecord[]>([]);
    const [tagName, setTagName] = useState('');

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


    const handleSubmit = (tagName: string) => {

        // Api request here
        const filteredAlarmRecords = dummyTagRecords.filter((record) => {
            return (
                record.tagName == tagName
            );
        });

        setTagRecords(filteredAlarmRecords);
    };
    return (
        <div>
            <div className="row align-items-center margin-bottom">
                <div className="col-md-6">
                    <Form.Group controlId="dateFrom" className="d-flex align-items-center vertical-align justify-content-between">
                        <Form.Label className="col-md-2">Tag Name:</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter tag name"
                            value={tagName}
                            onChange={(e) => setTagName(e.target.value)}
                        />
                    </Form.Group>
                </div>

                <div className='col-md-5'></div>

                <div className="col-md-1">
                    <Button onClick={() => handleSubmit(tagName)} variant="primary">
                        Filter
                    </Button>
                </div>
            </div>

            <TagRecordsList
                tagRecords={tagRecords}
                setTagRecords={setTagRecords}
            />
        </div>
    );
}

export default TagHistoryReport