import MapboxDraw from "@mapbox/mapbox-gl-draw";
import { Fragment, useCallback, useState } from "react";
import { useControl } from "react-map-gl";
import "@mapbox/mapbox-gl-draw/dist/mapbox-gl-draw.css";

const DrawControl = (props) => {
  useControl(
    () => new MapboxDraw(props),
    ({ map }) => {
      map.on("draw.create", props.onCreate);
      map.on("draw.update", props.onUpdate);
      map.on("draw.delete", props.onDelete);
    },
    ({ map }) => {
      map.off("draw.create", props.onCreate);
      map.off("draw.update", props.onUpdate);
      map.off("draw.delete", props.onDelete);
    },
    {
      position: props.position,
    }
  );

  return <Fragment />;
};

const MapDraw = () => {
  const [features, setFeatures] = useState({});

  console.log(features);

  const onUpdate = useCallback((e) => {
    const { features } = e;
    setFeatures((prevFeatures) => {
      const newFeatures = { ...prevFeatures };
      for (const f of features) {
        newFeatures[f.id] = f;
      }
      return newFeatures;
    });
  }, []);

  const onDelete = useCallback((e) => {
    const { features } = e;
    setFeatures((prevFeatures) => {
      const newFeatures = { ...prevFeatures };
      for (const f of features) {
        delete newFeatures[f.id];
      }
      return newFeatures;
    });
  }, []);

  return (
    <DrawControl
      position="bottom-right"
      displayControlsDefault={false}
      onCreate={onUpdate}
      onUpdate={onUpdate}
      onDelete={onDelete}
      controls={{
        point: true,
        line_string: true,
        polygon: true,
        trash: true,
        combine_features: false,
        uncombine_features: false,
      }}
    />
  );
};

export default MapDraw;
