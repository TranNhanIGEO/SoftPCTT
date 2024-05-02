import "./FormSelect.css";
import clsx from "clsx";

const FormSelect = ({
  disabled,
  isSelected,
  defaultValue,
  className,
  children,
  ...ortherProps
}) => {
  const classes = clsx("form-select", {
    [className]: className,
    disabled,
  });

  return (
    <select disabled={disabled} className={classes} {...ortherProps}>
      {isSelected && <option>{defaultValue}</option>}
      {children?.map((child) => (
        <option
          key={child.props.value}
          id={child.props.value}
          value={child.props.value}
        >
          {child.props.children}
        </option>
      ))}
    </select>
  );
};

export default FormSelect;
