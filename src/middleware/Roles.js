import { useSelector } from "react-redux";
import useToken from "src/hooks/useToken";
import { getRoleLists, getUserRoleLists } from "src/stores/global";
import { getLayerRoles } from "src/utils/getLayerRoles";

export const EditRole = (props) => {
  const _thisRole = process.env.REACT_APP_EDITROLE;
  return <CheckRole _thisRole={_thisRole} {...props} />;
};

export const ReportRole = (props) => {
  const _thisRole = process.env.REACT_APP_REPORTROLE;
  return <CheckRole _thisRole={_thisRole} {...props} />;
};

const CheckRole = ({ children, _thisDistrict, _thisLayer, _thisRole }) => {
  const currentUser = useToken();
  const [roleLists] = useSelector(getRoleLists);
  const [userRoleLists] = useSelector(getUserRoleLists);

  const hasLayerReportRole = getLayerRoles({
    currentUser,
    roleLists,
    userRoleLists,
    _thisDistrict,
    _thisLayer,
    _thisRole,
  });
  if (!hasLayerReportRole) return;
  return children;
};
