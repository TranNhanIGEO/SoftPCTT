import {
  useSearchDispatch,
  useSearchState,
  setToggleSimpleSearch,
  setToggleAdvancedSearch,
} from "src/contexts/Search";

const useSearch = () => {
  const { whereClause } = useSearchState();
  const dispatch = useSearchDispatch();

  const onOpenSimpleSearch = () => {
    dispatch(setToggleSimpleSearch(true));
  };

  const onOpenAdvancedSearch = () => {
    dispatch(setToggleAdvancedSearch(true));
  };

  return {
    whereClause,
    onOpenSimpleSearch,
    onOpenAdvancedSearch,
  };
};

export default useSearch;
