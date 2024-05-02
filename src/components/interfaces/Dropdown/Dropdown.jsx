import "./Dropdown.css";
import { useState } from "react";
import { Button } from "src/components/interfaces/Button";
import DropdownContext, { useDropdown } from "src/contexts/Dropdown";

const Dropdown = ({ children }) => {
  const [visibleDropdown, setToggleDropdown] = useState(true);

  const toggleDropdown = (bool) => {
    setToggleDropdown(bool ? bool : !visibleDropdown);
  };

  return (
    <div className="dropdown">
      <DropdownContext.Provider value={{ visibleDropdown, toggleDropdown }}>
        {children}
      </DropdownContext.Provider>
    </div>
  );
};

const ToggleDropdown = ({ onClick, onBlur, children, ...ortherProps }) => {
  const { toggleDropdown } = useDropdown();

  const handleClick = (e) => {
    onClick && onClick(e);
    toggleDropdown();
  };

  const handleBlur = (e) => {
    onBlur && onBlur(e);
    toggleDropdown(false);
  };

  return (
    <Button onClick={handleClick} onBlur={handleBlur} {...ortherProps}>
      {children}
    </Button>
  );
};

const MenuDropdown = ({ children }) => {
  const { visibleDropdown } = useDropdown();
  return visibleDropdown && <div className="dropdown-menu">{children}</div>;
};

const ItemDropdown = ({ children }) => {
  return <div className="dropdown-item">{children}</div>;
};

Dropdown.Toggle = ToggleDropdown;
Dropdown.Menu = MenuDropdown;
Dropdown.Item = ItemDropdown;

export default Dropdown;
