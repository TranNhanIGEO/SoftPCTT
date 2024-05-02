import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { refreshUser } from "src/services/auth";
import { currentRefresh, currentToken } from "src/stores/auth";

const useRefresh = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const token = useSelector(currentToken);
  const refresh = useSelector(currentRefresh);
  return () => refreshUser({ token, refresh, dispatch, navigate });
};

export default useRefresh;
