import "./LayerRole.css";
import { Fragment, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import clsx from "clsx";
import Form from "src/components/interfaces/Form";
import {
  userRoleLists,
  toggleLayerRoles,
  changeSelectAllLayers,
  unChangeSelectAllLayers,
} from "src/stores/admin";

const LayerRole = ({ layerRoles, _thisDistrictID }) => {
  const dispatch = useDispatch();
  const userRoleList = useSelector(userRoleLists);

  const handleSelectAllLayers = useCallback(() => {
    const hasDistricts = userRoleList.find(
      (r) => r["mahuyen"] === _thisDistrictID
    );
    const isSelectAllLayers = layerRoles.every((r) =>
      hasDistricts["malopdulieu"].includes(r.layerID)
    );

    const newRole = {
      mahuyen: _thisDistrictID,
      malopdulieu: layerRoles?.map((lyr) => lyr.layerID),
    };

    !isSelectAllLayers
      ? dispatch(changeSelectAllLayers(newRole))
      : dispatch(unChangeSelectAllLayers(newRole));
  }, [userRoleList, _thisDistrictID, layerRoles, dispatch]);

  const handleTogglelayer = useCallback(
    (e) => {
      const { value } = e.target;
      const findedRole = userRoleList?.find(
        (role) => role["mahuyen"] === _thisDistrictID
      );
      const updateLayer = {
        mahuyen: findedRole["mahuyen"],
        malopdulieu: value,
      };
      dispatch(toggleLayerRoles(updateLayer));
    },
    [userRoleList, _thisDistrictID, dispatch]
  );

  return (
    <Fragment>
      <div
        className={clsx("layer-roles", {
          active: userRoleList?.some(
            (role) => role["mahuyen"] === _thisDistrictID
          ),
        })}
      >
        <div className="layer-roles-title">
          <span>Lớp dữ liệu</span>
          <Form.Check
            type="checkbox"
            checked={userRoleList
              ?.filter((r) => r["malopdulieu"] !== null)
              ?.some(
                (r) =>
                  r["malopdulieu"]?.length === layerRoles?.length &&
                  r["mahuyen"] === _thisDistrictID
              )}
            onChange={handleSelectAllLayers}
          />
        </div>
        <div className="layer-roles-content">
          <ul className="layer-roles-list">
            {userRoleList
              ?.filter((r) => r.huyen !== null)
              ?.some((r) => r["mahuyen"] === _thisDistrictID) &&
              layerRoles?.map((l) => (
                <li key={l.layerID}>
                  <Form.Check
                    type="checkbox"
                    id={l.layerID}
                    label={l.layerName}
                    value={l.layerID}
                    onChange={handleTogglelayer}
                    checked={userRoleList
                      ?.filter((r) => r["malopdulieu"] !== null)
                      ?.some((r) => r["malopdulieu"]?.includes(l.layerID))}
                  />
                </li>
              ))}
          </ul>
        </div>
      </div>
    </Fragment>
  );
};

export default LayerRole;
