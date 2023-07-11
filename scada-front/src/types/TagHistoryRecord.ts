export class TagHistoryRecord {
    tagName: string;
    timestamp: Date;
    tagValue: number;

    constructor(tagName: string, timestamp: Date, tagValue: number) {
        this.tagName = tagName;
        this.timestamp = timestamp;
        this.tagValue = tagValue;
    }
}