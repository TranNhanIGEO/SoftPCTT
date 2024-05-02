import { img } from "src/assets";

const statistic = {
  "Áp thấp nhiệt đới": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsoATND", header: "Tổng số áp thấp nhiệt đới" },
    { accessorKey: "tenATND", header: "Tên áp thấp" },
  ],
  "Bão": [
    { accessorKey: "capdobao", header: "Cấp độ bão" },
    { accessorKey: "tansuatxuathien", header: "Tần suất xuất hiện" },
    { accessorKey: "phantramcapdobao", header: "Tỉ lệ (%)" },
    { accessorKey: "tencacconbao", header: "Tên các cơn bão" },
  ],
  "Lốc": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsoloc", header: "Tổng số lốc xoáy" },
  ],
  "Tuyến sạt lở": [
    { accessorKey: "mucdosatlo", header: "Mức độ sạt lở" },
    { accessorKey: "tongchieudaisatlo", header: "Tổng chiều dài sạt lở" },
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
  ],
  "Điểm sạt lở": [
    { accessorKey: "mucdosatlo", header: "Mức độ sạt lở" },
    { accessorKey: "soluongvitrisatlo", header: "Số vị trí sạt lở" },
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
  ],
  "Xâm nhập mặn (Độ mặn)": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tencactramdoman", header: "Tên trạm đo mặn" },
    { accessorKey: "domanthapnhat", header: "Độ mặn thấp nhất" },
    { accessorKey: "domancaonhat", header: "Độ mặn cao nhất" },
    { accessorKey: "domantrungbinh", header: "Độ mặn trung bình" },
    { accessorKey: "thoidiemdomanthapnhat", header: "Thời điểm độ mặn thấp nhất" },
    { accessorKey: "thoidiemdomancaonhat", header: "Thời điểm độ mặn cao nhất" },
  ],
  "Nắng nóng": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tentramdonhietdo", header: "Tên trạm đo nhiệt độ" },
    { accessorKey: "nhietdothapnhat", header: "Nhiệt độ thấp nhất (°C)" },
    { accessorKey: "nhietdocaonhat", header: "Nhiệt độ cao nhất (°C)" },
    { accessorKey: "nhietdotrungbinh", header: "Nhiệt độ trung bình (°C)" },
    { accessorKey: "thoidiemnhietdocaonhat", header: "Thời điểm nhiệt độ cao nhất" },
    { accessorKey: "thoidiemnhietdothapnhat", header: "Thời điểm nhiệt độ thấp nhất" },
  ],
  "Mưa": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tentramdomua", header: "Tên trạm đo mưa" },
    { accessorKey: "luongmuathapnhat", header: "Lượng mưa thấp nhất (mm)" },
    { accessorKey: "luongmuacaonhat", header: "Lượng mưa cao nhất (mm)" },
    { accessorKey: "luongmuatrungbinh", header: "Lượng mưa trung bình (mm)" },
    { accessorKey: "thoidiemluongmuathapnhat", header: "Thời điểm lượng mưa thấp nhất" },
    { accessorKey: "thoidiemluongmuacaonhat", header: "Thời điểm lượng mưa cao nhất" },
  ],
  "Mực nước": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tentramdomucnuoc", header: "Tên trạm đo mực nước" },
    { accessorKey: "mucnuocthapnhat", header: "Mực nước thấp nhất" },
    { accessorKey: "mucnuoccaonhat", header: "Mực nước cao nhất" },
    { accessorKey: "mucnuoctrungbinh", header: "Mực nước trung bình" },
    { accessorKey: "thoidiemmucnuocthapnhat", header: "Thời điểm mực nước thấp nhất" },
    { accessorKey: "thoidiemmucnuoccaonhat", header: "Thời điểm mực nước cao nhất" },
  ],
  "Hồ chứa": [
    { accessorKey: "nam", header: "Năm" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tencachochua", header: "Tên hồ chứa" },
    { accessorKey: "thongso", header: "Thông số" },
    { accessorKey: "luuluongthapnhat", header: "Lưu lượng thấp nhất" },
    { accessorKey: "luuluongcaonhat", header: "Lưu lượng cao nhất" },
    { accessorKey: "luuluongtrungbinh", header: "Lưu lượng trung bình" },
    { accessorKey: "thoidiemluuluongthapnhat", header: "Thời điểm lưu lượng thấp nhất" },
    { accessorKey: "thoidiemluuluongcaonhat", header: "Thời điểm lưu lượng cao nhất" },
  ],
  "Kè": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsophantu", header: "Tổng số phần tử" },
    { accessorKey: "tongchieudaike", header: "Tổng chiều dài kè"},
  ],
  "Đê bao, bờ bao": [
    { accessorKey: "donviquanly", header: "Đơn vị quản lý" },
    { accessorKey: "tongsophantutheodvql", header: "Tổng số phần tử theo đơn vị quản lý" },
    { accessorKey: "tongchieudaidebaobobao", header: "Tổng chiều dài đê bao bờ bao" },
  ],
  "Cống, đập": [
    { accessorKey: "capcongtrinh", header: "Cấp công trình" },
    { accessorKey: "donviquanly", header: "Đơn vị quản lý" },
    { accessorKey: "tongsocapcongtrinh", header: "Tổng số cống đập theo cấp công trình" },
    { accessorKey: "tongchieudaitheocapcongtrinh", header: "Tổng chiều dài cống đập theo cấp công trình" },
  ],
  "Mốc cảnh báo triều cường": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsomoccanhbaotrieucuong", header: "Tổng số mốc cảnh báo triều cường" },
  ],
  "Biển cảnh báo sạt lở": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tentuyensr", header: "Tên tuyến sông, rạch" },
    { accessorKey: "tongsobiencanhbaosatlo", header: "Tổng số biển cảnh báo sạt lở" },
  ],
  "Khu neo đậu tàu thuyền": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsokhuneodautauthuyen", header: "Tổng số khu neo đậu tàu thuyền" },
  ],
  "Thiệt hại do thiên tai": [
    { accessorKey: "doituongthiethai", header: "Đối tượng thiệt hại" },
    { accessorKey: "soluong", header: "Số lượng" },
  ],
  "Vị trí xung yếu": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsovitrixungyeu", header: "Tổng số vị trí xung yếu" },
    { accessorKey: "vitrixungyeutheophuongan", header: "Vị trí xung yếu theo phương án" },
  ],
  "Vị trí an toàn": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsovitriantoan", header: "Tổng số vị trí an toàn" },
    { accessorKey: "vitriantoantheophuongan", header: "Vị trí an toàn theo phương án" },
  ],
  "Kế hoạch dự kiến di dời, sơ tán dân": [
    { accessorKey: "quan_huyen_tp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tongsohodidoibao8_9", header: "Tổng số hộ dân di dời khi có bão cấp 8-9" },
    { accessorKey: "tongsonguoididoibao8_9", header: "Tổng số người dân di dời khi có bão cấp 8-9" },
    { accessorKey: "tongsohodidoibao10_13", header: "Tổng số hộ dân di dời khi có bão cấp 10-13" },
    { accessorKey: "tongsonguoididoibao10_13", header: "Tổng số người dân di dời khi có bão cấp 10-13" },
  ],
  "Kế hoạch lực lượng dự kiến huy động": [
    { accessorKey: "qhtp", header: "Quận/Huyện/TP. Thủ Đức" },
    { accessorKey: "tenlucluong", header: "Tên lực lượng" },
    { accessorKey: "thanhpho", header: "Thành phố" },
    { accessorKey: "quanhuyen", header: "Quận/huyện" },
    { accessorKey: "phuongxathitran", header: "Phường/xã/thị trấn" },
    { accessorKey: "apkhupho", header: "Ấp/khu phố" },
    { accessorKey: "tongcong", header: "Tổng" },
  ],
  "Kế hoạch phương tiện, trang thiết bị dự kiến huy động": [
    { accessorKey: "tenphuongtienttb", header: "Tên phương tiện, trang thiết bị" },
    { accessorKey: "soluongphuongtienttb", header: "Số lượng phương tiện, trang thiết bị" },
    { accessorKey: "donvitinh", header: "Đơn vị tính" },
    { accessorKey: "donviquanly", header: "Đơn vị quản lý" },
  ],
};

const columns = {
  "Áp thấp nhiệt đới": [
    ...statistic["Áp thấp nhiệt đới"],
    { accessorKey: "tenapthap", header: "Tên áp thấp", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "apsuat", header: "Áp suất", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tocdogio", header: "Tốc độ gió (kt)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "ngaybatdau", header: "Ngày bắt đầu", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "ngayketthuc", header: "Ngày kết thúc", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "centerid", header: "Trung tâm phát bão", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tenvn", header: "Tên Việt Nam", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "kvahhcm", header: "Ảnh hưởng trực tiếp TPHCM", size: 120, input: { type: "text", format: "" } }, 
  ],
  "Bão": [
    ...statistic["Bão"],
    { accessorKey: "tenbao", header: "Tên bão", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120,  input: { type: "date", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120,  input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "apsuat", header: "Áp suất", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tocdogio", header: "Tốc độ gió (kt)", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "capbao", header: "Cấp bão", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "ngaybatdau", header: "Ngày bắt đầu", size: 120,  input: { type: "date", format: "" } },
    { accessorKey: "ngayketthuc", header: "Ngày kết thúc", size: 120,  input: { type: "date", format: "" } },
    { accessorKey: "centerid", header: "Trung tâm phát bão", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tenvn", header: "Tên Việt Nam", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "kvahhcm", header: "Ảnh hưởng trực tiếp TPHCM", size: 120,  input: { type: "text", format: "" } }, 
  ],
  "Lốc": [
    ...statistic["Lốc"],
    { accessorKey: "tenlocxoay", header: "Tên lốc", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "apsuat", header: "Áp suất", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tocdogio", header: "Tốc độ gió (kt)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Tuyến sạt lở": [
    ...statistic["Tuyến sạt lở"],
    { accessorKey: "vitri", header: "Vị trí", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tuyensong", header: "Tuyến sông", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "capsong", header: "Cấp sông", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chieurong", header: "Chiều rộng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "mucdo", header: "Mức độ sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tinhtrang", header: "Tình trạng sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "anhhuong", header: "Ảnh hưởng của sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "khoangcachah", header: "Khoảng cách ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "ditichah", header: "Diện tích ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "sohoah", header: "Số hộ dân bị ảnh hưởng", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "songuoiah", header: "Số người bị ảnh hưởng", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "hatangah", header: "Hạ tầng bị ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "congtrinhchongsl", header: "Công trình chống sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chudautu", header: "Chủ đầu tư", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tenduan", header: "Tên dự án", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "quymoduan", header: "Quy mô dự án", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tongmucduan", header: "Tổng mức đầu tư dự án", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "tiendothuchien", header: "Tiến độ thực hiện", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "nguongoc", header: "Nguồn", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "dubao", header: "Dự báo", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "ctxdke", header: "Công trình xây dựng kè", size: 120,  input: { type: "text", format: "" } },
  ],
  "Điểm sạt lở": [
    ...statistic["Điểm sạt lở"],
    { accessorKey: "vitri", header: "Vị trí", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tuyensong", header: "Tuyến sông", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "capsong", header: "Cấp sông", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chieurong", header: "Chiều rộng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "mucdo", header: "Mức độ sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tinhtrang", header: "Tình trạng sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "anhhuong", header: "Ảnh hưởng của sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "khoangcachah", header: "Khoảng cách ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "ditichah", header: "Diện tích ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "sohoah", header: "Số hộ dân bị ảnh hưởng", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "songuoiah", header: "Số người bị ảnh hưởng", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "hatangah", header: "Hạ tầng bị ảnh hưởng", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "congtrinhchongsl", header: "Công trình chống sạt lở", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "chudautu", header: "Chủ đầu tư", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tenduan", header: "Tên dự án", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "quymoduan", header: "Quy mô dự án", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "tongmucduan", header: "Tổng mức đầu tư dự án", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "tiendothuchien", header: "Tiến độ thực hiện", size: 120,  input: { type: "text", format: "" },},
    { accessorKey: "nguongoc", header: "Nguồn", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "dubao", header: "Dự báo", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120,  input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120,  input: { type: "number", format: "" } },
    { accessorKey: "ctxdke", header: "Công trình xây dựng kè", size: 120,  input: { type: "text", format: "" } },
  ],
  "Xâm nhập mặn (Độ mặn)": [
    ...statistic["Xâm nhập mặn (Độ mặn)"],
    { accessorKey: "tentram", header: "Tên trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "tensong", header: "Tên sông", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "doman", header: "Độ mặn (‰)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
  ],
  "Nắng nóng": [
    ...statistic["Nắng nóng"],
    { accessorKey: "tentram", header: "Tên trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "captram", header: "Cấp trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitritram", header: "Vị trí trạm", size: 120, input: { type: "text", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "thang", header: "Tháng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "sogionang", header: "Số giờ nắng trong tháng", size: 220, input: { type: "text", format: "" } },
    { accessorKey: "nhietdomin", header: "Nhiệt độ thấp nhất (°C)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "nhietdomax", header: "Nhiệt độ cao nhất (°C)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "nhietdotb", header: "Nhiệt độ trung bình (°C)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
  ],
  "Mưa": [
    ...statistic["Mưa"],
    { accessorKey: "tentram", header: "Tên trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "captram", header: "Cấp trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitritram", header: "Vị trí trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "luongmua", header: "Lượng mưa (mm)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
  ],
  "Mực nước": [
    ...statistic["Mực nước"],
    { accessorKey: "tentram", header: "Tên trạm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    // { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    // { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "mucnuoc", header: "Mực nước (m)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "docaodinhtrieu", header: "Độ cao đỉnh triều (m)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "docaochantrieu", header: "Độ cao chân triều (m)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "baodongi", header: "Mức báo động cấp I", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "baodongii", header: "Mức báo động cấp II", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "baodongiii", header: "Mức báo động cấp III", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
  ],
  "Hồ chứa": [
    ...statistic["Hồ chứa"],
    { accessorKey: "ten", header: "Tên", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "loaiho", header: "Loại hồ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "h", header: "Độ cao mực nước hồ (m)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "w", header: "Thể tích nước ở hồ (.10^6m3)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "qvh", header: "Lưu lượng nước về hồ (m3/s)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "qxa", header: "Lưu lượng xả tràn (m3/s)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "qcsi", header: "Lưu lượng nước trung bình (m3/s)", size: 120, input: { type: "text", format: "" },},
    { accessorKey: "qcsii", header: "Lưu lượng cửa số 2 (m3/s)", size: 120, input: { type: "text", format: "" },},
    // { accessorKey: "qcsiii", header: "Lưu lượng cửa số 3 (m3/s)", size: 120, input: { type: "text", format: "" },},
    // { accessorKey: "qtb", header: "Lưu lượng nước trung bình (m3/s)", size: 120, input: { type: "text", format: "" },},
    { accessorKey: "bh", header: "Bốc hơi", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "r", header: "Lượng mưa đo ở hồ (mm)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Cháy rừng tự nhiên": [
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "diadiem", header: "Địa điểm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "tgchay", header: "Thời gian cháy", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tgdap", header: "Thời gian dập tắt", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "dtchay", header: "Diện tích cháy (ha)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hientrang", header: "Hiện trạng", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Kè": [
    ...statistic["Kè"],
    { accessorKey: "tenkenhmuong", header: "Tên kênh mương", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhdaykenh", header: "Cao trình đáy kênh", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongkenh", header: "Bề rộng kênh", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hesomai", header: "Hệ số mái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhbotrai", header: "Cao trình bờ trái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhbophai", header: "Cao trình bờ phải", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongbotrai", header: "Bề rộng bờ trái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongbophai", header: "Bề rộng bờ phải", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hanhlangbaove", header: "Hành lang bảo vệ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "capcongtrinh", header: "Cấp công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "cumcongtrinh", header: "Cụm công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ketcaucongtrinh", header: "Kết cấu công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "muctieunhiemvu", header: "Mục tiêu nhiệm vụ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "diadiem", header: "Địa điểm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hethongcttl", header: "Hệ thống CTTL", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "donviquanly", header: "Đơn vị quản lý", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namsudung", header: "Năm sử dụng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Đê bao, bờ bao": [
    ...statistic["Đê bao, bờ bao"],
    { accessorKey: "tenkenhmuong", header: "Tên kênh mương", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhdaykenh", header: "Cao trình đáy kênh", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongkenh", header: "Bề rộng kênh", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hesomai", header: "Hệ số mái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhbotrai", header: "Cao trình bờ trái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhbophai", header: "Cao trình bờ phải", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongbotrai", header: "Bề rộng bờ trái", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berongbophai", header: "Bề rộng bờ phải", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hanhlangbaove", header: "Hành lang bảo vệ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "capcongtrinh", header: "Cấp công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ketcaucongtrinh", header: "Kết cấu công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "muctieunhiemvu", header: "Mục tiêu nhiệm vụ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "diadiem", header: "Địa điểm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hethongcttl", header: "Hệ thống CTTL", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "donviquanly", header: "Đơn vị quản lý", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namsudung", header: "Năm sử dụng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Cống, đập": [
    ...statistic["Cống, đập"],
    { accessorKey: "tencongdap", header: "Tên cống đập", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "lytrinh", header: "Lý trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "cumcongtrinh", header: "Cụm công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "goithau", header: "Gói thầu", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "loaicongtrinh", header: "Loại công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hinhthuc", header: "Hình thức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "duongkinh", header: "Đường kính", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "berong", header: "Bề rộng", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "chieucao", header: "Chiều cao", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "socua", header: "Số cửa", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "caotrinhdaycong", header: "Cao trình đáy cống", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "caotrinhdinhcong", header: "Cao trình đỉnh cống", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hinhthucvanhanh", header: "Hình thức vận hành", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "muctieunhiemvu", header: "Mục tiêu nhiệm vụ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "diadiem", header: "Địa điểm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "capcongtrinh", header: "Cấp công trình", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "hethongcttl", header: "Hệ thống CTTL", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "donviquanly", header: "Đơn vị quản lý", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namsudung", header: "Năm sử dụng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Mốc cảnh báo triều cường": [
    ...statistic["Mốc cảnh báo triều cường"],
    { accessorKey: "tenmoc", header: "Tên mốc", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "giatri", header: "Giá trị", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vitri", header: "Vị trí", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namsudung", header: "Năm sử dụng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Biển cảnh báo sạt lở": [
    ...statistic["Biển cảnh báo sạt lở"],
    { accessorKey: "sohieubien", header: "Số hiệu biển báo", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tuyensr", header: "Tên tuyến sông, rạch", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "toadox", header: "Tọa Độ X", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "toadoy", header: "Tọa Độ Y", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "vitrisatlo", header: "Vị trí sạt lở", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "phamvi", header: "Phạm vi", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namxaydung", header: "Năm xây dựng", size: 120, input: { type: "number", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
    {
      accessorKey: "hinhanh",
      header: "Hình ảnh (.jpg, .jpeg, .png)",
      size: 120,
      input: { type: "file", format: "image/png, image/jpeg" },
      Cell: ({ cell }) => (
        <img
          src={`${process.env.REACT_APP_DOMAIN}${
            process.env.REACT_APP_API_GET_WARNINGSIGNDATA
          }/${"photo"}/${cell.getValue("Hình ảnh (.jpg, .jpeg, .png)")}`}
          alt=""
          width="100px"
          heigth="100px"
          onClick={(e) => e.target.requestFullscreen()}
        />
      ),
    },
  ],
  "Khu neo đậu tàu thuyền": [
    ...statistic["Khu neo đậu tàu thuyền"],
    { accessorKey: "ten", header: "Tên", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "diachi", header: "Địa chỉ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "kinhdo", header: "Kinh độ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vido", header: "Vĩ độ", size: 120, input: { type: "text", format: "" } }, 
    { accessorKey: "dosaunuoc", header: "Độ sâu vùng nước đậu tàu (m)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "succhua", header: "Sức chứa", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "coloaitau", header: "Cỡ, loại tàu được vào khu neo đậu", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "vitrivl", header: "Vị trí bắt đầu vào luồng", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "huongluong", header: "Hướng luồng", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "chieudai", header: "Chiều dài", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "sdt", header: "Số điện thoại", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "tansoll", header: "Tần số liên lạc", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Thiệt hại do thiên tai": [
    ...statistic["Thiệt hại do thiên tai"],
    { accessorKey: "loaithientai", header: "Loại thiên tai", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "doituongthiethai", header: "Đối tượng thiệt hại", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "motathiethai", header: "Mô tả thiệt hại", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "dvtthiethai", header: "Đơn vị tính thiệt hại", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "soluong", header: "Số lượng", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "giatrithiethai", header: "Giá trị thiệt hại (đồng)", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "diadiem", header: "Địa điểm", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "gio", header: "Giờ", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "ngay", header: "Ngày (dd/mm/yyyy)", size: 120, input: { type: "date", format: "" } },
    { accessorKey: "maxa", header: "Phường/xã", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "mahuyen", header: "Quận/Huyện/TP. Thủ Đức", size: 120, input: { type: "text", format: "" } },
    { accessorKey: "namcapnhat", header: "Năm cập nhật", size: 120, input: { type: "number", format: "" } },
  ],
  "Vị trí xung yếu": [
    ...statistic["Vị trí xung yếu"],
    {
      accessorKey: "vitri",
      header: "Vị trí",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "toadox",
      header: "Tọa Độ X",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "toadoy",
      header: "Tọa Độ Y",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "sodan",
      header: "Số dân",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "maxa",
      header: "Mã xã",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "phuongan",
      header: "Phương án",
      size: 120,
      input: { type: "text", format: "" },
    },
  ],
  "Vị trí an toàn": [
    ...statistic["Vị trí an toàn"],
    {
      accessorKey: "vitri",
      header: "Vị trí",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "toadox",
      header: "Tọa Độ X",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "toadoy",
      header: "Tọa Độ Y",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "succhua",
      header: "Sức chứa",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "maxa",
      header: "Mã xã",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "phuongan",
      header: "Phương án",
      size: 120,
      input: { type: "text", format: "" },
    },
  ],
  "Hướng di chuyển sơ tán dân": [
    {
      accessorKey: "chieudai",
      header: "Chiều dài (m)",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "tenhuong",
      header: "Hướng",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "khuvuc",
      header: "Khu vực",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
  ],
  "Kế hoạch dự kiến di dời, sơ tán dân": [
    ...statistic["Kế hoạch dự kiến di dời, sơ tán dân"],
    {
      accessorKey: "sovb",
      header: "Số văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "ngayvb",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "loaivb",
      header: "Loại văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "quanhuyen",
      header: "Quận/huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "sophuongdidan",
      header: "Số phường có dân cần di dời, sơ tán",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "sohocandidoi",
      header: "Số hộ cần di dời (không phân biệt cấp bão)",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "soho_bao8_9",
      header: "Số hộ dân di dời, sơ tán có bão cấp 8-9",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "songuoi_bao8_9",
      header: "Số người di dời, sơ tán khi có bão cấp 8-9",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "soho_bao10_13",
      header: "Số hộ dân di dời, sơ tán có bão cấp 10-13",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "songuoi_bao10_13",
      header: "Số người di dời, sơ tán khi có bão cấp 10-13",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
  ],
  "Kế hoạch lực lượng dự kiến huy động": [
    ...statistic["Kế hoạch lực lượng dự kiến huy động"],
    {
      accessorKey: "sovb",
      header: "Số văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "ngayvb",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "loaivb",
      header: "Loại văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "qhtp",
      header: "Quận/Huyện/TP. Thủ Đức",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "tenlucluong",
      header: "Tên lực lượng",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "capql",
      header: "Cấp quản lý",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "slnguoihd",
      header: "Số lượng người huy động",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
    { 
      accessorKey: "namsudung", 
      header: "Năm sử dụng", 
      size: 120,
      input: { type: "number", format: "" }, 
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
  ],
  "Kế hoạch phương tiện, trang thiết bị dự kiến huy động": [
    ...statistic["Kế hoạch phương tiện, trang thiết bị dự kiến huy động"],
    {
      accessorKey: "tenphuongtienttb",
      header: "Tên phương tiện, trang thiết bị",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dvql",
      header: "Đơn vị quản lý",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "soluong",
      header: "Số lượng",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dvt",
      header: "Đơn vị tính",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "sovb",
      header: "Số văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "ngayvb",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "loaivb",
      header: "Loại văn bản",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
    {
      accessorKey: "phannhom1",
      header: "Phân loại thiết bị nhóm 1",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "phannhom2",
      header: "Phân loại thiết bị nhóm 2",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "phannhom3",
      header: "Phân loại thiết bị nhóm 3",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
  ],
  "Danh bạ điện thoại": [
    {
      accessorKey: "quanhuyen",
      header: "Quận/huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "hoten",
      header: "Họ tên",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "cvcoquan",
      header: "Chức vụ cơ quan",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "cvbch",
      header: "Chức vụ ban chấp hành",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dtcoquan",
      header: "SĐT cơ quan",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dtdidong",
      header: "SĐT di động",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "fax",
      header: "Fax",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
  ],
  "Thông tin lưu trữ tư liệu hình ảnh": [
    {
      accessorKey: "tenhinhanh",
      header: "Tên hình ảnh (.jpg, .jpeg, .png)",
      size: 120,
      input: { type: "file", format: "image/png, image/jpeg" },
      Cell: ({ cell }) => (
        <img
          src={`${process.env.REACT_APP_DOMAIN}${
            process.env.REACT_APP_API_GET_PHOTODATAMATERIAL
          }/${"photo"}/${cell.getValue("Tên hình ảnh (.jpg, .jpeg, .png)")}`}
          alt=""
          width="100%"
          heigth="200px"
          onClick={(e) => e.target.requestFullscreen()}
        />
      ),
    },
    {
      accessorKey: "ngayhinhanh",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "noidung",
      header: "Nội dung",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "diadiem",
      header: "Địa điểm",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dvql",
      header: "Đơn vị quản lý",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "nguongoc",
      header: "Nguồn gốc",
      size: 120,
      input: { type: "text", format: "" },
      Cell: ({ cell }) => {
        const isOrtherMaterial = cell.getValue("Nguồn gốc")?.includes("https");
        return isOrtherMaterial ? (
          <a
            href={cell.getValue("Nguồn gốc")}
            target="_blank"
            rel="noopener noreferrer"
          >
            <span>{cell.getValue("Nguồn gốc")}</span>
          </a>
        ) : (
          <span>{cell.getValue("Nguồn gốc")}</span>
        );
      },
    },
    {
      accessorKey: "maxa",
      header: "Mã xã",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
  ],
  "Thông tin lưu trữ tư liệu video": [
    {
      accessorKey: "tenvideo",
      header: "Tên video (.mp4, .mov)",
      size: 120,
      input: { type: "file", format: "video/mp4, video/quicktime, .mov, .mp4" },
      Cell: ({ cell }) => (
        <video controls width="100%" heigth="200px">
          <source
            type="video/mp4"
            src={`${process.env.REACT_APP_DOMAIN}${
              process.env.REACT_APP_API_GET_VIDEODATAMATERIAL
            }/${"media"}/${cell.getValue("Tên video (.mp4, .mov)")}`}
          />
          <source
            type="video/mov"
            src={`${process.env.REACT_APP_DOMAIN}${
              process.env.REACT_APP_API_GET_VIDEODATAMATERIAL
            }/${"media"}/${cell.getValue("Tên video (.mp4, .mov)")}`}
          />
        </video>
      ),
    },
    {
      accessorKey: "ngayvideo",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "noidung",
      header: "Nội dung",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "diadiem",
      header: "Địa điểm",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dvql",
      header: "Đơn vị quản lý",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "nguongoc",
      header: "Nguồn gốc",
      size: 120,
      input: { type: "text", format: "" },
      Cell: ({ cell }) => {
        const isOrtherMaterial = cell.getValue("Nguồn gốc")?.includes("https");
        return isOrtherMaterial ? (
          <a
            href={cell.getValue("Nguồn gốc")}
            target="_blank"
            rel="noopener noreferrer"
          >
            <span>{cell.getValue("Nguồn gốc")}</span>
          </a>
        ) : (
          <span>{cell.getValue("Nguồn gốc")}</span>
        );
      },
    },
    {
      accessorKey: "maxa",
      header: "Mã xã",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
  ],
  "Thông tin lưu trữ tư liệu khác": [
    {
      accessorKey: "tentulieu",
      header: "Tên tư liệu (.pdf)",
      size: 120,
      input: { type: "file", format: "application/pdf, .pdf" },
      Cell: ({ cell }) => (
        <a
          href={`${process.env.REACT_APP_DOMAIN}${
            process.env.REACT_APP_API_GET_ORTHERDATAMATERIAL
          }/${"pdf"}/${cell.getValue("Tên tư liệu (.pdf)")}`}
          target="blank"
          rel="noopener noreferrer"
          style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}
        >
          {cell.getValue("Tên tư liệu (.pdf)") && (
            <img width="32px" height="32px" src={img.pdfImg} alt="" />
          )}
          <span>{cell.getValue("Tên tư liệu (.pdf)")}</span>
        </a>
      ),
    },
    {
      accessorKey: "ngaytulieu",
      header: "Ngày",
      size: 120,
      input: { type: "date", format: "" },
    },
    {
      accessorKey: "noidung",
      header: "Nội dung",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "diadiem",
      header: "Địa điểm",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "dvql",
      header: "Đơn vị quản lý",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "nguongoc",
      header: "Nguồn gốc",
      size: 120,
      input: { type: "text", format: "" },
      Cell: ({ cell }) => {
        const isOrtherMaterial = cell.getValue("Nguồn gốc")?.includes("https");
        return isOrtherMaterial ? (
          <a
            href={cell.getValue("Nguồn gốc")}
            target="_blank"
            rel="noopener noreferrer"
          >
            <span>{cell.getValue("Nguồn gốc")}</span>
          </a>
        ) : (
          <span>{cell.getValue("Nguồn gốc")}</span>
        );
      },
    },
    {
      accessorKey: "maxa",
      header: "Mã xã",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "mahuyen",
      header: "Mã huyện",
      size: 120,
      input: { type: "text", format: "" },
    },
    {
      accessorKey: "namcapnhat",
      header: "Năm cập nhật",
      size: 120,
      input: { type: "number", format: "" },
    },
  ],
  account: [
    { accessorKey: "username", header: "Tài khoản", size: 120 },
    { accessorKey: "fullname", header: "Tên người dùng", size: 120 },
    { accessorKey: "email", header: "Email", size: 120 },
    { accessorKey: "phone", header: "Số điện thoại", size: 120 },
    { accessorKey: "unit", header: "Đơn vị", size: 120 },
    { accessorKey: "department", header: "Phòng ban", size: 120 },
  ],
  history: [
    { accessorKey: "rowid", header: "Mã", size: 120 },
    { accessorKey: "username", header: "Tài khoản cập nhật", size: 120 },
    { accessorKey: "operation", header: "Phương thức", size: 120 },
    { accessorKey: "operationdate", header: "Ngày cập nhật", size: 120 },
    { accessorKey: "olddata", header: "Dữ liệu gốc", size: 120 },
    { accessorKey: "changedata", header: "Dữ liệu thay đổi", size: 120 },
  ],
};

export default columns;
