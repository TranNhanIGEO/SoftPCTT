import "./DistrictRole.css";
import { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import Form from "src/components/interfaces/Form";
import LayerRole from "./LayerRole";
import {
  userRoleLists,
  toggleDistrictRoles,
  setUserRole,
  userAccount,
  userRoles,
  changeSelectAllDistricts,
  unChangeSelectAllDistricts,
} from "src/stores/admin";

const DistrictRole = ({ districtRoles, _thisRoleID }) => {
  const dispatch = useDispatch();
  const userInfo = useSelector(userAccount);
  const userRole = useSelector(userRoles);
  const userRoleList = useSelector(userRoleLists);

  const handleSelectAllDistricts = useCallback(() => {
    const layers = (layers) => {
      return layers.map((lyr) => lyr.layerID);
    };

    const newRole = districtRoles?.map((dt) => {
      return {
        memberid: userInfo?.["memberid"],
        roleid: _thisRoleID,
        mahuyen: dt.districtID,
        malopdulieu: layers(dt.layers),
      };
    });

    const hasDistricts = userRoleList.filter(
      (r) => r["roleid"] === _thisRoleID
    );
    const isSelectAllDistricts = hasDistricts.length === districtRoles.length;
    !isSelectAllDistricts
      ? dispatch(changeSelectAllDistricts(newRole))
      : dispatch(unChangeSelectAllDistricts(newRole));
  }, [userInfo, userRoleList, _thisRoleID, districtRoles, dispatch]);

  const handleToggleDistrict = useCallback(
    (e) => {
      const { value } = e.target;
      const newRole = {
        ...userRole,
        memberid: userInfo?.["memberid"],
        roleid: _thisRoleID,
        mahuyen: value,
      };
      dispatch(toggleDistrictRoles(newRole));
      dispatch(setUserRole(newRole));
    },
    [userRole, userInfo, _thisRoleID, dispatch]
  );

  return (
    <div className="district-roles">
      <div className="district-roles-title">
        <span>Quận/Huyện</span>
        <Form.Check
          type="checkbox"
          onChange={handleSelectAllDistricts}
          checked={
            userRoleList
              ?.filter((r) => r["mahuyen"] !== null)
              ?.filter((r) => r["roleid"] === _thisRoleID).length ===
              districtRoles.length &&
            userRoleList
              ?.filter((r) => r["roleid"] === _thisRoleID)
              ?.reduce((a, r) => [...a, ...r["malopdulieu"]], [])?.length ===
              districtRoles?.reduce((a, r) => [...a, ...r.layers], [])?.length
          }
        />
      </div>
      <div className="district-roles-content">
        <ul className="district-roles-list">
          {districtRoles?.map((dt) => (
            <li key={dt.districtID}>
              <Form.Check
                type="checkbox"
                id={dt.districtID}
                label={dt.districtName}
                value={dt.districtID}
                onChange={handleToggleDistrict}
                checked={userRoleList
                  ?.filter((r) => r["mahuyen"] !== null)
                  ?.some((r) => r["mahuyen"] === dt.districtID)}
              />
              <LayerRole
                layerRoles={dt.layers}
                _thisDistrictID={dt.districtID}
              />
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default DistrictRole;
