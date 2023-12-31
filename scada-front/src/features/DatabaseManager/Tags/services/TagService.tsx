import axios from 'axios';
import { CREATE_TAG, DELETE_TAG, GET_LAST_TAG_VALUE, GET_TAGS, UPDATE_TAG_OUTPUT_VALUE, UPDATE_TAG_SCAN } from '../../../../api';
import { AnalogInputTag, AnalogOutputTag, DigitalInputTag, DigitalOutputTag, Tag } from '../../../../types/Tag';

axios.interceptors.request.use(
    config => {
      const token = localStorage.getItem("token")
      if (token) {
        config.headers['Authorization'] = 'Bearer ' + token
      }
      return config
    },
    error => {
      Promise.reject(error)
    }
  )


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
                throw new Error(error.response.data);
            });
    },


    getTagLastValue: async function (tagName: string): Promise<string> {
        return axios.get(GET_LAST_TAG_VALUE(), {params: {tagName: tagName}})
            .then(response => {
                let data: string = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },


    delete: async function (tagName: string): Promise<Tag> {
        return axios.delete(DELETE_TAG(), {params : {tagName : tagName}})
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },

    updateScan: async function (tagName: string): Promise<Tag> {
        return axios.patch(UPDATE_TAG_SCAN(), null, {params : {tagName : tagName}})
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },

    updateOutputValue: async function (tagName: string, value: number): Promise<Tag> {
        return axios.patch(UPDATE_TAG_OUTPUT_VALUE(), null, {params : {tagName : tagName, value: value}})
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },

    create: async function (tag: Tag): Promise<Tag> {
        return axios.post(CREATE_TAG(), tag)
            .then(response => {
                let data: Tag = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    }
}

export default TagService;