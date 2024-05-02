import "./Button.css";
import clsx from "clsx";

const Button = ({
  className,
  onClick,
  children,
  isSubmit = false,
  primary = false,
  secondary = false,
  success = false,
  warning = false,
  danger = false,
  outline = false,
  rounded = false,
  disabled = false,
  small = false,
  large = false,
  ...ortherProps
}) => {
  const classes = clsx("btn", {
    [className]: className,
    primary,
    secondary,
    success,
    warning,
    danger,
    outline,
    disabled,
    rounded,
    small,
    large,
  });

  return (
    <button
      type={isSubmit ? "submit" : "button"}
      className={classes}
      onClick={onClick}
      disabled={disabled}
      {...ortherProps}
    >
      {children}
    </button>
  );
};

export default Button;
