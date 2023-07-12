import axios from 'axios';
import { CREATE_ALARM, DELETE_ALARM, GET_ALARMS, GET_ALARM_BY_TAG } from '../../../../api';
import { Alarm } from '../types/Alarm';

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

const AlarmsService = {

    getAll: async function (): Promise<Alarm[]> {
        return axios.get(GET_ALARMS())
            .then(response => {
                let data: Alarm[]= response.data.map((alarm: any) => {
                    new Alarm(alarm.type, alarm.alarmPriority, alarm.limit, alarm.alarmName, alarm.tagName);
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },

    getByTagName: async function (name: string): Promise<Alarm[]> {
        return axios.get(GET_ALARM_BY_TAG(), { params : {name : name}})
            .then(response => {
                let data: Alarm[]= response.data.map((alarm: any) => {
                    return new Alarm(alarm.type, alarm.alarmPriority, alarm.limit, alarm.alarmName, alarm.tagName);
                });
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },


    delete: async function (alarmName: string): Promise<Alarm> {
        return axios.delete(DELETE_ALARM(), { params : {alarmName : alarmName}})
            .then(response => {
                let data: Alarm = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    },

    create: async function (alarm: Alarm): Promise<Alarm> {
        return axios.post(CREATE_ALARM(), alarm)
            .then(response => {
                let data: Alarm = response.data;
                return data;
            })
            .catch(error => {
                console.log(error)
                throw new Error(error.response.data);
            });
    }
}

export default AlarmsService;