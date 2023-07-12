import axios from "axios"
import { ALARM_HISTORY_PRIORITY, ALARM_HISTORY_TIMESTAMP } from "../../../api"
import { AlarmHistoryRecord } from "../../../types/AlarmHistoryRecord"

export const ReportService = {
    getAlarmHistoryTimespan: async function(dateFrom: Date, dateTo: Date) : Promise<AlarmHistoryRecord[]> {
        return await axios.get(ALARM_HISTORY_TIMESTAMP(), {
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
    }

    
}