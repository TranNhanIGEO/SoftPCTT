import { removeValDuplicates } from "src/tools/removeDuplicates";

export const layerVisualization = ({ data, name, argument, shape }) => {
  const arrPos = data.map((dt) => dt[name]);
  const merPos = removeValDuplicates(arrPos);
  const avgPos = merPos
    .map((pos) => {
      const arrVal = data.filter((dt) => dt[name] === pos);
      const argVal = arrVal.map((dt) => Number(dt[argument]));
      const sumVal = argVal.reduce((acc, val) => acc + val);
      const lengthVal = arrVal?.length;
      const val = sumVal / lengthVal;
      const shapeVal = JSON.parse(arrVal[0][shape]);
      const [lon, lat] = shapeVal?.coordinates;
      const objValues = { lon, lat, val };
      return objValues;
    })
    .filter((dt) => {
      return dt.lon !== -Infinity && dt.lat !== -Infinity;
    });
  return avgPos;
};
