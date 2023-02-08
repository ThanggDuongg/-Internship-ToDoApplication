import axiosClient from './axiosClient';

const path = '/user';

const userApi = {
  login: (params: any) => {
    const url = path + '/login';
    return axiosClient.post(url, params);
  },
  register: (params: any) => {
    const url = path + '/register';
    return axiosClient.post(url, params);
  },
  refreshToken: (params: any) => {
    const url = path + '/refreshtoken';
    return axiosClient.post(url, params);
  },
};

export default userApi;
