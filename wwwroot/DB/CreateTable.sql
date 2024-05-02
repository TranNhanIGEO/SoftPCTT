-- DROP TABLE Color;
CREATE TABLE Color(
    doituongthiethai Character varying(50) PRIMARY KEY,
	dvtthiethai Character varying(50),
    color Character varying(7)
);

INSERT INTO Color(doituongthiethai) SELECT DISTINCT doituongthiethai FROM thiethai_thientai;
-- Đơn vị tính đối tượng thiệt hại:
-- Người, tài sản, cây xanh, công trình - số lượng
-- Nông nghiệp (tấn): tấn
-- Nông nghiệp (ha): ha
-- Đất (m): m
-- Đất (m²): m2

CREATE TABLE Member(
	MemberId Character varying(32) NOT NULL PRIMARY KEY,
	Username Character varying(32) NOT NULL,
	FullName Character varying(64) NOT NULL,
	Department Character varying(256) NOT NULL,
	Unit Character varying(256) NOT NULL,
	Phone Character varying(32) NOT NULL,
	Email Character varying(64) NOT NULL
);

-- DROP TABLE MemberPasswords;
CREATE TABLE MemberPasswords(
	MemberId Character varying(32) REFERENCES Member(MemberId),
	Password Character varying(256) NOT NULL,
	CreateDate Timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
	UpdateDate Timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
	IsDeleted Boolean DEFAULT 0::Boolean
);


-- DROP TABLE Roles;
CREATE TABLE Roles(
	RoleId INT NOT NULL PRIMARY KEY,
	RoleName CHARACTER VARYING(256) NOT NULL,
	MaHuyen CHARACTER VARYING(3)[]
);

INSERT INTO Roles(RoleId, RoleName, MaHuyen) VALUES(959610, 'Xem dữ liệu', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO Roles(RoleId, RoleName, MaHuyen) VALUES(305693, 'Xuất báo cáo', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO Roles(RoleId, RoleName, MaHuyen) VALUES(365778, 'Chỉnh sửa/cập nhật', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO Roles(RoleId, RoleName) VALUES(856531, 'Quản trị hệ thống');

-- DROP TABLE MemberRoles;
CREATE TABLE MemberRoles(
	MemberId Character varying(32) NOT NULL REFERENCES Member(MemberId),
	RoleId INT NOT NULL REFERENCES Roles(RoleId),
	MaHuyen Character varying(3) NOT NULL,
	MaLopDuLieu Character varying[] NOT NULL
);

INSERT INTO MemberRoles(MemberId, RoleId, MaHuyen, MaLopDuLieu) 
VALUES('54b7e9f472faeadeeed9032b86f15435', 959610, '771', ARRAY['959610785395764', '959610785696363', 'Cống đập', 'Hướng di chuyển']);
INSERT INTO MemberRoles(MemberId, RoleId, MaHuyen, MaLopDuLieu) 
VALUES('54b7e9f472faeadeeed9032b86f15435', 305693, '767', ARRAY['959610785395764', 'Điểm xung yếu', '959610785696363', 'Nắng nóng']);



-- select * from member
-- INSERT INTO MemberRoles(MemberId, roleId) VALUES('54b7e9f472faeadeeed9032b86f15435', ARRAY[172743, 316196, 876471, 700690])

-- select * from roles r, memberroles mr where r.roleid = any(mr.roleid)

-- INSERT INTO member VALUES(MD5(random()::Character varying)::Character varying, 'admin', 'Admin', 'Ban PCTT', 'Quận 12', '0335357497', 'truongtri7112000@gmail.com');
-- INSERT INTO memberpasswords(memberid, password) VALUES('54b7e9f472faeadeeed9032b86f15435', 'PJkJr+wlNU1VHa4hWQuybjjVPyFzuNPcPu5MBH56scHri4UQPjvnumE7MbtcnDYhTcnxSkL9ei/bhIVrylxEwg==');
-- INSERT INTO memberpasswords(memberid, password) VALUES('integer', sha512('123'::bytea));

-- DROP TABLE MemberLogins;
CREATE TABLE MemberLogins(
	Id Character varying(32) PRIMARY KEY,
	Username Character varying(20) NOT NULL,
	LoginDate Timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
	Isdeleted Boolean DEFAULT 0::Boolean
);

-- DROP TABLE LopDoiTuong;
CREATE TABLE LopDoiTuong(
	MaLopDoiTuong INT NOT NULL PRIMARY KEY,
	RoleId INT[] NOT NULL,
	NhomDuLieu Character varying(256),
	TenGocBang Character varying(256),
	LopDuLieu Character varying(256),
	MaHuyen CHARACTER VARYING(3)[]
);

INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(395764, ARRAY[959610, 305693], 'Thiên tai', 'ApThapNhietDoi', 'Áp thấp nhiệt đới', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(696363, ARRAY[959610, 305693], 'Thiên tai', 'Bao', 'Bão', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(400072, ARRAY[959610, 305693], 'Thiên tai', 'LocXoay', 'Lốc', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(522327, ARRAY[959610, 305693], 'Thiên tai', 'SatLo_Line', 'Tuyến sạt lở', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(946275, ARRAY[959610, 305693], 'Thiên tai', 'SatLo_Point', 'Điểm sạt lở', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(864770, ARRAY[959610, 305693], 'Thiên tai', 'DoMan', 'Xâm nhập mặn (Độ mặn)', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(436210, ARRAY[959610, 305693], 'Thiên tai', 'NangNong', 'Nắng nóng', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(240215, ARRAY[959610, 305693], 'Thiên tai', 'Mua', 'Mưa', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(234300, ARRAY[959610, 305693], 'Thiên tai', 'MucNuoc', 'Mực nước', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(395457, ARRAY[959610, 305693], 'Thiên tai', 'HoChua', 'Hồ chứa', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(345052, ARRAY[959610, 305693], 'Thiên tai', 'ChayRung', 'Cháy rừng tự nhiên', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(658774, ARRAY[959610, 305693], 'Công trình PCTT', 'Ke', 'Kè', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(650880, ARRAY[959610, 305693], 'Công trình PCTT', 'DeBao_BoBao', 'Đê bao, bờ bao', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(473727, ARRAY[959610, 305693], 'Công trình PCTT', 'CongDap', 'Cống, đập', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(978952, ARRAY[959610, 305693], 'Công trình PCTT', 'MocCanhBaoTrieuCuong', 'Mốc cảnh báo triều cường', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(292361, ARRAY[959610, 305693], 'Công trình PCTT', 'BienCanhBaoSatLo', 'Biển cảnh báo sạt lở', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(122965, ARRAY[959610, 305693], 'Công trình PCTT', 'KhuNeoDau', 'Khu neo đậu tàu thuyền', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(543566, ARRAY[959610, 305693, 365778], 'Thiệt hại do thiên tai', 'ThietHai_ThienTai', 'Thiệt hại do thiên tai', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(259516, ARRAY[959610, 305693, 365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'DiemXungYeu', 'Vị trí xung yếu', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(919588, ARRAY[959610, 305693, 365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'DiemAnToan', 'Vị trí an toàn', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(224197, ARRAY[959610, 305693, 365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'HuongDiChuyen', 'Hướng di chuyển sơ tán dân', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(699304, ARRAY[365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'DuKienDiDan', 'Kế hoạch dự kiến di dời, sơ tán dân', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(810214, ARRAY[365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'LucLuongHuyDong', 'Kế hoạch lực lượng dự kiến huy động', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(519937, ARRAY[365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'PhuongTienHuyDong', 'Kế hoạch phương tiện, trang thiết bị dự kiến huy động', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(413591, ARRAY[365778], 'Phương án, kế hoạch về phòng, tránh ứng phó', 'DanhBaDT', 'Danh bạ điện thoại', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(565171, ARRAY[365778], 'Tư liệu về công tác PCTT', 'TuLieuHinhAnh', 'Thông tin lưu trữ tư liệu hình ảnh', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(670693, ARRAY[365778], 'Tư liệu về công tác PCTT', 'TuLieuVideo', 'Thông tin lưu trữ tư liệu video', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);
INSERT INTO LopDoiTuong(MaLopDoiTuong, RoleId, NhomDuLieu, TenGocBang, LopDuLieu, MaHuyen) VALUES(775014, ARRAY[365778], 'Tư liệu về công tác PCTT', 'TuLieuKhac', 'Thông tin lưu trữ tư liệu khác', ARRAY['785', '787', '783', '784', '786', '760', '771', '772', '761', '769', '770', '773', '774', '775', '778', '776', '765', '777', '764', '768', '766', '767']);


-- select * from lopdoituongtemp
-- select * from roles
-- SELECT mahuyen, tenhuyen FROM rghuyen
-- SELECT * FROM LopDoiTuongTemp
-- delete from lopdoituongtemp

-- DROP TABLE LopDoiTuongTemp;
CREATE TABLE LopDoiTuongTemp(
	MaLopDuLieuTemp Text NOT NULL PRIMARY KEY,
	LopDuLieuTemp Character varying(256)
);

INSERT INTO LopDoiTuongTemp 
SELECT CONCAT(r.roleid, h.mahuyen, ldt.malopdoituong) AS LopDuLieuTemp, ldt.lopdulieu AS MaLopDuLieuTemp 
FROM roles r
JOIN rghuyen h ON h.mahuyen = ANY(r.mahuyen)
JOIN lopdoituong ldt ON h.mahuyen = ANY(ldt.mahuyen);

-- DROP TABLE QuanHuyenTemp;
CREATE TABLE QuanHuyenTemp(
	mahuyentemp Character varying(10),
	tenhuyentemp Character varying(20)
);
INSERT INTO QuanHuyenTemp 
SELECT CONCAT(r.roleid, h.mahuyen) AS MaHuyenTemp, h.tenhuyen AS tenhuyentemp 
FROM roles r
JOIN RgHuyen h ON h.mahuyen = ANY(r.mahuyen);

-- DROP TABLE RefreshToken;
CREATE TABLE RefreshToken(
	RefreshTokenId Character varying(32) NOT NULL PRIMARY KEY,
-- 	AccessToken Text,
-- 	MemberId Character varying(32) NOT NULL REFERENCES Member(MemberId),
	MemberLoginId Character varying(32) NOT NULL REFERENCES MemberLogins (Id),
	RefreshDate Timestamp without time zone DEFAULT NOW()::timestamp without time zone,
	Token Character varying(100) NOT NULL
);

-- DROP TABLE History;
CREATE TABLE History
(
    id character varying(32) NOT NULL PRIMARY KEY ,
	tablename character varying(32),
    rowid character varying(16),
    username character varying(32),
    operation character varying(50),
    operationdate timestamp without time zone DEFAULT now(),
    olddata json,
    changedata text
)

