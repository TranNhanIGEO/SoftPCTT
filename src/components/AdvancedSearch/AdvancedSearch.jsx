import "./AdvancedSearch.css";
import { useCallback, useEffect } from "react";
import Modal from "src/components/interfaces/Modal";
import { getQueryColumns } from "src/utils/getQueryColumns";
import { removeValDuplicates } from "src/tools/removeDuplicates";
import { EnterQuery, SelectColumn, SelectQuery } from "./modules";
import {
  useSearchState,
  useSearchDispatch,
  setToggleAdvancedSearch,
  setColumnQuery,
  setValueSuggests,
  setWhereClauses,
  resetValueSuggests,
  resetWhereClauses,
} from "src/contexts/Search";

const AdvancedSearch = ({ source, currentLayer, isFullZone, onSubmit }) => {
  const { visibleAdvancedSearch, whereClause } = useSearchState();
  const dispatch = useSearchDispatch();
  const data = source[currentLayer];

  const handleCloseModal = () => {
    dispatch(setToggleAdvancedSearch(false));
  };

  useEffect(() => {
    const columns = getQueryColumns(data, currentLayer, isFullZone);
    dispatch(setColumnQuery(columns));
  }, [data, currentLayer, isFullZone, dispatch]);

  const handleSelectColumn = useCallback(
    (e) => {
      const { name, value } = e.target;
      const preWhere = whereClause["where"] ?? "";
      dispatch(setWhereClauses({ [name]: preWhere + "a." + value }));
      if (!data?.length) return;
      const thisColumn = data.map((col) => col[value.toLowerCase()]);
      const thisValue = removeValDuplicates(thisColumn);
      const handledValue = thisValue
        ?.filter((val) => val !== null && val !== " - ")
        ?.sort();
      const wardColumn = process.env.REACT_APP_WARDIDCOLUMN;
      const districtColumn = process.env.REACT_APP_DISTRICTIDCOLUMN;
      const dateColumn = process.env.REACT_APP_DATECOLUMN;
      const realName = (val) => {
        const hasDate = value.toLowerCase() === dateColumn;
        if (hasDate) return val?.split("/")?.reverse()?.join("-");
        return val;
      };
      const realValue = (val) => {
        const isWardID = value.toLowerCase() === wardColumn;
        const isDistrictID = value.toLowerCase() === districtColumn;
        const hasDate = value.toLowerCase() === dateColumn;
        if (hasDate) return val?.split("/")?.reverse()?.join("-");
        if (isWardID || isDistrictID) return val?.slice(0, val?.indexOf(" - "));
        return val;
      };
      const valueSuggests = handledValue.map((val) => ({
        name: realName(val),
        value: realValue(val),
      }));
      dispatch(setValueSuggests({ [name]: valueSuggests }));
    },
    [data, whereClause, dispatch]
  );

  const handleSelectQuery = useCallback(
    (e) => {
      const { name, value } = e.target;
      const preWhere = whereClause["where"] ?? "";
      dispatch(setWhereClauses({ [name]: preWhere + value }));
    },
    [whereClause, dispatch]
  );

  const handleEnterQuery = useCallback(
    (e) => {
      const { name, value } = e.target;
      dispatch(setWhereClauses({ [name]: value }));
    },
    [dispatch]
  );

  const handleResetForm = useCallback(() => {
    dispatch(resetValueSuggests({}));
    dispatch(resetWhereClauses({}));
  }, [dispatch]);

  useEffect(() => {
    if (visibleAdvancedSearch) return;
    dispatch(resetValueSuggests({}));
    dispatch(resetWhereClauses({}));
  }, [visibleAdvancedSearch, dispatch]);

  return (
    <Modal isOpen={visibleAdvancedSearch} onClose={handleCloseModal}>
      <Modal.Header>TÌM KIẾM THÔNG TIN</Modal.Header>
      <Modal.Body>
        <div className="advancedsearch-form">
          <SelectColumn onDBClick={handleSelectColumn} />
          <SelectQuery onDBClick={handleSelectQuery} />
          <EnterQuery layer={currentLayer} onChange={handleEnterQuery} />
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

export default AdvancedSearch;
