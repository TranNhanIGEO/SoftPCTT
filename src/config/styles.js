const styles = {
  mapboxDraw: [
    {
      id: "gl-draw-polygon-fill-inactive",
      type: "fill",
      filter: [
        "all",
        ["==", "active", "false"],
        ["==", "$type", "Polygon"],
        ["!=", "mode", "static"],
      ],
      paint: {
        "fill-color": [
          "case",
          ["==", ["get", "user_class_id"], 1],
          "#00ff00",
          ["==", ["get", "user_class_id"], 2],
          "#0000ff",
          "#ff0000",
        ],
        "fill-outline-color": "#3bb2d0",
        "fill-opacity": 0.5,
      },
    },
    {
      id: "gl-draw-polygon-fill-active",
      type: "fill",
      filter: ["all", ["==", "active", "true"], ["==", "$type", "Polygon"]],
      paint: {
        "fill-color": "#fbb03b",
        "fill-outline-color": "#fbb03b",
        "fill-opacity": 0.1,
      },
    },
    {
      id: "gl-draw-polygon-midpoint",
      type: "circle",
      filter: ["all", ["==", "$type", "Point"], ["==", "meta", "midpoint"]],
      paint: {
        "circle-radius": 3,
        "circle-color": "#fbb03b",
      },
    },
    {
      id: "gl-draw-polygon-stroke-inactive",
      type: "line",
      filter: [
        "all",
        ["==", "active", "false"],
        ["==", "$type", "Polygon"],
        ["!=", "mode", "static"],
      ],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#3bb2d0",
        "line-width": 2,
      },
    },
    {
      id: "gl-draw-polygon-stroke-active",
      type: "line",
      filter: ["all", ["==", "active", "true"], ["==", "$type", "Polygon"]],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#fbb03b",
        "line-dasharray": [0.2, 2],
        "line-width": 2,
      },
    },
    {
      id: "gl-draw-line-inactive",
      type: "line",
      filter: [
        "all",
        ["==", "active", "false"],
        ["==", "$type", "LineString"],
        ["!=", "mode", "static"],
      ],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#3bb2d0",
        "line-width": 4,
      },
    },
    {
      id: "gl-draw-line-active",
      type: "line",
      filter: ["all", ["==", "$type", "LineString"], ["==", "active", "true"]],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#fbb03b",
        "line-dasharray": [0.2, 2],
        "line-width": 4,
      },
    },
    {
      id: "gl-draw-polygon-and-line-vertex-stroke-inactive",
      type: "circle",
      filter: [
        "all",
        ["==", "meta", "vertex"],
        ["==", "$type", "Point"],
        ["!=", "mode", "static"],
      ],
      paint: {
        "circle-radius": 7,
        "circle-color": "#fff",
      },
    },
    {
      id: "gl-draw-polygon-and-line-vertex-inactive",
      type: "circle",
      filter: [
        "all",
        ["==", "meta", "vertex"],
        ["==", "$type", "Point"],
        ["!=", "mode", "static"],
      ],
      paint: {
        "circle-radius": 5,
        "circle-color": "#fbb03b",
      },
    },
    {
      id: "gl-draw-point-point-stroke-inactive",
      type: "circle",
      filter: [
        "all",
        ["==", "active", "false"],
        ["==", "$type", "Point"],
        ["==", "meta", "feature"],
        ["!=", "mode", "static"],
      ],
      paint: {
        "circle-radius": 7,
        "circle-opacity": 1,
        "circle-color": "#fff",
      },
    },
    {
      id: "gl-draw-point-inactive",
      type: "circle",
      filter: [
        "all",
        ["==", "active", "false"],
        ["==", "$type", "Point"],
        ["==", "meta", "feature"],
        ["!=", "mode", "static"],
      ],
      paint: {
        "circle-radius": 5,
        "circle-color": "#3bb2d0",
      },
    },
    {
      id: "gl-draw-point-stroke-active",
      type: "circle",
      filter: [
        "all",
        ["==", "$type", "Point"],
        ["==", "active", "true"],
        ["!=", "meta", "midpoint"],
      ],
      paint: {
        "circle-radius": 9,
        "circle-color": "#fff",
      },
    },
    {
      id: "gl-draw-point-active",
      type: "circle",
      filter: [
        "all",
        ["==", "$type", "Point"],
        ["!=", "meta", "midpoint"],
        ["==", "active", "true"],
      ],
      paint: {
        "circle-radius": 7,
        "circle-color": "#fbb03b",
      },
    },
    {
      id: "gl-draw-polygon-fill-static",
      type: "fill",
      filter: ["all", ["==", "mode", "static"], ["==", "$type", "Polygon"]],
      paint: {
        "fill-color": "#404040",
        "fill-outline-color": "#404040",
        "fill-opacity": 0.1,
      },
    },
    {
      id: "gl-draw-polygon-stroke-static",
      type: "line",
      filter: ["all", ["==", "mode", "static"], ["==", "$type", "Polygon"]],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#404040",
        "line-width": 2,
      },
    },
    {
      id: "gl-draw-line-static",
      type: "line",
      filter: ["all", ["==", "mode", "static"], ["==", "$type", "LineString"]],
      layout: {
        "line-cap": "round",
        "line-join": "round",
      },
      paint: {
        "line-color": "#404040",
        "line-width": 4,
      },
    },
    {
      id: "gl-draw-point-static",
      type: "circle",
      filter: ["all", ["==", "mode", "static"], ["==", "$type", "Point"]],
      paint: {
        "circle-radius": 7,
        "circle-color": "#404040",
      },
    },
  ],
};

// const mapboxView = [
//   {
//     id: "Áp thấp nhiệt đới",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Áp thấp nhiệt đới-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "ngay"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Đường đi của áp thấp",
//     type: "line",
//     paint: {
//       "line-color": "#540075",
//       "line-width": 2.5,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Bão",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Bão-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "ngay"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Đường đi của bão",
//     type: "line",
//     paint: {
//       "line-color": "#0d2eff",
//       "line-width": 2.5,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Lốc",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Lốc-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Tuyến sạt lở",
//     type: "line",
//     paint: {
//       "line-color": "#FF0000",
//       "line-width": 2,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Điểm sạt lở",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Điểm sạt lở-img",
//       "icon-size": 0.8,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Xâm nhập mặn (Độ mặn)",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Xâm nhập mặn (Độ mặn)-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "tentram"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Nắng nóng",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Nắng nóng-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "tentram"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Mưa",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Mưa-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "tentram"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Mực nước",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Mực nước-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "tentram"],
//       "text-offset": [0, 2.5],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Hồ chứa",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,

//       "text-color": "#000000",
//       "text-halo-color": "#ffffff",
//       "text-halo-width": 1.5,
//     },
//     layout: {
//       "icon-image": "Hồ chứa-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",

//       "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
//       "text-field": ["get", "ten"],
//       "text-offset": [0, 2.8],
//       "text-size": 10,
//       "text-anchor": "center",
//     },
//   },
//   {
//     id: "Hệ thống hồ chứa",
//     type: "line",
//     paint: {
//       "line-color": "#47a1d8",
//       "line-width": 2,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Cháy rừng tự nhiên",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Cháy rừng tự nhiên-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Kè",
//     type: "line",
//     paint: {
//       "line-color": "#852a2b",
//       "line-width": 2,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [4, 2],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Đê bao, bờ bao",
//     type: "line",
//     paint: {
//       "line-color": "#1E1623",
//       "line-width": 2,
//       "line-gap-width": 0,
//       "line-opacity": 1,
//       "line-dasharray": [2, 2],
//     },
//     layout: {
//       "line-join": "round",
//       "line-cap": "round",
//     },
//   },
//   {
//     id: "Cống, đập",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Cống, đập-img",
//       "icon-size": 0.7,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Mốc cảnh báo triều cường",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Mốc cảnh báo triều cường-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Biển cảnh báo sạt lở",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Biển cảnh báo sạt lở-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Khu neo đậu tàu thuyền",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Khu neo đậu tàu thuyền-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Vị trí xung yếu",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Vị trí xung yếu-img",
//       "icon-size": 0.8,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
//   {
//     id: "Vị trí an toàn",
//     type: "symbol",
//     paint: {
//       "icon-color": "transparent",
//       "icon-halo-color": "#FFFFFF",
//       "icon-halo-width": 0,
//     },
//     layout: {
//       "icon-image": "Vị trí an toàn-img",
//       "icon-size": 1,
//       "icon-anchor": "center",
//       "icon-allow-overlap": false,
//       "icon-ignore-placement": false,
//       "symbol-placement": "point",
//     },
//   },
// ];

export default styles;
