const url = "http://localhost:7109/Api"

export const GET_TAGS = () => url + "/Tag/GetAll";
export const DELETE_TAG = () => url + "/DatabaseManager/DeleteTag";
export const UPDATE_TAG_SCAN = () => url + "/DatabaseManager/UpdateTagScan";
export const CREATE_TAG = () => url + "/DatabaseManager/CreateTag";

export const LOGIN = () => url + "/DatabaseManager/Login";