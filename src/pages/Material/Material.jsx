import "./Material.css";
import { ShowMaterial } from "./pages";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import useToken from "src/hooks/useToken";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { getViewLayers } from "src/utils/getViewLayers";
import { SearchProvider } from "src/contexts/Search";
import { apiRoleLists, apiShowUserRoles } from "src/services/global";
import { currentPage, getRoleLists, setPage } from "src/stores/global";
import {
  setAllMaterialDistricts,
  setAllMaterialLayers,
} from "src/stores/material";

const Material = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const selectedPage = useSelector(currentPage);
  const [roleLists] = useSelector(getRoleLists);
  const currentUser = useToken();
  const memberid = currentUser?.["nameidentifier"];

  useEffect(() => {
    const page = process.env.REACT_APP_MATERIALPAGE;
    apiRoleLists({ page, axiosJWT, dispatch });
    apiShowUserRoles({ memberid, axiosJWT, dispatch });
  }, [memberid, axiosJWT, dispatch]);

  useEffect(() => {
    if (selectedPage) return;
    dispatch(setPage(process.env.REACT_APP_MATERIALPAGE));
  }, [selectedPage, dispatch]);

  useEffect(() => {
    if (!roleLists?.length) return;
    const materialArgs = getViewLayers({
      pageType: process.env.REACT_APP_MATERIALPAGE,
      roleLists: roleLists,
    });
    const materialDistricts = materialArgs?.districts;
    const materialLayers = materialArgs?.layers;
    dispatch(setAllMaterialDistricts(materialDistricts));
    dispatch(setAllMaterialLayers(materialLayers));
  }, [roleLists, dispatch]);

  return (
    <div className="material">
      <SearchProvider>
        <ShowMaterial />
      </SearchProvider>
    </div>
  );
};

export default Material;
