import { icon, line } from "src/assets";

const legends = {
  temparature: [
    { icon: "10°C", legend: "10°C" },
    { icon: "20°C", legend: "20°C" },
    { icon: "30°C", legend: "30°C" },
    { icon: "40°C", legend: "40°C" },
  ],
  salinity: [
    { icon: "0‰", legend: "0‰" },
    { icon: "10‰", legend: "10‰" },
    { icon: "20‰", legend: "20‰" },
  ],
  items: [
    { icon: line.districtLine, legend: "Ranh giới huyện" },
    { icon: line.trafficLine, legend: "Giao thông" },
    { icon: line.waterSystemLine, legend: "Thủy hệ" },
    { icon: icon.tropicalIcon, legend: "Tâm áp thấp nhiệt đới" },
    { icon: icon.stormIcon, legend: "Tâm bão" },
    { icon: line.depressionLine, legend: "Đường đi của áp thấp" },
    { icon: line.stormLine, legend: "Đường đi của bão" },
    { icon: icon.tornadoIcon, legend: "Lốc" },
    { icon: line.failureLine, legend: "Tuyến sạt lở" },
    { icon: icon.triangleIcon, legend: "Điểm sạt lở" },
    { icon: icon.circleIcon, legend: "Trạm quan trắc xâm nhập mặn (Độ mặn)" },
    { icon: icon.sunIcon, legend: "Trạm quan trắc nhiệt độ" },
    { icon: icon.rainIcon, legend: "Trạm quan trắc lượng mưa" },
    { icon: icon.waterLevelIcon, legend: "Trạm quan trắc mực nước" },
    { icon: icon.lakeIcon, legend: "Hồ chứa" },
    { icon: icon.fireIcon, legend: "Vị trí cháy rừng tự nhiên" },
    { icon: line.embankmentLine, legend: "Kè" },
    { icon: line.dikeLine, legend: "Đê bao, bờ bao" },
    { icon: icon.sewerIcon, legend: "Cống, đập" },
    { icon: icon.warningMarkIcon, legend: "Mốc cảnh báo triều cường" },
    { icon: icon.warningSignIcon, legend: "Biển cảnh báo sạt lở" },
    { icon: icon.anchorIcon, legend: "Khu neo đậu tàu thuyền" },
    { icon: icon.weakIcon, legend: "Vị trí xung yếu" },
    { icon: icon.safeIcon, legend: "Vị trí an toàn" },
    { icon: icon.startIcon, legend: "Điểm bắt đầu" },
    { icon: icon.endIcon, legend: "Điểm kết thúc" },
    { icon: line.directionLine, legend: "Hướng di chuyển sơ tán dân" },
  ],
};

export default legends;
