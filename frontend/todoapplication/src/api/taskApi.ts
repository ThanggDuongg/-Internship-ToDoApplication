import axiosClient from './axiosClient';

const path = '/task';

const taskApi = {
  create: (params: any) => {
    const url = path;
    return axiosClient.post(url, params);
  },
  update: (params: any) => {
    const url = path;
    return axiosClient.put(url, params);
  },
  delete: (params: any) => {
    const url = path;
    return axiosClient.delete(url, { params });
  },
  getAll: (params: any) => {
    const url = path;
    return axiosClient.get(url, { params });
  },
  getAllByUserId: (params: any) => {
    const url = path + '/getallbyuseridandstatustask';
    return axiosClient.get(url, { params });
  },
};

export default taskApi;
