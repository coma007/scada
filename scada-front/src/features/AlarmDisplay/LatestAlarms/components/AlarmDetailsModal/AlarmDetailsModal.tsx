import React from 'react'
import { Modal, Button } from 'react-bootstrap';
import { Alarm } from '../../../../DatabaseManager/Alarms/types/Alarm';

const AlarmDetailsModal = (props: { showModal: boolean, handleCloseModal: any, selectedAlarm: Alarm }) => {

    const [alarm, setAlarm] = React.useState<Alarm>(new Alarm("", 0, 0, "", ""));
    React.useEffect(() => {
        setAlarm(props.selectedAlarm)
    }, [])

    return (
        <Modal show={props.showModal} onHide={() => { props.handleCloseModal() }}>
            <Modal.Header closeButton>
                <Modal.Title>Tag details</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div style={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                    <div style={{ textAlign: 'left' }}>
                        <p><strong>Name</strong></p>
                        <p><strong>Tag</strong></p>
                        <p><strong>Limit</strong></p>
                        <p><strong>Type</strong></p>
                        <p><strong>Priority</strong></p>
                    </div>
                    <div style={{ textAlign: 'right' }}>
                        <p>{alarm.alarmName}</p>
                        <p>{alarm.tagName}</p>
                        <p>{alarm.limit}</p>
                        <p>{alarm.type}</p>
                        <p>{alarm.alarmPriority}</p>
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => { props.handleCloseModal() }}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal >
    )
}

export default AlarmDetailsModal;