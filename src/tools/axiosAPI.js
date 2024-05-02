import axios from "axios";

const axiosMAP = axios.create({
  baseURL: `${process.env.REACT_APP_MAPBOX_HOST}`,
});

const axiosAPI = axios.create({
  baseURL: `${process.env.REACT_APP_DOMAIN}`,
});

const axiosJWT = axios.create({
  baseURL: `${process.env.REACT_APP_DOMAIN}`,
});

export { axiosMAP, axiosAPI, axiosJWT };
