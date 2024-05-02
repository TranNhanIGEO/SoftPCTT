using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class LucLuongHuyDongRepository : BaseRepository{
    public LucLuongHuyDongRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<LucLuongHuyDong> GetLucLuongHuyDongs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDong>("SELECT a.objectid, a.idkhlucluong, a.qhtp, a.tenlucluong, a.capql, a.slnguoihd, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.namsudung, a.mahuyen FROM LucLuongHuyDongDetail a LEFT JOIN RgHuyen h ON h.mahuyen = a.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.qhtp ASC, a.tenlucluong ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<LucLuongHuyDong>("SELECT * FROM GetLucLuongHuyDongs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDong>("SELECT a.objectid, a.idkhlucluong, a.qhtp, a.tenlucluong, a.capql, a.slnguoihd, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.namsudung, a.mahuyen FROM LucLuongHuyDong a LEFT JOIN RgHuyen h ON h.mahuyen = a.mahuyen WHERE " + SqlQuery + " ORDER BY a.qhtp ASC, a.tenlucluong ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<LucLuongHuyDong>("SELECT * FROM GetLucLuongHuyDongs(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public LucLuongHuyDongAddEdit? GetLucLuongHuyDong(int objectid){
        return connection.QueryFirstOrDefault<LucLuongHuyDongAddEdit>("SELECT * FROM GetLucLuongHuyDong(@_objectid)", new{
            _objectid = objectid
        });
    }
    public IEnumerable<LucLuongHuyDongHistory> GetLucLuongHuyDongHistory(){
        return connection.Query<LucLuongHuyDongHistory>("SELECT * FROM GetLucLuongHuyDongHistory()", commandType: CommandType.Text);
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM LucLuongHuyDongDetail", commandType: CommandType.Text);
    }
    public int Add(LucLuongHuyDongAddEdit obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        int? slnguoihd = obj.slnguoihd == null ? null : Convert.ToInt32(obj.slnguoihd);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        int? namsudung = obj.namsudung == null ? null : Convert.ToInt32(obj.namsudung);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("AddLucLuongHuyDong", 
                new{
                    _objectid = obj.objectid, 
                    _idkhlucluong = "LLHD" +  obj.objectid.ToString(),
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _qhtp = obj.qhtp,
                    _tenlucluong = obj.tenlucluong,
                    _capql = obj.capql,
                    _slnguoihd = slnguoihd,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _namsudung = namsudung,
                    _mahuyen = obj.mahuyen,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0])
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;        
        return connection.ExecuteScalar<int>("AddLucLuongHuyDong", 
            new{
                    _objectid = obj.objectid, 
                    _idkhlucluong = "LLHD" +  obj.objectid.ToString(),
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _qhtp = obj.qhtp,
                    _tenlucluong = obj.tenlucluong,
                    _capql = obj.capql,
                    _slnguoihd = slnguoihd,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _namsudung = namsudung,
                    _mahuyen = obj.mahuyen,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, LucLuongHuyDongAddEdit obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        int? slnguoihd = obj.slnguoihd == null ? null : Convert.ToInt32(obj.slnguoihd);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        int? namsudung = obj.namsudung == null ? null : Convert.ToInt32(obj.namsudung);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("EditLucLuongHuyDong", 
            new{
                    _objectid = objectid, 
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _qhtp = obj.qhtp,
                    _tenlucluong = obj.tenlucluong,
                    _capql = obj.capql,
                    _slnguoihd = slnguoihd,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _namsudung = namsudung,
                    _mahuyen = obj.mahuyen,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0])
                }, commandType: CommandType.StoredProcedure
            );  
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;  
        return connection.ExecuteScalar<int>("EditLucLuongHuyDong", 
            new{
                    _objectid = objectid, 
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _qhtp = obj.qhtp,
                    _tenlucluong = obj.tenlucluong,
                    _capql = obj.capql,
                    _slnguoihd = slnguoihd,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _namsudung = namsudung,
                    _mahuyen = obj.mahuyen,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteLucLuongHuyDong", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<LucLuongHuyDongDetailStatistics> GetLucLuongHuyDongDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDongDetailStatistics>("SELECT t.qhtp, t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong FROM ( SELECT qhtp, tenlucluong, (CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho, (CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen, (CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran, (CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho FROM lucluonghuydong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY a.qhtp, a.tenlucluong, a.capql ORDER BY a.qhtp ASC ) AS t GROUP BY t.qhtp, t.tenlucluong ORDER BY t.qhtp ASC, t.tenlucluong ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<LucLuongHuyDongDetailStatistics>("SELECT t.qhtp, t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong FROM ( SELECT qhtp, tenlucluong, (CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho, (CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen, (CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran, (CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho FROM lucluonghuydong a WHERE a.mahuyen = @_mahuyen GROUP BY a.qhtp, a.tenlucluong, a.capql ORDER BY a.qhtp ASC ) AS t GROUP BY t.qhtp, t.tenlucluong ORDER BY t.qhtp ASC, t.tenlucluong ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDongDetailStatistics>("SELECT t.qhtp, t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong FROM ( SELECT qhtp, tenlucluong, (CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho, (CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen, (CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran, (CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho FROM lucluonghuydong a WHERE " + SqlQuery + " GROUP BY a.qhtp, a.tenlucluong, a.capql ORDER BY a.qhtp ASC ) AS t GROUP BY t.qhtp, t.tenlucluong ORDER BY t.qhtp ASC, t.tenlucluong ASC");            
            }
            return connection.Query<LucLuongHuyDongDetailStatistics>("SELECT t.qhtp, t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong FROM ( SELECT qhtp, tenlucluong, (CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho, (CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen, (CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran, (CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho FROM lucluonghuydong a GROUP BY a.qhtp, a.tenlucluong, a.capql ORDER BY a.qhtp ASC ) AS t GROUP BY t.qhtp, t.tenlucluong ORDER BY t.qhtp ASC, t.tenlucluong ASC");  
        }       
    }
    public IEnumerable<LucLuongHuyDongTotalStatistics> GetLucLuongHuyDongTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
         if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDongTotalStatistics>("(SELECT a.tenlucluong::text, SUM(a.slnguoihd) AS tongcong, 'Lực lượng' AS phamvithongke FROM LucLuongHuyDongDetail a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY a.tenlucluong ORDER BY a.tenlucluong ASC)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<LucLuongHuyDongTotalStatistics>("(SELECT a.tenlucluong::text, SUM(a.slnguoihd) AS tongcong, 'Lực lượng' AS phamvithongke FROM LucLuongHuyDongDetail a WHERE a.mahuyen = @_mahuyen GROUP BY a.tenlucluong ORDER BY a.tenlucluong ASC)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{       
            if (SqlQuery != "null"){
                return connection.Query<LucLuongHuyDongTotalStatistics>("(SELECT a.tenlucluong::text, SUM(a.slnguoihd) AS tongcong, 'Lực lượng' AS phamvithongke FROM lucluonghuydong a WHERE " + SqlQuery + " GROUP BY a.tenlucluong ORDER BY a.tenlucluong ASC)");            
            }
            return connection.Query<LucLuongHuyDongTotalStatistics>("(SELECT a.tenlucluong::text, SUM(a.slnguoihd) AS tongcong, 'Lực lượng' AS phamvithongke FROM lucluonghuydong a GROUP BY a.tenlucluong ORDER BY a.tenlucluong ASC)");  
        }
    }
}