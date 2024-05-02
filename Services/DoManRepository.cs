using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class DoManRepository : BaseRepository{
    public DoManRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<DoMan> GetDoMans(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DoMan>("SELECT a.objectid, a.idtramman,  a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tensong, a.doman, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM DoMan a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DoMan>("SELECT * FROM GetDoMans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DoMan>("SELECT a.objectid, a.idtramman, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tensong, a.doman, a.vitri,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM DoMan a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC" + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DoMan>("SELECT * FROM GetDoMans(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<SalinityDetailStatistics> GetSalinityDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SalinityDetailStatistics>(
                    "SELECT x.year AS nam, z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM ( SELECT a.year, a.min AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM  ( SELECT a.year, MIN(a.doman) FROM DoMan a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.min = b.doman GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM ( SELECT a.year, MAX(a.doman) FROM DoMan a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.max = b.doman GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM DoMan a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"               
                    , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<SalinityDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM ( SELECT a.year, a.min AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM  ( SELECT a.year, MIN(a.doman) FROM DoMan a WHERE a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.min = b.doman GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM ( SELECT a.year, MAX(a.doman) FROM DoMan a WHERE a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.max = b.doman GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM DoMan a WHERE a.MaHuyen = @_mahuyen AND a.doman != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"                
                , new{
                _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SalinityDetailStatistics>(
                    "SELECT x.year AS nam, z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM ( SELECT a.year, a.min AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM  ( SELECT a.year, MIN(a.doman) FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.min = b.doman GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM ( SELECT a.year, MAX(a.doman) FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.max = b.doman GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC");                
            }
            return connection.Query<SalinityDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM ( SELECT a.year, a.min AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM  ( SELECT a.year, MIN(a.doman) FROM DoMan a WHERE a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.min = b.doman GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  FROM ( SELECT a.year, MAX(a.doman) FROM DoMan a WHERE a.doman != 0 GROUP BY a.year ) a LEFT JOIN DoMan b ON b.year = a.year AND a.max = b.doman GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM DoMan a WHERE a.doman != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC");
        }
    }   
    public IEnumerable<SalinityTotalStatistics> GetSalinityTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SalinityTotalStatistics>(
                    "SELECT z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.doman IN(SELECT MIN(a.doman) from (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC ) a GROUP BY a.doman) x, (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.doman IN(SELECT MAX(a.doman) from (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC) a GROUP BY a.doman) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.doman != 0) a) z"               
                    , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<SalinityTotalStatistics>(
                "SELECT z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE a.mahuyen = @_mahuyen AND a.doman IN(SELECT MIN(a.doman) from (SELECT * FROM DoMan a WHERE a.mahuyen = @_mahuyen AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC ) a GROUP BY a.doman) x, (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE a.mahuyen = @_mahuyen AND a.doman IN(SELECT MAX(a.doman) from (SELECT * FROM DoMan a WHERE a.mahuyen = @_mahuyen AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC) a GROUP BY a.doman) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM (SELECT * FROM DoMan a WHERE a.mahuyen = @_mahuyen AND a.doman != 0) a) z"                
                , new{
                _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SalinityTotalStatistics>(
                    "SELECT z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE " + SqlQuery + " AND a.doman IN(SELECT MIN(a.doman) from (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC ) a GROUP BY a.doman) x, (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE " + SqlQuery + " AND a.doman IN(SELECT MAX(a.doman) from (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC) a GROUP BY a.doman) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM (SELECT * FROM DoMan a WHERE " + SqlQuery + " AND a.doman != 0) a) z");                
            }
            return connection.Query<SalinityTotalStatistics>(
                "SELECT z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh FROM (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE a.doman IN(SELECT MIN(a.doman) from (SELECT * FROM DoMan a WHERE a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC ) a GROUP BY a.doman) x, (SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a WHERE a.doman IN(SELECT MAX(a.doman) from (SELECT * FROM DoMan a WHERE a.doman != 0) a) GROUP BY a.ngay, a.doman ORDER BY a.ngay ASC) a GROUP BY a.doman) y, (SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb FROM (SELECT * FROM DoMan a WHERE a.doman != 0) a) z");
        }
    } 
    public DoMan? GetDoMan(int objectid){
        return connection.QueryFirstOrDefault<DoMan>("SELECT * FROM GetDoMan(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM DoMan", commandType: CommandType.Text);
    }
    public int Add(DoMan obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? doman = obj.doman == null ? null : Convert.ToDouble(obj.doman);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddDoMan", 
                    new{
                        _objectid = obj.objectid,
                        _idtramman = "DM" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("AddDoMan", 
                    new{
                        _objectid = obj.objectid,
                        _idtramman = "DM" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("AddDoMan", 
                    new{
                        _objectid = obj.objectid,
                        _idtramman = "DM" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("AddDoMan", 
                    new{
                        _objectid = obj.objectid,
                        _idtramman = "DM" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
    public int Edit(int objectid, DoMan obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? doman = obj.doman == null ? null : Convert.ToDouble(obj.doman);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditDoMan", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("EditDoMan", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("EditDoMan", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
                return connection.ExecuteScalar<int>("EditDoMan", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _tensong = obj.tensong,
                        _doman = doman,
                        _vitri = obj.vitri,
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
        return connection.ExecuteScalar<int>("DeleteDoMan", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}


