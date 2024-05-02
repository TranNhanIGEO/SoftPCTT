import { useEffect } from "react";
import { useSelector } from "react-redux";
import useRefresh from "./useRefresh";
import { axiosJWT } from "src/tools/axiosAPI";
import { currentToken } from "src/stores/auth";

const useAxiosJWT = () => {
  const accessToken = useSelector(currentToken);
  const refreshToken = useRefresh();

  useEffect(() => {
    const onSuccessRequest = (config) => {
      const token = config.headers.authorization;
      if (!token) config.headers["Authorization"] = `Bearer ${accessToken}`;
      return config;
    };
    const onErrorRequest = (error) => {
      return Promise.reject(error);
    };

    const onSuccessResponse = (response) => {
      return response;
    };
    const onErrorResponse = async (error) => {
      const prevReq = error.config;
      if (error.response?.status === 401 && !prevReq.sent) {
        prevReq.sent = true;
        await refreshToken();
        prevReq.headers["Authorization"] = `Bearer ${accessToken}`;
        return axiosJWT(prevReq);
      }
      return Promise.reject(error);
    };

    const requestInterceptor = axiosJWT.interceptors.request.use(
      onSuccessRequest,
      onErrorRequest
    );
    const responseInterceptor = axiosJWT.interceptors.response.use(
      onSuccessResponse,
      onErrorResponse
    );

    return () => {
      axiosJWT.interceptors.request.eject(requestInterceptor);
      axiosJWT.interceptors.response.eject(responseInterceptor);
    };
  }, [accessToken, refreshToken]);

  return axiosJWT;
};

export default useAxiosJWT;
