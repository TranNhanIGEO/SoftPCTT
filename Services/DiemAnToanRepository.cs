using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class DiemAnToanRepository : BaseRepository{
    public DiemAnToanRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<DiemAnToan> GetDiemAnToans(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DiemAnToan>("SELECT a.objectid, a.idantoan, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.succhua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM DiemAnToan a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE (" + SqlQuery + ") AND a.MaHuyen = @_mahuyen", new{
                    _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DiemAnToan>("SELECT * FROM GetDiemAnToans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DiemAnToan>("SELECT a.objectid, a.idantoan, a.vitri, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.succhua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.phuongan, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM DiemAnToan a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DiemAnToan>("SELECT * FROM GetDiemAnToans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM DiemAnToan", commandType: CommandType.Text);
    }

    public DiemAnToan? GetDiemAnToan(int objectid){
        return connection.QueryFirstOrDefault<DiemAnToan>("SELECT * FROM GetDiemAnToan(@_objectid)", new{
            _objectid = objectid
        }, commandType: CommandType.Text);
    }

    public int Add(DiemAnToan obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        int? succhua = obj.succhua == null ? null : Convert.ToInt32(obj.succhua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);

        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("AddDiemAnToan", 
                new{
                    _objectid = obj.objectid,
                    _idantoan = "HDC" +  obj.objectid.ToString(),
                    _vitri = obj.vitri,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _succhua = succhua,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat  = namcapnhat,
                    _ghichu = nullstring,
                    _phuongan = obj.phuongan
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddDiemAnToan", 
            new{
                    _objectid = obj.objectid,
                    _idantoan = "HDC" +  obj.objectid.ToString(),
                    _vitri = obj.vitri,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _succhua = succhua,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat  = namcapnhat,
                    _ghichu = nullstring,
                    _phuongan = obj.phuongan
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, DiemAnToan obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        int? succhua = obj.succhua == null ? null : Convert.ToInt32(obj.succhua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);

        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("EditDiemAnToan", 
                new{
                    _objectid = objectid,
                    _vitri = obj.vitri,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _succhua = succhua,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat  = namcapnhat,
                    _ghichu = nullstring,
                    _phuongan = obj.phuongan
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditDiemAnToan", 
            new{
                    _objectid = objectid,
                    _vitri = obj.vitri,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _succhua = succhua,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat  = namcapnhat,
                    _ghichu = nullstring,
                    _phuongan = obj.phuongan
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteDiemAnToan",
         new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<DiemAnToanStatistics> GetDiemAnToanStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<DiemAnToanStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<DiemAnToanStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<DiemAnToanStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + ")");            
            }
            return connection.Query<DiemAnToanStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC ) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan FROM DiemAnToan a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)");  
        }       
    }
}