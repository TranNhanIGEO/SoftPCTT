import "./EdittingMaterial.css";
import { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import config from "src/config";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import useToken from "src/hooks/useToken";
import { currentLayer } from "src/stores/material";
import { apiDeleteMaterial } from "src/services/material";
import { Button } from "src/components/interfaces/Button";
import { edittingMaterialObj } from "./edittingMaterialObj";
import Form from "src/components/interfaces/Form";
import PreviewFile from "src/components/PreviewFile";

const EdittingMaterial = ({ pageType }) => {
  const edittingPage = edittingMaterialObj[pageType];
  const axiosJWT = useAxiosJWT();
  const navigate = useNavigate();
  const toast = useToast();
  const currentUser = useToken();
  const memberID = currentUser?.["nameidentifier"];
  const { id } = useParams();
  const selectedLayer = useSelector(currentLayer);
  const configColumns = config.columns[selectedLayer];
  const configServices = config.services.material[selectedLayer];
  const [materialFile, setMaterialFile] = useState(null);
  const [materialInfo, setMaterialInfo] = useState({});
  const [materialField] = useState(() => {
    const columns = configColumns.filter((col) => col.input);
    return columns;
  });

  useEffect(() => {
    const showOnlyData = async () => {
      const thisAPI = configServices?.editApi;
      const onGetData = edittingPage.data.onGet;
      const getData = await onGetData({ materialField, thisAPI, id, axiosJWT });
      setMaterialInfo(getData);
    };
    showOnlyData();
  }, [configServices, edittingPage, materialField, id, axiosJWT]);

  const handleDeleteMaterial = useCallback(() => {
    const thisAPI = configServices?.editApi;
    apiDeleteMaterial({ thisAPI, id, memberID, axiosJWT, navigate, toast });
  }, [configServices, id, memberID, axiosJWT, navigate, toast]);

  const handleClearFile = useCallback(() => {
    const nameColumn = configServices?.file?.name;
    setMaterialFile(null);
    setMaterialInfo((prev) => ({ ...prev, file: "", [nameColumn]: "" }));
  }, [configServices]);

  const handleEnterForm = useCallback(
    (e) => {
      const { name, value, accept, files } = e.target;
      const specialFields = ["tentulieu", "tenhinhanh", "tenvideo"];
      if (specialFields.includes(name)) {
        const file = files?.[0];
        if (!file) {
          setMaterialFile(null);
          setMaterialInfo((prev) => ({ ...prev, file: "", [name]: "" }));
          return;
        }
        const type = file?.type;
        if (!accept.includes(type)) {
          toast.error({
            title: "Cảnh báo",
            message: `Vui lòng chọn tệp có định dạng ${accept}`,
          });
          return;
        }
        setMaterialFile({ type: type, source: URL.createObjectURL(file) });
        setMaterialInfo((prev) => ({ ...prev, file: file, [name]: value }));
        return;
      }
      setMaterialInfo((prev) => ({ ...prev, [name]: value }));
    },
    [toast]
  );

  const handleEdittingMaterial = useCallback(
    (e) => {
      e.preventDefault();
      const thisAPI = configServices?.editApi;
      const onSubmit = edittingPage.submit.onSubmit;
      onSubmit({
        thisAPI,
        id,
        materialInfo,
        memberID,
        axiosJWT,
        navigate,
        toast,
      });
    },
    [
      configServices,
      edittingPage,
      id,
      materialInfo,
      memberID,
      axiosJWT,
      navigate,
      toast,
    ]
  );

  return (
    <div className="edit-data">
      <div className="edit-data-container">
        <div className="edit-data-form">
          <div className="edit-data-title">
            <span>{edittingPage.title}</span>
            {pageType === "update" && (
              <Button outline onClick={handleDeleteMaterial}>
                Xóa
              </Button>
            )}
          </div>
          <form className="edit-data-content">
            <div className="edit-data-argument">
              <Form>
                {materialField.map((m) => (
                  <Form.Group key={m.accessorKey} controlID={m.accessorKey}>
                    <Form.Label>{m.header}</Form.Label>
                    <Form.Control
                      type={m.input.type}
                      id={m.accessorKey}
                      name={m.accessorKey}
                      accept={m.input.format}
                      value={materialInfo[m.accessorKey] ?? ""}
                      onChange={handleEnterForm}
                    />
                  </Form.Group>
                ))}
              </Form>
              <PreviewFile file={materialFile} onClear={handleClearFile} />
            </div>
          </form>
          <div className="edit-data-action">
            <Button outline onClick={() => navigate("/material")}>
              Thoát
            </Button>
            <Button isSubmit outline onClick={handleEdittingMaterial}>
              {edittingPage.submit.text}
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EdittingMaterial;
