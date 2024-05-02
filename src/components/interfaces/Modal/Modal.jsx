import "./Modal.css";
import { useEffect } from "react";
import ModalContext, { useModal } from "src/contexts/Modal";
import { Button } from "../Button";

const Modal = ({ children, isOpen, onClose }) => {
  const handleEnter = (e) => {
    e.key === "Escape" && onClose();
  };

  useEffect(() => {
    document.addEventListener("keydown", handleEnter);
    return () => document.removeEventListener("keydown", handleEnter);
  });

  return (
    isOpen && (
      <div className="modal-overlay">
        <div className="modal-dialog" onClick={(e) => e.stopPropagation()}>
          <div className="modal-content">
            <ModalContext.Provider value={{ onClose }}>
              {children}
            </ModalContext.Provider>
          </div>
        </div>
      </div>
    )
  );
};

const ModalDismiss = ({ children, type }) => {
  const { onClose } = useModal();
  return (
    <button type={type} className="modal-close" onClick={onClose}>
      {children}
    </button>
  );
};

const ModalButton = ({ children, type, onClick }) => {
  return (
    <Button small type={type} className="modal-button" onClick={onClick}>
      {children}
    </Button>
  );
};

const ModalHeader = ({ children }) => {
  return (
    <div className="modal-header">
      <div className="modal-title">{children}</div>
      <ModalDismiss type="button">&times;</ModalDismiss>
    </div>
  );
};

const ModalBody = ({ children }) => {
  return <div className="modal-body">{children}</div>;
};

const ModalFooter = ({ children }) => {
  return <div className="modal-footer">{children}</div>;
};

Modal.Header = ModalHeader;
Modal.Body = ModalBody;
Modal.Footer = ModalFooter;
Modal.Button = ModalButton;
Modal.Dismiss = ModalDismiss;

export default Modal;
