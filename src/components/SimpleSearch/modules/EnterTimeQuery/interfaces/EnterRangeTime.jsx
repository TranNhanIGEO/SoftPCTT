import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import "dayjs/locale/vi";
import { Fragment } from "react";
import { useSearchState } from "src/contexts/Search";

const EnterRangeTime = ({ timeType, onRange }) => {
  const { whereClause } = useSearchState();

  return (
    <Fragment>
      <div className="simple-value-enter">
        <h3>Từ</h3>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="vi">
          <DatePicker
            views={timeType.views}
            format={timeType.format}
            value={dayjs(whereClause[timeType.range.start] ?? "")}
            onChange={(val) => onRange(timeType.range.start, val)}
          />
        </LocalizationProvider>
      </div>
      <div className="simple-value-enter">
        <h3>Đến</h3>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="vi">
          <DatePicker
            views={timeType.views}
            format={timeType.format}
            value={dayjs(whereClause[timeType.range.end] ?? "")}
            onChange={(val) => onRange(timeType.range.end, val)}
          />
        </LocalizationProvider>
      </div>
    </Fragment>
  );
};

export default EnterRangeTime;
