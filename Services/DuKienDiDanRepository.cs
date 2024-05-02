using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class DuKienDiDanRepository : BaseRepository{
    public DuKienDiDanRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<DuKienDiDan> GetDuKienDiDans(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DuKienDiDan>("SELECT a.objectid, a.idkhsotan, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.quanhuyen, a.mahuyen, a.sophuongdidan, a.soho_bao8_9, a.songuoi_bao8_9, a.soho_bao10_13, a.songuoi_bao10_13, a.namcapnhat, a.ghichu, a.sohocandidoi FROM DuKienDiDan a WHERE (" + SqlQuery + ") AND a.MaHuyen = @_mahuyen ORDER BY a.quanhuyen ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DuKienDiDan>("SELECT * FROM GetDuKienDiDans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DuKienDiDan>("SELECT a.objectid, a.idkhsotan, a.sovb, TO_CHAR(a.ngayvb, 'dd/mm/yyyy')::Text AS ngayvb, a.loaivb, a.quanhuyen, a.mahuyen, a.sophuongdidan, a.soho_bao8_9, a.songuoi_bao8_9, a.soho_bao10_13, a.songuoi_bao10_13, a.namcapnhat, a.ghichu, a.sohocandidoi FROM DuKienDiDan a WHERE " + SqlQuery + " ORDER BY a.quanhuyen ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DuKienDiDan>("SELECT * FROM GetDuKienDiDans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public DuKienDiDan? GetDuKienDiDan(int objectid){
        return connection.QueryFirstOrDefault<DuKienDiDan>("SELECT * FROM GetDuKienDiDan(@_objectid)", new{
            _objectid = objectid
        });
    }    
   
    public IEnumerable<DuKienDiDanStatistics> GetDuKienDiDanStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<DuKienDiDanStatistics>("(SELECT a.QuanHuyen AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY a.QuanHuyen ORDER BY a.QuanHuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<DuKienDiDanStatistics>("(SELECT a.QuanHuyen AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE a.MaHuyen = @_mahuyen GROUP BY a.QuanHuyen ORDER BY a.QuanHuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE  a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<DuKienDiDanStatistics>("(SELECT a.QuanHuyen AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE " + SqlQuery + " GROUP BY a.QuanHuyen ORDER BY a.QuanHuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a WHERE " + SqlQuery + " )");            
            }
            return connection.Query<DuKienDiDanStatistics>("(SELECT a.QuanHuyen AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a GROUP BY a.QuanHuyen ORDER BY a.QuanHuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 FROM DuKienDiDan a)");  
        }       
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM DuKienDiDan", commandType: CommandType.Text);
    }
    public int Add(DuKienDiDan obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        int? sophuongdidan = obj.sophuongdidan == null ? null : Convert.ToInt32(obj.sophuongdidan);
        int? soho_bao8_9 = obj.soho_bao8_9 == null ? null : Convert.ToInt32(obj.soho_bao8_9);
        int? songuoi_bao8_9 = obj.songuoi_bao8_9 == null ? null : Convert.ToInt32(obj.songuoi_bao8_9);
        int? soho_bao10_13 = obj.soho_bao10_13 == null ? null : Convert.ToInt32(obj.soho_bao10_13);
        int? songuoi_bao10_13 = obj.songuoi_bao10_13 == null ? null : Convert.ToInt32(obj.songuoi_bao10_13);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        int? sohocandidoi = obj.sohocandidoi == null ? null : Convert.ToInt32(obj.sohocandidoi);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("AddDuKienDiDan", 
                new{
                    _objectid = obj.objectid, 
                    _idkhsotan = "DD" +  obj.objectid.ToString(),
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _quanhuyen = obj.quanhuyen,
                    _mahuyen = obj.mahuyen,
                    _sophuongdidan = sophuongdidan,
                    _soho_bao8_9 = soho_bao8_9,
                    _songuoi_bao8_9 = songuoi_bao8_9,
                    _soho_bao10_13 = soho_bao10_13,
                    _songuoi_bao10_13 = songuoi_bao10_13,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _sohocandidoi = sohocandidoi,
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
        return connection.ExecuteScalar<int>("AddDuKienDiDan", 
            new{
                    _objectid = obj.objectid, 
                    _idkhsotan = "DD" +  obj.objectid.ToString(),
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _quanhuyen = obj.quanhuyen,
                    _mahuyen = obj.mahuyen,
                    _sophuongdidan = sophuongdidan,
                    _soho_bao8_9 = soho_bao8_9,
                    _songuoi_bao8_9 = songuoi_bao8_9,
                    _soho_bao10_13 = soho_bao10_13,
                    _songuoi_bao10_13 = songuoi_bao10_13,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _sohocandidoi = sohocandidoi,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }    
    public int Edit(int objectid, DuKienDiDan obj){ 
        DateTime ngayvb = Convert.ToDateTime(obj.ngayvb);
        int? sophuongdidan = obj.sophuongdidan == null ? null : Convert.ToInt32(obj.sophuongdidan);
        int? soho_bao8_9 = obj.soho_bao8_9 == null ? null : Convert.ToInt32(obj.soho_bao8_9);
        int? songuoi_bao8_9 = obj.songuoi_bao8_9 == null ? null : Convert.ToInt32(obj.songuoi_bao8_9);
        int? soho_bao10_13 = obj.soho_bao10_13 == null ? null : Convert.ToInt32(obj.soho_bao10_13);
        int? songuoi_bao10_13 = obj.songuoi_bao10_13 == null ? null : Convert.ToInt32(obj.songuoi_bao10_13);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        int? sohocandidoi = obj.sohocandidoi == null ? null : Convert.ToInt32(obj.sohocandidoi);

        if (obj.ngayvb != null){
            string[] date = obj.ngayvb.ToString().Split('-');
            return connection.ExecuteScalar<int>("EditDuKienDiDan", 
            new{
                    _objectid = objectid, 
                    _sovb = obj.sovb,
                    _ngayvb = ngayvb,
                    _loaivb = obj.loaivb,
                    _quanhuyen = obj.quanhuyen,
                    _mahuyen = obj.mahuyen,
                    _sophuongdidan = sophuongdidan,
                    _soho_bao8_9 = soho_bao8_9,
                    _songuoi_bao8_9 = songuoi_bao8_9,
                    _soho_bao10_13 = soho_bao10_13,
                    _songuoi_bao10_13 = songuoi_bao10_13,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _sohocandidoi = sohocandidoi,
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

        return connection.ExecuteScalar<int>("EditDuKienDiDan", 
            new{
                    _objectid = objectid, 
                    _sovb = obj.sovb,
                    _ngayvb = ngaynull,
                    _loaivb = obj.loaivb,
                    _quanhuyen = obj.quanhuyen,
                    _mahuyen = obj.mahuyen,
                    _sophuongdidan = sophuongdidan,
                    _soho_bao8_9 = soho_bao8_9,
                    _songuoi_bao8_9 = songuoi_bao8_9,
                    _soho_bao10_13 = soho_bao10_13,
                    _songuoi_bao10_13 = songuoi_bao10_13,
                    _namcapnhat = namcapnhat,
                    // _ghichu = Convert.ToString(obj.ghichu),
                    _sohocandidoi = sohocandidoi,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteDuKienDiDan", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}