export const removeValDuplicates = (arr) => {
  return [...new Set(arr)];
};

export const removeObjDuplicates = (arr, col) => {
  return arr.filter(
    (objs, idx) => arr?.findIndex((obj) => objs[col] === obj[col]) === idx
  );
};
