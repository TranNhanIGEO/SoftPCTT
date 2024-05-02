import { Layer, Legend, Direction } from "./";
import { LayerTab, LegendTab, DirectionTab } from "./";

export const tabPanels = [
  {
    value: "layerTab",
    condition: ["isDesktop", "isMobile"],
    children: <LayerTab />,
    content: <Layer />,
  },
  {
    value: "directionTab",
    condition: ["isDesktop"],
    children: <DirectionTab />,
    content: <Direction />,
  },
  {
    value: "legendTab",
    condition: ["isDesktop", "isMobile"],
    children: <LegendTab />,
    content: <Legend />,
  },
];
