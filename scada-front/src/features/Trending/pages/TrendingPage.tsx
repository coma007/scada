import React, { useEffect, useState } from 'react'
import TagRecordsList from '../../../components/TagRecordsList/TagRecordsList'
import LatestAlarmsPage from '../../AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage'
import GraphComponent from '../components/GraphComponent/GraphComponent'
import { Tag, AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag } from '../../DatabaseManager/Tags/types/Tag'
import { TagHistoryRecord } from '../../../types/TagHistoryRecord'

const TrendingPage = () => {
    const [tagRecords, setTagRecords] = useState<TagHistoryRecord[]>([]);
    const [selectedTagRecord, setSelectedTagRecord] = useState<TagHistoryRecord>();
    const [selectedTag, setSelectedTag] = useState<Tag>();

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

    useEffect(() => {
        // API call
        // setSelectedTag(...)

    }, [selectedTagRecord])

    const handleViewGraph = (tagName: string) => {
        console.log(tagName);
    }



    return (
        <div className="content">
            <div className="titleLine">
                <h3>Trending</h3>

            </div>

            <div className='row'>
                <div className="col-7">
                    <h5>Input Tag Values</h5>
                    <TagRecordsList
                        tagRecords={tagRecords}
                        setTagRecords={setTagRecords}
                        viewGraph={true}
                        handleViewGraph={handleViewGraph} />
                </div>
                <div className="col-5 full-size">
                    <GraphComponent selectedTag={{scanTime : 10}}></GraphComponent>
                </div>
            </div>
        </div>
    )
}

export default TrendingPage
