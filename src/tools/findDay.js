export const firstDay = (y, m) => {
  return new Date(y, m, 1).getDate();
};

export const lastDay = (y, m) => {
  return new Date(y, m + 1, 0).getDate();
};
