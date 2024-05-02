-- =================================== TRIGGER FUNCTION ===================================
-- Before: Insert, Update
-- After: Delete, Update
-----------------------------------------

-- Table: apthapnhietdoi
-- DROP TRIGGER IF EXISTS TriggerBeforeApThapNhietDoi ON apthapnhietdoi; 
-- DROP FUNCTION BeforeApThapNhietDoi;
CREATE OR REPLACE FUNCTION BeforeApThapNhietDoi() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('apthapnhietdoi', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeApThapNhietDoi
    BEFORE INSERT OR UPDATE ON apthapnhietdoi
    FOR EACH ROW EXECUTE FUNCTION BeforeApThapNhietDoi();

-- DROP TRIGGER IF EXISTS TriggerAfterApThapNhietDoi ON apthapnhietdoi; 
-- DROP FUNCTION AfterApThapNhietDoi;
CREATE OR REPLACE FUNCTION AfterApThapNhietDoi() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('apthapnhietdoi', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterApThapNhietDoi
    AFTER DELETE OR UPDATE ON apthapnhietdoi
    FOR EACH ROW EXECUTE FUNCTION AfterApThapNhietDoi();
	
-- Table: bao
-- DROP TRIGGER IF EXISTS TriggerBeforeBao ON bao; 
-- DROP FUNCTION BeforeBao;
CREATE OR REPLACE FUNCTION BeforeBao() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('bao', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeBao
    BEFORE INSERT OR UPDATE ON bao
    FOR EACH ROW EXECUTE FUNCTION BeforeBao();

-- DROP TRIGGER IF EXISTS TriggerAfterBao ON bao; 
-- DROP FUNCTION AfterBao;
CREATE OR REPLACE FUNCTION AfterBao() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('bao', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterBao
    AFTER DELETE OR UPDATE ON bao
    FOR EACH ROW EXECUTE FUNCTION AfterBao();
	
-- Table: biencanhbaosatlo
-- DROP TRIGGER IF EXISTS TriggerBeforeBienCanhBaoSatLo ON biencanhbaosatlo; 
-- DROP FUNCTION BeforeBienCanhBaoSatLo;
CREATE OR REPLACE FUNCTION BeforeBienCanhBaoSatLo() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('biencanhbaosatlo', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeBienCanhBaoSatLo
    BEFORE INSERT OR UPDATE ON biencanhbaosatlo
    FOR EACH ROW EXECUTE FUNCTION BeforeBienCanhBaoSatLo();

-- DROP TRIGGER IF EXISTS TriggerAfterBienCanhBaoSatLo ON biencanhbaosatlo; 
-- DROP FUNCTION AfterBienCanhBaoSatLo;
CREATE OR REPLACE FUNCTION AfterBienCanhBaoSatLo() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('biencanhbaosatlo', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterBienCanhBaoSatLo
    AFTER DELETE OR UPDATE ON biencanhbaosatlo
    FOR EACH ROW EXECUTE FUNCTION AfterBienCanhBaoSatLo();	
	
-- Table: congdap
-- DROP TRIGGER IF EXISTS TriggerBeforeCongDap ON congdap; 
-- DROP FUNCTION BeforeCongDap;
CREATE OR REPLACE FUNCTION BeforeCongDap() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('congdap', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeCongDap
    BEFORE INSERT OR UPDATE ON congdap
    FOR EACH ROW EXECUTE FUNCTION BeforeCongDap();

-- DROP TRIGGER IF EXISTS TriggerAfterCongDap ON congdap; 
-- DROP FUNCTION AfterCongDap;
CREATE OR REPLACE FUNCTION AfterCongDap() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('congdap', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterCongDap
    AFTER DELETE OR UPDATE ON congdap
    FOR EACH ROW EXECUTE FUNCTION AfterCongDap();	
	
-- Table: debao_bobao
-- DROP TRIGGER IF EXISTS TriggerBeforeDeBaoboBao ON debao_bobao; 
-- DROP FUNCTION BeforeDeBaoboBao;
CREATE OR REPLACE FUNCTION BeforeDeBaoboBao() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('debao_bobao', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeDeBaoboBao
    BEFORE INSERT OR UPDATE ON debao_bobao
    FOR EACH ROW EXECUTE FUNCTION BeforeDeBaoboBao();

-- DROP TRIGGER IF EXISTS TriggerAfterDeBaoboBao ON debao_bobao; 
-- DROP FUNCTION AfterDeBaoboBao;
CREATE OR REPLACE FUNCTION AfterDeBaoboBao() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('debao_bobao', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterDeBaoboBao
    AFTER DELETE OR UPDATE ON debao_bobao
    FOR EACH ROW EXECUTE FUNCTION AfterDeBaoboBao();

-- Table: diemantoan
-- DROP TRIGGER IF EXISTS TriggerBeforeDiemAnToan ON diemantoan; 
-- DROP FUNCTION BeforeDiemAnToan;
CREATE OR REPLACE FUNCTION BeforeDiemAnToan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('diemantoan', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeDiemAnToan
    BEFORE INSERT OR UPDATE ON diemantoan
    FOR EACH ROW EXECUTE FUNCTION BeforeDiemAnToan();

-- DROP TRIGGER IF EXISTS TriggerAfterDiemAnToan ON diemantoan; 
-- DROP FUNCTION AfterDiemAnToan;
CREATE OR REPLACE FUNCTION AfterDiemAnToan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('diemantoan', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterDiemAnToan
    AFTER DELETE OR UPDATE ON diemantoan
    FOR EACH ROW EXECUTE FUNCTION AfterDiemAnToan();

-- Table: diemxungyeu
-- DROP TRIGGER IF EXISTS TriggerBeforeDiemXungYeu ON diemxungyeu; 
-- DROP FUNCTION BeforeDiemXungYeu;
CREATE OR REPLACE FUNCTION BeforeDiemXungYeu() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('diemxungyeu', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeDiemXungYeu
    BEFORE INSERT OR UPDATE ON diemxungyeu
    FOR EACH ROW EXECUTE FUNCTION BeforeDiemXungYeu();

-- DROP TRIGGER IF EXISTS TriggerAfterDiemXungYeu ON diemxungyeu; 
-- DROP FUNCTION AfterDiemXungYeu;
CREATE OR REPLACE FUNCTION AfterDiemXungYeu() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('diemxungyeu', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterDiemXungYeu
    AFTER DELETE OR UPDATE ON diemxungyeu
    FOR EACH ROW EXECUTE FUNCTION AfterDiemXungYeu();

-- Table: doman
-- DROP TRIGGER IF EXISTS TriggerBeforeDoMan ON doman; 
-- DROP FUNCTION BeforeDoMan;
CREATE OR REPLACE FUNCTION BeforeDoMan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('doman', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeDoMan
    BEFORE INSERT OR UPDATE ON doman
    FOR EACH ROW EXECUTE FUNCTION BeforeDoMan();

-- DROP TRIGGER IF EXISTS TriggerAfterDoMan ON doman; 
-- DROP FUNCTION AfterDoMan;
CREATE OR REPLACE FUNCTION AfterDoMan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('doman', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterDoMan
    AFTER DELETE OR UPDATE ON doman
    FOR EACH ROW EXECUTE FUNCTION AfterDoMan();

-- Table: dukiendidan
-- DROP TRIGGER IF EXISTS TriggerBeforeDuKienDiDan ON dukiendidan; 
-- DROP FUNCTION BeforeDuKienDiDan;
CREATE OR REPLACE FUNCTION BeforeDuKienDiDan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('dukiendidan', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeDuKienDiDan
    BEFORE INSERT OR UPDATE ON dukiendidan
    FOR EACH ROW EXECUTE FUNCTION BeforeDuKienDiDan();

-- DROP TRIGGER IF EXISTS TriggerAfterDuKienDiDan ON dukiendidan; 
-- DROP FUNCTION AfterDuKienDiDan;
CREATE OR REPLACE FUNCTION AfterDuKienDiDan() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('dukiendidan', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterDuKienDiDan
    AFTER DELETE OR UPDATE ON dukiendidan
    FOR EACH ROW EXECUTE FUNCTION AfterDuKienDiDan();

-- Table: giaothong_pgon
-- DROP TRIGGER IF EXISTS TriggerBeforeGiaoThongPgon ON giaothong_pgon; 
-- DROP FUNCTION BeforeGiaoThongPgon;
CREATE OR REPLACE FUNCTION BeforeGiaoThongPgon() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('giaothong_pgon', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeGiaoThongPgon
    BEFORE INSERT OR UPDATE ON giaothong_pgon
    FOR EACH ROW EXECUTE FUNCTION BeforeGiaoThongPgon();

-- DROP TRIGGER IF EXISTS TriggerAfterGiaoThongPgon ON giaothong_pgon; 
-- DROP FUNCTION AfterGiaoThongPgon;
CREATE OR REPLACE FUNCTION AfterGiaoThongPgon() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('giaothong_pgon', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterGiaoThongPgon
    AFTER DELETE OR UPDATE ON giaothong_pgon
    FOR EACH ROW EXECUTE FUNCTION AfterGiaoThongPgon();

-- Table: huongdichuyen
-- DROP TRIGGER IF EXISTS TriggerBeforeHuongDiChuyen ON huongdichuyen; 
-- DROP FUNCTION BeforeHuongDiChuyen;
CREATE OR REPLACE FUNCTION BeforeHuongDiChuyen() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('huongdichuyen', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeHuongDiChuyen
    BEFORE INSERT OR UPDATE ON huongdichuyen
    FOR EACH ROW EXECUTE FUNCTION BeforeHuongDiChuyen();

-- DROP TRIGGER IF EXISTS TriggerAfterHuongDiChuyen ON huongdichuyen; 
-- DROP FUNCTION AfterHuongDiChuyen;
CREATE OR REPLACE FUNCTION AfterHuongDiChuyen() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('huongdichuyen', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterHuongDiChuyen
    AFTER DELETE OR UPDATE ON huongdichuyen
    FOR EACH ROW EXECUTE FUNCTION AfterHuongDiChuyen();

-- Table: ke
-- DROP TRIGGER IF EXISTS TriggerBeforeKe ON ke; 
-- DROP FUNCTION BeforeKe;
CREATE OR REPLACE FUNCTION BeforeKe() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('ke', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeKe
    BEFORE INSERT OR UPDATE ON ke
    FOR EACH ROW EXECUTE FUNCTION BeforeKe();

-- DROP TRIGGER IF EXISTS TriggerAfterKe ON ke; 
-- DROP FUNCTION AfterKe;
CREATE OR REPLACE FUNCTION AfterKe() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('ke', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterKe
    AFTER DELETE OR UPDATE ON ke
    FOR EACH ROW EXECUTE FUNCTION AfterKe();

-- Table: locxoay
-- DROP TRIGGER IF EXISTS TriggerBeforeLocXoay ON locxoay; 
-- DROP FUNCTION BeforeLocXoay;
CREATE OR REPLACE FUNCTION BeforeLocXoay() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('locxoay', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeLocXoay
    BEFORE INSERT OR UPDATE ON locxoay
    FOR EACH ROW EXECUTE FUNCTION BeforeLocXoay();

-- DROP TRIGGER IF EXISTS TriggerAfterLocXoay ON locxoay; 
-- DROP FUNCTION AfterLocXoay;
CREATE OR REPLACE FUNCTION AfterLocXoay() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('locxoay', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterLocXoay
    AFTER DELETE OR UPDATE ON locxoay
    FOR EACH ROW EXECUTE FUNCTION AfterLocXoay();

-- Table: lucluonghuydong
-- DROP TRIGGER IF EXISTS TriggerBeforeLucLuongHuyDong ON lucluonghuydong; 
-- DROP FUNCTION BeforeLucLuongHuyDong;
CREATE OR REPLACE FUNCTION BeforeLucLuongHuyDong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('lucluonghuydong', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeLucLuongHuyDong
    BEFORE INSERT OR UPDATE ON lucluonghuydong
    FOR EACH ROW EXECUTE FUNCTION BeforeLucLuongHuyDong();

-- DROP TRIGGER IF EXISTS TriggerAfterLucLuongHuyDong ON lucluonghuydong; 
-- DROP FUNCTION AfterLucLuongHuyDong;
CREATE OR REPLACE FUNCTION AfterLucLuongHuyDong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('lucluonghuydong', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterLucLuongHuyDong
    AFTER DELETE OR UPDATE ON lucluonghuydong
    FOR EACH ROW EXECUTE FUNCTION AfterLucLuongHuyDong();

-- Table: member
-- DROP TRIGGER IF EXISTS TriggerBeforeMember ON member; 
-- DROP FUNCTION BeforeMember;
CREATE OR REPLACE FUNCTION BeforeMember() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('member', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMember
    BEFORE INSERT OR UPDATE ON member
    FOR EACH ROW EXECUTE FUNCTION BeforeMember();

-- DROP TRIGGER IF EXISTS TriggerAfterMember ON member; 
-- DROP FUNCTION AfterMember;
CREATE OR REPLACE FUNCTION AfterMember() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('member', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMember
    AFTER DELETE OR UPDATE ON member
    FOR EACH ROW EXECUTE FUNCTION AfterMember();

-- Table: memberlogins
-- DROP TRIGGER IF EXISTS TriggerBeforeMemberlogins ON memberlogins; 
-- DROP FUNCTION BeforeMemberlogins;
CREATE OR REPLACE FUNCTION BeforeMemberlogins() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('memberlogins', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMemberlogins
    BEFORE INSERT OR UPDATE ON memberlogins
    FOR EACH ROW EXECUTE FUNCTION BeforeMemberlogins();

-- DROP TRIGGER IF EXISTS TriggerAfterMemberlogins ON memberlogins; 
-- DROP FUNCTION AfterMemberlogins;
CREATE OR REPLACE FUNCTION AfterMemberlogins() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('memberlogins', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMemberlogins
    AFTER DELETE OR UPDATE ON memberlogins
    FOR EACH ROW EXECUTE FUNCTION AfterMemberlogins();

-- Table: roles
-- DROP TRIGGER IF EXISTS TriggerBeforeRoles ON roles; 
-- DROP FUNCTION BeforeRoles;
CREATE OR REPLACE FUNCTION BeforeRoles() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('roles', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeRoles
    BEFORE INSERT OR UPDATE ON roles
    FOR EACH ROW EXECUTE FUNCTION BeforeRoles();

-- DROP TRIGGER IF EXISTS TriggerAfterRoles ON roles; 
-- DROP FUNCTION AfterRoles;
CREATE OR REPLACE FUNCTION AfterRoles() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('roles', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterRoles
    AFTER DELETE OR UPDATE ON roles
    FOR EACH ROW EXECUTE FUNCTION AfterRoles();

-- Table: memberroles
-- DROP TRIGGER IF EXISTS TriggerBeforeMemberRoles ON memberroles; 
-- DROP FUNCTION BeforeMemberRoles;
CREATE OR REPLACE FUNCTION BeforeMemberRoles() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('memberroles', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMemberRoles
    BEFORE INSERT OR UPDATE ON memberroles
    FOR EACH ROW EXECUTE FUNCTION BeforeMemberRoles();

-- DROP TRIGGER IF EXISTS TriggerAfterMemberRoles ON memberroles; 
-- DROP FUNCTION AfterMemberRoles;
CREATE OR REPLACE FUNCTION AfterMemberRoles() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('memberroles', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMemberRoles
    AFTER DELETE OR UPDATE ON memberroles
    FOR EACH ROW EXECUTE FUNCTION AfterMemberRoles();

-- Table: moccanhbaotrieucuong
-- DROP TRIGGER IF EXISTS TriggerBeforeMocCanhBaoTrieuCuong ON moccanhbaotrieucuong; 
-- DROP FUNCTION BeforeMocCanhBaoTrieuCuong;
CREATE OR REPLACE FUNCTION BeforeMocCanhBaoTrieuCuong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('moccanhbaotrieucuong', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMocCanhBaoTrieuCuong
    BEFORE INSERT OR UPDATE ON moccanhbaotrieucuong
    FOR EACH ROW EXECUTE FUNCTION BeforeMocCanhBaoTrieuCuong();

-- DROP TRIGGER IF EXISTS TriggerAfterMocCanhBaoTrieuCuong ON moccanhbaotrieucuong; 
-- DROP FUNCTION AfterMocCanhBaoTrieuCuong;
CREATE OR REPLACE FUNCTION AfterMocCanhBaoTrieuCuong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('moccanhbaotrieucuong', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMocCanhBaoTrieuCuong
    AFTER DELETE OR UPDATE ON moccanhbaotrieucuong
    FOR EACH ROW EXECUTE FUNCTION AfterMocCanhBaoTrieuCuong();

-- Table: mua
-- DROP TRIGGER IF EXISTS TriggerBeforeMua ON mua; 
-- DROP FUNCTION BeforeMua;
CREATE OR REPLACE FUNCTION BeforeMua() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('mua', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMua
    BEFORE INSERT OR UPDATE ON mua
    FOR EACH ROW EXECUTE FUNCTION BeforeMua();

-- DROP TRIGGER IF EXISTS TriggerAfterMua ON mua; 
-- DROP FUNCTION AfterMua;
CREATE OR REPLACE FUNCTION AfterMua() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('mua', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMua
    AFTER DELETE OR UPDATE ON mua
    FOR EACH ROW EXECUTE FUNCTION AfterMua();

-- Table: mucnuoc
-- DROP TRIGGER IF EXISTS TriggerBeforeMucNuoc ON mucnuoc; 
-- DROP FUNCTION BeforeMucNuoc;
CREATE OR REPLACE FUNCTION BeforeMucNuoc() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('mucnuoc', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeMucNuoc
    BEFORE INSERT OR UPDATE ON mucnuoc
    FOR EACH ROW EXECUTE FUNCTION BeforeMucNuoc();

-- DROP TRIGGER IF EXISTS TriggerAfterMucNuoc ON mucnuoc; 
-- DROP FUNCTION AfterMucNuoc;
CREATE OR REPLACE FUNCTION AfterMucNuoc() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('mucnuoc', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterMucNuoc
    AFTER DELETE OR UPDATE ON mucnuoc
    FOR EACH ROW EXECUTE FUNCTION AfterMucNuoc();

-- Table: nangnong
-- DROP TRIGGER IF EXISTS TriggerBeforeNangNong ON nangnong; 
-- DROP FUNCTION BeforeNangNong;
CREATE OR REPLACE FUNCTION BeforeNangNong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('nangnong', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeNangNong
    BEFORE INSERT OR UPDATE ON nangnong
    FOR EACH ROW EXECUTE FUNCTION BeforeNangNong();

-- DROP TRIGGER IF EXISTS TriggerAfterNangNong ON nangnong; 
-- DROP FUNCTION AfterNangNong;
CREATE OR REPLACE FUNCTION AfterNangNong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('nangnong', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterNangNong
    AFTER DELETE OR UPDATE ON nangnong
    FOR EACH ROW EXECUTE FUNCTION AfterNangNong();

-- Table: phuongtienhuydong
-- DROP TRIGGER IF EXISTS TriggerBeforePhuongTienHuyDong ON phuongtienhuydong; 
-- DROP FUNCTION BeforePhuongTienHuyDong;
CREATE OR REPLACE FUNCTION BeforePhuongTienHuyDong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('phuongtienhuydong', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforePhuongTienHuyDong
    BEFORE INSERT OR UPDATE ON phuongtienhuydong
    FOR EACH ROW EXECUTE FUNCTION BeforePhuongTienHuyDong();

-- DROP TRIGGER IF EXISTS TriggerAfterPhuongTienHuyDong ON phuongtienhuydong; 
-- DROP FUNCTION AfterPhuongTienHuyDong;
CREATE OR REPLACE FUNCTION AfterPhuongTienHuyDong() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('phuongtienhuydong', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterPhuongTienHuyDong
    AFTER DELETE OR UPDATE ON phuongtienhuydong
    FOR EACH ROW EXECUTE FUNCTION AfterPhuongTienHuyDong();

-- Table: rghuyen
-- DROP TRIGGER IF EXISTS TriggerBeforeRgHuyen ON rghuyen; 
-- DROP FUNCTION BeforeRgHuyen;
CREATE OR REPLACE FUNCTION BeforeRgHuyen() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('rghuyen', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeRgHuyen
    BEFORE INSERT OR UPDATE ON rghuyen
    FOR EACH ROW EXECUTE FUNCTION BeforeRgHuyen();

-- DROP TRIGGER IF EXISTS TriggerAfterRgHuyen ON rghuyen; 
-- DROP FUNCTION AfterRgHuyen;
CREATE OR REPLACE FUNCTION AfterRgHuyen() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('rghuyen', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterRgHuyen
    AFTER DELETE OR UPDATE ON rghuyen
    FOR EACH ROW EXECUTE FUNCTION AfterRgHuyen();

-- Table: rgxa
-- DROP TRIGGER IF EXISTS TriggerBeforeRgXa ON rgxa; 
-- DROP FUNCTION BeforeRgXa;
CREATE OR REPLACE FUNCTION BeforeRgXa() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('rgxa', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeRgXa
    BEFORE INSERT OR UPDATE ON rgxa
    FOR EACH ROW EXECUTE FUNCTION BeforeRgXa();

-- DROP TRIGGER IF EXISTS TriggerAfterRgXa ON rgxa; 
-- DROP FUNCTION AfterRgXa;
CREATE OR REPLACE FUNCTION AfterRgXa() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('rgxa', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterRgXa
    AFTER DELETE OR UPDATE ON rgxa
    FOR EACH ROW EXECUTE FUNCTION AfterRgXa();

-- Table: satlo_line
-- DROP TRIGGER IF EXISTS TriggerBeforeSatLoLine ON satlo_line; 
-- DROP FUNCTION BeforeSatLoLine;
CREATE OR REPLACE FUNCTION BeforeSatLoLine() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('satlo_line', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeSatLoLine
    BEFORE INSERT OR UPDATE ON satlo_line
    FOR EACH ROW EXECUTE FUNCTION BeforeSatLoLine();

-- DROP TRIGGER IF EXISTS TriggerAfterSatLoLine ON satlo_line; 
-- DROP FUNCTION AfterSatLoLine;
CREATE OR REPLACE FUNCTION AfterSatLoLine() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('satlo_line', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterSatLoLine
    AFTER DELETE OR UPDATE ON satlo_line
    FOR EACH ROW EXECUTE FUNCTION AfterSatLoLine();

-- Table: satlo_point
-- DROP TRIGGER IF EXISTS TriggerBeforeSatLoPoint ON satlo_point; 
-- DROP FUNCTION BeforeSatLoPoint;
CREATE OR REPLACE FUNCTION BeforeSatLoPoint() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('satlo_point', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeSatLoPoint
    BEFORE INSERT OR UPDATE ON satlo_point
    FOR EACH ROW EXECUTE FUNCTION BeforeSatLoPoint();

-- DROP TRIGGER IF EXISTS TriggerAfterSatLoPoint ON satlo_point; 
-- DROP FUNCTION AfterSatLoPoint;
CREATE OR REPLACE FUNCTION AfterSatLoPoint() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('satlo_point', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterSatLoPoint
    AFTER DELETE OR UPDATE ON satlo_point
    FOR EACH ROW EXECUTE FUNCTION AfterSatLoPoint();

-- Table: thiethai_thientai
-- DROP TRIGGER IF EXISTS TriggerBeforeThietHaiThienTai ON thiethai_thientai; 
-- DROP FUNCTION BeforeThietHaiThienTai;
CREATE OR REPLACE FUNCTION BeforeThietHaiThienTai() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('thiethai_thientai', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeThietHaiThienTai
    BEFORE INSERT OR UPDATE ON thiethai_thientai
    FOR EACH ROW EXECUTE FUNCTION BeforeThietHaiThienTai();

-- DROP TRIGGER IF EXISTS TriggerAfterThietHaiThienTai ON thiethai_thientai; 
-- DROP FUNCTION AfterThietHaiThienTai;
CREATE OR REPLACE FUNCTION AfterThietHaiThienTai() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('thiethai_thientai', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterThietHaiThienTai
    AFTER DELETE OR UPDATE ON thiethai_thientai
    FOR EACH ROW EXECUTE FUNCTION AfterThietHaiThienTai();

-- Table: thuyhe_pgon
-- DROP TRIGGER IF EXISTS TriggerBeforeThuyHePgon ON thuyhe_pgon; 
-- DROP FUNCTION BeforeThuyHePgon;
CREATE OR REPLACE FUNCTION BeforeThuyHePgon() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('thuyhe_pgon', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeThuyHePgon
    BEFORE INSERT OR UPDATE ON thuyhe_pgon
    FOR EACH ROW EXECUTE FUNCTION BeforeThuyHePgon();

-- DROP TRIGGER IF EXISTS TriggerAfterThuyHePgon ON thuyhe_pgon; 
-- DROP FUNCTION AfterThuyHePgon;
CREATE OR REPLACE FUNCTION AfterThuyHePgon() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('thuyhe_pgon', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterThuyHePgon
    AFTER DELETE OR UPDATE ON thuyhe_pgon
    FOR EACH ROW EXECUTE FUNCTION AfterThuyHePgon();

-- Table: tulieuhinhanh
-- DROP TRIGGER IF EXISTS TriggerBeforeTuLieuHinhAnh ON tulieuhinhanh; 
-- DROP FUNCTION BeforeTuLieuHinhAnh;
CREATE OR REPLACE FUNCTION BeforeTuLieuHinhAnh() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieuhinhanh', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeTuLieuHinhAnh
    BEFORE INSERT OR UPDATE ON tulieuhinhanh
    FOR EACH ROW EXECUTE FUNCTION BeforeTuLieuHinhAnh();

-- DROP TRIGGER IF EXISTS TriggerAfterTuLieuHinhAnh ON tulieuhinhanh; 
-- DROP FUNCTION AfterTuLieuHinhAnh;
CREATE OR REPLACE FUNCTION AfterTuLieuHinhAnh() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieuhinhanh', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterTuLieuHinhAnh
    AFTER DELETE OR UPDATE ON tulieuhinhanh
    FOR EACH ROW EXECUTE FUNCTION AfterTuLieuHinhAnh();


-- Table: tulieukhac
-- DROP TRIGGER IF EXISTS TriggerBeforeTuLieuKhac ON tulieukhac; 
-- DROP FUNCTION BeforeTuLieuKhac;
CREATE OR REPLACE FUNCTION BeforeTuLieuKhac() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieukhac', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeTuLieuKhac
    BEFORE INSERT OR UPDATE ON tulieukhac
    FOR EACH ROW EXECUTE FUNCTION BeforeTuLieuKhac();

-- DROP TRIGGER IF EXISTS TriggerAfterTuLieuKhac ON tulieukhac; 
-- DROP FUNCTION AfterTuLieuKhac;
CREATE OR REPLACE FUNCTION AfterTuLieuKhac() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieukhac', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterTuLieuKhac
    AFTER DELETE OR UPDATE ON tulieukhac
    FOR EACH ROW EXECUTE FUNCTION AfterTuLieuKhac();

-- Table: tulieuvideo
-- DROP TRIGGER IF EXISTS TriggerBeforeTuLieuVideo ON tulieuvideo; 
-- DROP FUNCTION BeforeTuLieuVideo;
CREATE OR REPLACE FUNCTION BeforeTuLieuVideo() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieuvideo', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeTuLieuVideo
    BEFORE INSERT OR UPDATE ON tulieuvideo
    FOR EACH ROW EXECUTE FUNCTION BeforeTuLieuVideo();

-- DROP TRIGGER IF EXISTS TriggerAfterTuLieuVideo ON tulieuvideo; 
-- DROP FUNCTION AfterTuLieuVideo;
CREATE OR REPLACE FUNCTION AfterTuLieuVideo() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('tulieuvideo', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterTuLieuVideo
    AFTER DELETE OR UPDATE ON tulieuvideo
    FOR EACH ROW EXECUTE FUNCTION AfterTuLieuVideo();

-- Table: xalu
-- DROP TRIGGER IF EXISTS TriggerBeforeXaLu ON xalu; 
-- DROP FUNCTION BeforeXaLu;
CREATE OR REPLACE FUNCTION BeforeXaLu() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(NEW.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('xalu', payload::text));
        RETURN NEW;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerBeforeXaLu
    BEFORE INSERT OR UPDATE ON xalu
    FOR EACH ROW EXECUTE FUNCTION BeforeXaLu();

-- DROP TRIGGER IF EXISTS TriggerAfterXaLu ON xalu; 
-- DROP FUNCTION AfterXaLu;
CREATE OR REPLACE FUNCTION AfterXaLu() 
RETURNS trigger AS $$
    DECLARE
        payload jsonb;
    BEGIN
        SELECT to_jsonb(OLD.*) INTO payload;
        payload := jsonb_set(payload, '{dml_action}', to_jsonb(TG_OP));
        PERFORM (SELECT pg_notify('xalu', payload::text));
        RETURN OLD;
    END;
$$ LANGUAGE 'plpgsql';
CREATE TRIGGER TriggerAfterXaLu
    AFTER DELETE OR UPDATE ON xalu
    FOR EACH ROW EXECUTE FUNCTION AfterXaLu();