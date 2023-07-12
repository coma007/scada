import React from 'react'
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap'
import { AnalogInputTag } from '../../types/Tag'
import style from './TagList.module.css';


const TagList = (props: { tags: any[], viewOnly: boolean, handleOpenDetailsModal: any, handleRemoveTag?: any, handleScanTag?: any, handleOpenAlarmsModal?: any }) => {
    return (
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th className={style.nameColumn}>Name</th>
                    <th className={style.descriptionColumn}>Description</th>
                    <th >I/O Address</th>
                    <th className={style.typeColumn}> Type</th>
                    <th >Actions</th>
                </tr>
            </thead>
            <tbody>
                {props.tags.map((tag) => (
                    <tr key={tag.tagName}>
                        <td >{tag.tagName}</td>
                        <td >{tag.description}</td>
                        <td >{tag.ioAddress}</td>
                        <td >{tag.tagType}</td>
                        <td >
                            <Button variant="info" size="sm" onClick={() => props.handleOpenDetailsModal(tag)}>
                                <OverlayTrigger
                                    placement="bottom"
                                    overlay={<Tooltip id="info-tooltip">Tag details</Tooltip>}
                                >
                                    <i className="bi bi-info-circle"></i>
                                </OverlayTrigger>
                            </Button>{' '}
                            {!props.viewOnly &&
                                <>
                                    <Button variant="danger" size="sm" onClick={() => props.handleRemoveTag(tag.tagName)}>
                                        <OverlayTrigger
                                            placement="bottom"
                                            overlay={<Tooltip id="remove-tooltip">Remove tag</Tooltip>}
                                        >
                                            <i className="bi bi-trash"></i>
                                        </OverlayTrigger>
                                    </Button>{' '}
                                    {(tag.tagType == "analog_input" || tag.tagType == "digital_input") && <>
                                        {((tag as AnalogInputTag).scan) ? <>
                                            <Button variant="dark" size="sm" onClick={() => props.handleScanTag(tag.tagName)}>
                                                <OverlayTrigger
                                                    placement="bottom"
                                                    overlay={<Tooltip id="scan-tooltip">Turn off scan</Tooltip>}
                                                >
                                                    <i className="bi bi-play-circle"></i>
                                                </OverlayTrigger>
                                            </Button>{' '}
                                        </> : <>
                                            <Button variant="success" size="sm" onClick={() => props.handleScanTag(tag.tagName)}>
                                                <OverlayTrigger
                                                    placement="bottom"
                                                    overlay={<Tooltip id="scan-tooltip">Turn on scan</Tooltip>}
                                                >
                                                    <i className="bi bi-play-circle"></i>
                                                </OverlayTrigger>
                                            </Button>{' '}
                                        </>
                                        }
                                    </>
                                    }
                                    {(tag.tagType == "analog_input") && <Button variant="warning" size="sm" onClick={() => props.handleOpenAlarmsModal(tag)}>
                                        <OverlayTrigger
                                            placement="bottom"
                                            overlay={<Tooltip id="info-tooltip">View alarms</Tooltip>}
                                        >
                                            <i className="bi bi-alarm"></i>
                                        </OverlayTrigger>
                                    </Button>
                                    }
                                </>
                            }
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>


    )
}

export default TagList