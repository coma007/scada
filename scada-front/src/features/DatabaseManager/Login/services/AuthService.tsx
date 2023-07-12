import axios from 'axios';
import { LOGIN } from '../../../../api';
import { Credentials } from '../types/User';

export const AuthService = {
  login: async function (credentials: Credentials): Promise<string> {
    let url = LOGIN();
    let response = await axios.post(url, credentials);
    return response.data;
  }
}

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