import "./FormLabel.css";
import clsx from "clsx";
import { useForm } from "src/contexts/Form";

const FormLabel = ({ className, children, ...ortherProps }) => {
  const { controlID } = useForm();
  return (
    <label
      htmlFor={controlID}
      className={clsx("form-label", className)}
      {...ortherProps}
    >
      {children}
    </label>
  );
};

export default FormLabel;
