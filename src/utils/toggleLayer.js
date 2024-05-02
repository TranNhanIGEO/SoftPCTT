export const directionLayers = [
  "Điểm bắt đầu",
  "Điểm kết thúc",
  "Hệ thống dẫn đường-Đường",
  "Hệ thống dẫn đường-Điểm",
  "Điểm dẫn đường hiện tại",
];
export const depressionLayers = [
  "Áp thấp nhiệt đới",
  "Ngày xảy ra áp thấp",
  "Đường đi của áp thấp",
];
export const stormLayers = ["Bão", "Ngày xảy ra bão", "Đường đi của bão"];
export const hotLayers = ["Nắng nóng", "Trạm đo nhiệt độ"];
export const salinityLayers = ["Xâm nhập mặn (Độ mặn)", "Trạm đo mặn"];
export const rainLayers = ["Mưa", "Trạm đo mưa"];
export const waterLevelLayers = ["Mực nước", "Trạm đo mực nước"];
export const lakeLayers = ["Hồ chứa", "Hồ chứa nước", "Hệ thống hồ chứa"];
export const visualTemparatureLayers = ["Mô hình hóa nhiệt độ"];
export const visualSalinityLayers = [
  "Mô hình hóa độ mặn 1",
  "Mô hình hóa độ mặn 2",
  "Mô hình hóa độ mặn 3",
  "Mô hình hóa độ mặn 4",
  "Mô hình hóa độ mặn 5",
  "Mô hình hóa độ mặn 6",
  "Mô hình hóa độ mặn 7",
  "Mô hình hóa độ mặn 8",
  "Mô hình hóa độ mặn 9",
  "Mô hình hóa độ mặn 10",
  "Mô hình hóa độ mặn 11",
  "Mô hình hóa độ mặn 12",
  "Mô hình hóa độ mặn 13",
  "Mô hình hóa độ mặn 14",
  "Mô hình hóa độ mặn 15",
  "Mô hình hóa độ mặn 16",
  "Mô hình hóa độ mặn 17",
  "Mô hình hóa độ mặn 18",
  "Mô hình hóa độ mặn 19",
  "Mô hình hóa độ mặn 20",
  "Mô hình hóa độ mặn 21",
  "Mô hình hóa độ mặn 22",
  "Mô hình hóa độ mặn 23",
  "Mô hình hóa độ mặn 24",
  "Mô hình hóa độ mặn 25",
];

export const removeLayer = ({ layers, map }) => {
  layers.forEach((lyr) => {
    const hasLayer = map.getLayer(lyr);
    if (!hasLayer) return;
    map.removeLayer(lyr);
  });
};

export const moveLayer = ({ layers, map }) => {
  layers.forEach((lyr) => {
    const hasLayer = map.getLayer(lyr);
    if (!hasLayer) return;
    map.moveLayer(lyr);
  });
};

export const toggleOrtherLayer = ({ layers, isChecked, map }) => {
  const hasLayers = map?.getLayer(layers?.[0]);
  if (!isChecked && hasLayers) {
    layers.forEach((lyr) => {
      const hasLayer = map.getLayer(lyr);
      if (!hasLayer) return;
      map?.setLayoutProperty(lyr, "visibility", "none");
    });
    return;
  }
  if (isChecked && hasLayers) {
    layers.forEach((lyr) => {
      const hasLayer = map.getLayer(lyr);
      if (!hasLayer) return;
      map?.setLayoutProperty(lyr, "visibility", "visible");
    });
    return;
  }
  return;
};

export const toggleLayer = ({ layerName, isChecked, hasLayer, map }) => {
  if (!hasLayer && layerName === "Hướng di chuyển sơ tán dân") {
    return toggleOrtherLayer({ layers: directionLayers, isChecked, map });
  }
  if (!hasLayer && layerName === "Mô hình hóa độ mặn") {
    return toggleOrtherLayer({ layers: visualSalinityLayers, isChecked, map });
  }
  if (!hasLayer && layerName === "Mô hình hóa nhiệt độ") {
    return toggleOrtherLayer({ layers: visualTemparatureLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Bão") {
    return toggleOrtherLayer({ layers: stormLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Áp thấp nhiệt đới") {
    return toggleOrtherLayer({ layers: depressionLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Nắng nóng") {
    return toggleOrtherLayer({ layers: hotLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Xâm nhập mặn (Độ mặn)") {
    return toggleOrtherLayer({ layers: salinityLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Mưa") {
    return toggleOrtherLayer({ layers: rainLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Mực nước") {
    return toggleOrtherLayer({ layers: waterLevelLayers, isChecked, map });
  }
  if (hasLayer && layerName === "Hồ chứa") {
    return toggleOrtherLayer({ layers: lakeLayers, isChecked, map });
  }
  if (!isChecked && hasLayer) {
    return map?.setLayoutProperty(layerName, "visibility", "none");
  }
  if (isChecked && hasLayer) {
    return map?.setLayoutProperty(layerName, "visibility", "visible");
  }
  return;
};
