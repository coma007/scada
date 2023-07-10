import React from 'react';
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import style from './AllTagsPage.module.css';
import TagDetailsModal from '../../components/TagDetailsModal/TagDetailsModal';

const AllTagsPage: React.FC = () => {

    // Replace this with  actual data (useState)
    const tags = [
        {
            name: 'Tag 1',
            description: 'Description 1',
            ioAddress: 'IO Address 1',
            type: 'analog_input',
            scan: true
        },
        {
            name: 'Tag 2',
            description: 'Description 2',
            ioAddress: 'IO Address 2',
            type: 'analog_output',
        },
    ];

    const [showModal, setShowModal] = React.useState(false);
    const [selectedTag, setSelectedTag] = React.useState<any>(null);

    const handleOpenModal = (tag: any) => {
        setSelectedTag(tag);
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setShowModal(false);
    };

    // Function to handle tag removal (to be implemented)
    const handleRemoveTag = (tagName: string) => {
        // Make HTTP request to remove the tag using the provided tagId
        console.log(`Remove tag with ID: ${tagName}`);
    };

    // Function to handle scan button click (to be implemented)
    const handleScanButtonClick = (tagName: string) => {
        // Make HTTP request to toggle scan for the tag using the provided tagId
        console.log(`Toggle scan for tag with ID: ${tagName}`);
    };

    return (
        <div className={style.content}>
            <h3>All Tags</h3>
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
                    {tags.map((tag) => (
                        <tr key={tag.name}>
                            <td >{tag.name}</td>
                            <td >{tag.description}</td>
                            <td >{tag.ioAddress}</td>
                            <td >{tag.type}</td>
                            <td >
                                <Button variant="info" size="sm" onClick={() => handleOpenModal(tag)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="info-tooltip">Tag details</Tooltip>}
                                    >
                                        <i className="bi bi-info-circle"></i>
                                    </OverlayTrigger>
                                </Button>{' '}
                                <Button variant="danger" size="sm" onClick={() => handleRemoveTag(tag.name)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="remove-tooltip">Remove tag</Tooltip>}
                                    >
                                        <i className="bi bi-trash"></i>
                                    </OverlayTrigger>
                                </Button>{' '}
                                {(tag.type == "analog_input" || tag.type == "digital_input") && <>
                                    {(tag.scan) ? <>
                                        <Button variant="dark" size="sm" onClick={() => handleScanButtonClick(tag.name)}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="scan-tooltip">Turn off scan</Tooltip>}
                                            >
                                                <i className="bi bi-play-circle"></i>
                                            </OverlayTrigger>
                                        </Button>{' '}
                                    </> : <>
                                        <Button variant="primary" size="sm" onClick={() => handleScanButtonClick(tag.name)}>
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
                                {(tag.type == "analog_input") && <Button variant="warning" size="sm" onClick={() => handleOpenModal(tag)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="info-tooltip">View alarms</Tooltip>}
                                    >
                                        <i className="bi bi-alarm"></i>
                                    </OverlayTrigger>
                                </Button>
                                }
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            {selectedTag && (
                <TagDetailsModal
                    selectedTag={selectedTag}
                    showModal={showModal}
                    handleCloseModal={handleCloseModal} />
            )}
        </div >
    );
}

export default AllTagsPage;
