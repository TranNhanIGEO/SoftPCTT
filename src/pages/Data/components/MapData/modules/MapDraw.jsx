import "@mapbox/mapbox-gl-draw/dist/mapbox-gl-draw.css";
import { useControl } from "react-map-gl";
import mapboxgl from "mapbox-gl";
import MapboxDraw from "@mapbox/mapbox-gl-draw";
import config from "src/config";
import {
  Fragment,
  forwardRef,
  useCallback,
  useEffect,
  useImperativeHandle,
  useRef,
} from "react";

const controls = {
  Point: {
    options: {
      point: true,
      line_string: false,
      polygon: false,
      trash: true,
      combine_features: false,
      uncombine_features: false,
    },
    limit: 1,
    actions: (map, draw, coordinates) => {
      draw.deleteAll();
      map.flyTo({ center: coordinates, zoom: 15 });
    },
  },
  LineString: {
    options: {
      point: false,
      line_string: true,
      polygon: false,
      trash: true,
      combine_features: false,
      uncombine_features: false,
    },
    limit: 1,
    actions: (map, draw, coordinates) => {
      draw.deleteAll();
      const bounds = new mapboxgl.LngLatBounds();
      coordinates.forEach((coord) => bounds.extend(coord));
      if (bounds?.isEmpty()) return;
      map.fitBounds(bounds, { maxZoom: 15 });
    },
  },
  MultiLineString: {
    options: {
      point: false,
      line_string: true,
      polygon: false,
      trash: true,
      combine_features: true,
      uncombine_features: true,
    },
    limit: undefined,
    actions: (map, draw, coordinates) => {
      const bounds = new mapboxgl.LngLatBounds();
      coordinates.forEach((coords) =>
        coords.forEach((coord) => bounds.extend(coord))
      );
      if (bounds?.isEmpty()) return;
      map.fitBounds(bounds, { maxZoom: 15 });
    },
  },
};

const DrawControl = forwardRef((props, ref) => {
  const mapboxDraw = useControl(
    () => new MapboxDraw(props),
    ({ map }) => {
      map.on("draw.create", props.onCreate);
      map.on("draw.update", props.onUpdate);
      map.on("draw.delete", props.onDelete);
      map.on("draw.modechange", props.onModeChange);
      map.on("draw.combine", props.onCombine);
      map.on("draw.uncombine", props.onUncombine);
    },
    ({ map }) => {
      map.off("draw.create", props.onCreate);
      map.off("draw.update", props.onUpdate);
      map.off("draw.delete", props.onDelete);
      map.off("draw.modechange", props.onModeChange);
      map.off("draw.combine", props.onCombine);
      map.off("draw.uncombine", props.onUncombine);
    },
    {
      position: props.position,
    }
  );
  useImperativeHandle(ref, () => mapboxDraw, [mapboxDraw]);
  return <Fragment />;
});

const MapDraw = ({ map, shape, onShow, onUpdate, onDelete, onMerge }) => {
  const drawRef = useRef();

  useEffect(() => {
    if (!map.current || !shape.coordinates?.length) return;
    const {id, ...geometry} = shape;
    const feature = { type: "Feature", id, geometry, properties: {} };
    const {coordinates} = geometry;
    const actions = controls[shape?.type]?.actions;
    actions(map.current, drawRef.current, coordinates);
    drawRef.current.add(feature);
    onShow(feature)
  }, [map, shape, onShow]);

  const handleUpdate = useCallback((e) => {
    const { features } = e;
    const [feature] = features;
    onUpdate(feature)
  }, [onUpdate]);

  const handleDelete = useCallback((e) => {
    const { features } = e;
    const [feature] = features;
    onDelete(feature)
  }, [onDelete]);

  const handleMerge = useCallback((e) => {
    const { deletedFeatures, createdFeatures } = e;
    onMerge(deletedFeatures, createdFeatures)
  }, [onMerge])

  const handleModeChange = useCallback(() => {
    const layers = drawRef.current.getAll();
    const features = layers.features;
    const totalFeatures = features.length;
    const limitFeatures = controls[shape?.type]?.limit;
    if (totalFeatures <= limitFeatures || !limitFeatures) return;
    drawRef.current.changeMode("simple_select");
    alert(`Bạn chỉ có thể thêm ${limitFeatures} đối tượng cho 1 thuộc tính!`);
  }, [shape])
  
  return (
    <DrawControl
      position="top-right"
      ref={drawRef}
      styles={config.styles.mapboxDraw}
      displayControlsDefault={false}
      controls={controls[shape?.type]?.options}
      onCreate={handleUpdate}
      onUpdate={handleUpdate}
      onDelete={handleDelete}
      onModeChange={handleModeChange}
      onCombine={handleMerge}
      onUncombine={handleMerge}
    />
  );
};

export default MapDraw;
