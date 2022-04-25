import axios from "axios";

const Api = axios.create({
  baseURL: "https://localhost:44357/api/",
});

Api.interceptors.request.use(
  (config) => {
    let token = localStorage.getItem("UserToken");

    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
      config.headers["Content-Type"] = "multipart/form-data";
    }
    return config;
  },

  (error) => {
    return Promise.reject(error);
  }
);

export default Api;
