using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class PhuongTienHuyDongRepository : BaseRepository{
    public PhuongTienHuyDongRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<PhuongTienHuyDong> GetPhuongTienHuyDongs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<PhuongTienHuyDong>("SELECT a.objectid, a.idkhphuogtien, a.tenphuongtienttb, a.dvql, a.dvt, a.soluong, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.phannhom1, a.phannhom2, a.phannhom3, a.mahuyen FROM PhuongTienHuyDong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen ORDER BY a.tenphuongtienttb ASC"
                    ,new{
                        _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<PhuongTienHuyDong>("SELECT * FROM GetPhuongTienHuyDongs(@_mahuyen)"
                ,new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){   
                return connection.Query<PhuongTienHuyDong>("SELECT a.objectid, a.idkhphuogtien, a.tenphuongtienttb, a.dvql, a.dvt, a.soluong, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.namcapnhat, a.ghichu, a.phannhom1, a.phannhom2, a.phannhom3, a.mahuyen FROM PhuongTienHuyDong a WHERE " + SqlQuery + " ORDER BY a.tenphuongtienttb ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<PhuongTienHuyDong>("SELECT * FROM GetPhuongTienHuyDongs(@_mahuyen)"
                ,new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }   
    }
    public PhuongTienHuyDongAddEdit? GetPhuongTienHuyDong(int objectid){
        return connection.QueryFirstOrDefault<PhuongTienHuyDongAddEdit>("SELECT * FROM GetPhuongTienHuyDong(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM PhuongTienHuyDong", commandType: CommandType.Text);
    }
    public int Add(PhuongTienHuyDongAddEdit obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        double? soluong = obj.soluong == null ? null : Convert.ToDouble(obj.soluong);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("AddPhuongTienHuyDong", 
                new{
                    _objectid = obj.objectid,
                    _idkhphuogtien = "PTTB" +  obj.objectid.ToString(),
                    _tenphuongtienttb = obj.tenphuongtienttb,
                    _dvql = obj.dvql,
                    _dvt = obj.dvt,
                    _soluong = soluong,
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _phannhom1 = obj.phannhom1,
                    _phannhom2 = obj.phannhom2,
                    _phannhom3 = obj.phannhom3,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0]),
                    _mahuyen = obj.mahuyen
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;  
        return connection.ExecuteScalar<int>("AddPhuongTienHuyDong", 
            new{
                    _objectid = obj.objectid,
                    _idkhphuogtien = "PTTB" +  obj.objectid.ToString(),
                    _tenphuongtienttb = obj.tenphuongtienttb,
                    _dvql = obj.dvql,
                    _dvt = obj.dvt,
                    _soluong = soluong,
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _phannhom1 = obj.phannhom1,
                    _phannhom2 = obj.phannhom2,
                    _phannhom3 = obj.phannhom3,
                    _day = day,
                    _month = month,
                    _year = year,
                    _mahuyen = obj.mahuyen
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, PhuongTienHuyDongAddEdit obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        double? soluong = obj.soluong == null ? null : Convert.ToDouble(obj.soluong);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("EditPhuongTienHuyDong", 
            new{
                    _objectid = objectid,
                    _tenphuongtienttb = obj.tenphuongtienttb,
                    _dvql = obj.dvql,
                    _dvt = obj.dvt,
                    _soluong = soluong,
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _phannhom1 = obj.phannhom1,
                    _phannhom2 = obj.phannhom2,
                    _phannhom3 = obj.phannhom3,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0]),
                    _mahuyen = obj.mahuyen
            }, commandType: CommandType.StoredProcedure
        );
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;  
        return connection.ExecuteScalar<int>("EditPhuongTienHuyDong", 
            new{
                    _objectid = objectid,
                    _tenphuongtienttb = obj.tenphuongtienttb,
                    _dvql = obj.dvql,
                    _dvt = obj.dvt,
                    _soluong = soluong,
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _phannhom1 = obj.phannhom1,
                    _phannhom2 = obj.phannhom2,
                    _phannhom3 = obj.phannhom3,
                    _day = day,
                    _month = month,
                    _year = year,
                    _mahuyen = obj.mahuyen
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeletePhuongTienHuyDong", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<PhuongTienHuyDongDetailStatistics> GetPhuongTienHuyDongDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<PhuongTienHuyDongDetailStatistics>("SELECT a.dvql AS donviquanly, a.tenphuongtienttb, SUM(a.soluong) AS Soluongphuongtienttb, a.dvt AS Donvitinh FROM PhuongTienHuyDong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY a.dvql, a.tenphuongtienttb, a.dvt ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<PhuongTienHuyDongDetailStatistics>("SELECT a.dvql AS donviquanly, a.tenphuongtienttb, SUM(a.soluong) AS Soluongphuongtienttb, a.dvt AS Donvitinh FROM PhuongTienHuyDong a WHERE a.mahuyen = @_mahuyen GROUP BY a.dvql, a.tenphuongtienttb, a.dvt ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC"
            , new{
                _mahuyen = mahuyen
            });        
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<PhuongTienHuyDongDetailStatistics>("SELECT a.dvql AS donviquanly, a.tenphuongtienttb, SUM(a.soluong) AS Soluongphuongtienttb, a.dvt AS Donvitinh FROM PhuongTienHuyDong a WHERE " + SqlQuery + " GROUP BY a.dvql, a.tenphuongtienttb, a.dvt ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC");            
            }
            return connection.Query<PhuongTienHuyDongDetailStatistics>("SELECT a.dvql AS donviquanly, a.tenphuongtienttb, SUM(a.soluong) AS Soluongphuongtienttb, a.dvt AS Donvitinh FROM PhuongTienHuyDong a GROUP BY a.dvql, a.tenphuongtienttb, a.dvt ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC");  
        }       
    }
    public IEnumerable<PhuongTienHuyDongTotalStatistics> GetPhuongTienHuyDongTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<PhuongTienHuyDongTotalStatistics>("SELECT a.tenphuongtienttb, a.dvt AS Donvitinh, SUM(a.soluong) AS Soluongphuongtienttb, STRING_AGG(CONCAT(a.dvql, ' (', a.soluong, ')'), '; ' ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC ) AS donviquanly FROM PhuongTienHuyDong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY a.tenphuongtienttb, a.dvt ORDER BY a.tenphuongtienttb ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<PhuongTienHuyDongTotalStatistics>("SELECT a.tenphuongtienttb, a.dvt AS Donvitinh, SUM(a.soluong) AS Soluongphuongtienttb, STRING_AGG(CONCAT(a.dvql, ' (', a.soluong, ')'), '; ' ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC ) AS donviquanly FROM PhuongTienHuyDong a WHERE a.mahuyen = @_mahuyen GROUP BY a.tenphuongtienttb, a.dvt ORDER BY a.tenphuongtienttb ASC"
            , new{
                _mahuyen = mahuyen
            });        
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<PhuongTienHuyDongTotalStatistics>("SELECT a.tenphuongtienttb, a.dvt AS Donvitinh, SUM(a.soluong) AS Soluongphuongtienttb, STRING_AGG(CONCAT(a.dvql, ' (', a.soluong, ')'), '; ' ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC ) AS donviquanly FROM PhuongTienHuyDong a WHERE " + SqlQuery + " GROUP BY a.tenphuongtienttb, a.dvt ORDER BY a.tenphuongtienttb ASC");            
            }
            return connection.Query<PhuongTienHuyDongTotalStatistics>("SELECT a.tenphuongtienttb, a.dvt AS Donvitinh, SUM(a.soluong) AS Soluongphuongtienttb, STRING_AGG(CONCAT(a.dvql, ' (', a.soluong, ')'), '; ' ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1 WHEN a.dvql LIKE 'BCH%' THEN 2 WHEN a.dvql LIKE 'Công an%' THEN 3 WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4 WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5 WHEN a.dvql LIKE 'Sở Lao động%' THEN 6 WHEN a.dvql LIKE 'Lực lượng%' THEN 7 WHEN a.dvql LIKE 'Công ty%' THEN 8 WHEN a.dvql LIKE 'Công viên%' THEN 9 WHEN a.dvql LIKE 'Trung tâm%' THEN 10 WHEN a.dvql LIKE 'Cảng%' THEN 11 WHEN a.dvql LIKE 'Thành phố%' THEN 12 WHEN a.dvql = 'Quận 1' THEN 13 WHEN a.dvql = 'Quận 3' THEN 14 WHEN a.dvql = 'Quận 4' THEN 15 WHEN a.dvql = 'Quận 5' THEN 16 WHEN a.dvql = 'Quận 6' THEN 17 WHEN a.dvql = 'Quận 7' THEN 18 WHEN a.dvql = 'Quận 8' THEN 19 WHEN a.dvql = 'Quận 10' THEN 20 WHEN a.dvql = 'Quận 11' THEN 21 WHEN a.dvql = 'Quận 12' THEN 22 WHEN a.dvql = 'Quận Gò Vấp' THEN 23 WHEN a.dvql = 'Quận Phú Nhuận' THEN 24 WHEN a.dvql = 'Quận Tân Bình' THEN 25 WHEN a.dvql = 'Quận Tân Phú' THEN 26 WHEN a.dvql = 'Quận Bình Tân' THEN 27 WHEN a.dvql = 'Quận Bình Thạnh' THEN 28 WHEN a.dvql = 'Huyện Bình Chánh' THEN 29 WHEN a.dvql = 'Huyện Củ Chi' THEN 30 WHEN a.dvql = 'Huyện Hóc Môn' THEN 31 WHEN a.dvql = 'Huyện Nhà Bè' THEN 32 WHEN a.dvql = 'Huyện Cần Giờ' THEN 33 ElSE 34 END ASC ) AS donviquanly FROM PhuongTienHuyDong a GROUP BY a.tenphuongtienttb, a.dvt ORDER BY a.tenphuongtienttb ASC");  
        }       
    }
}