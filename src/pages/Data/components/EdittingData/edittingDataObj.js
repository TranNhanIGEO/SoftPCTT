import { v4 as uuidv4 } from "uuid";
import proj4 from "proj4";
import config from "src/config";
import { apiShowData, apiCreateData, apiUpdateData } from "src/services/home";
proj4.defs(config.projects);

export const typeCoords = [
  {
    type: [process.env.REACT_APP_LONGITUDE, process.env.REACT_APP_LATITUDE],
    value: "isLngLatCoords",
  },
  {
    type: [process.env.REACT_APP_X_COORDS, process.env.REACT_APP_Y_COORDS],
    value: "isXYCoords",
  },
];

export const coordActions = {
  isLngLatCoords: {
    get: (type, data) => {
      const lng = Number(data[process.env.REACT_APP_LONGITUDE]);
      const lat = Number(data[process.env.REACT_APP_LATITUDE]);
      const randomID = uuidv4().replaceAll("-", "");
      const lnglatCoords = [lng, lat];
      return { id: randomID, type: type, coordinates: lnglatCoords };
    },
    set: (newData, coordinates) => {
      const [lng, lat] = coordinates;
      newData[process.env.REACT_APP_LONGITUDE] = lng;
      newData[process.env.REACT_APP_LATITUDE] = lat;
      return newData;
    },
    reset: (newData) => {
      newData[process.env.REACT_APP_LONGITUDE] = String(0.0);
      newData[process.env.REACT_APP_LATITUDE] = String(0.0);
      return newData;
    },
  },
  isXYCoords: {
    get: (type, data) => {
      const xCoords = Number(data[process.env.REACT_APP_X_COORDS]);
      const yCoords = Number(data[process.env.REACT_APP_Y_COORDS]);
      const randomID = uuidv4().replaceAll("-", "");
      const coordinates = [xCoords, yCoords];
      const xyCoords = proj4("EPSG:9210", "EPSG:4326", coordinates);
      return { id: randomID, type: type, coordinates: xyCoords };
    },
    set: (newData, coordinates) => {
      const xyCoords = proj4("EPSG:4326", "EPSG:9210", coordinates);
      const [xCoords, yCoords] = xyCoords;
      newData[process.env.REACT_APP_X_COORDS] = xCoords;
      newData[process.env.REACT_APP_Y_COORDS] = yCoords;
      return newData;
    },
    reset: (newData) => {
      newData[process.env.REACT_APP_X_COORDS] = String(0.0);
      newData[process.env.REACT_APP_Y_COORDS] = String(0.0);
      return newData;
    },
  },
};

export const shapeActions = {
  Point: {
    show: (oldData) => {
      const newData = { ...oldData };
      delete newData.shape;
      return newData;
    },
    update: (oldData, coordinates) => {
      const newData = { ...oldData };
      const keys = Object.keys(newData);
      const isCoords = typeCoords.find((tcs) =>
        tcs.type.every((t) => keys.includes(t))
      );
      const setCoords = coordActions[isCoords?.value]?.set;
      return setCoords(newData, coordinates);
    },
    delete: (oldData) => {
      const newData = { ...oldData };
      const keys = Object.keys(newData);
      const isCoords = typeCoords.find((tcs) =>
        tcs.type.every((t) => keys.includes(t))
      );
      const resetCoords = coordActions[isCoords?.value]?.reset;
      return resetCoords(newData);
    },
  },
  LineString: {
    show: (oldData, id) => {
      const newData = { ...oldData };
      if (!newData.shape) return newData;
      const geometry = JSON.parse(newData.shape);
      delete newData.shape;
      const { coordinates } = geometry;
      newData[process.env.REACT_APP_COORDINATES] = {
        ...newData[process.env.REACT_APP_COORDINATES],
        [id]: `(${coordinates.map((coord) => coord?.join(" "))?.toString()})`,
      };
      return newData;
    },
    update: (oldData, coordinates, id) => {
      const newData = { ...oldData };
      newData[process.env.REACT_APP_COORDINATES] = {
        ...newData[process.env.REACT_APP_COORDINATES],
        [id]: `(${coordinates.map((coord) => coord?.join(" "))?.toString()})`,
      };
      return newData;
    },
    delete: (oldData, id) => {
      const newData = { ...oldData };
      delete newData[process.env.REACT_APP_COORDINATES][id];
      return newData;
    },
  },
  MultiLineString: {
    show: (oldData, id) => {
      const newData = { ...oldData };
      if (!newData.shape) return newData;
      const geometry = JSON.parse(newData.shape);
      delete newData.shape;
      const { coordinates: multiCoordinates } = geometry;
      const newCoords = multiCoordinates.map(
        (coordinates) =>
          `(${coordinates.map((coord) => coord?.join(" "))?.toString()})`
      );
      newData[process.env.REACT_APP_COORDINATES] = {
        ...newData[process.env.REACT_APP_COORDINATES],
        [id]: `${newCoords?.join(", ")}`,
      };
      return newData;
    },
    update: (oldData, coordinates, id) => {
      const newData = { ...oldData };
      const multiCoordinates = coordinates;
      const newCoords = multiCoordinates.map(
        (coordinates) =>
          `(${coordinates.map((coord) => coord?.join(" "))?.toString()})`
      );
      newData[process.env.REACT_APP_COORDINATES] = {
        ...newData[process.env.REACT_APP_COORDINATES],
        [id]: `${newCoords?.join(", ")}`,
      };
      return newData;
    },
    delete: (oldData, id) => {
      return shapeActions.LineString.delete(oldData, id);
    },
  },
};

export const mergeActions = (oldData, oldFeatures, newFeatures) => {
  let newData = { ...oldData };
  oldFeatures.forEach((feature) => {
    const { id, geometry } = feature;
    const { type } = geometry;
    const deleteShape = shapeActions[type]?.delete;
    newData = deleteShape(newData, id);
  });
  newFeatures.forEach((feature) => {
    const { id, geometry } = feature;
    const { type, coordinates } = geometry;
    const updateShape = shapeActions[type]?.update;
    newData = updateShape(newData, coordinates, id);
  });
  return newData;
};

export const edittingDataObj = {
  create: {
    title: "THÊM MỚI DỮ LIỆU",
    data: {
      onGet: ({ dataField }) => {
        if (!dataField?.length) return {};
        const newInfos = dataField.reduce((acc, col) => {
          acc[col.accessorKey] = "";
          return acc;
        }, {});
        return newInfos;
      },
    },
    submit: {
      text: "Thêm mới",
      onSubmit: apiCreateData,
    },
  },
  update: {
    title: "CHỈNH SỬA DỮ LIỆU",
    data: {
      onGet: async ({ thisAPI, id, axiosJWT }) => {
        const response = await apiShowData({ thisAPI, id, axiosJWT });
        return response;
      },
    },
    submit: {
      text: "Chỉnh sửa",
      onSubmit: apiUpdateData,
    },
  },
};
