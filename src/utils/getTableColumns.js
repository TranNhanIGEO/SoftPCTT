import config from "src/config";

export const getTableColumns = (data, name) => {
  const thisTable = config.columns[name];
  // const arrColumns = typeof thisTable === "function" ? thisTable() : thisTable;
  const columnName = Object.keys(data[0]);
  const findColumn = (column, type) => {
    const thisColumn = thisTable?.find((col) => col.accessorKey === column);
    switch (type) {
      case "header":
        return thisColumn?.header;
      case "size":
        return thisColumn?.size;
      case "input":
        return thisColumn?.input;
      case "cell":
        return thisColumn?.Cell;
      default:
        throw new Error("Unknown column type");
    }
  };
  const columns = columnName
    ?.filter((column) => thisTable?.some((col) => col.accessorKey === column))
    ?.map((column) => ({
      accessorKey: column,
      header: findColumn(column, "header"),
      size: findColumn(column, "size"),
      input: findColumn(column, "input"),
      Cell: findColumn(column, "cell"),
    }));
  return columns;
};
