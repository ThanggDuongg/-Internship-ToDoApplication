import axios from 'axios';
import queryString from 'query-string';
import userApi from './userApi';
import jwt from 'jwt-decode';

// Set up default config for http requests here
// Please have a look at here `https://github.com/axios/axios#request-config` for the full list of configs

const axiosClient = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  headers: {
    'content-type': 'application/json',
  },

  //use queryString handle parameter
  paramsSerializer: (params) => queryString.stringify(params),
});

axiosClient.interceptors.request.use(async (config: any) => {
  // Handle token here ...
  const auth = JSON.parse(localStorage.getItem('auth') || '{}');
  if (Object.keys(auth).length !== 0) {
    const accessToken = auth.accessToken;
    config.headers['Authorization'] = `Bearer ${accessToken}`;
  }

  return config;
});

axiosClient.interceptors.response.use(
  (response) => {
    // if (response && response.data) {
    //     return response.data;
    // }

    return response;
  },
  async (error: any) => {
    // Handle errors
    const config = error.config;
    if (error.response && error.response.status === 401 && !config._retry) {
      try {
        const auth = JSON.parse(localStorage.getItem('auth') || '{}');
        if (Object.keys(auth).length !== 0) {
          const refreshToken = auth.refreshToken;
          const datenow = new Date();
          const decodedToken = jwt(refreshToken);

          const response = await userApi.refreshToken({
            refreshToken: refreshToken,
          });
          console.log('>>> axiosClient: ', response);
          if (response.status === 401) {
            localStorage.removeItem('user');
            localStorage.removeItem('auth');
            window.location.reload();
          }
          config._retry = true;
          if (response.data.success) {
            localStorage.setItem('auth', JSON.stringify(response.data.data));
            const user = jwt(response.data.data.accessToken);
            localStorage.setItem('user', JSON.stringify(user));
          }
        }
      } catch (error) {
        localStorage.removeItem('user');
        localStorage.removeItem('auth');
        window.location.reload();
        return Promise.reject(error);
      }
    }
    return Promise.reject(error);
  },
);

export default axiosClient;
