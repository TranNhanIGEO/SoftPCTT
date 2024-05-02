import { Fragment } from "react";
import { Button, ButtonGroup } from "src/components/interfaces/Button";
import config from "src/config";

const LayerStatistic = ({
  pageServices,
  currentLayer,
  currentDistrict,
  isDisabled,
  onClick,
}) => {
  const configServices = pageServices[currentLayer];
  const configStatistics = config.statistics[currentLayer];

  return (
    <Fragment>
      {configServices?.statisticApi &&
        configStatistics?.forms?.districts?.includes(currentDistrict) && (
          <ButtonGroup>
            {configStatistics?.forms?.arguments
              ?.filter((f) => f.for?.includes(currentDistrict))
              ?.map((f) => (
                <Button
                  outline
                  key={f.name}
                  value={f.id}
                  disabled={isDisabled}
                  onClick={onClick}
                >
                  {f.name}
                </Button>
              ))}
          </ButtonGroup>
        )}
    </Fragment>
  );
};

export default LayerStatistic;
