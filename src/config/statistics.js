const districts = [
  "760", "761", "764", "765", "766", "767", "768", "769",
  "770", "771", "772", "773", "774", "775", "776", "777", "778",
  "783", "784", "785", "786", "787",
];

const statistics = {
  "Áp thấp nhiệt đới": {
    type: "bar",
    features: [
      { id: "tongsoATND", label: "Số lượng áp thấp nhiệt đới", color: "" },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng áp thấp nhiệt đới" },
    xaxis: { name: "nam", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null"],
      arguments: [{ id: "detail", name: "Thống kê", for: ["null"] }],
    },
  },
  "Bão": {
    type: "bar",
    features: [
      { id: "tansuatxuathien", label: "Tần suất xuất hiện", color: "mamau" },
    ],
    colors: [],
    yaxis: { title: "Tần suất xuất hiện" },
    xaxis: { name: "capdobao", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null"],
      arguments: [{ id: "detail", name: "Thống kê", for: ["null"] }],
    },
  },
  "Lốc": {
    type: "bar",
    features: [{ id: "tongsoloc", label: "Số lượng lốc", color: "" }],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng lốc" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Kè": {
    type: "bar",
    features: [{ id: "tongchieudaike", label: "Chiều dài kè", color: "" }],
    colors: ["#258dde"],
    yaxis: { title: "Chiều dài kè" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "m" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Đê bao, bờ bao": {
    type: "bar",
    features: [
      {
        id: "tongchieudaidebaobobao",
        label: "Chiều dài đê bao, bờ bao",
        color: "",
      },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Chiều dài đê bao, bờ bao" },
    xaxis: { name: "donviquanly", alternate: "" },
    unit: { name: "", alternate: "m" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Mốc cảnh báo triều cường": {
    type: "bar",
    features: [
      {
        id: "tongsomoccanhbaotrieucuong",
        label: "Số lượng mốc cảnh báo triều cường",
        color: "",
      },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng mốc cảnh báo triều cường" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Biển cảnh báo sạt lở": {
    type: "bar",
    features: [
      {
        id: "tongsobiencanhbaosatlo",
        label: "Số lượng biển cảnh báo sạt lở",
        color: "",
      },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng biển cảnh báo sạt lở" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Khu neo đậu tàu thuyền": {
    type: "bar",
    features: [
      {
        id: "tongsokhuneodautauthuyen",
        label: "Số lượng khu neo đậu tàu thuyền",
        color: "",
      },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng khu neo đậu tàu thuyền" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Vị trí xung yếu": {
    type: "bar",
    features: [
      {
        id: "tongsovitrixungyeu",
        label: "Số lượng vị trí xung yếu",
        color: "",
      },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng vị trí xung yếu" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Vị trí an toàn": {
    type: "bar",
    features: [
      { id: "tongsovitriantoan", label: "Số lượng vị trí an toàn", color: "" },
    ],
    colors: ["#258dde"],
    yaxis: { title: "Số lượng vị trí an toàn" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Nắng nóng": {
    type: "line",
    features: [
      { id: "nhietdomax", label: "Cao nhất", color: "#ff0000" },
      { id: "nhietdotb", label: "Trung bình", color: "#55fe00" },
      { id: "nhietdomin", label: "Thấp nhất", color: "#ffff00" },
      { id: "nhietdocaonhat", label: "Cao nhất", color: "#ff0000" },
      { id: "nhietdotrungbinh", label: "Trung bình", color: "#55fe00" },
      { id: "nhietdothapnhat", label: "Thấp nhất", color: "#ffff00" },
    ],
    colors: [],
    yaxis: { title: "Nhiệt độ" },
    xaxis: { name: "ngay", alternate: "nam" },
    unit: { name: "", alternate: "°C" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        {
          id: "total",
          name: "Thống kê toàn thời gian",
          for: ["null", ...districts],
        },
        {
          id: "detail",
          name: "Thống kê theo từng năm",
          for: ["null", ...districts],
        },
      ],
    },
  },
  "Xâm nhập mặn (Độ mặn)": {
    type: "line",
    features: [
      { id: "doman", label: "Độ mặn", color: "#B8860B" },
      { id: "domancaonhat", label: "Cao nhất", color: "#ff0000" },
      { id: "domantrungbinh", label: "Trung bình", color: "#55fe00" },
      { id: "domanthapnhat", label: "Thấp nhất", color: "#ffff00" },
    ],
    colors: [],
    yaxis: { title: "Độ mặn" },
    xaxis: { name: "ngay", alternate: "nam" },
    unit: { name: "", alternate: "‰" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        {
          id: "total",
          name: "Thống kê toàn thời gian",
          for: ["null", ...districts],
        },
        {
          id: "detail",
          name: "Thống kê theo từng năm",
          for: ["null", ...districts],
        },
      ],
    },
  },
  "Mưa": {
    type: "line",
    features: [
      { id: "luongmua", label: "Lượng mưa", color: "#258dde" },
      { id: "luongmuacaonhat", label: "Cao nhất", color: "#ff0000" },
      { id: "luongmuatrungbinh", label: "Trung bình", color: "#55fe00" },
      { id: "luongmuathapnhat", label: "Thấp nhất", color: "#ffff00" },
    ],
    colors: [],
    yaxis: { title: "Lượng mưa" },
    xaxis: { name: "ngay", alternate: "nam" },
    unit: { name: "", alternate: "mm" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        {
          id: "total",
          name: "Thống kê toàn thời gian",
          for: ["null", ...districts],
        },
        {
          id: "detail",
          name: "Thống kê theo từng năm",
          for: ["null", ...districts],
        },
      ],
    },
  },
  "Mực nước": {
    type: "line",
    features: [
      { id: "docaodinhtrieu", label: "Độ cao đỉnh triều", color: "#ff0000" },
      { id: "docaochantrieu", label: "Độ cao chân triều", color: "#55fe00" },
      { id: "mucnuoccaonhat", label: "Cao nhất", color: "#ff0000" },
      { id: "mucnuoctrungbinh", label: "Trung bình", color: "#55fe00" },
      { id: "mucnuocthapnhat", label: "Thấp nhất", color: "#ffff00" },
    ],
    colors: [],
    yaxis: { title: "Độ cao" },
    xaxis: { name: "ngay", alternate: "nam" },
    unit: { name: "", alternate: "m" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        {
          id: "total",
          name: "Thống kê toàn thời gian",
          for: ["null", ...districts],
        },
        {
          id: "detail",
          name: "Thống kê theo từng năm",
          for: ["null", ...districts],
        },
      ],
    },
  },
  "Hồ chứa": {
    type: "line",
    features: [
      { id: "qvh", label: "Lưu lượng nước về hồ", color: "#ff0000" },
      { id: "qxa", label: "Lưu lượng xả", color: "#55fe00" },
      { id: "luuluongcaonhat", label: "Cao nhất", color: "#ff0000" },
      { id: "luuluongtrungbinh", label: "Trung bình", color: "#55fe00" },
      { id: "luuluongthapnhat", label: "Thấp nhất", color: "#ffff00" },
    ],
    colors: [],
    yaxis: { title: "Lưu lượng" },
    xaxis: { name: "ngay", alternate: "nam" },
    unit: { name: "", alternate: "m&#179;" },
    element: {},
    forms: {
      districts: ["null"],
      arguments: [
        { id: "total", name: "Thống kê toàn thời gian", for: ["null"] },
        { id: "detail", name: "Thống kê theo từng năm", for: ["null"] },
      ],
    },
  },
  "Thiệt hại do thiên tai": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Số lượng thiệt hại" },
    xaxis: { name: "tenhuyen", alternate: "phamvithongke" },
    unit: { name: "dvtthiethai", alternate: "" },
    element: { name: "doituongthiethai", value: "soluong", color: "mamau" },
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "total", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Tuyến sạt lở": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Chiều dài tuyến sạt lở" },
    xaxis: { name: "quan_huyen_tp", alternate: "phamvithongke" },
    unit: { name: "", alternate: "m" },
    element: { name: "mucdosatlo", value: "tongchieudaisatlo", color: "mamau" },
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "total", name: "Thống kê toàn bộ", for: ["null"] },
        { id: "detail", name: "Thống kê chi tiết", for: ["null"] },
        { id: "total", name: "Thống kê", for: [...districts] },
      ],
    },
  },
  "Điểm sạt lở": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Số lượng điểm sạt lở" },
    xaxis: { name: "quan_huyen_tp", alternate: "phamvithongke" },
    unit: { name: "", alternate: "" },
    element: { name: "mucdosatlo", value: "soluongvitrisatlo", color: "mamau" },
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "total", name: "Thống kê toàn bộ", for: ["null"] },
        { id: "detail", name: "Thống kê chi tiết", for: ["null"] },
        { id: "total", name: "Thống kê", for: [...districts] },
      ],
    },
  },
  "Cống, đập": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Số lượng cống, đập" },
    xaxis: { name: "donviquanly", alternate: "phamvithongke" },
    unit: { name: "", alternate: "" },
    element: {
      name: "capcongtrinh",
      value: "tongsocapcongtrinh",
      color: "mamau",
    },
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "total", name: "Thống kê toàn bộ", for: ["null"] },
        { id: "detail", name: "Thống kê chi tiết", for: ["null"] },
        { id: "total", name: "Thống kê", for: [...districts] },
      ],
    },
  },
  "Kế hoạch dự kiến di dời, sơ tán dân": {
    type: "bar",
    features: [
      {
        id: "tongsonguoididoibao8_9",
        label: "Số lượng người di dời bão cấp 8 đến 9",
        color: "",
      },
      {
        id: "tongsohodidoibao8_9",
        label: "Số lượng hộ di dời bão cấp 8 đến 9",
        color: "",
      },
      {
        id: "tongsonguoididoibao10_13",
        label: "Số lượng người di dời bão cấp 10 đến 13",
        color: "",
      },
      {
        id: "tongsohodidoibao10_13",
        label: "Số lượng hộ di dời bão cấp 10 đến 13",
        color: "",
      },
    ],
    colors: ["#FFD700", "#FF0000", "#FFA500", "#B22222"],
    yaxis: { title: "Số lượng người - hộ di dời" },
    xaxis: { name: "quan_huyen_tp", alternate: "" },
    unit: { name: "", alternate: "" },
    element: {},
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "detail", name: "Thống kê", for: ["null", ...districts] },
      ],
    },
  },
  "Kế hoạch lực lượng dự kiến huy động": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Số lượng huy động" },
    xaxis: { name: "qhtp", alternate: "phamvithongke" },
    unit: { name: "", alternate: "" },
    element: { name: "tenlucluong", value: "tongcong" },
    forms: {
      districts: ["null", ...districts],
      arguments: [
        { id: "total", name: "Thống kê toàn bộ", for: ["null"] },
        { id: "detail", name: "Thống kê chi tiết", for: ["null"] },
        { id: "total", name: "Thống kê", for: [...districts] },
      ],
    },
  },
  "Kế hoạch phương tiện, trang thiết bị dự kiến huy động": {
    type: "column",
    features: [],
    colors: [],
    yaxis: { title: "Số lượng phương tiện, trang thiết bị" },
    xaxis: { name: "donviquanly", alternate: "" },
    unit: { name: "", alternate: "" },
    element: { name: "tenphuongtienttb", value: "soluongphuongtienttb" },
    forms: {
      districts: ["null"],
      arguments: [
        { id: "total", name: "Thống kê toàn bộ", for: ["null"] },
        { id: "detail", name: "Thống kê chi tiết", for: ["null"] },
      ],
    },
  },
};

export default statistics;
