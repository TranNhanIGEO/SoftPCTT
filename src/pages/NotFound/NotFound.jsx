import "./NotFound.css";
import React from "react";
import { Link } from "react-router-dom";
import { img } from "src/assets";

const NotFound = () => (
  <div className="notfound">
    <img src={img.notFoundImg} alt="" />
    <Link to="/">Quay về trang chủ</Link>
  </div>
);

export default NotFound;
