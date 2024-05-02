export const initColumnChart = {
  chart: {
    type: "column",
  },
  accessibility: {
    enabled: false,
  },
  title: {
    text: "",
    align: "center",
    style: {
      color: "#2C2C2C",
      fontWeight: "bold",
      fontSize: "1.3rem",
    },
  },
  subtitle: {
    text: "",
    align: "center",
    style: {
      color: "#2C2C2C",
      fontWeight: "bold",
      fontSize: "1.2rem",
    },
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
    crosshair: true,
    labels: {
      style: {
        color: "#2C2C2C",
        fontWeight: "bold",
        fontSize: "1.2rem",
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
      style: {
        color: "#2C2C2C",
        fontWeight: "bold",
        fontSize: "1.1rem",
      },
    },
  },
  legend: {
    enabled: false,
  },
  credits: {
    enabled: false,
  },
  plotOptions: {
    column: {
      pointPadding: 0.2,
      borderWidth: 0,
    },
    series: {
      states: {
        inactive: {
          opacity: 0.05,
        },
      },
    },
  },
  series: [
    {
      name: "",
      data: [],
    },
  ],
  colors: [],
};
