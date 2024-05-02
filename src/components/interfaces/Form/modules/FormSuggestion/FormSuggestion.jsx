import "./FormSuggestion.css";
import { useCallback, useMemo, useState } from "react";
import { convertLanguages } from "src/tools/convertLanguages";

const FormSuggestion = ({
  id,
  data,
  value,
  onChange,
  placeholder,
  disabled = false,
  ...ortherProps
}) => {
  const [isShowSuggestions, setIsShowSuggestions] = useState(false);

  const onChangeSuggestions = useCallback(
    (e) => {
      const { value } = e.target;
      onChange && onChange(id, value);
    },
    [id, onChange]
  );

  const onClickSuggestion = useCallback(
    (val) => {
      setIsShowSuggestions(false);
      onChange && onChange(id, val);
    },
    [id, onChange]
  );

  const onFocusSuggestion = useCallback(() => {
    setIsShowSuggestions(true);
  }, []);

  const onBlurSuggestion = useCallback(() => {
    setTimeout(() => setIsShowSuggestions(false), 150);
  }, []);

  const ListValueSuggestion = useMemo(() => {
    if (!isShowSuggestions) return [];
    return data
      ?.filter((val) =>
        convertLanguages(val["name"] ?? val)
          .slice(0, value.length)
          .includes(convertLanguages(value))
      )
      ?.slice(0, 50)
      ?.map((val) => (
        <button
          key={val["value"] ?? val}
          onClick={() => onClickSuggestion(val["value"] ?? val)}
        >
          {val["name"] ?? val}
        </button>
      ));
  }, [onClickSuggestion, isShowSuggestions, value, data]);

  return (
    <div className="form-suggestion">
      <input
        className="form-suggestion-control"
        id={id}
        name={id}
        value={value}
        placeholder={placeholder}
        disabled={disabled}
        onChange={(e) => onChangeSuggestions(e)}
        onKeyUp={() => onFocusSuggestion()}
        onFocus={() => onFocusSuggestion()}
        onBlur={() => onBlurSuggestion()}
        autoComplete="off"
        {...ortherProps}
      />
      <div className="form-suggestion-list">{ListValueSuggestion}</div>
    </div>
  );
};

export default FormSuggestion;
