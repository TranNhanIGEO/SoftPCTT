import "./Login.css";
import { LoginForm } from "./components"
import { Theme } from "src/components/Theme";

const Login = () => {
  return (
    <div className="login">
      <Theme />
      <LoginForm />
    </div>
  );
};

export default Login;
