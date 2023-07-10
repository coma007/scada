import React from 'react';
import { Table, Button, Modal } from 'react-bootstrap';
import style from './AllTagsPage.module.css';
import { stringLiteral } from '@babel/types';

const AllTagsPage: React.FC = () => {
    // Replace this with  actual data
    const tags = [
        {
            name: 'Tag 1',
            description: 'Description 1',
            ioAddress: 'IO Address 1',
            type: 'analog_input',
        },
        {
            name: 'Tag 2',
            description: 'Description 2',
            ioAddress: 'IO Address 2',
            type: 'digital_input',
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
                                <Button variant="primary" size="sm" onClick={() => handleScanButtonClick(tag.name)}>
                                    <i className="bi bi-play-circle"></i>
                                </Button>{' '}
                                <Button variant="info" size="sm" onClick={() => handleOpenModal(tag.name)}>
                                    <i className="bi bi-info-circle"></i>
                                </Button>{' '}
                                <Button variant="danger" size="sm" onClick={() => handleRemoveTag(tag.name)}>
                                    <i className="bi bi-trash"></i>
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            {
                selectedTag && (
                    <Modal show={showModal} onHide={handleCloseModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>Tag Details</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <p>Name: {selectedTag.name}</p>
                            <p>Description: {selectedTag.description}</p>
                            <p>I/O Address: {selectedTag.ioAddress}</p>
                            <p>Type: {selectedTag.type}</p>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={handleCloseModal}>
                                Close
                            </Button>
                        </Modal.Footer>
                    </Modal>
                )
            }
        </div >
    );
};

export default AllTagsPage;
