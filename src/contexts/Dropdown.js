import { createContext, useContext } from "react";

const DropdownContext = createContext();

export const useDropdown = () => useContext(DropdownContext);

export default DropdownContext;
