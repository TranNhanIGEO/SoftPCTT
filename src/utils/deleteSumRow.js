import config from "src/config";

export const deleteSumRow = ({ layer, data }) => {
  switch (layer) {
    case "Áp thấp nhiệt đới":
    case "Lốc":
    case "Kè":
    case "Đê bao, bờ bao":
    case "Mốc cảnh báo triều cường":
    case "Biển cảnh báo sạt lở":
    case "Khu neo đậu tàu thuyền":
    case "Vị trí xung yếu":
    case "Vị trí an toàn":
    case "Kế hoạch dự kiến di dời, sơ tán dân":
      const thisLayer = config.statistics[layer];
      const sumRow = thisLayer?.xaxis?.name;
      return data?.filter((dt) => dt[sumRow] !== "Tổng cộng");
    default:
      return data;
  }
};
