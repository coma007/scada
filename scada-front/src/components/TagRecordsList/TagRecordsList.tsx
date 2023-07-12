import React from 'react'
import { TagHistoryRecord } from '../../types/TagHistoryRecord';
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import { formatDate } from '../../utils/DateFormatter';
import AlarmRecordsList from '../AlarmRecordsList/AlarmRecordsList';
import TagDetailsModal from '../TagDetailsModal/TagDetailsModal';
import { Tag } from '../../types/Tag';
import style from './TagRecordsList.module.css';

const TagRecordsList = (props: { tagRecords: TagHistoryRecord[], setTagRecords: any, viewGraph?: boolean, handleViewGraph?: any }) => {

    const [selectedRecord, setSelectedRecord] = React.useState<TagHistoryRecord | undefined>();
    const [selectedTag, setSelectedTag] = React.useState<Tag | undefined>();
    const [showDetailsModal, setShowDetailsModal] = React.useState(false);

    const handleOpenDetailsModal = (record: TagHistoryRecord) => {
        setSelectedRecord(record);
        // find tag based on tagName in record
        // setSelectedTag(...)
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
    };


    return (
        <div>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th className={style.nameColumn}>Tag Name</th>
                        <th className={style.timestampColumn}>Timestamp</th>
                        <th className={style.valueColumn}>Value</th>
                        <th >Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {props.tagRecords.map((tagRecord) => (
                        <tr key={tagRecord.tagName}>
                            <td >{tagRecord.tagName}</td>
                            <td >{formatDate(tagRecord.timestamp)}</td>
                            <td >{tagRecord.tagValue}</td>
                            <td >
                                <Button variant="info" size="sm" onClick={() => handleOpenDetailsModal(tagRecord)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="remove-tooltip">Alarm details</Tooltip>}
                                    >
                                        <i className="bi bi-info-circle"></i>
                                    </OverlayTrigger>
                                </Button>{' '}

                                {props.viewGraph &&
                                    <Button variant="secondary" size="sm" onClick={() => props.handleViewGraph(tagRecord.tagName)}>
                                        <OverlayTrigger
                                            placement="bottom"
                                            overlay={<Tooltip id="remove-tooltip">View history</Tooltip>}
                                        >
                                            <i className="bi bi-bar-chart-fill"></i>
                                        </OverlayTrigger>
                                    </Button>
                                }
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            {selectedRecord && (
                <TagDetailsModal
                    selectedTag={selectedTag!}
                    showModal={showDetailsModal}
                    handleCloseModal={handleCloseDetailsModal} />
            )}

        </div>
    );
}

export default TagRecordsList;