using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class MuaRepository : BaseRepository{
    public MuaRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<Mua> GetMuas(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<Mua>("SELECT a.objectid, a.idtrammua, a.tentram, a.captram, a.vitritram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.luongmua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM Mua a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE (" + SqlQuery + ") AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Mua>("SELECT * FROM GetMuas(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<Mua>("SELECT a.objectid, a.idtrammua, a.tentram, a.captram, a.vitritram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.luongmua, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM Mua a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Mua>("SELECT * FROM GetMuas(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<RainTotalStatistics> GetRainTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<RainTotalStatistics>(
                    "SELECT z.tentram AS Tentramdomua, z.Tongsophantu, x.luongmua AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.luongmua AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM(SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua IN(SELECT MIN(a.luongmua) from (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) x, (SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua IN(SELECT MAX(a.luongmua) from (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0) a) z"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<RainTotalStatistics>(
                "SELECT z.tentram AS Tentramdomua, z.Tongsophantu, x.luongmua AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.luongmua AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM(SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua IN(SELECT MIN(a.luongmua) from (SELECT * FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) x, (SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua IN(SELECT MAX(a.luongmua) from (SELECT * FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM (SELECT * FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0) a) z"
                , new{
                    _mahuyen = mahuyen
            });          
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<RainTotalStatistics>(
                "SELECT z.tentram AS Tentramdomua, z.Tongsophantu, x.luongmua AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.luongmua AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM(SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE " + SqlQuery + " AND a.luongmua IN(SELECT MIN(a.luongmua) from (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) x, (SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE " + SqlQuery + " AND a.luongmua IN(SELECT MAX(a.luongmua) from (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM (SELECT * FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0) a) z");               
            }
            return connection.Query<RainTotalStatistics>(
            "SELECT z.tentram AS Tentramdomua, z.Tongsophantu, x.luongmua AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.luongmua AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM(SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE a.luongmua IN(SELECT MIN(a.luongmua) from (SELECT * FROM Mua a WHERE a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) x, (SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a WHERE a.luongmua IN(SELECT MAX(a.luongmua) from (SELECT * FROM Mua a WHERE a.luongmua != 0) a) GROUP BY a.ngay, a.luongmua ORDER BY a.ngay ASC) a GROUP BY a.luongmua) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM (SELECT * FROM Mua a WHERE a.luongmua != 0) a) z"); 
        }     
    }
    public IEnumerable<RainDetailStatistics> GetRainDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<RainDetailStatistics>(
                    "SELECT x.year AS nam, z.tentram AS Tentramdomua, z.Tongsophantu, x.min AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.max AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MIN(a.luongmua) FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.min GROUP BY a.year, a.min ORDER BY a.year ASC ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MAX(a.luongmua) FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.max GROUP BY a.year, a.max ORDER BY a.year ASC ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM Mua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<RainDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdomua, z.Tongsophantu, x.min AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.max AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MIN(a.luongmua) FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.min GROUP BY a.year, a.min ORDER BY a.year ASC ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MAX(a.luongmua) FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.max GROUP BY a.year, a.max ORDER BY a.year ASC ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM Mua a WHERE a.mahuyen = @_mahuyen AND a.luongmua != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year"
                , new{
                    _mahuyen = mahuyen
            });          
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<RainDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdomua, z.Tongsophantu, x.min AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.max AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MIN(a.luongmua) FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.min GROUP BY a.year, a.min ORDER BY a.year ASC ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MAX(a.luongmua) FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.max GROUP BY a.year, a.max ORDER BY a.year ASC ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM Mua a WHERE " + SqlQuery + " AND a.luongmua != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year");               
            }
            return connection.Query<RainDetailStatistics>(
            "SELECT x.year AS nam, z.tentram AS Tentramdomua, z.Tongsophantu, x.min AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.max AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MIN(a.luongmua) FROM Mua a WHERE a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.min GROUP BY a.year, a.min ORDER BY a.year ASC ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay FROM ( SELECT a.year, MAX(a.luongmua) FROM Mua a WHERE a.luongmua != 0 GROUP BY a.year ) a LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.max GROUP BY a.year, a.max ORDER BY a.year ASC ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb FROM Mua a WHERE a.luongmua != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year"); 
        }     
    }
    public Mua? GetMua(int objectid){
        return connection.QueryFirstOrDefault<Mua>("SELECT * FROM GetMua(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM Mua", commandType: CommandType.Text);
    }
    public int Add(Mua obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? luongmua = obj.luongmua == null ? null : Convert.ToDouble(obj.luongmua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddMua", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammua = "MUA" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddMua", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammua = "MUA" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddMua", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammua = "MUA" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddMua", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammua = "MUA" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Edit(int objectid, Mua obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? luongmua = obj.luongmua == null ? null : Convert.ToDouble(obj.luongmua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditMua", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditMua", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditMua", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditMua", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _luongmua = luongmua,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteMua", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}