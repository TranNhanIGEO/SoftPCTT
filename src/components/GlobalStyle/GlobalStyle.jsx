import "./GlobalStyle.css";
import PropTypes from "prop-types";

const GlobalStyle = ({ children }) => {
  return children;
}

GlobalStyle.propTypes = {
  children: PropTypes.node.isRequired,
};

export default GlobalStyle;
