import "./RootLayout.css"
import PropTypes from "prop-types";
import { Footer, Header, Loading } from "src/layouts/components";

const RootLayout = ({ children }) => {
  return (
    <div className="wrapper">
      <Header />
      <main className="content">
        {children}
      </main>
      <Footer />
      <Loading />
    </div>
  );
};

RootLayout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default RootLayout;
