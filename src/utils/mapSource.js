const getAttributes = (dt, attributes) => {
  if (!attributes) return {};
  const attribute = Object.fromEntries(
    attributes?.map((obj) => [obj, dt[obj]])
  );
  return attribute;
};

export const polygonSrc = ({ name, data, attributes }) => {
  const newData = data?.filter((dt) => dt.shape);
  const boundaries = newData?.map((dt) => ({
    type: "Feature",
    geometry: JSON.parse(dt.shape),
    properties: getAttributes(dt, attributes),
  }));
  const collection = {
    type: "FeatureCollection",
    features: boundaries,
  };
  const geojson = {
    type: "geojson",
    name: name,
    data: collection,
  };
  return geojson;
};

export const polylineSrc = ({ name, data, attributes }) => {
  const newData = data?.filter((dt) => dt.line || dt.shape);
  const lines = newData?.map((dt) => ({
    type: "Feature",
    geometry: JSON.parse(dt.line ?? dt.shape),
    properties: getAttributes(dt, attributes),
  }));
  const collection = {
    type: "FeatureCollection",
    features: lines,
  };
  const geojson = {
    type: "geojson",
    name: name,
    data: collection,
  };
  return geojson;
};

export const pointSrc = ({ name, data, attributes }) => {
  const newData = data?.filter((dt) => dt.shape || dt.centroid);
  const points = newData?.map((dt) => ({
    type: "Feature",
    geometry: JSON.parse(dt.shape ?? dt.centroid),
    properties: getAttributes(dt, attributes),
  }));
  const collection = {
    type: "FeatureCollection",
    features: points,
  };
  const geojson = {
    type: "geojson",
    name: name,
    data: collection,
  };
  return geojson;
};

export const setImageSrc = ({ name, image, map }) => {
  map?.loadImage(image, (error, image) => {
    if (map?.hasImage(name) || error) return;
    map?.addImage(name, image, { sdf: false });
  });
};
