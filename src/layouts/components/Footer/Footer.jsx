import "./Footer.css";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import config from "src/config";
import { apiCountLogin } from "src/services/global";
import { getCountLogin } from "src/stores/global";

const Footer = () => {
  const dispatch = useDispatch();
  const [countLoginUser] = useSelector(getCountLogin);
  const configCounst = config.counts;

  useEffect(() => {
    apiCountLogin({ dispatch });
  }, [dispatch]);

  return (
    <footer className="footer">
      <div className="footer-content">
        <div className="footer-title">
          <span>Copyright: &copy; IGEO company</span>
        </div>
        <div className="footer-brand">
          <span>Cơ sở dữ liệu phục vụ cho công tác phòng chống thiên tai</span>
        </div>
        <div className="footer-countlogin">
          {configCounst.map((count) => (
            <span key={count.value}>
              {count.title}: {countLoginUser?.[count.value]}
            </span>
          ))}
        </div>
      </div>
    </footer>
  );
};

export default Footer;
