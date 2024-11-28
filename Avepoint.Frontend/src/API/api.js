import axios from "axios";

const API_BASE = import.meta.env.VITE_API_URL;

export const getCafes = async () => axios.get(`${API_BASE}/cafe`);
export const getEmployees = async () => axios.get(`${API_BASE}/employee`);
export const addOrUpdateCafe = async (data, id) =>
  id
    ? axios.put(`${API_BASE}/cafe/${id}`, data)
    : axios.post(`${API_BASE}/cafe`, data);
export const addOrUpdateEmployee = async (data, id) =>
  id
    ? axios.put(`${API_BASE}/employee/${id}`, data)
    : axios.post(`${API_BASE}/employee`, data);
export const deleteEntity = async (type, id) =>
  axios.delete(`${API_BASE}/${type}/${id}`);
