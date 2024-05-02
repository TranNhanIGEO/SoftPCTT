using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class ApThapNhietDoiRepository : BaseRepository{
    public ApThapNhietDoiRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<ApThapNhietDoi> GetApThapNhietDois(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                    return connection.Query<SearchApThapNhietDoi>(
                    "SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape, ST_AsGeoJson(ST_Transform(x.line, 4326)) AS line FROM ApThapNhietDoi a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen LEFT JOIN (SELECT a.idapthap, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line FROM ApThapNhietDoi a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY a.year, a.idapthap) x ON x.idapthap = a.idapthap AND x.year = a.year WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC, a.gio ASC"
                    ,new{
                        _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ApThapNhietDoi>("SELECT * FROM GetApThapNhietDois(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SearchApThapNhietDoi>(
                "SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape, ST_AsGeoJson(ST_Transform(x.line, 4326)) AS line FROM ApThapNhietDoi a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen LEFT JOIN (SELECT a.idapthap, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line FROM ApThapNhietDoi a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " GROUP BY a.year, a.idapthap) x ON x.idapthap = a.idapthap AND x.year = a.year WHERE " + SqlQuery + " ORDER BY a.ngay ASC, a.gio ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ApThapNhietDoi>("SELECT * FROM GetApThapNhietDois(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<TropicalDepressionStatistics> GetTropicalDepressionStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TropicalDepressionStatistics>("SELECT a.nam::Text, a.tongsoATND, a.tenATND  FROM (SELECT a.year AS nam, COUNT(a.year) AS tongsoATND, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM (SELECT a.tenapthap, a.year FROM ApThapNhietDoi a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen GROUP BY a.tenapthap, a.year ORDER BY a.year ASC, a.tenapthap ASC) a GROUP BY a.year) a"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<TropicalDepressionStatistics>("SELECT a.nam::Text, a.tongsoATND, a.tenATND  FROM (SELECT a.year AS nam, COUNT(a.year) AS tongsoATND, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM (SELECT a.tenapthap, a.year FROM ApThapNhietDoi a WHERE a.mahuyen = @_mahuyen GROUP BY a.tenapthap, a.year ORDER BY a.year ASC, a.tenapthap ASC) a GROUP BY a.year) a"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TropicalDepressionStatistics>("SELECT a.nam::Text, a.tongsoATND, a.tenATND  FROM (SELECT a.year AS nam, COUNT(a.year) AS tongsoATND, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM (SELECT a.tenapthap, a.year FROM ApThapNhietDoi a WHERE " + SqlQuery + " GROUP BY a.tenapthap, a.year ORDER BY a.year ASC, a.tenapthap ASC) a GROUP BY a.year) a UNION ALL SELECT 'Tổng cộng' AS nam, COUNT(*) AS tongsoatnd, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM ( SELECT a.tenapthap, a.year FROM ApThapNhietDoi a WHERE " + SqlQuery + "  GROUP BY a.tenapthap, a.year ) a");            
            }
            return connection.Query<TropicalDepressionStatistics>("SELECT a.nam::Text, a.tongsoATND, a.tenATND  FROM (SELECT a.year AS nam, COUNT(a.year) AS tongsoATND, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM (SELECT a.tenapthap, a.year FROM ApThapNhietDoi a GROUP BY a.tenapthap, a.year ORDER BY a.year ASC, a.tenapthap ASC) a GROUP BY a.year) a UNION ALL SELECT 'Tổng cộng' AS nam, COUNT(*) AS tongsoatnd, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM ( SELECT a.tenapthap, a.year FROM ApThapNhietDoi a GROUP BY a.tenapthap, a.year ) a");  
        }       
    }   
    public ApThapNhietDoiDetail? GetApThapNhietDoi(int objectid){
        return connection.QueryFirstOrDefault<ApThapNhietDoiDetail>("SELECT * FROM GetApThapNhietDoi(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM ApThapNhietDoi", commandType: CommandType.Text);
    }    
    public int Add(ApThapNhietDoi obj){ 
        DateTime? ngay = Convert.ToDateTime(obj.ngay);
        DateTime? ngaybatdau = obj.ngaybatdau == null ? null : Convert.ToDateTime(obj.ngaybatdau);
        DateTime? ngayketthuc = obj.ngayketthuc == null ? null : Convert.ToDateTime(obj.ngayketthuc);
        double? apsuat = obj.apsuat == null ? null : Convert.ToDouble(obj.apsuat);
        double? tocdogio = obj.tocdogio == null ? null : Convert.ToDouble(obj.tocdogio);
        double? nulltoado = null;
        string? nullstring = null!;
        double? gio = obj.gio == null ? null : Convert.ToDouble(obj.gio);
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddApThapNhietDoi", 
                    new{
                        _objectid = obj.objectid,
                        _idapthap = obj.tenapthap + "-" + Convert.ToInt32(date[0]),
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0]),
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddApThapNhietDoi", 
                    new{
                        _objectid = obj.objectid,
                        _idapthap = obj.tenapthap + "-" + Convert.ToInt32(date[0]),
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0]),
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddApThapNhietDoi", 
                    new{
                        _objectid = obj.objectid,
                        _idapthap = obj.tenapthap + "-",
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddApThapNhietDoi", 
                    new{
                        _objectid = obj.objectid,
                        _idapthap = obj.tenapthap + "-",
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Edit(int objectid, ApThapNhietDoi obj){ 
        DateTime? ngay = Convert.ToDateTime(obj.ngay);
        DateTime? ngaybatdau = obj.ngaybatdau == null ? null : Convert.ToDateTime(obj.ngaybatdau);
        DateTime? ngayketthuc = obj.ngayketthuc == null ? null : Convert.ToDateTime(obj.ngayketthuc);
        double? apsuat = obj.apsuat == null ? null : Convert.ToDouble(obj.apsuat);
        double? tocdogio = obj.tocdogio == null ? null : Convert.ToDouble(obj.tocdogio);
        double? nulltoado = null;
        string? nullstring = null!;
        double? gio = obj.gio == null ? null : Convert.ToDouble(obj.gio);
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditApThapNhietDoi", 
                    new{
                        _objectid = objectid,
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0]),
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditApThapNhietDoi", 
                    new{
                        _objectid = objectid,
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0]),
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditApThapNhietDoi", 
                    new{
                        _objectid = objectid,
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditApThapNhietDoi", 
                    new{
                        _objectid = objectid,
                        _tenapthap = obj.tenapthap,
                        _gio = gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _apsuat = apsuat,
                        _tocdogio = tocdogio,
                        _vitri = obj.vitri,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteApThapNhietDoi", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}




