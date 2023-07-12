import React, { useState, useEffect } from 'react';
import { Line } from 'react-chartjs-2';
import { CDBContainer } from 'cdbreact';

import { Chart, registerables } from 'chart.js';
import style from './GraphComponent.module.css';
import { formatTime } from '../../../../utils/DateFormatter';
import { ReportService } from '../../../ReportManager/services/ReportsService';
import { TagHistoryRecord } from '../../../../types/TagHistoryRecord';
import { WebSocketService } from '../../../../api/services/WebSocketService';

Chart.register(...registerables);


const GraphComponent = (props: { selectedTag: any }) => {
    const [data, setData] = useState<{ labels: any, datasets: any } | undefined>(undefined);
    const [graphData, setGraphData] = useState<({value: number|null, timestamp: string | null})[]>([])
    const [socket, setSocket] = useState<WebSocket | null>(null);
    const [tagName, setTagName] = useState<string>("");

    useEffect(() => {
        return WebSocketService.createSocket(setSocket);
    }, []);

    const processValue = (message: any) => {
        //console.log(message);
        let newTagValue: TagHistoryRecord = new TagHistoryRecord(message.TagName, message.Timestamp, message.TagValue);
        if (newTagValue.tagName === tagName) {
            let newData = [...graphData, {value: newTagValue.tagValue, timestamp: formatTime(newTagValue.timestamp)}]
            newData.shift()
            console.log(newData)
            setGraphData(newData)
        }
    }

    useEffect(() => {
        WebSocketService.defineSocket(socket, "NewTagRecordCreated", processValue);
    }, [socket]);

    const getLastData = (data: TagHistoryRecord[]) : ({value: number | null, timestamp: string | null})[] => {
        if (data.length >= 5) {
            return [{value: data[4].tagValue, timestamp: formatTime(data[4].timestamp)}, {value: data[3].tagValue, timestamp: formatTime(data[3].timestamp)},
            {value: data[2].tagValue, timestamp: formatTime(data[2].timestamp)}, {value: data[1].tagValue, timestamp: formatTime(data[1].timestamp)},
            {value: data[0].tagValue, timestamp: formatTime(data[0].timestamp)}]
        }
        else {
            let newData : ({value: number | null, timestamp: string | null})[] = []
            for (let record of data) {
                newData = [{value: record.tagValue, timestamp: formatTime(record.timestamp)}, ...newData]
            }
            let length = newData.length;
            while (length < 5) {
                newData = [{value: null, timestamp: null}, ...newData]
                length = newData.length;
            }
            return newData
        }
    }

    const fetchData = async () => {
        let data = await ReportService.getTagHistory(props.selectedTag.tagName)
        let graphData = getLastData(data)
        console.log(data);
        // let newData = {
        //     labels:
        //         [formatTime(new Date(new Date().getTime() - 4 * new Date(props.selectedTag.scanTime * 1000).getTime())),
        //         formatTime(new Date(new Date().getTime() - 3 * new Date(props.selectedTag.scanTime * 1000).getTime())),
        //         formatTime(new Date(new Date().getTime() - 2 * new Date(props.selectedTag.scanTime * 1000).getTime())),
        //         formatTime(new Date(new Date().getTime() - 1 * new Date(props.selectedTag.scanTime * 1000).getTime())),
        //         formatTime(new Date(new Date().getTime()))],
        //     datasets: [
        //         {
        //             label: 'Recent tag values',
        //             fill: false,
        //             lineTension: 0.1,
        //             borderColor: '#3E86C4',
        //             borderDash: [],
        //             pointBorderWidth: 1,
        //             pointHoverRadius: 5,
        //             data: graphData,
        //         },
        //     ],
        // }
        setGraphData(graphData)
    }


    useEffect(() => {
        if (socket) {
            socket.onmessage = async (event) => {
                let message: string = await event.data.text();
                let tokens = message.split("=>");
                message = JSON.parse(tokens[1]);
                console.log('Received message:', message);
                processValue(message)
            }
        }
    }, [graphData, tagName])

    useEffect(() => {
        console.log(props.selectedTag)
        if (props.selectedTag === undefined || props.selectedTag.tagName === undefined) {
            return
        }
        setTagName(props.selectedTag.tagName);
        fetchData()
    }, [props.selectedTag])


    useEffect(() => {
        if (graphData.length === 0) {
            return
        }
        //console.log(data.datasets[0].data)
        //let oldData = data?.datasets[0].data.slice(1)
        //console.log(oldData);

        // retreive value sent via ws and add it to the beginning of the list
        // newValue = ... 
        //let newData = [...oldData, Math.random() * 100] // should be [...oldData, newValue];
        //console.log("new", newData);
        let labels : string[] = [];
        let data : (number|null)[] = [];
        for (let value of graphData) {
            if (value.value === null) {
                labels.push("")
            } else {
                labels.push(value.timestamp!)
            }
            data.push(value.value)
        }

        let newDataSet = {
            label: 'Recent tag values',
            fill: false,
            lineTension: 0.1,
            borderColor: '#3E86C4',
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            data: data,
        };

        let newState = {
            labels: labels,
            datasets: [newDataSet],
        };

        setData(newState);
    }, [graphData]); // change on ws, remove "data"


    return (
        <div className={style.card}>
            Visualization ({props.selectedTag.tagName})
            <CDBContainer>
                {data !== undefined &&
                    <Line data={data} options={{
                        responsive: true, scales: {
                            y: {
                                suggestedMin: props.selectedTag.range.min,
                                suggestedMax: props.selectedTag.range.max
                            }
                        }
                    }} height={"300px"} />
                }
            </CDBContainer>
        </div>
    );
};

export default GraphComponent;