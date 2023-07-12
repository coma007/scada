import React, { useEffect } from 'react';
import { Table, Button, OverlayTrigger, Tooltip } from 'react-bootstrap';
import TagDetailsModal from '../../../../../components/TagDetailsModal/TagDetailsModal';
import TagCreateModal from '../../components/TagCreateModal/TagCreateModal';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../../../../../types/Tag';
import TagService from '../../services/TagService';
import AllAlarmsOfTagModal from '../../../Alarms/components/AllAlarmsOfTagModal/AllAlarmsOfTagModal';
import TagList from '../../../../../components/TagList/TagList';

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
    const [selectedTag, setSelectedTag] = React.useState<Tag | undefined>();

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
        try {
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
        try {
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
        <div className="content">
            <div className="titleLine">
                <h3>Database Manager</h3>
                <Button variant="primary" onClick={handleOpenCreateModal}>
                    Add new tag
                </Button>
            </div>

            <TagList
                tags={tags}
                viewOnly={true}
                handleOpenAlarmsModal={handleOpenAlarmsModal}
                handleOpenDetailsModal={handleOpenDetailsModal}
                handleRemoveTag={handleRemoveTag}
                handleScanTag={handleScanTag} />

            <TagCreateModal
                showModal={showCreateModal}
                handleCloseModal={handleCloseCreateModal} />

            {
                selectedTag && (
                    <TagDetailsModal
                        selectedTag={selectedTag}
                        showModal={showDetailsModal}
                        handleCloseModal={handleCloseDetailsModal} />
                )
            }
            {
                selectedTag && (
                    <AllAlarmsOfTagModal
                        selectedTag={selectedTag}
                        showModal={showAlarmsModal}
                        handleCloseModal={handleCloseAlarmsModal} />
                )
            }
        </div >
    );
}

export default AllTagsPage;

