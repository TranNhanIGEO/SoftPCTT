import { Attribute, Statistic } from "./";
import { AttributeTab, StatisticTab } from "./";

export const tabTables = [
  {
    value: "attributeTab",
    condition: ["isDesktop", "isMobile"],
    children: <AttributeTab />,
    content: <Attribute />,
  },
  {
    value: "statisticTab",
    condition: ["isDesktop"],
    children: <StatisticTab />,
    content: <Statistic />,
  },
];
