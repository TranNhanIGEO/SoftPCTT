import "./FormCheck.css";
import clsx from "clsx";

const FormCheck = ({
  type,
  id,
  label,
  className,
  checked = false,
  disabled = false,
  ...ortherProps
}) => {
  return (
    <div className={clsx("form-check", className)}>
      <input
        type={type}
        id={id}
        disabled={disabled}
        checked={checked}
        {...ortherProps}
      />
      <label htmlFor={id}>{label}</label>
    </div>
  );
};

export default FormCheck;
