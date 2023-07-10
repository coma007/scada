import axios from 'axios';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../types/Tag';
import { DELETE_TAG, GET_TAGS } from '../../../../api';

const TagService = {

    getAll: async function (): Promise<Tag[]> {
        return axios.get(GET_TAGS())
            .then(response => {
                let data: Tag[]= response.data.map((tag: any) => {

                    switch(tag.tagType){
                        case "digital_input":{
                            return new DigitalInputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.scanTime, tag.scan);
                        }
                        case "analog_input":{
                            return new AnalogInputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.scanTime, tag.scan, tag.lowLimit, tag.highLimit, tag.units);                         
                        }
                        case "digital_output":{
                            return new DigitalOutputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.initialVAlue)
                        }
                        case "analog_output":{
                            return new AnalogOutputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.initialValue, tag.lowLimit, tag.highLimit, tag.units)
                        }
                        default:{
                            return null;
                        }
                    }
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                return error;
            });
    },


    // TODO fetch  token
    delete: async function (tagName: string): Promise<Tag> {
        let token = "";
        return axios.delete(DELETE_TAG(), {headers: {Authorization: "Bearer " + token}, params : {tagName : tagName}})
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                return error;
            });
    }
}

export default TagService;