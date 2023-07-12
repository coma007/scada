import axios from "axios"
import { LATEST_VALUES } from "../../../api"
import { TagHistoryRecord } from "../../../types/TagHistoryRecord"

export const TrendingService = {
    getLatestInput: async function() : Promise<TagHistoryRecord[]> {
        return axios.get(LATEST_VALUES()).then(response => {
            let tags : TagHistoryRecord[] = response.data;
            return tags;
        }).catch(error => {
            console.log(error)
            throw new Error(error.response.data);
        })
    }
}