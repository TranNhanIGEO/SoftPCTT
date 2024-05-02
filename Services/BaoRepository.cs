using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class BaoRepository : BaseRepository{
    public BaoRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<Bao> GetBaos(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                    return connection.Query<SearchBao>("SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape , ST_AsGeoJson(ST_Transform(x.line, 4326)) AS line FROM Bao a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen LEFT JOIN (SELECT a.idbao, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line FROM Bao a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY a.year, a.idbao) x ON x.idbao = a.idbao AND x.year = a.year WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC, a.gio ASC"
                    ,new{
                        _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Bao>("SELECT * FROM GetBaos(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SearchBao>("SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape , ST_AsGeoJson(ST_Transform(x.line, 4326)) AS line FROM Bao a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen LEFT JOIN (SELECT a.idbao, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line FROM Bao a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " GROUP BY a.year, a.idbao) x ON x.idbao = a.idbao AND x.year = a.year WHERE " + SqlQuery + " ORDER BY a.ngay ASC, a.gio ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Bao>("SELECT * FROM GetBaos(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    } 
    public IEnumerable<StormStatistics> GetStormStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<StormStatistics>("SELECT a.capdobao, a.tansuatxuathien, a.tencacconbao, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao, a.mamau FROM ( SELECT a.capbao AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao, c.color AS mamau FROM (  SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY a.tenbao, a.capbao, a.year ) a LEFT JOIN Color c ON c.name = a.capbao GROUP BY a.capbao, c.color ) a, ( SELECT COUNT(a.tenbao) AS Tong FROM ( SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY a.tenbao, a.capbao, a.year ) a ) b"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<StormStatistics>("SELECT a.capdobao, a.tansuatxuathien, a.tencacconbao, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao, a.mamau FROM ( SELECT a.capbao AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao, c.color AS mamau FROM (  SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE a.MaHuyen = @_mahuyen GROUP BY a.tenbao, a.capbao, a.year ) a LEFT JOIN Color c ON c.name = a.capbao GROUP BY a.capbao, c.color ) a, ( SELECT COUNT(a.tenbao) AS Tong FROM ( SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE a.MaHuyen = @_mahuyen GROUP BY a.tenbao, a.capbao, a.year ) a ) b"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<StormStatistics>("SELECT a.capdobao, a.tansuatxuathien, a.tencacconbao, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao, a.mamau FROM ( SELECT a.capbao AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao, c.color AS mamau FROM (  SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE " + SqlQuery + " GROUP BY a.tenbao, a.capbao, a.year ) a LEFT JOIN Color c ON c.name = a.capbao GROUP BY a.capbao, c.color ) a, ( SELECT COUNT(a.tenbao) AS Tong FROM ( SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE " + SqlQuery + " GROUP BY a.tenbao, a.capbao, a.year ) a ) b");            
            }
            return connection.Query<StormStatistics>("SELECT a.capdobao, a.tansuatxuathien, a.tencacconbao, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao, a.mamau FROM ( SELECT a.capbao AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao, c.color AS mamau FROM (  SELECT a.tenbao, a.capbao, a.year FROM Bao a GROUP BY a.tenbao, a.capbao, a.year ) a LEFT JOIN Color c ON c.name = a.capbao GROUP BY a.capbao, c.color ) a, ( SELECT COUNT(a.tenbao) AS Tong FROM ( SELECT a.tenbao, a.capbao, a.year FROM Bao a GROUP BY a.tenbao, a.capbao, a.year ) a ) b");  
        }       
    }   
   public BaoDetail? GetBao(int objectid){
        return connection.QueryFirstOrDefault<BaoDetail>("SELECT * FROM GetBao(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM Bao", commandType: CommandType.Text);
    }    
    public int Add(Bao obj){ 
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
                return connection.ExecuteScalar<int>("AddBao", 
                    new{
                        _objectid = obj.objectid,
                        _idbao = obj.tenbao + "-" + Convert.ToInt32(date[0]),
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddBao", 
                    new{
                        _objectid = obj.objectid,
                        _idbao = obj.tenbao + "-" + Convert.ToInt32(date[0]),
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
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
                return connection.ExecuteScalar<int>("AddBao", 
                    new{
                        _objectid = obj.objectid,
                        _idbao = obj.tenbao + "-",
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddBao", 
                    new{
                        _objectid = obj.objectid,
                        _idbao = obj.tenbao + "-",
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
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
    public int Edit(int objectid, Bao obj){ 
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
                return connection.ExecuteScalar<int>("EditBao", 
                    new{
                        _objectid = objectid,
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditBao", 
                    new{
                        _objectid = objectid,
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
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
                return connection.ExecuteScalar<int>("EditBao", 
                    new{
                        _objectid = objectid,
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
                        _ngaybatdau = ngaybatdau,
                        _ngayketthuc = ngayketthuc,
                        _centerid = obj.centerid,
                        _tenvn = obj.tenvn,
                        _kvahhcm = obj.kvahhcm
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditBao", 
                    new{
                        _objectid = objectid,
                        _tenbao = obj.tenbao,
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
                        _capbao = obj.capbao,                        
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
        return connection.ExecuteScalar<int>("DeleteBao", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }

}

 


