export const polygonLyr = ({ name, color, opacity }) => {
  return {
    id: name,
    type: "fill",
    minZoom: 9,
    maxZoom: 18,
    paint: {
      "fill-color": color,
      "fill-opacity": opacity,
    },
    layout: {
      "visibility": "visible",
    },
  };
};

export const polylineLyr = ({ name, color, width, stroke, dash, opacity }) => {
  return {
    id: name,
    type: "line",
    minZoom: 9,
    maxZoom: 18,
    paint: {
      "line-color": color,
      "line-width": width,
      "line-gap-width": stroke,
      "line-opacity": opacity,
      'line-dasharray': dash,
    },
    layout: {
      "line-join": "round",
      "line-cap": "round",
      "visibility": "visible",
    },
  };
};

export const pointLyr = ({ name, color, radius, stroke, shadow }) => {
  return {
    id: name,
    type: "circle",
    minZoom: 9,
    maxZoom: 18,
    paint: {
      "circle-color": color,
      "circle-radius": radius,
      "circle-stroke-width": stroke,
      "circle-stroke-color": shadow,
    },
    layout: {
      "visibility": "visible",
    },
  };
};

export const symbolLyr = ({ name, image, size, placement }) => {
  return {
    id: name,
    type: "symbol",
    minZoom: 9,
    maxZoom: 18,
    paint: {
      "icon-color": "transparent",
      "icon-halo-color": "#FFFFFF",
      "icon-halo-width": 0,
    },
    layout: {
      "icon-image": image,
      "icon-size": size,
      "icon-anchor": "center",
      "icon-allow-overlap": false,
      "icon-ignore-placement": false,
      "visibility": "visible",
      "symbol-placement": placement ?? "point",
    },
  };
};

export const labelLyr = ({ name, attributes, color, shadow, size, offset }) => {
  return {
    id: name,
    type: "symbol",
    minZoom: 9,
    maxZoom: 18,
    layout: {
      "text-font": ["Open Sans Bold", "Arial Unicode MS Bold"],
      "text-field": ["get", ...attributes],
      "text-offset": offset,
      "text-size": size,
      "text-anchor": "center",
      "visibility": "visible",
    },
    paint: {
      "text-color": color,
      "text-halo-color": shadow,
      "text-halo-width": 1.5,
    },
  };
};
