import "./Tab.css";
import { Fragment, useState } from "react";
import clsx from "clsx";

const TabPanel = ({ activeKey, children }) => {
  const [currentKey, setKey] = useState(activeKey);
  return (
    <div className="tabs-container">
      <div className="tabs-control">
        {children.map((child) => (
          <div
            key={child.props.id}
            className={clsx("tab-item", {
              active: currentKey === child.props.id,
            })}
            onClick={() => setKey(child.props.id)}
          >
            {child.props.title}
          </div>
        ))}
      </div>
      <div className="tabs-content">
        {children.map((child) => (
          <div
            key={child.props.id}
            className={clsx("tab-pane", {
              active: currentKey === child.props.id,
            })}
          >
            {child.props.children}
          </div>
        ))}
      </div>
    </div>
  );
};

export const Tab = () => {
  return <Fragment />;
};

export default TabPanel;
