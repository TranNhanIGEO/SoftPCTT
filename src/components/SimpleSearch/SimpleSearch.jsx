import "./SimpleSearch.css";
import { useCallback, useEffect } from "react";
import Modal from "src/components/interfaces/Modal";
import config from "src/config";
import { getQueryColumns } from "src/utils/getQueryColumns";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import {
  SelectColumn,
  SelectTime,
  EnterFirstQuery,
  EnterSecondQuery,
  EnterTimeQuery,
} from "./modules";
import {
  useSearchDispatch,
  useSearchState,
  setToggleSimpleSearch,
  setColumnQuery,
  setValueSuggests,
  setWhereClauses,
  resetValueSuggests,
  resetWhereClauses,
} from "src/contexts/Search";

const SimpleSearch = ({ source, currentLayer, isFullZone, onSubmit }) => {
  const { visibleSimpleSearch, columnQuery, whereClause } = useSearchState();
  const dispatch = useSearchDispatch();
  const data = source[currentLayer];
  const configTables = config.table;
  const hadDateColumn = columnQuery?.some((col) =>
    configTables.dateColumn.includes(col.id.toLowerCase())
  );

  const handleCloseModal = () => {
    dispatch(setToggleSimpleSearch(false));
  };

  useEffect(() => {
    const columns = getQueryColumns(data, currentLayer, isFullZone);
    dispatch(setColumnQuery(columns));
  }, [data, currentLayer, isFullZone, dispatch]);

  const handleSelectColumn = useCallback(
    (e) => {
      const { name, value } = e.target;
      dispatch(setWhereClauses({ ...whereClause, [name]: value }));
      if (!data?.length) return;
      const thisColumn = data.map((col) => col[value.toLowerCase()]);
      const thisValue = removeValDuplicates(thisColumn);
      const handledValue = thisValue
        ?.filter((val) => val !== null && val !== " - ")
        ?.sort();
      const wardColumn = process.env.REACT_APP_WARDIDCOLUMN;
      const districtColumn = process.env.REACT_APP_DISTRICTIDCOLUMN;
      const realValue = (val) => {
        const isWardID = value.toLowerCase() === wardColumn;
        const isDistrictID = value.toLowerCase() === districtColumn;
        if (isWardID || isDistrictID) return val.slice(0, val.indexOf(" - "));
        if (Number(val) || val === "0") return Number(val);
        return val;
      };
      const valueSuggests = handledValue.map((val) => ({
        name: val,
        value: realValue(val),
      }));
      dispatch(setValueSuggests({ [name]: valueSuggests }));
    },
    [data, whereClause, dispatch]
  );

  const handleEnterQuery = useCallback(
    (name, value) => {
      dispatch(setWhereClauses({ ...whereClause, [name]: value }));
    },
    [whereClause, dispatch]
  );

  const handleSelectTime = useCallback(
    (e) => {
      const { name, value } = e.target;
      const initVar = {
        startTime: null,
        endTime: null,
        onlyTime: null,
        aYear: null,
        aFewMonth: [],
      };
      dispatch(setWhereClauses({ ...whereClause, ...initVar, [name]: value }));
    },
    [whereClause, dispatch]
  );

  const handleResetForm = useCallback(() => {
    dispatch(resetValueSuggests({}));
    dispatch(resetWhereClauses({}));
  }, [dispatch]);

  useEffect(() => {
    if (visibleSimpleSearch) return;
    dispatch(resetValueSuggests({}));
    dispatch(resetWhereClauses({}));
  }, [visibleSimpleSearch, dispatch]);

  return (
    <Modal isOpen={visibleSimpleSearch} onClose={handleCloseModal}>
      <Modal.Header>TÌM KIẾM THÔNG TIN</Modal.Header>
      <Modal.Body>
        <div className="simple-search-form">
          <div className="simple-search-content">
            <div className="simple-var">
              <div className="simple-var-title">Tham số 1</div>
              <SelectColumn name="firstColumn" onChange={handleSelectColumn} />
              <EnterFirstQuery
                name="firstColumn"
                argument="firstWhere"
                onChange={handleEnterQuery}
              />
            </div>
            <div className="simple-var">
              <div className="simple-var-title">Tham số 2</div>
              <SelectColumn name="secondColumn" onChange={handleSelectColumn} />
              <EnterSecondQuery
                name="secondColumn"
                argument="secondWhere"
                onChange={handleEnterQuery}
              />
            </div>
            {hadDateColumn && (
              <div className="simple-var">
                <div className="simple-var-title">Thời gian</div>
                <SelectTime name="timeType" onChange={handleSelectTime} />
                <EnterTimeQuery name="timeType" />
              </div>
            )}
          </div>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Modal.Button type="button" onClick={handleResetForm}>
          Xóa
        </Modal.Button>
        <Modal.Button type="submit" onClick={onSubmit}>
          Tìm kiếm
        </Modal.Button>
      </Modal.Footer>
    </Modal>
  );
};

export default SimpleSearch;
