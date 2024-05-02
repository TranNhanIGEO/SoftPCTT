using System.Data;
using WebApi.Models;
using Dapper;
namespace WebApi.Services;

public class KhuNeoDauRepository : BaseRepository{
    public KhuNeoDauRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<KhuNeoDau> GetKhuNeoDaus(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<KhuNeoDau>("SELECT a.objectid, a.idknd, a.ten, a.diachi, ROUND(a.kinhdodd::Numeric, 6) AS kinhdo, ROUND(a.vidodd::Numeric, 6) AS vido, ROUND(a.kinhdodc::Numeric, 6) AS kinhdodc, ROUND(a.vidodc::Numeric, 6) AS vidodc, a.dosaunuoc, a.succhua, a.coloaitau, a.vitrivl, a.huongluong, a.chieudai::Numeric AS chieudai, a.sdt, a.tansoll,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM KhuNeoDau a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<KhuNeoDau>("SELECT * FROM GetKhuNeoDaus(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<KhuNeoDau>("SELECT a.objectid, a.idknd, a.ten, a.diachi, ROUND(a.kinhdodd::Numeric, 6) AS kinhdo, ROUND(a.vidodd::Numeric, 6) AS vido, ROUND(a.kinhdodc::Numeric, 6) AS kinhdodc, ROUND(a.vidodc::Numeric, 6) AS vidodc, a.dosaunuoc, a.succhua, a.coloaitau, a.vitrivl, a.huongluong, a.chieudai::Numeric AS chieudai, a.sdt, a.tansoll,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM KhuNeoDau a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<KhuNeoDau>("SELECT * FROM GetKhuNeoDaus(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<KhuNeoDauStatistics> GetKhuNeoDauStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<KhuNeoDauStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<KhuNeoDauStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<KhuNeoDauStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + ")");            
            }
            return connection.Query<KhuNeoDauStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen FROM KhuNeoDau a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)");  
        }       
    }
    public KhuNeoDau? GetKhuNeoDau(int objectid){
        return connection.QueryFirstOrDefault<KhuNeoDau>("SELECT * FROM GetKhuNeoDau(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM KhuNeoDau", commandType: CommandType.Text);
    } 
    public int Add(KhuNeoDau obj){  
        double? nulltoado = null;
        string? nullstring = null!;
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdodd = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vidodd = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? dosaunuoc = obj.dosaunuoc == null ? null : Convert.ToDouble(obj.dosaunuoc);
        
        if (obj.kinhdo != null && obj.vido != null){
            return connection.ExecuteScalar<int>("AddKhuNeoDau", 
                new{
                    _objectid = obj.objectid,
                    _idknd = "KND" +  obj.objectid.ToString(),
                    _ten = obj.ten,
                    _diachi = obj.diachi,
                    _kinhdodd = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdodd), 6)),
                    _vidodd = Convert.ToDouble(Math.Round(Convert.ToDecimal(vidodd), 6)),
                    _kinhdodc = nulltoado,
                    _vidodc = nulltoado,
                    _dosaunuoc = dosaunuoc,
                    _succhua = obj.succhua,
                    _coloaitau = obj.coloaitau,
                    _vitrivl = obj.vitrivl,
                    _huongluong = obj.huongluong,
                    _chieudai = chieudai,
                    _sdt = obj.sdt,
                    _tansoll = obj.tansoll,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddKhuNeoDau", 
            new{
                    _objectid = obj.objectid,
                    _idknd = "KND" +  obj.objectid.ToString(),
                    _ten = obj.ten,
                    _diachi = obj.diachi,
                    _kinhdodd = nulltoado,
                    _vidodd = nulltoado,
                    _kinhdodc = nulltoado,
                    _vidodc = nulltoado,
                    _dosaunuoc = dosaunuoc,
                    _succhua = obj.succhua,
                    _coloaitau = obj.coloaitau,
                    _vitrivl = obj.vitrivl,
                    _huongluong = obj.huongluong,
                    _chieudai = chieudai,
                    _sdt = obj.sdt,
                    _tansoll = obj.tansoll,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, KhuNeoDau obj){  
        double? nulltoado = null;
        string? nullstring = null!;
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdodd = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vidodd = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? dosaunuoc = obj.dosaunuoc == null ? null : Convert.ToDouble(obj.dosaunuoc);
        
        if (obj.kinhdo != null && obj.vido != null){
            return connection.ExecuteScalar<int>("EditKhuNeoDau", 
                new{
                    _objectid = objectid,
                    _ten = obj.ten,
                    _diachi = obj.diachi,
                    _kinhdodd = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdodd), 6)),
                    _vidodd = Convert.ToDouble(Math.Round(Convert.ToDecimal(vidodd), 6)),
                    _kinhdodc = nulltoado,
                    _vidodc = nulltoado,
                    _dosaunuoc = dosaunuoc,
                    _succhua = obj.succhua,
                    _coloaitau = obj.coloaitau,
                    _vitrivl = obj.vitrivl,
                    _huongluong = obj.huongluong,
                    _chieudai = chieudai,
                    _sdt = obj.sdt,
                    _tansoll = obj.tansoll,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditKhuNeoDau", 
            new{
                    _objectid = objectid,
                    _ten = obj.ten,
                    _diachi = obj.diachi,
                    _kinhdodd = nulltoado,
                    _vidodd = nulltoado,
                    _kinhdodc = nulltoado,
                    _vidodc = nulltoado,
                    _dosaunuoc = dosaunuoc,
                    _succhua = obj.succhua,
                    _coloaitau = obj.coloaitau,
                    _vitrivl = obj.vitrivl,
                    _huongluong = obj.huongluong,
                    _chieudai = chieudai,
                    _sdt = obj.sdt,
                    _tansoll = obj.tansoll,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteKhuNeoDau", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}