import "./Direction.css";
import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { DirectionNotes, DirectionRoutes } from "./interfaces";
import { img } from "src/assets";
import {
  allLayers,
  directionSystem,
  setCurrentRouteCoords,
} from "src/stores/home";

export const DirectionTab = () => {
  return (
    <Fragment>
      <img src={img.directionImg} alt="" />
      <span>Hệ thống dẫn đường</span>
    </Fragment>
  );
};

const Direction = () => {
  const dispatch = useDispatch();
  const allLayer = useSelector(allLayers);
  const directions = useSelector(directionSystem);
  const [steps, setSteps] = useState([]);
  const [summary, setSummary] = useState({});

  useEffect(() => {
    if (!directions?.routes) {
      setSummary({});
      setSteps([]);
      return;
    }
    const [routes] = directions.routes;
    const [legs] = routes.legs;
    const { distance, duration } = legs;
    const summary = {
      distance: (distance / 1000).toFixed(2),
      duration: (duration / 60).toFixed(2),
    };
    setSummary(summary);
    const legSteps = legs.steps;
    const steps = legSteps.map((step) => ({
      ...step,
      distance: (step.distance / 1000).toFixed(2),
      duration: (step.duration / 60).toFixed(2),
    }));
    setSteps(steps);
  }, [directions]);

  const handleFlyTo = (coords) => {
    dispatch(setCurrentRouteCoords(coords));
  };

  return (
    <div className="tab-direction">
      <div className="direction-title">
        <span>DẪN ĐƯỜNG</span>
      </div>
      <div className="direction-container">
        {!steps.length || !allLayer?.includes("Hướng di chuyển sơ tán dân") ? (
          <DirectionNotes />
        ) : (
          <DirectionRoutes
            summary={summary}
            steps={steps}
            onClick={handleFlyTo}
          />
        )}
      </div>
    </div>
  );
};

export default Direction;
