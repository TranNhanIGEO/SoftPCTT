const routes = {
  notfound: "/*",
  login: "/login",
  home: "/",
  management: {
    showData: "/management",
    createData: "/management/create",
    updateData: "/management/update/:id",
  },
  admin: {
    showUsers: "/admin",
    createUser: "/admin/create",
    updateUser: "/admin/update/:id",
    historyUpdate: "/admin/history",
  },
  material: {
    showMaterials: "/material",
    createMaterial: "/material/create",
    updateMaterial: "/material/update/:id",
  },
  solution: {
    showSolutions: "/solution",
    createSolution: "/solution/create",
    updateSolution: "/solution/update/:id",
  },
};

export default routes;
