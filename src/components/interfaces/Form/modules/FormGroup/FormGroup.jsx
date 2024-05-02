import "./FormGroup.css";
import clsx from "clsx";
import FormContext from "src/contexts/Form";

const FormGroup = ({ className, children, controlID, ...ortherProps }) => {
  return (
    <FormContext.Provider value={{ controlID }}>
      <div className={clsx("form-group", className)} {...ortherProps}>
        {children}
      </div>
    </FormContext.Provider>
  );
};

export default FormGroup;
