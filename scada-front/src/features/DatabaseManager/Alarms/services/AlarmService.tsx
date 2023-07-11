import axios from 'axios';
import { CREATE_ALARM, GET_ALARMS, GET_ALARM_BY_TAG } from '../../../../api';
import { Alarm } from '../types/Alarm';

const AlarmsService = {

    getAll: async function (): Promise<Alarm[]> {
        return axios.get(GET_ALARMS())
            .then(response => {
                let data: Alarm[]= response.data.map((alarm: any) => {
                    new Alarm(alarm.type, alarm.alarmPriority, alarm.limit, alarm.alarmName, alarm.tagName);
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    },

    getByTagName: async function (name: string): Promise<Alarm[]> {
        let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
        return axios.get(GET_ALARM_BY_TAG(), {headers: {Authorization: "Bearer " + token}, params : {name : name}})
            .then(response => {
                let data: Alarm[]= response.data.map((alarm: any) => {
                    return new Alarm(alarm.type, alarm.alarmPriority, alarm.limit, alarm.alarmName, alarm.tagName);
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    },


//     // TODO fetch  token
//     delete: async function (tagName: string): Promise<Tag> {
//         let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
//         return axios.delete(DELETE_TAG(), {headers: {Authorization: "Bearer " + token}, params : {tagName : tagName}})
//             .then(response => {
//                 let data: Tag = response.data;
//                 return data;
//             })
//             .catch(error => {
//                 console.log(error)
//                 throw new Error(error);
//             });
//     },

//     // TODO fetch  token
//     updateScan: async function (tagName: string): Promise<Tag> {
//         let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
//         return axios.patch(UPDATE_TAG_SCAN(), null, {headers: {Authorization: "Bearer " + token}, params : {tagName : tagName}})
//             .then(response => {
//                 let data: Tag = response.data;
//                 return data;
//             })
//             .catch(error => {
//                 console.log(error)
//                 throw new Error(error);
//             });
//     },

//     // TODO fetch  token
//     create: async function (tag: Tag): Promise<Tag> {
//         let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
//         return axios.post(CREATE_TAG(), tag, {headers: {Authorization: "Bearer " + token}})
//             .then(response => {
//                 let data: Tag = response.data;
//                 return data;
//             })
//             .catch(error => {
//                 console.log(error)
//                 throw new Error(error);
//             });
//     }
}

export default AlarmsService;