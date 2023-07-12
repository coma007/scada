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
    const [graphData, setGraphData] = useState<(number|null)[]>([])
    const [socket, setSocket] = useState<WebSocket | null>(null);
    const [tagName, setTagName] =  useState<string>("");
    
    useEffect(() => {
        return WebSocketService.createSocket(setSocket);
    }, []);
    
    const processValue = (message: any) => {
        //console.log(message);
        let newTagValue : TagHistoryRecord = new TagHistoryRecord(message.TagName, message.Timestamp, message.TagValue);
        if (newTagValue.tagName === tagName) {
            let newData = [...graphData, newTagValue.tagValue]
            newData.shift()
            console.log(newData)
            setGraphData(newData)
        }
    }
    
    useEffect(() => {
    WebSocketService.defineSocket(socket, "NewTagRecordCreated", processValue);
    }, [socket]);

    const getLastData = (data: TagHistoryRecord[]) : (number | null)[] => {
        if (data.length >= 5) {
            return [data[4].tagValue, data[3].tagValue, data[2].tagValue, data[1].tagValue, data[0].tagValue]
        }
        else {
            let newData : (number | null)[] = []
            for (let record of data) {
                newData = [record.tagValue, ...newData]
            }
            let length = newData.length;
            while (length < 5) {
                newData = [null, ...newData]
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
    },[graphData, tagName])

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

        let newDataSet = {
            label: 'Recent tag values',
            fill: false,
            lineTension: 0.1,
            borderColor: '#3E86C4',
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            data: graphData,
        };

        let newState = {
            labels:
                [formatTime(new Date(new Date().getTime() - 4 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 3 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 2 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 1 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime()))],
            datasets: [newDataSet],
        };

        setData(newState);
    }, [graphData]); // change on ws, remove "data"


    return (
        <div className={style.card}>
            <CDBContainer>
                {data !== undefined &&
                    <Line data={data} options={{ responsive: true, scales: {
                        y : {
                            suggestedMin: props.selectedTag.range.min,
                            suggestedMax: props.selectedTag.range.max
                        }
                    } }} height={"300px"} />
                }
            </CDBContainer>
        </div>
    );
};

export default GraphComponent;