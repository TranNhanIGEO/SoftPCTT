-- DROP INDEX
DROP INDEX GiaoThong_Pgon_IDX;
DROP INDEX ThuyHe_Pgon_IDX;
DROP INDEX History_IDX;

-- DROP FUNCTION
DROP FUNCTION LoginMember;
DROP FUNCTION GetHuyens;
DROP FUNCTION GetHuyen;
DROP FUNCTION GetXas;
DROP FUNCTION GetGiaoThongs;
-- DROP FUNCTION GetGiaoThong; 
DROP FUNCTION GetThuyHes;
-- DROP FUNCTION GetThuyHe;
DROP FUNCTION PasswordChangeTime;
DROP FUNCTION GetMemberRoles(Character varying);
DROP FUNCTION CountLogins;
DROP FUNCTION GetMembers;
DROP FUNCTION GetMember;
DROP FUNCTION GetRoleOfMember;
DROP FUNCTION GetRoleByMember;
DROP FUNCTION GetDeBaoBoBaos;
DROP FUNCTION GetDeBaoBoBao;
DROP FUNCTION GetMocCanhBaoTrieuCuongs;
DROP FUNCTION GetMocCanhBaoTrieuCuong;
DROP FUNCTION GetDiemAnToans;
DROP FUNCTION GetDiemAnToan;
DROP FUNCTION GetDiemXungYeus;
DROP FUNCTION GetCongDaps;
DROP FUNCTION GetCongDap;
DROP FUNCTION GetKes;
DROP FUNCTION GetKe;
DROP FUNCTION GetApThapNhietDois;
DROP FUNCTION GetApThapNhietDoi;
-- DROP FUNCTION GetBaos;
DROP FUNCTION GetDoMans;
DROP FUNCTION GetDoMan;
-- DROP FUNCTION SalinityStatistics;
DROP FUNCTION GetBaos;
DROP FUNCTION GetBao;
DROP FUNCTION GetNangNongs;
DROP FUNCTION GetNangNong;
-- DROP FUNCTION TemperatureStatistics;
DROP FUNCTION GetMucNuocs;
DROP FUNCTION GetMucNuoc;
-- DROP FUNCTION TidalStatistics;
DROP FUNCTION GetMuas;
DROP FUNCTION GetMua;
-- DROP FUNCTION RainStatistics;
DROP FUNCTION GetHoChuas;
DROP FUNCTION GetHoChua;
-- DROP FUNCTION ReservoirStatistics;
DROP FUNCTION GetRefreshToken;
DROP FUNCTION GetThietHaiThienTais;
DROP FUNCTION GetThietHaiThienTai;
DROP FUNCTION GetLocXoays;
DROP FUNCTION GetLocXoay;
DROP FUNCTION GetChayRungs;
DROP FUNCTION GetChayRung;
DROP FUNCTION GetKhuNeoDaus;
DROP FUNCTION GetKhuNeoDau;
DROP FUNCTION GetDanhBaDTs;
DROP FUNCTION GetDuKienDiDans;
DROP FUNCTION GetDuKienDiDan;
DROP FUNCTION GetBienCanhBaoSatLos;
DROP FUNCTION GetBienCanhBaoSatLo;
DROP FUNCTION GetTuLieuHinhAnhs;
DROP FUNCTION GetTuLieuHinhAnh;
DROP FUNCTION GetTuLieuKhacs;
DROP FUNCTION GetTuLieuKhac;
DROP FUNCTION GetTuLieuVideos;
DROP FUNCTION GetTuLieuVideo;
DROP FUNCTION GetLucLuongHuyDongs;
DROP FUNCTION GetLucLuongHuyDong;
DROP FUNCTION GetPhuongTienHuyDongs;
DROP FUNCTION GetPhuongTienHuyDong;
DROP FUNCTION GetSatLoLines;
DROP FUNCTION GetSatLoLine;
DROP FUNCTION GetSatLoPoints;
DROP FUNCTION GetHuongDiChuyens;
DROP FUNCTION GetHuongDiChuyen;
DROP FUNCTION GetDiemXungYeu;
DROP FUNCTION GetLopDoiTuongs;  
DROP FUNCTION GetDanhBaDT;
DROP FUNCTION GetThuyHeHoChuas;
DROP FUNCTION GetQdhoangSaTruongSa;
DROP FUNCTION GetHistory;

-- DROP PROCEDURE
DROP PROCEDURE AddMemberLogin;
DROP PROCEDURE AddMember;
DROP PROCEDURE EditMember;
DROP PROCEDURE DeleteMember;
DROP PROCEDURE AddMemberRoles;
DROP PROCEDURE ChangePassword;
DROP PROCEDURE ResetPassword;
DROP PROCEDURE EditMemberLogin;
-- DROP PROCEDURE EditMemberRoles;
-- DROP PROCEDURE DeleteMemberRoleByDistrict;
-- DROP PROCEDURE DeleteMemberRoleByFunction;
DROP PROCEDURE DeleteMemberRole;
DROP PROCEDURE AddRefreshToken;
DROP PROCEDURE EditRefreshToken;
DROP PROCEDURE DeteleRefreshToken;
DROP PROCEDURE AddPhuongTienHuyDong;
DROP PROCEDURE EditPhuongTienHuyDong;
DROP PROCEDURE DeletePhuongTienHuyDong;
DROP PROCEDURE AddLucLuongHuyDong;
DROP PROCEDURE EditLucLuongHuyDong;
DROP PROCEDURE DeleteLucLuongHuyDong;
DROP PROCEDURE AddDanhBaDT;
DROP PROCEDURE EditDanhBaDT;
DROP PROCEDURE DeleteDanhbaDT;
DROP PROCEDURE AddHuongDiChuyen;
DROP PROCEDURE EditHuongDiChuyen;
DROP PROCEDURE DeleteHuongDiChuyen;
DROP PROCEDURE AddDiemAnToan;
DROP PROCEDURE EditDiemAnToan;
DROP PROCEDURE DeleteDiemAnToan;
DROP PROCEDURE AddDiemXungYeu;
DROP PROCEDURE EditDiemXungYeu;
DROP PROCEDURE DeleteDiemXungYeu;
DROP PROCEDURE AddDuKienDiDan;
DROP PROCEDURE EditDuKienDiDan;
DROP PROCEDURE DeleteDuKienDiDan;
DROP PROCEDURE AddLocXoay;
DROP PROCEDURE EditLocXoay;
DROP PROCEDURE DeleteLocXoay;
DROP PROCEDURE AddHistory;
DROP PROCEDURE EditHistory;
DROP PROCEDURE DeleteHistory;
DROP PROCEDURE AddTuLieuHinhAnh;
DROP PROCEDURE EditTuLieuHinhAnh;
DROP PROCEDURE DeleteTuLieuHinhAnh;
DROP PROCEDURE AddTuLieuVideo;
DROP PROCEDURE EditTuLieuVideo;
DROP PROCEDURE DeleteTuLieuVideo;
DROP PROCEDURE AddTuLieuKhac;
DROP PROCEDURE EditTuLieuKhac;
DROP PROCEDURE DeleteTuLieuKhac;
DROP PROCEDURE AddDoMan;
DROP PROCEDURE EditDoMan;
DROP PROCEDURE DeleteDoMan;
DROP PROCEDURE AddNangNong;
DROP PROCEDURE EditNangNong;
DROP PROCEDURE DeleteNangNong;
DROP PROCEDURE AddMua;
DROP PROCEDURE EditMua;
DROP PROCEDURE DeleteMua;
DROP PROCEDURE AddMucNuoc;
DROP PROCEDURE EditMucNuoc;
DROP PROCEDURE DeleteMucNuoc;
DROP PROCEDURE AddHoChua;
DROP PROCEDURE EditHoChua;
DROP PROCEDURE DeleteHoChua;
DROP PROCEDURE AddCongDap;
DROP PROCEDURE EditCongDap;
DROP PROCEDURE DeleteCongDap;
DROP PROCEDURE AddMocCanhBaoTrieuCuong;
DROP PROCEDURE EditMocCanhBaoTrieuCuong;
DROP PROCEDURE DeleteMocCanhBaoTrieuCuong;
DROP PROCEDURE AddBienCanhBaoSatLo;
DROP PROCEDURE EditBienCanhBaoSatLo;
DROP PROCEDURE DeleteBienCanhBaoSatLo;
DROP PROCEDURE AddKhuNeoDau;
DROP PROCEDURE EditKhuNeoDau;
DROP PROCEDURE DeleteKhuNeoDau;
DROP PROCEDURE AddThietHaiThienTai;
DROP PROCEDURE EditThietHaiThienTai;
DROP PROCEDURE DeleteThietHaiThienTai;
DROP PROCEDURE AddSatLoLine;
DROP PROCEDURE EditSatLoLine;
DROP PROCEDURE DeleteSatLoLine;
DROP PROCEDURE AddKe;
DROP PROCEDURE EditKe;
DROP PROCEDURE DeleteKe;
DROP PROCEDURE AddApThapNhietDoi;
DROP PROCEDURE EditApThapNhietDoi;
DROP PROCEDURE DeleteApThapNhietDoi;
DROP PROCEDURE AddBao;
DROP PROCEDURE EditBao;
DROP PROCEDURE DeleteBao;
DROP PROCEDURE AddDeBaoBoBao;
DROP PROCEDURE EditDeBaoBoBao;
DROP PROCEDURE DeleteDeBaoBoBao;
DROP PROCEDURE AddChayRung;
DROP PROCEDURE EditChayRung;
DROP PROCEDURE DeleteChayRung;

-- DROP TRIGGER FUNCTION

DROP TRIGGER IF EXISTS TriggerBeforeRoles ON roles; 
DROP FUNCTION BeforeRoles;
DROP TRIGGER IF EXISTS TriggerAfterRoles ON roles; 
DROP FUNCTION AfterRoles;

DROP TRIGGER IF EXISTS TriggerBeforeMemberRoles ON memberroles; 
DROP FUNCTION BeforeMemberRoles;
DROP TRIGGER IF EXISTS TriggerAfterMemberRoles ON memberroles; 
DROP FUNCTION AfterMemberRoles;

DROP TRIGGER IF EXISTS TriggerBeforeApThapNhietDoi ON apthapnhietdoi; 
DROP FUNCTION BeforeApThapNhietDoi;
DROP TRIGGER IF EXISTS TriggerAfterApThapNhietDoi ON apthapnhietdoi; 
DROP FUNCTION AfterApThapNhietDoi;

DROP TRIGGER IF EXISTS TriggerBeforeBao ON bao; 
DROP FUNCTION BeforeBao;
DROP TRIGGER IF EXISTS TriggerAfterBao ON bao; 
DROP FUNCTION AfterBao;

DROP TRIGGER IF EXISTS TriggerBeforeBienCanhBaoSatLo ON biencanhbaosatlo; 
DROP FUNCTION BeforeBienCanhBaoSatLo;
DROP TRIGGER IF EXISTS TriggerAfterBienCanhBaoSatLo ON biencanhbaosatlo; 
DROP FUNCTION AfterBienCanhBaoSatLo;

DROP TRIGGER IF EXISTS TriggerBeforeCongDap ON congdap; 
DROP FUNCTION BeforeCongDap;
DROP TRIGGER IF EXISTS TriggerAfterCongDap ON congdap; 
DROP FUNCTION AfterCongDap;

DROP TRIGGER IF EXISTS TriggerBeforeDeBaoboBao ON debao_bobao; 
DROP FUNCTION BeforeDeBaoboBao;
DROP TRIGGER IF EXISTS TriggerAfterDeBaoboBao ON debao_bobao; 
DROP FUNCTION AfterDeBaoboBao;

DROP TRIGGER IF EXISTS TriggerBeforeDiemAnToan ON diemantoan; 
DROP FUNCTION BeforeDiemAnToan;
DROP TRIGGER IF EXISTS TriggerAfterDiemAnToan ON diemantoan; 
DROP FUNCTION AfterDiemAnToan;

DROP TRIGGER IF EXISTS TriggerBeforeDiemXungYeu ON diemxungyeu; 
DROP FUNCTION BeforeDiemXungYeu;
DROP TRIGGER IF EXISTS TriggerAfterDiemXungYeu ON diemxungyeu; 
DROP FUNCTION AfterDiemXungYeu;

DROP TRIGGER IF EXISTS TriggerBeforeDoMan ON doman; 
DROP FUNCTION BeforeDoMan;
DROP TRIGGER IF EXISTS TriggerAfterDoMan ON doman; 
DROP FUNCTION AfterDoMan;

DROP TRIGGER IF EXISTS TriggerBeforeDuKienDiDan ON dukiendidan; 
DROP FUNCTION BeforeDuKienDiDan;
DROP TRIGGER IF EXISTS TriggerAfterDuKienDiDan ON dukiendidan; 
DROP FUNCTION AfterDuKienDiDan;

DROP TRIGGER IF EXISTS TriggerBeforeGiaoThongPgon ON giaothong_pgon; 
DROP FUNCTION BeforeGiaoThongPgon;
DROP TRIGGER IF EXISTS TriggerAfterGiaoThongPgon ON giaothong_pgon; 
DROP FUNCTION AfterGiaoThongPgon;

DROP TRIGGER IF EXISTS TriggerBeforeHuongDiChuyen ON huongdichuyen; 
DROP FUNCTION BeforeHuongDiChuyen;
DROP TRIGGER IF EXISTS TriggerAfterHuongDiChuyen ON huongdichuyen; 
DROP FUNCTION AfterHuongDiChuyen;

DROP TRIGGER IF EXISTS TriggerBeforeKe ON ke; 
DROP FUNCTION BeforeKe;
DROP TRIGGER IF EXISTS TriggerAfterKe ON ke; 
DROP FUNCTION AfterKe;

DROP TRIGGER IF EXISTS TriggerBeforeLocXoay ON locxoay; 
DROP FUNCTION BeforeLocXoay;
DROP TRIGGER IF EXISTS TriggerAfterLocXoay ON locxoay; 
DROP FUNCTION AfterLocXoay;

DROP TRIGGER IF EXISTS TriggerBeforeLucLuongHuyDong ON lucluonghuydong; 
DROP FUNCTION BeforeLucLuongHuyDong;
DROP TRIGGER IF EXISTS TriggerAfterLucLuongHuyDong ON lucluonghuydong; 
DROP FUNCTION AfterLucLuongHuyDong;

DROP TRIGGER IF EXISTS TriggerBeforeMember ON member; 
DROP FUNCTION BeforeMember;
DROP TRIGGER IF EXISTS TriggerAfterMember ON member; 
DROP FUNCTION AfterMember;

DROP TRIGGER IF EXISTS TriggerBeforeMemberlogins ON memberlogins; 
DROP FUNCTION BeforeMemberlogins;
DROP TRIGGER IF EXISTS TriggerAfterMemberlogins ON memberlogins; 
DROP FUNCTION AfterMemberlogins;

DROP TRIGGER IF EXISTS TriggerBeforeMocCanhBaoTrieuCuong ON moccanhbaotrieucuong; 
DROP FUNCTION BeforeMocCanhBaoTrieuCuong;
DROP TRIGGER IF EXISTS TriggerAfterMocCanhBaoTrieuCuong ON moccanhbaotrieucuong; 
DROP FUNCTION AfterMocCanhBaoTrieuCuong;

DROP TRIGGER IF EXISTS TriggerBeforeMua ON mua; 
DROP FUNCTION BeforeMua;
DROP TRIGGER IF EXISTS TriggerAfterMua ON mua; 
DROP FUNCTION AfterMua;

DROP TRIGGER IF EXISTS TriggerBeforeMucNuoc ON mucnuoc; 
DROP FUNCTION BeforeMucNuoc;
DROP TRIGGER IF EXISTS TriggerAfterMucNuoc ON mucnuoc; 
DROP FUNCTION AfterMucNuoc;

DROP TRIGGER IF EXISTS TriggerBeforeNangNong ON nangnong; 
DROP FUNCTION BeforeNangNong;
DROP TRIGGER IF EXISTS TriggerAfterNangNong ON nangnong; 
DROP FUNCTION AfterNangNong;

DROP TRIGGER IF EXISTS TriggerBeforePhuongTienHuyDong ON phuongtienhuydong; 
DROP FUNCTION BeforePhuongTienHuyDong;
DROP TRIGGER IF EXISTS TriggerAfterPhuongTienHuyDong ON phuongtienhuydong; 
DROP FUNCTION AfterPhuongTienHuyDong;

DROP TRIGGER IF EXISTS TriggerBeforeRgHuyen ON rghuyen; 
DROP FUNCTION BeforeRgHuyen;
DROP TRIGGER IF EXISTS TriggerAfterRgHuyen ON rghuyen; 
DROP FUNCTION AfterRgHuyen;

DROP TRIGGER IF EXISTS TriggerBeforeRgXa ON rgxa; 
DROP FUNCTION BeforeRgXa;
DROP TRIGGER IF EXISTS TriggerAfterRgXa ON rgxa; 
DROP FUNCTION AfterRgXa;

DROP TRIGGER IF EXISTS TriggerBeforeSatLoLine ON satlo_line; 
DROP FUNCTION BeforeSatLoLine;
DROP TRIGGER IF EXISTS TriggerAfterSatLoLine ON satlo_line; 
DROP FUNCTION AfterSatLoLine;

DROP TRIGGER IF EXISTS TriggerBeforeSatLoPoint ON satlo_point; 
DROP FUNCTION BeforeSatLoPoint;
DROP TRIGGER IF EXISTS TriggerAfterSatLoPoint ON satlo_point; 
DROP FUNCTION AfterSatLoPoint;

DROP TRIGGER IF EXISTS TriggerBeforeThietHaiThienTai ON thiethai_thientai; 
DROP FUNCTION BeforeThietHaiThienTai;
DROP TRIGGER IF EXISTS TriggerAfterThietHaiThienTai ON thiethai_thientai; 
DROP FUNCTION AfterThietHaiThienTai;

DROP TRIGGER IF EXISTS TriggerBeforeThuyHePgon ON thuyhe_pgon; 
DROP FUNCTION BeforeThuyHePgon;
DROP TRIGGER IF EXISTS TriggerAfterThuyHePgon ON thuyhe_pgon; 
DROP FUNCTION AfterThuyHePgon;

DROP TRIGGER IF EXISTS TriggerBeforeTuLieuHinhAnh ON tulieuhinhanh; 
DROP FUNCTION BeforeTuLieuHinhAnh;
DROP TRIGGER IF EXISTS TriggerAfterTuLieuHinhAnh ON tulieuhinhanh; 
DROP FUNCTION AfterTuLieuHinhAnh;

DROP TRIGGER IF EXISTS TriggerBeforeTuLieuKhac ON tulieukhac; 
DROP FUNCTION BeforeTuLieuKhac;
DROP TRIGGER IF EXISTS TriggerAfterTuLieuKhac ON tulieukhac; 
DROP FUNCTION AfterTuLieuKhac;

DROP TRIGGER IF EXISTS TriggerBeforeTuLieuVideo ON tulieuvideo; 
DROP FUNCTION BeforeTuLieuVideo;
DROP TRIGGER IF EXISTS TriggerAfterTuLieuVideo ON tulieuvideo; 
DROP FUNCTION AfterTuLieuVideo;

DROP TRIGGER IF EXISTS TriggerBeforeXaLu ON xalu; 
DROP FUNCTION BeforeXaLu;
DROP TRIGGER IF EXISTS TriggerAfterXaLu ON xalu; 
DROP FUNCTION AfterXaLu;

