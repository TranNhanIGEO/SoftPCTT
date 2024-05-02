import "./Header.css";
import { Fragment, useCallback, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import clsx from "clsx";
import { img } from "src/assets";
import { logoutUser } from "src/services/auth";
import config from "src/config";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToken from "src/hooks/useToken";
import { setPage } from "src/stores/global";
import { currentRefresh } from "src/stores/auth";

const Header = () => {
  const currentUser = useToken();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const axiosJWT = useAxiosJWT();
  const refresh = useSelector(currentRefresh);
  const [isOpenMenu, toggleOpenMenu] = useState(false);
  const isLoginPage = useLocation()?.pathname === "/login";
  const configPages = config.pages;

  const handleChangePage = useCallback(
    (e) => {
      const { id } = e.target.parentNode;
      dispatch(setPage(id));
      toggleOpenMenu(false);
    },
    [dispatch]
  );

  const handleLogout = useCallback(() => {
    logoutUser({ refresh, axiosJWT, dispatch, navigate });
    toggleOpenMenu(false);
  }, [refresh, axiosJWT, dispatch, navigate]);

  return (
    <header className="header">
      <div className="header-container">
        <div className="header-content">
          <img className="header-logo" alt="" src={img.pcttLogoImg} />
          <span className="header-title">
            CƠ SỞ DỮ LIỆU PHÒNG CHỐNG THIÊN TAI - TP.HCM
          </span>
        </div>
        {!isLoginPage && (
          <Fragment>
            <ul className={clsx("header-transfer", { active: isOpenMenu })}>
              {configPages
                .filter((page) =>
                  page.role.some((r) => currentUser?.["role"]?.includes(r))
                )
                .map((page) => (
                  <li key={page.id} id={page.id} onClick={handleChangePage}>
                    <Link to={page.path}>{page.children}</Link>
                  </li>
                ))}
              {currentUser && (
                <Fragment>
                  <li>Chào {currentUser?.["name"]}!</li>
                  <li onClick={handleLogout}>
                    <img src={img.logoutImg} alt="" />
                  </li>
                </Fragment>
              )}
            </ul>
            <div className="header-menu">
              <img
                src={img.openMenuImg}
                alt=""
                onClick={() => toggleOpenMenu((prev) => !prev)}
              />
            </div>
          </Fragment>
        )}
      </div>
    </header>
  );
};

export default Header;
