import { useSelector } from "react-redux";
import { currentToken, currentUser } from "src/stores/auth";
import decodeToken from "src/utils/decodeToken";

const useToken = () => {
  const accessToken = useSelector(currentToken);
  const userInfo = decodeToken(accessToken);
  const hasUser = useSelector(currentUser);
  if (!accessToken && !hasUser) return;
  return userInfo;
};

export default useToken;
