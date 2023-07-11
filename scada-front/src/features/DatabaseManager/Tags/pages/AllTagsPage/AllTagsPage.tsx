import React from 'react';
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import TagDetailsModal from '../../components/TagDetailsModal/TagDetailsModal';
import TagCreateModal from '../../components/TagCreateModal/TagCreateModal';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../../types/Tag';
import TagService from '../../services/TagService';
import AllAlarmsOfTagModal from '../../../Alarms/components/AllAlarmsOfTagModal/AllAlarmsOfTagModal';
import style from './AllTagsPage.module.css';

const AllTagsPage: React.FC = () => {
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
    const [showAlarmsModal, setShowAlarmsModal] = React.useState(false);
    const [selectedTag, setSelectedTag] = React.useState<Tag|undefined>();

    const handleOpenAlarmsModal = (tag: Tag) => {
        setSelectedTag(tag);
        setShowAlarmsModal(true);
    };

    const handleCloseAlarmsModal = () => {
        setShowAlarmsModal(false);
    };

    const handleOpenDetailsModal = (tag: Tag) => {
        setSelectedTag(tag);
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
    };

    const handleRemoveTag = async (tagName: string) => {
        try{
            let tag: Tag = await TagService.delete(tagName);
            console.log(tag);
            console.log(`Remove tag with ID: ${tagName}`);
            let updatedList = tags.filter(tag => tag.tagName !== tagName);
            setTags(updatedList);
        } catch (error) {
          console.error('Error fetching data:', error);
        }

    };

    const handleScanTag = async (tagName: string) => {
        try{
            let tag: Tag = await TagService.updateScan(tagName);
            console.log(tag);
            console.log(`Toggle scan for tag with ID: ${tagName}`);
            let index = tags.findIndex(tag => tag.tagName === tagName);

            let updatedTags = [...tags];
            updatedTags[index] = tag;
            setTags(updatedTags);
        } catch (error) {
          console.error('Error fetching data:', error);
        }
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
                        <tr key={tag.tagName}>
                            <td >{tag.tagName}</td>
                            <td >{tag.description}</td>
                            <td >{tag.ioAddress}</td>
                            <td >{tag.tagType}</td>
                            <td >
                                <Button variant="info" size="sm" onClick={() => handleOpenDetailsModal(tag)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="info-tooltip">Tag details</Tooltip>}
                                    >
                                        <i className="bi bi-info-circle"></i>
                                    </OverlayTrigger>
                                </Button>{' '}
                                <Button variant="danger" size="sm" onClick={() => handleRemoveTag(tag.tagName)}>
                                    <OverlayTrigger
                                        placement="bottom"
                                        overlay={<Tooltip id="remove-tooltip">Remove tag</Tooltip>}
                                    >
                                        <i className="bi bi-trash"></i>
                                    </OverlayTrigger>
                                </Button>{' '}
                                {(tag.tagType == "analog_input" || tag.tagType == "digital_input") && <>
                                    {((tag as AnalogInputTag).scan) ? <>
                                        <Button variant="dark" size="sm" onClick={() => handleScanTag(tag.tagName)}>
                                            <OverlayTrigger
                                                placement="bottom"
                                                overlay={<Tooltip id="scan-tooltip">Turn off scan</Tooltip>}
                                            >
                                                <i className="bi bi-play-circle"></i>
                                            </OverlayTrigger>
                                        </Button>{' '}
                                    </> : <>
                                        <Button variant="primary" size="sm" onClick={() => handleScanTag(tag.tagName)}>
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
                                {(tag.type == "analog_input") && <Button variant="warning" size="sm" onClick={() => handleOpenAlarmsModal(tag)}>
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
            {selectedTag && (
                <AllAlarmsOfTagModal
                    selectedTag={selectedTag}
                    showModal={showAlarmsModal}
                    handleCloseModal={handleCloseAlarmsModal} />
            )}
            <TagCreateModal
                showModal={showCreateModal}
                handleCloseModal={handleCloseCreateModal} />
        </div >
    );
}

export default AllTagsPage;
