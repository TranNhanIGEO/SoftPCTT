import {
  apiShowMaterial,
  apiCreateMaterial,
  apiUpdateMaterial,
} from "src/services/material";

export const edittingMaterialObj = {
  create: {
    title: "THÊM MỚI TƯ LIỆU",
    data: {
      onGet: ({ materialField }) => {
        if (!materialField?.length) return {};
        const newInfos = materialField.reduce((acc, col) => {
          acc[col.accessorKey] = "";
          return acc;
        }, {});
        return newInfos;
      },
    },
    submit: {
      text: "Thêm mới",
      onSubmit: apiCreateMaterial,
    },
  },
  update: {
    title: "CHỈNH SỬA TƯ LIỆU",
    data: {
      onGet: async ({ thisAPI, id, axiosJWT }) => {
        const response = await apiShowMaterial({ thisAPI, id, axiosJWT });
        return response;
      },
    },
    submit: {
      text: "Chỉnh sửa",
      onSubmit: apiUpdateMaterial,
    },
  },
};
