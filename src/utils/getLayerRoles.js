export const getLayerRoles = ({
  currentUser,
  roleLists,
  userRoleLists,
  _thisDistrict,
  _thisLayer,
  _thisRole,
}) => {
  // Check user account has this role
  if (!_thisDistrict || !_thisLayer) return;
  const userRole = currentUser?.["role"];
  const hasRole = userRole?.includes(_thisRole);
  if (!hasRole) return false;

  // Check user role has this layer
  const findedRole = roleLists?.find(
    (role) =>
      role["roleid"].toString() === _thisRole &&
      role["mahuyen"] === _thisDistrict &&
      role["lopdulieu"] === _thisLayer
  );
  const thisDistrictRole = findedRole?.["maquanhuyen"];
  const thisLayerRole = findedRole?.["malopdulieu"];
  const userThisRole = userRoleLists?.filter((role) =>
    role["roleid"].toString().includes(_thisRole)
  );
  const hasLayerThisRole = userThisRole?.some(
    (role) =>
      role["roleid"].toString() === _thisRole &&
      role["mahuyen"] === thisDistrictRole &&
      role["malopdulieu"].includes(thisLayerRole)
  );
  if (hasLayerThisRole) return true;

  // Check user role has all layers
  const layerThisRole = roleLists?.filter((role) =>
    role["roleid"].toString().includes(_thisRole)
  );
  const allLayerThisRole = userThisRole.reduce((acc, role) => {
    return ([...acc, ...role["malopdulieu"]])
  }, []);
  const hasAllLayerThisRole = layerThisRole.every((role) =>
    allLayerThisRole.includes(role["malopdulieu"])
  );
  if (hasAllLayerThisRole) return true;
  
  // Check user account has admin role
  const hasAdminRole = userRoleLists?.some((role) =>
    role["roleid"].toString().includes(process.env.REACT_APP_ADMINROLE)
  );
  if (hasAdminRole) return true;
  
  return false;
};
