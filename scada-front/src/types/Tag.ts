export abstract class Tag {
    tagName: string;
    tagType: string;
    description: string;
    ioAddress: number;

    constructor(tagName: string, tagType: string, description: string, ioAddress: number) {
        this.tagName = tagName;
        this.tagType = tagType;
        this.description = description;
        this.ioAddress = ioAddress;
    }
}

export class DigitalInputTag extends Tag {
    scanTime: number;
    scan: boolean;
    driver: string;

    constructor(tagName: string, tagType: string, description: string, ioAddress: number, scanTime: number, scan: boolean, driver: string) {
        super(tagName, tagType, description, ioAddress);
        this.scanTime = scanTime;
        this.scan = scan;
        this.driver = driver;
    }
}

export class DigitalOutputTag extends Tag {
    initialValue: number;

    constructor(tagName: string, tagType: string, description: string, ioAddress: number, initialValue: number) {
        super(tagName, tagType, description, ioAddress);
        this.initialValue = initialValue;
    }
}

export class AnalogInputTag extends Tag {
    scanTime: number;
    scan: boolean;
    lowLimit: number;
    highLimit: number;
    driver: string;
    units: string;

    constructor(
        tagName: string,
        tagType: string,
        description: string,
        ioAddress: number,
        scanTime: number,
        scan: boolean,
        lowLimit: number,
        highLimit: number,
        units: string,
        driver: string
    ) {
        super(tagName, tagType, description, ioAddress);
        this.scanTime = scanTime;
        this.scan = scan;
        this.lowLimit = lowLimit;
        this.highLimit = highLimit;
        this.units = units;
        this.driver = driver;
    }
}

export class AnalogOutputTag extends Tag {
    initialValue: number;
    lowLimit: number;
    highLimit: number;
    units: string;

    constructor(
        tagName: string,
        tagType: string,
        description: string,
        ioAddress: number,
        initialValue: number,
        lowLimit: number,
        highLimit: number,
        units: string
    ) {
        super(tagName, tagType, description, ioAddress);
        this.initialValue = initialValue;
        this.lowLimit = lowLimit;
        this.highLimit = highLimit;
        this.units = units;
    }
}
