import { Fragment } from "react";
import clsx from "clsx";
import Form from "src/components/interfaces/Form";
import { img } from "src/assets";

const VisualizationMaps = ({ group, allType, allLayer, onDrop, onChange }) => {
  return (
    <div
      key={group.groupID}
      className={clsx("thematicmap-according", {
        active: allType.includes(group.groupID.toString()),
      })}
    >
      <button
        className="thematicmap-according-title"
        value={group.groupID}
        onClick={onDrop}
      >
        <img src={img.mapslayerImg} alt="" />
        <span>{group.groupName}</span>
      </button>
      <div className="thematicmap-according-content">
        <ul className="thematicmap-according-list">
          <Maps
            data={group.layers}
            allLayer={allLayer}
            onChange={onChange}
          />
        </ul>
      </div>
    </div>
  );
};

export default VisualizationMaps;

const Maps = ({ data, allLayer, onChange }) => {
  return (
    <Fragment>
      {data?.map((l) => (
        <li key={l.layerID}>
          <Form.Check
            type="checkbox"
            className={"thematicmap-checkbox-item"}
            id={l.layerID}
            label={l.layerName}
            value={l.layerName}
            onChange={(e) => onChange(e, l.source)}
            checked={allLayer?.includes(l.layerName)}
          />
        </li>
      ))}
    </Fragment>
  );
};
