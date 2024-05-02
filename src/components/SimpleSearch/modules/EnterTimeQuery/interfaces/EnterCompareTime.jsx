import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import "dayjs/locale/vi";
import { Fragment } from "react";
import { useSearchState } from "src/contexts/Search";

const EnterCompareTime = ({ timeType, onCompare }) => {
  const { whereClause } = useSearchState();

  return (
    <Fragment>
      <div className="simple-value-enter">
        <h3>Thời gian</h3>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="vi">
          <DatePicker
            views={timeType.views}
            format={timeType.format}
            value={dayjs(whereClause[timeType.compare.only] ?? "")}
            onChange={(val) => onCompare(timeType.compare.only, val)}
          />
        </LocalizationProvider>
      </div>
    </Fragment>
  );
};

export default EnterCompareTime;
