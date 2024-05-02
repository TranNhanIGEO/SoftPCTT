import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { EdittingUser, AssignRole } from "src/pages/Admin/components";
import {
  getUserAccount,
  getUserRoles,
  initState,
  isAssignRole,
  toggleAssignRole,
} from "src/stores/admin";

const CreateUser = () => {
  const { userAccount, userRoleLists } = initState;
  const isPermission = useSelector(isAssignRole);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(toggleAssignRole(false));
    dispatch(getUserAccount(userAccount));
    dispatch(getUserRoles(userRoleLists));
  }, [userAccount, userRoleLists, dispatch]);

  return (
    <div className="edit-data">
      <div className="edit-data-container">
        <EdittingUser pageType="create" />
        {isPermission && <AssignRole />}
      </div>
    </div>
  );
};

export default CreateUser;
