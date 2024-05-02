export const findMinium = (arr, col) => {
  if (!arr.length) return;
  const minium = arr
    ?.filter((val) => {
      return val[col] !== null;
    })
    ?.reduce((acc, val) => {
      return acc[col] < val[col] ? acc : val;
    });
  return minium;
};

export const findMaximum = (arr, col) => {
  if (!arr.length) return;
  const maximum = arr
    ?.filter((val) => {
      return val[col] !== null;
    })
    ?.reduce((acc, val) => {
      return acc[col] > val[col] ? acc : val;
    });
  return maximum;
};
