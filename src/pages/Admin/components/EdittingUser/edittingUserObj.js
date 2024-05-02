import { apiCreateMember, apiUpdateMember } from "src/services/admin";
import { formValidation } from "src/utils/formValidation";

export const edittingUserObj = {
  create: {
    title: "THÊM MỚI NGƯỜI DÙNG",
    data: {
      onEdit: ({ userInfo }) => {
        const formNotValid = formValidation(userInfo);
        const isNotValid = Object.keys(formNotValid).length !== 0;
        if (isNotValid) return formNotValid;
        return {};
      },
    },
    submit: {
      text: "Thêm mới",
      onSubmit: apiCreateMember,
    },
  },
  update: {
    title: "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG",
    data: {
      onEdit: ({ userInfo }) => {
        const { password, ...userAccount } = userInfo;
        const formNotValid = formValidation(userAccount);
        const isNotValid = Object.keys(formNotValid).length !== 0;
        if (isNotValid) return formNotValid;
        return {};
      },
    },
    submit: {
      text: "Chỉnh sửa",
      onSubmit: apiUpdateMember,
    },
  },
};
