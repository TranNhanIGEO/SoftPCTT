import { useCallback, useState } from "react";
import { useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { UploadExcel } from "src/components/UploadExcel";
import config from "src/config";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useToken from "src/hooks/useToken";
import { apiCreateData } from "src/services/home";
import { getDataMaxID } from "src/stores/home";

const UsingExel = () => {
  const axiosJWT = useAxiosJWT();
  const navigate = useNavigate();
  const currentUser = useToken();
  const memberID = currentUser?.["nameidentifier"];
  const toast = useToast();
  const { state } = useLocation();
  const { layer } = state;
  const configServices = config.services.home[layer];
  const maxObjectID = useSelector(getDataMaxID);
  const [data, setData] = useState([]);

  const handleSubmit = useCallback(
    (newData) => {
      const thisAPI = configServices?.editApi;
      const thisForm = configServices?.table?.form;
      if (!thisAPI) return;
      const infoData = newData.map((dt, idx) => ({
        ...dt,
        objectid: maxObjectID + idx + 1,
      }));
      apiCreateData({
        thisAPI,
        thisForm,
        infoData,
        memberID,
        axiosJWT,
        navigate,
        toast,
      });
    },
    [configServices, maxObjectID, memberID, axiosJWT, navigate, toast]
  );

  return (
    <div className="edit-data">
      <div className="edit-data-excel">
        <UploadExcel data={data} onView={setData} onSubmit={handleSubmit} />
      </div>
    </div>
  );
};

export default UsingExel;
