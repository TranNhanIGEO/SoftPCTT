import "./LoginForm.css";
import { useCallback, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import CryptoJS from "crypto-js";
import { loginUser } from "src/services/auth";
import { errorStatus, loginFailed } from "src/stores/auth";
import useToast from "src/hooks/useToast";
import config from "src/config";
import { Button } from "src/components/interfaces/Button";
import Form from "src/components/interfaces/Form";

const LoginForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const toast = useToast();
  const errorLogin = useSelector(errorStatus);
  const [enteredLogin, setEnterLogin] = useState(false);
  const [userInfo, setUserInfo] = useState({ usr: "", pwd: "" });
  const configForms = config.forms;

  const handleLogin = (e) => {
    const { name, value } = e.target;
    setUserInfo((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setEnterLogin(true);
    if (!userInfo.usr) return dispatch(loginFailed("Bạn chưa nhập tài khoản!"));
    if (!userInfo.pwd) return dispatch(loginFailed("Bạn chưa nhập mật khẩu!"));
    const hash = CryptoJS.SHA512(userInfo.pwd);
    const hashedPwd = hash.toString(CryptoJS.enc.Base64);
    const userAccount = { ...userInfo, pwd: hashedPwd };
    loginUser({ userAccount, dispatch, navigate });
  };

  const handleForgetPassword = useCallback(() => {
    toast.info({
      title: "Thông báo",
      message: "Vui lòng liên hệ quản trị viên để được cấp lại mật khẩu mới!",
    });
  }, [toast]);

  const handleLoginNoAccount = useCallback(() => {
    const hash = CryptoJS.SHA512(process.env.REACT_APP_GUEST_PASSWORD);
    const hashedPwd = hash.toString(CryptoJS.enc.Base64);
    const userAccount = { usr: "guest", pwd: hashedPwd };
    loginUser({ userAccount, dispatch, navigate });
  }, [dispatch, navigate]);

  return (
    <div className="login-container">
      <div className="login-title">ĐĂNG NHẬP</div>
      <form className="login-form" onSubmit={(e) => handleSubmit(e)}>
        {enteredLogin && errorLogin && (
          <span className="login-error">{errorLogin}</span>
        )}
        <Form>
          {configForms.userLoginForms.map((form) => (
            <Form.Group key={form.id} controlID={form.id}>
              <Form.Label>{form.label}</Form.Label>
              <Form.Control
                type={form.type}
                id={form.id}
                name={form.id}
                value={userInfo[form.id]}
                onChange={(e) => handleLogin(e)}
              />
            </Form.Group>
          ))}
        </Form>
        <div className="login-btn">
          <Button isSubmit outline>
            Đăng nhập
          </Button>
        </div>
        <div className="forget-password">
          <Button onClick={handleForgetPassword}>Quên mật khẩu?</Button>
        </div>
        <div className="guestlogin-btn">
          <Button outline onClick={handleLoginNoAccount}>
            Sử dụng mà không cần đăng nhập
          </Button>
        </div>
      </form>
    </div>
  );
};

export default LoginForm;
