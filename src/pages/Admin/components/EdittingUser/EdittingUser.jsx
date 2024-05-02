import "./EdittingUser.css";
import { Fragment, useCallback, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import CryptoJS from "crypto-js";
import { v4 as uuidv4 } from "uuid";
import useAxiosJWT from "src/hooks/useAxiosJWT";
import useToast from "src/hooks/useToast";
import config from "src/config";
import { Button, ButtonGroup } from "src/components/interfaces/Button";
import { formValidation } from "src/utils/formValidation";
import { edittingUserObj } from "./edittingUserObj";
import { apiDeleteMember, apiUpdateMemberPassword } from "src/services/admin";
import {
  toggleAssignRole,
  userRoleLists,
  userAccount,
  setUserAccount,
  isAssignRole,
  initState,
} from "src/stores/admin";
import Form from "src/components/interfaces/Form";

const EdittingUser = ({ pageType }) => {
  const edittingPage = edittingUserObj[pageType];
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const axiosJWT = useAxiosJWT();
  const toast = useToast();
  const { id } = useParams();
  const { userAccount: userAccountValidate } = initState;
  const isPermission = useSelector(isAssignRole);
  const userInfo = useSelector(userAccount);
  const userRoleList = useSelector(userRoleLists);
  const randomUserID = uuidv4().replaceAll("-", "");
  const [formValidate, setFormValidate] = useState(userAccountValidate);
  const configForms = config.forms;

  const handleDeleteUser = useCallback(() => {
    apiDeleteMember({ id, axiosJWT, navigate, toast });
  }, [id, axiosJWT, navigate, toast]);

  const handleEnterForm = useCallback(
    (e) => {
      const { name, value } = e.target;
      dispatch(setUserAccount({ ...userInfo, [name]: value }));
    },
    [userInfo, dispatch]
  );

  const handleTogglePassword = useCallback(() => {
    typeof userInfo.password === "string"
      ? dispatch(setUserAccount({ ...userInfo, password: null }))
      : dispatch(setUserAccount({ ...userInfo, password: "" }));
  }, [userInfo, dispatch]);

  const handleResetPassword = useCallback(() => {
    if (!userInfo.password) return;
    const formNotValid = formValidation(userInfo);
    const isNotValid = Object.keys(formNotValid).length !== 0;
    if (isNotValid) return setFormValidate(formNotValid);
    setFormValidate({});
    const hash = CryptoJS.SHA512(userInfo.password);
    const hashedPwd = hash.toString(CryptoJS.enc.Base64);
    const updatePassWord = {
      memberid: userInfo.memberid,
      pwd: hashedPwd,
    };
    apiUpdateMemberPassword({ updatePassWord, axiosJWT, toast });
    dispatch(setUserAccount({ ...userInfo, password: null }));
  }, [userInfo, axiosJWT, dispatch, toast]);

  const handleEditUser = useCallback(() => {
    const onEditData = edittingPage.data.onEdit;
    const dataValidation = onEditData({ userInfo });
    setFormValidate(dataValidation);
    const isNotValid = Object.keys(dataValidation).length !== 0;
    if (isNotValid) return;
    dispatch(toggleAssignRole(true));
    if (userInfo.memberid) return;
    dispatch(setUserAccount({ ...userInfo, memberid: randomUserID }));
  }, [edittingPage, randomUserID, userInfo, dispatch]);

  const handleSubmit = useCallback(() => {
    const onSubmit = edittingPage.submit.onSubmit;
    const hash = CryptoJS.SHA512(userInfo.password);
    const hashedPwd = hash.toString(CryptoJS.enc.Base64);
    const userAccount = { ...userInfo, password: hashedPwd };
    onSubmit({ axiosJWT, userAccount, userRoleList, navigate, toast });
  }, [edittingPage, userInfo, userRoleList, axiosJWT, navigate, toast]);

  return (
    <div className="edit-data-form">
      <div className="edit-data-title">
        <span>CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG</span>
        {pageType === "update" && (
          <Button onClick={handleDeleteUser}>
            Xóa
          </Button>
        )}
      </div>
      <form className="edit-data-content">
        <div className="edit-data-argument">
          <Form>
            {configForms.userInfoForms.map((form) => (
              <Form.Group key={form.id} controlID={form.id}>
                <Form.Label>{form.label}</Form.Label>
                <Form.Control
                  type={form.type}
                  id={form.id}
                  name={form.id}
                  disabled={
                    typeof userInfo[form.id] !== "string" || isPermission
                  }
                  value={userInfo[form.id] ?? ""}
                  onChange={handleEnterForm}
                />
                {pageType === "update" && form.id === "password" && (
                  <ResetPassword
                    userInfo={userInfo}
                    onToggle={handleTogglePassword}
                    onReset={handleResetPassword}
                  />
                )}
                {Object.keys(formValidate).length !== 0 &&
                  formValidate[form.id] && <span>{formValidate[form.id]}</span>}
              </Form.Group>
            ))}
          </Form>
        </div>
      </form>
      <div className="edit-data-action">
        <Button outline onClick={() => navigate("/admin")}>
          Thoát
        </Button>
        {!isPermission ? (
          <Button outline onClick={handleEditUser}>
            Phân quyền
          </Button>
        ) : (
          <Button outline onClick={handleSubmit}>
            Chỉnh sửa
          </Button>
        )}
      </div>
    </div>
  );
};

const ResetPassword = ({ userInfo, onToggle, onReset }) => {
  const isPermission = useSelector(isAssignRole);
  return (
    <Fragment>
      {typeof userInfo["password"] !== "string" ? (
        <Button
          outline
          className="edit-data-password"
          onClick={onToggle}
          disabled={isPermission}
        >
          Đặt lại mật khẩu
        </Button>
      ) : (
        <ButtonGroup>
          <Button outline className="edit-data-password" onClick={onReset}>
            Lưu
          </Button>
          <Button outline className="edit-data-password" onClick={onToggle}>
            Hủy
          </Button>
        </ButtonGroup>
      )}
    </Fragment>
  );
};

export default EdittingUser;
