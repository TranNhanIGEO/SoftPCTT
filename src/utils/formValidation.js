export const formValidation = (values) => {
  const errors = {};
  const regexUsername = /^[a-zA-Z0-9_@.]{8,12}$/;
  const regexPassword =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
  const regexEmail =
    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  const regexPhone = /(\+84|84|0[3|5|7|8|9])+([0-9]{8})\b/;
  if (!values.username) {
    errors.username = "Tài khoản là bắt buộc!";
  } else if (values.username.length < 8) {
    errors.username = "Tài khoản phải có ít nhất 8 ký tự!";
  } else if (values.username.length > 12) {
    errors.username = "Tài khoản chỉ có tối đa 12 ký tự!";
  } else if (!regexUsername.test(values.username)) {
    errors.username = "Tài khoản chỉ bao gồm chữ hoa, chữ thường, chữ số và các ký tự '_@.'";
  }

  if (values.password !== undefined) {
    if (!values.password) {
      errors.password = "Mật khẩu là bắt buộc!";
    } else if (values.password.length < 8) {
      errors.password = "Mật khẩu phải có ít nhất 8 ký tự!";
    } else if (values.password.length > 32) {
      errors.password = "Mật khẩu chỉ có tối đa 32 ký tự!";
    } else if (!regexPassword.test(values.password)) {
      errors.password = "Mật khẩu phải bao gồm chữ hoa, chữ thường, chữ số và ít nhất một ký tự đặt biệt!";
    }
  }

  if (!values.fullname) {
    errors.fullname = "Bạn vẫn chưa nhập tên người dùng!";
  }
  
  if (!values.unit) {
    errors.unit = "Bạn vẫn chưa nhập tên đơn vị!";
  }

  if (!values.department) {
    errors.department = "Bạn vẫn chưa nhập tên phòng ban!";
  }

  if (!values.email) {
    errors.email = "Bạn vẫn chưa nhập email!";
  } else if (!regexEmail.test(values.email)) {
    errors.email = "Email không đúng định dạng!";
  }

  if (!values.phone) {
    errors.phone = "Bạn vẫn chưa nhập số điện thoại!";
  } else if (values.phone.length < 10) {
    errors.phone = "Số điện thoại phải chứa 10 số!";
  } else if (values.phone.length > 10) {
    errors.phone = "Số điện thoại chỉ chứa 10 số!";
  } else if (!regexPhone.test(values.phone)) {
    errors.phone = "Số điện thoại không được chứa chữ viết và ký tự đặc biệt!";
  }
  return errors;
};
