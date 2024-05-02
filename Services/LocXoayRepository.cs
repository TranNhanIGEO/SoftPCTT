using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class LocXoayRepository : BaseRepository{
    public LocXoayRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<LocXoay> GetLocXoays(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<LocXoay>("SELECT a.objectid, a.idlocxoay, a.tenlocxoay, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM LocXoay a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<LocXoay>("SELECT * FROM GetLocXoays(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<LocXoay>("SELECT a.objectid, a.idlocxoay, a.tenlocxoay, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM LocXoay a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<LocXoay>("SELECT * FROM GetLocXoays(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public LocXoay? GetLocXoay(int objectid){
        return connection.QueryFirstOrDefault<LocXoay>("SELECT * FROM GetLocXoay(@_objectid)", new{
            _objectid = objectid
        });
    }
    public IEnumerable<TornadoStatistics> GetTornadoStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TornadoStatistics>("SELECT a.tenhuyen AS quan_huyen_tp, a.tongsoloc FROM ( SELECT h.tenhuyen, COUNT(h.tenhuyen) tongsoloc FROM LocXoay a JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) a UNION ALL SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(*) AS tongsoloc FROM LocXoay a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<TornadoStatistics>("SELECT a.tenhuyen AS quan_huyen_tp, a.tongsoloc FROM ( SELECT h.tenhuyen, COUNT(h.tenhuyen) tongsoloc FROM LocXoay a JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.mahuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) a UNION ALL SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(*) AS tongsoloc FROM LocXoay a WHERE a.mahuyen = @_mahuyen"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TornadoStatistics>("SELECT a.tenhuyen AS quan_huyen_tp, a.tongsoloc FROM ( SELECT h.tenhuyen, COUNT(h.tenhuyen) tongsoloc FROM LocXoay a JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) a UNION ALL SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(*) AS tongsoloc FROM LocXoay a WHERE " + SqlQuery + "");            
            }
            return connection.Query<TornadoStatistics>("SELECT a.tenhuyen AS quan_huyen_tp, a.tongsoloc FROM ( SELECT h.tenhuyen, COUNT(h.tenhuyen) tongsoloc FROM LocXoay a JOIN RgHuyen h ON a.mahuyen = h.mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) a UNION ALL SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(*) AS tongsoloc FROM LocXoay a");  
        }       
    } 
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM LocXoay", commandType: CommandType.Text);
    }
    public int Add(LocXoay obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        double? apsuat = obj.apsuat == null ? null : Convert.ToDouble(obj.apsuat);
        double? tocdogio = obj.tocdogio == null ? null : Convert.ToDouble(obj.tocdogio);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.toadox != null && obj.toadoy != null){
                return connection.ExecuteScalar<int>("AddLocXoay", 
                    new{
                        _objectid = obj.objectid, 
                        _idlocxoay = "LX" +  obj.objectid.ToString(),
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngay,
                        _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                        _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.toadox == null && obj.toadoy == null){
                return connection.ExecuteScalar<int>("AddLocXoay", 
                    new{
                        _objectid = obj.objectid, 
                        _idlocxoay = "LX" +  obj.objectid.ToString(),
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            DateTime? ngaynull = null;
            int? nullDMY = null;  
            if (obj.toadox != null && obj.toadoy != null){
                return connection.ExecuteScalar<int>("AddLocXoay", 
                    new{
                        _objectid = obj.objectid, 
                        _idlocxoay = "LX" +  obj.objectid.ToString(),
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngaynull,
                        _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                        _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.toadox == null && obj.toadoy == null){
                return connection.ExecuteScalar<int>("AddLocXoay", 
                    new{
                        _objectid = obj.objectid, 
                        _idlocxoay = "LX" +  obj.objectid.ToString(),
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Edit(int objectid, LocXoay obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        double? apsuat = obj.apsuat == null ? null : Convert.ToDouble(obj.apsuat);
        double? tocdogio = obj.tocdogio == null ? null : Convert.ToDouble(obj.tocdogio);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.toadox != null && obj.toadoy != null){
                return connection.ExecuteScalar<int>("EditLocXoay", 
                    new{
                        _objectid = objectid,
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngay,
                        _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                        _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.toadox == null && obj.toadoy == null){
                return connection.ExecuteScalar<int>("EditLocXoay", 
                    new{
                        _objectid = objectid,
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            DateTime? ngaynull = null;
            int? nullDMY = null;  
            if (obj.toadox != null && obj.toadoy != null){
                return connection.ExecuteScalar<int>("EditLocXoay", 
                    new{
                        _objectid = objectid,
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngaynull,
                        _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                        _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.toadox == null && obj.toadoy == null){
                return connection.ExecuteScalar<int>("EditLocXoay", 
                    new{
                        _objectid = objectid,
                        _tenlocxoay = obj.tenlocxoay,
                        _gio = obj.gio, 
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri, 
                        _maxa = obj.maxa, 
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        // _ghichu = Convert.ToString(obj.ghichu),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteLocXoay", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}