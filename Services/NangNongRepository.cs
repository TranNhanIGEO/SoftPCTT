using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class NangNongRepository : BaseRepository{
    public NangNongRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<NangNong> GetNangNongs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<NangNong>("SELECT a.objectid, a.idtramkt, a.tentram, a.captram, a.vitritram, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.thang, a.sogionang, a.nhietdomin, a.nhietdomax, a.nhietdotb, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM NangNong a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<NangNong>("SELECT * FROM GetNangNongs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<NangNong>("SELECT a.objectid, a.idtramkt, a.tentram, a.captram, a.vitritram, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.thang, a.sogionang, a.nhietdomin, a.nhietdomax, a.nhietdotb, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM NangNong a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC" + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<NangNong>("SELECT * FROM GetNangNongs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public NangNong? GetNangNong(int objectid){
        return connection.QueryFirstOrDefault<NangNong>("SELECT * FROM GetNangNong(@_objectid)", new{
            _objectid = objectid
        });
    }    
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM NangNong", commandType: CommandType.Text);
    }    
    public IEnumerable<TemperatureDetailStatistics> GetTemperatureDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TemperatureDetailStatistics>(
                    "SELECT x.year AS nam, z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.min AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.max AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.nhietdomin) FROM NangNong a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.nhietdomin != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.min = b.nhietdomin GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.nhietdomax) FROM NangNong a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.nhietdomax != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.max = b.nhietdomax GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb FROM NangNong a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.nhietdotb != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<TemperatureDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.min AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.max AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.nhietdomin) FROM NangNong a WHERE a.MaHuyen = @_mahuyen AND a.nhietdomin != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.min = b.nhietdomin GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.nhietdomax) FROM NangNong a WHERE a.MaHuyen = @_mahuyen AND a.nhietdomax != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.max = b.nhietdomax GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb FROM NangNong a WHERE a.MaHuyen = @_mahuyen AND a.nhietdotb != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TemperatureDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.min AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.max AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.nhietdomin) FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdomin != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.min = b.nhietdomin GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.nhietdomax) FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdomax != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.max = b.nhietdomax GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdotb != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC");
            }
            return connection.Query<TemperatureDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.min AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.max AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.nhietdomin) FROM NangNong a WHERE a.nhietdomin != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.min = b.nhietdomin GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.nhietdomax) FROM NangNong a WHERE a.nhietdomax != 0 GROUP BY a.year ) a LEFT JOIN NangNong b ON b.year = a.year AND a.max = b.nhietdomax GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb FROM NangNong a WHERE a.nhietdotb != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC");
        }          
    }
    public IEnumerable<TemperatureTotalStatistics> GetTemperatureTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TemperatureTotalStatistics>(
                    "SELECT z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.nhietdomin AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.nhietdomax AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM (SELECT a.nhietdomin, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.nhietdomin IN (SELECT MIN(a.nhietdomin) from NangNong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.nhietdomin != 0) GROUP BY a.nhietdomin) x, (SELECT a.nhietdomax, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.nhietdomax IN (SELECT MAX(a.nhietdomax) from NangNong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.nhietdomax != 0)GROUP BY a.nhietdomax) y, (SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	FROM NangNong a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.nhietdotb != 0 ) z"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<TemperatureTotalStatistics>(
                "SELECT z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.nhietdomin AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.nhietdomax AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM (SELECT a.nhietdomin, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE a.mahuyen = @_mahuyen AND a.nhietdomin IN (SELECT MIN(a.nhietdomin) from NangNong a WHERE a.mahuyen = @_mahuyen AND a.nhietdomin != 0) GROUP BY a.nhietdomin) x, (SELECT a.nhietdomax, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE a.mahuyen = @_mahuyen AND a.nhietdomax IN (SELECT MAX(a.nhietdomax) from NangNong a WHERE a.mahuyen = @_mahuyen AND a.nhietdomax != 0)GROUP BY a.nhietdomax) y, (SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	FROM NangNong a WHERE a.mahuyen = @_mahuyen AND a.nhietdotb != 0 ) z"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TemperatureTotalStatistics>(
                "SELECT z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.nhietdomin AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.nhietdomax AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM (SELECT a.nhietdomin, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdomin IN (SELECT MIN(a.nhietdomin) from NangNong a WHERE " + SqlQuery + " AND a.nhietdomin != 0) GROUP BY a.nhietdomin) x, (SELECT a.nhietdomax, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdomax IN (SELECT MAX(a.nhietdomax) from NangNong a WHERE " + SqlQuery + " AND a.nhietdomax != 0)GROUP BY a.nhietdomax) y, (SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	FROM NangNong a WHERE " + SqlQuery + " AND a.nhietdotb != 0 ) z");
            }
            return connection.Query<TemperatureTotalStatistics>(
                "SELECT z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.nhietdomin AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.nhietdomax AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh FROM (SELECT a.nhietdomin, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE a.nhietdomin IN (SELECT MIN(a.nhietdomin) from NangNong a WHERE a.nhietdomin != 0) GROUP BY a.nhietdomin) x, (SELECT a.nhietdomax, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM NangNong a WHERE a.nhietdomax IN (SELECT MAX(a.nhietdomax) from NangNong a WHERE a.nhietdomax != 0)GROUP BY a.nhietdomax) y, (SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	FROM NangNong a WHERE a.nhietdotb != 0 ) z");
        }          
    }
    public int Add(NangNong obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        short? thang = obj.thang == null ? null : Convert.ToInt16(obj.thang);
        double? sogionang = obj.sogionang == null ? null : Convert.ToDouble(obj.sogionang);
        double? nhietdomin = obj.nhietdomin == null ? null : Convert.ToDouble(obj.nhietdomin);
        double? nhietdomax = obj.nhietdomax == null ? null : Convert.ToDouble(obj.nhietdomax);
        double? nhietdotb = obj.nhietdotb == null ? null : Convert.ToDouble(obj.nhietdotb);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddNangNong", 
                    new{
                        _objectid = obj.objectid,
                        _idtramkt = "NN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _ngay = ngay,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddNangNong", 
                    new{
                        _objectid = obj.objectid,
                        _idtramkt = "NN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _ngay = ngay,
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
                return connection.ExecuteScalar<int>("AddNangNong", 
                    new{
                        _objectid = obj.objectid,
                        _idtramkt = "NN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _ngay = ngaynull,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddNangNong", 
                    new{
                        _objectid = obj.objectid,
                        _idtramkt = "NN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _ngay = ngaynull,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Edit(int objectid, NangNong obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        short? thang = obj.thang == null ? null : Convert.ToInt16(obj.thang);
        double? sogionang = obj.sogionang == null ? null : Convert.ToDouble(obj.sogionang);
        double? nhietdomin = obj.nhietdomin == null ? null : Convert.ToDouble(obj.nhietdomin);
        double? nhietdomax = obj.nhietdomax == null ? null : Convert.ToDouble(obj.nhietdomax);
        double? nhietdotb = obj.nhietdotb == null ? null : Convert.ToDouble(obj.nhietdotb);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditNangNong", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _ngay = ngay,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditNangNong", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _ngay = ngay,
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
                return connection.ExecuteScalar<int>("EditNangNong", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _ngay = ngaynull,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditNangNong", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _captram = obj.captram,
                        _vitritram = obj.vitritram,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _thang = thang,
                        _sogionang = sogionang,
                        _nhietdomin = nhietdomin,
                        _nhietdomax =nhietdomax,
                        _nhietdotb = nhietdotb,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _ngay = ngaynull,
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
        return connection.ExecuteScalar<int>("DeleteNangNong", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}