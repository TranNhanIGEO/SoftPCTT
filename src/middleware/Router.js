import { Navigate, Outlet } from "react-router-dom";
import useToken from "src/hooks/useToken";
import { NotFound } from "src/pages";

const Protected = () => {
  const currentUser = useToken();
  return !currentUser ? <Navigate to={"/login"} /> : <Outlet />;
};

const Allowed = () => {
  const currentUser = useToken();
  return currentUser ? <Navigate to={"/"} /> : <Outlet />;
};

const Required = ({ role }) => {
  const currentUser = useToken();
  const userRoles = currentUser?.["role"];
  const hasRole = role.some((r) => userRoles?.includes(r));
  return !hasRole ? <NotFound /> : <Outlet />;
};

export { Protected, Allowed, Required };
