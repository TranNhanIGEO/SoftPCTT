import "./AssignRole.css";
import { useCallback, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { TabPanel, Tab } from "src/components/interfaces/Tab";
import Form from "src/components/interfaces/Form";
import { DistrictRole } from "./modules";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import { getRoleLists } from "src/stores/global";
import {
  allRoles,
  setAllRoles,
  toggleAssignRole,
  userAccount,
  toggleSelectAdminRole,
  userRoleLists,
} from "src/stores/admin";
import { Button } from "src/components/interfaces/Button";

const AssignRole = () => {
  const dispatch = useDispatch();
  const allRole = useSelector(allRoles);
  const userInfo = useSelector(userAccount);
  const userRoleList = useSelector(userRoleLists);
  const [roleLists] = useSelector(getRoleLists);
  const [defaultTabRole] = allRole;

  useEffect(() => {
    const rolenameData = roleLists?.map((role) => role["rolename"]);
    const districtNameData = roleLists?.map((role) => role["tenhuyen"]);
    const allRolesName = removeValDuplicates(rolenameData);
    const allDistrictsName = removeValDuplicates(districtNameData);

    const layers = (role, district) => {
      return roleLists
        ?.filter((r) => r["rolename"] === role && r["tenhuyen"] === district)
        ?.map((r) => ({
          layerID: r["malopdulieu"],
          layerName: r["lopdulieu"],
        }));
    };

    const districtID = (role, district) => {
      const thisDistrict = roleLists?.find(
        (r) => r["rolename"] === role && r["tenhuyen"] === district
      );
      return thisDistrict.maquanhuyen;
    };

    const districts = (role) => {
      return allDistrictsName?.map((district) => ({
        districtName: district,
        districtID: districtID(role, district),
        layers: layers(role, district),
      }));
    };

    const roleID = (role) => {
      const thisRole = roleLists?.find((r) => r["rolename"] === role);
      return thisRole.roleid;
    };

    const roles = allRolesName?.map((role) => ({
      roleName: role,
      roleID: roleID(role),
      districts: districts(role),
    }));
    dispatch(setAllRoles(roles));
  }, [roleLists, dispatch]);

  const handleSelectAdminRole = useCallback(() => {
    const layers = (layers) => {
      return layers.map((lyr) => lyr.layerID);
    };

    const districts = (roleID, districts) => {
      return districts.map((dt) => ({
        memberid: userInfo["memberid"],
        roleid: roleID,
        mahuyen: dt.districtID,
        malopdulieu: layers(dt.layers),
      }));
    };

    const roleType = allRole?.map((r) => {
      return districts(r.roleID, r.districts);
    });

    const everyRoles = roleType.reduce((acc, val) => {
      acc.push(...val);
      return acc;
    }, []);

    const roleAdmin = {
      memberid: userInfo["memberid"],
      roleid: Number(process.env.REACT_APP_ADMINROLE),
      mahuyen: null,
      malopdulieu: null,
    };

    const newRole = [...everyRoles, roleAdmin];
    dispatch(toggleSelectAdminRole(newRole));
  }, [userInfo, allRole, dispatch]);

  const handleCloseAssignRole = () => {
    dispatch(toggleAssignRole());
  };

  return (
    <div className="edit-role-form">
      <div className="edit-role-admin">
        <Form.Check
          type="checkbox"
          id="adminrole"
          label="Quản trị hệ thống"
          value=""
          onChange={handleSelectAdminRole}
          checked={userRoleList?.some(
            (r) => r["roleid"]?.toString() === process.env.REACT_APP_ADMINROLE
          )}
        />
      </div>
      <div className="edit-role-normal">
        {allRole?.length && (
          <TabPanel activeKey={defaultTabRole?.roleID}>
            {allRole?.map((r) => (
              <Tab key={r.roleID} id={r.roleID} title={r.roleName}>
                <DistrictRole
                  districtRoles={r.districts}
                  _thisRoleID={r.roleID}
                />
              </Tab>
            ))}
          </TabPanel>
        )}
      </div>
      <div className="edit-role-action">
        <Button outline onClick={handleCloseAssignRole}>
          Đóng
        </Button>
      </div>
    </div>
  );
};

export default AssignRole;
