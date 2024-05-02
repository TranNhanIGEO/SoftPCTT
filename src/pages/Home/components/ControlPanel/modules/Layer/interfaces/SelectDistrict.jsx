import Form from "src/components/interfaces/Form";

const SelectDistrict = ({ districtLists, currentDistrict, onChange }) => {
  return (
    <div className="select-district">
      <Form.Select
        value={currentDistrict ?? undefined}
        defaultValue="CHỌN QUẬN/HUYỆN"
        isSelected={!currentDistrict}
        onChange={onChange}
      >
        {districtLists?.map((r) => (
          <option
            key={r.districtIDOrigin}
            id={r.districtIDOrigin}
            value={r.districtIDOrigin}
          >
            {r.districtName}
          </option>
        ))}
      </Form.Select>
    </div>
  );
};

export default SelectDistrict;
