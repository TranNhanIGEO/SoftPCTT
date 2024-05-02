using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class BienCanhBaoSatLoRepository : BaseRepository{
    public BienCanhBaoSatLoRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<BienCanhBaoSatLo> GetBienCanhBaoSatLos(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<BienCanhBaoSatLo>("SELECT a.objectid, a.idbcbsl, a.sohieubien, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitrisatlo, a.phamvi,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namxaydung, a.hinhanh, a.namcapnhat, a.ghichu, a.tuyensr, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.namxaydung ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<BienCanhBaoSatLo>("SELECT * FROM GetBienCanhBaoSatLos(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<BienCanhBaoSatLo>("SELECT a.objectid, a.idbcbsl, a.sohieubien, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.vitrisatlo, a.phamvi,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namxaydung, a.hinhanh, a.namcapnhat, a.ghichu, a.tuyensr, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.namxaydung ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<BienCanhBaoSatLo>("SELECT * FROM GetBienCanhBaoSatLos(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<BienCanhBaoStatistics> GetBienCanhBaoStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<BienCanhBaoStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<BienCanhBaoStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE a.MaHuyen = @_mahuyen)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<BienCanhBaoStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + " GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen WHERE " + SqlQuery + ")");            
            }
            return connection.Query<BienCanhBaoStatistics>("(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen GROUP BY h.tenhuyen ORDER BY h.tenhuyen ASC) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr FROM BienCanhBaoSatLo a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)");  
        }       
    }
    public BienCanhBaoSatLo? GetBienCanhBaoSatLo(int objectid){
        return connection.QueryFirstOrDefault<BienCanhBaoSatLo>("SELECT * FROM GetBienCanhBaoSatLo(@_objectid)", new{
            _objectid = objectid
        });
    }
    public string? GetOldName(int objectid){
        return connection.QueryFirstOrDefault<string>("SELECT hinhanh FROM BienCanhBaoSatLo WHERE objectid = @_objectid", new{_objectid = objectid});
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM BienCanhBaoSatLo", commandType: CommandType.Text);
    } 
    public int Add(int objectid, BienCanhBaoSatLoAddEdit obj, string? hinhanh){  
        double? nulltoado = null;
        string? nullstring = null!;
        short? namxaydung = obj.namxaydung == "null" ? null : Convert.ToInt16(obj.namxaydung);
        short? namcapnhat = obj.namcapnhat == "null" ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == "null" ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == "null" ? null : Convert.ToDouble(obj.toadoy);
        
        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("AddBienCanhBaoSatLo", 
                new{
                    _objectid = objectid,
                    _idbcbsl = "BBSL" +  objectid.ToString(),
                    _sohieubien = obj.sohieubien,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _vitrisatlo = obj.vitrisatlo == "null" ? null : obj.vitrisatlo,
                    _phamvi = obj.phamvi == "null" ? null : obj.phamvi,
                    _maxa = obj.maxa == "null" ? null : obj.maxa,
                    _mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen,
                    _namxaydung = namxaydung,
                    _hinhanh = hinhanh,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _tuyensr = obj.tuyensr == "null" ? null : obj.tuyensr
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddBienCanhBaoSatLo", 
            new{
                    _objectid = objectid,
                    _idbcbsl = "BBSL" +  objectid.ToString(),
                    _sohieubien = obj.sohieubien,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _vitrisatlo = obj.vitrisatlo == "null" ? null : obj.vitrisatlo,
                    _phamvi = obj.phamvi == "null" ? null : obj.phamvi,
                    _maxa = obj.maxa == "null" ? null : obj.maxa,
                    _mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen,
                    _namxaydung = namxaydung,
                    _hinhanh = hinhanh,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _tuyensr = obj.tuyensr == "null" ? null : obj.tuyensr
            }, commandType: CommandType.StoredProcedure
        );
    }
   public int Edit(int objectid, BienCanhBaoSatLoAddEdit obj, string? hinhanh){  
        double? nulltoado = null;
        string? nullstring = null!;
        short? namxaydung = obj.namxaydung == "null" ? null : Convert.ToInt16(obj.namxaydung);
        short? namcapnhat = obj.namcapnhat == "null" ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == "null" ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == "null" ? null : Convert.ToDouble(obj.toadoy);
        
        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("EditBienCanhBaoSatLo", 
                new{
                    _objectid = objectid,
                    _sohieubien = obj.sohieubien,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _vitrisatlo = obj.vitrisatlo == "null" ? null : obj.vitrisatlo,
                    _phamvi = obj.phamvi == "null" ? null : obj.phamvi,
                    _maxa = obj.maxa == "null" ? null : obj.maxa,
                    _mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen,
                    _namxaydung = namxaydung,
                    _hinhanh = hinhanh,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _tuyensr = obj.tuyensr == "null" ? null : obj.tuyensr
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditBienCanhBaoSatLo", 
            new{
                    _objectid = objectid,
                    _sohieubien = obj.sohieubien,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _vitrisatlo = obj.vitrisatlo == "null" ? null : obj.vitrisatlo,
                    _phamvi = obj.phamvi == "null" ? null : obj.phamvi,
                    _maxa = obj.maxa == "null" ? null : obj.maxa,
                    _mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen,
                    _namxaydung = namxaydung,
                    _hinhanh = hinhanh,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _tuyensr = obj.tuyensr == "null" ? null : obj.tuyensr
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteBienCanhBaoSatLo", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}