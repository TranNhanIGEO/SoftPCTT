import "./Admin.css";
import { useEffect } from "react";
import { ShowAccounts } from "./pages";
import { useDispatch } from "react-redux";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { apiShowAllMembers } from "src/services/admin";
import { apiRoleLists } from "src/services/global";

const Admin = () => {
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();

  useEffect(() => {
    apiShowAllMembers({ axiosJWT, dispatch });
  }, [axiosJWT, dispatch]);

  useEffect(() => {
    const page = process.env.REACT_APP_ADMINPAGE;
    apiRoleLists({ page, axiosJWT, dispatch });
  }, [axiosJWT, dispatch]);

  return (
    <div className="admin">
      <ShowAccounts />
    </div>
  );
};

export default Admin;
