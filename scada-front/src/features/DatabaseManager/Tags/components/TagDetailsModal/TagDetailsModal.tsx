import React from "react";
import { Button, Modal } from 'react-bootstrap';

const TagDetailsModal = (props: { showModal: boolean, handleCloseModal: any, selectedTag: any }) => {
    return (
        <Modal show={props.showModal} onHide={props.handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Details for '{props.selectedTag.name}'</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                    <div style={{ textAlign: 'left' }}>
                        <p><strong>Name:</strong></p>
                        <p><strong>Type:</strong></p>
                        <p><strong>I/O Address:</strong></p>
                        {props.selectedTag.type === 'digital_input' && (
                            <>
                                <p><strong>Scan Time:</strong></p>
                                <p><strong>Scan On:</strong></p>
                            </>
                        )}
                        {props.selectedTag.type === 'analog_input' && (
                            <>
                                <p><strong>Scan Time:</strong></p>
                                <p><strong>Scan On:</strong></p>
                                <p><strong>Low Limit:</strong></p>
                                <p><strong>High Limit:</strong></p>
                                <p><strong>Units:</strong></p>
                            </>
                        )}
                        {props.selectedTag.type === 'analog_output' && (
                            <>
                                <p><strong>Low Limit:</strong></p>
                                <p><strong>High Limit:</strong></p>
                                <p><strong>Units:</strong></p>
                            </>
                        )}
                    </div>
                    <div style={{ textAlign: 'right' }}>
                        <p>{props.selectedTag.name}</p>
                        <p>{props.selectedTag.type}</p>
                        <p>{props.selectedTag.ioAddress}</p>
                        {props.selectedTag.type === 'digital_input' && (
                            <>
                                <p>{props.selectedTag.scanTime}</p>
                                <p>{props.selectedTag.scanOn}</p>
                            </>
                        )}
                        {props.selectedTag.type === 'analog_input' && (
                            <>
                                <p>{props.selectedTag.scanTime}</p>
                                <p>{props.selectedTag.scanOn}</p>
                                <p>{props.selectedTag.lowLimit}</p>
                                <p>{props.selectedTag.highLimit}</p>
                                <p>{props.selectedTag.units}</p>
                            </>
                        )}
                        {props.selectedTag.type === 'analog_output' && (
                            <>
                                <p>{props.selectedTag.lowLimit}</p>
                                <p>{props.selectedTag.highLimit}</p>
                                <p>{props.selectedTag.units}</p>
                            </>
                        )}
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.handleCloseModal}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default TagDetailsModal;