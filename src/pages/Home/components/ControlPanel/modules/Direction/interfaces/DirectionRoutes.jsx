import config from "src/config";

const DirectionRoutes = ({ summary, steps, onClick }) => {
  return (
    <div className="directions-routes">
      <div className="directions-summary">
        <h1>Khoảng cách: {summary?.distance} km</h1>
        <span>Thời gian: {summary?.duration} phút</span>
      </div>
      <div className="directions-instructions">
        <div className="directions-instructions-wrapper">
          <ol className="directions-steps">
            {steps?.map((step, idx) => (
              <li
                key={idx}
                className="directions-step"
                onClick={() => onClick(step.maneuver.location)}
              >
                <span className={"directions-icon"}>
                  {config.arrows
                    .filter((icon) => icon.name === step.maneuver.modifier)
                    .map((icon) => icon.icon)}
                </span>
                <div className="directions-step-maneuver">
                  <span>{step.maneuver.instruction}</span>
                </div>
                <div className="directions-step-estimate">
                  <div className="directions-step-distance">
                    ~ {step.distance} km
                  </div>
                  <div className="directions-step-duration">
                    ~ {step.duration} phút
                  </div>
                </div>
              </li>
            ))}
          </ol>
        </div>
      </div>
    </div>
  );
};

export default DirectionRoutes;
