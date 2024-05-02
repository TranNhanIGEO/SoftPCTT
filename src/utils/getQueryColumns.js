import config from "src/config";

export const getQueryColumns = (data, currentLayer, isFullZone) => {
  const configColumns = config.columns[currentLayer];
  const configTables = config.table;

  if (!data?.length) return;
  const columnName = Object.keys(data[0]);
  const label = (column) => {
    const thisColumn = configColumns?.find((col) => col.accessorKey === column);
    return thisColumn?.header;
  };
  const columns = columnName
    ?.filter(
      (column) =>
        !configTables.idColumn.includes(column) &&
        (isFullZone || column !== process.env.REACT_APP_DISTRICTIDCOLUMN)
    )
    ?.map((column) => ({
      id: column.toUpperCase(),
      label: label(column),
    }));

  return columns;
};
