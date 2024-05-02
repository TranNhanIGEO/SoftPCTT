using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;
public class MucNuocRepository : BaseRepository{
    public MucNuocRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<MucNuoc> GetMucNuocs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<MucNuoc>("SELECT a.objectid, a.idtrammucnuoc, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.mucnuoc, a.docaodinhtrieu, a.docaochantrieu, a.baodongi, a.baodongii, a.baodongiii,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM MucNuoc a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE (" + SqlQuery + ") AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<MucNuoc>("SELECT * FROM GetMucNuocs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<MucNuoc>("SELECT a.objectid, a.idtrammucnuoc, a.tentram, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.mucnuoc, a.docaodinhtrieu, a.docaochantrieu, a.baodongi, a.baodongii, a.baodongiii,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM MucNuoc a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC" + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<MucNuoc>("SELECT * FROM GetMucNuocs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public MucNuoc? GetMucNuoc(int objectid){
        return connection.QueryFirstOrDefault<MucNuoc>("SELECT * FROM GetMucNuoc(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM MucNuoc", commandType: CommandType.Text);
    }   
    public int Add(MucNuoc obj){  
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? mucnuoc = obj.mucnuoc == null ? null : Convert.ToDouble(obj.mucnuoc);
        double? docaodinhtrieu = obj.docaodinhtrieu == null ? null : Convert.ToDouble(obj.docaodinhtrieu);
        double? docaochantrieu = obj.docaochantrieu == null ? null : Convert.ToDouble(obj.docaochantrieu);
        double? baodongi = obj.baodongi == null ? null : Convert.ToDouble(obj.baodongi);  
        double? baodongii = obj.baodongii == null ? null : Convert.ToDouble(obj.baodongii);
        double? baodongiii = obj.baodongiii == null ? null : Convert.ToDouble(obj.baodongiii);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddMucNuoc", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammucnuoc = "MN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("AddMucNuoc", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammucnuoc = "MN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("AddMucNuoc", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammucnuoc = "MN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("AddMucNuoc", 
                    new{
                        _objectid = obj.objectid,
                        _idtrammucnuoc = "MN" +  obj.objectid.ToString(),
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
    public int Edit(int objectid, MucNuoc obj){  
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? mucnuoc = obj.mucnuoc == null ? null : Convert.ToDouble(obj.mucnuoc);
        double? docaodinhtrieu = obj.docaodinhtrieu == null ? null : Convert.ToDouble(obj.docaodinhtrieu);
        double? docaochantrieu = obj.docaochantrieu == null ? null : Convert.ToDouble(obj.docaochantrieu);
        double? baodongi = obj.baodongi == null ? null : Convert.ToDouble(obj.baodongi);  
        double? baodongii = obj.baodongii == null ? null : Convert.ToDouble(obj.baodongii);
        double? baodongiii = obj.baodongiii == null ? null : Convert.ToDouble(obj.baodongiii);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditMucNuoc", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("EditMucNuoc", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngay,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("EditMucNuoc", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
                return connection.ExecuteScalar<int>("EditMucNuoc", 
                    new{
                        _objectid = objectid,
                        _tentram = obj.tentram,
                        _gio = obj.gio,
                        _ngay = ngaynull,
                        _toadox = nulltoado,
                        _toadoy = nulltoado,
                        _mucnuoc = mucnuoc,
                        _docaodinhtrieu = docaodinhtrieu,
                        _docaochantrieu = docaochantrieu,
                        _baodongi = baodongi,
                        _baodongii = baodongii,
                        _baodongiii = baodongiii,
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
        return connection.ExecuteScalar<int>("DeleteMucNuoc", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<TidalDetailStatistics> GetTidalDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TidalDetailStatistics>(
                    "SELECT x.year AS nam, z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM ( SELECT a.year, a.min AS docaochantrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM (SELECT a.year, MIN(a.docaochantrieu) FROM MucNuoc a WHERE  " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.min = b.docaochantrieu GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS docaodinhtrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.docaodinhtrieu) FROM MucNuoc a WHERE  " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.max = b.docaodinhtrieu GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM MucNuoc a WHERE  " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<TidalDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM ( SELECT a.year, a.min AS docaochantrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM (SELECT a.year, MIN(a.docaochantrieu) FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.min = b.docaochantrieu GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS docaodinhtrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.docaodinhtrieu) FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.max = b.docaodinhtrieu GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"
                , new{
                    _mahuyen = mahuyen
            });        
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TidalDetailStatistics>(
                "SELECT x.year AS nam, z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM ( SELECT a.year, a.min AS docaochantrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM (SELECT a.year, MIN(a.docaochantrieu) FROM MucNuoc a WHERE  " + SqlQuery + " AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.min = b.docaochantrieu GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS docaodinhtrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.docaodinhtrieu) FROM MucNuoc a WHERE  " + SqlQuery + " AND a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.max = b.docaodinhtrieu GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM MucNuoc a WHERE  " + SqlQuery + " AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC");
            }
            return connection.Query<TidalDetailStatistics>(
            "SELECT x.year AS nam, z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM ( SELECT a.year, a.min AS docaochantrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM (SELECT a.year, MIN(a.docaochantrieu) FROM MucNuoc a WHERE a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.min = b.docaochantrieu GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS docaodinhtrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.docaodinhtrieu) FROM MucNuoc a WHERE a.docaodinhtrieu != 0 GROUP BY a.year ) a LEFT JOIN MucNuoc b ON b.year = a.year AND a.max = b.docaodinhtrieu GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM MucNuoc a WHERE a.docaodinhtrieu != 0 AND a.docaochantrieu != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC"); 
        }     
    }
    public IEnumerable<TidalTotalStatistics> GetTidalTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<TidalTotalStatistics>(
                    "SELECT z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM (SELECT a.docaochantrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaochantrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaochantrieu IN(SELECT MIN(a.docaochantrieu) from (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaochantrieu != 0) a ) GROUP BY a.ngay, a.docaochantrieu ORDER BY a.ngay ASC) a GROUP BY a.docaochantrieu) x, (SELECT a.docaodinhtrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaodinhtrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE  " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu IN( SELECT MAX(a.docaodinhtrieu) from (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu != 0) a ) GROUP BY a.ngay, a.docaodinhtrieu ORDER BY a.ngay ASC) a GROUP BY a.docaodinhtrieu) y, ( SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0) a) z"
                    , new{
                        _mahuyen = mahuyen
                });
            }
            return connection.Query<TidalTotalStatistics>(
                "SELECT z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM (SELECT a.docaochantrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaochantrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaochantrieu IN(SELECT MIN(a.docaochantrieu) from (SELECT * FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaochantrieu != 0) a ) GROUP BY a.ngay, a.docaochantrieu ORDER BY a.ngay ASC) a GROUP BY a.docaochantrieu) x, (SELECT a.docaodinhtrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaodinhtrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE  mahuyen = @_mahuyen AND a.docaodinhtrieu IN( SELECT MAX(a.docaodinhtrieu) from (SELECT * FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaodinhtrieu != 0) a ) GROUP BY a.ngay, a.docaodinhtrieu ORDER BY a.ngay ASC) a GROUP BY a.docaodinhtrieu) y, ( SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM (SELECT * FROM MucNuoc a WHERE mahuyen = @_mahuyen AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0) a) z"
                , new{
                    _mahuyen = mahuyen
            });        
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<TidalTotalStatistics>(
                "SELECT z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM (SELECT a.docaochantrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaochantrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE " + SqlQuery + " AND a.docaochantrieu IN(SELECT MIN(a.docaochantrieu) from (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND a.docaochantrieu != 0) a ) GROUP BY a.ngay, a.docaochantrieu ORDER BY a.ngay ASC) a GROUP BY a.docaochantrieu) x, (SELECT a.docaodinhtrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaodinhtrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE  " + SqlQuery + " AND a.docaodinhtrieu IN( SELECT MAX(a.docaodinhtrieu) from (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND a.docaodinhtrieu != 0) a ) GROUP BY a.ngay, a.docaodinhtrieu ORDER BY a.ngay ASC) a GROUP BY a.docaodinhtrieu) y, ( SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM (SELECT * FROM MucNuoc a WHERE " + SqlQuery + " AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0) a) z");
            }
            return connection.Query<TidalTotalStatistics>(
            "SELECT z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh FROM (SELECT a.docaochantrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaochantrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE a.docaochantrieu IN(SELECT MIN(a.docaochantrieu) from (SELECT * FROM MucNuoc a WHERE a.docaochantrieu != 0) a ) GROUP BY a.ngay, a.docaochantrieu ORDER BY a.ngay ASC) a GROUP BY a.docaochantrieu) x, (SELECT a.docaodinhtrieu, STRING_AGG(a.ngay, '; ') AS Ngay FROM (SELECT a.docaodinhtrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a WHERE a.docaodinhtrieu IN( SELECT MAX(a.docaodinhtrieu) from (SELECT * FROM MucNuoc a WHERE a.docaodinhtrieu != 0) a ) GROUP BY a.ngay, a.docaodinhtrieu ORDER BY a.ngay ASC) a GROUP BY a.docaodinhtrieu) y, ( SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb FROM (SELECT * FROM MucNuoc a WHERE a.docaodinhtrieu != 0 AND a.docaochantrieu != 0) a) z"); 
        }     
    }
}