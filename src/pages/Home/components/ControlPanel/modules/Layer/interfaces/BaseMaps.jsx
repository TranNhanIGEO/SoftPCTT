import config from "src/config";
import Form from "src/components/interfaces/Form";

const BaseMaps = ({ allLayer, onChange }) => {
  const configServices = config.services.basemaps;

  return (
    <ul className="basemap-list">
      {Object.keys(configServices).map((maps) => (
        <li key={maps}>
          <Form.Check
            type="checkbox"
            key={maps}
            id={maps}
            label={maps}
            value={maps}
            onChange={onChange}
            checked={allLayer.includes(maps)}
            className={"basemap-checkbox"}
          />
        </li>
      ))}
    </ul>
  );
};

export default BaseMaps;
