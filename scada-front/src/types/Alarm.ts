export class Alarm {
    type: string;
    alarmPriority: number;
    limit: number;
    alarmName: string;
    tagName: string;

    constructor(type: string, alarmPriority: number, limit: number, alarmName: string, tagName: string) {
        this.type = type;
        this.alarmPriority = alarmPriority;
        this.limit = limit;
        this.alarmName = alarmName;
        this.tagName = tagName;
    }
}