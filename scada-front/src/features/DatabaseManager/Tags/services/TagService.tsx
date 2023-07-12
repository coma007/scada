import axios from 'axios';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../../../../types/Tag';
import { CREATE_TAG, DELETE_TAG, GET_TAGS, UPDATE_TAG_SCAN } from '../../../../api';

const TagService = {

    getAll: async function (): Promise<Tag[]> {
        return axios.get(GET_TAGS())
            .then(response => {
                let data: Tag[] = response.data.map((tag: any) => {

                    switch (tag.tagType) {
                        case "digital_input": {
                            return new DigitalInputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.scanTime, tag.scan, tag.driver);
                        }
                        case "analog_input": {
                            return new AnalogInputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.scanTime, tag.scan, tag.lowLimit, tag.highLimit, tag.units, tag.driver);
                        }
                        case "digital_output": {
                            return new DigitalOutputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.initialVAlue)
                        }
                        case "analog_output": {
                            return new AnalogOutputTag(tag.tagName, tag.tagType, tag.description, tag.ioAddress, tag.initialValue, tag.lowLimit, tag.highLimit, tag.units)
                        }
                        default: {
                            return null;
                        }
                    }
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    },


    // TODO fetch  token
    delete: async function (tagName: string): Promise<Tag> {
        let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
        return axios.delete(DELETE_TAG(), { headers: { Authorization: "Bearer " + token }, params: { tagName: tagName } })
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    },

    // TODO fetch  token
    updateScan: async function (tagName: string): Promise<Tag> {
        let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
        return axios.patch(UPDATE_TAG_SCAN(), null, { headers: { Authorization: "Bearer " + token }, params: { tagName: tagName } })
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    },

    // TODO fetch  token
    create: async function (tag: Tag): Promise<Tag> {
        let token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYm9iaSIsImV4cCI6MTY4OTEwNjE4MH0.YS3Oyo6twPqXSwlAFG8eDhFCKAZUliyD8ORi6XlqEvN3rwDlI_6Xjv4tEjDurAY3RZl1S0Qbc4d5PN1nH1dBBQ";
        return axios.post(CREATE_TAG(), tag, { headers: { Authorization: "Bearer " + token } })
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error);
            });
    }
}

export default TagService;