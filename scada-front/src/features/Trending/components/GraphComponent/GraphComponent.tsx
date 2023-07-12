import React, { useState, useEffect } from 'react';
import { Line } from 'react-chartjs-2';
import { CDBContainer } from 'cdbreact';

import { Chart, registerables } from 'chart.js';
import style from './GraphComponent.module.css';
import { formatTime } from '../../../../utils/DateFormatter';

Chart.register(...registerables);


const GraphComponent = (props: { selectedTag: any }) => {
    const [data, setData] = useState<{ labels: any, datasets: any } | undefined>(undefined);

    useEffect(() => {
        if (props.selectedTag === undefined) {
            return
        }
        let newData = {
            labels:
                [formatTime(new Date(new Date().getTime() - 4 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 3 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 2 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime() - 1 * new Date(props.selectedTag.scanTime * 1000).getTime())),
                formatTime(new Date(new Date().getTime()))],
            datasets: [
                {
                    label: 'Recent tag values',
                    fill: false,
                    lineTension: 0.1,
                    borderColor: '#3E86C4',
                    borderDash: [],
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    data: [65, 59, 80, 81, 56],
                },
            ],
        }
        setData(newData)
    }, [props.selectedTag])


    useEffect(() => {
        if (data === undefined) {
            return
        }
        console.log(data.datasets[0].data)
        let oldData = data?.datasets[0].data.slice(1)
        console.log(oldData);

        // retreive value sent via ws and add it to the beginning of the list
        // newValue = ... 
        let newData = [...oldData, Math.random() * 100] // should be [...oldData, newValue];
        console.log("new", newData);

        let newDataSet = {
            label: 'Recent tag values',
            fill: false,
            lineTension: 0.1,
            borderColor: '#3E86C4',
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            data: newData,
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

        setTimeout(() => {

            setData(newState);    // remove setTimeout, keep setData
        },
            5000);


    }, [data]); // change on ws, remove "data"


    return (
        <div className={style.card}>
            <CDBContainer>
                {data !== undefined &&
                    <Line data={data} options={{ responsive: true }} height={"300px"} />
                }
            </CDBContainer>
        </div>
    );
};

export default GraphComponent;