-- DROP FUNCTION LoginMember;
CREATE FUNCTION LoginMember(
	Usr Character varying(20),
	Pwd Character varying(256)
)
RETURNS TABLE (memberid Character varying, username Character varying, fullname Character varying, department Character varying, unit Character varying, phone Character varying, email Character varying, isdeleted boolean)
AS $$
	BEGIN
		RETURN QUERY                             
			FROM member m, memberpasswords mp 
			WHERE m.memberid = mp.memberid
			AND m.username = Usr AND mp.password = Pwd;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM LoginMember('admin', '9rB7bBNA6Ue4Yd71+LCS2O5xCCbcVr0XW9yPOhawuKz4U8ZHhqcQ3t+dFSTWHjJQTifWDeFZrxELw5QUkHMVeA==')

-- DROP FUNCTION GetHuyens;
CREATE FUNCTION GetHuyens()
RETURNS TABLE (objectid integer, mahuyen character varying, tenhuyen character varying, dientichtunhien double precision, shape_length double precision, shape_area double precision, centroid text, shape text)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT h.objectid, h.mahuyen, h.tenhuyen, h.dientichtunhien, h.shape_length, h.shape_area, st_asgeojson(st_transform(st_centroid(h.shape), 4326)) AS centroid, st_asgeojson(st_transform(h.shape, 4326)) AS shape 
			FROM rghuyen h;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHuyens();

-- DROP FUNCTION GetHuyen;
CREATE FUNCTION GetHuyen(
	_mahuyen Character varying
)
RETURNS TABLE (objectid integer, mahuyen Character varying, tenhuyen character varying, dientichtunhien double precision, shape_length double precision, shape_area double precision, centroid text, shape text)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT h.objectid, h.mahuyen, h.tenhuyen, h.dientichtunhien, h.shape_length, h.shape_area, st_asgeojson(st_transform(st_centroid(h.shape), 4326)) AS centroid, st_asgeojson(st_transform(h.shape, 4326)) AS shape 
			FROM rghuyen h
			WHERE h.MaHuyen = _mahuyen;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHuyen('787');

-- DROP FUNCTION GetXas;
CREATE FUNCTION GetXas()
RETURNS TABLE (objectid integer, maxa character varying, tenxa character varying, mahuyen character varying, tenhuyen character varying, dientichtunhien double precision, shape_length double precision, shape_area double precision, centroid text, shape text)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT x.objectid, x.maxa, x.tenxa, x.mahuyen, x.tenhuyen, x.dientichtunhien, x.shape_length, x.shape_area, st_asgeojson(st_transform(st_centroid(x.shape), 4326)) AS centroid, st_asgeojson(st_transform(x.shape, 4326)) AS shape 
			FROM rgxa x
			ORDER BY x.objectid ASC;	  
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetXas();

-- DROP FUNCTION GetGiaoThongs;
CREATE FUNCTION GetGiaoThongs(
	_mahuyen Character varying
)
RETURNS TABLE (shape text)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT st_asgeojson(st_transform(gt.shape, 4326)) AS shape 
				FROM giaothong_pgon gt
				JOIN RgXa xa ON gt.IdXa::Character varying = xa.MaXa
				JOIN RgHuyen h ON xa.MaHuyen = h.MaHuyen
				AND h.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT st_asgeojson(st_transform(gt.shape, 4326)) AS shape 
				FROM giaothong_pgon gt;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetGiaoThongs('787');

-- DROP FUNCTION GetGiaoThong;
-- CREATE FUNCTION GetGiaoThong(
-- 	_mahuyen Character varying
-- )
-- RETURNS TABLE (shape text)
-- AS $$
-- 	BEGIN
-- 		RETURN QUERY	
-- 			SELECT st_asgeojson(st_transform(gt.shape, 4326)) AS shape 
-- 			FROM giaothong_pgon gt

-- 			JOIN rgxa xa ON gt.idxa::Character varying = xa.maxa
-- 			AND xa.mahuyen = _mahuyen;
-- 	END;
-- $$ LANGUAGE plpgsql;
-- SELECT * FROM GetGiaoThong('787');		
		
-- 		IF EXISTS (SELECT * FROM RgHuyen WHERE Mahuyen = _mahuyen) THEN
-- 			RETURN QUERY		
-- 				SELECT st_asgeojson(st_transform(gt.shape, 4326)) AS shape 
-- 				FROM giaothong_pgon gt
-- 				JOIN RgXa xa ON gt.IdXa::Character varying = xa.MaXa
-- 				AND xa.MaHuyen = _mahuyen;
-- 		ELSE
-- 			RETURN QUERY
-- 				SELECT st_asgeojson(st_transform(gt.shape, 4326)) AS shape 
-- 				FROM giaothong_pgon gt;
-- 		END IF;

-- DROP FUNCTION GetThuyHes;
CREATE FUNCTION GetThuyHes(
	_mahuyen Character varying
)
RETURNS TABLE (shape text)
		IF (_mahuyen != 'null') THENa
			RETURN QUERY		
				SELECT st_asgeojson(st_transform(th.shape, 4326)) AS shape 
				FROM ThuyHe_Pgon th
				JOIN RgXa xa ON th.IdXa::Character varying = xa.MaXa
				JOIN RgHuyen h ON xa.MaHuyen = h.MaHuyen
				AND h.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT st_asgeojson(st_transform(th.shape, 4326)) AS shape 
				FROM ThuyHe_Pgon th;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetThuyHes('787');

-- DROP FUNCTION GetThuyHe;
-- CREATE FUNCTION GetThuyHe(
-- 	_mahuyen Character varying
-- )
-- RETURNS TABLE (shape text)
-- AS $$
-- 	BEGIN
-- 		RETURN QUERY	
-- 			SELECT st_asgeojson(st_transform(th.shape, 4326)) AS shape 
-- 			FROM thuyhe_pgon th
-- 			JOIN rgxa xa ON th.idxa::Character varying = xa.maxa
-- 			AND xa.mahuyen = _mahuyen;
-- 	END;
-- $$ LANGUAGE plpgsql;
-- SELECT * FROM GetThuyHe('787');

-- DROP FUNCTION GetMembers;
CREATE FUNCTION GetMembers()
RETURNS TABLE(MemberId Character varying, Username Character varying, FullName Character varying, Department Character varying, Unit Character varying, Phone Character varying, Email  Character varying)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT mem.memberid, mem.username, mem.fullname, mem.department, mem.unit, mem.phone, mem.email
			FROM Member mem
			WHERE mem.memberid != 'xk794vvXR4rgi7f3ddcFwHfjwd928fGs';
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMembers()

-- DROP FUNCTION GetMember;
CREATE FUNCTION GetMember(
	_memberid Character varying
)
RETURNS TABLE(MemberId Character varying, Username Character varying, FullName Character varying, Department Character varying, Unit Character varying, Phone Character varying, Email  Character varying)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT mem.memberid, mem.username, mem.fullname, mem.department, mem.unit, mem.phone, mem.email 
			FROM Member mem
			WHERE mem.memberid = _memberid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMember('54b7e9f472faeadeeed9032b86f15435')

-- DROP FUNCTION PasswordChangeTime;
CREATE FUNCTION PasswordChangeTime(
	MemId Character varying, 
	Pwd Character varying
)
RETURNS TABLE(ChangeTime Text)
AS $$
	BEGIN
		RETURN QUERY
			SELECT (CASE 
				WHEN (((DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('MINUTE', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('SECOND', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT < 60 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', (((DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('MINUTE', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('SECOND', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT, ' giây trước.')
				WHEN ((DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('MINUTE', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT < 60 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', ((DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate)) * 60 + DATE_PART('MINUTE', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT, ' phút trước.')
				WHEN (DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT <= 24 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', (DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate) * 24 + DATE_PART('HOUR', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT, ' giờ trước.')
				WHEN (DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate))::INT <= 31 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', (DATE_PART('DAY', CURRENT_TIMESTAMP - mempwd.UpdateDate)), ' ngày trước.')
				WHEN ((DATE_PART('YEAR', CURRENT_TIMESTAMP) - DATE_PART('YEAR', mempwd.UpdateDate)) * 12 + (DATE_PART('MONTH', CURRENT_TIMESTAMP) - DATE_PART('MONTH', mempwd.UpdateDate)))::INT <= 12 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', ((DATE_PART('YEAR', CURRENT_TIMESTAMP) - DATE_PART('YEAR', mempwd.UpdateDate)) * 12 + (DATE_PART('MONTH', CURRENT_TIMESTAMP) - DATE_PART('MONTH', mempwd.UpdateDate)))::INT, ' tháng trước.')
				-- YEAR: CURRENT_TIMESTAMP (2023) - mempwd.UpdateDate
				WHEN (DATE_PART('YEAR', CURRENT_TIMESTAMP) - DATE_PART('YEAR', mempwd.UpdateDate))::INT <= 10 THEN CONCAT('Bạn đã đổi mật khẩu này cách đây ', (DATE_PART('YEAR', CURRENT_TIMESTAMP) - DATE_PART('YEAR', mempwd.UpdateDate)), ' năm trước.')
			END
			) AS ChangeTime
			FROM memberpasswords mempwd 
			WHERE mempwd.MemberId = MemId AND mempwd.Password = Pwd AND mempwd.IsDeleted = true;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM PasswordChangeTime('184c44f4c44d736e677713a540b87e88', encode(sha512('123'::bytea), 'hex'))

-- DROP FUNCTION CountLogins;
CREATE FUNCTION CountLogins()
RETURNS TABLE(danghoatdong Smallint,ngayhomnay Smallint, thanghientai Smallint, tatca Smallint)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.danghoatdong::Smallint, b.ngayhomnay::Smallint, c.thanghientai::Smallint, d.tatca::Smallint
			FROM (SELECT COUNT(Username) AS danghoatdong FROM MemberLogins WHERE IsDeleted = 0::Boolean) a,
				 (SELECT COUNT(Username) AS ngayhomnay FROM MemberLogins WHERE logindate::date = CURRENT_TIMESTAMP::date AND IsDeleted != 0::Boolean) b,
				 (SELECT COUNT(Username) AS thanghientai FROM MemberLogins WHERE to_char(logindate, 'YYYY-MM') = to_char(CURRENT_TIMESTAMP, 'YYYY-MM') AND IsDeleted != 0::Boolean) c,
				 (SELECT COUNT(Username) AS tatca FROM MemberLogins WHERE IsDeleted != 0::Boolean) d;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM CountLogins();

-- DROP FUNCTION GetMemberRoles(Character varying);
CREATE FUNCTION GetMemberRoles(
	_pagename Character varying
)
RETURNS TABLE(roleid INT, RoleName Character varying, mahuyen Character varying, tenhuyen Character varying, malopdoituong int, Nhomdulieu Character varying, tengocbang Character varying, lopdulieu Character varying, malopdulieu text, maquanhuyen text)
AS $$
	BEGIN	
		IF (_pagename = 'Trang chủ') THEN
			RETURN QUERY
				SELECT r.roleid, r.rolename, h.mahuyen, h.tenhuyen, ldt.malopdoituong, ldt.nhomdulieu, ldt.tengocbang, ldt.lopdulieu, CONCAT(r.roleid, h.mahuyen, ldt.malopdoituong) AS MaLopDuLieu, CONCAT(r.roleid, h.mahuyen) AS MaQuanHuyen 
				FROM Roles r
				JOIN RgHuyen h ON h.mahuyen = ANY(r.mahuyen)
				JOIN LopDoiTuong ldt ON h.MaHuyen = ANY (ldt.mahuyen)
				AND (r.roleid = 959610 OR r.roleid = 305693 OR r.roleid = 365778)
				AND ldt.malopdoituong NOT IN (775014, 670693, 565171, 519937, 810214, 699304, 413591);
		END IF;
		IF (_pagename = 'Phương án, kế hoạch về phòng, tránh ứng phó') THEN
			RETURN QUERY
				SELECT r.roleid, r.rolename, h.mahuyen, h.tenhuyen, ldt.malopdoituong, ldt.nhomdulieu, ldt.tengocbang, ldt.lopdulieu, CONCAT(r.roleid, h.mahuyen, ldt.malopdoituong) AS MaLopDuLieu, CONCAT(r.roleid, h.mahuyen) AS MaQuanHuyen 
				FROM Roles r
				JOIN RgHuyen h ON h.mahuyen = ANY(r.mahuyen)
				JOIN LopDoiTuong ldt ON h.MaHuyen = ANY (ldt.mahuyen)
				AND (r.roleid = 959610 OR r.roleid = 305693 OR r.roleid = 365778)
				AND ldt.malopdoituong IN (259516, 919588, 224197, 699304, 810214, 519937, 413591);
		END IF;
		IF (_pagename = 'Tư liệu về công tác PCTT') THEN
			RETURN QUERY
				SELECT r.roleid, r.rolename, h.mahuyen, h.tenhuyen, ldt.malopdoituong, ldt.nhomdulieu, ldt.tengocbang, ldt.lopdulieu, CONCAT(r.roleid, h.mahuyen, ldt.malopdoituong) AS MaLopDuLieu, CONCAT(r.roleid, h.mahuyen) AS MaQuanHuyen 
				FROM Roles r
				JOIN RgHuyen h ON h.mahuyen = ANY(r.mahuyen)
				JOIN LopDoiTuong ldt ON h.MaHuyen = ANY (ldt.mahuyen)
				AND (r.roleid = 959610 OR r.roleid = 305693 OR r.roleid = 365778)
				AND ldt.malopdoituong IN (565171, 670693, 775014);
		END IF;
		IF (_pagename = 'Quản trị hệ thống') THEN
			RETURN QUERY
				SELECT r.roleid, r.rolename, h.mahuyen, h.tenhuyen, ldt.malopdoituong, ldt.nhomdulieu, ldt.tengocbang, ldt.lopdulieu, CONCAT(r.roleid, h.mahuyen, ldt.malopdoituong) AS MaLopDuLieu, CONCAT(r.roleid, h.mahuyen) AS MaQuanHuyen 
				FROM Roles r
				JOIN RgHuyen h ON h.mahuyen = ANY(r.mahuyen)
				JOIN LopDoiTuong ldt ON h.MaHuyen = ANY (ldt.mahuyen);	
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMemberRoles('Quản trị hệ thống')

-- SELECT * FROM GetMemberRoles('Phương án, kế hoạch về phòng, tránh ứng phó')

-- 959610 (xem du lieu); 305693 (xuat bao cao); 365778 (chinh sua/cap nhat); 856531 (quan tri he thong)

-- DROP FUNCTION GetDeBaoBoBaos;
CREATE FUNCTION GetDeBaoBoBaos(
	_mahuyen Character varying
)
RETURNS TABLE(
	objectid int, idkenhmuong character varying, tenkenhmuong character varying, vitri character varying, chieudai Numeric, caotrinhdaykenh character varying, berongkenh character varying, hesomai character varying, caotrinhbotrai character varying, caotrinhbophai character varying, berongbotrai character varying, berongbophai character varying, hanhlangbaove character varying, capcongtrinh character varying, ketcaucongtrinh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape_length double precision, shape text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idkenhmuong, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, a.shape_length, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM debao_bobao a				
				LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
				WHERE h.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idkenhmuong, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, a.shape_length, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM debao_bobao a
				LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326));
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDeBaoBoBaos('null');

-- DROP FUNCTION GetDeBaoBoBao;
CREATE FUNCTION GetDeBaoBoBao(
	_objectid int
)
RETURNS TABLE(
	objectid int, idkenhmuong character varying, tenkenhmuong character varying, vitri character varying, chieudai Numeric, caotrinhdaykenh character varying, berongkenh character varying, hesomai character varying, caotrinhbotrai character varying, caotrinhbophai character varying, berongbotrai character varying, berongbophai character varying, hanhlangbaove character varying, capcongtrinh character varying, ketcaucongtrinh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape_length double precision, shape text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idkenhmuong, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, a.shape_length, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM debao_bobao a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDeBaoBoBao(1);

-- DROP FUNCTION GetMocCanhBaoTrieuCuongs;
CREATE FUNCTION GetMocCanhBaoTrieuCuongs(
	_mahuyen Character varying
)
RETURNS TABLE (objectid int, idmoccbtc Character varying, tenmoc Character varying, giatri Character varying, toadox numeric, toadoy numeric, vitri Character varying, maxa Character varying, mahuyen Character varying, namsudung Smallint, namcapnhat Smallint, ghichu Character varying, shape text)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idmoccbtc, a.tenmoc, a.giatri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namsudung, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM moccanhbaotrieucuong a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idmoccbtc, a.tenmoc, a.giatri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namsudung, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM moccanhbaotrieucuong a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMocCanhBaoTrieuCuongs('null');

-- DROP FUNCTION GetMocCanhBaoTrieuCuong;
CREATE FUNCTION GetMocCanhBaoTrieuCuong(
	_objectid int
)
RETURNS TABLE (objectid int, idmoccbtc Character varying, tenmoc Character varying, giatri Character varying, toadox numeric, toadoy numeric, vitri Character varying, maxa Character varying, mahuyen Character varying, namsudung Smallint, namcapnhat Smallint, ghichu Character varying, shape text)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idmoccbtc, a.tenmoc, a.giatri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namsudung, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM moccanhbaotrieucuong a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMocCanhBaoTrieuCuong(1);

-- DROP FUNCTION GetDiemAnToans;
CREATE FUNCTION GetDiemAnToans(
	_mahuyen Character varying
)
RETURNS TABLE (objectid INT, idantoan Character varying, vitri Character varying, toadox numeric, toadoy numeric, succhua INT, maxa Character varying, mahuyen Character varying, namcapnhat Smallint, ghichu Character varying, phuongan Character varying, shape Text)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idantoan, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.succhua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DiemAnToan a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idantoan, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.succhua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DiemAnToan a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDiemAnToans('null');

-- DROP FUNCTION GetDiemAnToan;
CREATE FUNCTION GetDiemAnToan(
	_objectid INT
)
RETURNS TABLE (objectid INT, idantoan Character varying, vitri Character varying, toadox numeric, toadoy numeric, succhua INT, maxa Character varying, mahuyen Character varying, namcapnhat Smallint, ghichu Character varying, phuongan Character varying, shape Text)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idantoan, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.succhua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM DiemAnToan a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDiemAnToan(123);

-- DROP FUNCTION GetDiemXungYeus;
CREATE FUNCTION GetDiemXungYeus(
	_mahuyen Character varying
)
RETURNS TABLE (objectid int, idxungyeu character varying, vitri Character varying, toadox numeric, toadoy numeric, sodan INT, maxa Character varying, mahuyen Character varying, namcapnhat Smallint, ghichu Character varying, phuongan Character varying, shape Text)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idxungyeu, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.sodan,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DiemXungYeu a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idxungyeu, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.sodan,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DiemXungYeu a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDiemXungYeus('787');

-- DROP FUNCTION GetDiemXungYeu;
CREATE FUNCTION GetDiemXungYeu(
	_objectid INT
)
RETURNS TABLE (objectid int, idxungyeu character varying, vitri Character varying, toadox numeric, toadoy numeric, sodan INT, maxa Character varying, mahuyen Character varying, namcapnhat Smallint, ghichu Character varying, phuongan Character varying, shape Text)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.objectid, a.idxungyeu, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.sodan,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM DiemXungYeu a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDiemXungYeu(692);

-- DROP FUNCTION GetCongDaps;
CREATE FUNCTION GetCongDaps(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idcongdap character varying, tencongdap character varying, lytrinh character varying, toadox numeric, toadoy numeric, cumcongtrinh character varying, goithau character varying, loaicongtrinh character varying, hinhthuc character varying, chieudai double precision, duongkinh double precision, berong double precision, chieucao double precision, socua smallint, caotrinhdaycong character varying, caotrinhdinhcong character varying, hinhthucvanhanh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, capcongtrinh character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idcongdap, a.tencongdap, a.lytrinh, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.cumcongtrinh, a.goithau, a.loaicongtrinh, a.hinhthuc, a.chieudai, a.duongkinh, a.berong, a.chieucao, a.socua, a.caotrinhdaycong, a.caotrinhdinhcong, a.hinhthucvanhanh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.capcongtrinh, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM CongDap a
				LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
				WHERE h.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idcongdap, a.tencongdap, a.lytrinh, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.cumcongtrinh, a.goithau, a.loaicongtrinh, a.hinhthuc, a.chieudai, a.duongkinh, a.berong, a.chieucao, a.socua, a.caotrinhdaycong, a.caotrinhdinhcong, a.hinhthucvanhanh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.capcongtrinh, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM CongDap a
				LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326));
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetCongDaps('null');

-- DROP FUNCTION GetCongDap;
CREATE FUNCTION GetCongDap(
	_objectid INT
)
RETURNS TABLE (
	objectid INT, idcongdap character varying, tencongdap character varying, lytrinh character varying, toadox numeric, toadoy numeric, cumcongtrinh character varying, goithau character varying, loaicongtrinh character varying, hinhthuc character varying, chieudai double precision, duongkinh double precision, berong double precision, chieucao double precision, socua smallint, caotrinhdaycong character varying, caotrinhdinhcong character varying, hinhthucvanhanh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, capcongtrinh character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idcongdap, a.tencongdap, a.lytrinh, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.cumcongtrinh, a.goithau, a.loaicongtrinh, a.hinhthuc, a.chieudai, a.duongkinh, a.berong, a.chieucao, a.socua, a.caotrinhdaycong, a.caotrinhdinhcong, a.hinhthucvanhanh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.capcongtrinh, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM CongDap a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetCongDap(1);

-- DROP FUNCTION GetKes;
CREATE FUNCTION GetKes(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idke character varying, tenkenhmuong character varying, vitri character varying, chieudai Numeric, caotrinhdaykenh double precision, berongkenh double precision, hesomai double precision, caotrinhbotrai character varying, caotrinhbophai character varying, berongbotrai character varying, berongbophai character varying, hanhlangbaove character varying, capcongtrinh character varying, cumcongtrinh character varying, ketcaucongtrinh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape_length double precision, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idke, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.cumcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a. namcapnhat, a.ghichu, a.shape_length, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM Ke a
				LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
				WHERE h.MaHuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idke, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.cumcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a. namcapnhat, a.ghichu, a.shape_length, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM Ke a
				LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326));
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetKes('null');

-- DROP FUNCTION GetKe;
CREATE FUNCTION GetKe(
	_objectid int
)
RETURNS TABLE (
	objectid int, idke character varying, tenkenhmuong character varying, vitri character varying, chieudai Numeric, caotrinhdaykenh double precision, berongkenh double precision, hesomai double precision, caotrinhbotrai character varying, caotrinhbophai character varying, berongbotrai character varying, berongbophai character varying, hanhlangbaove character varying, capcongtrinh character varying, cumcongtrinh character varying, ketcaucongtrinh character varying, muctieunhiemvu character varying, diadiem character varying, namsudung character varying, hethongcttl character varying, donviquanly character varying, namcapnhat smallint, ghichu character varying, shape_length double precision, shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idke, a.tenkenhmuong, a.vitri, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.cumcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a. namcapnhat, a.ghichu, a.shape_length, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM Ke a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetKe(9);

-- DROP FUNCTION GetApThapNhietDois;
CREATE FUNCTION GetApThapNhietDois(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid INT, idapthap character varying, tenapthap character varying, gio double precision, ngay text, toadox numeric, toadoy Numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat int, ghichu character varying, kinhdo numeric, vido numeric, ngaybatdau text, ngayketthuc text, centerid character varying, tenvn character varying, kvahhcm character varying
)
AS $$
	BEGIN	
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm
				FROM ApThapNhietDoi a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.ngay ASC, a.gio ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm
				FROM ApThapNhietDoi a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC, a.gio ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetApThapNhietDois('null');

-- DROP FUNCTION GetApThapNhietDoi;
CREATE FUNCTION GetApThapNhietDoi(
	_objectid int
)
RETURNS TABLE (
	objectid int, idapthap character varying, tenapthap character varying, gio double precision, ngay text, toadox numeric, toadoy Numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat int, ghichu character varying, kinhdo numeric, vido numeric, ngaybatdau text, ngayketthuc text, centerid character varying, tenvn character varying, kvahhcm character varying, shape text
)
AS $$
	BEGIN	
		RETURN QUERY		
			SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, a.mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
			FROM ApThapNhietDoi a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetApThapNhietDoi(1);

-- DROP FUNCTION GetBaos;
CREATE FUNCTION GetBaos(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid INT, idbao character varying, tenbao character varying, gio double precision, ngay text, toadox numeric, toadoy numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat int, ghichu character varying, kinhdo Numeric, vido Numeric, capbao character varying, ngaybatdau Text, ngayketthuc text, centerid character varying, tenvn character varying, kvahhcm character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm
				FROM Bao a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC, a.gio ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm
				FROM Bao a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC, a.gio ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;

-- DROP FUNCTION GetBao;
CREATE FUNCTION GetBao(
	_objectid INT
)
RETURNS TABLE (
	objectid INT, idbao character varying, tenbao character varying, gio double precision, ngay text, toadox numeric, toadoy numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat int, ghichu character varying, kinhdo Numeric, vido Numeric, capbao character varying, ngaybatdau Text, ngayketthuc text, centerid character varying, tenvn character varying, kvahhcm character varying, shape text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
			FROM Bao a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetBao(1);

-- DROP FUNCTION GetDoMans;
CREATE FUNCTION GetDoMans(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idtramman character varying, tentram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, tensong Character varying, doman double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idtramman, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tensong, a.doman, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DoMan a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idtramman, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tensong, a.doman, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM DoMan a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDoMans('null');

-- DROP FUNCTION GetDoMan;
CREATE FUNCTION GetDoMan(
	_objectid int
)
RETURNS TABLE (
	objectid int, idtramman character varying, tentram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, tensong Character varying, doman double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idtramman, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tensong, a.doman, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM DoMan a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDoMan(1);

-- DROP FUNCTION GetHuongDiChuyens;
CREATE FUNCTION GetHuongDiChuyens(
	_mahuyen character varying
)
RETURNS TABLE (
	objectid int, iddichuyen character varying, chieudai numeric, tenhuong character varying, khuvuc character varying, namcapnhat Smallint, ghichu character varying, mahuyen character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.iddichuyen, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.tenhuong, a.khuvuc, a.namcapnhat, a.ghichu, a.mahuyen
				FROM HuongDiChuyen a
				WHERE a.MaHuyen = _mahuyen 
				ORDER BY a.khuvuc ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
			SELECT a.objectid, a.iddichuyen, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.tenhuong, a.khuvuc, a.namcapnhat, a.ghichu, a.mahuyen
			FROM HuongDiChuyen a
			ORDER BY a.khuvuc ASC;
		END IF;	
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHuongDiChuyens('787');

-- DROP FUNCTION GetHuongDiChuyen;
CREATE FUNCTION GetHuongDiChuyen(
	_objectid INT
)
RETURNS TABLE (
	objectid int, iddichuyen character varying, chieudai numeric, tenhuong character varying, khuvuc character varying, namcapnhat Smallint, ghichu character varying, mahuyen character varying
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.iddichuyen, ROUND(a.chieudai::Numeric, 2) AS Chieudai, a.tenhuong, a.khuvuc, a.namcapnhat, a.ghichu, a.mahuyen
			FROM HuongDiChuyen a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHuongDiChuyen(3500);

-- DROP FUNCTION GetMucNuocs;
CREATE FUNCTION GetMucNuocs(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idtrammucnuoc character varying, tentram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, mucnuoc double precision, docaodinhtrieu double precision, docaochantrieu double precision, baodongi double precision, baodongii double precision, baodongiii double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.idtrammucnuoc, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.mucnuoc, a.docaodinhtrieu, a.docaochantrieu, a.baodongi, a.baodongii, a.baodongiii,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM MucNuoc a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idtrammucnuoc, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.mucnuoc, a.docaodinhtrieu, a.docaochantrieu, a.baodongi, a.baodongii, a.baodongiii,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM MucNuoc a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMucNuocs('null');

-- DROP FUNCTION GetMucNuoc;
CREATE FUNCTION GetMucNuoc(
	_objectid int
)
RETURNS TABLE (
	objectid int, idtrammucnuoc character varying, tentram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, mucnuoc double precision, docaodinhtrieu double precision, docaochantrieu double precision, baodongi double precision, baodongii double precision, baodongiii double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape Text
)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT a.objectid, a.idtrammucnuoc, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.mucnuoc, a.docaodinhtrieu, a.docaochantrieu, a.baodongi, a.baodongii, a.baodongiii,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
			FROM MucNuoc a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;				
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMucNuoc(1);

-- DROP FUNCTION GetNangNongs;
CREATE FUNCTION GetNangNongs(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid INT, idtramkt character varying, tentram character varying, captram character varying, vitritram character varying, toadox numeric, toadoy numeric, thang smallint, sogionang double precision, nhietdomin double precision, nhietdomax double precision, nhietdotb double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, ngay text, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.idtramkt, a.tentram, a.captram, a.vitritram, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.thang, a.sogionang, a.nhietdomin, a.nhietdomax, a.nhietdotb, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM NangNong a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idtramkt, a.tentram, a.captram, a.vitritram, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.thang, a.sogionang, a.nhietdomin, a.nhietdomax, a.nhietdotb, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM NangNong a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetNangNongs('null');

-- DROP FUNCTION GetNangNong;
CREATE FUNCTION GetNangNong(
	_objectid int
)
RETURNS TABLE (
	objectid INT, idtramkt character varying, tentram character varying, captram character varying, vitritram character varying, toadox numeric, toadoy numeric, thang smallint, sogionang double precision, nhietdomin double precision, nhietdomax double precision, nhietdotb double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, ngay text, shape Text
)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT a.objectid, a.idtramkt, a.tentram, a.captram, a.vitritram, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.thang, a.sogionang, a.nhietdomin, a.nhietdomax, a.nhietdotb, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
			FROM NangNong a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;				
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetNangNong('null');
	
-- DROP FUNCTION GetMuas;
CREATE FUNCTION GetMuas(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idtrammua character varying, tentram character varying, captram character varying, vitritram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, luongmua double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.idtrammua, a.tentram, a.captram, a.vitritram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.luongmua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM Mua a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idtrammua, a.tentram, a.captram, a.vitritram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.luongmua,  a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM Mua a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMuas('769');

-- DROP FUNCTION GetMua;
CREATE FUNCTION GetMua(
	_objectid int
)
RETURNS TABLE (
	objectid int, idtrammua character varying, tentram character varying, captram character varying, vitritram character varying, gio character varying, ngay text, toadox numeric, toadoy numeric, luongmua double precision, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, kinhdo numeric, vido numeric, shape Text
)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT a.objectid, a.idtrammua, a.tentram, a.captram, a.vitritram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.luongmua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM Mua a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetMua(1);

-- DROP FUNCTION GetHoChuas;
CREATE FUNCTION GetHoChuas(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idhochua character varying, ten character varying, loaiho character varying, vitri character varying, kinhdo numeric, vido numeric, maxa character varying, mahuyen character varying, ngay text, h double precision, w double precision, qvh double precision, qxa double precision, qcsi double precision, qcsii double precision, qcsiii double precision, qtb double precision, bh double precision, r double precision, namcapnhat integer, ghichu character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.idhochua, a.ten, a.loaiho, a.vitri, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.h, a.w, a.qvh, a.qxa, a.qcsi, a.qcsii, a.qcsiii, a.qtb, a.bh, a.r, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM HoChua a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idhochua, a.ten, a.loaiho, a.vitri, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.h, a.w, a.qvh, a.qxa, a.qcsi, a.qcsii, a.qcsiii, a.qtb, a.bh, a.r, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM HoChua a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHoChuas('null');

-- DROP FUNCTION GetHoChua;
CREATE FUNCTION GetHoChua(
	_objectid int
)
RETURNS TABLE (
	objectid int, idhochua character varying, ten character varying, loaiho character varying, vitri character varying, kinhdo numeric, vido numeric, maxa character varying, mahuyen character varying, ngay text, h double precision, w double precision, qvh double precision, qxa double precision, qcsi double precision, qcsii double precision, qcsiii double precision, qtb double precision, bh double precision, r double precision, namcapnhat integer, ghichu character varying, shape Text
)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT a.objectid, a.idhochua, a.ten, a.loaiho, a.vitri, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.h, a.w, a.qvh, a.qxa, a.qcsi, a.qcsii, a.qcsiii, a.qtb, a.bh, a.r, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM HoChua a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;				
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHoChua(1);

-- DROP FUNCTION GetRefreshToken;
CREATE FUNCTION GetRefreshToken(
	_token Character varying
)
RETURNS TABLE(refreshtokenid Character varying, memberloginid Character varying, Token Character varying)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.refreshtokenid, a.memberloginid, a.Token
			FROM RefreshToken a 
			WHERE a.Token = _token;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetRefreshToken('jusHXRtsZHge/Iv+0EvJY17B6NX+1q6vBeO9MxVF4Zgaqtuqr3o0VCjez4WlJ3YoyGqml7JfkMRia/mTE7GJmQ==')

-- DROP FUNCTION GetThietHaiThienTais;
CREATE FUNCTION GetThietHaiThienTais(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idthiethai character varying, loaithientai character varying, doituongthiethai character varying, motathiethai character varying, dvtthiethai character varying, soluong double precision, giatrithiethai double precision, diadiem character varying, gio character varying, ngay text, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY	
				SELECT a.objectid, a.idthiethai, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, a.soluong, a.giatrithiethai, a.diadiem, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM thiethai_thientai a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;				
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idthiethai, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, a.soluong, a.giatrithiethai, a.diadiem, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM thiethai_thientai a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;	
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetThietHaiThienTais('null');

-- DROP FUNCTION GetThietHaiThienTai;
CREATE FUNCTION GetThietHaiThienTai(
	_objectid int
)
RETURNS TABLE (
	objectid int, idthiethai character varying, loaithientai character varying, doituongthiethai character varying, motathiethai character varying, dvtthiethai character varying, soluong double precision, giatrithiethai double precision, diadiem character varying, gio character varying, ngay text, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT a.objectid, a.idthiethai, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, a.soluong, a.giatrithiethai, a.diadiem, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
			FROM thiethai_thientai a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;				
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetThietHaiThienTai(1);

-- DROP FUNCTION GetLocXoays;
CREATE FUNCTION GetLocXoays(
	_mahuyen Character varying
)
RETURNS TABLE (get
	objectid INT, idlocxoay character varying, tenlocxoay character varying, gio character varying, ngay text, toadox Numeric, toadoy Numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idlocxoay, a.tenlocxoay, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM LocXoay a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idlocxoay, a.tenlocxoay, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
				FROM LocXoay a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetLocXoays('787');

-- DROP FUNCTION GetLocXoay;
CREATE FUNCTION GetLocXoay(
	_objectid Int
)
RETURNS TABLE (
	objectid INT, idlocxoay character varying, tenlocxoay character varying, gio character varying, ngay text, toadox Numeric, toadoy Numeric, apsuat double precision, tocdogio double precision, vitri character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, shape Text
)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.objectid, a.idlocxoay, a.tenlocxoay, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape 
			FROM LocXoay a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetLocXoay(1);

-- DROP FUNCTION GetChayRungs;
CREATE FUNCTION GetChayRungs(
	_mahuyen Character varying
)
RETURNS TABLE (
	objectid int, idchay character varying, ngay character varying, diadiem character varying, toadox numeric, toadoy numeric, tgchay character varying, tgdap character varying, dtchay double precision, hientrang character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idchay, a.ngay, a.diadiem, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tgchay, a.tgdap, a.dtchay, a.hientrang, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM ChayRung a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.MaHuyen = _mahuyen
				ORDER BY a.ngay ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idchay, a.ngay, a.diadiem, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tgchay, a.tgdap, a.dtchay, a.hientrang, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM ChayRung a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngay ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetChayRungs('784');

-- DROP FUNCTION GetChayRung;
CREATE FUNCTION GetChayRung(
	_objectid int
)
RETURNS TABLE (
	objectid int, idchay character varying, ngay character varying, diadiem character varying, toadox numeric, toadoy numeric, tgchay character varying, tgdap character varying, dtchay double precision, hientrang character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idchay, a.ngay, a.diadiem, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tgchay, a.tgdap, a.dtchay, a.hientrang, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM ChayRung a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid =  _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetChayRung(1);

-- DROP FUNCTION GetKhuNeoDaus;
CREATE FUNCTION GetKhuNeoDaus(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid int, idknd character varying, ten character varying, diachi character varying, kinhdodd numeric, vidodd numeric, kinhdodc numeric, vidodc numeric, dosaunuoc double precision, succhua Character varying, coloaitau character varying, vitrivl character varying, huongluong character varying, chieudai numeric, sdt character varying, tansoll character varying, maxa character varying, mahuyen character varying, namcapnhat integer, ghichu character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idknd, a.ten, a.diachi, ROUND(a.kinhdodd::Numeric, 3) AS kinhdodd, ROUND(a.vidodd::Numeric, 3) AS vidodd, ROUND(a.kinhdodc::Numeric, 3) AS kinhdodc, ROUND(a.vidodc::Numeric, 3) AS vidodc, a.dosaunuoc, a.succhua, a.coloaitau, a.vitrivl, a.huongluong, ROUND(a.chieudai::Numeric, 2) AS chieudai, a.sdt, a.tansoll,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM KhuNeoDau a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen =  _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idknd, a.ten, a.diachi, ROUND(a.kinhdodd::Numeric, 3) AS kinhdodd, ROUND(a.vidodd::Numeric, 3) AS vidodd, ROUND(a.kinhdodc::Numeric, 3) AS kinhdodc, ROUND(a.vidodc::Numeric, 3) AS vidodc, a.dosaunuoc, a.succhua, a.coloaitau, a.vitrivl, a.huongluong, ROUND(a.chieudai::Numeric, 2) AS chieudai, a.sdt, a.tansoll,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM KhuNeoDau a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetKhuNeoDaus('null');

-- DROP FUNCTION GetKhuNeoDau;
CREATE FUNCTION GetKhuNeoDau(
	_objectid int
)
RETURNS TABLE (
	 objectid int, idknd character varying, ten character varying, diachi character varying, kinhdodd numeric, vidodd numeric, kinhdodc numeric, vidodc numeric, dosaunuoc double precision, succhua Character varying, coloaitau character varying, vitrivl character varying, huongluong character varying, chieudai numeric, sdt character varying, tansoll character varying, maxa character varying, mahuyen character varying, namcapnhat integer, ghichu character varying, shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idknd, a.ten, a.diachi, ROUND(a.kinhdodd::Numeric, 3) AS kinhdodd, ROUND(a.vidodd::Numeric, 3) AS vidodd, ROUND(a.kinhdodc::Numeric, 3) AS kinhdodc, ROUND(a.vidodc::Numeric, 3) AS vidodc, a.dosaunuoc, a.succhua, a.coloaitau, a.vitrivl, a.huongluong, ROUND(a.chieudai::Numeric, 2) AS chieudai, a.sdt, a.tansoll,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM KhuNeoDau a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid =  _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetKhuNeoDau(23);

-- DROP FUNCTION GetDanhBaDTs;
CREATE FUNCTION GetDanhBaDTs(
	_mahuyen character varying
)
RETURNS TABLE (
	 objectid int, iddanhba character varying, quanhuyen character varying, hoten character varying, cvcoquan character varying, cvbch character varying, dtcoquan character varying, dtdidong character varying, fax character varying, mahuyen character varying, namcapnhat integer, ghichu character varying
)
AS $$
	BEGIN		
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.iddanhba, a.quanhuyen, a.hoten, a.cvcoquan, a.cvbch, a.dtcoquan, a.dtdidong, a.fax, a.mahuyen, a.namcapnhat, a.ghichu 
				FROM DanhBaDT a
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.quanhuyen ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.iddanhba, a.quanhuyen, a.hoten, a.cvcoquan, a.cvbch, a.dtcoquan, a.dtdidong, a.fax, a.mahuyen, a.namcapnhat, a.ghichu 
				FROM DanhBaDT a
				ORDER BY a.quanhuyen ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDanhBaDTs('null');

-- DROP FUNCTION GetDanhBaDT;
CREATE FUNCTION GetDanhBaDT(
	_objectid int
)
RETURNS TABLE (
	 objectid int, iddanhba character varying, quanhuyen character varying, hoten character varying, cvcoquan character varying, cvbch character varying, dtcoquan character varying, dtdidong character varying, fax character varying, mahuyen character varying, namcapnhat integer, ghichu character varying
)
AS $$
	BEGIN		
		RETURN QUERY
			SELECT a.objectid, a.iddanhba, a.quanhuyen, a.hoten, a.cvcoquan, a.cvbch, a.dtcoquan, a.dtdidong, a.fax, a.mahuyen, a.namcapnhat, a.ghichu 
			FROM DanhBaDT a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDanhBaDT(1);

-- DROP FUNCTION GetHistory;
CREATE FUNCTION GetHistory(
	_tablename Character Varying
)
RETURNS TABLE (
    id Character Varying, rowid Character Varying, username character varying, operation character varying, operationdate character varying, olddata text, changedata text 
)
AS $$
	BEGIN	
		RETURN QUERY
			SELECT a.id, a.rowid, a.username, a.operation, TO_CHAR(a.operationdate, 'dd/mm/yyyy')::character varying AS operationdate, b.olddata, a.changedata
			FROM History a
			LEFT JOIN
			(
				SELECT a.id, STRING_AGG(CONCAT(a.key, ': "', a.value, '"'), '; ') AS olddata
				FROM
				(
					select a.id, b.key, b.value from History a
					, json_each_text(a.olddata) b
				) a
				GROUP by a.id
			) b ON a.id = b.id
			WHERE a.tablename = _tablename
			ORDER BY a.id ASC;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetHistory('DanhBaDT');

-- DROP FUNCTION GetDuKienDiDans;
CREATE FUNCTION GetDuKienDiDans(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid int, idkhsotan character varying, sovb character varying, ngayvb text, loaivb character varying, quanhuyen character varying, mahuyen character varying, sophuongdidan integer, soho_bao8_9 integer, songuoi_bao8_9 integer, soho_bao10_13 integer, songuoi_bao10_13 integer, namcapnhat smallint, ghichu character varying, sohocandidoi INT
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idkhsotan, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.quanhuyen, a.mahuyen, a.sophuongdidan, a.soho_bao8_9, a.songuoi_bao8_9, a.soho_bao10_13, a.songuoi_bao10_13, a.namcapnhat, a.ghichu, a.sohocandidoi
				FROM DuKienDiDan a
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.quanhuyen ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idkhsotan, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.quanhuyen,  a.mahuyen, a.sophuongdidan, a.soho_bao8_9, a.songuoi_bao8_9, a.soho_bao10_13, a.songuoi_bao10_13, a.namcapnhat, a.ghichu, a.sohocandidoi
				FROM DuKienDiDan a
				ORDER BY a.quanhuyen ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDuKienDiDans('787');

-- DROP FUNCTION GetDuKienDiDan;
CREATE FUNCTION GetDuKienDiDan(
	_objectid int
)
RETURNS TABLE (
	 objectid int, idkhsotan character varying, sovb character varying, ngayvb text, loaivb character varying, quanhuyen character varying, mahuyen character varying, sophuongdidan integer, soho_bao8_9 integer, songuoi_bao8_9 integer, soho_bao10_13 integer, songuoi_bao10_13 integer, namcapnhat smallint, ghichu character varying, sohocandidoi INT
)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.objectid, a.idkhsotan, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.quanhuyen,  a.mahuyen, a.sophuongdidan, a.soho_bao8_9, a.songuoi_bao8_9, a.soho_bao10_13, a.songuoi_bao10_13, a.namcapnhat, a.ghichu, a.sohocandidoi
			FROM DuKienDiDan a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetDuKienDiDan('2');

-- DROP FUNCTION GetBienCanhBaoSatLos;
CREATE FUNCTION GetBienCanhBaoSatLos(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid int, idbcbsl character varying, sohieubien character varying, toadox numeric, toadoy numeric, vitrisatlo character varying, phamvi character varying, maxa character varying, mahuyen character varying, namxaydung smallint, hinhanh character varying, namcapnhat smallint, ghichu character varying, tuyensr character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idbcbsl, a.sohieubien, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitrisatlo, a.phamvi,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namxaydung, a.hinhanh, a.namcapnhat, a.ghichu, a.tuyensr, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM BienCanhBaoSatLo a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.namxaydung ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idbcbsl, a.sohieubien, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitrisatlo, a.phamvi, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namxaydung, a.hinhanh, a.namcapnhat, a.ghichu, a.tuyensr, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM BienCanhBaoSatLo a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.namxaydung ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetBienCanhBaoSatLos('769');

-- DROP FUNCTION GetBienCanhBaoSatLo;
CREATE FUNCTION GetBienCanhBaoSatLo(
	_objectid int
)
RETURNS TABLE (
	 objectid int, idbcbsl character varying, sohieubien character varying, toadox numeric, toadoy numeric, vitrisatlo character varying, phamvi character varying, maxa character varying, mahuyen character varying, namxaydung smallint, hinhanh character varying, namcapnhat smallint, ghichu character varying, tuyensr character varying, shape Text, file character varying
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idbcbsl, a.sohieubien, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitrisatlo, a.phamvi,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namxaydung, ''::character varying AS hinhanh, a.namcapnhat, a.ghichu, a.tuyensr, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape, a.hinhanh AS file
			FROM BienCanhBaoSatLo a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetBienCanhBaoSatLo(64);

-- DROP FUNCTION GetTuLieuHinhAnhs;
CREATE FUNCTION GetTuLieuHinhAnhs(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid INT, idhinhanh character varying, tenhinhanh character varying, ngayhinhanh text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idhinhanh, a.tenhinhanh, TO_CHAR(a.ngayhinhanh, 'dd/mm/yyyy')::Text AS ngayhinhanh, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuHinhAnh a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.ngayhinhanh ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idhinhanh, a.tenhinhanh, TO_CHAR(a.ngayhinhanh, 'dd/mm/yyyy')::Text AS ngayhinhanh, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuHinhAnh a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngayhinhanh ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuHinhAnhs('null');

-- DROP FUNCTION GetTuLieuHinhAnh;
CREATE FUNCTION GetTuLieuHinhAnh(
	_objectid INT
)
RETURNS TABLE (
	 objectid INT, idhinhanh character varying, tenhinhanh character varying, ngayhinhanh text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.objectid, a.idhinhanh, ''::character varying AS tenhinhanh, TO_CHAR(a.ngayhinhanh, 'dd/mm/yyyy')::Text AS ngayhinhanh, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
			FROM TuLieuHinhAnh a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuHinhAnh(1);

-- DROP FUNCTION GetTuLieuKhacs;
CREATE FUNCTION GetTuLieuKhacs(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid INT, idtulieu character varying, tentulieu character varying, ngaytulieu text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idtulieu, a.tentulieu, TO_CHAR(a.ngaytulieu, 'dd/mm/yyyy')::Text AS ngaytulieu, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuKhac a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.ngaytulieu ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idtulieu, a.tentulieu, TO_CHAR(a.ngaytulieu, 'dd/mm/yyyy')::Text AS ngaytulieu, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuKhac a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngaytulieu ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuKhacs('765');

-- DROP FUNCTION GetTuLieuKhac;
CREATE FUNCTION GetTuLieuKhac(
	_objectid INT
)
RETURNS TABLE (
	 objectid INT, idtulieu character varying, tentulieu character varying, ngaytulieu text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idtulieu, ''::character varying AS tentulieu, TO_CHAR(a.ngaytulieu, 'dd/mm/yyyy')::Text AS ngaytulieu, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
			FROM TuLieuKhac a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuKhac(10);

-- DROP FUNCTION GetTuLieuVideos;
CREATE FUNCTION GetTuLieuVideos(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid int, idvideo character varying, tenvideo character varying, ngayvideo text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idvideo, a.tenvideo, TO_CHAR(a.ngayvideo, 'dd/mm/yyyy')::Text AS ngayvideo, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuVideo a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.ngayvideo ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idvideo, a.tenvideo, TO_CHAR(a.ngayvideo, 'dd/mm/yyyy')::Text AS ngayvideo, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
				FROM TuLieuVideo a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				ORDER BY a.ngayvideo ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuVideos('null');

-- DROP FUNCTION GetTuLieuVideo;
CREATE FUNCTION GetTuLieuVideo(
	_objectid INT
)
RETURNS TABLE (
	 objectid int, idvideo character varying, tenvideo character varying, ngayvideo text, noidung character varying, diadiem character varying, dvql character varying, nguongoc character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying
)
AS $$
	BEGIN	
		RETURN QUERY		
			SELECT a.objectid, a.idvideo, ''::character varying AS tenvideo, TO_CHAR(a.ngayvideo, 'dd/mm/yyyy')::Text AS ngayvideo, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu
			FROM TuLieuVideo a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetTuLieuVideo(1);

-- DROP FUNCTION GetLucLuongHuyDongs;
CREATE FUNCTION GetLucLuongHuyDongs(
	_mahuyen Character varying
)
RETURNS TABLE (
	 objectid int, idkhlucluong character varying, qhtp character varying, tenlucluong character varying, capql character varying, slnguoihd int, sovb character varying, ngayvb text, loaivb character varying, namcapnhat smallint, ghichu character varying, namsudung integer, mahuyen character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idkhlucluong, a.qhtp, a.tenlucluong, a.capql, a.slnguoihd, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.namsudung, a.mahuyen
				FROM LucLuongHuyDongDetail a
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.qhtp ASC, a.tenlucluong ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idkhlucluong, a.qhtp, a.tenlucluong, a.capql, a.slnguoihd, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.namsudung, a.mahuyen
				FROM LucLuongHuyDong a
				ORDER BY a.qhtp ASC, a.tenlucluong ASC;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetLucLuongHuyDongs('785');

-- DROP FUNCTION GetLucLuongHuyDong;
CREATE FUNCTION GetLucLuongHuyDong(
	_objectid int
)
RETURNS TABLE (
	 objectid int, idkhlucluong character varying, qhtp character varying, tenlucluong character varying, capql character varying, slnguoihd int, sovb character varying, ngayvb text, loaivb character varying, namcapnhat smallint, ghichu character varying, namsudung integer, mahuyen character varying
)
AS $$
	BEGIN
		RETURN QUERY
			SELECT a.objectid, a.idkhlucluong, a.qhtp, a.tenlucluong, a.capql, a.slnguoihd, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.namsudung, a.mahuyen
			FROM LucLuongHuyDongDetail a
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetLucLuongHuyDong(1);

-- DROP FUNCTION GetPhuongTienHuyDongs;
CREATE FUNCTION GetPhuongTienHuyDongs(
	_mahuyen character varying
)
RETURNS TABLE (
	 objectid int, idkhphuogtien character varying, tenphuongtienttb character varying, dvql character varying, dvt character varying, soluong double precision, sovb character varying, ngayvb text, loaivb character varying, namcapnhat smallint, ghichu character varying, phannhom1 character varying, phannhom2 character varying, phannhom3 character varying, mahuyen character varying
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idkhphuogtien, a.tenphuongtienttb, a.dvql, a.dvt, a.soluong, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.phannhom1, a.phannhom2, a.phannhom3, a.mahuyen
				FROM PhuongTienHuyDong a
				WHERE a.mahuyen = _mahuyen
				ORDER BY a.tenphuongtienttb ASC;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idkhphuogtien, a.tenphuongtienttb, a.dvql, a.dvt, a.soluong, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.phannhom1, a.phannhom2, a.phannhom3, a.mahuyen
				FROM PhuongTienHuyDong a
				ORDER BY a.tenphuongtienttb ASC;
		END IF;	
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetPhuongTienHuyDongs('null');

-- DROP FUNCTION GetPhuongTienHuyDong;
CREATE FUNCTION GetPhuongTienHuyDong(
	_objectid INT
)
RETURNS TABLE (
	 objectid int, idkhphuogtien character varying, tenphuongtienttb character varying, dvql character varying, dvt character varying, soluong double precision, sovb character varying, ngayvb text, loaivb character varying, namcapnhat smallint, ghichu character varying, phannhom1 character varying, phannhom2 character varying, phannhom3 character varying, mahuyen character varying
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idkhphuogtien, a.tenphuongtienttb, a.dvql, a.dvt, a.soluong, a.sovb, (a.ngayvb::date)::text, a.loaivb, a.namcapnhat, a.ghichu, a.phannhom1, a.phannhom2, a.phannhom3, a.mahuyen
			FROM PhuongTienHuyDong a
			WHERE a.objectid = _objectid
			ORDER BY a.ngayvb ASC;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetPhuongTienHuyDong(1);

-- DROP FUNCTION GetSatLoLines;
CREATE FUNCTION GetSatLoLines(
	_mahuyen Character varying
)
RETURNS TABLE (
	  objectid integer, idsatlol character varying, vitri character varying, tuyensong character varying, capsong character varying, chieudai numeric, chieurong numeric, mucdo character varying, tinhtrang character varying, anhhuong character varying, khoangcachah character varying, ditichah double precision, sohoah integer, songuoiah integer, hatangah character varying, congtrinhchongsl character varying, chudautu character varying, tenduan character varying, quymoduan character varying, tongmucduan integer, tiendothuchien character varying, nguongoc character varying, dubao character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, ctxdke character varying, shape_length double precision, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idsatlol, a.vitri, a.tuyensong, a.capsong, ROUND(a.chieudai::Numeric, 2) AS chieudai, ROUND(a.chieurong::Numeric, 2) AS chieurong, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, a.shape_length, ST_Asgeojson(ST_Transform(ST_LineMerge(a.shape), 4326)) AS shape
				FROM SatLo_Line a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idsatlol, a.vitri, a.tuyensong, a.capsong, ROUND(a.chieudai::Numeric, 2) AS chieudai, ROUND(a.chieurong::Numeric, 2) AS chieurong, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, a.shape_length, ST_Asgeojson(ST_Transform(ST_LineMerge(a.shape), 4326)) AS shape
				FROM SatLo_Line a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetSatLoLines('null');

-- DROP FUNCTION GetSatLoLine;
CREATE FUNCTION GetSatLoLine(
	_objectid int
)
RETURNS TABLE (
	  objectid int, idsatlol character varying, vitri character varying, tuyensong character varying, capsong character varying, chieudai numeric, chieurong numeric, mucdo character varying, tinhtrang character varying, anhhuong character varying, khoangcachah character varying, ditichah double precision, sohoah integer, songuoiah integer, hatangah character varying, congtrinhchongsl character varying, chudautu character varying, tenduan character varying, quymoduan character varying, tongmucduan integer, tiendothuchien character varying, nguongoc character varying, dubao character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, ctxdke character varying, shape_length double precision, shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.objectid, a.idsatlol, a.vitri, a.tuyensong, a.capsong, ROUND(a.chieudai::Numeric, 2) AS chieudai, ROUND(a.chieurong::Numeric, 2) AS chieurong, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, a.shape_length, ST_Asgeojson(ST_Transform(ST_LineMerge(a.shape), 4326)) AS shape
			FROM SatLo_Line a
			LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
			WHERE a.objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetSatLoLine(1);

-- DROP FUNCTION GetSatLoPoints;
CREATE FUNCTION GetSatLoPoints(
	_mahuyen Character varying
)
RETURNS TABLE (
	  objectid integer, idsatlop character varying, vitri character varying, tuyensong character varying, capsong character varying, chieudai Numeric, chieurong Numeric, mucdo character varying, tinhtrang character varying, anhhuong character varying, khoangcachah character varying, ditichah double precision, sohoah integer, songuoiah integer, hatangah character varying, congtrinhchongsl character varying, chudautu character varying, tenduan character varying, quymoduan character varying, tongmucduan integer, tiendothuchien character varying, nguongoc character varying, dubao character varying, maxa character varying, mahuyen character varying, namcapnhat smallint, ghichu character varying, ctxdke character varying, shape Text
)
AS $$
	BEGIN
		IF (_mahuyen != 'null') THEN
			RETURN QUERY		
				SELECT a.objectid, a.idsatlop, a.vitri, a.tuyensong, a.capsong, ROUND(a.chieudai::Numeric, 2) AS chieudai, ROUND(a.chieurong::Numeric, 2) AS chieurong, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM SatLo_Point a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
				WHERE a.mahuyen = _mahuyen;
		END IF;
		IF (_mahuyen = 'null') THEN
			RETURN QUERY
				SELECT a.objectid, a.idsatlop, a.vitri, a.tuyensong, a.capsong, ROUND(a.chieudai::Numeric, 2) AS chieudai, ROUND(a.chieurong::Numeric, 2) AS chieurong, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
				FROM SatLo_Point a
				LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetSatLoPoints('null');

-- Thong ke baofet
-- SELECT a.*, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao
-- FROM
-- (
-- 	SELECT a.ghichu AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao
-- 	FROM
-- 		(
-- 			SELECT a.tenbao, a.ghichu, a.year
-- 			FROM Bao a
-- 			WHERE a.year between 2015 and 2020
-- 			GROUP BY a.tenbao, a.ghichu, a.year
-- 		) a
-- 	GROUP BY a.ghichu
-- ) a,
-- (
-- 	SELECT COUNT(a.tenbao) AS Tong
-- 	FROM
-- 	(
-- 		SELECT a.tenbao, a.ghichu, a.year
-- 		FROM Bao a
-- 		WHERE a.year between 2015 and 2020
-- 		GROUP BY a.tenbao, a.ghichu, a.year
-- 	) a
-- ) b

-- DROP FUNCTION GetThuyHeHoChuas;
CREATE FUNCTION GetThuyHeHoChuas()
RETURNS TABLE (
	 TenThuyHeHoChua Character varying, Shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.ten AS TenThuyHeHoChua, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM ThuyHe_HoChua a;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetThuyHeHoChuas();

-- DROP FUNCTION GetQdhoangSaTruongSa;
CREATE FUNCTION GetQdhoangSaTruongSa()
RETURNS TABLE (
	 quandao Character varying, shape Text
)
AS $$
	BEGIN
		RETURN QUERY		
			SELECT a.quandao, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape
			FROM qd_hoangsa_truongsa a;
	END;
-- SELECT * FROM GetQdhoangSaTruongSa();

-- =========================================== CREATE PROCEDURE =======================================

-- DROP PROCEDURE AddMember;
CREATE PROCEDURE AddMember(
	_memberid Character varying,
	_password Character varying, 
	_username Character varying,
	_fullname Character varying,
	_department Character varying,
	_unit Character varying,
	_phone Character varying,
	_email Character varying
)  
AS $$
	INSERT INTO Member(MemberId, Username, FullName, Department, Unit, Phone, Email) VALUES (_memberid, _username, _fullname, _department, _unit, _phone, _email);
	INSERT INTO MemberPasswords(MemberId, Password) VALUES(_memberid, _password);
$$ LANGUAGE SQL; 
-- CALL AddMember('tri', 'tri', 'tri', 'tri', 'tri', 'tri', 'tri', 'tri')

-- DROP PROCEDURE EditMember;
CREATE PROCEDURE EditMember(
	_username Character varying,
	_fullname Character varying,
	_department Character varying,
	_unit Character varying,
	_phone Character varying,
	_email Character varying
)
AS $$
	UPDATE Member SET Username = _username, FullName = _fullname, Department = _department, Unit = _unit, Phone = _phone, Email = _email 
	WHERE MemberId = _memberid;
$$ LANGUAGE SQL;
-- CALL EditMember('tri', 'truongtri', 'truongtri', 'truongtri', 'truongtri', 'truongtri', 'truongtri')

-- DROP PROCEDURE DeleteMember;
CREATE PROCEDURE DeleteMember(
	_memberid Character varying
)  
AS $$
	DELETE FROM MemberRoles WHERE MemberId = _memberid;
	DELETE FROM MemberPasswords WHERE MemberId = _memberid;	
	DELETE FROM Member WHERE MemberId = _memberid
$$ LANGUAGE SQL; 
-- CALL DeleteMember('tri')

-- DROP PROCEDURE ChangePassword;
CREATE PROCEDURE ChangePassword(
	_memid Character varying,
	_oldpwd Character varying,
	_newpwd Character varying
)
AS $$
	BEGIN
		IF EXISTS(SELECT * FROM Member m, MemberPasswords mp WHERE m.MemberId = mp.MemberId AND m.MemberId = MemId AND mp.Password = _oldpwd) THEN
			UPDATE MemberPasswords mp SET IsDeleted = 1::Boolean, UpdateDate = CURRENT_TIMESTAMP WHERE mp.MemberId = MemId AND mp.Password = _oldpwd;
			INSERT INTO MemberPasswords(MemberId, Password) VALUES(_memid, _newpwd);
		END IF;
	END;
$$ LANGUAGE plpgsql; 
-- CALL ChangePassword('54b7e9f472faeadeeed9032b86f15435', 'PJkJr+wlNU1VHa4hWQuybjjVPyFzuNPcPu5MBH56scHri4UQPjvnumE7MbtcnDYhTcnxSkL9ei/bhIVrylxEwg==', '9rB7bBNA6Ue4Yd71+LCS2O5xCCbcVr0XW9yPOhawuKz4U8ZHhqcQ3t+dFSTWHjJQTifWDeFZrxELw5QUkHMVeA==');

-- DROP PROCEDURE ResetPassword;
CREATE PROCEDURE ResetPassword(
	_memid Character varying,
	_pwd Character varying
)
AS $$
	BEGIN
		DELETE FROM MemberPasswords WHERE MemberId = _memid;
		INSERT INTO memberpasswords(memberid, password) VALUES(_memid, _pwd);
	END;
$$ LANGUAGE plpgsql; 
-- CALL ResetPassword('54b7e9f472faeadeeed9032b86f15435', 'PJkJr+wlNU1VHa4hWQuybjjVPyFzuNPcPu5MBH56scHri4UQPjvnumE7MbtcnDYhTcnxSkL9ei/bhIVrylxEwg==', '9rB7bBNA6Ue4Yd71+LCS2O5xCCbcVr0XW9yPOhawuKz4U8ZHhqcQ3t+dFSTWHjJQTifWDeFZrxELw5QUkHMVeA==');


-- DROP PROCEDURE AddMemberLogin;
CREATE PROCEDURE AddMemberLogin(
	_memlogid character varying,
	_username character varying
)
AS $$
	INSERT INTO memberlogins(Id, username) VALUES(_memlogid, _username);
$$ LANGUAGE SQL;editmemberl
-- CALL AddMemberLogin('123', 'user');
-- SELECT * FROM memberlogins

-- DROP PROCEDURE EditMemberLogin;
CREATE PROCEDURE EditMemberLogin(
	_id character varying
)
AS $$
	UPDATE MemberLogins SET IsDeleted = 1::Boolean WHERE id = _id;
$$ LANGUAGE SQL;
-- CALL EditMemberLogin(5);

-- DROP FUNCTION GetRoleOfMember; -- lay ra roleid cua nguoi dung va luu vao Claim (Token)
CREATE FUNCTION GetRoleOfMember(
	_memberid Character varying
)
RETURNS TABLE (RoleId INT)
AS $$
	BEGIN
		RETURN QUERY	
			SELECT DISTINCT mr.RoleId 
			FROM MemberRoles mr
			WHERE MemberId = _memberid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetRoleOfMember('54b7e9f472faeadeeed9032b86f15435');

-- DROP FUNCTION GetRoleByMember; - lay ra cac lop du lieu ma tai khoan co the truy cap
CREATE FUNCTION GetRoleByMember(
	_memberid Character varying
)
RETURNS TABLE (
	MemberId Character varying, Roleid INT, MaHuyen Character varying, MaLopDuLieu Character varying[] 
)
AS $$
	BEGIN
		RETURN QUERY
			SELECT mr.memberid, mr.roleid, mr.mahuyen, mr.malopdulieu FROM MemberRoles mr WHERE mr.MemberId = _memberid;
	END;
$$ LANGUAGE plpgsql;
-- SELECT * FROM GetRoleByMember('54b7e9f472faeadeeed9032b86f15435')

-- DROP PROCEDURE AddMemberRoles;
CREATE PROCEDURE AddMemberRoles(
	_memberid Character varying,
	_roleid INT,
	_mahuyen Character varying,
	_malopdulieu Character varying[]
)
AS $$
	INSERT INTO MemberRoles(MemberId, RoleId, MaHuyen, MaLopDuLieu) VALUES(_memberid, _roleid, _mahuyen, _malopdulieu)
$$ LANGUAGE SQL;
-- CALL AddMemberRoles('54b7e9f472faeadeeed9032b86f15435', 959610, '888', ARRAY['Áp thấp nhiệt đới', 'Kè', 'Cống đập', 'Hướng di chuyển'])

-- DROP PROCEDURE DeleteMemberRole;
CREATE PROCEDURE DeleteMemberRole(
	_memberid Character varying
)
AS $$
	DELETE FROM MemberRoles WHERE MemberId = _memberid
$$ LANGUAGE SQL;
-- CALL DeleteMemberRole('54b7e9f472faeadeeed9032b86f15435')

-- DROP PROCEDURE AddRefreshToken;
CREATE PROCEDURE AddRefreshToken(
	_refreshtokenid Character varying,
-- 	_memberid Character varying,
	_memlogid Character varying,
	_token Character varying
)
AS $$
	INSERT INTO RefreshToken (RefreshTokenId, MemberLoginId, Token) 
	VALUES (_refreshtokenid, _memlogid, _token);
$$ LANGUAGE SQL;
-- CALL AddRefreshToken('123', '54b7e9f472faeadeeed9032b86f15435', '95aa47b253bb4aea961275aaecd9ae7b', '123', 0::Boolean, 0::Boolean, '2023-11-06 13:45:38.25939+07', '2023-11-06 13:45:38.25939+07')

-- DROP PROCEDURE EditRefreshToken;
-- CREATE PROCEDURE EditRefreshToken(
-- 	_newaccesstoken Text,
-- 	_newrefreshtoken Text,
-- 	_oldrefreshtoken Text
-- )
-- AS $$
-- 	BEGIN
-- 		IF EXISTS (SELECT * FROM RefreshToken  WHERE Token = _oldrefreshtoken) THEN
-- 			UPDATE RefreshToken SET AccessToken = _newaccesstoken, Token = _newrefreshtoken WHERE Token = _oldrefreshtoken;
-- 		END IF;		
-- 	END;
-- $$ LANGUAGE plpgsql;
-- CALL EditRefreshToken('123', '456', '+5+JEF4Y7eEoXCjFVIwSKnavaf0poEKK908LXzXgIt0=')

-- DROP PROCEDURE DeteleRefreshToken;
CREATE PROCEDURE DeteleRefreshToken(
	_MemberLoginId Character varying
)
AS $$
	DELETE FROM RefreshToken WHERE MemberLoginId = _MemberLoginId;			
$$ LANGUAGE SQL;
-- CALL DeteleRefreshToken('aa8004e570d44132860224ec86500f74')

-- DROP PROCEDURE AddPhuongTienHuyDong;
CREATE PROCEDURE AddPhuongTienHuyDong(
    _objectid integer,
	_idkhphuogtien Character varying,
    _tenphuongtienttb character varying,
    _dvql character varying,
    _dvt character varying,
    _soluong double precision,
    _sovb character varying,
    _ngayvb timestamp without time zone,
    _loaivb character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _phannhom1 character varying,
    _phannhom2 character varying,
    _phannhom3 character varying,
    _day integer,
    _month integer,
    _year integer,
	_mahuyen character varying
)
AS $$
	INSERT INTO PhuongTienHuyDong(objectid, idkhphuogtien, tenphuongtienttb, dvql, dvt, soluong, sovb, ngayvb, loaivb, namcapnhat, phannhom1, phannhom2, phannhom3, day, month, year, mahuyen)
	VALUES (_objectid, _idkhphuogtien, _tenphuongtienttb, _dvql, _dvt, _soluong, _sovb, _ngayvb, _loaivb, _namcapnhat, _phannhom1, _phannhom2, _phannhom3, _day, _month, _year, _mahuyen);
$$ LANGUAGE SQL;
-- CALL AddPhuongTienHuyDong()

-- DROP PROCEDURE EditPhuongTienHuyDong;
CREATE PROCEDURE EditPhuongTienHuyDong(
    _objectid integer,
    _tenphuongtienttb character varying,
    _dvql character varying,
    _dvt character varying,
    _soluong double precision,
    _sovb character varying,
    _ngayvb timestamp without time zone,
    _loaivb character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _phannhom1 character varying,
    _phannhom2 character varying,
    _phannhom3 character varying,
    _day integer,
    _month integer,
    _year integer,
	_mahuyen character varying
)
AS $$
	UPDATE PhuongTienHuyDong
	SET tenphuongtienttb=_tenphuongtienttb, dvql=_dvql, dvt=_dvt, soluong=_soluong, sovb=_sovb, ngayvb=_ngayvb, loaivb=_loaivb, namcapnhat=_namcapnhat, phannhom1=_phannhom1, phannhom2=_phannhom2, phannhom3=_phannhom3, day=_day, month=_month, year=_year, mahuyen = _mahuyen
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditPhuongTienHuyDong()

-- DROP PROCEDURE DeletePhuongTienHuyDong;
CREATE PROCEDURE DeletePhuongTienHuyDong(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM PhuongTienHuyDong WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeletePhuongTienHuyDong(592)

-- DROP PROCEDURE AddLucLuongHuyDong;
CREATE PROCEDURE AddLucLuongHuyDong(
	_objectid integer,
	_idkhlucluong Character varying,
	_sovb character varying,
	_ngayvb timestamp without time zone,
	_loaivb character varying,
	_qhtp character varying,
	_tenlucluong character varying,
	_capql character varying,
	_slnguoihd integer,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_namsudung integer,
	_mahuyen character varying,
	_day integer,
	_month integer,
	_year integer
)
AS $$
	INSERT INTO LucLuongHuyDongDetail (objectid, idkhlucluong, sovb, ngayvb, loaivb, qhtp, tenlucluong, capql, slnguoihd, namcapnhat, namsudung, mahuyen, day, month, year)
	VALUES (_objectid, _idkhlucluong, _sovb, _ngayvb, _loaivb, _qhtp, _tenlucluong, _capql, _slnguoihd, _namcapnhat, _namsudung, _mahuyen, _day, _month, _year);
$$ LANGUAGE SQL;
-- CALL AddLucLuongHuyDong()

-- DROP PROCEDURE EditLucLuongHuyDong;
CREATE PROCEDURE EditLucLuongHuyDong(
	_objectid integer,
	_sovb character varying,
	_ngayvb timestamp without time zone,
	_loaivb character varying,
	_qhtp character varying,
	_tenlucluong character varying,
	_capql character varying,
	_slnguoihd integer,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_namsudung integer,
	_mahuyen character varying,
	_day integer,
	_month integer,
	_year integer
)
AS $$	
	UPDATE LucLuongHuyDongDetail
	SET sovb=_sovb, ngayvb=_ngayvb, loaivb=_loaivb, qhtp = _qhtp, tenlucluong=_tenlucluong, capql = _capql, slnguoihd = _slnguoihd, namcapnhat=_namcapnhat, namsudung=_namsudung, mahuyen=_mahuyen, day=_day, month=_month, year=_year
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditLucLuongHuyDong()

-- DROP PROCEDURE DeleteLucLuongHuyDong;
CREATE PROCEDURE DeleteLucLuongHuyDong(
    _objectid int
)
AS $$
	DELETE FROM LucLuongHuyDongDetail WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteLucLuongHuyDong(205)

-- DROP PROCEDURE AddDanhBaDT;
CREATE PROCEDURE AddDanhBaDT(
	_objectid integer,
	_iddanhba character varying,
	_quanhuyen character varying,
	_hoten character varying,
	_cvcoquan character varying,
	_cvbch character varying,
	_dtcoquan character varying,
	_dtdidong character varying,
	_fax character varying,
	_mahuyen character varying,
	_namcapnhat integer
-- 	_ghichu character varying
)
AS $$
	BEGIN
		INSERT INTO DanhBaDT (objectid, iddanhba, quanhuyen, hoten, cvcoquan, cvbch, dtcoquan, dtdidong, fax, mahuyen, namcapnhat)
		VALUES (_objectid, _iddanhba, _quanhuyen, _hoten, _cvcoquan, _cvbch, _dtcoquan, _dtdidong, _fax, _mahuyen, _namcapnhat);
-- 		VALUES (_objectid, CONCAT('DBDT', (SELECT MAX(Objectid) FROM DanhBaDT) + 1), _quanhuyen, _hoten, _cvcoquan, _cvbch, _dtcoquan, _dtdidong, _fax, _mahuyen, _namcapnhat, _ghichu);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDanhBaDT()

-- DROP PROCEDURE EditDanhBaDT;
CREATE PROCEDURE EditDanhBaDT(
-- 	_username character varying,
	_objectid integer,
	_quanhuyen character varying,
	_hoten character varying,
	_cvcoquan character varying,
	_cvbch character varying,
	_dtcoquan character varying,
	_dtdidong character varying,
	_fax character varying,
	_mahuyen character varying,
	_namcapnhat integer
-- 	_ghichu character varying
)
AS $$
	BEGIN		
		UPDATE DanhBaDT
		SET quanhuyen = _quanhuyen, hoten = _hoten, cvcoquan = _cvcoquan, cvbch = _cvbch, dtcoquan = _dtcoquan, dtdidong = _dtdidong, fax = _fax, mahuyen = _mahuyen, namcapnhat = _namcapnhat
-- 		SET quanhuyen = _quanhuyen, hoten = _hoten, cvcoquan = _cvcoquan, cvbch = _cvbch, dtcoquan = _dtcoquan, dtdidong = _dtdidong, fax = _fax, mahuyen = _mahuyen, namcapnhat = _namcapnhat, ghichu = _ghichu
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDanhBaDT()

-- DROP PROCEDURE DeleteDanhbaDT;
CREATE PROCEDURE DeleteDanhbaDT(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM DanhbaDT WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeleteDanhbaDT(592)

-- DROP PROCEDURE AddHuongDiChuyen;
CREATE PROCEDURE AddHuongDiChuyen(
	_objectid integer,
	_iddichuyen character varying,
	_chieudai double precision,
	_tenhuong character varying,
	_khuvuc character varying,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_mahuyen character varying
)
AS $$
	BEGIN
		INSERT INTO HuongDiChuyen(objectid, iddichuyen, chieudai, tenhuong, khuvuc, namcapnhat, mahuyen)
		VALUES (_objectid, CONCAT('HDC', (SELECT MAX(Objectid) FROM HuongDiChuyen) + 1), _chieudai, _tenhuong, _khuvuc, _namcapnhat, _mahuyen);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddHuongDiChuyen()

-- DROP PROCEDURE EditHuongDiChuyen;
CREATE PROCEDURE EditHuongDiChuyen(
	_objectid integer ,
	_chieudai double precision,
	_tenhuong character varying,
	_khuvuc character varying,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_mahuyen character varying
)
AS $$
	BEGIN		
		UPDATE HuongDiChuyen
		SET chieudai = _chieudai, tenhuong = _tenhuong, khuvuc = _khuvuc, namcapnhat = _namcapnhat, mahuyen = _mahuyen
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditHuongDiChuyen()

-- DROP PROCEDURE DeleteHuongDiChuyen;
CREATE PROCEDURE DeleteHuongDiChuyen(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM HuongDiChuyen WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeleteHuongDiChuyen(592)

-- DROP PROCEDURE AddDiemAnToan;
CREATE PROCEDURE AddDiemAnToan(
	_objectid int,
	_idantoan character varying,
	_vitri character varying,
	_toadox double precision,
    _toadoy double precision,
	_succhua integer,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_phuongan character varying
)
AS $$
	DECLARE 
		-- 900914: HCM-VN2000
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO DiemAnToan(objectid, shape, idantoan, vitri, toadox, toadoy, succhua, maxa, mahuyen, namcapnhat, ghichu, phuongan)
		VALUES (_objectid, geom::geometry, _idantoan, _vitri, _toadox, _toadoy, _succhua, _maxa, _mahuyen, _namcapnhat, _ghichu, _phuongan);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDiemAnToan()

-- DROP PROCEDURE EditDiemAnToan;
CREATE PROCEDURE EditDiemAnToan(
	_objectid int,
	_vitri character varying,
	_toadox double precision,
    _toadoy double precision,
	_succhua integer,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_phuongan character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text);
	BEGIN
		UPDATE DiemAnToan
		SET shape=geom::geometry, vitri=_vitri, toadox=_toadox, toadoy=_toadoy, succhua=_succhua, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, phuongan=_phuongan
		WHERE objectid = _objectid;	
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDiemAnToan()

-- DROP PROCEDURE DeleteDiemAnToan;
CREATE PROCEDURE DeleteDiemAnToan(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM DiemAnToan WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeleteDiemAnToan(205)

-- DROP PROCEDURE AddDiemXungYeu;
CREATE PROCEDURE AddDiemXungYeu(
	_objectid int,
	_idxungyeu character varying,
	_vitri character varying,
	_toadox double precision,
    _toadoy double precision,
	_sodan int,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_phuongan character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text);
	BEGIN
		INSERT INTO DiemXungYeu(objectid, shape, idxungyeu, vitri, toadox, toadoy, sodan, maxa, mahuyen, namcapnhat, ghichu, phuongan)
		VALUES (_objectid, geom::geometry, _idxungyeu, _vitri, _toadox, _toadoy, _sodan, _maxa, _mahuyen, _namcapnhat, _ghichu, _phuongan);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDiemXungYeu()

-- DROP PROCEDURE EditDiemXungYeu;
CREATE PROCEDURE EditDiemXungYeu(
	_objectid int,
	_vitri character varying,
	_toadox double precision,
    _toadoy double precision,
	_sodan int,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_phuongan character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text);
	BEGIN
		UPDATE DiemXungYeu
		SET shape=geom::geometry, vitri=_vitri, toadox=_toadox, toadoy=-toadoy, sodan=_sodan, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, phuongan=_phuongan
		WHERE objectid = _objectid;	
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDiemXungYeu()

-- DROP PROCEDURE DeleteDiemXungYeu;
CREATE PROCEDURE DeleteDiemXungYeu(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM DiemXungYeu WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeleteDiemXungYeu(205)

-- DROP PROCEDURE AddDuKienDiDan;
CREATE PROCEDURE AddDuKienDiDan(
	_objectid int,
	_idkhsotan character varying,
	_sovb character varying,
	_ngayvb timestamp without time zone,
	_loaivb character varying,
	_quanhuyen character varying,
	_mahuyen character varying,
	_sophuongdidan integer,
	_soho_bao8_9 integer,
	_songuoi_bao8_9 integer,
	_soho_bao10_13 integer,
	_songuoi_bao10_13 integer,
	_namcapnhat smallint,
	_sohocandidoi integer,
	_day int,
	_month int,
	_year int
)
AS $$
	BEGIN
		INSERT INTO DuKienDiDan(objectid, idkhsotan, sovb, ngayvb, loaivb, quanhuyen, mahuyen, sophuongdidan, soho_bao8_9, songuoi_bao8_9, soho_bao10_13, songuoi_bao10_13, namcapnhat, sohocandidoi, day, month, year)
		VALUES (_objectid, _idkhsotan, _sovb, _ngayvb, _loaivb, _quanhuyen, _mahuyen, _sophuongdidan, _soho_bao8_9, _songuoi_bao8_9, _soho_bao10_13, _songuoi_bao10_13, _namcapnhat, _sohocandidoi, _day, _month, _year);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDuKienDiDan()

-- DROP PROCEDURE EditDuKienDiDan;
CREATE PROCEDURE EditDuKienDiDan(
	_objectid int,
	_sovb character varying,
	_ngayvb timestamp without time zone,
	_loaivb character varying,
	_quanhuyen character varying,
	_mahuyen character varying,
	_sophuongdidan integer,
	_soho_bao8_9 integer,
	_songuoi_bao8_9 integer,
	_soho_bao10_13 integer,
	_songuoi_bao10_13 integer,
	_namcapnhat smallint,
	_sohocandidoi integer,
	_day int,
	_month int,
	_year int
)
AS $$
	BEGIN
		UPDATE DuKienDiDan
		SET sovb=_sovb, ngayvb=_ngayvb, loaivb=_loaivb, quanhuyen=_quanhuyen, mahuyen=_mahuyen, sophuongdidan=_sophuongdidan, soho_bao8_9=_soho_bao8_9, songuoi_bao8_9=_songuoi_bao8_9, soho_bao10_13=_soho_bao10_13, songuoi_bao10_13=_songuoi_bao10_13, namcapnhat=_namcapnhat, sohocandidoi=_sohocandidoi, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDuKienDiDan()

-- DROP PROCEDURE DeleteDuKienDiDan;
CREATE PROCEDURE DeleteDuKienDiDan(
    _objectid int
)
AS $$
	DELETE FROM DuKienDiDan WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteDuKienDiDan(205)

-- DROP PROCEDURE AddLocXoay;
CREATE PROCEDURE AddLocXoay(
    _objectid integer,
    _idlocxoay character varying,
    _tenlocxoay character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying
	_day int,
	_month int,
	_year int
)
AS $$
	DECLARE 
-- 		SELECT ST_Transform(ST_GeomFromText('POINT(619529.824 1203062.514)', 900914), 4326)
-- 		SELECT ST_Transform(ST_SetSrid(ST_MakePoint(619529.824, 1203062.514), 900914), 4326)
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO LocXoay(objectid, shape, idlocxoay, tenlocxoay, gio, ngay, toadox, toadoy, apsuat, tocdogio, vitri, maxa, mahuyen, namcapnhat, day, month, year)
		VALUES (_objectid, geom::geometry, _idlocxoay, _tenlocxoay, _gio, _ngay, _toadox, _toadoy, _apsuat, _tocdogio, _vitri, _maxa, _mahuyen, _namcapnhat, _day, _month, _year);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddLocXoay(113::int, 'LX113'::character varying, 'test'::character varying, 'test'::character varying, '2023-01-01'::timestamp without time zone, 1172184.13::double precision, 606964.79::double precision, 45::double precision, 45::double precision, 'test'::character varying, '787'::character varying, '787'::character varying, 123::smallint, 1::int, 1::int, 1::int)

-- DROP PROCEDURE EditLocXoay;
CREATE PROCEDURE EditLocXoay(
    _objectid integer,
    _tenlocxoay character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying
	_day int,
	_month int,
	_year int
)
AS $$	
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		UPDATE LocXoay
		SET shape = geom::geometry, tenlocxoay=_tenlocxoay, gio=_gio, ngay=_ngay, apsuat=_apsuat, tocdogio=_tocdogio, vitri=_vitri, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditLocXoay()

-- DROP PROCEDURE DeleteLocXoay;
CREATE PROCEDURE DeleteLocXoay(
    _objectid int
)
AS $$
	BEGIN
		DELETE FROM LocXoay WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL DeleteLocXoay(205)

-- DROP PROCEDURE AddHistory;
CREATE PROCEDURE AddHistory(
	_id character varying,
	_tablename character varying,
	_rowid character varying,
	_username character varying,
	_operation character varying
)
AS $$
	BEGIN
-- 		DanhBaDT
		IF (_tablename = 'DanhBaDT') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT iddanhba AS "Mã danh bạ điện thoại", COALESCE(quanhuyen, '') AS "Quận/Huyện/Tp", COALESCE(hoten, '') AS "Họ tên", COALESCE(cvcoquan, '') AS "Chức vụ cơ quan", COALESCE(cvbch, '') AS "Chức vụ Ban chỉ huy", COALESCE(dtcoquan, '') AS "Số điện thoại cơ quan", COALESCE(dtdidong, '') AS "Số điện thoại di động", COALESCE(fax, '') AS "Fax", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật" FROM DanhBaDT WHERE iddanhba = _rowid) a));	
		END IF;
-- 		DiemXungYeu
		IF (_tablename = 'DiemXungYeu') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idxungyeu AS "Mã vị trí xung yếu", COALESCE(vitri, '') AS "Vị trí", COALESCE(toadox::Character varying, '') AS "Tọa độ x", COALESCE(toadoy::Character varying, '') AS "Tọa độ y", COALESCE(sodan::Character varying, '') AS "Số dân", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(phuongan, '') AS "Phương án" FROM Diemxungyeu WHERE idxungyeu = _rowid) a));	
		END IF;
-- 		DiemAnToan
		IF (_tablename = 'DiemAnToan') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idantoan AS "Mã vị trí an toàn", COALESCE(vitri, '') AS "Vị trí", COALESCE(toadox::Character varying, '') AS "Tọa độ x", COALESCE(toadoy::Character varying, '') AS "Tọa độ y", COALESCE(succhua::Character varying, '') AS "Sức chứa", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(phuongan, '') AS "Phương án" FROM DiemAnToan WHERE idantoan = _rowid) a));	
		END IF;
-- 		DuKienDiDan
		IF (_tablename = 'DuKienDiDan') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idkhsotan AS "Mã kế hoạch dự kiến di dời, sơ tán dân", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(quanhuyen, '') AS "Quận huyện", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(sophuongdidan::Character varying, '') AS "Số phường di dân", COALESCE(soho_bao8_9::Character varying, '') AS "Số hộ di dời bão cấp 8, 9", COALESCE(songuoi_bao8_9::Character varying, '') AS "Số người di dời bão cấp 8, 9", COALESCE(soho_bao10_13::Character varying, '') AS "Số hộ di dời bão cấp 10, 13", COALESCE(songuoi_bao10_13::Character varying, '') AS "Số người di dời bão cấp 10, 13", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(sohocandidoi::Character varying, '') AS "Số hộ cần di dời" FROM DuKienDiDan WHERE idkhsotan = _rowid) a));	
		END IF;
-- 		LucLuongHuyDong
		IF (_tablename = 'LucLuongHuyDong') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idkhlucluong AS "Mã kế hoạch lực lượng, dự kiến huy động", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(qhtp, '') AS "Quận/Huyện/Tp", COALESCE(tenlucluong, '') AS "Tên lực lượng", COALESCE(capql, '') AS "Cấp quản lý", COALESCE(slnguoihd::Character varying, '') AS "Số lượng người huy động", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(namsudung::Character varying, '') AS "Năm sử dụng", COALESCE(mahuyen, '') AS "Mã huyện" FROM LucLuongHuyDongDetail WHERE idkhlucluong = _rowid) a));	
		END IF;
-- 		PhuongTienHuyDong
		IF (_tablename = 'PhuongTienHuyDong') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idkhphuogtien AS "Mã kế hoạch phương tiện, trang thiết bị", COALESCE(tenphuongtienttb, '') AS "Tên phương tiện, trang thiết bị", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(dvt, '') AS "Đơn vị tính", COALESCE(soluong::Character varying, '') AS "Số lượng", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(phannhom1, '') AS "Phân nhóm 1", COALESCE(phannhom2, '') AS "Phân nhóm 2", COALESCE(phannhom3, '') AS "Phân nhóm 3", COALESCE(mahuyen, '') AS "Mã huyện" FROM PhuongtienHuyDong WHERE idkhphuogtien = _rowid) a));	
		END IF;
-- 		TuLieuHinhAnh
		IF (_tablename = 'TuLieuHinhAnh') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idhinhanh AS "Mã tư liệu hình ảnh", COALESCE(tenhinhanh, '') AS "Tên hình ảnh", COALESCE(ngayhinhanh::Character varying, '') AS "Ngày hình ảnh", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật" FROM TuLieuHinhAnh WHERE idhinhanh = _rowid) a));	
		END IF;
-- 		TuLieuVideo
		IF (_tablename = 'TuLieuVideo') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idvideo AS "Mã tư liệu video", COALESCE(tenvideo, '') AS "Tên video", COALESCE(ngayvideo::Character varying, '') AS "Ngày video", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật" FROM TuLieuVideo WHERE idvideo = _rowid) a));	
		END IF;
-- 		TuLieuKhac
		IF (_tablename = 'TuLieuKhac') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idtulieu AS "Mã tư liệu khác", COALESCE(tentulieu, '') AS "Tên tư liệu", COALESCE(ngaytulieu::Character varying, '') AS "Ngày tư liệu", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật" FROM TuLieuKhac WHERE idtulieu = _rowid) a));	
		END IF;
-- 		LocXoay
		IF (_tablename = 'LocXoay') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idlocxoay AS "Mã lốc xoáy", COALESCE(tenlocxoay, '') AS "Tên lốc xoáy", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(apsuat::character varying, '') AS "Áp suất", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM LocXoay WHERE idlocxoay = _rowid) a));	
		END IF;
-- 		DoMan
		IF (_tablename = 'DoMan') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idtramman AS "Mã trạm mặn", COALESCE(tentram, '') AS "Tên trạm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(tensong, '') AS "Tên sông", COALESCE(doman::character varying, '') AS "Độ mặn", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" FROM DoMan WHERE idtramman = _rowid) a));	
		END IF;
-- 		NangNong
		IF (_tablename = 'NangNong') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idtramkt AS "Mã trạm đo nắng nóng", COALESCE(tentram, '') AS "Tên trạm", COALESCE(captram, '') AS "Cấp trạm", COALESCE(vitritram, '') AS "Vị trí trạm", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(thang::character varying, '') AS "Tháng", COALESCE(sogionang::character varying, '') AS "Số giờ nắng", COALESCE(nhietdomin::character varying, '') AS "Nhiệt độ min", COALESCE(nhietdomax::character varying, '') AS "Nhiệt độ max", COALESCE(nhietdotb::character varying, '') AS "Nhiệt độ trung bình", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(ngay::character varying, '') AS "Ngày" FROM NangNong WHERE idtramkt = _rowid) a));	
		END IF;
-- 		Mua
		IF (_tablename = 'Mua') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idtrammua AS "Mã trạm đo mưa", COALESCE(tentram, '') AS "Tên trạm", COALESCE(captram, '') AS "Cấp trạm", COALESCE(vitritram, '') AS "Vị trí trạm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(luongmua::character varying, '') AS "Lượng mưa", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" FROM Mua WHERE idtrammua = _rowid) a));	
		END IF;
-- 		MucNuoc
		IF (_tablename = 'MucNuoc') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idtrammucnuoc AS "Mã trạm đo mực nước", COALESCE(tentram, '') AS "Tên trạm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(mucnuoc::character varying, '') AS "Mực nước", COALESCE(docaodinhtrieu::character varying, '') AS "Độ cao đỉnh triều", COALESCE(docaochantrieu::character varying, '') AS "Độ cao chân triều", COALESCE(baodongi::character varying, '') AS "Báo động i", COALESCE(baodongii::character varying, '') AS "Báo động ii", COALESCE(baodongiii::character varying, '') AS "Báo động iii", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" FROM MucNuoc WHERE idtrammucnuoc = _rowid) a));	
		END IF;
-- 		HoChua
		IF (_tablename = 'HoChua') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idhochua AS "Mã hồ chứa", COALESCE(ten, '') AS "Tên", COALESCE(loaiho, '') AS "Loại hồ", COALESCE(vitri, '') AS "Vị trí", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(h::character varying, '') AS "Độ cao mực nước hồ (m)", COALESCE(w::character varying, '') AS "Thể tích nước ở hồ (10⁶m³)", COALESCE(qvh::character varying, '') AS "Lưu lượng nước về hồ (m³/s)", COALESCE(qxa::character varying, '') AS "Lưu lượng xả (m³/s)", COALESCE(qcsi::character varying, '') AS "Lưu lượng cửa số 1 (bằng Qphát) (m³/s)", COALESCE(qcsii::character varying, '') AS "Lưu lượng cửa số 2 (m³/s)", COALESCE(qcsiii::character varying, '') AS "Lưu lượng cửa số 3 (m³/s)", COALESCE(qtb::character varying, '') AS "Lưu lượng nước trung bình (m³/s)", COALESCE(bh::character varying, '') AS "Bốc hơi (mm)", COALESCE(r::character varying, '') AS "Lượng mưa đo ở hồ (mm)", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM HoChua WHERE idhochua = _rowid) a));	
		END IF;
-- 		CongDap
		IF (_tablename = 'CongDap') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idcongdap AS "Mã cống đập", COALESCE(tencongdap, '') AS "Tên cống đập", COALESCE(lytrinh, '') AS "Lý trình", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(cumcongtrinh, '') AS "Cụm công trình", COALESCE(goithau, '') AS "Gói thầu", COALESCE(loaicongtrinh, '') AS "Loại công trình", COALESCE(hinhthuc, '') AS "Hình thức", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(duongkinh::character varying, '') AS "Đường kính", COALESCE(berong::character varying, '') AS "Bề rộng", COALESCE(chieucao::character varying, '') AS "Chiều cao", COALESCE(socua::character varying, '') AS "Số cửa", COALESCE(caotrinhdaycong, '') AS "Cao trình đáy cống", COALESCE(caotrinhdinhcong, '') AS "Cao trình đỉnh cống", COALESCE(hinhthucvanhanh, '') AS "Hình thức vận hành", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM CongDap WHERE idcongdap = _rowid) a));	
		END IF;
-- 		MocCanhBaoTrieuCuong
		IF (_tablename = 'MocCanhBaoTrieuCuong') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idmoccbtc AS "Mã mốc cảnh báo triều cường", COALESCE(tenmoc, '') AS "Tên mốc", COALESCE(giatri, '') AS "Giá trị", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namsudung::character varying, '') AS "Năm sử dụng", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM MocCanhBaoTrieuCuong WHERE idmoccbtc = _rowid) a));	
		END IF;
-- 		BienCanhBaoSatLo
		IF (_tablename = 'BienCanhBaoSatLo') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idbcbsl AS "Mã biển cảnh báo sạt lở", COALESCE(sohieubien, '') AS "Số hiệu biển", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(vitrisatlo, '') AS "Vị trí sạt lở", COALESCE(phamvi, '') AS "Phạm vi", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namxaydung::character varying, '') AS "Năm xây dựng", COALESCE(hinhanh, '') AS "Hình ảnh", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(tuyensr, '') AS "Tuyến sông, rạch" FROM BienCanhBaoSatLo WHERE idbcbsl = _rowid) a));	
		END IF;
-- 		KhuNeoDau
		IF (_tablename = 'KhuNeoDau') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idknd AS "Mã khu neo đậu", COALESCE(ten, '') AS "Tên", COALESCE(diachi, '') AS "Địa chỉ", COALESCE(kinhdodd::character varying, '') AS "Kinh độ điểm đầu", COALESCE(vidodd::character varying, '') AS "Vĩ độ điểm đầu", COALESCE(kinhdodc::character varying, '') AS "Kinh độ điểm cuối", COALESCE(vidodc::character varying, '') AS "Vĩ độ điểm cuối", COALESCE(dosaunuoc::character varying, '') AS "Độ sâu nước", COALESCE(succhua, '') AS "Sức chứa", COALESCE(coloaitau, '') AS "Cỡ, loại tàu", COALESCE(vitrivl, '') AS "Vị trí bắt đầu vào luồng", COALESCE(huongluong, '') AS "Hướng luồng", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(sdt, '') AS "Số điện thoại", COALESCE(tansoll, '') AS "Tần số liên lạc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM KhuNeoDau WHERE idknd = _rowid) a));	
		END IF;
-- 		ThietHai_ThienTai
		IF (_tablename = 'ThietHai_ThienTai') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idthiethai AS "Mã thiệt hại do thiên tai", COALESCE(loaithientai, '') AS "Loại thiên tai", COALESCE(doituongthiethai, '') AS "Đối tượng thiệt hại", COALESCE(motathiethai, '') AS "Mô tả thiệt hại", COALESCE(dvtthiethai::character varying, '') AS "Đơn vị tính thiệt hại", COALESCE(soluong::character varying, '') AS "Số lượng", COALESCE(giatrithiethai::character varying, '') AS "Giá trị thiệt hại", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM ThietHai_ThienTai WHERE idthiethai = _rowid) a));	
		END IF;
-- 		SatLo_Line
		IF (_tablename = 'SatLo_Line') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idsatlol AS "Mã tuyến sạt lở", COALESCE(vitri, '') AS "Vị trí", COALESCE(tuyensong, '') AS "Tuyến sông", COALESCE(capsong, '') AS "Cấp sông", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(chieurong::character varying, '') AS "Chiều rộng", COALESCE(mucdo, '') AS "Mức độ", COALESCE(tinhtrang, '') AS "Tình trạng", COALESCE(anhhuong, '') AS "Ảnh hưởng", COALESCE(khoangcachah, '') AS "Khoảng cách ảnh hưởng", COALESCE(ditichah::character varying, '') AS "Diện tích ảnh hưởng", COALESCE(sohoah::character varying, '') AS "Số hộ dân bị ảnh hưởng", COALESCE(songuoiah::character varying, '') AS "Số người bị ảnh hưởng", COALESCE(hatangah, '') AS "Hạ tầng ảnh hưởng", COALESCE(congtrinhchongsl, '') AS "Công trình chống sạt lở", COALESCE(chudautu, '') AS "Chủ đầu tư", COALESCE(tenduan, '') AS "Tên dự án", COALESCE(quymoduan, '') AS "Quy mô dự án", COALESCE(tongmucduan::character varying, '') AS "Tổng mức đầu tư dự án", COALESCE(tiendothuchien, '') AS "Tiến độ thực hiện", COALESCE(nguongoc, '') AS "Nguồn gốc dữ liệu", COALESCE(dubao, '') AS "Dự báo", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(ctxdke, '') AS "Chủ trương đầu tư dự án kè chống sạt lở" FROM SatLo_Line WHERE idsatlol = _rowid) a));	
		END IF;
-- 		Ke
		IF (_tablename = 'Ke') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idke AS "Mã kè", COALESCE(tenkenhmuong, '') AS "Tên kênh mương", COALESCE(vitri, '') AS "Vị trí", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(caotrinhdaykenh::character varying, '') AS "Cao trình đáy kênh", COALESCE(berongkenh::character varying, '') AS "Bề rộng kênh", COALESCE(hesomai::character varying, '') AS "Hệ số mái", COALESCE(caotrinhbotrai, '') AS "Cao trình bờ trái", COALESCE(caotrinhbophai, '') AS "Cao trình bờ phải", COALESCE(berongbotrai, '') AS "Bề rộng bờ trái (m)", COALESCE(berongbophai, '') AS "Bề rộng bờ phải (m)", COALESCE(hanhlangbaove, '') AS "Hành lang bảo vệ", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(cumcongtrinh, '') AS "Cụm công trình", COALESCE(ketcaucongtrinh, '') AS "Kết cấu công trình", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM Ke WHERE idke = _rowid) a));	
		END IF;
-- 		ApThapNhietDoi
		IF (_tablename = 'ApThapNhietDoi') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idapthap AS "Mã áp thấp nhiệt đới", COALESCE(tenapthap, '') AS "Tên áp thấp nhiệt đới", COALESCE(gio::character varying, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(apsuat::character varying, '') AS "Áp suất (mb)", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió (kt)", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(ngaybatdau::character varying, '') AS "Ngày bắt đầu áp thấp", COALESCE(ngayketthuc::character varying, '') AS "Ngày kết thúc áp thấp", COALESCE(centerid, '') AS "Trung tâm phát bão", COALESCE(tenvn, '') AS "Tên Việt Nam của áp thấp", COALESCE(kvahhcm, '') AS "Ảnh hưởng trực tiếp đến thành phố Hồ Chí Minh" FROM ApThapNhietDoi WHERE idapthap = _rowid) a));	
		END IF;
-- 		Bao
		IF (_tablename = 'Bao') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idbao AS "Mã bão", COALESCE(tenbao, '') AS "Tên bão", COALESCE(gio::character varying, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(apsuat::character varying, '') AS "Áp suất (mb)", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió (kt)", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(capbao, '') AS "Cấp bão", COALESCE(ngaybatdau::character varying, '') AS "Ngày bắt đầu bão", COALESCE(ngayketthuc::character varying, '') AS "Ngày kết thúc bão", COALESCE(centerid, '') AS "Trung tâm phát bão", COALESCE(tenvn, '') AS "Tên Việt Nam của bão", COALESCE(kvahhcm, '') AS "Ảnh hưởng trực tiếp đến thành phố Hồ Chí Minh" FROM Bao WHERE idbao = _rowid) a));	
		END IF;
-- 		DeBaoBoBao
		IF (_tablename = 'DeBaoBoBao') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idkenhmuong AS "Mã kênh mương", COALESCE(tenkenhmuong, '') AS "Tên kênh mương", COALESCE(vitri, '') AS "Vị trí", COALESCE(chieudai::character varying, '') AS "Chiều dài (m)", COALESCE(caotrinhdaykenh, '') AS "Cao trình đáy kênh", COALESCE(berongkenh, '') AS "Bề rộng kênh", COALESCE(hesomai, '') AS "Hệ số mái", COALESCE(caotrinhbotrai, '') AS "Cao trình bờ trái", COALESCE(caotrinhbophai, '') AS "Cao trình bờ phải", COALESCE(berongbotrai, '') AS "Bề rộng bờ trái (m)", COALESCE(berongbophai, '') AS "Bề rộng bờ phải (m)", COALESCE(hanhlangbaove, '') AS "Hành lang bảo vệ", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(ketcaucongtrinh, '') AS "Kết cấu công trình", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM DeBao_BoBao WHERE idkenhmuong = _rowid) a));	
		END IF;
-- 		ChayRung
		IF (_tablename = 'ChayRung') THEN	
			INSERT INTO History(id, tablename, rowid, username, operation, olddata)
			VALUES (_id, _tablename, _rowid, _username, _operation, (SELECT to_json(a.*) AS olddata FROM (SELECT idchay AS "Mã điểm cháy", COALESCE(ngay, '') AS "Ngày tháng", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(tgchay, '') AS "Thời gian cháy", COALESCE(tgdap, '') AS "Thời gian dập tắt", COALESCE(dtchay::character varying, '') AS "Diện tích cháy (ha)", COALESCE(hientrang, '') AS "Hiện trạng", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" FROM ChayRung WHERE idchay = _rowid) a));	
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- CALL AddHistory(800, 'Admin', 'INSERT');

-- DROP PROCEDURE EditHistory;
CREATE PROCEDURE EditHistory(
	_id character varying,
	_tablename character varying,
	_rowid character varying
)
AS $$
	BEGIN
-- 		DanhBaDT
		IF (_tablename = 'DanhBaDT') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT iddanhba AS "Mã danh bạ điện thoại", COALESCE(quanhuyen, '') AS "Quận/Huyện/Tp", COALESCE(hoten, '') AS "Họ tên", COALESCE(cvcoquan, '') AS "Chức vụ cơ quan", COALESCE(cvbch, '') AS "Chức vụ Ban chỉ huy", COALESCE(dtcoquan, '') AS "Số điện thoại cơ quan", COALESCE(dtdidong, '') AS "Số điện thoại di động", COALESCE(fax, '') AS "Fax", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật" 
								FROM danhbadt 
								WHERE iddanhba = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		DiemXungYeu
		IF (_tablename = 'DiemXungYeu') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idxungyeu AS "Mã vị trí xung yếu", COALESCE(vitri, '') AS "Vị trí", COALESCE(toadox::Character varying, '') AS "Tọa độ x", COALESCE(toadoy::Character varying, '') AS "Tọa độ y", COALESCE(sodan::Character varying, '') AS "Số dân", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(phuongan, '') AS "Phương án"
								FROM DiemXungYeu 
								WHERE idxungyeu = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		DiemAnToan
		IF (_tablename = 'DiemAnToan') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idantoan AS "Mã vị trí an toàn", COALESCE(vitri, '') AS "Vị trí", COALESCE(toadox::Character varying, '') AS "Tọa độ x", COALESCE(toadoy::Character varying, '') AS "Tọa độ y", COALESCE(succhua::Character varying, '') AS "Sức chứa", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(phuongan, '') AS "Phương án" 
								FROM DiemAnToan 
								WHERE idantoan = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		DuKienDiDan
		IF (_tablename = 'DuKienDiDan') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idkhsotan AS "Mã kế hoạch dự kiến di dời, sơ tán dân", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(quanhuyen, '') AS "Quận huyện", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(sophuongdidan::Character varying, '') AS "Số phường di dân", COALESCE(soho_bao8_9::Character varying, '') AS "Số hộ di dời bão cấp 8, 9", COALESCE(songuoi_bao8_9::Character varying, '') AS "Số người di dời bão cấp 8, 9", COALESCE(soho_bao10_13::Character varying, '') AS "Số hộ di dời bão cấp 10, 13", COALESCE(songuoi_bao10_13::Character varying, '') AS "Số người di dời bão cấp 10, 13", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(sohocandidoi::Character varying, '') AS "Số hộ cần di dời" 
								FROM DuKienDiDan 
								WHERE idkhsotan = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;	
-- 		LucLuongHuyDong
		IF (_tablename = 'LucLuongHuyDong') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idkhlucluong AS "Mã kế hoạch lực lượng, dự kiến huy động", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(qhtp, '') AS "Quận/Huyện/Tp", COALESCE(tenlucluong, '') AS "Tên lực lượng", COALESCE(capql, '') AS "Cấp quản lý", COALESCE(slnguoihd::Character varying, '') AS "Số lượng người huy động", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(namsudung::Character varying, '') AS "Năm sử dụng", COALESCE(mahuyen, '') AS "Mã huyện"
								FROM LucLuongHuyDongDetail 
								WHERE idkhlucluong = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		PhuongTienHuyDong
		IF (_tablename = 'PhuongTienHuyDong') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idkhphuogtien AS "Mã kế hoạch phương tiện, trang thiết bị", COALESCE(tenphuongtienttb, '') AS "Tên phương tiện, trang thiết bị", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(dvt, '') AS "Đơn vị tính", COALESCE(soluong::Character varying, '') AS "Số lượng", COALESCE(sovb, '') AS "Số văn bản", COALESCE(ngayvb::Character varying, '') AS "Ngày văn bản", COALESCE(loaivb, '') AS "Loại văn bản", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(phannhom1, '') AS "Phân nhóm 1", COALESCE(phannhom2, '') AS "Phân nhóm 2", COALESCE(phannhom3, '') AS "Phân nhóm 3", COALESCE(mahuyen, '') AS "Mã huyện"	
								FROM PhuongTienHuyDong 
								WHERE idkhphuogtien = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		TuLieuHinhAnh
		IF (_tablename = 'TuLieuHinhAnh') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idhinhanh AS "Mã tư liệu hình ảnh", COALESCE(tenhinhanh, '') AS "Tên hình ảnh", COALESCE(ngayhinhanh::Character varying, '') AS "Ngày hình ảnh", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật"
								FROM TuLieuHinhAnh 
								WHERE idhinhanh = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		TuLieuVideo
		IF (_tablename = 'TuLieuVideo') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idvideo AS "Mã tư liệu video", COALESCE(tenvideo, '') AS "Tên video", COALESCE(ngayvideo::Character varying, '') AS "Ngày video", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật"
								FROM TuLieuVideo
								WHERE idvideo = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		TuLieuKhac
		IF (_tablename = 'TuLieuKhac') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idtulieu AS "Mã tư liệu khác", COALESCE(tentulieu, '') AS "Tên tư liệu", COALESCE(ngaytulieu::Character varying, '') AS "Ngày tư liệu", COALESCE(noidung, '') AS "Nội dung", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(dvql, '') AS "Đơn vị quản lý", COALESCE(nguongoc, '') AS "Nguồn gốc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật"
								FROM TuLieuKhac
								WHERE idtulieu = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		LocXoay
		IF (_tablename = 'LocXoay') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idlocxoay AS "Mã lốc xoáy", COALESCE(tenlocxoay, '') AS "Tên lốc xoáy", COALESCE(gio) AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(apsuat::character varying, '') AS "Áp suất", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú"
								FROM LocXoay
								WHERE idlocxoay = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		DoMan
		IF (_tablename = 'DoMan') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idtramman AS "Mã trạm mặn", COALESCE(tentram, '') AS "Tên trạm", COALESCE(gio) AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(tensong, '') AS "Tên sông", COALESCE(doman::character varying, '') AS "Độ mặn", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" 
								FROM DoMan	
								WHERE idtramman = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		NangNong
		IF (_tablename = 'NangNong') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idtramkt AS "Mã trạm đo nắng nóng", COALESCE(tentram, '') AS "Tên trạm", COALESCE(captram, '') AS "Cấp trạm", COALESCE(vitritram, '') AS "Vị trí trạm", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(thang::character varying, '') AS "Tháng", COALESCE(sogionang::character varying, '') AS "Số giờ nắng", COALESCE(nhietdomin::character varying, '') AS "Nhiệt độ min", COALESCE(nhietdomax::character varying, '') AS "Nhiệt độ max", COALESCE(nhietdotb::character varying, '') AS "Nhiệt độ trung bình", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(ngay::character varying, '') AS "Ngày" 
								FROM NangNong								
								WHERE idtramkt = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		Mua
		IF (_tablename = 'Mua') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idtrammua AS "Mã trạm đo mưa", COALESCE(tentram, '') AS "Tên trạm", COALESCE(captram, '') AS "Cấp trạm", COALESCE(vitritram, '') AS "Vị trí trạm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(luongmua::character varying, '') AS "Lượng mưa", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" 
								FROM Mua								
								WHERE idtrammua = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		MucNuoc
		IF (_tablename = 'MucNuoc') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idtrammucnuoc AS "Mã trạm đo mực nước", COALESCE(tentram, '') AS "Tên trạm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(mucnuoc::character varying, '') AS "Mực nước", COALESCE(docaodinhtrieu::character varying, '') AS "Độ cao đỉnh triều", COALESCE(docaochantrieu::character varying, '') AS "Độ cao chân triều", COALESCE(baodongi::character varying, '') AS "Báo động i", COALESCE(baodongii::character varying, '') AS "Báo động ii", COALESCE(baodongiii::character varying, '') AS "Báo động iii", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ" 
								FROM MucNuoc								
								WHERE idtrammucnuoc = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		HoChua
		IF (_tablename = 'HoChua') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idhochua AS "Mã hồ chứa", COALESCE(ten, '') AS "Tên", COALESCE(loaiho, '') AS "Loại hồ", COALESCE(vitri, '') AS "Vị trí", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(h::character varying, '') AS "Độ cao mực nước hồ (m)", COALESCE(w::character varying, '') AS "Thể tích nước ở hồ (10⁶m³)", COALESCE(qvh::character varying, '') AS "Lưu lượng nước về hồ (m³/s)", COALESCE(qxa::character varying, '') AS "Lưu lượng xả (m³/s)", COALESCE(qcsi::character varying, '') AS "Lưu lượng cửa số 1 (bằng Qphát) (m³/s)", COALESCE(qcsii::character varying, '') AS "Lưu lượng cửa số 2 (m³/s)", COALESCE(qcsiii::character varying, '') AS "Lưu lượng cửa số 3 (m³/s)", COALESCE(qtb::character varying, '') AS "Lưu lượng nước trung bình (m³/s)", COALESCE(bh::character varying, '') AS "Bốc hơi (mm)", COALESCE(r::character varying, '') AS "Lượng mưa đo ở hồ (mm)", COALESCE(namcapnhat::Character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM HoChua								
								WHERE idhochua = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		CongDap
		IF (_tablename = 'CongDap') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idcongdap AS "Mã cống đập", COALESCE(tencongdap, '') AS "Tên cống đập", COALESCE(lytrinh, '') AS "Lý trình", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(cumcongtrinh, '') AS "Cụm công trình", COALESCE(goithau, '') AS "Gói thầu", COALESCE(loaicongtrinh, '') AS "Loại công trình", COALESCE(hinhthuc, '') AS "Hình thức", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(duongkinh::character varying, '') AS "Đường kính", COALESCE(berong::character varying, '') AS "Bề rộng", COALESCE(chieucao::character varying, '') AS "Chiều cao", COALESCE(socua::character varying, '') AS "Số cửa", COALESCE(caotrinhdaycong, '') AS "Cao trình đáy cống", COALESCE(caotrinhdinhcong, '') AS "Cao trình đỉnh cống", COALESCE(hinhthucvanhanh, '') AS "Hình thức vận hành", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM CongDap							
								WHERE idcongdap = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		MocCanhBaoTrieuCuong
		IF (_tablename = 'MocCanhBaoTrieuCuong') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idmoccbtc AS "Mã mốc cảnh báo triều cường", COALESCE(tenmoc, '') AS "Tên mốc", COALESCE(giatri, '') AS "Giá trị", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namsudung::character varying, '') AS "Năm sử dụng", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM MocCanhBaoTrieuCuong							
								WHERE idmoccbtc = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		BienCanhBaoSatLo
		IF (_tablename = 'BienCanhBaoSatLo') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idbcbsl AS "Mã biển cảnh báo sạt lở", COALESCE(sohieubien, '') AS "Số hiệu biển", COALESCE(toadox::character varying, '') AS "Tọa độ x", COALESCE(toadoy::character varying, '') AS "Tọa độ y", COALESCE(vitrisatlo, '') AS "Vị trí sạt lở", COALESCE(phamvi, '') AS "Phạm vi", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namxaydung::character varying, '') AS "Năm xây dụng", COALESCE(hinhanh, '') AS "Hình ảnh", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(tuyensr, '') AS "Tuyến sông, rạch" 
								FROM BienCanhBaoSatLo								
								WHERE idbcbsl = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		KhuNeoDau
		IF (_tablename = 'KhuNeoDau') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idknd AS "Mã khu neo đậu", COALESCE(ten, '') AS "Tên", COALESCE(diachi, '') AS "Địa chỉ", COALESCE(kinhdodd::character varying, '') AS "Kinh độ điểm đầu", COALESCE(vidodd::character varying, '') AS "Vĩ độ điểm đầu", COALESCE(kinhdodc::character varying, '') AS "Kinh độ điểm cuối", COALESCE(vidodc::character varying, '') AS "Vĩ độ điểm cuối", COALESCE(dosaunuoc::character varying, '') AS "Độ sâu nước", COALESCE(succhua, '') AS "Sức chứa", COALESCE(coloaitau, '') AS "Cỡ, loại tàu", COALESCE(vitrivl, '') AS "Vị trí bắt đầu vào luồng", COALESCE(huongluong, '') AS "Hướng luồng", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(sdt, '') AS "Số điện thoại", COALESCE(tansoll, '') AS "Tần số liên lạc", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM KhuNeoDau 								
								WHERE idknd = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		ThietHai_ThienTai
		IF (_tablename = 'ThietHai_ThienTai') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idthiethai AS "Mã thiệt hại do thiên tai", COALESCE(loaithientai, '') AS "Loại thiên tai", COALESCE(doituongthiethai, '') AS "Đối tượng thiệt hại", COALESCE(motathiethai, '') AS "Mô tả thiệt hại", COALESCE(dvtthiethai::character varying, '') AS "Đơn vị tính thiệt hại", COALESCE(soluong::character varying, '') AS "Số lượng", COALESCE(giatrithiethai::character varying, '') AS "Giá trị thiệt hại", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(gio, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú"
								FROM ThietHai_ThienTai							
								WHERE idthiethai = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		SatLo_Line
		IF (_tablename = 'SatLo_Line') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idsatlol AS "Mã tuyến sạt lở", COALESCE(vitri, '') AS "Vị trí", COALESCE(tuyensong, '') AS "Tuyến sông", COALESCE(capsong, '') AS "Cấp sông", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(chieurong::character varying, '') AS "Chiều rộng", COALESCE(mucdo, '') AS "Mức độ", COALESCE(tinhtrang, '') AS "Tình trạng", COALESCE(anhhuong, '') AS "Ảnh hưởng", COALESCE(khoangcachah, '') AS "Khoảng cách ảnh hưởng", COALESCE(ditichah::character varying, '') AS "Diện tích ảnh hưởng", COALESCE(sohoah::character varying, '') AS "Số hộ dân bị ảnh hưởng", COALESCE(songuoiah::character varying, '') AS "Số người bị ảnh hưởng", COALESCE(hatangah, '') AS "Hạ tầng ảnh hưởng", COALESCE(congtrinhchongsl, '') AS "Công trình chống sạt lở", COALESCE(chudautu, '') AS "Chủ đầu tư", COALESCE(tenduan, '') AS "Tên dự án", COALESCE(quymoduan, '') AS "Quy mô dự án", COALESCE(tongmucduan::character varying, '') AS "Tổng mức đầu tư dự án", COALESCE(tiendothuchien, '') AS "Tiến độ thực hiện", COALESCE(nguongoc, '') AS "Nguồn gốc dữ liệu", COALESCE(dubao, '') AS "Dự báo", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(ctxdke, '') AS "Chủ trương đầu tư dự án kè chống sạt lở" 
								FROM SatLo_Line								
								WHERE idsatlol = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		Ke
		IF (_tablename = 'Ke') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idke AS "Mã kè", COALESCE(tenkenhmuong, '') AS "Tên kênh mương", COALESCE(vitri, '') AS "Vị trí", COALESCE(chieudai::character varying, '') AS "Chiều dài", COALESCE(caotrinhdaykenh::character varying, '') AS "Cao trình đáy kênh", COALESCE(berongkenh::character varying, '') AS "Bề rộng kênh", COALESCE(hesomai::character varying, '') AS "Hệ số mái", COALESCE(caotrinhbotrai, '') AS "Cao trình bờ trái", COALESCE(caotrinhbophai, '') AS "Cao trình bờ phải", COALESCE(berongbotrai, '') AS "Bề rộng bờ trái (m)", COALESCE(berongbophai, '') AS "Bề rộng bờ phải (m)", COALESCE(hanhlangbaove, '') AS "Hành lang bảo vệ", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(cumcongtrinh, '') AS "Cụm công trình", COALESCE(ketcaucongtrinh, '') AS "Kết cấu công trình", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM Ke							
								WHERE idke = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		ApThapNhietDoi
		IF (_tablename = 'ApThapNhietDoi') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idapthap AS "Mã áp thấp nhiệt đới", COALESCE(tenapthap, '') AS "Tên áp thấp nhiệt đới", COALESCE(gio::character varying, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(apsuat::character varying, '') AS "Áp suất (mb)", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió (kt)", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(ngaybatdau::character varying, '') AS "Ngày bắt đầu áp thấp", COALESCE(ngayketthuc::character varying, '') AS "Ngày kết thúc áp thấp", COALESCE(centerid, '') AS "Trung tâm phát bão", COALESCE(tenvn, '') AS "Tên Việt Nam của áp thấp", COALESCE(kvahhcm, '') AS "Ảnh hưởng trực tiếp đến thành phố Hồ Chí Minh" 
								FROM ApThapNhietDoi						
								WHERE idapthap = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		Bao
		IF (_tablename = 'Bao') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idbao AS "Mã bão", COALESCE(tenbao, '') AS "Tên bão", COALESCE(gio::character varying, '') AS "Giờ", COALESCE(ngay::character varying, '') AS "Ngày", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(apsuat::character varying, '') AS "Áp suất (mb)", COALESCE(tocdogio::character varying, '') AS "Tốc độ gió (kt)", COALESCE(vitri, '') AS "Vị trí", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú", COALESCE(kinhdo::character varying, '') AS "Kinh độ", COALESCE(vido::character varying, '') AS "Vĩ độ", COALESCE(capbao, '') AS "Cấp bão", COALESCE(ngaybatdau::character varying, '') AS "Ngày bắt đầu bão", COALESCE(ngayketthuc::character varying, '') AS "Ngày kết thúc bão", COALESCE(centerid, '') AS "Trung tâm phát bão", COALESCE(tenvn, '') AS "Tên Việt Nam của bão", COALESCE(kvahhcm, '') AS "Ảnh hưởng trực tiếp đến thành phố Hồ Chí Minh" 
								FROM Bao				
								WHERE idbao = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		DeBaoBoBao
		IF (_tablename = 'DeBaoBoBao') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idkenhmuong AS "Mã kênh mương", COALESCE(tenkenhmuong, '') AS "Tên kênh mương", COALESCE(vitri, '') AS "Vị trí", COALESCE(chieudai::character varying, '') AS "Chiều dài (m)", COALESCE(caotrinhdaykenh, '') AS "Cao trình đáy kênh", COALESCE(berongkenh, '') AS "Bề rộng kênh", COALESCE(hesomai, '') AS "Hệ số mái", COALESCE(caotrinhbotrai, '') AS "Cao trình bờ trái", COALESCE(caotrinhbophai, '') AS "Cao trình bờ phải", COALESCE(berongbotrai, '') AS "Bề rộng bờ trái (m)", COALESCE(berongbophai, '') AS "Bề rộng bờ phải (m)", COALESCE(hanhlangbaove, '') AS "Hành lang bảo vệ", COALESCE(capcongtrinh, '') AS "Cấp công trình", COALESCE(ketcaucongtrinh, '') AS "Kết cấu công trình", COALESCE(muctieunhiemvu, '') AS "Mục tiêu nhiệm vụ", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(namsudung, '') AS "Năm sử dụng", COALESCE(hethongcttl, '') AS "Hệ thống CTTL", COALESCE(donviquanly, '') AS "Đơn vị quản lý", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM DeBao_BoBao			
								WHERE idkenhmuong = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
-- 		ChayRung
		IF (_tablename = 'ChayRung') THEN
			UPDATE History
			SET changedata = 
			(
		-- 		SELECT json_agg(json_build_object('Cột',old2.key,'Giá trị cũ',old2.value,'Giá trị mới',new2.value))
				SELECT STRING_AGG(CONCAT('Tên cột: ', a.okey, ': Giá trị cũ: "', a.oval, '", Giá trị mới: "', a.nval, '"'), '; ')
				FROM
				(
					SELECT old2.key as okey, old2.value as oval, new2.key as nkey, new2.value as nval
					FROM
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT olddata FROM History WHERE id = _id
						) a, json_each_text(a.olddata)
					) old2 
					JOIN 
					(
						SELECT Key::Character varying, Value::Character varying
						FROM
						(
							SELECT to_json(a.*) AS olddata
							FROM
							(
								SELECT idchay AS "Mã điểm cháy", COALESCE(ngay, '') AS "Ngày tháng", COALESCE(diadiem, '') AS "Địa điểm", COALESCE(toadox::character varying, '') AS "Tọa độ X", COALESCE(toadoy::character varying, '') AS "Tọa độ Y", COALESCE(tgchay, '') AS "Thời gian cháy", COALESCE(tgdap, '') AS "Thời gian dập tắt", COALESCE(dtchay::character varying, '') AS "Diện tích cháy (ha)", COALESCE(hientrang, '') AS "Hiện trạng", COALESCE(maxa, '') AS "Mã xã", COALESCE(mahuyen, '') AS "Mã huyện", COALESCE(namcapnhat::character varying, '') AS "Năm cập nhật", COALESCE(ghichu, '') AS "Ghi chú" 
								FROM ChayRung			
								WHERE idchay = _rowid
							) a
						) a, json_each_text(a.olddata)
					) new2
					ON old2.key = new2.key
					WHERE old2.value <> new2.value
				) a
			)
			WHERE id = _id;
		END IF;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditHistory();

-- DROP PROCEDURE DeleteHistory;
CREATE PROCEDURE DeleteHistory(
	_id character varying
)
AS $$
	DELETE FROM History WHERE id = _id;
$$ LANGUAGE SQL;
-- CALL DeleteHistory(3);

-- DROP PROCEDURE AddTuLieuHinhAnh;
CREATE PROCEDURE AddTuLieuHinhAnh(
	_objectid integer,
	_idhinhanh character varying,
	_tenhinhanh character varying,
	_ngayhinhanh timestamp without time zone,
	_noidung character varying,
	_diadiem character varying,
	_dvql character varying,
	_nguongoc character varying,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_day integer,
	_month integer,
	_year integer
)
AS $$
	INSERT INTO tulieuhinhanh(objectid, idhinhanh, tenhinhanh, ngayhinhanh, noidung, diadiem, dvql, nguongoc, maxa, mahuyen, namcapnhat, day, month, year)
	VALUES (_objectid, _idhinhanh, _tenhinhanh, _ngayhinhanh, _noidung, _diadiem, _dvql, _nguongoc, _maxa, _mahuyen, _namcapnhat, _day, _month, _year);
$$ LANGUAGE SQL;
-- CALL AddTuLieuHinhAnh()

-- DROP PROCEDURE EditTuLieuHinhAnh;
CREATE PROCEDURE EditTuLieuHinhAnh(
	_objectid integer,
	_tenhinhanh character varying,
	_ngayhinhanh timestamp without time zone,
	_noidung character varying,
	_diadiem character varying,
	_dvql character varying,
	_nguongoc character varying,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
-- 	_ghichu character varying,
	_day integer,
	_month integer,
	_year integer
)
AS $$
	UPDATE TuLieuHinhAnh
	SET tenhinhanh=_tenhinhanh, ngayhinhanh=_ngayhinhanh, noidung=_noidung, diadiem=_diadiem, dvql=_dvql, nguongoc=_nguongoc, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, day=_day, month=_month, year=_year
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditTuLieuHinhAnh()

-- DROP PROCEDURE DeleteTuLieuHinhAnh;
CREATE PROCEDURE DeleteTuLieuHinhAnh(
    _objectid int
)
AS $$
	DELETE FROM TuLieuHinhAnh WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteTuLieuHinhAnh(205)

-- DROP PROCEDURE AddTuLieuVideo;
CREATE PROCEDURE AddTuLieuVideo(
    _objectid integer,
    _idvideo character varying,
    _tenvideo character varying,
    _ngayvideo timestamp without time zone,
    _noidung character varying,
    _diadiem character varying,
    _dvql character varying,
    _nguongoc character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	INSERT INTO tulieuvideo(objectid, idvideo, tenvideo, ngayvideo, noidung, diadiem, dvql, nguongoc, maxa, mahuyen, namcapnhat, day, month, year)
	VALUES (_objectid, _idvideo, _tenvideo, _ngayvideo, _noidung, _diadiem, _dvql, _nguongoc, _maxa, _mahuyen, _namcapnhat, _day, _month, _year);
$$ LANGUAGE SQL;
-- CALL AddTuLieuVideo()

-- DROP PROCEDURE EditTuLieuVideo;
CREATE PROCEDURE EditTuLieuVideo(
    _objectid integer,
    _tenvideo character varying,
    _ngayvideo timestamp without time zone,
    _noidung character varying,
    _diadiem character varying,
    _dvql character varying,
    _nguongoc character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	UPDATE tulieuvideo
	SET tenvideo=_tenvideo, ngayvideo=_ngayvideo, noidung=_noidung, diadiem=_diadiem, dvql=_dvql, nguongoc=_nguongoc, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, day=_day, month=_month, year=_year
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditTuLieuVideo()

-- DROP PROCEDURE DeleteTuLieuVideo;
CREATE PROCEDURE DeleteTuLieuVideo(
    _objectid int
)
AS $$
	DELETE FROM TuLieuVideo WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteTuLieuVideo(205)

-- DROP PROCEDURE AddTuLieuKhac;
CREATE PROCEDURE AddTuLieuKhac(
    _objectid integer,
    _idtulieu character varying,
    _tentulieu character varying,
    _ngaytulieu timestamp without time zone,
    _noidung character varying,
    _diadiem character varying,
    _dvql character varying,
    _nguongoc character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	INSERT INTO TuLieuKhac(objectid, idtulieu, tentulieu, ngaytulieu, noidung, diadiem, dvql, nguongoc, maxa, mahuyen, namcapnhat, day, month, year)
	VALUES (_objectid, _idtulieu, _tentulieu, _ngaytulieu, _noidung, _diadiem, _dvql, _nguongoc, _maxa, _mahuyen, _namcapnhat, _day, _month, _year);
$$ LANGUAGE SQL;
-- CALL AddTuLieuKhac()

-- DROP PROCEDURE EditTuLieuKhac;
CREATE PROCEDURE EditTuLieuKhac(
    _objectid integer,
    _tentulieu character varying,
    _ngaytulieu timestamp without time zone,
    _noidung character varying,
    _diadiem character varying,
    _dvql character varying,
    _nguongoc character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
--     _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	UPDATE TuLieuKhac
	SET tentulieu=_tentulieu, ngaytulieu=_ngaytulieu, noidung=_noidung, diadiem=_diadiem, dvql=_dvql, nguongoc=_nguongoc, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, day=_day, month=_month, year=_year
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditTuLieuKhac()

-- DROP PROCEDURE DeleteTuLieuKhac;
CREATE PROCEDURE DeleteTuLieuKhac(
    _objectid int
)
AS $$
	DELETE FROM TuLieuKhac WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteTuLieuKhac(205)

-- DROP PROCEDURE AddSatLoPoint;
-- CREATE PROCEDURE AddSatLoPoint(
--     _objectid integer,
--     _idtulieu character varying,
--     _tentulieu character varying,
--     _ngaytulieu timestamp without time zone,
--     _noidung character varying,
--     _diadiem character varying,
--     _dvql character varying,
--     _nguongoc character varying,
--     _maxa character varying,
--     _mahuyen character varying,
--     _namcapnhat smallint,
-- --     _ghichu character varying,
--     _day integer,
--     _month integer,
--     _year integer
-- )
-- AS $$
-- 	INSERT INTO TuLieuKhac(objectid, idtulieu, tentulieu, ngaytulieu, noidung, diadiem, dvql, nguongoc, maxa, mahuyen, namcapnhat, day, month, year)
-- 	VALUES (_objectid, _idtulieu, _tentulieu, _ngaytulieu, _noidung, _diadiem, _dvql, _nguongoc, _maxa, _mahuyen, _namcapnhat, _day, _month, _year);
-- $$ LANGUAGE SQL;
-- CALL AddSatLoPoint(ST_GeomFromText)

-- DROP PROCEDURE AddDoMan;
CREATE PROCEDURE AddDoMan(
    _objectid integer,
    _idtramman character varying,
    _tentram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _tensong character varying,
    _doman double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
-- 		SELECT ST_GeomFromText('POINT(106.8786 10.37739)', 4326)
-- 		SELECT ST_SetSrid(ST_MakePoint(106.8786, 10.37739),4326)
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO doman(objectid, shape, idtramman, tentram, gio, ngay, toadox, toadoy, tensong, doman, vitri, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, day, month, year)
		VALUES (_objectid, geom::geometry, _idtramman, _tentram, _gio, _ngay, _toadox, _toadoy, _tensong, _doman, _vitri, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _day, _month, _year);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDoMan()

-- DROP PROCEDURE EditDoMan;
CREATE PROCEDURE EditDoMan(
    _objectid integer,
    _tentram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _tensong character varying,
    _doman double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE DoMan
		SET shape=geom::geometry, tentram=_tentram, gio=_gio, ngay=_ngay, toadox=_toadox, toadoy=_toadoy, tensong=_tensong, doman=_doman, vitri=_vitri, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDoMan()

-- DROP PROCEDURE DeleteDoMan;
CREATE PROCEDURE DeleteDoMan(
    _objectid int
)
AS $$
	DELETE FROM DoMan WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteDoMan(1)

-- DROP PROCEDURE AddNangNong;
CREATE PROCEDURE AddNangNong(
	_objectid integer,
	_idtramkt character varying,
	_tentram character varying,
	_captram character varying,
	_vitritram character varying,
	_toadox double precision,
	_toadoy double precision,
	_thang smallint,
	_sogionang double precision,
	_nhietdomin double precision,
	_nhietdomax double precision,
	_nhietdotb double precision,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_kinhdo double precision,
	_vido double precision,
	_ngay timestamp without time zone,
	_day integer,
	_month integer,
	_year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO NangNong(objectid, shape, idtramkt, tentram, captram, vitritram, toadox, toadoy, thang, sogionang, nhietdomin, nhietdomax, nhietdotb, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, ngay, day, month, year)
		VALUES (_objectid, geom::geometry, _idtramkt, _tentram, _captram, _vitritram, _toadox, _toadoy, _thang, _sogionang, _nhietdomin, _nhietdomax, _nhietdotb, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _ngay, _day, _month, _year);		
	END;
$$ LANGUAGE plpgsql;
-- CALL AddNangNong()

-- DROP PROCEDURE EditNangNong;
CREATE PROCEDURE EditNangNong(
	_objectid integer,
	_tentram character varying,
	_captram character varying,
	_vitritram character varying,
	_toadox double precision,
	_toadoy double precision,
	_thang smallint,
	_sogionang double precision,
	_nhietdomin double precision,
	_nhietdomax double precision,
	_nhietdotb double precision,
	_maxa character varying,
	_mahuyen character varying,
	_namcapnhat smallint,
	_ghichu character varying,
	_kinhdo double precision,
	_vido double precision,
	_ngay timestamp without time zone,
	_day integer,
	_month integer,
	_year integer
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE NangNong
		SET shape=geom::geometry, tentram=_tentram, captram=_captram, vitritram=_vitritram, toadox=_toadox, toadoy=_toadoy, thang=_thang, sogionang=_sogionang, nhietdomin=_nhietdomin, nhietdomax=_nhietdomax, nhietdotb=_nhietdotb, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, ngay=_ngay, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditNangNong()

-- DROP PROCEDURE DeleteNangNong;
CREATE PROCEDURE DeleteNangNong(
    _objectid int
)
AS $$
	DELETE FROM NangNong WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteNangNong(1)

-- DROP PROCEDURE AddMua;
CREATE PROCEDURE AddMua(
    _objectid integer,
    _idtrammua character varying,
    _tentram character varying,
    _captram character varying,
    _vitritram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _luongmua double precision,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO Mua(objectid, shape, idtrammua, tentram, captram, vitritram, gio, ngay, toadox, toadoy, luongmua, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, day, month, year)
		VALUES (_objectid, geom::geometry, _idtrammua, _tentram, _captram, _vitritram, _gio, _ngay, _toadox, _toadoy, _luongmua, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _day, _month, _year);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddMua()

-- DROP PROCEDURE EditMua;
CREATE PROCEDURE EditMua(
    _objectid integer,
    _tentram character varying,
    _captram character varying,
    _vitritram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _luongmua double precision,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		UPDATE Mua
		SET shape=geom::geometry, tentram=_tentram, captram=_captram, vitritram=_vitritram, gio=_gio, ngay=_ngay, toadox=_toadox, toadoy=_toadoy, luongmua=_luongmua, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditMua()

-- DROP PROCEDURE DeleteMua;
CREATE PROCEDURE DeleteMua(
    _objectid int
)
AS $$
	DELETE FROM Mua WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteMua(1)

-- DROP PROCEDURE AddMucNuoc;
CREATE PROCEDURE AddMucNuoc(
    _objectid integer,
    _idtrammucnuoc character varying,
    _tentram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _mucnuoc double precision,
    _docaodinhtrieu double precision,
    _docaochantrieu double precision,
    _baodongi double precision,
    _baodongii double precision,
    _baodongiii double precision,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO MucNuoc(objectid, shape, idtrammucnuoc, tentram, gio, ngay, toadox, toadoy, mucnuoc, docaodinhtrieu, docaochantrieu, baodongi, baodongii, baodongiii, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, day, month, year)
		VALUES (_objectid, geom::geometry, _idtrammucnuoc, _tentram, _gio, _ngay, _toadox, _toadoy, _mucnuoc, _docaodinhtrieu, _docaochantrieu, _baodongi, _baodongii, _baodongiii, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _day, _month, _year);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddMucNuoc()

-- DROP PROCEDURE EditMucNuoc;
CREATE PROCEDURE EditMucNuoc(
    _objectid integer,
    _tentram character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _mucnuoc double precision,
    _docaodinhtrieu double precision,
    _docaochantrieu double precision,
    _baodongi double precision,
    _baodongii double precision,
    _baodongiii double precision,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		UPDATE MucNuoc
		SET shape=geom::geometry, tentram=_tentram, gio=_gio, ngay=_ngay, toadox=_toadox, toadoy=_toadoy, mucnuoc=_mucnuoc, docaodinhtrieu=_docaodinhtrieu, docaochantrieu=_docaochantrieu, baodongi=_baodongi, baodongii=_baodongii, baodongiii=_baodongiii, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditMucNuoc()

-- DROP PROCEDURE DeleteMucNuoc;
CREATE PROCEDURE DeleteMucNuoc(
    _objectid int
)
AS $$
	DELETE FROM MucNuoc WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteMucNuoc(1)

-- DROP PROCEDURE AddHoChua;
CREATE PROCEDURE AddHoChua(
    _objectid integer,
    _idhochua character varying,
    _ten character varying,
    _loaiho character varying,
    _vitri character varying,
    _kinhdo double precision,
    _vido double precision,
    _maxa character varying,
    _mahuyen character varying,
    _ngay timestamp without time zone,
    _h double precision,
    _w double precision,
    _qvh double precision,
    _qxa double precision,
    _qcsi double precision,
    _qcsii double precision,
    _qcsiii double precision,
    _qtb double precision,
    _bh double precision,
    _r double precision,
    _namcapnhat integer,
    _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO HoChua(objectid, shape, idhochua, ten, loaiho, vitri, kinhdo, vido, maxa, mahuyen, ngay, h, w, qvh, qxa, qcsi, qcsii, qcsiii, qtb, bh, r, namcapnhat, ghichu, day, month, year)
		VALUES ( _objectid, geom::geometry, _idhochua, _ten, _loaiho, _vitri, _kinhdo, _vido, _maxa, _mahuyen, _ngay, _h, _w, _qvh, _qxa, _qcsi, _qcsii, _qcsiii, _qtb, _bh, _r, _namcapnhat, _ghichu, _day, _month, _year);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddHoChua()

-- DROP PROCEDURE EditHoChua;
CREATE PROCEDURE EditHoChua(
    _objectid integer,
    _ten character varying,
    _loaiho character varying,
    _vitri character varying,
    _kinhdo double precision,
    _vido double precision,
    _maxa character varying,
    _mahuyen character varying,
    _ngay timestamp without time zone,
    _h double precision,
    _w double precision,
    _qvh double precision,
    _qxa double precision,
    _qcsi double precision,
    _qcsii double precision,
    _qcsiii double precision,
    _qtb double precision,
    _bh double precision,
    _r double precision,
    _namcapnhat integer,
    _ghichu character varying,
    _day integer,
    _month integer,
    _year integer
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		UPDATE HoChua
		SET shape=geom::geometry, ten=_ten, loaiho=_loaiho, vitri=_vitri, kinhdo=_kinhdo, vido=_vido, maxa=_maxa, mahuyen=_mahuyen, ngay=_ngay, h=_h, w=_w, qvh=_qvh, qxa=_qxa, qcsi=_qcsi, qcsii=_qcsii, qcsiii=_qcsiii, qtb=_qtb, bh=_bh, r=_r, namcapnhat=_namcapnhat, ghichu=_ghichu, day=_day, month=_month, year=_year
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditHoChua()

-- DROP PROCEDURE DeleteHoChua;
CREATE PROCEDURE DeleteHoChua(
    _objectid int
)
AS $$
	DELETE FROM HoChua WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteHoChua(1)

-- DROP PROCEDURE AddCongDap;
CREATE PROCEDURE AddCongDap(
    _objectid integer,
    _idcongdap character varying,
    _tencongdap character varying,
    _lytrinh character varying,
    _toadox double precision,
    _toadoy double precision,
    _cumcongtrinh character varying,
    _goithau character varying,
    _loaicongtrinh character varying,
    _hinhthuc character varying,
    _chieudai double precision,
    _duongkinh double precision,
    _berong double precision,
    _chieucao double precision,
    _socua smallint,
    _caotrinhdaycong character varying,
    _caotrinhdinhcong character varying,
    _hinhthucvanhanh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _capcongtrinh character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO CongDap(objectid, shape, idcongdap, tencongdap, lytrinh, toadox, toadoy, cumcongtrinh, goithau, loaicongtrinh, hinhthuc, chieudai, duongkinh, berong, chieucao, socua, caotrinhdaycong, caotrinhdinhcong, hinhthucvanhanh, muctieunhiemvu, diadiem, namsudung, capcongtrinh, hethongcttl, donviquanly, namcapnhat, ghichu)
		VALUES ( _objectid, geom::geometry, _idcongdap, _tencongdap, _lytrinh, _toadox, _toadoy, _cumcongtrinh, _goithau, _loaicongtrinh, _hinhthuc, _chieudai, _duongkinh, _berong, _chieucao, _socua, _caotrinhdaycong, _caotrinhdinhcong, _hinhthucvanhanh, _muctieunhiemvu, _diadiem, _namsudung, _capcongtrinh, _hethongcttl, _donviquanly, _namcapnhat, _ghichu);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddCongDap(113::int, 'LX113'::character varying, 'test'::character varying, 'test'::character varying, '2023-01-01'::timestamp without time zone, 1172184.13::double precision, 606964.79::double precision, 45::double precision, 45::double precision, 'test'::character varying, '787'::character varying, '787'::character varying, 123::smallint, 1::int, 1::int, 1::int)

-- DROP PROCEDURE EditCongDap;
CREATE PROCEDURE EditCongDap(
    _objectid integer,
    _tencongdap character varying,
    _lytrinh character varying,
    _toadox double precision,
    _toadoy double precision,
    _cumcongtrinh character varying,
    _goithau character varying,
    _loaicongtrinh character varying,
    _hinhthuc character varying,
    _chieudai double precision,
    _duongkinh double precision,
    _berong double precision,
    _chieucao double precision,
    _socua smallint	,
    _caotrinhdaycong character varying,
    _caotrinhdinhcong character varying,
    _hinhthucvanhanh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _capcongtrinh character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		UPDATE CongDap
		SET shape=geom::geometry, tencongdap=_tencongdap, lytrinh=_lytrinh, toadox=_toadox, toadoy=_toadoy, cumcongtrinh=_cumcongtrinh, goithau=_goithau, loaicongtrinh=_loaicongtrinh, hinhthuc=_hinhthuc, chieudai=_chieudai, duongkinh=_duongkinh, berong=_berong, chieucao=_chieucao, socua=_socua, caotrinhdaycong=_caotrinhdaycong, caotrinhdinhcong=_caotrinhdinhcong, hinhthucvanhanh=_hinhthucvanhanh, muctieunhiemvu=_muctieunhiemvu, diadiem=_diadiem, namsudung=_namsudung, capcongtrinh=_capcongtrinh, hethongcttl=_hethongcttl, donviquanly=_donviquanly, namcapnhat=_namcapnhat, ghichu=_ghichu
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditCongDap()

-- DROP PROCEDURE DeleteCongDap;
CREATE PROCEDURE DeleteCongDap(
    _objectid int
)
AS $$
	DELETE FROM CongDap WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteCongDap(1)

-- DROP PROCEDURE AddMocCanhBaoTrieuCuong;
CREATE PROCEDURE AddMocCanhBaoTrieuCuong(
    _objectid integer,
    _idmoccbtc character varying,
    _tenmoc character varying,
    _giatri character varying,
    _toadox double precision,
    _toadoy double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namsudung smallint,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO MocCanhBaoTrieuCuong(objectid, shape, idmoccbtc, tenmoc, giatri, toadox, toadoy, vitri, maxa, mahuyen, namsudung, namcapnhat, ghichu)
		VALUES (_objectid, geom::geometry, _idmoccbtc, _tenmoc, _giatri, _toadox, _toadoy, _vitri, _maxa, _mahuyen, _namsudung, _namcapnhat, _ghichu);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddMocCanhBaoTrieuCuong()

-- DROP PROCEDURE EditMocCanhBaoTrieuCuong;
CREATE PROCEDURE EditMocCanhBaoTrieuCuong(
    _objectid integer,
    _tenmoc character varying,
    _giatri character varying,
    _toadox double precision,
    _toadoy double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namsudung smallint,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		UPDATE MocCanhBaoTrieuCuong
		SET shape=geom::geometry, tenmoc=_tenmoc, giatri=_giatri, toadox=_toadox, toadoy=_toadoy, vitri=_vitri, maxa=_maxa, mahuyen=_mahuyen, namsudung=_namsudung, namcapnhat=_namcapnhat, ghichu=_ghichu
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditMocCanhBaoTrieuCuong()

-- DROP PROCEDURE DeleteMocCanhBaoTrieuCuong;
CREATE PROCEDURE DeleteMocCanhBaoTrieuCuong(
    _objectid int
)
AS $$
	DELETE FROM MocCanhBaoTrieuCuong WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteMocCanhBaoTrieuCuong(1)

-- DROP PROCEDURE AddBienCanhBaoSatLo;
CREATE PROCEDURE AddBienCanhBaoSatLo(
    _objectid integer,
    _idbcbsl character varying,
    _sohieubien character varying,
    _toadox double precision,
    _toadoy double precision,
    _vitrisatlo character varying,
    _phamvi character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namxaydung smallint,
    _hinhanh character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _tuyensr character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO BienCanhBaoSatLo(objectid, shape, idbcbsl, sohieubien, toadox, toadoy, vitrisatlo, phamvi, maxa, mahuyen, namxaydung, hinhanh, namcapnhat, ghichu, tuyensr)
		VALUES (_objectid, geom::geometry, _idbcbsl, _sohieubien, _toadox, _toadoy, _vitrisatlo, _phamvi, _maxa, _mahuyen, _namxaydung, _hinhanh, _namcapnhat, _ghichu, _tuyensr);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddBienCanhBaoSatLo()

-- DROP PROCEDURE EditBienCanhBaoSatLo;
CREATE PROCEDURE EditBienCanhBaoSatLo(
    _objectid integer,
    _sohieubien character varying,
    _toadox double precision,
    _toadoy double precision,
    _vitrisatlo character varying,
    _phamvi character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namxaydung smallint,
    _hinhanh character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _tuyensr character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		UPDATE BienCanhBaoSatLo
		SET shape=geom::geometry, sohieubien=_sohieubien, toadox=_toadox, toadoy=_toadoy, vitrisatlo=_vitrisatlo, phamvi=_phamvi, maxa=_maxa, mahuyen=_mahuyen, namxaydung=_namxaydung, hinhanh=_hinhanh, namcapnhat=_namcapnhat, ghichu=_ghichu, tuyensr=_tuyensr
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditBienCanhBaoSatLo()

-- DROP PROCEDURE DeleteBienCanhBaoSatLo;
CREATE PROCEDURE DeleteBienCanhBaoSatLo(
    _objectid int
)
AS $$
	DELETE FROM BienCanhBaoSatLo WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteBienCanhBaoSatLo(1)

-- DROP PROCEDURE AddKhuNeoDau;
CREATE PROCEDURE AddKhuNeoDau(
    _objectid integer,
    _idknd character varying,
    _ten character varying,
    _diachi character varying,
    _kinhdodd double precision,
    _vidodd double precision,
    _kinhdodc double precision,
    _vidodc double precision,
    _dosaunuoc double precision,
    _succhua character varying,
    _coloaitau character varying,
    _vitrivl character varying,
    _huongluong character varying,
    _chieudai double precision,
    _sdt character varying,
    _tansoll character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdodd || ' ' || _vidodd || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text); 
	BEGIN
		INSERT INTO khuneodau(objectid, shape, idknd, ten, diachi, kinhdodd, vidodd, kinhdodc, vidodc, dosaunuoc, succhua, coloaitau, vitrivl, huongluong, chieudai, sdt, tansoll, maxa, mahuyen, namcapnhat, ghichu)
		VALUES ( _objectid, geom::geometry, _idknd, _ten, _diachi, _kinhdodd, _vidodd, _kinhdodc, _vidodc, _dosaunuoc, _succhua, _coloaitau, _vitrivl, _huongluong, _chieudai, _sdt, _tansoll, _maxa, _mahuyen, _namcapnhat, _ghichu);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddKhuNeoDau()

-- DROP PROCEDURE EditKhuNeoDau;
CREATE PROCEDURE EditKhuNeoDau(
    _objectid integer,
    _ten character varying,
    _diachi character varying,
    _kinhdodd double precision,
    _vidodd double precision,
    _kinhdodc double precision,
    _vidodc double precision,
    _dosaunuoc double precision,
    _succhua character varying,
    _coloaitau character varying,
    _vitrivl character varying,
    _huongluong character varying,
    _chieudai double precision,
    _sdt character varying,
    _tansoll character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _kinhdodd || ' ' || _vidodd || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE khuneodau
		SET shape=geom::geometry, ten=_ten, diachi=_diachi, kinhdodd=_kinhdodd, vidodd=_vidodd, kinhdodc=_kinhdodc, vidodc=_vidodc, dosaunuoc=_dosaunuoc, succhua=_succhua, coloaitau=_coloaitau, vitrivl=_vitrivl, huongluong=_huongluong, chieudai=_chieudai, sdt=_sdt, tansoll=_tansoll, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditKhuNeoDau()

-- DROP PROCEDURE DeleteKhuNeoDau;
CREATE PROCEDURE DeleteKhuNeoDau(
    _objectid int
)
AS $$
	DELETE FROM KhuNeoDau WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteKhuNeoDau(1)

-- DROP PROCEDURE AddThietHaiThienTai;
CREATE PROCEDURE AddThietHaiThienTai(
    _objectid integer,
    _idthiethai character varying,
    _loaithientai character varying,
    _doituongthiethai character varying,
    _motathiethai character varying,
    _dvtthiethai character varying,
    _soluong double precision,
    _giatrithiethai double precision,
    _diadiem character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _day integer,
    _month integer,
    _year integer,
    _date character varying
)
AS $$
	INSERT INTO ThietHai_ThienTai(objectid, idthiethai, loaithientai, doituongthiethai, motathiethai, dvtthiethai, soluong, giatrithiethai, diadiem, gio, ngay, maxa, mahuyen, namcapnhat, ghichu, day, month, year, date)
	VALUES (_objectid, _idthiethai, _loaithientai, _doituongthiethai, _motathiethai, _dvtthiethai, _soluong, _giatrithiethai, _diadiem, _gio, _ngay, _maxa, _mahuyen, _namcapnhat, _ghichu, _day, _month, _year, _date);
$$ LANGUAGE SQL;
-- CALL AddThietHaiThienTai()

-- DROP PROCEDURE EditThietHaiThienTai;
CREATE PROCEDURE EditThietHaiThienTai(
    _objectid integer,
    _loaithientai character varying,
    _doituongthiethai character varying,
    _motathiethai character varying,
    _dvtthiethai character varying,
    _soluong double precision,
    _giatrithiethai double precision,
    _diadiem character varying,
    _gio character varying,
    _ngay timestamp without time zone,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _day integer,
    _month integer,
    _year integer,
    _date character varying
)
AS $$
	UPDATE ThietHai_ThienTai
	SET loaithientai=_loaithientai, doituongthiethai=_doituongthiethai, motathiethai=_motathiethai, dvtthiethai=_dvtthiethai, soluong=_soluong, giatrithiethai=_giatrithiethai, diadiem=_diadiem, gio=_gio, ngay=_ngay, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, day=_day, month=_month, year=_year, date=_date
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL EditThietHaiThienTai()

-- DROP PROCEDURE DeleteThietHaiThienTai;
CREATE PROCEDURE DeleteThietHaiThienTai(
    _objectid int
)
AS $$
	DELETE FROM ThietHai_ThienTai WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteThietHaiThienTai(1)

-- DROP PROCEDURE AddSatLoLine;
CREATE PROCEDURE AddSatLoLine(
    _objectid integer,
    _idsatlol character varying,
    _vitri character varying,
    _tuyensong character varying,
    _capsong character varying,
    _chieudai double precision,
    _chieurong double precision,
    _mucdo character varying,
    _tinhtrang character varying,
    _anhhuong character varying,
    _khoangcachah character varying,
    _ditichah double precision,
    _sohoah integer,
    _songuoiah integer,
    _hatangah character varying,
    _congtrinhchongsl character varying,
    _chudautu character varying,
    _tenduan character varying,
    _quymoduan character varying,
    _tongmucduan integer,
    _tiendothuchien character varying,
    _nguongoc character varying,
    _dubao character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _ctxdke character varying,
    _shape_length double precision,
	_toado character varying
)
AS $$
	DECLARE
		geom_line text := (SELECT ST_Multi(ST_GeomFromText(toado, 4326))::text);
		geom_point text := (SELECT ST_LineInterpolatePoint(ST_GeomFromText(toado, 4326), 0.5))::text;
	BEGIN
		INSERT INTO SatLo_Line(objectid, shape, idsatlol, vitri, tuyensong, capsong, chieudai, chieurong, mucdo, tinhtrang, anhhuong, khoangcachah, ditichah, sohoah, songuoiah, hatangah, congtrinhchongsl, chudautu, tenduan, quymoduan, tongmucduan, tiendothuchien, nguongoc, dubao, maxa, mahuyen, namcapnhat, ghichu, ctxdke, shape_length)
		VALUES (_objectid, geom_line::geometry, _idsatlol, _vitri, _tuyensong, _capsong, _chieudai, _chieurong, _mucdo, _tinhtrang, _anhhuong, _khoangcachah, _ditichah, _sohoah, _songuoiah, _hatangah, _congtrinhchongsl, _chudautu, _tenduan, _quymoduan, _tongmucduan, _tiendothuchien, _nguongoc, _dubao, _maxa, _mahuyen, _namcapnhat, _ghichu, _ctxdke, _shape_length);
	
		INSERT INTO SatLo_Point(objectid, shape, idsatlop, vitri, tuyensong, capsong, chieudai, chieurong, mucdo, tinhtrang, anhhuong, khoangcachah, ditichah, sohoah, songuoiah, hatangah, congtrinhchongsl, chudautu, tenduan, quymoduan, tongmucduan, tiendothuchien, nguongoc, dubao, maxa, mahuyen, namcapnhat, ghichu, ctxdke)
		VALUES (_objectid, geom_point::geometry, _idsatlol, _vitri, _tuyensong, _capsong, _chieudai, _chieurong, _mucdo, _tinhtrang, _anhhuong, _khoangcachah, _ditichah, _sohoah, _songuoiah, _hatangah, _congtrinhchongsl, _chudautu, _tenduan, _quymoduan, _tongmucduan, _tiendothuchien, _nguongoc, _dubao, _maxa, _mahuyen, _namcapnhat, _ghichu, _ctxdke);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddSatLoLine()

-- DROP PROCEDURE EditSatLoLine;
CREATE PROCEDURE EditSatLoLine(
    _objectid integer,
    _vitri character varying,
    _tuyensong character varying,
    _capsong character varying,
    _chieudai double precision,
    _chieurong double precision,
    _mucdo character varying,
    _tinhtrang character varying,
    _anhhuong character varying,
    _khoangcachah character varying,
    _ditichah double precision,
    _sohoah integer,
    _songuoiah integer,
    _hatangah character varying,
    _congtrinhchongsl character varying,
    _chudautu character varying,
    _tenduan character varying,
    _quymoduan character varying,
    _tongmucduan integer,
    _tiendothuchien character varying,
    _nguongoc character varying,
    _dubao character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _ctxdke character varying,
    _shape_length double precision,
	_toado Text
)
AS $$
	DECLARE 
		toado text := 'LINESTRING (' || _toado || ')';
		geom_line text := (SELECT ST_Multi(ST_GeomFromText(toado, 4326))::text);
		geom_point text := (SELECT ST_LineInterpolatePoint(ST_GeomFromText(toado, 4326), 0.5))::text;
	BEGIN
		UPDATE SatLo_Line
		SET shape=geom_line::geometry, vitri=_vitri, tuyensong=_tuyensong, capsong=_capsong, chieudai=_chieudai, chieurong=_chieurong, mucdo=_mucdo, tinhtrang=_tinhtrang, anhhuong=_anhhuong, khoangcachah=_khoangcachah, ditichah=_ditichah, sohoah=_sohoah, songuoiah=_songuoiah, hatangah=_hatangah, congtrinhchongsl=_congtrinhchongsl, chudautu=_chudautu, tenduan=_tenduan, quymoduan=_quymoduan, tongmucduan=_tongmucduan, tiendothuchien=_tiendothuchien, nguongoc=_nguongoc, dubao=_dubao, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, ctxdke=_ctxdke, shape_length=_shape_length
		WHERE objectid = _objectid;	
		
		UPDATE SatLo_Point
		SET shape=geom_point::geometry, vitri=_vitri, tuyensong=_tuyensong, capsong=_capsong, chieudai=_chieudai, chieurong=_chieurong, mucdo=_mucdo, tinhtrang=_tinhtrang, anhhuong=_anhhuong, khoangcachah=_khoangcachah, ditichah=_ditichah, sohoah=_sohoah, songuoiah=_songuoiah, hatangah=_hatangah, congtrinhchongsl=_congtrinhchongsl, chudautu=_chudautu, tenduan=_tenduan, quymoduan=_quymoduan, tongmucduan=_tongmucduan, tiendothuchien=_tiendothuchien, nguongoc=_nguongoc, dubao=_dubao, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, ctxdke=_ctxdke
		WHERE objectid = _objectid;		
	END;
$$ LANGUAGE plpgsql;
-- CALL EditSatLoLine()

-- DROP PROCEDURE DeleteSatLoLine;
CREATE PROCEDURE DeleteSatLoLine(
    _objectid int
)
AS $$
	DELETE FROM SatLo_Line WHERE objectid = _objectid;
	DELETE FROM SatLo_Point WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteSatLoLine(1)

-- DROP PROCEDURE AddKe;
CREATE PROCEDURE AddKe(
    _objectid integer,
    _idke character varying,
    _tenkenhmuong character varying,
    _vitri character varying,
    _chieudai double precision,
    _caotrinhdaykenh double precision,
    _berongkenh double precision,
    _hesomai double precision,
    _caotrinhbotrai character varying,
    _caotrinhbophai character varying,
    _berongbotrai character varying,
    _berongbophai character varying,
    _hanhlangbaove character varying,
    _capcongtrinh character varying,
    _cumcongtrinh character varying,
    _ketcaucongtrinh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _shape_length double precision,
    _toado character varying
)
AS $$
	DECLARE
-- 		SELECT ST_GeomFromText('MULTILINESTRING ((106.8938 10.38262, 106.8954 10.38395, 106.8976 10.38401))', 4326)

		toado text := 'MULTILINESTRING ' || _toado;
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		INSERT INTO Ke(objectid, shape, idke, tenkenhmuong, vitri, chieudai, caotrinhdaykenh, berongkenh, hesomai, caotrinhbotrai, caotrinhbophai, berongbotrai, berongbophai, hanhlangbaove, capcongtrinh, cumcongtrinh, ketcaucongtrinh, muctieunhiemvu, diadiem, namsudung, hethongcttl, donviquanly, namcapnhat, ghichu, shape_length)
		VALUES (_objectid, geom::geometry, _idke, _tenkenhmuong, _vitri, _chieudai, _caotrinhdaykenh, _berongkenh, _hesomai, _caotrinhbotrai, _caotrinhbophai, _berongbotrai, _berongbophai, _hanhlangbaove, _capcongtrinh, _cumcongtrinh, _ketcaucongtrinh, _muctieunhiemvu, _diadiem, _namsudung, _hethongcttl, _donviquanly, _namcapnhat, _ghichu, _shape_length);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddKe()

-- DROP PROCEDURE EditKe;
CREATE PROCEDURE EditKe(
    _objectid integer,
    _tenkenhmuong character varying,
    _vitri character varying,
    _chieudai double precision,
    _caotrinhdaykenh double precision,
    _berongkenh double precision,
    _hesomai double precision,
    _caotrinhbotrai character varying,
    _caotrinhbophai character varying,
    _berongbotrai character varying,
    _berongbophai character varying,
    _hanhlangbaove character varying,
    _capcongtrinh character varying,
    _cumcongtrinh character varying,
    _ketcaucongtrinh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _shape_length double precision,
    _toado character varying
)
AS $$
	DECLARE
		toado text := 'MULTILINESTRING (' || _toado || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);	

-- 		toado text := 'LINESTRING (' || _toado || ')';
-- 		geom text := (SELECT ST_Multi(ST_GeomFromText(toado, 4326))::text);
	BEGIN
		UPDATE Ke
		SET shape=geom::geometry, tenkenhmuong=_tenkenhmuong, vitri=_vitri, chieudai=_chieudai, caotrinhdaykenh=_caotrinhdaykenh, berongkenh=_berongkenh, hesomai=_hesomai, caotrinhbotrai=_caotrinhbotrai, caotrinhbophai=_caotrinhbophai, berongbotrai=_berongbotrai, berongbophai=_berongbophai, hanhlangbaove=_hanhlangbaove, capcongtrinh=_capcongtrinh, cumcongtrinh=_cumcongtrinh, ketcaucongtrinh=_ketcaucongtrinh, muctieunhiemvu=_muctieunhiemvu, diadiem=_diadiem, namsudung=_namsudung, hethongcttl=_hethongcttl, donviquanly=_donviquanly, namcapnhat=_namcapnhat, ghichu=_ghichu, shape_length=_shape_length
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditKe()

-- DROP PROCEDURE DeleteKe;
CREATE PROCEDURE DeleteKe(
    _objectid int
)
AS $$
	DELETE FROM Ke WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteKe(1)

-- DROP PROCEDURE AddDeBaoBoBao;
CREATE PROCEDURE AddDeBaoBoBao(
    _objectid integer,
    _idkenhmuong character varying,
    _tenkenhmuong character varying,
    _vitri character varying,
    _chieudai double precision,
    _caotrinhdaykenh character varying,
    _berongkenh character varying,
    _hesomai character varying,
    _caotrinhbotrai character varying,
    _caotrinhbophai character varying,
    _berongbotrai character varying,
    _berongbophai character varying,
    _hanhlangbaove character varying,
    _capcongtrinh character varying,
    _ketcaucongtrinh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _shape_length double precision,
    _toado character varying
)
AS $$
	DECLARE
		toado text := 'MULTILINESTRING ' || _toado;
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		INSERT INTO DeBao_BoBao(objectid, shape, idkenhmuong, tenkenhmuong, vitri, chieudai, caotrinhdaykenh, berongkenh, hesomai, caotrinhbotrai, caotrinhbophai, berongbotrai, berongbophai, hanhlangbaove, capcongtrinh, ketcaucongtrinh, muctieunhiemvu, diadiem, namsudung, hethongcttl, donviquanly, namcapnhat, ghichu, shape_length)
		VALUES (_objectid, geom::geometry, _idkenhmuong, _tenkenhmuong, _vitri, _chieudai, _caotrinhdaykenh, _berongkenh, _hesomai, _caotrinhbotrai, _caotrinhbophai, _berongbotrai, _berongbophai, _hanhlangbaove, _capcongtrinh, _ketcaucongtrinh, _muctieunhiemvu, _diadiem, _namsudung, _hethongcttl, _donviquanly, _namcapnhat, _ghichu, _shape_length);	
	END;
$$ LANGUAGE plpgsql;
-- CALL AddDeBaoBoBao()

-- DROP PROCEDURE EditDeBaoBoBao;
CREATE PROCEDURE EditDeBaoBoBao(
    _objectid integer,
    _tenkenhmuong character varying,
    _vitri character varying,
    _chieudai double precision,
    _caotrinhdaykenh character varying,
    _berongkenh character varying,
    _hesomai character varying,
    _caotrinhbotrai character varying,
    _caotrinhbophai character varying,
    _berongbotrai character varying,
    _berongbophai character varying,
    _hanhlangbaove character varying,
    _capcongtrinh character varying,
    _ketcaucongtrinh character varying,
    _muctieunhiemvu character varying,
    _diadiem character varying,
    _namsudung character varying,
    _hethongcttl character varying,
    _donviquanly character varying,
    _namcapnhat smallint,
    _ghichu character varying,
    _shape_length double precision,
    _toado character varying
)
AS $$
	DECLARE
		toado text := 'MULTILINESTRING ' || _toado;
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE DeBao_BoBao
		SET shape=geom::geometry, tenkenhmuong=_tenkenhmuong, vitri=_vitri, chieudai=_chieudai, caotrinhdaykenh=_caotrinhdaykenh, berongkenh=_berongkenh, hesomai=_hesomai, caotrinhbotrai=_caotrinhbotrai, caotrinhbophai=_caotrinhbophai, berongbotrai=_berongbotrai, berongbophai=_berongbophai, hanhlangbaove=_hanhlangbaove, capcongtrinh=_capcongtrinh, ketcaucongtrinh=_ketcaucongtrinh, muctieunhiemvu=_muctieunhiemvu, diadiem=_diadiem, namsudung=_namsudung, hethongcttl=_hethongcttl, donviquanly=_donviquanly, namcapnhat=_namcapnhat, ghichu=_ghichu, shape_length=_shape_length
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditDeBaoBoBao()

-- DROP PROCEDURE DeleteDeBaoBoBao;
CREATE PROCEDURE DeleteDeBaoBoBao(
    _objectid int
)
AS $$
	DELETE FROM DeBao_BoBao 
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteDeBaoBoBao(1)

-- DROP PROCEDURE AddApThapNhietDoi;
CREATE PROCEDURE AddApThapNhietDoi(
    _objectid integer,
    _idapthap character varying,
    _tenapthap character varying,
    _gio double precision,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
	_day integer,
    _month integer,
    _year integer,
    _ngaybatdau timestamp without time zone,
    _ngayketthuc timestamp without time zone,
    _centerid character varying,
    _tenvn character varying,
    _kvahhcm character varying
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		INSERT INTO ApThapNhietDoi(objectid, shape, idapthap, tenapthap, gio, ngay, toadox, toadoy, apsuat, tocdogio, vitri, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, day, month, year, ngaybatdau, ngayketthuc, centerid, tenvn, kvahhcm)
		VALUES (_objectid, geom::geometry, _idapthap, _tenapthap, _gio, _ngay, _toadox, _toadoy, _apsuat, _tocdogio, _vitri, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _day, _month, _year, _ngaybatdau, _ngayketthuc, _centerid, _tenvn, _kvahhcm);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddApThapNhietDoi()

-- DROP PROCEDURE EditApThapNhietDoi;
CREATE PROCEDURE EditApThapNhietDoi(
    _objectid integer,
    _tenapthap character varying,
    _gio double precision,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day integer,
    _month integer,
    _year integer,
    _ngaybatdau timestamp without time zone,
    _ngayketthuc timestamp without time zone,
    _centerid character varying,
    _tenvn character varying,
    _kvahhcm character varying
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE ApThapNhietDoi
		SET shape=geom::geometry, tenapthap=_tenapthap, gio=_gio, ngay=_ngay, toadox=_toadox, toadoy=_toadoy, apsuat=_apsuat, tocdogio=_tocdogio, vitri=_vitri, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, day=_day, month=_month, year=_year, ngaybatdau=_ngaybatdau, ngayketthuc=_ngayketthuc, centerid=_centerid, tenvn=_tenvn, kvahhcm=_kvahhcm
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditApThapNhietDoi()

-- DROP PROCEDURE DeleteApThapNhietDoi;
CREATE PROCEDURE DeleteApThapNhietDoi(
    _objectid int
)
AS $$
	DELETE FROM ApThapNhietDoi 
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteApThapNhietDoi(1)

-- DROP PROCEDURE AddBao;
CREATE PROCEDURE AddBao(
    _objectid integer,
    _idbao character varying,
    _tenbao character varying,
    _gio double precision,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day double precision,
    _month double precision,
    _year double precision,
    _capbao character varying,
    _ngaybatdau timestamp without time zone,
    _ngayketthuc timestamp without time zone,
    _centerid character varying,
    _tenvn character varying,
    _kvahhcm character varying
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		INSERT INTO Bao(objectid, shape, idbao, tenbao, gio, ngay, toadox, toadoy, apsuat, tocdogio, vitri, maxa, mahuyen, namcapnhat, ghichu, kinhdo, vido, day, month, year, capbao, ngaybatdau, ngayketthuc, centerid, tenvn, kvahhcm)
		VALUES (_objectid, geom::geometry, _idbao, _tenbao, _gio, _ngay, _toadox, _toadoy, _apsuat, _tocdogio, _vitri, _maxa, _mahuyen, _namcapnhat, _ghichu, _kinhdo, _vido, _day, _month, _year, _capbao, _ngaybatdau, _ngayketthuc, _centerid, _tenvn, _kvahhcm);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddBao()

-- DROP PROCEDURE EditBao;
CREATE PROCEDURE EditBao(
    _objectid integer,
    _tenbao character varying,
    _gio double precision,
    _ngay timestamp without time zone,
    _toadox double precision,
    _toadoy double precision,
    _apsuat double precision,
    _tocdogio double precision,
    _vitri character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat integer,
    _ghichu character varying,
    _kinhdo double precision,
    _vido double precision,
    _day double precision,
    _month double precision,
    _year double precision,
    _capbao character varying,
    _ngaybatdau timestamp without time zone,
    _ngayketthuc timestamp without time zone,
    _centerid character varying,
    _tenvn character varying,
    _kvahhcm character varying
)
AS $$
	DECLARE
		toado text := 'POINT(' || _kinhdo || ' ' || _vido || ')';
		geom text := (SELECT ST_GeomFromText(toado, 4326)::text);
	BEGIN
		UPDATE Bao
		SET shape=geom::geometry, tenbao=_tenbao, gio=_gio, ngay=_ngay, toadox=_toadox, toadoy=_toadoy, apsuat=_apsuat, tocdogio=_tocdogio, vitri=_vitri, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu, kinhdo=_kinhdo, vido=_vido, day=_day, month=_month, year=_year, capbao=_capbao, ngaybatdau=_ngaybatdau, ngayketthuc=_ngayketthuc, centerid=_centerid, tenvn=_tenvn, kvahhcm=_kvahhcm
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditBao()

-- DROP PROCEDURE DeleteBao;
CREATE PROCEDURE DeleteBao(
    _objectid int
)
AS $$
	DELETE FROM Bao 
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteBao(1)

-- DROP PROCEDURE AddChayRung;
CREATE PROCEDURE AddChayRung(
    _objectid integer,
    _idchay character varying,
    _ngay character varying,
    _diadiem character varying,
    _toadox double precision,
    _toadoy double precision,
    _tgchay character varying,
    _tgdap character varying,
    _dtchay double precision,
    _hientrang character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		INSERT INTO ChayRung(objectid, shape, idchay, ngay, diadiem, toadox, toadoy, tgchay, tgdap, dtchay, hientrang, maxa, mahuyen, namcapnhat, ghichu)
		VALUES (_objectid, geom::geometry, _idchay, _ngay, _diadiem, _toadox, _toadoy, _tgchay, _tgdap, _dtchay, _hientrang, _maxa, _mahuyen, _namcapnhat, _ghichu);
	END;
$$ LANGUAGE plpgsql;
-- CALL AddChayRung()

-- DROP PROCEDURE EditChayRung;
CREATE PROCEDURE EditChayRung(
    _objectid integer,
    _ngay character varying,
    _diadiem character varying,
    _toadox double precision,
    _toadoy double precision,
    _tgchay character varying,
    _tgdap character varying,
    _dtchay double precision,
    _hientrang character varying,
    _maxa character varying,
    _mahuyen character varying,
    _namcapnhat smallint,
    _ghichu character varying
)
AS $$
	DECLARE 
		toado text := 'POINT(' || _toadox || ' ' || _toadoy || ')';
		geom text := (SELECT ST_Transform(ST_GeomFromText(toado, 900914), 4326)::text); 
	BEGIN
		UPDATE ChayRung
		SET shape=geom::geometry, ngay=_ngay, diadiem=_diadiem, toadox=_toadox, toadoy=_toadoy, tgchay=_tgchay, tgdap=_tgdap, dtchay=_dtchay, hientrang=_hientrang, maxa=_maxa, mahuyen=_mahuyen, namcapnhat=_namcapnhat, ghichu=_ghichu
		WHERE objectid = _objectid;
	END;
$$ LANGUAGE plpgsql;
-- CALL EditChayRung()

-- DROP PROCEDURE DeleteChayRung;
CREATE PROCEDURE DeleteChayRung(
    _objectid int
)
AS $$
	DELETE FROM ChayRung 
	WHERE objectid = _objectid;
$$ LANGUAGE SQL;
-- CALL DeleteChayRung(1)



