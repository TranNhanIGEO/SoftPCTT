const pages = [
  {
    id: process.env.REACT_APP_HOMEPAGE,
    path: "/",
    children: "Bản đồ",
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    id: process.env.REACT_APP_SOLUTIONPAGE,
    path: "/solution",
    children: "Kế hoạch - Phương án",
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    id: process.env.REACT_APP_MATERIALPAGE,
    path: "/material",
    children: "Tư liệu",
    role: [process.env.REACT_APP_VIEWROLE],
  },
  {
    id: process.env.REACT_APP_ADMINPAGE,
    path: "/admin",
    children: "Quản trị hệ thống",
    role: [process.env.REACT_APP_ADMINROLE],
  },
];

export default pages;
