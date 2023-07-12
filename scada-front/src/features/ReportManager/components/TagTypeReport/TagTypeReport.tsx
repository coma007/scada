import { useEffect, useState } from 'react';
import AlarmRecordsList from '../../../../components/AlarmRecordsList/AlarmRecordsList';
import { AlarmHistoryRecord } from '../../../../types/AlarmHistoryRecord';
import { Button } from 'react-bootstrap';
import TagRecordsList from '../../../../components/TagRecordsList/TagRecordsList';
import React from 'react';
import { Tag, AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag } from '../../../../types/Tag';
import TagList from '../../../../components/TagList/TagList';
import TagDetailsModal from '../../../../components/TagDetailsModal/TagDetailsModal';

const TagTypeReport = () => {
    let dummyTags: Tag[] = [
        new AnalogInputTag('AnalogInput1', 'analog_input', 'Analog Input Tag 1', 1001, 1000, true, 0, 100, "SIMULATION", 'V'),
        new AnalogOutputTag('AnalogOutput1', 'analog_output', 'Analog Output Tag 1', 2001, 50, 0, 100, 'mA'),
        new DigitalInputTag('DigitalInput1', 'digital_input', 'Digital Input Tag 1', 3001, 500, true, "SIMULATION"),
        new DigitalOutputTag('DigitalOutput1', 'digital_output', 'Digital Output Tag 1', 4001, 1),
        new AnalogInputTag('AnalogInput2', 'analog_input', 'Analog Input Tag 2', 1002, 1000, true, 0, 100, "SIMULATION", 'V'),
    ];
    const [tags, setTags] = React.useState<Tag[]>([]);

    React.useEffect(() => {
        setTags(dummyTags);
    }, [])

    const [selectedType, setSelectedType] = useState<string | undefined>();

    const handlePriorityChange = (event: any) => {

        setSelectedType(event.target.value);
    };

    const handleSubmit = () => {
        if (selectedType !== undefined) {
            // API request here ... 

            // setTags(...)
        }
    }

    const [showDetailsModal, setShowDetailsModal] = React.useState(false);
    const [selectedTag, setSelectedTag] = React.useState<Tag | undefined>();

    const handleOpenDetailsModal = (tag: Tag) => {
        console.log(tag)
        setSelectedTag(tag);
        setShowDetailsModal(true);
    };

    const handleCloseDetailsModal = () => {
        setShowDetailsModal(false);
    };

    return (
        <div>

            <div className="row align-items-center margin-bottom">
                <div className="col-md-3">
                    <select className="form-select" value={selectedType} onChange={handlePriorityChange}>
                        <option value="">Choose Tag Type</option>
                        <option value="analog_input">Analog Input</option>
                        <option value="digital_input">Digital Input</option>
                    </select>
                </div>


                <div className='col-md-8'></div>

                <div className="col-md-1">
                    <Button variant="primary" onClick={handleSubmit}>
                        Filter
                    </Button>
                </div>
            </div>

            <TagList tags={tags} viewOnly={true} handleOpenDetailsModal={handleOpenDetailsModal} />

            {
                selectedTag && (
                    <TagDetailsModal
                        selectedTag={selectedTag}
                        showModal={showDetailsModal}
                        handleCloseModal={handleCloseDetailsModal} />
                )
            }
        </div>


    );
};

export default TagTypeReport;
