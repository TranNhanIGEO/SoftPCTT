import "./FormControl.css";
import clsx from "clsx";

const FormControl = ({
  type,
  asTextArea,
  placeholder,
  className,
  disabled = false,
  ...ortherProps
}) => {
  let Control = "input";
  if (asTextArea) Control = "textarea";

  return (
    <Control
      type={type ?? "text"}
      disabled={disabled}
      placeholder={placeholder}
      className={clsx("form-control", className)}
      {...ortherProps}
    />
  );
};

export default FormControl;
