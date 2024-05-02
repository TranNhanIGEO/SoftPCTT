export const convertQuery = (obj) => {
  return new URLSearchParams(obj).toString();
};
