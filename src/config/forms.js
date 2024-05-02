const forms = {
  userInfoForms: [
    { id: "username", type: "text", label: "Tên đăng nhập" },
    { id: "password", type: "password", label: "Mật khẩu" },
    { id: "fullname", type: "text", label: "Họ và tên" },
    { id: "unit", type: "text", label: "Đơn vị" },
    { id: "department", type: "text", label: "Phòng ban" },
    { id: "phone", type: "tel", label: "Số điện thoại" },
    { id: "email", type: "text", label: "Email" },
  ],
  userLoginForms: [
    { id: "usr", type: "text", label: "Tên tài khoản" },
    { id: "pwd", type: "password", label: "Mật khẩu" },
  ]
};

export default forms;
