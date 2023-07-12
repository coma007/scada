import axios from "axios"
import { ALARM_HISTORY_PRIORITY, ALARM_HISTORY_TIMESPAN, GET_ALARM, GET_TAG, TAG_HISTORY, TAG_HISTORY_TIMESPAN, TAG_HISTORY_TYPE } from "../../../api"
import { AlarmHistoryRecord } from "../../../types/AlarmHistoryRecord"
import { TagHistoryRecord } from "../../../types/TagHistoryRecord"
import { Alarm } from "../../../types/Alarm"
import { Tag } from "../../../types/Tag"

export const ReportService = {
    getAlarm : async function(alarmName:string) : Promise<Alarm> {
        return await axios.get(GET_ALARM(), {
            params : {
                alarmName: alarmName
            }
        }).then(response => {
            console.log(response.data)
            let alarm : Alarm = response.data;
            return alarm
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getTag : async function(tagName:string) : Promise<Tag> {
        return await axios.get(GET_TAG(), {
            params : {
                tagName: tagName
            }
        }).then(response => {
            console.log(response.data)
            let tag : Tag = response.data;
            return tag
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getAlarmHistoryTimespan: async function(dateFrom: Date, dateTo: Date) : Promise<AlarmHistoryRecord[]> {
        return await axios.get(ALARM_HISTORY_TIMESPAN(), {
            params : {
                start: dateFrom,
                end: dateTo
            }
        }).then(response => {
            let alarms : AlarmHistoryRecord[] = response.data;
            return alarms
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getAlarmHistoryPriority: async function(dateFrom: number) : Promise<AlarmHistoryRecord[]> {
        return await axios.get(ALARM_HISTORY_PRIORITY(), {
            params : {
                priority: dateFrom
            }
        }).then(response => {
            let alarms : AlarmHistoryRecord[] = response.data;
            return alarms
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getTagHistoryTimespan: async function(dateFrom: Date, dateTo: Date) : Promise<TagHistoryRecord[]> {
        return await axios.get(TAG_HISTORY_TIMESPAN(), {
            params : {
                startDateTime: dateFrom,
                endDateTime: dateTo
            }
        }).then(response => {
            let tags : TagHistoryRecord[] = response.data;
            return tags
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getTagHistoryType: async function(signalType: string) : Promise<TagHistoryRecord[]> {
        return await axios.get(TAG_HISTORY_TYPE(), {
            params : {
                signalType: signalType
            }
        }).then(response => {
            let tags : TagHistoryRecord[] = response.data;
            return tags
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getTagHistory: async function(tagName: string) : Promise<TagHistoryRecord[]> {
        return await axios.get(TAG_HISTORY(), {
            params : {
                tagName: tagName
            }
        }).then(response => {
            let tags : TagHistoryRecord[] = response.data;
            return tags
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },
    
}