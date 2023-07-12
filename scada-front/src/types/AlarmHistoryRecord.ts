export class AlarmHistoryRecord {
    alarmName: string;
    timestamp: Date;
    tagValue: number;
    message: string;
    tag?: string;

    constructor(alarmName: string, timestamp: Date, tagValue: number, message: string, tag?: string) {
        this.alarmName = alarmName;
        this.timestamp = timestamp;
        this.tagValue = tagValue;
        this.message = message;
        this.tag = tag;
    }
}

export interface AllAlarms {
    tagName : string,
    tagAlarms : AlarmHistoryRecord[]
}