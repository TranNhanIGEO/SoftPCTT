import "./CreateData.css";
import { MapProvider } from "react-map-gl";
import { UsingExcel, UsingForm } from "./components";
import config from "src/config";
import { useLocation, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { apiMaxDataID } from "src/services/home";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { useDispatch } from "react-redux";

const createType = {
  multi: UsingExcel,
  only: UsingForm,
};

const CreateData = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { state } = useLocation();
  const { layer } = state;
  const configServices = config.services.home[layer];
  const CreateType = createType[configServices?.table?.element];

  useEffect(() => {
    if (!layer) return navigate("/");
    const thisAPI = configServices?.editApi;
    apiMaxDataID({ thisAPI, axiosJWT, dispatch });
  }, [layer, configServices, axiosJWT, dispatch, navigate]);

  return (
    <MapProvider>
      <CreateType />
    </MapProvider>
  );
};

export default CreateData;
