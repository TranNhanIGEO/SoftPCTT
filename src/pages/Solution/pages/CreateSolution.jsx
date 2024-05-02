import { useCallback, useState } from "react";
import { useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { UploadExcel } from "src/components/UploadExcel";
import config from "src/config";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useToken from "src/hooks/useToken";
import { apiCreateSolution } from "src/services/solution";
import { getSolutionMaxID } from "src/stores/solution";

const CreateSolution = () => {
  const axiosJWT = useAxiosJWT();
  const navigate = useNavigate();
  const currentUser = useToken();
  const memberID = currentUser?.["nameidentifier"];
  const toast = useToast();
  const { state } = useLocation();
  const { layer } = state;
  const configServices = config.services.solution[layer];
  const maxObjectID = useSelector(getSolutionMaxID);
  const [data, setData] = useState([]);

  const handleSubmit = useCallback(
    (newData) => {
      const thisAPI = configServices?.editApi;
      if (!thisAPI) return;
      const addedSolutions = newData.map((solution, idx) => ({
        ...solution,
        objectid: maxObjectID + idx + 1,
      }));
      apiCreateSolution({
        thisAPI,
        addedSolutions,
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

export default CreateSolution;