import PropTypes from "prop-types";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import { currentRefresh, currentToken } from "src/stores/auth";
import { apiLogoutUser } from "src/api/auth";
// import useRefresh from "src/hooks/useRefresh";
// import { currentUser } from "src/stores/auth";

const Browser = ({ children }) => {
  const axiosJWT = useAxiosJWT();
  const refresh = useSelector(currentRefresh);
  const token = useSelector(currentToken);
  // const refreshToken = useRefresh();
  // const hasUser = useSelector(currentUser);

  // useEffect(() => {
  //   if (accessToken && hasUser) return;
  //   refreshToken();
  // }, [hasUser, accessToken, refreshToken]);

  const logoutUser = async () => {
    await axiosJWT.post(apiLogoutUser, { rt: refresh });
  };

  useEffect(() => {
    if (!token) return;
    window.addEventListener("beforeunload", logoutUser);
    return () => window.removeEventListener("beforeunload", logoutUser);
  });

  return children;
};

Browser.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Browser;
