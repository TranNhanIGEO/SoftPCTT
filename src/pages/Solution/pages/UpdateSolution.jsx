import { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import config from "src/config";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useToken from "src/hooks/useToken";
import { currentLayer } from "src/stores/solution";
import {
  apiDeleteSolution,
  apiShowSolution,
  apiUpdateSolution,
} from "src/services/solution";
import { Button } from "src/components/interfaces/Button";
import Form from "src/components/interfaces/Form";

const UpdateSolution = () => {
  const axiosJWT = useAxiosJWT();
  const navigate = useNavigate();
  const toast = useToast();
  const currentUser = useToken();
  const memberID = currentUser?.["nameidentifier"];
  const { id } = useParams();
  const selectedLayer = useSelector(currentLayer);
  const configColumns = config.columns[selectedLayer];
  const configServices = config.services.solution[selectedLayer];
  const [solutionInfos, setSolutionInfos] = useState({});
  const [solutionFields] = useState(() => {
    const columns = configColumns.filter((col) => col.input);
    return columns;
  });

  useEffect(() => {
    const showOnlySolution = async () => {
      const thisAPI = configServices?.editApi;
      const response = await apiShowSolution({ thisAPI, id, axiosJWT });
      setSolutionInfos(response);
    };
    showOnlySolution();
  }, [configServices, id, axiosJWT]);

  const handleDeleteSolution = useCallback(() => {
    const thisAPI = configServices?.editApi;
    apiDeleteSolution({ thisAPI, id, memberID, axiosJWT, navigate, toast });
  }, [configServices, id, memberID, axiosJWT, navigate, toast]);

  const handleEnterForm = useCallback((e) => {
    const { name, value } = e.target;
    setSolutionInfos((prev) => ({ ...prev, [name]: value }));
  }, []);

  const handleUpdateSolution = useCallback(
    (e) => {
      e.preventDefault();
      const thisAPI = configServices?.editApi;
      const solution = { ...solutionInfos };
      const fields = Object.keys(solutionInfos);
      const infoSolution = fields.reduce((acc, val) => {
        solution[val] = solution[val]?.toString() || null;
        return { ...acc, [val]: solution[val] };
      }, {});
      apiUpdateSolution({
        thisAPI,
        id,
        infoSolution,
        memberID,
        axiosJWT,
        navigate,
        toast,
      });
    },
    [configServices, id, solutionInfos, memberID, axiosJWT, navigate, toast]
  );

  return (
    <div className="edit-data">
      <div className="edit-data-container">
        <div className="edit-data-form">
          <div className="edit-data-title">
            <span>CHỈNH SỬA PHƯƠNG ÁN - KẾ HOẠCH</span>
            <Button outline onClick={handleDeleteSolution}>
              Xóa
            </Button>
          </div>
          <form className="edit-data-content">
            <div className="edit-data-argument">
              <Form>
                {solutionFields.map((s) => (
                  <Form.Group key={s.accessorKey} controlID={s.accessorKey}>
                    <Form.Label>{s.header}</Form.Label>
                    <Form.Control
                      type={s.input.type}
                      id={s.accessorKey}
                      name={s.accessorKey}
                      value={solutionInfos[s.accessorKey] ?? ""}
                      onChange={handleEnterForm}
                    />
                  </Form.Group>
                ))}
              </Form>
            </div>
          </form>
          <div className="edit-data-action">
            <Button outline onClick={() => navigate("/solution")}>
              Thoát
            </Button>
            <Button isSubmit outline onClick={handleUpdateSolution}>
              Chỉnh sửa
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UpdateSolution;
