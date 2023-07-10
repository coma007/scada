import React from 'react';
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import TagDetailsModal from '../../components/TagDetailsModal/TagDetailsModal';
import TagCreateModal from '../../components/TagCreateModal/TagCreateModal';
import style from './AllTagsPage.module.css';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../../types/Tag';
import TagService from '../../services/TagService';

const AllTagsPage: React.FC = () => {

    // Replace this with  actual data (useState)

    // let dummyTags: Tag[] = [
    //     new AnalogInputTag('AnalogInput1', 'analog_input', 'Analog Input Tag 1', 1001, 1000, true, 0, 100, 'V'),
    //     new AnalogOutputTag('AnalogOutput1', 'analog_output', 'Analog Output Tag 1', 2001, 50, 0, 100, 'mA'),
    //     new DigitalInputTag('DigitalInput1', 'digital_input', 'Digital Input Tag 1', 3001, 500, true),
    //     new DigitalOutputTag('DigitalOutput1', 'digital_output', 'Digital Output Tag 1', 4001, 1),
    //     new AnalogInputTag('AnalogInput2', 'analog_input', 'Analog Input Tag 2', 1002, 1000, true, 0, 100, 'V'),
    // ];
    const [tags, setTags] = React.useState<Tag[]>([]);

    const fetchData = async () => {
        try {
          let tags = await TagService.getAll();
          setTags(tags);
        } catch (error) {
          console.error('Error fetching data:', error);
        }
      };

    React.useEffect(() => {
        fetchData();
    }, [])

    const [showDetailsModal, setShowDetailsModal] = React.useState(false);
    const [selectedTag, setSelectedTag] = React.useState<any>(null);

    const handleOpenDetailsModal = (tag: Tag) => {
        setSelectedTag(tag);
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
    };

    // Function to handle tag removal (to be implemented)
    const handleRemoveTag = async (tagName: string) => {
        // Make HTTP request to remove the tag using the provided tagId
        let tag: Tag = await TagService.delete(tagName);
        console.log(tag);
        console.log(`Remove tag with ID: ${tagName}`);
    };

    // Function to handle scan button click (to be implemented)
    const handleScanTag = async (tagName: string) => {
        // Make HTTP request to toggle scan for the tag using the provided tagId
        let tag: Tag = await TagService.updateScan(tagName);
        console.log(tag);
        console.log(`Toggle scan for tag with ID: ${tagName}`);
    };


    const [showCreateModal, setShowCreateModal] = React.useState(false);


    const handleOpenCreateModal = () => {
        setShowCreateModal(true);
    };

    const handleCloseCreateModal = (tag: Tag) => {
        setShowCreateModal(false);
        console.log(tag)
        if (tag !== undefined) {
            tags.push(tag);
            setTags(tags)
        }
    };

    return (
        <div className={style.content}>
            <div className={style.titleLine}>
                <h3>All Tags</h3>
                <Button variant="primary" onClick={handleOpenCreateModal}>
                    Add new tag
                </Button>
            </div>
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
                                <Button variant="info" size="sm" onClick={() => handleOpenDetailsModal(tag)}>
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
                                    {((tag as AnalogInputTag).scan) ? <>
                                        <Button variant="dark" size="sm" onClick={() => handleScanTag(tag.name)}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="scan-tooltip">Turn off scan</Tooltip>}
                                            >
                                                <i className="bi bi-play-circle"></i>
                                            </OverlayTrigger>
                                        </Button>{' '}
                                    </> : <>
                                        <Button variant="primary" size="sm" onClick={() => handleScanTag(tag.name)}>
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
                                {(tag.type == "analog_input") && <Button variant="warning" size="sm" onClick={() => handleOpenDetailsModal(tag)}>
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
                    showModal={showDetailsModal}
                    handleCloseModal={handleCloseDetailsModal} />
            )}
            <TagCreateModal
                showModal={showCreateModal}
                handleCloseModal={handleCloseCreateModal} />
        </div >
    );
}

export default AllTagsPage;
