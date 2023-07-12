const url = "http://localhost:7109/Api"

export const GET_TAGS = () => url + "/Tag/GetAll";
export const GET_TAG = () => url + "/DatabaseManager/GetTag";
export const DELETE_TAG = () => url + "/DatabaseManager/DeleteTag";
export const UPDATE_TAG_SCAN = () => url + "/DatabaseManager/UpdateTagScan";
export const UPDATE_TAG_OUTPUT_VALUE = () => url + "/DatabaseManager/UpdateTagOutputValue";
export const CREATE_TAG = () => url + "/DatabaseManager/CreateTag";
export const GET_LAST_TAG_VALUE = () => url + "/DatabaseManager/GetTagLastValue";

export const GET_ALARMS = () => url + "/Alarm/GetAll";
export const GET_ALARM = () => url + "/DatabaseManager/GetAlarm";
export const GET_ALARM_BY_TAG = () => url + "/DatabaseManager/GetAlarmByTagName";
export const CREATE_ALARM = () => url + "/DatabaseManager/CreateAlarm";
export const DELETE_ALARM = () => url + "/DatabaseManager/DeleteAlarm";

export const LOGIN = () => url + "/DatabaseManager/Login";

export const WEBSOCKET = () => "ws://localhost:7109/Api/Websocket";


export const ALARM_HISTORY_TIMESPAN = () => url + "/ReportManager/GetAlarmsBetween"
export const ALARM_HISTORY_PRIORITY = () => url + "/ReportManager/GetAlarmsByPriority"
export const TAG_HISTORY_TIMESPAN = () => url + "/ReportManager/GetTagValuesBetween"
export const TAG_HISTORY_TYPE = () => url + "/ReportManager/GetLastTagValues"
export const TAG_HISTORY = () => url + "/ReportManager/GetAllTagValuesByTagName"