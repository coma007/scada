import axios from "axios"
import { GET_TAG, LATEST_VALUES } from "../../../api"
import { TagHistoryRecord } from "../../../types/TagHistoryRecord"
import { AnalogInputTag } from "../../../types/Tag"

export const TrendingService = {
    getLatestInput: async function() : Promise<TagHistoryRecord[]> {
        return axios.get(LATEST_VALUES()).then(response => {
            let tags : TagHistoryRecord[] = response.data;
            return tags;
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    },

    getAnalogTag: async function(tagName : string) : Promise<AnalogInputTag> {
        return await axios.get(GET_TAG(), {
            params : {
                tagName: tagName
            }
        }).then(response => {
            console.log(response.data)
            let tag : AnalogInputTag = response.data;
            return tag
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    }
}