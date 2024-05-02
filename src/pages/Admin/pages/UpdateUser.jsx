import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { EdittingUser, AssignRole } from "src/pages/Admin/components";
import { isAssignRole, toggleAssignRole } from "src/stores/admin";
import { apiShowMemberAccount, apiShowMemberRoles } from "src/services/admin";

const UpdateUser = () => {
  const isPermission = useSelector(isAssignRole);
  const axiosJWT = useAxiosJWT();
  const dispatch = useDispatch();
  const { id } = useParams();

  useEffect(() => {
    dispatch(toggleAssignRole(true));
    apiShowMemberAccount({ id, axiosJWT, dispatch });
    apiShowMemberRoles({ id, axiosJWT, dispatch });
  }, [id, axiosJWT, dispatch]);

  return (
    <div className="edit-data">
      <div className="edit-data-container">
        <EdittingUser pageType="update" />
        {isPermission && <AssignRole />}
      </div>
    </div>
  );
};

export default UpdateUser;
