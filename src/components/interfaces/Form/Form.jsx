import {
  FormGroup,
  FormLabel,
  FormControl,
  FormCheck,
  FormSuggestion,
  FormSelect,
} from "./modules";

const Form = ({ children }) => {
  return children;
};

Form.Group = FormGroup;
Form.Label = FormLabel;
Form.Control = FormControl;
Form.Select = FormSelect;
Form.Check = FormCheck;
Form.Suggestion = FormSuggestion;

export default Form;
