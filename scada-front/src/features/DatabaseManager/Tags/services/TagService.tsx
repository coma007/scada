import axios from 'axios';
import { Tag } from '../types/Tag';
import { GET_TAGS } from '../../../../api';

const TagService = {

    getAll: async function (): Promise<Tag[]> {
        return axios.get(GET_TAGS())
            .then(response => {
                let data: Tag[]= response.data.map((tag: any) => {
                    let a: Tag = {
                        description: tag.description,
                        ioAddress: tag.ioAddress,
                        name: tag.tagName,
                        type: tag.tagType,
                    }
                    return a;
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                return error;
            });
    }
}

export default TagService;