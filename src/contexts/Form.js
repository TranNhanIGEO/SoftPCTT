import { createContext, useContext } from "react";

const FormContext = createContext();

export const useForm = () => useContext(FormContext);

export default FormContext;
