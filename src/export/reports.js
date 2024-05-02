import { currentTime } from "src/tools/currentTime";

export const reports = ({ type, data }) => {
  const subtitle = data.subtitle.text;
  const title = data.title.text;
  if (!data.report?.length) return;
  const source = data.report?.[0];
  if (!source) return;
  const report = source.map((dt) => {
    const data = { ...dt };
    const fields = Object.keys(data).reduce((acc, val) => {
      data[val] = data[val] ?? "";
      return { ...acc, [val]: data[val] };
    }, {});
    return fields;
  });
  const exportData = {
    isDetail: type === "detail",
    isTotal: type === "total",
    data: report,
    date: subtitle.toUpperCase(),
    time: currentTime(),
    district: title
      ?.slice(title.indexOf("- "), title.length)
      ?.replace("- ", "")
      ?.toUpperCase(),
  };
  return exportData;
};
