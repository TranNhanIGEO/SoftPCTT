import "./Solution.css";
import { Fragment, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import useToken from "src/hooks/useToken";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { tabTables } from "./pages/tabTables";
import { getViewLayers } from "src/utils/getViewLayers";
import { SearchProvider } from "src/contexts/Search";
import { apiRoleLists, apiShowUserRoles } from "src/services/global";
import { currentPage, getRoleLists, setPage } from "src/stores/global";
import {
  currentTabTable,
  setViewRoleLists,
  setTabTable,
} from "src/stores/solution";

const Solution = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const currentTab = useSelector(currentTabTable);
  const selectedPage = useSelector(currentPage);
  const [roleLists] = useSelector(getRoleLists);
  const currentUser = useToken();
  const memberid = currentUser?.["nameidentifier"];

  useEffect(() => {
    const page = process.env.REACT_APP_SOLUTIONPAGE;
    apiRoleLists({ page, axiosJWT, dispatch });
    apiShowUserRoles({ memberid, axiosJWT, dispatch });
  }, [memberid, axiosJWT, dispatch]);

  useEffect(() => {
    if (currentTab) return;
    dispatch(setTabTable("solutionTab"));
  }, [currentTab, dispatch]);

  useEffect(() => {
    if (selectedPage) return;
    dispatch(setPage(process.env.REACT_APP_SOLUTIONPAGE));
  }, [selectedPage, dispatch]);

  useEffect(() => {
    if (!roleLists?.length) return;
    const solutionArgs = getViewLayers({
      pageType: process.env.REACT_APP_SOLUTIONPAGE,
      roleLists: roleLists,
    });
    const viewRoleLists = solutionArgs?.districts;
    dispatch(setViewRoleLists(viewRoleLists));
  }, [roleLists, dispatch]);

  return (
    <div className="solution">
      <SearchProvider>
        {tabTables
          .filter((tab) => tab.value === currentTab)
          .map((tab) => (
            <Fragment key={tab.value}>{tab.content}</Fragment>
          ))}
      </SearchProvider>
    </div>
  );
};

export default Solution;
