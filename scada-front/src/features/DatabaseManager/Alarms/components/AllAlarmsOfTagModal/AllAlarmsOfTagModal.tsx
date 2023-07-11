import React, { useEffect } from 'react'
import { Button, Form, Modal, OverlayTrigger, Table, Tooltip } from 'react-bootstrap'
import style from './AllAlarmsOfTagModal.module.css';
import { Alarm } from '../../types/Alarm';

const AllAlarmsOfTagModal = (props: { showModal: boolean, handleCloseModal: any, selectedTag: any }) => {
  const dummyAlarms: Alarm[] = [
    new Alarm('High', 1, 100, 'Alarm 1', 'Tag 1'),
    new Alarm('Medium', 2, 200, 'Alarm 2', 'Tag 2'),
    new Alarm('Low', 3, 300, 'Alarm 3', 'Tag 3'),
    new Alarm('Medium', 1, 150, 'Alarm 4', 'Tag 1'),
    new Alarm('High', 2, 250, 'Alarm 5', 'Tag 2'),
  ];

  const [alarms, setAlarms] = React.useState<Alarm[]>([]);

  useEffect(() => {
    setAlarms(dummyAlarms);
  }, [])

  const [newAlarm, setNewAlarm] = React.useState(new Alarm('', 0, 0, '', props.selectedTag.name));
  const [isAddingActive, setIsAddingActive] = React.useState(false);

  const handleAddNewAlarm = () => {
    setIsAddingActive(true);
    setNewAlarm(new Alarm('', 0, 0, '', props.selectedTag.name));
  };

  const handleCloseNewAlarm = () => {
    setIsAddingActive(false);
  };

  const handleCreateAlarm = () => {
    // Api request
    // ...

    alarms.push(newAlarm);
    setAlarms(alarms);
  };

  const handleRemoveAlarm = (selectedAlarm: Alarm) => {
    // Api request
    // ...

    let updatedList = alarms.filter(alarm => alarm !== selectedAlarm);
    setAlarms(updatedList);
  };


  return (
    <Modal dialogClassName='alarms-dialog' show={props.showModal} onHide={props.handleCloseModal}>
      <Modal.Header closeButton>
        <div className="title-line">
          <Modal.Title>Alarms of tag '{props.selectedTag.name}'</Modal.Title>
          {!isAddingActive &&
            <Button variant="primary" onClick={handleAddNewAlarm}>
              Add new alarm
            </Button>
          }
        </div>
      </Modal.Header>
      <Modal.Body>
        <Table striped bordered>
          <thead>
            <tr>
              <th className={style.textCol}>Alarm Name</th>
              <th className={style.textCol}>Type</th>
              <th className={style.numberCol}>Priority</th>
              <th className={style.numberCol}>Limit</th>
              <th className={style.actionsCol}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {isAddingActive &&
              <tr>
                <td>
                  <Form.Control
                    type="text"
                    value={newAlarm.alarmName}
                    onChange={(e) => setNewAlarm({ ...newAlarm, alarmName: e.target.value })}
                  />
                </td>
                <td>
                  <Form.Control
                    type="text"
                    value={newAlarm.type}
                    onChange={(e) => setNewAlarm({ ...newAlarm, type: e.target.value })}
                  />
                </td>
                <td >
                  <Form.Control
                    type="number"
                    value={newAlarm.alarmPriority}
                    onChange={(e) => setNewAlarm({ ...newAlarm, alarmPriority: parseInt(e.target.value) })}
                  />
                </td>
                <td >
                  <Form.Control
                    type="number"
                    value={newAlarm.limit}
                    onChange={(e) => setNewAlarm({ ...newAlarm, limit: parseInt(e.target.value) })}
                  />
                </td>
                <td>
                  <Button variant="primary" size="sm" onClick={handleCreateAlarm}>
                    <OverlayTrigger
                      placement="bottom"
                      overlay={<Tooltip id="info-tooltip">Create alarm</Tooltip>}
                    >
                      <i className="bi bi-plus-lg"></i>
                    </OverlayTrigger>
                  </Button>{' '}
                  <Button variant="secondary" size="sm" onClick={handleCloseNewAlarm}>
                    <OverlayTrigger
                      placement="bottom"
                      overlay={<Tooltip id="info-tooltip">Cancel</Tooltip>}
                    >
                      <i className="bi bi-x-lg"></i>
                    </OverlayTrigger>
                  </Button>
                </td>
              </tr>
            }
            {/* Render existing alarms */}
            {alarms.map((alarm, index) => (
              <tr key={index}>
                <td>{alarm.alarmName}</td>
                <td>{alarm.type}</td>
                <td>{alarm.alarmPriority}</td>
                <td>{alarm.limit}</td>
                <td>
                  <Button variant="danger" size="sm" onClick={() => handleRemoveAlarm(alarm)}>
                    <OverlayTrigger
                      placement="bottom"
                      overlay={<Tooltip id="remove-tooltip">Remove alarm</Tooltip>}
                    >
                      <i className="bi bi-trash"></i>
                    </OverlayTrigger>
                  </Button>{' '}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.handleCloseModal}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default AllAlarmsOfTagModal;