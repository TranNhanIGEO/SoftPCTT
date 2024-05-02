import config from "src/config";
import {
  Admin,
  Data,
  Home,
  Login,
  Material,
  NotFound,
  Solution,
} from "src/pages";
import { CreateUser, UpdateUser, HistoryUpdate } from "src/pages/Admin/pages";
import { CreateData, UpdateData } from "src/pages/Data/pages";
import { CreateMaterial, UpdateMaterial } from "src/pages/Material/pages";
import { CreateSolution, UpdateSolution } from "src/pages/Solution/pages";

const protectedRoutes = [
  {
    path: config.routes.home,
    component: <Home />,
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    path: config.routes.management.showData,
    component: <Data />,
    role: [process.env.REACT_APP_EDITROLE],
  },
  {
    path: config.routes.management.createData,
    component: <CreateData />,
    role: [process.env.REACT_APP_EDITROLE],
  },
  {
    path: config.routes.management.updateData,
    component: <UpdateData />,
    role: [process.env.REACT_APP_EDITROLE],
  },
  {
    path: config.routes.admin.showUsers,
    component: <Admin />,
    role: [process.env.REACT_APP_ADMINROLE],
  },
  {
    path: config.routes.admin.createUser,
    component: <CreateUser />,
    role: [process.env.REACT_APP_ADMINROLE],
  },
  {
    path: config.routes.admin.updateUser,
    component: <UpdateUser />,
    role: [process.env.REACT_APP_ADMINROLE],
  },
  {
    path: config.routes.admin.historyUpdate,
    component: <HistoryUpdate />,
    role: [process.env.REACT_APP_ADMINROLE],
  },
  {
    path: config.routes.material.showMaterials,
    component: <Material />,
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    path: config.routes.material.createMaterial,
    component: <CreateMaterial />,
    role: [process.env.REACT_APP_EDITROLE],
  },
  {
    path: config.routes.material.updateMaterial,
    component: <UpdateMaterial />,
    role: [process.env.REACT_APP_EDITROLE],
  },
  {
    path: config.routes.solution.showSolutions,
    component: <Solution />,
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    path: config.routes.solution.createSolution,
    component: <CreateSolution />,
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    path: config.routes.solution.updateSolution,
    component: <UpdateSolution />,
    role: [process.env.REACT_APP_EDITROLE],
  },
];

const allowedRoutes = [
  {
    path: config.routes.login,
    component: <Login />,
  },
];

const errorRoutes = {
  path: config.routes.notfound,
  component: <NotFound />,
};

export { protectedRoutes, allowedRoutes, errorRoutes };
