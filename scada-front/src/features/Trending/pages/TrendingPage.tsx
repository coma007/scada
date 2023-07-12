import React, { useEffect, useState } from 'react'
import TagRecordsList from '../../../components/TagRecordsList/TagRecordsList'
import LatestAlarmsPage from '../../AlarmDisplay/LatestAlarms/pages/LatestAlarmsPage/LatestAlarmsPage'
import GraphComponent from '../components/GraphComponent/GraphComponent'
import { Tag, AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag } from '../../../types/Tag'
import { TagHistoryRecord } from '../../../types/TagHistoryRecord'
import { TrendingService } from '../services/TrendingService'
import { WebSocketService } from '../../../api/services/WebSocketService'
import { ReportService } from '../../ReportManager/services/ReportsService'

const TrendingPage = () => {
    const [tagRecords, setTagRecords] = useState<TagHistoryRecord[]>([]);
    const [selectedTagRecord, setSelectedTagRecord] = useState<TagHistoryRecord>();
    const [selectedTag, setSelectedTag] = useState<string | undefined>();
    const [selectedScanTime, setSelectedScanTime] = useState<number | undefined>();
    const [selectedRange, setSelectedRange] = useState<{min: number, max: number} | undefined>();
    const [socket, setSocket] = useState<WebSocket | null>(null);

    const processValue = (message: any) => {
        //console.log(message);
        let newTagValue : TagHistoryRecord = new TagHistoryRecord(message.TagName, message.Timestamp, message.TagValue);
        let found = false;
        let newTagRecords : TagHistoryRecord[] = [];
        for (let tagValue of tagRecords) {
            if (tagValue.tagName === newTagValue.tagName) {
                tagValue.tagValue = newTagValue.tagValue;
                tagValue.timestamp = newTagValue.timestamp;
                found = true;
            }
            newTagRecords.push(tagValue);
        }
        if (!found) {
            newTagRecords.push(newTagValue)
        }
        setTagRecords(newTagRecords)
    }

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
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
        new TagHistoryRecord(
            'Tag 5',
            new Date(),
            30,
        ),
    ];

    const fetchData = async () => {
        let tagValues = await TrendingService.getLatestInput();
        setTagRecords([...tagValues])
    }

    useEffect(() => {
        fetchData();
        //setTagRecords(dummyTagRecords);
    }, []);

    // useEffect(() => {
    //     setSelectedTag(selectedTagRecord?.tagName)
    // }, [selectedTagRecord])

    const handleViewGraph = async (tagName: string) => {
        console.log(tagName);
        let scan = 10;
        let [min, max] = [0,100]
        setSelectedTag(tagName)
        try {
            let tag = await TrendingService.getAnalogTag(tagName);
            scan = tag.scanTime
            min = tag.lowLimit
            max = tag.highLimit
        } catch(e : any) {
            console.log(e.message)
        }
        setSelectedScanTime(scan)
        setSelectedRange({min:min, max:max})
    }

    useEffect(() => {
        WebSocketService.createSocket(setSocket);
    }, []);

    useEffect(() => {
        WebSocketService.defineSocket(socket, "NewTagRecordCreated", processValue);
    }, [socket]);

    useEffect(()=>{
        if (socket) {
            socket.onmessage = async (event) => {
                let message : string = await event.data.text();
                let tokens = message.split("=>");
                message = JSON.parse(tokens[1]);
                console.log('Received message:', message);
                processValue(message)
            }
        }
    },[tagRecords])


    return (
        <div className="content">
            <div className="titleLine">
                <h3>Trending</h3>

            </div>

            <div className='row'>
                <div className="col-7 scrollable">
                    <TagRecordsList
                        tagRecords={tagRecords}
                        setTagRecords={setTagRecords}
                        viewGraph={true}
                        handleViewGraph={handleViewGraph} />
                </div>
                <div className="col-5 full-size">
                    <GraphComponent selectedTag={{ tagName: selectedTag, scanTime: selectedScanTime, range: selectedRange }}></GraphComponent>
                </div>
            </div>
        </div>
    )
}

export default TrendingPage
