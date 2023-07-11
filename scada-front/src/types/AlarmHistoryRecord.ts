export class AlarmHistoryRecord {
    alarmName: string;
    timestamp: Date;
    tagValue: number;
    message: string;

    constructor(alarmName: string, timestamp: Date, tagValue: number, message: string) {
        this.alarmName = alarmName;
        this.timestamp = timestamp;
        this.tagValue = tagValue;
        this.message = message;
    }
}