import React from "react";
import { Alert, Button, Form, Modal } from 'react-bootstrap';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from "../../../../../types/Tag";
import TagService from "../../services/TagService";
import style from './TagCreateModal.module.css';

const TagCreateModal = (props: { showModal: boolean, handleCloseModal: any }) => {
    const [name, setName] = React.useState('');
    const [description, setDescription] = React.useState('');
    const [ioAddress, setIoAddress] = React.useState('');
    const [type, setType] = React.useState('digital_output');
    const [scanTime, setScanTime] = React.useState('');
    const [scanOn, setScanOn] = React.useState("true");
    const [lowLimit, setLowLimit] = React.useState('');
    const [highLimit, setHighLimit] = React.useState('');
    const [units, setUnits] = React.useState('');
    const [initialValue, setInitialValue] = React.useState('');
    const [driver, setDriver] = React.useState('SIMULATION');

    const [errorMessage, setErrorMessage] = React.useState('');

    const handleTypeChange = (event: any) => {
        setType(event.target.value);
    };

    const handleDriverChange = (event: any) => {
        setDriver(event.target.value);
    };

    const handleCloseModal = async () => {
        let tag: Tag = createTag();
        // Close the modal and perform any necessary actions
        try {
            let newTag: Tag = await TagService.create(tag);
            props.handleCloseModal(tag);
            setErrorMessage('');
        } catch (error: any) {
            setErrorMessage(error.message);
        }
    };

    const createTag = (): Tag => {
        switch (type) {
            case 'digital_input':
                return new DigitalInputTag(
                    name,
                    'digital_input',
                    description,
                    Number(ioAddress),
                    Number(scanTime),
                    scanOn === 'true',
                    driver
                );
            case 'digital_output':
                return new DigitalOutputTag(
                    name,
                    'digital_output',
                    description,
                    Number(ioAddress),
                    Number(initialValue)
                );
            case 'analog_input':
                return new AnalogInputTag(
                    name,
                    'analog_input',
                    description,
                    Number(ioAddress),
                    Number(scanTime),
                    scanOn === 'true',
                    Number(lowLimit),
                    Number(highLimit),
                    units,
                    driver
                );
            case 'analog_output':
                return new AnalogOutputTag(
                    name,
                    'analog_output',
                    description,
                    Number(ioAddress),
                    Number(initialValue),
                    Number(lowLimit),
                    Number(highLimit),
                    units
                );
            default:
                throw new Error(`Invalid type: ${type}`);
        }
    };

    return (
        <Modal dialogClassName="dialog" show={props.showModal} onHide={() => props.handleCloseModal()}>
            <Modal.Header closeButton>
                <Modal.Title>Create new tag</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {errorMessage && <Alert variant="danger">{errorMessage}</Alert>}
                <Form >
                    <div className={style.grid}>
                        <Form.Group>
                            <Form.Label>Name</Form.Label>
                            <Form.Control type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Description</Form.Label>
                            <Form.Control type="text" value={description} onChange={(e) => setDescription(e.target.value)} required />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>I/O Address</Form.Label>
                            <Form.Control type="text" value={ioAddress} onChange={(e) => setIoAddress(e.target.value)} required />
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Type</Form.Label>
                            <Form.Control as="select" value={type} onChange={handleTypeChange} required>
                                <option value="digital_input">Digital Input</option>
                                <option value="digital_output">Digital Output</option>
                                <option value="analog_input">Analog Input</option>
                                <option value="analog_output">Analog Output</option>
                            </Form.Control>
                        </Form.Group>
                    </div>
                    <hr className="my-4" />
                    <div className={style.grid}>
                        {/* Conditional rendering of additional fields */}
                        {type === 'digital_input' && (
                            <>
                                <Form.Group>
                                    <Form.Label>Scan Time</Form.Label>
                                    <Form.Control type="number" value={scanTime} onChange={(e) => setScanTime(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Scan On</Form.Label>
                                    <Form.Control as="select" value={scanOn} onChange={(e) => setScanOn(e.target.value)}>
                                        <option value="true">True</option>
                                        <option value="false">False</option>
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Driver</Form.Label>
                                    <Form.Control as="select" value={driver} onChange={handleDriverChange} required>
                                        <option value="SIMULATION">Simulation</option>
                                        <option value="REALTIME">RealTime</option>
                                    </Form.Control>
                                </Form.Group>
                            </>
                        )}

                        {type === 'digital_output' && (
                            <>
                                <Form.Group>
                                    <Form.Label>Initial Value</Form.Label>
                                    <Form.Control type="number" value={initialValue} onChange={(e) => setInitialValue(e.target.value)} />
                                </Form.Group>
                            </>
                        )}

                        {type === 'analog_input' && (
                            <>
                                <Form.Group>
                                    <Form.Label>Scan Time</Form.Label>
                                    <Form.Control type="number" value={scanTime} onChange={(e) => setScanTime(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Scan On</Form.Label>
                                    <Form.Control as="select" value={scanOn} onChange={(e) => setScanOn(e.target.value)}>
                                        <option value="true">True</option>
                                        <option value="false">False</option>
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Low Limit</Form.Label>
                                    <Form.Control type="number" value={lowLimit} onChange={(e) => setLowLimit(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>High Limit</Form.Label>
                                    <Form.Control type="number" value={highLimit} onChange={(e) => setHighLimit(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Units</Form.Label>
                                    <Form.Control type="text" value={units} onChange={(e) => setUnits(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Driver</Form.Label>
                                    <Form.Control as="select" value={driver} onChange={handleDriverChange} required>
                                        <option value="SIMULATION">Simulation</option>
                                        <option value="REALTIME">RealTime</option>
                                    </Form.Control>
                                </Form.Group>
                            </>
                        )}

                        {type === 'analog_output' && (
                            <>
                                <Form.Group>
                                    <Form.Label>Low Limit</Form.Label>
                                    <Form.Control type="number" value={lowLimit} onChange={(e) => setLowLimit(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>High Limit</Form.Label>
                                    <Form.Control type="number" value={highLimit} onChange={(e) => setHighLimit(e.target.value)} />
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Units</Form.Label>
                                    <Form.Control type="text" value={units} onChange={(e) => setUnits(e.target.value)} />
                                </Form.Group><>
                                    <Form.Group>
                                        <Form.Label>Initial Value</Form.Label>
                                        <Form.Control type="number" value={initialValue} onChange={(e) => setInitialValue(e.target.value)} />
                                    </Form.Group>
                                </>
                            </>
                        )}
                    </div>
                    <br />
                    <small>Please note that these are I/O Address distributions
                        <br />
                        - SD for AI signal takes addresses from 0 to 9. <br />
                        (sin: 0, 1, 2; cos: 3, 4, 5; ramp: 6, 7, 8, 9) <br />
                        - SD for DI signal takes addresses from 10 to 99. <br />
                        - RTD for AI signal takes addresses from 100 to 119. <br />
                    </small>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={handleCloseModal}>
                    Save
                </Button>
                <Button variant="secondary" onClick={() => props.handleCloseModal()}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default TagCreateModal;