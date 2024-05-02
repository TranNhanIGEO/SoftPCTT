export const initLineChart = {
  chart: {
    height: null,
    type: "spline",
  },
  accessibility: {
    enabled: false,
  },
  navigator: {
    enabled: true,
    height: 20,
  },
  rangeSelector: {
    enabled: false,
    allButtonsEnabled: true,
    selected: 1,
    inputEnabled: true,
  },
  title: {
    style: {
      color: "#2C2C2C",
      fontWeight: "bold",
      fontSize: "1.2rem",
    },
    text: "",
  },
  subtitle: {
    style: {
      color: "#2C2C2C",
      fontWeight: "bold",
      fontSize: "1.1rem",
    },
    text: "",
  },
  tooltip: {
    hideDelay: 150,
    style: {
      color: "#0A5064",
      fontWeight: "bold",
      fontSize: "1.2rem",
    },
  },
  xAxis: {
    categories: [],
    labels: {
      distance: 2,
      style: {
        color: "#2C2C2C",
        fontWeight: "bold",
        fontSize: "1rem",
      },
    },
  },
  yAxis: {
    title: {
      style: {
        color: "#2C2C2C",
        fontWeight: "bold",
        fontSize: "1.2rem",
      },
      text: "",
    },
    labels: {
      format: "",
      style: {
        color: "#2C2C2C",
        fontWeight: "bold",
        fontSize: "1.1rem",
      },
    },
  },

  credits: {
    enabled: false,
  },
  plotOptions: {
    spline: {
      marker: {
        radius: 4,
        lineColor: "#666666",
        lineWidth: 1,
      },
    },
  },
  series: [
    {
      name: "",
      data: [],
    },
    {
      name: "",
      data: [],
    },
    {
      name: "",
      data: [],
    },
  ],
  colors: [],
  lang: {
    noData: "Loading",
  },
  noData: {
    style: {
      fontWeight: "bold",
      fontSize: "15px",
      color: "#000000",
    },
  },
};
