import React from "react";
import { Button, Modal, OverlayTrigger, Tooltip } from 'react-bootstrap';
import style from './TagDetailsModal.module.css';

// TODO scan on empty
const TagDetailsModal = (props: { showModal: boolean, handleCloseModal: any, selectedTag: any }) => {
    const [editMode, setEditMode] = React.useState(false);
    const [editedValue, setEditedValue] = React.useState(props.selectedTag.initialValue);

    const handleEditClick = () => {
        setEditMode(true);
    };

    const handleSaveClick = () => {
        // Perform the save action with the editedValue
        console.log("Save:", editedValue);

        // Exit edit mode and update the actual value
        setEditMode(false);
        props.selectedTag.initialValue = editedValue;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEditedValue(e.target.value);
    };

    return (
        <Modal show={props.showModal} onHide={() => { props.handleCloseModal(); setEditMode(false) }}>
            <Modal.Header closeButton>
                <Modal.Title>Tag details</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                    <div style={{ textAlign: 'left' }}>
                        <p><strong>Name</strong></p>
                        <p><strong>Type</strong></p>
                        <p><strong>I/O Address:</strong></p>
                        {props.selectedTag.tagType === 'digital_input' && (
                            <>
                                <p><strong>Scan Time</strong></p>
                                <p><strong>Scan On</strong></p>
                                <p><strong>Driver</strong></p>
                            </>
                        )}
                        {props.selectedTag.tagType === 'digital_output' && (
                            <>
                                <p><strong>Value</strong></p>
                            </>
                        )}
                        {props.selectedTag.tagType === 'analog_input' && (
                            <>
                                <p><strong>Scan Time</strong></p>
                                <p><strong>Scan On</strong></p>
                                <p><strong>Low Limit</strong></p>
                                <p><strong>High Limit</strong></p>
                                <p><strong>Units</strong></p>
                                <p><strong>Driver</strong></p>
                            </>
                        )}
                        {props.selectedTag.tagType === 'analog_output' && (
                            <>
                                <p><strong>Low Limit</strong></p>
                                <p><strong>High Limit</strong></p>
                                <p><strong>Units</strong></p>
                                <p><strong>Value</strong></p>
                            </>
                        )}
                    </div>
                    <div style={{ textAlign: 'right' }}>
                        <p>{props.selectedTag.tagName}</p>
                        <p>{props.selectedTag.tagType}</p>
                        <p>{props.selectedTag.ioAddress}</p>
                        {props.selectedTag.tagType === 'digital_input' && (
                            <>
                                <p>{props.selectedTag.scanTime}</p>
                                <p>{props.selectedTag.scanOn}</p>
                                <p>{props.selectedTag.driver}</p>
                            </>
                        )}
                        {props.selectedTag.tagType === 'digital_output' && (
                            <>
                                {editMode ? (
                                    <div className={style.inline}>
                                        <input
                                            type="text"
                                            value={editedValue}
                                            onChange={handleChange}
                                        />
                                        <Button variant="primary" size="sm" onClick={handleSaveClick}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="remove-tooltip">Save value</Tooltip>}
                                            >
                                                <i className="bi bi-pencil"></i>
                                            </OverlayTrigger>
                                        </Button>
                                    </div>
                                ) : (
                                    <div className={style.inline}>
                                        <span>{props.selectedTag.initialValue}</span>
                                        <Button variant="white" size="sm" onClick={handleEditClick}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="remove-tooltip">Edit value</Tooltip>}
                                            >
                                                <i className="bi bi-pencil"></i>
                                            </OverlayTrigger>
                                        </Button>
                                    </div>
                                )}
                            </>
                        )}
                        {props.selectedTag.tagType === 'analog_input' && (
                            <>
                                <p>{props.selectedTag.scanTime}</p>
                                <p>{props.selectedTag.scanOn}</p>
                                <p>{props.selectedTag.lowLimit}</p>
                                <p>{props.selectedTag.highLimit}</p>
                                <p>{props.selectedTag.units}</p>
                                <p>{props.selectedTag.driver}</p>
                            </>
                        )}
                        {props.selectedTag.tagType === 'analog_output' && (
                            <>
                                <p>{props.selectedTag.lowLimit}</p>
                                <p>{props.selectedTag.highLimit}</p>
                                <p>{props.selectedTag.units}</p>
                                <>
                                    {editMode ? (
                                        <div className={style.inline}>
                                            <input
                                                type="text"
                                                value={editedValue}
                                                onChange={handleChange}
                                            />
                                            <Button variant="primary" size="sm" onClick={handleSaveClick}>
                                                <OverlayTrigger
                                                    placement="bottom"
                                                    overlay={<Tooltip id="remove-tooltip">Save value</Tooltip>}
                                                >
                                                    <i className="bi bi-pencil"></i>
                                                </OverlayTrigger>
                                            </Button>
                                        </div>
                                    ) : (
                                        <div className={style.inline}>
                                            <span>{props.selectedTag.initialValue}</span>
                                            <Button variant="white" size="sm" onClick={handleEditClick}>
                                                <OverlayTrigger
                                                    placement="bottom"
                                                    overlay={<Tooltip id="remove-tooltip">Edit value</Tooltip>}
                                                >
                                                    <i className="bi bi-pencil"></i>
                                                </OverlayTrigger>
                                            </Button>
                                        </div>
                                    )}
                                </>
                            </>
                        )}
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => { props.handleCloseModal(); setEditMode(false) }}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal >
    );
}

export default TagDetailsModal;
