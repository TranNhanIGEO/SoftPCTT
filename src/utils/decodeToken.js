import { jwtDecode } from "jwt-decode";

const decodeToken = (token) => {
  if (!token) return;
  // const handledToken = token.replace("_", ".")
  // const realToken = `${process.env.REACT_APP_SECRET_JWT}.${handledToken}`
  const decodeJWT = jwtDecode(token);
  const keyJWT = Object.keys(decodeJWT);
  const valueJWT = Object.values(decodeJWT);
  const claimXMLS = process.env.REACT_APP_CLAIM_XMLS;
  const claimMCRS = process.env.REACT_APP_CLAIM_MCRS;
  const emptyString = "";
  const idxRole = keyJWT.findIndex((key) => key === "role");
  const isRoleString = typeof valueJWT[idxRole] === "string";
  valueJWT[idxRole] = isRoleString
    ? [valueJWT[idxRole]]
    : valueJWT[idxRole];
  const objJWT = keyJWT.reduce((acc, val, i) => {
    val = val?.includes(claimXMLS) ? val.replace(claimXMLS, emptyString) : val;
    val = val?.includes(claimMCRS) ? val.replace(claimMCRS, emptyString) : val;
    return { ...acc, [val]: valueJWT[i] };
  }, {});
  return objJWT;
};

export default decodeToken;
