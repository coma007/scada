export abstract class Tag {
    name: string;
    type: string;
    description: string;
    ioAddress: number;

    constructor(name: string, type: string, description: string, ioAddress: number) {
        this.name = name;
        this.type = type;
        this.description = description;
        this.ioAddress = ioAddress;
    }
}

export class DigitalInputTag extends Tag {
    scanTime: number;
    scan: boolean;

    constructor(name: string, type: string, description: string, ioAddress: number, scanTime: number, scan: boolean) {
        super(name, type, description, ioAddress);
        this.scanTime = scanTime;
        this.scan = scan;
    }
}

export class DigitalOutputTag extends Tag {
    initialValue: number;

    constructor(name: string, type: string, description: string, ioAddress: number, initialValue: number) {
        super(name, type, description, ioAddress);
        this.initialValue = initialValue;
    }
}

export class AnalogInputTag extends Tag {
    scanTime: number;
    scan: boolean;
    lowLimit: number;
    highLimit: number;
    units: string;

    constructor(
        name: string,
        type: string,
        description: string,
        ioAddress: number,
        scanTime: number,
        scan: boolean,
        lowLimit: number,
        highLimit: number,
        units: string
    ) {
        super(name, type, description, ioAddress);
        this.scanTime = scanTime;
        this.scan = scan;
        this.lowLimit = lowLimit;
        this.highLimit = highLimit;
        this.units = units;
    }
}

export class AnalogOutputTag extends Tag {
    initialValue: number;
    lowLimit: number;
    highLimit: number;
    units: string;

    constructor(
        name: string,
        type: string,
        description: string,
        ioAddress: number,
        initialValue: number,
        lowLimit: number,
        highLimit: number,
        units: string
    ) {
        super(name, type, description, ioAddress);
        this.initialValue = initialValue;
        this.lowLimit = lowLimit;
        this.highLimit = highLimit;
        this.units = units;
    }
}
