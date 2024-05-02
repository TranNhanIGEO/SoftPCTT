const handledTime = (time) => {
  const timeString = time.toString();
  if (timeString.length >= 2) return time;
  return "0" + time;
};

export const currentTime = () => {
  const today = new Date();
  const day = handledTime(today.getDate());
  const month = handledTime(today.getMonth() + 1);
  const year = handledTime(today.getFullYear());
  const hour = handledTime(today.getHours());
  const minute = handledTime(today.getMinutes());
  const second = handledTime(today.getSeconds());
  const date = day + "/" + month + "/" + year;
  const time = hour + ":" + minute + ":" + second;
  const dateTime = date + " " + time;
  return dateTime;
};
